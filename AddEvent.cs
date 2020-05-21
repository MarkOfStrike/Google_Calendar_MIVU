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

            this.work = wrk;
            this.Icon = Properties.Resources.Google_Calendar_icon_icons_com_75710;

            calendars = work.GetCalendarsName().Where(c => c.AccessRole != "reader").ToList();

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
            if (!string.IsNullOrWhiteSpace(eventSummary.Text))
            {
                work.CreateEvent(eventSummary.Text, eventStart.Value, eventEnd.Value, calendars[CalendarsForEvents.SelectedIndex].Id, string.IsNullOrEmpty(eventDescription.Text) ? null : eventDescription.Text, string.IsNullOrEmpty(eventAttendees.Text) ? null : eventAttendees.Text.Split('\n').ToList(), string.IsNullOrEmpty(eventLocation.Text) ? null : eventLocation.Text);

                Close();
            }
            else
            {
                MessageBox.Show("Введите название события!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            
        }

        private void eventStart_ValueChanged(object sender, EventArgs e)
        {
            if (eventStart.Value > eventEnd.Value)
            {
                eventEnd.Value = eventStart.Value;
            }
        }

        private void eventEnd_ValueChanged(object sender, EventArgs e)
        {
            if (eventEnd.Value < eventStart.Value)
            {
                eventStart.Value = eventEnd.Value;
            }
        }
    }
}
