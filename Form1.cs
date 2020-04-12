using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;

namespace Google_Calendar_Desktop_App
{
    public partial class Form1 : Form
    {
        //static string[] Scopes = { CalendarService.Scope.CalendarReadonly, CalendarService.Scope.CalendarEvents, CalendarService.Scope.CalendarEventsReadonly};
        //static string ApplicationName = "Google Calendar API .NET";
        private CalendarService _service;

        public Form1()
        {
            InitializeComponent();
            _service = InitServ("credentials.json");
            EventsCalendar();
        }


        private void EventsCalendar()
        {
            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets, Scopes, "MarkOfStrike", CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            

            Events events = request.Execute();

            


            eventsCal.Text = "";
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = Convert.ToDateTime(eventItem.Start.Date).ToShortDateString();
                    }

                    eventsCal.Text += $"{eventItem.Summary} {when} \n";

                }
            }
            else
            {
                eventsCal.Text = "Предстоящих событий не найдено...";
                //MessageBox.Show("Предстоящих событий не найдено...");
            }
        }

        private void updEvent_Tick(object sender, EventArgs e)
        {
            EventsCalendar();
        }

        /// <summary>
        /// Инициализация календаря
        /// </summary>
        /// <param name="keyFilePath">Путь к токену</param>
        /// <returns>Методы для работы с каленарем</returns>
        private CalendarService InitServ(string keyFilePath)
        {
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
                        GoogleClientSecrets.Load(stream).Secrets, Scopes, "MarkOfStrike", CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                }

                var _service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "My Calendar v0.1",
                });


                return _service;

            }
            catch (Exception ex)
            {
                throw;
            }

        }


    }
}
