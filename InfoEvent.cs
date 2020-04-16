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


        public InfoEvent(Event selectEvent, CalendarListEntry selectCalendar, List<CalendarListEntry> allCalendars)
        {
            InitializeComponent();

            SelectEvent = selectEvent ?? throw new ArgumentNullException(nameof(selectEvent));//(?? оператор null-объединения)Объединение нулей x??y будет равно y если x = null, в противном случае равно x
            SourceCalendar = selectCalendar ?? throw new ArgumentNullException(nameof(selectCalendar));
            AllCalendars = allCalendars ?? throw new ArgumentNullException(nameof(selectCalendar));
            


            workEvent = SelectEvent;

            eventSummary.Text = SelectEvent.Summary;
            eventStart.Value = Convert.ToDateTime(SelectEvent.Start).Date;
            eventEnd.Value = Convert.ToDateTime(SelectEvent.End).Date;
            eventDescription.Text = SelectEvent.Description;
            foreach (var attend in SelectEvent.Attendees)
            {
                eventAttendees.AppendText(attend.Email);
            }
            eventLocation.Text = SelectEvent.Location;

            foreach (var calendar in AllCalendars)
            {
                CalendarsForEvents.Items.Add(calendar);
            }
            CalendarsForEvents.Text = SourceCalendar.Summary;




        }





    }
}
