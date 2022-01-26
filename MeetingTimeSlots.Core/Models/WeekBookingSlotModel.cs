using MeetingTimeSlots.Core.Models;
using System.Collections.Generic;

namespace MeetingTimeSlot.Core.Models
{
    public class WeekBookingSlotModel
    {
        public int Day { get; set; }
        public List<BookedSlotModel> BookedSlots { get; } = new List<BookedSlotModel>();
    }
}
