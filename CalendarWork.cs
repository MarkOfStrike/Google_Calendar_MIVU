using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        private CalendarService Service { get; set; }
        public string User { get; set; }
        //public bool Connect { get; set; }
        public static bool Connect { get; set; }
        //public List<char> Identity = new List<char>();
        public char[] Identity;

        //private WorkBD wbd = new WorkBD();
        private Random ran = new Random();

        public CalendarWork(string keyFilePath, string user)
        {
            User = user;

            StartTimer();

            try
            {
                string[] Scopes = {

                    CalendarService.Scope.Calendar,
                    CalendarService.Scope.CalendarEvents
                    //CalendarService.Scope.CalendarEventsReadonly

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

        public CalendarWork()
        {
            StartTimer();
        }

        private static void StartTimer()
        {
            TimerCallback tm = new TimerCallback(Check_Connect);
            System.Threading.Timer timer = new System.Threading.Timer(tm, Connect, 0, 5000);
        }

        public Events GetEvents(string calendarName = "primary", int maxRes = 10)
        {
            Events events = null;

            if (Connect)
            {
                EventsResource.ListRequest request = Service.Events.List(calendarName);//В скобках название календаря 'primary' использует календарь по умолчанию
                request.TimeMin = DateTime.Now;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = maxRes;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                events = request.Execute();
            }
            else
            {
                if (calendarName == "primary")
                {
                    calendarName = "primary:true";
                }

                var event_s = WorkBD.Select_query($"select top({maxRes}) ec.Calendar_event from Activity a, Name_Calendar nc, Events_calendar ec where ec.Id = a.EvendId and Date_Event > '{DateTime.Now}'  and a.NameId = (select id from Name_Calendar where calendar_name like '%{calendarName}%') order by Date_Event");

                List<Event> ev = new List<Event>();

                while (event_s.Read())
                {
                    events.Items.Add(JsonConvert.DeserializeObject<Event>(event_s.GetString(0)));
                }

                events.Items.OrderBy(x => x.Start);

                

            }

            
            return events;

        }


        public Events GetAllEvents(string calendarName = "primaty")
        {
            Events events = null;

            if (Connect)
            {
                EventsResource.ListRequest request = Service.Events.List(calendarName);//В скобках название календаря 'primary' использует календарь по умолчанию
                request.TimeMin = new DateTime(2000, 1, 1, 0,0,0);
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = 2500;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                events = request.Execute();
            }
            else
            {
                if (calendarName == "primary")
                {
                    calendarName = "primary:true";
                }

                var event_s = WorkBD.Select_query($"select ec.Calendar_event from Activity a, Name_Calendar nc, Events_calendar ec where ec.Id = a.EvendId and Date_Event > '{DateTime.Now}'  and a.NameId = (select id from Name_Calendar where calendar_name like '%{calendarName}%') order by Date_Event");

                List<Event> ev = new List<Event>();

                while (event_s.Read())
                {
                    events.Items.Add(JsonConvert.DeserializeObject<Event>(event_s.GetString(0)));
                }

                event_s.Close();
                events.Items.OrderBy(x => x.Start);



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
            _start.DateTime = start;

            EventDateTime _end = new EventDateTime();
            _end.DateTime = end;

            body.Start = _start;
            body.End = _end;
            body.Location = location;
            body.Summary = summary;
            body.Description = description;


            if (Connect)
            {
                EventsResource.InsertRequest request = new EventsResource.InsertRequest(Service, body, calendarName);

                Event response = request.Execute();

                MessageBox.Show("Событие создано!");
            }
            else
            {
                for (int i = 0; i < 16; i++)
                {
                    body.Id += Identity[ran.Next(0, Identity.Length + 1)].ToString();
                }

                WorkBD.Execution_query($"insert into Ins_Event (Cal_Id, New_Ev) values ((select id from Name_Calendar where calendar_name like '&{calendarName}&'), {JsonConvert.SerializeObject(body)})");

                MessageBox.Show("Запись добавлена в локальную базу данных!");
            }



        }

       /// <summary>
       /// Создание нового события в календаре
       /// </summary>
       /// <param name="event">Событие</param>
       /// <param name="calendarId">Id календаря</param>
        public void CreateEvent(Event @event, string calendarId)
        {
                EventsResource.InsertRequest request = new EventsResource.InsertRequest(Service, @event, calendarId);
                Event response = request.Execute();
            
        }



        /// <summary>
        /// Удаление событие календаря
        /// </summary>
        /// <param name="event">Событие</param>
        public void DeleteEvent(Event @event, string calendarName)
        {
            if (Connect)
            {
                EventsResource.DeleteRequest request = new EventsResource.DeleteRequest(Service, calendarName, @event.Id);
                //Event response = request.Execute();
                request.Execute();

                
                MessageBox.Show("Событие удалено!");
            }
            else
            {
                int idEvent = 0;

                var result = WorkBD.Select_query($"select id from Events_Calendar where Calendar_event = '{JsonConvert.SerializeObject(@event)}'");

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        idEvent = result.GetInt32(0);
                    }

                    result.Close();
                    WorkBD.Execution_query($"insert into Del_Event (Events_Cal_id) values ((select id from activity where EvendId = {idEvent}))");

                    MessageBox.Show("Событие добавлено на очередь для удаления!");

                }
                else
                {
                    var nextVariant = WorkBD.Select_query($"select id from Ins_Event where New_Ev = '{JsonConvert.SerializeObject(@event)}'");

                    if (nextVariant.HasRows)
                    {
                        while (nextVariant.Read())
                        {
                            idEvent = nextVariant.GetInt32(0);
                        }
                        nextVariant.Close();

                        WorkBD.Execution_query($"delete from Ins_Event where id = {idEvent}");

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
        public void UpdateEvent(Event new_event, CalendarListEntry calendar, Event source_event = null)
        {
            if (Connect)
            {
                EventsResource.UpdateRequest request = new EventsResource.UpdateRequest(Service, new_event, calendar.Id, new_event.Id);//Вместо User использовать название календаря
                Event resource = request.Execute();
            }
            else
            {
                WorkBD.Execution_query($"insert into Mod_Event (RecordId, New_Event, New_Calendar) values ((select Id from Activity where EvendId = (select Id from Events_calendar where Calendar_event = '{JsonConvert.SerializeObject(source_event)}')), '{JsonConvert.SerializeObject(new_event)}', (select Id from Name_Calendar where calendar_name = '{JsonConvert.SerializeObject(calendar)}'))");
            }


            

            MessageBox.Show("Событие обновлено");

        }


        /// <summary>
        /// Получение списка календарей
        /// </summary>
        /// <returns></returns>
        public CalendarList GetCalendarsName()
        {
            CalendarList result = null;

            if (Connect)
            {
                var request = Service.CalendarList.List();
                result = request.Execute();
            }
            else
            {
                var caList = WorkBD.Select_query($"select calendar_name from Name_Calendar");
                while (caList.Read())
                {
                    result.Items.Add(JsonConvert.DeserializeObject<CalendarListEntry>(caList.GetString(0)));
                }
                caList.Close();
            }

            
            return result;
        }



        /// <summary>
        /// Проверка интернет соединения
        /// </summary>
        /// <returns></returns>
        private static void Check_Connect(object obj)
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    Connect = true;
            }
            catch
            {
                Connect = false;
            }
        }



    }
}
