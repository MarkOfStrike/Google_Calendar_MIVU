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
using System.Net;
using Newtonsoft.Json;
using Google_Calendar_Desktop_App.Properties;

namespace Google_Calendar_Desktop_App
{
    public partial class MainForm : Form
    {
        //static string[] Scopes = { CalendarService.Scope.CalendarReadonly, CalendarService.Scope.CalendarEvents, CalendarService.Scope.CalendarEventsReadonly};
        //static string ApplicationName = "Google Calendar API .NET";
        private CalendarService _service;

        private List<Calendar_Events> Calendar_s = new List<Calendar_Events>();
        private CalendarWork work;
        //private List<Color> colors = new List<Color>();

        //private WorkBD wbd = new WorkBD();

        private int month;
        private int year;


        public MainForm()
        {
            InitializeComponent();

            this.Icon = Resources.Google_Calendar_icon_icons_com_75710;

            work = new CalendarWork("credentials.json", "MarkOfStrike");
            //work.Connect = Check_Connect();

            List<char> mas = new List<char>();

            for (int i = 0; i < 26; i++)
            {
                mas.Add((char)(65+i));
                mas.Add((char)(97+i));
            }

            for (int q = 0; q < 10; q++)
            {
                mas.Add((char)(48 + q));
            }

            //mas.CopyTo(work.Identity);

            work.Identity = mas.ToArray();

            month = DateTime.Now.Month;
            year = DateTime.Now.Year;

            

            


            Filling();


            int count = 0;

            foreach (var items in Calendar_s)
            {
                calendarsItem.Items.Add(items.nameCalendar);
                CalendarsForEvents.Items.Add(items.nameCalendar);

                if (items.calendar.Primary != null && items.calendar.Primary == true)
                {
                    calendarsItem.SetItemChecked(calendarsItem.Items.Count - 1, true);
                }

                foreach (var events in items.events)
                {
                    upcomingEvents.Rows.Add(++count, events.Summary, Convert.ToDateTime(events.Start.Date).ToShortDateString(), items.calendar.Id, events.Id /*Convert.ToDateTime(events.Start.Date).ToShortDateString()*/);
                }

            }

            CalendarsForEvents.Text = CalendarsForEvents.Items[0].ToString();

            Month(month);
            Drawing_Calendar(new DateTime(year, month, 1));

            Synchronization();

        }

        /// <summary>
        /// Заполнение листа с календарями и событиями
        /// </summary>
        private void Filling()
        {
            Calendar_s.Clear();

            foreach (var calendar in work.GetCalendarsName().Items)
            {
                Calendar_s.Add(new Calendar_Events { calendar = calendar, idCalendar = calendar.Id, nameCalendar = calendar.Summary, events = work.GetAllEvents(calendar.Id).Items.ToList() });
            }

        }

        /// <summary>
        /// Проверка интернет соединения
        /// </summary>
        /// <returns></returns>
        private bool Check_Connect()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// название месяца с годом
        /// </summary>
        /// <param name="month">Месяц</param>
        private void Month(int month)
        {
            switch (month)
            {
                case 1:
                    monthOfYear.Text = $"Январь {year}";
                    break;
                case 2:
                    monthOfYear.Text = $"Февраль {year}";
                    break;
                case 3:
                    monthOfYear.Text = $"Март {year}";
                    break;
                case 4:
                    monthOfYear.Text = $"Апрель {year}";
                    break;
                case 5:
                    monthOfYear.Text = $"Май {year}";
                    break;
                case 6:
                    monthOfYear.Text = $"Июнь {year}";
                    break;
                case 7:
                    monthOfYear.Text = $"Июль {year}";
                    break;
                case 8:
                    monthOfYear.Text = $"Август {year}";
                    break;
                case 9:
                    monthOfYear.Text = $"Сентябрь {year}";
                    break;
                case 10:
                    monthOfYear.Text = $"Октябрь {year}";
                    break;
                case 11:
                    monthOfYear.Text = $"Ноябрь {year}";
                    break;
                case 12:
                    monthOfYear.Text = $"Декабрь {year}";
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Отрисовка календаря в DataGridView
        /// </summary>
        /// <param name="first">Первый день месяца</param>
        private void Drawing_Calendar(DateTime first)
        {
            dataGridView3.Rows.Clear();
            dataGridView3.Rows.Add();
            dataGridView3.Rows.Add();
            dataGridView3.Rows.Add();
            dataGridView3.Rows.Add();
            dataGridView3.Rows.Add();
            dataGridView3.Rows.Add();

            int firstDayOfWeek = 0;

            
            switch (first.DayOfWeek.ToString())
            {
                case "Monday":
                    firstDayOfWeek = 1;
                    break;
                case "Tuesday":
                    firstDayOfWeek = 2;
                    break;
                case "Wednesday":
                    firstDayOfWeek = 3;
                    break;
                case "Thursday":
                    firstDayOfWeek = 4;
                    break;
                case "Friday":
                    firstDayOfWeek = 5;
                    break;
                case "Saturday":
                    firstDayOfWeek = 6;
                    break;
                case "Sunday":
                    firstDayOfWeek = 7;
                    break;

                default:
                    break;
            }


            int day = 1;

            

            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView3.Rows[i].Cells.Count; j++)
                {
                    if (i == 0 && j < firstDayOfWeek)
                    {
                        while (j < firstDayOfWeek-1)
                        {
                            j++;
                        }
                    }
                    else if (i == dataGridView3.Rows[i].Cells.Count)
                    {
                        while (day < DateTime.DaysInMonth(first.Year, first.Month) + 1)
                        {
                            dataGridView3.Rows[i].Cells[j].Value = EventDay(day);
                            day++;
                            j++;
                        }
                    }

                    if (day < DateTime.DaysInMonth(first.Year, first.Month) + 1)
                    {
                        dataGridView3.Rows[i].Cells[j].Value = EventDay(day);
                        day++;
                    }

                }

                int cell = 0;
                bool del = true;
                while (cell < dataGridView3.Rows[i].Cells.Count)
                {
                    if (dataGridView3.Rows[i].Cells[cell].Value != null)
                    {
                        del = false;
                    }

                    cell++;
                }

                if (del)
                {
                    dataGridView3.Rows.RemoveAt(i);
                }

            }


        }

        private string EventDay(int day)
        {
            string result = "";
            List<string> events = new List<string>();

            foreach (var item in calendarsItem.CheckedItems)
            {
                var tmp = Calendar_s.Find(x => x.calendar.Summary == item.ToString());

                DateTime date = new DateTime(year, month, day);

                string asd = date.Date.ToString("u");
                asd = asd.Substring(0, asd.IndexOf(' '));

                var ev = tmp.events.FindAll(p => p.Start.Date == asd);

                events.AddRange(ev.Select(w => w.Summary));

            }

            if (events.Count > 3)
            {
                result = $"{day} \n\n{events[0]}\n{events[1]}\nИ еще {events.Count - 2}";
            }
            else if(events.Count > 0 && events.Count <=3)
            {
                result = $"{day} \n";
                foreach (var eve in events)
                {
                    result += $"\n{eve}";
                }

                int tmp = 2;

                while (tmp - events.Count >= 0)
                {
                    result += "\n";
                    tmp--;
                }

            }
            else
            {
                result = $"{day} \n\n\n\n";
            }


            return result;
        }


        /// <summary>
        /// Нажатие на навигационную кнопку
        /// </summary>
        private void Click_btn()
        {
            if (month < 1)
            {
                month = 12;
                year--;
            }
            else if (month > 12)
            {
                month = 1;
                year++;
            }

            Month(month);
            Drawing_Calendar(new DateTime(year, month, 1));
        }

        /// <summary>
        /// Метод синхронизации данных с сервером
        /// </summary>
        private void Synchronization()
        {
            //work.Connect = Check_Connect();

            
            if (CalendarWork.Connect)
            {
                connectStatus.Text = "Соединение есть";
                connectStatus.Image = Resources.connect;


                //wbd.Open_con();
                WorkBD.Open_con();

                var delEvent = WorkBD.Select_query($"select nc.calendar_name, ec.Id, ec.Calendar_Event from Del_Event de, Name_Calendar nc, Events_calendar ec where nc.Id = (select NameId from Activity where Id = de.Event_Cal_id) and ec.Id = (select EvendId from Activity where Id = de.Event_Cal_id)");
                while (delEvent.Read())
                {
                    var Cal = JsonConvert.DeserializeObject<CalendarListEntry>(delEvent.GetString(0));
                    var Eve = JsonConvert.DeserializeObject<Event>(delEvent.GetString(2));

                    WorkBD.Execution_query($"delete from Events_calendar where Id = {delEvent.GetInt32(1)}");

                    work.DeleteEvent(Eve, Cal.Id);
                    
                }
                delEvent.Close();

                var insEvent = WorkBD.Select_query($"select nc.calendar_name, ie.New_Ev from Ins_Event ie, Name_Calendar nc where nc.Id = ie.Cal_Id");
                while (insEvent.Read())
                {
                    var Cal = JsonConvert.DeserializeObject<CalendarListEntry>(insEvent.GetString(0));
                    var Eve = JsonConvert.DeserializeObject<Event>(insEvent.GetString(1));

                    work.CreateEvent(Eve, Cal.Id);

                }
                WorkBD.Execution_query("delete from Ins_Event");
                insEvent.Close();

                var modEvent = WorkBD.Select_query($"select ec.Id, me.New_Event, nc.calendar_name from Mod_Event me, Events_calendar ec, Name_Calendar nc where ec.Id = (select EvendId from Activity where Id = me.RecordId) and nc.id = me.New_Calendar");
                while (modEvent.Read())
                {
                    var newEvent = JsonConvert.DeserializeObject<Event>(modEvent.GetString(1));
                    var Cal = JsonConvert.DeserializeObject<CalendarListEntry>(modEvent.GetString(2));

                    WorkBD.Execution_query($"delete from Events_calendar where id = {modEvent.GetInt32(0)}");

                    work.UpdateEvent(newEvent, Cal);

                }
                modEvent.Close();



                Filling();

                foreach (var items in Calendar_s)
                {
                    var res_Cal = WorkBD.Select_query($"select Id from Name_Calendar where calendar_name = '{JsonConvert.SerializeObject(items.calendar)}'");
                    if (!res_Cal.HasRows)
                    {
                        WorkBD.Execution_query($"insert into Name_Calendar (calendar_name) values ('{JsonConvert.SerializeObject(items.calendar)}')");
                    }
                    res_Cal.Close();

                    foreach (var events in items.events)
                    {
                        var result = WorkBD.Select_query($"select Id from Activity where NameId = (select Id from Name_Calendar where calendar_name = '{JsonConvert.SerializeObject(items.calendar)}') and EvendId = (select Id from Events_calendar where calendar_event = '{JsonConvert.SerializeObject(events)}')");

                        
                        if (!result.HasRows)
                        {
                            result.Close();
                            var check_ev = WorkBD.Select_query($"select Id from Events_calendar where Calendar_event = '{JsonConvert.SerializeObject(events)}'");
                            if (check_ev.HasRows)
                            {
                                while (check_ev.Read())
                                {
                                    WorkBD.Execution_query($"delete from Events_calendar where Id = {check_ev.GetInt32(0)}");
                                }

                                
                            }
                            check_ev.Close();

                            WorkBD.Execution_query($"insert into Events_calendar (Calendar_event) values ('{JsonConvert.SerializeObject(events)}')");
                            WorkBD.Execution_query($"insert into Activity (NameId, EvendId, Date_Event) values ((select Id from Name_Calendar where calendar_name = '{JsonConvert.SerializeObject(items.calendar)}'), (select Id from Events_calendar where Calendar_event = '{JsonConvert.SerializeObject(events)}'), convert(datetime, '{events.Start.Date}', 102))");
                        }

                        try
                        {
                            result.Close();
                        }
                        catch { }
                    }

                }

                //wbd.Close_con();
                WorkBD.Close_con();

            }
            else
            {
                connectStatus.Text = "Соединение отсутствует";
                connectStatus.Image = Resources.disconect;

                Filling();
            }

            Drawing_Calendar(new DateTime(year, month, 1));

        }



        private void updEvent_Tick(object sender, EventArgs e)
        {
            //Synchronization();
        }


        

        private void calendarsItem_SelectedValueChanged(object sender, EventArgs e)
        {
            Drawing_Calendar(new DateTime(year, month, 1));
        }

        private void nextMonth_Click(object sender, EventArgs e)
        {
            month++;
            Click_btn();
        }

        private void prevMonth_Click(object sender, EventArgs e)
        {
            month--;
            Click_btn();
        }

        private void createEvent_Click(object sender, EventArgs e)
        {
            AddEvent add = new AddEvent(work);
            add.ShowDialog();
            Synchronization();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            work.CreateEvent(eventSummary.Text, eventStart.Value, eventEnd.Value, Calendar_s.Find(x => x.nameCalendar == CalendarsForEvents.Text).calendar.Id, string.IsNullOrEmpty(eventDescription.Text) ? null : eventDescription.Text, string.IsNullOrEmpty(eventAttendees.Text) ? null : eventAttendees.Text.Split('\n').ToList(), string.IsNullOrEmpty(eventLocation.Text) ? null : eventLocation.Text);

            MessageBox.Show("Запись создана!");
        }
    }
}
