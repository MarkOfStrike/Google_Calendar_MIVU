using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google_Calendar_Desktop_App
{
    class Calendar_Events
    {
        public string idCalendar;
        public string nameCalendar;

        public CalendarListEntry calendar;

        public List<Event> events = new List<Event>();

    }
}
