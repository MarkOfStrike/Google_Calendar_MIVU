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
        private Event SelectEvent { get; set; }
        private CalendarListEntry SourceCalendar { get; set; }
        private List<CalendarListEntry> AllCalendars { get; set; }

        private Event workEvent;

        private CalendarWork Work;


        public InfoEvent(CalendarWork work ,Event selectEvent, CalendarListEntry selectCalendar/*, List<CalendarListEntry> allCalendars*/)
        {
            InitializeComponent();

            this.Work = work;

            SelectEvent = selectEvent ?? throw new ArgumentNullException(nameof(selectEvent));//(?? оператор null-объединения)Объединение нулей x??y будет равно y если x = null, в противном случае равно x
            SourceCalendar = selectCalendar ?? throw new ArgumentNullException(nameof(selectCalendar));

            //AllCalendars = allCalendars ?? throw new ArgumentNullException(nameof(allCalendars));
            AllCalendars = work.GetCalendarsName();



            workEvent = SelectEvent;

            var dateStart = Work.DateEvent(SelectEvent.Start);
            var dateEnd = Work.DateEvent(SelectEvent.End);

            eventSummary.Text = SelectEvent.Summary;
            eventStart.Value = dateStart.Date;
            //eventStart.Value = DateTime.Parse(SelectEvent.Start.DateTime.ToString() ?? SelectEvent.Start.Date ?? SelectEvent.Start.DateTimeRaw);
            eventEnd.Value = dateEnd.Date;
            //eventEnd.Value = DateTime.Parse(SelectEvent.End.DateTime.ToString() ?? SelectEvent.End.Date ?? SelectEvent.End.DateTimeRaw).Date;
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
                SelectEvent.Start.DateTime = SelectEvent.Start.DateTime == eventStart.Value ? SelectEvent.Start.DateTime : eventStart.Value;
                SelectEvent.End.DateTime = SelectEvent.End.DateTime == eventEnd.Value ? SelectEvent.End.DateTime : eventEnd.Value;
                SelectEvent.Description = string.IsNullOrWhiteSpace(eventDescription.Text) ? null : eventDescription.Text;
                SelectEvent.Attendees = string.IsNullOrWhiteSpace(eventAttendees.Text) ? null : AttendeesForEvent(eventAttendees.Text);
                SelectEvent.Location = string.IsNullOrWhiteSpace(eventLocation.Text) ? null : eventLocation.Text;

                Work.UpdateEvent(SelectEvent, SourceCalendar, SourceCalendar != AllCalendars[CalendarsForEvents.SelectedIndex] ? AllCalendars[CalendarsForEvents.SelectedIndex].Id : null);
                Close();
            }
            else
            {
                MessageBox.Show("Название не может быть пустым!");
            }

            


        }

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
    }
}
