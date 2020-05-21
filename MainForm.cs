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
using Newtonsoft.Json.Linq;

namespace Google_Calendar_Desktop_App
{
    public partial class MainForm : Form
    {
        private List<Calendar_Events> Calendar_s = new List<Calendar_Events>();
        private CalendarWork work;

        static Semaphore sem;

        private bool start = true;

        private int month;
        private int year;

        public MainForm(string user, string pass)
        {
            InitializeComponent();

            this.Icon = Resources.Google_Calendar_icon_icons_com_75710;

            sem = new Semaphore(1, 1);

            work = new CalendarWork("credentials.json", user, pass);

            month = DateTime.Now.Month;
            year = DateTime.Now.Year;

            Filling();
            UpdTable(DateTime.Now.Date);

        }


        #region Дополнительные методы

        /// <summary>
        /// Перезапись списка календарей
        /// </summary>
        private void NewCalendar()
        {
            calendarsItem.Items.Clear();

            foreach (var items in Calendar_s)
            {
                calendarsItem.Items.Add(items.nameCalendar);

                if (items.calendar.Primary != null && items.calendar.Primary == true)
                {
                    calendarsItem.SetItemChecked(calendarsItem.Items.Count - 1, true);
                }

            }
        }

        /// <summary>
        /// Обновление таблицы предстоящих событий
        /// </summary>
        /// <param name="checkedItems">Итемы чекбокса</param>
        /// <param name="date">Выбранный день</param>
        private void UpdTable(DateTime? date = null)
        {
            List<Event> events = new List<Event>();

            int selectRowEvent = upcomingEvents.SelectedRows.Count == 0 ? 0 : upcomingEvents.SelectedRows[0].Index;
            int selectRowDay = eventSelectDay.SelectedRows.Count == 0 ? 0 : eventSelectDay.SelectedRows[0].Index;


            foreach (var item in calendarsItem.CheckedItems)
            {
                events.AddRange(Calendar_s.Find(calendar => calendar.nameCalendar == item.ToString()).events.FindAll(x => date == null ? work.DateEvent(x.Start) > DateTime.Now : work.DateEvent(x.Start).Date == date.Value.Date));
            }

            work.SortEvents(events);

            int count = 0;
            int num = 0;

            if (date == null)
            {
                upcomingEvents.Rows.Clear();
                count = (int)numericUpDown1.Value > events.Count ? events.Count : (int)numericUpDown1.Value;
            }
            else
            {
                eventSelectDay.Rows.Clear();
                count = events.Count;
            }

            for (int i = 0; i < count; i++)
            {
                var item = Calendar_s.Find(x => x.events.Contains(events[i]));


                if (date == null)
                {
                    upcomingEvents.Rows.Add(++num, events[i].Summary, work.DateEvent(events[i].Start).ToShortDateString(), item.calendar.Id, events[i].Id);
                }
                else
                {
                    eventSelectDay.Rows.Add(++num, events[i].Summary, work.DateEvent(events[i].Start).ToShortDateString(), item.calendar.Id, events[i].Id);
                }

            }

            try
            {
                upcomingEvents.Rows[selectRowEvent].Selected = true;
            }
            catch { }

            try
            {
                eventSelectDay.Rows[selectRowDay].Selected = true;
            }
            catch { }


        }

        /// <summary>
        /// Заполнение листа с календарями и событиями
        /// </summary>
        private void Filling()
        {
            List<Calendar_Events> tmpList = new List<Calendar_Events>();

            int num = 0;

            foreach (var calendar in work.GetCalendarsName())
            {
                tmpList.Add(new Calendar_Events { calendar = calendar, idCalendar = calendar.Id, nameCalendar = $"{++num}.{calendar.Summary}", events = work.GetEvents(calendar.Id) });
            }

            if (tmpList.Count > 0)
            {
                if (tmpList.Count > Calendar_s.Count || tmpList.Count < Calendar_s.Count)
                {
                    Calendar_s = tmpList;
                    NewCalendar();
                }
                else
                {
                    Calendar_s = tmpList;
                }
            }

        }

        /// <summary>
        /// Название месяца с годом
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
        private void Drawing_Calendar(DateTime first, int selDay = 0)
        {

            Month(first.Month);

            int selRow = -1;
            int selCol = -1;

            if (calendarPage.SelectedCells.Count > 0)
            {
                selRow = calendarPage.CurrentCell.RowIndex;

                selCol = calendarPage.CurrentCell.ColumnIndex;

                if (selDay == 0)
                {
                    if (calendarPage.CurrentCell.Value != null)
                    {
                        string tmpRes = calendarPage.CurrentCell.Value.ToString().Split('\n')[0];

                        selDay = int.Parse(tmpRes);

                    }

                }

            }

            if (selRow == -1 && selCol == -1)
            {
                selDay = DateTime.Now.Day;
            }


            calendarPage.Rows.Clear();
            calendarPage.Rows.Add();
            calendarPage.Rows.Add();
            calendarPage.Rows.Add();
            calendarPage.Rows.Add();
            calendarPage.Rows.Add();
            calendarPage.Rows.Add();

            if (calendarPage.SelectedCells.Count > 0)
            {
                calendarPage.SelectedCells[0].Selected = false;
            }


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

            for (int i = 0; i < calendarPage.Rows.Count; i++)
            {
                for (int j = 0; j < calendarPage.Rows[i].Cells.Count; j++)
                {
                    if (i == 0 && j < firstDayOfWeek)
                    {
                        while (j < firstDayOfWeek - 1)
                        {
                            j++;
                        }
                    }

                    if (day == selDay)
                    {
                        selRow = i;
                        selCol = j;
                    }

                    if (i == calendarPage.Rows[i].Cells.Count)
                    {
                        while (day < DateTime.DaysInMonth(first.Year, first.Month) + 1)
                        {
                            calendarPage.Rows[i].Cells[j].Value = EventDay(day);
                            day++;
                            j++;
                        }
                    }

                    if (day < DateTime.DaysInMonth(first.Year, first.Month) + 1)
                    {
                        calendarPage.Rows[i].Cells[j].Value = EventDay(day);
                        day++;
                    }

                }

                int cell = 0;
                bool del = true;
                while (cell < calendarPage.Rows[i].Cells.Count)
                {
                    if (calendarPage.Rows[i].Cells[cell].Value != null)
                    {
                        del = false;
                    }

                    cell++;
                }

                if (del)
                {
                    calendarPage.Rows.RemoveAt(i);
                }



            }

            if (selRow != -1 && selCol != -1)
            {
                calendarPage.Rows[selRow].Cells[selCol].Selected = true;

                EventDayTable(calendarPage.CurrentCell.Value.ToString());

            }

        }

        /// <summary>
        /// События выбранного дня в таблице
        /// </summary>
        /// <param name="date"></param>
        private void EventDayTable(string date)
        {
            if (date != null)
            {
                UpdTable(new DateTime(year, month, int.Parse(date.Split('\n')[0])));

                miniCalendar.SelectionStart = new DateTime(year, month, int.Parse(date.Split('\n')[0]));
            }
        }

        /// <summary>
        /// Событие дня в календаре
        /// </summary>
        /// <param name="day">День</param>
        /// <returns>Все события дня для оотображения в календаре</returns>
        private string EventDay(int day)
        {
            string result = "";
            List<string> events = new List<string>();
            DateTime date = new DateTime(year, month, day);


            foreach (var item in calendarsItem.CheckedItems)
            {
                Calendar_Events tmp = null;

                while (tmp == null)
                {
                    tmp = Calendar_s.Find(nc => nc.nameCalendar == item.ToString());
                }

                events.AddRange(tmp.events.FindAll(p => work.DateEvent(p.Start).Date == date.Date).Select(s => s.Summary).ToList());

            }

            for (int i = 0; i < events.Count; i++)
            {
                events[i] = events[i].Length > 15 ? $"{events[i].Substring(0, 15)}..." : events[i];
            }

            if (events.Count > 3)
            {
                result = $"{day} \n\n{events[0]}\n{events[1]}\nИ еще {events.Count - 2}";
            }
            else if (events.Count > 0 && events.Count <= 3)
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
            miniCalendar.SelectionStart = new DateTime(year, month, miniCalendar.SelectionStart.Day);
        }

        /// <summary>
        /// Синхронизация
        /// </summary>
        private void Synchronization()
        {

            if (work.IsConnect())
            {
                connectStatus.Text = "Соединение есть";
                connectStatus.Image = Resources.connect;




                var delEvent = WorkBD.Select_query($"select nc.calendar_name, ec.Id, ec.Calendar_Event from Del_Event de, Name_Calendar nc, Events_calendar ec where nc.Id = (select NameId from Activity where Id = de.Event_Cal_id) and ec.Id = (select EvendId from Activity where Id = de.Event_Cal_id)");


                foreach (DataRow delete in delEvent.Rows)
                {
                    var Cal = JsonConvert.DeserializeObject<CalendarListEntry>(delete.ItemArray[0].ToString());
                    var Eve = JsonConvert.DeserializeObject<Event>(delete.ItemArray[2].ToString());

                    WorkBD.Execution_query($"delete from Events_calendar where Id = {delete.ItemArray[1].ToString()}");

                    work.DeleteEvent(Eve, Cal.Id);

                }
                WorkBD.Execution_query($"delete from Del_Event");



                var insEvent = WorkBD.Select_query($"select nc.Id_Calendar, ie.New_Ev from Ins_Event ie, Name_Calendar nc where nc.Id = ie.Cal_Id");

                foreach (DataRow insert in insEvent.Rows)
                {

                    var Eve = JsonConvert.DeserializeObject<Event>(insert.ItemArray[1].ToString());
                    Eve.Id = null;

                    work.CreateEvent(Eve, insert.ItemArray[0].ToString());
                }
                WorkBD.Execution_query("delete from Ins_Event");

                var modEvent = WorkBD.Select_query($"select nc.Id_Calendar, me.New_Event, ncc.Id_Calendar, case when me.New_Calendar = a.NameId then 1 else 0 end from Mod_Event me left join Activity a on a.Id = me.RecordId, Name_Calendar nc, Name_Calendar ncc where nc.Id = a.NameId and ncc.Id = me.New_Calendar");

                foreach (DataRow mod in modEvent.Rows)
                {
                    var newEvent = JsonConvert.DeserializeObject<Event>(mod.ItemArray[1].ToString());

                    if (mod.ItemArray[3].ToString() == "1")
                    {
                        work.UpdateEvent(newEvent, mod.ItemArray[0].ToString());
                    }
                    else
                    {
                        work.UpdateEvent(newEvent, mod.ItemArray[0].ToString(), mod.ItemArray[2].ToString());
                    }

                }
                WorkBD.Execution_query("delete from Mod_Event");



                ActionRun(() => Filling());

                foreach (var items in Calendar_s.ToArray())
                {

                    WorkBD.Execution_query($"if exists(select Id from Name_Calendar where Id_Calendar = N'{items.calendar.Id}' and Id_User = (select Id from Users where UserName = N'{work.User}')) update Name_Calendar set calendar_name = N'{JsonConvert.SerializeObject(items.calendar)}' where Id_Calendar = N'{items.calendar.Id}' and Id_User = (select Id from Users where UserName = N'{work.User}') else insert into Name_Calendar (calendar_name, Id_Calendar, Id_User) values (N'{JsonConvert.SerializeObject(items.calendar)}', N'{items.calendar.Id}', (select Id from Users where UserName = N'{work.User}'))");

                    foreach (Event events in items.events.ToArray())
                    {
                        var result = WorkBD.Select_query($"if exists(select Id from Activity where NameId = (select Id from Name_Calendar where Id_Calendar = N'{items.calendar.Id}' and Id_User = (select Id from Users where UserName = N'{work.User}')) and EvendId = (select Id from Events_calendar where Id_Event = N'{events.Id}')) select 1 else select 0");


                        if (result.Rows[0].ItemArray[0].ToString() == "1")
                        {
                            WorkBD.Execution_query($"update Events_calendar set Calendar_event = N'{JsonConvert.SerializeObject(events)}' where Id_Event = N'{events.Id}'");
                        }
                        else
                        {
                            WorkBD.Execution_query($"if not exists(select Id from Events_calendar where Id_Event = N'{events.Id}') insert into Events_calendar (Calendar_event, Id_Event) values (N'{JsonConvert.SerializeObject(events)}', N'{events.Id}')");
                            Thread.Sleep(10);
                            WorkBD.Execution_query($"insert into Activity (NameId, EvendId, Date_Event) values ((select Id from Name_Calendar where Id_Calendar = N'{items.calendar.Id}' and Id_User = (select Id from Users where UserName = N'{work.User}')), (select Id from Events_calendar where Id_Event = N'{events.Id}'), CONVERT(DATETIME, N'{work.DateEvent(events.Start).ToString("yyyyMMdd HH:mm:ss")}', 102) )");
                        }


                    }

                }


            }
            else
            {
                connectStatus.Text = "Соединение отсутствует";
                connectStatus.Image = Resources.disconect;

                ActionRun(() => Filling());

            }

            ActionRun(() => UpdTable());
            ActionRun(() => Drawing_Calendar(new DateTime(year, month, 1)));
        }

        /// <summary>
        /// Метод для подробного просмотра события 
        /// </summary>
        /// <param name="selEvent">Выбранное событие</param>
        /// <param name="selCalendar">Выбранный календарь</param>
        private void MoreDetails(Event selEvent, CalendarListEntry selCalendar)
        {
            if (Application.OpenForms.Cast<Form>().Any(f => f.Name == "InfoEvent")) Application.OpenForms["InfoEvent"].Close();

            InfoEvent info = new InfoEvent(work, selEvent, selCalendar);
            info.Show();
        }

        /// <summary>
        /// Выполнение метода в потоке в котором он был создан
        /// </summary>
        /// <param name="action">Делегат</param>
        private void ActionRun(Action action)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(action);
                }
                else
                {
                    action();
                }
            }
            catch { }

        }

        /// <summary>
        /// Запуск метода в асинхронном потоке
        /// </summary>
        /// <param name="action">Делегат метода</param>
        private async void AsyncRunMethod(Action action)
        {
            start = false;

            await Task.Factory.StartNew(action);

            start = true;
        }

        #endregion


        private void updEvent_Tick(object sender, EventArgs e)
        {


            //if (start)
            //{
            AsyncRunMethod(() => Synchronization());
            //}






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
            if (Application.OpenForms.Cast<Form>().Any(f => f.Name == "AddEvent")) Application.OpenForms["AddEvent"].Close();

            AddEvent add = new AddEvent(work);
            add.Show();
        }

        private void upcomingEvents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MoreDetails(Calendar_s.Find(cal => cal.calendar.Id == upcomingEvents.CurrentRow.Cells[3].Value.ToString()).events.Find(ev => ev.Id == upcomingEvents.CurrentRow.Cells[4].Value.ToString()), Calendar_s.Find(selCal => selCal.calendar.Id == upcomingEvents.CurrentRow.Cells[3].Value.ToString()).calendar);
        }

        private void calendarPage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EventDayTable(calendarPage.CurrentCell.Value.ToString());
        }

        private void eventSelectDay_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MoreDetails(Calendar_s.Find(cal => cal.calendar.Id == eventSelectDay.CurrentRow.Cells[3].Value.ToString()).events.Find(ev => ev.Id == eventSelectDay.CurrentRow.Cells[4].Value.ToString()), Calendar_s.Find(selCal => selCal.calendar.Id == eventSelectDay.CurrentRow.Cells[3].Value.ToString()).calendar);
        }

        private void miniCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            year = miniCalendar.SelectionRange.Start.Year;
            month = miniCalendar.SelectionRange.Start.Month;

            Drawing_Calendar(new DateTime(year, month, 1), miniCalendar.SelectionStart.Day);
        }

        private void switchingConBtn_Click(object sender, EventArgs e)
        {
            if (work.IsConnect())
            {
                work.StopTimer();
            }
            else
            {
                work.StartTimer();
            }
        }

        private void printCal_Click(object sender, EventArgs e)
        {
            List<Event> eventsPrint = new List<Event>();

            foreach (var item in calendarsItem.CheckedItems)
            {
                eventsPrint.AddRange(Calendar_s.Find(calendar => calendar.nameCalendar == item.ToString()).events);
            }

            if (Application.OpenForms.Cast<Form>().Any(f => f.Name == "PrintCalendarForm")) Application.OpenForms["PrintCalendarForm"].Close();

            PrintCalendarForm printCalendar = new PrintCalendarForm(eventsPrint);
            printCalendar.Show();
        }
    }
}
