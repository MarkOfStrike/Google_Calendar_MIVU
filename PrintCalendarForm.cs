using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Google_Calendar_Desktop_App
{
    public partial class PrintCalendarForm : Form
    {
        private List<Images_Calendar> calendarsImage = new List<Images_Calendar>();
        private List<Event> allEvents = new List<Event>();

        private int index = 0;
        private int indexPrint = 0;

        public PrintCalendarForm(List<Event> events)
        {
            InitializeComponent();

            this.Icon = Properties.Resources.Google_Calendar_icon_icons_com_75710;


            allEvents = events;

            startDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            endDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

            Calendar(new Dates { start = startDate.Value.Date, end = endDate.Value.Date });

            printDoc.DefaultPageSettings.Landscape = true;

            printDoc.PrintPage += PrintDoc_PrintPage;

        }

        private void PrintDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(calendarsImage[indexPrint].headerText, new Font("Microsoft Sans Serif", (float)16.25, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, new Point(40, 40), StringFormat.GenericTypographic);
            e.Graphics.DrawImage(calendarsImage[indexPrint].imageCalendar, 40, 80, e.PageBounds.Width - 90, e.PageBounds.Width / 2);

            indexPrint++;

            e.HasMorePages = (indexPrint < calendarsImage.Count);

            if (!e.HasMorePages)
            {
                indexPrint = 0;
            }

        }

        /// <summary>
        /// Следующее изображение
        /// </summary>
        private void PlusIndex()
        {
            if (index == calendarsImage.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }

            ViewCalendar();

        }

        /// <summary>
        /// Предыдущее изображение
        /// </summary>
        private void MinusIndex()
        {
            if (index == 0)
            {
                index = calendarsImage.Count - 1;
            }
            else
            {
                index--;
            }

            ViewCalendar();
        }

        /// <summary>
        /// Просмотр календаря
        /// </summary>
        private void ViewCalendar()
        {
            Bitmap img = new Bitmap(calendarsImage[index].imageCalendar);
            calendarImg.SizeMode = PictureBoxSizeMode.StretchImage;
            calendarImg.Image = img;
            calendarImg.Refresh();

            selectPage.Text = (index + 1).ToString();
            countPage.Text = calendarsImage.Count.ToString();


        }

        /// <summary>
        /// Заполнение и перерисовка таблицы в изображение
        /// </summary>
        /// <param name="dates">Даты</param>
        private void Calendar(Dates dates)
        {
            index = 0;
            calendarsImage.Clear();
            List<Dates> datesEvent = new List<Dates>();

            int count = dates.end.Month - dates.start.Month;

            for (int i = 0; i <= count; i++)
            {
                if (dates.end.Month - dates.start.Month == 0)
                {
                    datesEvent.Add(new Dates
                    {
                        start = dates.start,
                        end = dates.end
                    });
                }
                else
                {
                    datesEvent.Add(new Dates
                    {
                        start = dates.start,
                        end = new DateTime(dates.start.Year, dates.start.Month, DateTime.DaysInMonth(dates.start.Year, dates.start.Month))
                    });
                }


                dates.start = dates.start.AddDays(DateTime.DaysInMonth(dates.start.Year, dates.start.Month) - dates.start.Day + 1);
            }

            foreach (var date in datesEvent)
            {
                Drawing_Calendar(date);
            }


            ViewCalendar();
        }

        /// <summary>
        /// Название месяца с годом
        /// </summary>
        /// <param name="month">Месяц</param>
        private string Month(DateTime date)
        {
            string result = "";

            switch (date.Month)
            {
                case 1:
                    result = $"Январь {date.Year}";
                    break;
                case 2:
                    result = $"Февраль {date.Year}";
                    break;
                case 3:
                    result = $"Март {date.Year}";
                    break;
                case 4:
                    result = $"Апрель {date.Year}";
                    break;
                case 5:
                    result = $"Май {date.Year}";
                    break;
                case 6:
                    result = $"Июнь {date.Year}";
                    break;
                case 7:
                    result = $"Июль {date.Year}";
                    break;
                case 8:
                    result = $"Август {date.Year}";
                    break;
                case 9:
                    result = $"Сентябрь {date.Year}";
                    break;
                case 10:
                    result = $"Октябрь {date.Year}";
                    break;
                case 11:
                    result = $"Ноябрь {date.Year}";
                    break;
                case 12:
                    result = $"Декабрь {date.Year}";
                    break;

                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// Отрисовка календаря в DataGridView
        /// </summary>
        /// <param name="first">Первый день месяца</param>
        private void Drawing_Calendar(Dates dates)
        {
            DataGridView table = new DataGridView();

            table.Width = 900;
            table.Height = 430;


            table.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            table.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            table.AllowUserToAddRows = false;
            table.AllowUserToDeleteRows = false;
            table.ReadOnly = true;
            table.RowHeadersVisible = false;
            table.BackgroundColor = Color.White;
            table.BorderStyle = BorderStyle.None;
            table.RowsDefaultCellStyle = new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True, Alignment = DataGridViewContentAlignment.TopCenter, SelectionBackColor = Color.White, SelectionForeColor = Color.Black };

            table.Columns.Add("Column1", "Понедельник");
            table.Columns.Add("Column2", "Вторник");
            table.Columns.Add("Column3", "Среда");
            table.Columns.Add("Column4", "Четверг");
            table.Columns.Add("Column5", "Пятница");
            table.Columns.Add("Column6", "Суббота");
            table.Columns.Add("Column7", "Воскресенье");

            int firstDayOfWeek = 0;

            switch (new DateTime(dates.start.Year, dates.start.Month, 1).DayOfWeek.ToString())
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

            for (int i = 0; i < 6; i++)
            {
                table.Rows.Add();
                for (int j = 0; j < table.Rows[i].Cells.Count; j++)
                {
                    if (i == 0 && j < firstDayOfWeek)
                    {
                        while (j < firstDayOfWeek - 1)
                        {
                            j++;
                        }
                    }

                    if ((day < dates.start.Day && dates.start.Day != 1) || (day > dates.end.Day && day <= DateTime.DaysInMonth(dates.start.Year, dates.start.Month)))
                    {
                        table.Rows[i].Cells[j].Value = $"{day} \n\n\n\n";
                    }

                    if (day >= dates.start.Day && day <= dates.end.Day)
                    {
                        table.Rows[i].Cells[j].Value = EventDay(new DateTime(dates.start.Year, dates.start.Month, day));
                    }

                    day++;

                }

                int cell = 0;
                bool del = true;
                while (cell < table.Rows[i].Cells.Count)
                {
                    if (table.Rows[i].Cells[cell].Value != null)
                    {
                        del = false;
                    }

                    cell++;
                }

                if (del)
                {
                    table.Rows.RemoveAt(i);
                }
            }

            if (table.SelectedCells.Count > 0)
            {
                table.SelectedCells[0].Selected = false;
            }

            Bitmap imgCalendar = new Bitmap(table.Width, table.Height);

            table.DrawToBitmap(imgCalendar, new Rectangle(0, 0, table.Width, table.Height));

            Image img = Image.FromHbitmap(imgCalendar.GetHbitmap());

            calendarsImage.Add(new Images_Calendar { headerText = Month(dates.start), imageCalendar = img });

        }

        /// <summary>
        /// Событие дня в календаре
        /// </summary>
        /// <param name="day">День</param>
        /// <returns>Все события дня для оотображения в календаре</returns>
        private string EventDay(DateTime date)
        {
            string result = "";
            List<string> events = new List<string>();

            events.AddRange(allEvents.FindAll(p => DateEvent(p.Start).Date == date.Date).Select(s => s.Summary).ToList());

            if (events.Count > 3)
            {
                result = $"{date.Day} \n\n";

                foreach (var nameEvent in events)
                {
                    result += $"{nameEvent}\n";
                }


            }
            else if (events.Count > 0 && events.Count <= 3)
            {
                result = $"{date.Day} \n";
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
                result = $"{date.Day} \n\n\n\n";
            }

            return result;
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

        private void PrevBtn_Click(object sender, EventArgs e)
        {
            MinusIndex();
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            PlusIndex();
        }

        private void printBtn_Click(object sender, EventArgs e)
        {
            if (setupPageForPrint.ShowDialog() == DialogResult.OK)
            {
                previewDocument.ShowDialog();
            }

            indexPrint = 0;
        }

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            if (startDate.Value >= endDate.Value)
            {
                endDate.Value = startDate.Value;
                endDate.Value.AddDays(1);
            }

            Calendar(new Dates { start = startDate.Value.Date, end = endDate.Value.Date });
        }

        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            if (endDate.Value <= startDate.Value)
            {
                startDate.Value = endDate.Value;
                startDate.Value.AddDays(-1);
            }

            Calendar(new Dates { start = startDate.Value.Date, end = endDate.Value.Date });
        }
    }
}
