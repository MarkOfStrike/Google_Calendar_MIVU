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

namespace Google_Calendar_Desktop_App
{
    public partial class MainForm : Form
    {
        //static string[] Scopes = { CalendarService.Scope.CalendarReadonly, CalendarService.Scope.CalendarEvents, CalendarService.Scope.CalendarEventsReadonly};
        //static string ApplicationName = "Google Calendar API .NET";
        private CalendarService _service;

        private List<Calendar_Events> Calendar_s = new List<Calendar_Events>();
        private CalendarWork work;
        private List<Color> colors = new List<Color>();

        private WorkBD wbd = new WorkBD();

        private int month;
        private int year;


        public MainForm()
        {
            InitializeComponent();

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

            work.Identity = mas.ToArray();

            month = DateTime.Now.Month;
            year = DateTime.Now.Year;

            Month(month);
            Drawing_Calendar(new DateTime(year, month, 1));

            work = new CalendarWork("credentials.json", "MarkOfStrike");
            work.Connect = Check_Connect();


            Filling();


            //foreach (var item in work.GetCalendarsName().Items)
            //{
            //    Calendar_s.Add(new Calendar_Events { idCalendar = item.Id, nameCalendar = item.Summary, calendar = item, events = work.GetEvents(item.Id).Items.ToList()});
            //}

            //dataGridView2.Columns[2].Visible = false;

            DateTime date = DateTime.Now;

            //Drawing_Calendar(DateTime.Now);
            
            //MessageBox.Show(date.DayOfWeek.ToString());

            int count = 0;
            string ids = "";

            foreach (var items in Calendar_s)
            {
                checkedListBox1.Items.Add(items.nameCalendar);

                if ((bool)items.calendar.Primary)
                {
                    checkedListBox1.SetItemChecked(checkedListBox1.Items.Count - 1, true);
                }

                foreach (var events in items.events)
                {
                    dataGridView2.Rows.Add(++count,events.Summary, Convert.ToDateTime(events.Start.Date).ToShortDateString(), items.calendar.Id, events.Id /*Convert.ToDateTime(events.Start.Date).ToShortDateString()*/);
                }

                ids += $"{items.idCalendar} \n";
            }

            var itemName = Calendar_s.Find(x => x.calendar.Summary == checkedListBox1.Items[1].ToString()).events.Find(p => p.Summary == "");

            //List<Calendar_Events> sddsa = Calendar_s.Where(x => x.calendar.Summary == checkedListBox1.Items.ToString());

            

            //var ev = itemName.events.Find(p => p.Summary == dataGridView2.Rows[2].Cells[1].Value.ToString());

            //var sd = Calendar_s

            //MessageBox.Show(ids);

        }

        /// <summary>
        /// Заполнение листа с календарями и событиями
        /// </summary>
        private void Filling()
        {
            Calendar_s.Clear();

            foreach (var calendar in work.GetCalendarsName().Items)
            {
                Calendar_s.Add(new Calendar_Events { calendar = calendar, idCalendar = calendar.Id, nameCalendar = calendar.Summary, events = work.GetEvents(calendar.Id).Items.ToList() });
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
                    label8.Text = $"Январь {year}";
                    break;
                case 2:
                    label8.Text = $"Февраль {year}";
                    break;
                case 3:
                    label8.Text = $"Март {year}";
                    break;
                case 4:
                    label8.Text = $"Апрель {year}";
                    break;
                case 5:
                    label8.Text = $"Май {year}";
                    break;
                case 6:
                    label8.Text = $"Июнь {year}";
                    break;
                case 7:
                    label8.Text = $"Июль {year}";
                    break;
                case 8:
                    label8.Text = $"Август {year}";
                    break;
                case 9:
                    label8.Text = $"Сентябрь {year}";
                    break;
                case 10:
                    label8.Text = $"Октябрь {year}";
                    break;
                case 11:
                    label8.Text = $"Ноябрь {year}";
                    break;
                case 12:
                    label8.Text = $"Декабрь {year}";
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
                            dataGridView3.Rows[i].Cells[j].Value = $"{day} \n\n";

                            //Здесь написать алгоритм отрисовки элементов чтобы все ячейки были одного размера

                            day++;
                            j++;
                        }
                    }

                    if (day < DateTime.DaysInMonth(first.Year, first.Month) + 1)
                    {
                        dataGridView3.Rows[i].Cells[j].Value = $"{day} \n\n";
                        day++;
                    }

                }


            }


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
            work.Connect = Check_Connect();

            
            if (work.Connect)
            {
                var delEvent = wbd.Select_query($"select nc.calendar_name, ec.Id, ec.Calendar_Event from Del_Event de, Name_Calendar nc, Events_calendar ec where nc.Id = (select NameId from Activity where Id = de.Event_Cal_id) and ec.Id = (select EvendId from Activity where Id = de.Event_Cal_id)");
                while (delEvent.Read())
                {
                    var Cal = JsonConvert.DeserializeObject<CalendarListEntry>(delEvent.GetString(0));
                    var Eve = JsonConvert.DeserializeObject<Event>(delEvent.GetString(2));

                    wbd.Execution_query($"delete from Events_calendar where Id = {delEvent.GetInt32(1)}");

                    work.DeleteEvent(Eve, Cal.Id);
                    
                }


                var insEvent = wbd.Select_query($"select nc.calendar_name, ie.New_Ev from Ins_Event ie, Name_Calendar nc where nc.Id = ie.Cal_Id");
                while (insEvent.Read())
                {
                    var Cal = JsonConvert.DeserializeObject<CalendarListEntry>(insEvent.GetString(0));
                    var Eve = JsonConvert.DeserializeObject<Event>(insEvent.GetString(1));

                    work.CreateEvent(Eve, Cal.Id);

                }
                wbd.Execution_query("delete from Ins_Event");


                var modEvent = wbd.Select_query($"select ec.Id  me.New_Event, nc.calendar_name from Mod_Event me, Events_calendar ec, Name_Calendar nc where ec.Id = (select EvendId from Activity where Id = me.RecordId) and nc.id = me.New_Calendar");
                while (modEvent.Read())
                {
                    var newEvent = JsonConvert.DeserializeObject<Event>(modEvent.GetString(1));
                    var Cal = JsonConvert.DeserializeObject<CalendarListEntry>(modEvent.GetString(2));

                    wbd.Execution_query($"delete from Events_calendar where id = {modEvent.GetInt32(0)}");

                    work.UpdateEvent(newEvent, Cal);

                }




                Filling();

                foreach (var items in Calendar_s)
                {

                    foreach (var events in items.events)
                    {
                        var result = wbd.Select_query($"select Id from Activity where NameId = (select Id from Name_Calendar where calendar_name = '{JsonConvert.SerializeObject(items.calendar)}') and EvendId = (select Id from Events_calendar where calendar_event = '{JsonConvert.SerializeObject(events)}')");

                        if (!result.HasRows)
                        {
                            var check_ev = wbd.Select_query($"select Id from Events_calendar where Calendar_events = '{JsonConvert.SerializeObject(events)}'");
                            if (check_ev.HasRows)
                            {
                                while (check_ev.Read())
                                {
                                    wbd.Execution_query($"delete from Events_calendar where Id = {check_ev.GetInt32(0)}");
                                }

                                wbd.Execution_query($"insert into Events_calendar (Calendar_event) values ('{JsonConvert.SerializeObject(events)}')");
                                wbd.Execution_query($"insert into Activity (NameId, EvendId, Date_Event) values ((select Id from Name_Calendar where calendar_name = '{JsonConvert.SerializeObject(items.calendar)}'), (select Id from Events_calendar where Calendar_event = '{JsonConvert.SerializeObject(events)}'), '{Convert.ToDateTime(events.Start.DateTime)}')");
                            }
                        }

                    }

                }


            }
            else
            {
                Filling();
            }

            Drawing_Calendar(new DateTime(year, month, 1));

        }



        private void updEvent_Tick(object sender, EventArgs e)
        {
            Synchronization();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            month--;
            Click_btn();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            month++;
            Click_btn();
        }
    }
}
