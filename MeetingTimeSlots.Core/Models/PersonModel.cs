using MeetingTimeSlot.Core.Models;
using System.Collections.Generic;

namespace MeetingTimeSlots.Core.Models
{
    public class PersonModel
    {
        public string PersonId { get; set; }
        public string DayStartTime { get; set; }
        public string DayEndTime { get; set; }
        public List<WeekBookingSlotModel> WeekBookingSlots { get; } = new List<WeekBookingSlotModel>();
    }
}
