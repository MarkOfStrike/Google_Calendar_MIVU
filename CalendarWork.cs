using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Google_Calendar_Desktop_App
{
    public class CalendarWork
    {
        /// <summary>
        /// Данные пользователя
        /// </summary>
        private CalendarService Service;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string User { get; private set; }

        /// <summary>
        /// Сообщение об ошибки
        /// </summary>
        private bool ErrorMsg = true;

        /// <summary>
        /// Соединение с интернетом
        /// </summary>
        private bool isConnect { get; set; }

        /// <summary>
        /// Таймер
        /// </summary>
        private System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        /// <summary>
        /// Набор символов для идентификатора
        /// </summary>
        private char[] Identity;

        /// <summary>
        /// Рандом
        /// </summary>
        private Random ran = new Random();

        /// <summary>
        /// Инициализация класса
        /// </summary>
        /// <param name="keyFilePath"></param>
        /// <param name="user"></param>
        public CalendarWork(string keyFilePath, string user, string pass)
        {
            User = user;
            Identity = NewIdent();

            Check_Connect();

            timer1.Interval = 5000;
            timer1.Tick += TimerCheck_Tick;

            StartTimer();

            try
            {
                string[] Scopes = {

                    CalendarService.Scope.Calendar,
                    CalendarService.Scope.CalendarEvents

                };

                UserCredential credential;

                using (var stream = new FileStream(keyFilePath, FileMode.Open, FileAccess.Read))
                {
                    string credPath = "UsersToken";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets, Scopes, User, CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                }

                Service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "My Calendar v0.1",
                });


                WorkBD.Execution_query($"if not exists(select Id from Users where UserName = N'{user}') insert into Users (UserName, userPass) values (N'{user}', N'{pass}')");

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// Собтие тика таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerCheck_Tick(object sender, EventArgs e)
        {
            Check_Connect();
        }

        /// <summary>
        /// Запустить таймер
        /// </summary>
        public void StartTimer()
        {
            timer1.Start();
        }

        /// <summary>
        /// Остановить таймер
        /// </summary>
        public void StopTimer()
        {
            timer1.Stop();
            isConnect = false;
        }

        /// <summary>
        /// Проверка на соединение с интернетом
        /// </summary>
        /// <returns>Соединение с интернетом</returns>
        public bool IsConnect()
        {
            return isConnect;
        }

        /// <summary>
        /// Инициализация класса
        /// </summary>
        public CalendarWork()
        {
            StartTimer();
        }

        /// <summary>
        /// Получение всех событий
        /// </summary>
        /// <param name="calendarName">Идентификатор календаря</param>
        /// <returns>Все события календаря</returns>
        public List<Event> GetEvents(string calendarName = "primary")
        {
            List<Event> events = new List<Event>();

            if (isConnect)
            {
                EventsResource.ListRequest request = Service.Events.List(calendarName);//В скобках название календаря 'primary' использует календарь по умолчанию
                request.TimeMin = new DateTime(1970, 1, 1, 0, 0, 0);
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = 2500;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                Events ev = request.Execute();

                events.AddRange(ev.Items.ToList());

            }
            else
            {

                if (calendarName == "primary")
                {
                    calendarName = "primary:true";
                }

                var event_s = WorkBD.Select_query($"select case ec.Id_Event when me.Id_Mod_Event then case a.NameId when me.New_Calendar then me.New_Event end else ec.Calendar_event end  from Events_calendar ec, Activity a left join Mod_Event me on me.RecordId = a.Id where ec.Id = a.EvendId and a.NameId = (select Id from Name_Calendar where Id_Calendar = N'{calendarName}' and Id_User = (select Id from Users where UserName = N'{User}'))");

                var event_mod = WorkBD.Select_query($"select me.New_Event from Mod_Event me, Activity a where me.New_Calendar = (select Id from Name_Calendar where Id_Calendar = N'{calendarName}' and Id_User = (select Id from Users where UserName = N'{User}')) and a.Id = me.RecordId and me.New_Calendar != a.NameId");

                var event_sLocal = WorkBD.Select_query($"select New_Ev from Ins_Event where Cal_Id = (select id from Name_Calendar where calendar_name like N'%{calendarName}%' and Id_User = (select Id from Users where UserName = N'{User}'))");

                foreach (DataRow even in event_s.Rows)
                {
                    if (!string.IsNullOrWhiteSpace(even.ItemArray[0].ToString()))
                    {
                        events.Add(JsonConvert.DeserializeObject<Event>(even.ItemArray[0].ToString()));
                    }

                }

                foreach (DataRow evenMod in event_mod.Rows)
                {
                    events.Add(JsonConvert.DeserializeObject<Event>(evenMod.ItemArray[0].ToString()));
                }

                foreach (DataRow evenLocal in event_sLocal.Rows)
                {
                    events.Add(JsonConvert.DeserializeObject<Event>(evenLocal.ItemArray[0].ToString()));
                }

            }

            return events;
        }

        /// <summary>
        /// Создание нового события в календаре
        /// </summary>
        /// <param name="summary">Заголовок события</param>
        /// <param name="start">Дата начала</param>
        /// <param name="end">Дата конца</param>
        /// <param name="calendarName">Идентификатор календаря</param>
        /// <param name="location">Место проведения</param>
        /// <param name="Emails">Адреса упоминаний</param>
        public void CreateEvent(string summary, DateTime start, DateTime end, string calendarName, string description = null, List<string> Emails = null, string location = null)
        {
            Event body = new Event();


            if (Emails != null)
            {
                List<EventAttendee> attendees = new List<EventAttendee>();
                foreach (var mail in Emails)
                {
                    attendees.Add(new EventAttendee { Email = mail });
                }

                body.Attendees = attendees;
            }



            EventDateTime _start = new EventDateTime();
            _start.Date = start.Date.ToString("yyyy-MM-dd");

            EventDateTime _end = new EventDateTime();
            _end.Date = end.Date.ToString("yyyy-MM-dd");

            body.Start = _start;
            body.End = _end;
            body.Location = location;
            body.Summary = summary;
            body.Description = description;


            if (isConnect)
            {
                EventsResource.InsertRequest request = new EventsResource.InsertRequest(Service, body, calendarName);

                Event response = request.Execute();

            }
            else
            {
                for (int i = 0; i < 16; i++)
                {
                    body.Id += Identity[ran.Next(0, Identity.Length)].ToString();
                }

                WorkBD.Execution_query($"insert into Ins_Event (Cal_Id, New_Ev, Id_New_Ev) values ((select Id from Name_Calendar where Id_Calendar = N'{calendarName}' and Id_User = (select Id from Users where UserName = N'{User}')), N'{JsonConvert.SerializeObject(body)}', N'{body.Id}')");

            }



        }

        /// <summary>
        /// Создание нового события в календаре
        /// </summary>
        /// <param name="event">Событие</param>
        /// <param name="calendarId">Id календаря</param>
        public void CreateEvent(Event newEvent, string calendarId)
        {
            EventsResource.InsertRequest request = new EventsResource.InsertRequest(Service, newEvent, calendarId);
            Event response = request.Execute();

        }

        /// <summary>
        /// Удаление событие календаря
        /// </summary>
        /// <param name="eventForDel">Событие</param>
        public void DeleteEvent(Event eventForDel, string calendarName)
        {
            if (isConnect)
            {
                EventsResource.DeleteRequest request = new EventsResource.DeleteRequest(Service, calendarName, eventForDel.Id);
                WorkBD.Execution_query($"delete from Events_calendar where Id_Event = N'{eventForDel.Id}'");
                request.Execute();
            }
            else
            {
                int idEvent = 0;

                var result = WorkBD.Select_query($"select Id from Events_Calendar where Id_Event = N'{eventForDel.Id}'");

                if (result.Rows.Count > 0)
                {

                    idEvent = int.Parse(result.Rows[0].ItemArray[0].ToString());
                    WorkBD.Execution_query($"insert into Del_Event (Event_Cal_id) values ((select Id from Activity where EvendId = {idEvent}))");
                }
                else
                {
                    var nextVariant = WorkBD.Select_query($"select Id from Ins_Event where Id_New_Ev = N'{eventForDel.Id}'");

                    if (nextVariant.Rows.Count > 0)
                    {

                        idEvent = int.Parse(nextVariant.Rows[0].ItemArray[0].ToString());

                        WorkBD.Execution_query($"delete from Ins_Event where Id = {idEvent}");

                    }

                }
            }

        }

        /// <summary>
        /// Обновление записи календаря
        /// </summary>
        /// <param name="new_event">Новая запись</param>
        /// <param name="calendar">Календарь</param>
        /// <param name="source_event">Исходная запись</param>
        public void UpdateEvent(Event new_event, string sourceCalendarId, string newCalendarId = null)
        {
            if (isConnect)
            {
                EventsResource.UpdateRequest request = new EventsResource.UpdateRequest(Service, new_event, sourceCalendarId, new_event.Id);
                Event resource = request.Execute();
                WorkBD.Execution_query($"delete from Events_calendar where Id_Event = N'{resource.Id}'");
                if (newCalendarId != null)
                {
                    MoveEvent(resource.Id, sourceCalendarId, newCalendarId);
                }
            }
            else
            {
                WorkBD.Execution_query($"if exists(select Id from Events_calendar where Id_Event = N'{new_event.Id}') if exists(select Id from Mod_Event where Id_Mod_Event = N'{new_event.Id}') update Mod_Event set New_Event = N'{JsonConvert.SerializeObject(new_event)}', New_Calendar = (select Id from Name_Calendar where Id_Calendar = N'{newCalendarId}' and Id_User = (select Id from Users where UserName = N'{User}')) where Id_Mod_Event = N'{new_event.Id}' else insert into Mod_Event (RecordId, New_Event, New_Calendar, Id_Mod_Event) values ((select Id from Activity where EvendId = (select Id from Events_calendar where Id_Event = N'{new_event.Id}')), N'{JsonConvert.SerializeObject(new_event)}', (select Id from Name_Calendar where Id_Calendar = N'{newCalendarId}' and Id_User = (select Id from Users where UserName = N'{User}')), N'{new_event.Id}') else update Ins_Event set New_Ev = N'{JsonConvert.SerializeObject(new_event)}', Cal_Id = (select Id from Name_Calendar where Id_Calendar = N'{newCalendarId ?? sourceCalendarId}' and Id_User = (select Id from Users where UserName = N'{User}')) where Id_New_Ev = N'{new_event.Id}'");
                WorkBD.Execution_query($"if exists(select me.Id from Mod_Event me, Events_calendar ec where me.New_Event = ec.Calendar_event and me.Id_Mod_Event = N'{new_event.Id}' and ec.Id_Event = N'{new_event.Id}' and me.New_Calendar = (select ac.NameId from Activity ac where ac.EvendId = (select Id from Events_calendar where Id_Event = N'{new_event.Id}'))) delete from Mod_Event where Id_Mod_Event = N'{new_event.Id}'");

            }

        }

        /// <summary>
        /// Перемещение события в другой календарь
        /// </summary>
        /// <param name="sourceEventId">Идентификатор события</param>
        /// <param name="sourceCalendarId">Идентификатор исходного календаря</param>
        /// <param name="newCalendarId">Идентификатор нового календаря</param>
        private void MoveEvent(string sourceEventId, string sourceCalendarId, string newCalendarId)
        {
            EventsResource.MoveRequest request = new EventsResource.MoveRequest(Service, sourceCalendarId, sourceEventId, newCalendarId);
            request.Execute();
        }

        /// <summary>
        /// Получение списка календарей
        /// </summary>
        /// <returns></returns>
        public List<CalendarListEntry> GetCalendarsName()
        {
            CalendarList result = null;

            if (isConnect)
            {
                var request = Service.CalendarList.List();
                result = request.Execute();

                result.Items.OrderBy(x => x.Summary);

                ErrorMsg = true;

                return result.Items.ToList().OrderBy(x => x.AccessRole).ThenBy(p => p.Summary).ToList();

            }
            else
            {
                List<CalendarListEntry> calendarLists = new List<CalendarListEntry>();


                var caList = WorkBD.Select_query($"select calendar_name from Name_Calendar where Id_User = (select Id from Users where UserName = N'{User}')");

                foreach (DataRow rowcalendar in caList.Rows)
                {
                    calendarLists.Add(JsonConvert.DeserializeObject<CalendarListEntry>(rowcalendar.ItemArray[0].ToString()));
                }

                if (calendarLists.Count == 0)
                {
                    if (ErrorMsg)
                    {
                        MessageBox.Show("Отсутствует список календарей в локальной базе данных! Пожалуйста подключитесь к интернету и перезапустите программу для синхронизации ланных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ErrorMsg = false;
                    }
                }

                return calendarLists.OrderBy(x => x.AccessRole).ThenBy(p => p.Summary).ToList();

            }

        }

        /// <summary>
        /// Проверка интернет соединения
        /// </summary>
        /// <returns></returns>
        private void Check_Connect()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    isConnect = true;
            }
            catch
            {
                isConnect = false;
            }
        }

        /// <summary>
        /// Набор символов для идентификаторов
        /// </summary>
        /// <returns>Массив символов</returns>
        private char[] NewIdent()
        {
            List<char> mas = new List<char>();

            for (int i = 0; i < 26; i++)
            {
                mas.Add((char)(65 + i));
                mas.Add((char)(97 + i));
            }

            for (int q = 0; q < 10; q++)
            {
                mas.Add((char)(48 + q));
            }

            return mas.ToArray();
        }

        /// <summary>
        /// Получение преобразованной даты даты события
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public DateTime DateEvent(EventDateTime dateTime)
        {
            if (string.IsNullOrWhiteSpace(dateTime.Date))
            {
                if (string.IsNullOrWhiteSpace(dateTime.DateTime.ToString()))
                {
                    if (string.IsNullOrWhiteSpace(dateTime.DateTimeRaw))
                    {
                        return new DateTime(1970, 1, 1, 0, 0, 0);
                    }
                    else
                    {
                        return Convert.ToDateTime(dateTime.DateTimeRaw);
                    }
                }
                else
                {
                    return Convert.ToDateTime(dateTime.DateTime.ToString());
                }
            }
            else
            {
                return Convert.ToDateTime(dateTime.Date);
            }


        }

        /// <summary>
        /// Сортировка событий календаря
        /// </summary>
        /// <param name="events">Список событий</param>
        /// <returns>Отсортированный список событий</returns>
        public List<Event> SortEvents(List<Event> events)
        {
            Event tmpEvent;

            for (int i = 0; i < events.Count; i++)
            {
                for (int j = i + 1; j < events.Count; j++)
                {
                    if (DateEvent(events[i].Start) > DateEvent(events[j].Start))
                    {
                        tmpEvent = events[i];
                        events[i] = events[j];
                        events[j] = tmpEvent;
                    }
                }
            }

            return events;
        }

    }
}
