using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingTimeSlots.Common.Models
{
    public class PersonModel
    {
        public string PersonId { get; set; }
        public DateTime DayStartTime { get; set; }
        public DateTime DayEndTime { get; set; }
        public List<BookedSlotModel> BookedSlots { get; set; }
    }
}
