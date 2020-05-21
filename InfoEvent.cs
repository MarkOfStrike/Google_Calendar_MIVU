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
    public partial class InfoEvent : Form
    {
        private Event SelectEvent;
        private CalendarListEntry SourceCalendar;
        private List<CalendarListEntry> AllCalendars;

        private CalendarWork Work;


        public InfoEvent(CalendarWork work, Event selectEvent, CalendarListEntry selectCalendar)
        {
            InitializeComponent();

            this.Work = work;
            this.Icon = Properties.Resources.Google_Calendar_icon_icons_com_75710;

            SelectEvent = selectEvent ?? throw new ArgumentNullException(nameof(selectEvent));
            SourceCalendar = selectCalendar ?? throw new ArgumentNullException(nameof(selectCalendar));

            AllCalendars = work.GetCalendarsName().Where(x => x.AccessRole != "reader").ToList();


            var dateStart = Work.DateEvent(SelectEvent.Start);
            var dateEnd = Work.DateEvent(SelectEvent.End);

            eventSummary.Text = SelectEvent.Summary;
            eventStart.Value = Work.DateEvent(SelectEvent.Start).Date;
            eventEnd.Value = Work.DateEvent(SelectEvent.End).Date;
            eventDescription.Text = SelectEvent.Description;

            if (SelectEvent.Attendees != null)
            {
                foreach (var attend in SelectEvent.Attendees)
                {
                    eventAttendees.AppendText(attend.Email);
                }
            }

            eventLocation.Text = SelectEvent.Location;

            foreach (var calendar in AllCalendars)
            {
                CalendarsForEvents.Items.Add(calendar.Summary);
            }
            CalendarsForEvents.Text = SourceCalendar.Summary;

        }

        private void canelMod_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void modEvent_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(eventSummary.Text))
            {
                SelectEvent.Summary = SelectEvent.Summary == eventSummary.Text ? SelectEvent.Summary : eventSummary.Text;
                SelectEvent.Start.Date = SelectEvent.Start.Date == eventStart.Value.Date.ToString("yyyy-MM-dd") ? SelectEvent.Start.Date : eventStart.Value.Date.ToString("yyyy-MM-dd");
                SelectEvent.End.Date = SelectEvent.End.Date == eventEnd.Value.Date.ToString("yyyy-MM-dd") ? SelectEvent.End.Date : eventEnd.Value.Date.ToString("yyyy-MM-dd");
                SelectEvent.Description = string.IsNullOrWhiteSpace(eventDescription.Text) ? null : eventDescription.Text;
                SelectEvent.Attendees = string.IsNullOrWhiteSpace(eventAttendees.Text) ? null : AttendeesForEvent(eventAttendees.Text);
                SelectEvent.Location = string.IsNullOrWhiteSpace(eventLocation.Text) ? null : eventLocation.Text;

                Work.UpdateEvent(SelectEvent, SourceCalendar.Id, SourceCalendar != AllCalendars[CalendarsForEvents.SelectedIndex] ? AllCalendars[CalendarsForEvents.SelectedIndex].Id : null);
                Close();
            }
            else
            {
                MessageBox.Show("Название не может быть пустым!");
            }




        }

        /// <summary>
        /// Создание списка гостей
        /// </summary>
        /// <param name="text">Гости</param>
        /// <returns>Список гостей</returns>
        private List<EventAttendee> AttendeesForEvent(string text)
        {
            List<EventAttendee> attendees = new List<EventAttendee>();

            foreach (var mail in text.Split('\n'))
            {
                attendees.Add(new EventAttendee { Email = mail });
            }

            return attendees;
        }

        private void delEvent_Click(object sender, EventArgs e)
        {
            Work.DeleteEvent(SelectEvent, SourceCalendar.Id);
            Close();
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
