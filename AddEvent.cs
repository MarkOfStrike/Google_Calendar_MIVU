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
    public partial class AddEvent : Form
    {
        private List<CalendarListEntry> calendars = new List<CalendarListEntry>();

        public AddEvent()
        {
            InitializeComponent();

            CalendarWork work = new CalendarWork();

            calendars.AddRange(work.GetCalendarsName().Items);

            foreach (var item in calendars)
            {
                CalendarsForEvents.Items.Add(item.Summary);
            }

            CalendarsForEvents.Text = CalendarsForEvents.Items[0].ToString();

        }
    }
}
