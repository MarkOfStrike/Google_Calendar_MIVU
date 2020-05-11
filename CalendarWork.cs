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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Google_Calendar_Desktop_App
{
    public class CalendarWork
    {
        /// <summary>
        /// Данные пользователя
        /// </summary>
        private CalendarService Service { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Соединение с интернетом
        /// </summary>
        public static bool isConnect { get; set; }

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
        public CalendarWork(string keyFilePath, string user)
        {
            User = user;
            Identity = NewIdent();

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
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets, Scopes, user, CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                }

                Service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "My Calendar v0.1",
                });

                MessageBox.Show("Авторизация выполнена!");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"На данный момент подключение к интернету отсутствует \n{ex.Message}");
            }
        }

        /// <summary>
        /// Инициализация класса
        /// </summary>
        public CalendarWork()
        {
            StartTimer();
        }

        /// <summary>
        /// Таймер
        /// </summary>
        private static void StartTimer()
        {
            TimerCallback tm = new TimerCallback(Check_Connect);
            System.Threading.Timer timer = new System.Threading.Timer(tm, isConnect, 0, 5000);
        }

        ///// <summary>
        ///// Получение определенного количества событий
        ///// </summary>
        ///// <param name="calendarName">Идентификатор календаря</param>
        ///// <param name="maxRes">Количество результатов</param>
        ///// <returns>События</returns>
        //public Events GetEvents(string calendarName = "primary", int maxRes = 10)
        //{
        //    Events events = null;

        //    if (isConnect)
        //    {
        //        EventsResource.ListRequest request = Service.Events.List(calendarName);//В скобках название календаря 'primary' использует календарь по умолчанию
        //        request.TimeMin = DateTime.Now;
        //        request.ShowDeleted = false;
        //        request.SingleEvents = true;
        //        request.MaxResults = maxRes;
        //        request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

        //        events = request.Execute();
        //    }
        //    else
        //    {
        //        if (calendarName == "primary")
        //        {
        //            calendarName = "primary:true";
        //        }

        //        var event_s = WorkBD.Select_query($"select top({maxRes}) ec.Calendar_event from Activity a, Name_Calendar nc, Events_calendar ec where ec.Id = a.EvendId and Date_Event > '{DateTime.Now}'  and a.NameId = (select id from Name_Calendar where calendar_name like '%{calendarName}%') order by Date_Event");

        //        List<Event> ev = new List<Event>();

        //        while (event_s.Read())
        //        {
        //            events.Items.Add(JsonConvert.DeserializeObject<Event>(event_s.GetString(0)));
        //        }

        //        events.Items.OrderBy(x => x.Start);



        //    }

            
        //    return events;

        //}

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
                request.TimeMin = new DateTime(1970, 1, 1, 0,0,0);
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

                var event_s = WorkBD.Select_query($"select case when cast(ec.Id_Event as nvarchar(max)) = cast(me.Id_Mod_Event as nvarchar(max))  then me.New_Event else ec.Calendar_event end from Events_calendar ec, Mod_Event me, Activity a, Name_Calendar nc where ec.Id = a.EvendId and nc.Id = a.NameId and not exists(select * from Del_Event where Event_Cal_id = a.Id) and a.NameId = (select id from Name_Calendar where calendar_name like N'%{calendarName}%')");

                var event_sLocal = WorkBD.Select_query($"select New_Ev from Ins_Event where Cal_Id = (select id from Name_Calendar where calendar_name like N'%{calendarName}%')");
                
                foreach (DataRow even in event_s.Rows)
                {
                    events.Add(JsonConvert.DeserializeObject<Event>(even.ItemArray[0].ToString()));
                }

                foreach (DataRow evenLocal in event_sLocal.Rows)
                {
                    events.Add(JsonConvert.DeserializeObject<Event>(evenLocal.ItemArray[0].ToString()));
                }

                

            }

            events.OrderBy(x => x.Start);

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
            _start.Date = start.Date.ToString();

            EventDateTime _end = new EventDateTime();
            _end.Date = end.Date.ToString();

            body.Start = _start;
            body.End = _end;
            body.Location = location;
            body.Summary = summary;
            body.Description = description;


            if (isConnect)
            {
                EventsResource.InsertRequest request = new EventsResource.InsertRequest(Service, body, calendarName);

                Event response = request.Execute();

                MessageBox.Show("Событие создано!");
            }
            else
            {
                for (int i = 0; i < 16; i++)
                {
                    body.Id += Identity[ran.Next(0, Identity.Length)].ToString();
                }

                WorkBD.Execution_query($"insert into Ins_Event (Cal_Id, New_Ev, Id_New_Ev) values ((select Id from Name_Calendar where Id_Calendar = N'{calendarName}'), N'{JsonConvert.SerializeObject(body)}', N'{body.Id}')");

                MessageBox.Show("Запись добавлена в локальную базу данных!");
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
                //WorkBD.Open_con();
                WorkBD.Execution_query($"delete from Events_calendar where Id_Event = N'{eventForDel.Id}'");
                //WorkBD.Close_con();
                //Event response = request.Execute();
                request.Execute();

                //MessageBox.Show("Событие удалено!");
            }
            else
            {
                int idEvent = 0;

                var result = WorkBD.Select_query($"select Id from Events_Calendar where Id_Event = N'{eventForDel.Id}'");

                if (result.Rows.Count > 0)
                {

                    idEvent = int.Parse(result.Rows[0].ItemArray[0].ToString());

                    WorkBD.Execution_query($"insert into Del_Event (Event_Cal_id) values ((select Id from Activity where EvendId = {idEvent}))");

                    MessageBox.Show("Событие добавлено на очередь для удаления!");

                }
                else
                {
                    var nextVariant = WorkBD.Select_query($"select Id from Ins_Event where Id_New_Ev = N'{eventForDel.Id}'");

                    if (nextVariant.Rows.Count > 0)
                    {

                        idEvent = int.Parse(nextVariant.Rows[0].ItemArray[0].ToString());

                        WorkBD.Execution_query($"delete from Ins_Event where Id = {idEvent}");

                        MessageBox.Show("Событие удалено из локальной базы данных");

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
        public void UpdateEvent(Event new_event, CalendarListEntry calendar, string newCalendarId = null, Event source_event = null)
        {
            if (isConnect)
            {
                EventsResource.UpdateRequest request = new EventsResource.UpdateRequest(Service, new_event, calendar.Id, new_event.Id);//Вместо User использовать название календаря
                Event resource = request.Execute();
                if (newCalendarId != null)
                {
                    MoveEvent(resource.Id, calendar.Id, newCalendarId);
                }
                //MessageBox.Show("Красава, получилось");
            }
            else
            {
                //var checkEvent = WorkBD.Select_query($"select");


                WorkBD.Execution_query($"if exists(select Id from Events_calendar where Id_Event = N'{new_event.Id}') if exists(select Id from Mod_Event where Id_Mod_Event = N'{new_event.Id}') update Mod_Event set New_Event = N'{JsonConvert.SerializeObject(new_event)}' where Id_Mod_Event = N'{new_event.Id}' else insert into Mod_Event (RecordId, New_Event, New_Calendar, Id_Mod_Event) values ((select Id from Activity where EvendId = (select Id from Events_calendar where Id_Event = N'{new_event.Id}')), N'{JsonConvert.SerializeObject(new_event)}', (select Id from Name_Calendar where Id_Calendar = N'{newCalendarId}'), N'{new_event.Id}') else update Ins_Event set New_Ev = N'{JsonConvert.SerializeObject(new_event)}' where Id_New_Ev = N'{new_event.Id}'");
                WorkBD.Execution_query($"if exists(select me.Id from Mod_Event me, Events_calendar ec where me.New_Event = ec.Calendar_event and me.Id_Mod_Event = N'{new_event.Id}' and ec.Id_Event = N'{new_event.Id}') delete from Mod_Event where Id_Mod_Event = N'{new_event.Id}'");


                //WorkBD.Execution_query($"insert into Mod_Event (RecordId, New_Event, New_Calendar) values ((select Id from Activity where EvendId = (select Id from Events_calendar where Calendar_event = N'{JsonConvert.SerializeObject(source_event)}')), N'{JsonConvert.SerializeObject(new_event)}', (select Id from Name_Calendar where calendar_name = N'{JsonConvert.SerializeObject(calendar)}'))");
            }


            

            MessageBox.Show("Событие обновлено");

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

                return result.Items.ToList();

            }
            else
            {
                List<CalendarListEntry> calendarLists = new List<CalendarListEntry>();


                var caList = WorkBD.Select_query($"select calendar_name from Name_Calendar");

                foreach (DataRow rowcalendar in caList.Rows)
                {
                    calendarLists.Add(JsonConvert.DeserializeObject<CalendarListEntry>(rowcalendar.ItemArray[0].ToString()));
                }

                calendarLists.OrderBy(x => x.Summary);

                return calendarLists;

                //caList.Close();
            }


            
            
            
        }

        /// <summary>
        /// Проверка интернет соединения
        /// </summary>
        /// <returns></returns>
        private static void Check_Connect(object obj)
        {

            isConnect = false;

            #region Проверка соединения

            //try
            //{
            //    using (var client = new WebClient())
            //    using (client.OpenRead("http://google.com/generate_204"))
            //        isConnect = true;
            //}
            //catch
            //{
            //    isConnect = false;
            //}

            #endregion

        }

        /// <summary>
        /// Набор символов для идентификаторов
        /// </summary>
        /// <returns>Массив символов</returns>
        private char [] NewIdent()
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
                        return new DateTime(1970, 1, 1, 0,0,0);
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

    }
}
