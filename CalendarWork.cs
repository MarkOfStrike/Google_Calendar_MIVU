using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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



        public CalendarWork(string keyFilePath, string user)
        {
            User = user;

            try
            {
                string[] Scopes = {

                    CalendarService.Scope.Calendar,
                    CalendarService.Scope.CalendarEvents,
                    CalendarService.Scope.CalendarEventsReadonly

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



            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public Events GetEvents()
        {
            EventsResource.ListRequest request = Service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = request.Execute();

            


            return events;

        }

        /// <summary>
        /// Создание нового события в календаре
        /// </summary>
        /// <param name="summary">Заголовок события</param>
        /// <param name="start">Дата начала</param>
        /// <param name="end">Дата конца</param>
        /// <param name="location">Место проведения</param>
        /// <param name="Emails">Адреса упоминаний</param>
        public void CreateEvent(string summary, DateTime start, DateTime end, string location = null, List<string> Emails = null)
        {
            Event body = new Event();

            List<EventAttendee> attendees = new List<EventAttendee>();
            foreach (var mail in Emails)
            {
                attendees.Add(new EventAttendee { Email = mail });
            }

            body.Attendees = attendees;

            EventDateTime _start = new EventDateTime();
            _start.DateTime = start;

            EventDateTime _end = new EventDateTime();
            _end.DateTime = end;

            body.Start = _start;
            body.End = _end;
            body.Location = location;
            body.Summary = summary;

            try
            {
                EventsResource.InsertRequest request = new EventsResource.InsertRequest(Service, body, User);
                Event response = request.Execute();

                MessageBox.Show("Событие создано!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Удаление событие календаря
        /// </summary>
        /// <param name="event">Событие</param>
        public void DeleteEvent(Event @event)
        {
            EventsResource.DeleteRequest request = new EventsResource.DeleteRequest(Service, User, @event.Id);
            //Event response = request.Execute();
            request.Execute();

            MessageBox.Show("Событие удалено!");
        }


        public void UpdateEvent(Event @event)
        {
            EventsResource.UpdateRequest request = new EventsResource.UpdateRequest(Service, @event, User, @event.Id);
            Event resource = request.Execute();

            MessageBox.Show("Событие обновлено");

        }




    }
}
