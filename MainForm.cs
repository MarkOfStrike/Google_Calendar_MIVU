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

        private int month;
        private int year;



        private bool connect;

        public MainForm()
        {
            InitializeComponent();

            month = DateTime.Now.Month;
            year = DateTime.Now.Year;

            Month(month);
            Drawing_Calendar(new DateTime(year, month, 1));

            work = new CalendarWork("credentials.json", "MarkOfStrike");
            work.Connect = Check_Connect();


            foreach (var item in work.GetCalendarsName().Items)
            {
                Calendar_s.Add(new Calendar_Events { idCalendar = item.Id, nameCalendar = item.Summary, calendar = item, events = work.GetEvents(item.Id).Items.ToList()});
            }

            //dataGridView2.Columns[2].Visible = false;

            DateTime date = DateTime.Now;

            //Drawing_Calendar(DateTime.Now);
            
            //MessageBox.Show(date.DayOfWeek.ToString());

            int count = 0;
            string ids = "";

            foreach (var items in Calendar_s)
            {
                checkedListBox1.Items.Add(items.nameCalendar);
                foreach (var events in items.events)
                {
                    dataGridView2.Rows.Add(++count,events.Summary, Convert.ToDateTime(events.Start.Date).ToShortDateString(), items.calendar.Id, events.Id /*Convert.ToDateTime(events.Start.Date).ToShortDateString()*/);
                }

                ids += $"{items.idCalendar} \n";
            }

            var itemName = Calendar_s.Find(x => x.calendar.Summary == checkedListBox1.Items[1].ToString()).events.Find(p => p.Summary == "");
            //var ev = itemName.events.Find(p => p.Summary == dataGridView2.Rows[2].Cells[1].Value.ToString());

            //var sd = Calendar_s

            //MessageBox.Show(ids);

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

            //DateTime first = new DateTime(date.Year, date.Month, 1);

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
                            //var sq = $"{day} \n{day} \n{day} \n{day} \n{day} \n{day} \n{day} \n{day} \n";

                            //for (int f = 0; f < 3; f++)
                            //{
                            //    dataGridView3.Rows[i].Cells[j].Value += $"{day} \n";
                            //}

                            dataGridView3.Rows[i].Cells[j].Value = $"{day} \n\n";
                            day++;
                            j++;
                        }
                    }

                    if (day < DateTime.DaysInMonth(first.Year, first.Month) + 1)
                    {
                        //var sd = $"{day} \n{day} \n{day} \n{day} \n{day} \n{day} \n{day} \n{day} \n";

                        //for (int f = 0; f < 3; f++)
                        //{
                        //    dataGridView3.Rows[i].Cells[j].Value += $"{day} \n";
                        //}

                        dataGridView3.Rows[i].Cells[j].Value = $"{day} \n\n";
                        day++;
                    }

                }


            }

            //for (int a = 0; a < dataGridView3.Rows.Count; a++)
            //{
            //    for (int s = 0; s < dataGridView3.Rows[a].Cells.Count; s++)
            //    {
            //        dataGridView3.Rows[a].Cells[s].Style.
            //    }
            //}


            //dataGridView3.Rows[2].Cells[4].Value += $"\n\nЛя ля ля";


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


        private void Synchronization()
        {
            if (work.Connect)
            {

            }
            else
            {

            }
        }


        private void updEvent_Tick(object sender, EventArgs e)
        {
            work.Connect = Check_Connect();

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
