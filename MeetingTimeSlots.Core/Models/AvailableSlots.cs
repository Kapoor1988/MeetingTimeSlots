using Itenso.TimePeriod;
using System.Collections.Generic;

namespace MeetingTimeSlot.Core.Models
{
    public class AvailableSlots
    {
        public int Day { get; set; }
        public ITimePeriodCollection AvailableTimePeriods { get; set; }
        public List<string> TimePeriods { get; set; }
    }
}
