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
        private CalendarWork work;


        public AddEvent(CalendarWork wrk)
        {
            InitializeComponent();

            work = wrk;

            calendars.AddRange(work.GetCalendarsName().Items);

            foreach (var item in calendars)
            {
                CalendarsForEvents.Items.Add(item.Summary);
            }

            CalendarsForEvents.Text = CalendarsForEvents.Items[0].ToString();

        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void createEvent_Click(object sender, EventArgs e)
        {
            work.CreateEvent(eventSummary.Text, eventStart.Value, eventEnd.Value, calendars[calendars.IndexOf(calendars.Find(x => x.Summary == CalendarsForEvents.Text))].Id, eventDescription.Text, eventAttendees.Text.Split('\n').ToList(), eventLocation.Text);

            MessageBox.Show("Запись создана!");
            Close();
        }
    }
}
