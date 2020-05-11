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
        /// <summary>
        /// Ид календаря
        /// </summary>
        public string idCalendar;

        /// <summary>
        /// Имя календаря
        /// </summary>
        public string nameCalendar;

        /// <summary>
        /// Календарь
        /// </summary>
        public CalendarListEntry calendar;

        /// <summary>
        /// События календаря
        /// </summary>
        public List<Event> events = new List<Event>();
    }
}
