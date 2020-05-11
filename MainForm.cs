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

        private int month;
        private int year;



        public MainForm()
        {
            InitializeComponent();

            this.Icon = Resources.Google_Calendar_icon_icons_com_75710;

            work = new CalendarWork("credentials.json", "MarkOfStrike");
           
            month = DateTime.Now.Month;
            year = DateTime.Now.Year;


            Filling();


        }


        #region Дополнительные методы


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

            upcomingEvents.Rows.Clear();
            eventSelectDay.Rows.Clear();

            foreach (var item in calendarsItem.CheckedItems)
            {
                events.AddRange(Calendar_s.Find(calendar => calendar.nameCalendar == item.ToString()).events.FindAll(x => date == null ? work.DateEvent(x.Start) > DateTime.Now : work.DateEvent(x.Start).Date == date.Value.Date));
            }

            events.OrderBy(x => x.Start);

            int count = 0;
            int num = 0;

            if (date == null)
            {
                count = (int)numericUpDown1.Value > events.Count ? events.Count : (int)numericUpDown1.Value;
            }
            else
            {
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
            else
            {
                updEvent.Stop();
                MessageBox.Show("Отсутствует список календарей в локальной базе данных! Пожалуйста подключитесь к интернету и перезапустите программу для синхронизации ланных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void Drawing_Calendar(DateTime first)
        {

            Month(first.Month);

            int selRow = -1;
            int selCol = -1;

            int selDay = 0;



            if (dataGridView3.SelectedCells.Count > 0)
            {
                selRow = dataGridView3.CurrentCell.RowIndex;

                selCol = dataGridView3.CurrentCell.ColumnIndex;

                if (dataGridView3.CurrentCell.Value != null)
                {
                    string tmpRes = dataGridView3.CurrentCell.Value.ToString().Split('\n')[0];


                    selDay = int.Parse(tmpRes);

                }

            }


            dataGridView3.Rows.Clear();
            dataGridView3.Rows.Add();
            dataGridView3.Rows.Add();
            dataGridView3.Rows.Add();
            dataGridView3.Rows.Add();
            dataGridView3.Rows.Add();
            dataGridView3.Rows.Add();

            if (dataGridView3.SelectedCells.Count > 0)
            {
                dataGridView3.SelectedCells[0].Selected = false;
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

            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView3.Rows[i].Cells.Count; j++)
                {
                    if (day == selDay)
                    {
                        selRow = i;
                        selCol = j;
                    }

                    if (i == 0 && j < firstDayOfWeek)
                    {
                        while (j < firstDayOfWeek - 1)
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

            if (selRow != -1 && selCol != -1)
            {
                dataGridView3.Rows[selRow].Cells[selCol].Selected = true;
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

            foreach (var item in calendarsItem.CheckedItems)
            {
                Calendar_Events tmp = null;

                while (tmp == null)
                {
                    tmp = Calendar_s.Find(nc => nc.nameCalendar == item.ToString());
                }

                DateTime date = new DateTime(year, month, day);

                events.AddRange(tmp.events.FindAll(p => work.DateEvent(p.Start).Date == date.Date).Select(s => s.Summary).ToList());

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
        }

        #region OldSync

        ///// <summary>
        ///// Метод синхронизации данных с сервером
        ///// </summary>
        //private void Synchronization()
        //{
        //    if (CalendarWork.isConnect)
        //    {
        //        connectStatus.Text = "Соединение есть";
        //        connectStatus.Image = Resources.connect;


        //        WorkBD.Open_con();

        //        var delEvent = WorkBD.Select_query($"select nc.calendar_name, ec.Id, ec.Calendar_Event from Del_Event de, Name_Calendar nc, Events_calendar ec where nc.Id = (select NameId from Activity where Id = de.Event_Cal_id) and ec.Id = (select EvendId from Activity where Id = de.Event_Cal_id)");
        //        while (delEvent.Read())
        //        {
        //            var Cal = JsonConvert.DeserializeObject<CalendarListEntry>(delEvent.GetString(0));
        //            var Eve = JsonConvert.DeserializeObject<Event>(delEvent.GetString(2));

        //            //WorkBD.Execution_query($"delete from Events_calendar where Id = {delEvent.GetInt32(1)}");

        //            work.DeleteEvent(Eve, Cal.Id);

        //        }
        //        WorkBD.Execution_query($"delete from Events_calendar");

        //        var insEvent = WorkBD.Select_query($"select nc.calendar_name, ie.New_Ev from Ins_Event ie, Name_Calendar nc where nc.Id = ie.Cal_Id");
        //        while (insEvent.Read())
        //        {
        //            var Cal = JsonConvert.DeserializeObject<CalendarListEntry>(insEvent.GetString(0));
        //            var Eve = JsonConvert.DeserializeObject<Event>(insEvent.GetString(1));

        //            work.CreateEvent(Eve, Cal.Id);

        //        }
        //        WorkBD.Execution_query("delete from Ins_Event");

        //        var modEvent = WorkBD.Select_query($"select ec.Id, me.New_Event, nc.calendar_name from Mod_Event me, Events_calendar ec, Name_Calendar nc where ec.Id = (select EvendId from Activity where Id = me.RecordId) and nc.id = me.New_Calendar");
        //        while (modEvent.Read())
        //        {
        //            var newEvent = JsonConvert.DeserializeObject<Event>(modEvent.GetString(1));
        //            var Cal = JsonConvert.DeserializeObject<CalendarListEntry>(modEvent.GetString(2));

        //            WorkBD.Execution_query($"delete from Events_calendar where id = {modEvent.GetInt32(0)}");

        //            work.UpdateEvent(newEvent, Cal);

        //        }
        //        WorkBD.Execution_query("delete from Mod_Event");



        //        Filling();

        //        foreach (var items in Calendar_s)
        //        {
        //            var res_Cal = WorkBD.Select_query($"select Id from Name_Calendar where calendar_name = N'{JsonConvert.SerializeObject(items.calendar)}'");
        //            if (!res_Cal.HasRows)
        //            {
        //                WorkBD.Execution_query($"insert into Name_Calendar (calendar_name) values (N'{JsonConvert.SerializeObject(items.calendar)}')");
        //            }

        //            foreach (var events in items.events)
        //            {
        //                var result = WorkBD.Select_query($"select Id from Activity where NameId = (select Id from Name_Calendar where calendar_name = N'{JsonConvert.SerializeObject(items.calendar)}') and EvendId = (select Id from Events_calendar where calendar_event = N'{JsonConvert.SerializeObject(events)}')");


        //                if (!result.HasRows)
        //                {
        //                    var check_ev = WorkBD.Select_query($"select Id from Events_calendar where Calendar_event = N'{JsonConvert.SerializeObject(events)}'");
        //                    if (check_ev.HasRows)
        //                    {
        //                        while (check_ev.Read())
        //                        {
        //                            WorkBD.Execution_query($"delete from Events_calendar where Id = {check_ev.GetInt32(0)}");
        //                        }


        //                    }



        //                    WorkBD.Execution_query($"insert into Events_calendar (Calendar_event) values (N'{JsonConvert.SerializeObject(events)}')");
        //                    WorkBD.Execution_query($"insert into Activity (NameId, EvendId, Date_Event) values ((select Id from Name_Calendar where calendar_name = N'{JsonConvert.SerializeObject(items.calendar)}'), (select Id from Events_calendar where Calendar_event = N'{JsonConvert.SerializeObject(events)}'), CONVERT(DATETIME, N'{Convert.ToDateTime(events.Start.Date ?? events.Start.DateTime.ToString() ?? events.Start.DateTimeRaw).ToString("yyyyMMdd HH:mm:ss")}', 102) )");
        //                }

        //            }

        //        }

        //        WorkBD.Close_con();

        //    }
        //    else
        //    {
        //        connectStatus.Text = "Соединение отсутствует";
        //        connectStatus.Image = Resources.disconect;

        //        Filling();
        //    }

        //    UpdTable();
        //    Drawing_Calendar(new DateTime(year, month, 1));

        //}

        #endregion



        /// <summary>
        /// Метод синхронизации данных с сервером
        /// </summary>
        private async Task SynchronizationAsync()
        {
            //WorkBD.Open_con();
            await Task.Run(() =>
            {

                if (CalendarWork.isConnect)
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



                    var insEvent = WorkBD.Select_query($"select nc.calendar_name, ie.New_Ev from Ins_Event ie, Name_Calendar nc where nc.Id = ie.Cal_Id");

                    foreach (DataRow insert in insEvent.Rows)
                    {
                        var Cal = JsonConvert.DeserializeObject<CalendarListEntry>(insert.ItemArray[0].ToString());
                        var Eve = JsonConvert.DeserializeObject<Event>(insert.ItemArray[1].ToString());

                        work.CreateEvent(Eve, Cal.Id);
                    }
                    WorkBD.Execution_query("delete from Ins_Event");

                    var modEvent = WorkBD.Select_query($"select ec.Id, me.New_Event, nc.calendar_name from Mod_Event me, Events_calendar ec, Name_Calendar nc where ec.Id = (select EvendId from Activity where Id = me.RecordId) and nc.id = me.New_Calendar");

                    foreach (DataRow mod in modEvent.Rows)
                    {
                        var newEvent = JsonConvert.DeserializeObject<Event>(mod.ItemArray[1].ToString());
                        var Cal = JsonConvert.DeserializeObject<CalendarListEntry>(mod.ItemArray[2].ToString());

                        WorkBD.Execution_query($"delete from Events_calendar where id = {mod.ItemArray[0].ToString()}");

                        work.UpdateEvent(newEvent, Cal);
                    }


                    WorkBD.Execution_query("delete from Mod_Event");



                    ActionRun(() => Filling());

                    foreach (var items in Calendar_s.ToArray())
                    {

                        WorkBD.Execution_query($"if exists(select Id from Name_Calendar where json_value(calendar_name, '$.id') = N'{items.calendar.Id}') update Name_Calendar set calendar_name = N'{JsonConvert.SerializeObject(items.calendar)}' where Id_Calendar = N'{items.calendar.Id}' else insert into Name_Calendar (calendar_name, Id_Calendar) values (N'{JsonConvert.SerializeObject(items.calendar)}', N'{items.calendar.Id}')");

                        foreach (var events in items.events.ToArray())
                        {
                            var result = WorkBD.Select_query($"if exists(select Id from Activity where NameId = (select Id from Name_Calendar where Id_Calendar = N'{items.calendar.Id}') and EvendId = (select Id from Events_calendar where Id_Event = N'{events.Id}')) select 1 else select 0");


                            if (result.Rows[0].ItemArray[0].ToString() == "1")
                            {
                                WorkBD.Execution_query($"update Events_calendar set Calendar_event = N'{JsonConvert.SerializeObject(events)}' where Id_Event = N'{events.Id}'");
                            }
                            else
                            {
                                WorkBD.Execution_query($"if exists(select Id from Events_calendar where Id_Event = N'{events.Id}') update Events_calendar set Calendar_event = N'{JsonConvert.SerializeObject(events)}' where Id_Event = N'{events.Id}' else insert into Events_calendar (Calendar_event, Id_Event) values (N'{JsonConvert.SerializeObject(events)}', N'{events.Id}')");
                            }



                            WorkBD.Execution_query($"insert into Activity (NameId, EvendId, Date_Event) values ((select Id from Name_Calendar where Id_Calendar = N'{items.calendar.Id}'), (select Id from Events_calendar where Id_Event = N'{events.Id}'), CONVERT(DATETIME, N'{work.DateEvent(events.Start).ToString("yyyyMMdd HH:mm:ss")}', 102) )");

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

            });



            //_waitHandle.WaitOne();

            //WorkBD.Close_con();

        }





        private void SyncTest()
        {

            if (CalendarWork.isConnect)
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



                var insEvent = WorkBD.Select_query($"select nc.calendar_name, ie.New_Ev from Ins_Event ie, Name_Calendar nc where nc.Id = ie.Cal_Id");

                foreach (DataRow insert in insEvent.Rows)
                {
                    var Cal = JsonConvert.DeserializeObject<CalendarListEntry>(insert.ItemArray[0].ToString());
                    var Eve = JsonConvert.DeserializeObject<Event>(insert.ItemArray[1].ToString());

                    work.CreateEvent(Eve, Cal.Id);
                }
                WorkBD.Execution_query("delete from Ins_Event");

                var modEvent = WorkBD.Select_query($"select ec.Id, me.New_Event, nc.calendar_name from Mod_Event me, Events_calendar ec, Name_Calendar nc where ec.Id = (select EvendId from Activity where Id = me.RecordId) and nc.id = me.New_Calendar");

                foreach (DataRow mod in modEvent.Rows)
                {
                    var newEvent = JsonConvert.DeserializeObject<Event>(mod.ItemArray[1].ToString());
                    var Cal = JsonConvert.DeserializeObject<CalendarListEntry>(mod.ItemArray[2].ToString());

                    WorkBD.Execution_query($"delete from Events_calendar where id = {mod.ItemArray[0].ToString()}");

                    work.UpdateEvent(newEvent, Cal);
                }


                WorkBD.Execution_query("delete from Mod_Event");



                ActionRun(() => Filling());

                foreach (var items in Calendar_s.ToArray())
                {

                    WorkBD.Execution_query($"if exists(select Id from Name_Calendar where Id_Calendar = N'{items.calendar.Id}') update Name_Calendar set calendar_name = N'{JsonConvert.SerializeObject(items.calendar)}' where Id_Calendar = N'{items.calendar.Id}' else insert into Name_Calendar (calendar_name, Id_Calendar) values (N'{JsonConvert.SerializeObject(items.calendar)}', N'{items.calendar.Id}')");

                    foreach (var events in items.events.ToArray())
                    {
                        var result = WorkBD.Select_query($"if exists(select Id from Activity where NameId = (select Id from Name_Calendar where Id_Calendar = N'{items.calendar.Id}') and EvendId = (select Id from Events_calendar where Id_Event = N'{events.Id}')) select 1 else select 0");


                        if (result.Rows[0].ItemArray[0].ToString() == "1")
                        {
                            WorkBD.Execution_query($"update Events_calendar set Calendar_event = N'{JsonConvert.SerializeObject(events)}' where Id_Event = N'{events.Id}'");
                        }
                        else
                        {
                            //WorkBD.Execution_query($"if exists(select Id from Events_calendar where Id_Event = N'{events.Id}') update Events_calendar set Calendar_event = N'{JsonConvert.SerializeObject(events)}' where Id_Event = N'{events.Id}' else insert into Events_calendar (Calendar_event, Id_Event) values (N'{JsonConvert.SerializeObject(events)}', N'{events.Id}')");
                            WorkBD.Execution_query($"insert into Events_calendar (Calendar_event, Id_Event) values (N'{JsonConvert.SerializeObject(events)}', N'{events.Id}')");
                            WorkBD.Execution_query($"insert into Activity (NameId, EvendId, Date_Event) values ((select Id from Name_Calendar where Id_Calendar = N'{items.calendar.Id}'), (select Id from Events_calendar where Id_Event = N'{events.Id}'), CONVERT(DATETIME, N'{work.DateEvent(events.Start).ToString("yyyyMMdd HH:mm:ss")}', 102) )");
                        }



                        //WorkBD.Execution_query($"insert into Activity (NameId, EvendId, Date_Event) values ((select Id from Name_Calendar where Id_Calendar = N'{items.calendar.Id}'), (select Id from Events_calendar where Id_Event = N'{events.Id}'), CONVERT(DATETIME, N'{work.DateEvent(events.Start).ToString("yyyyMMdd HH:mm:ss")}', 102) )");

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




        #endregion


        private void updEvent_Tick(object sender, EventArgs e)
        {
            //_waitHandle.Set();
            //await SynchronizationAsync();

            AsyncRunMethod(() => SyncTest());

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
        }

        private void upcomingEvents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (Application.OpenForms.Cast<Form>().Any(f => f.Name == "InfoEvent")) Application.OpenForms["InfoEvent"].Close();


            InfoEvent info = new InfoEvent(work, Calendar_s.Find(cal => cal.calendar.Id == upcomingEvents.CurrentRow.Cells[3].Value.ToString()).events.Find(ev => ev.Id == upcomingEvents.CurrentRow.Cells[4].Value.ToString()), Calendar_s.Find(selCal => selCal.calendar.Id == upcomingEvents.CurrentRow.Cells[3].Value.ToString()).calendar);
            info.Show();

        }


        /// <summary>
        /// Выполнение метода в потоке в котором он был создан
        /// </summary>
        /// <param name="action">Делегат</param>
        private void ActionRun(Action action)
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


        private async void AsyncRunMethod(Action action)
        {
            await Task.Run(action);
        }


    }
}
