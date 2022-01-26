using Itenso.TimePeriod;
using MeetingTimeSlot.Contracts;
using MeetingTimeSlot.Core.Models;
using MeetingTimeSlots.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingTimeSlot.Implementation
{
    public class TimeSlots : ITimeSlots
    {
        /// <summary>
        /// Get all the available time slots
        /// </summary>
        public List<AvailableSlots> GetAvailableSlots(ParticipantModel model)
        {
            var weekBookingSlots = model.Persons.SelectMany(x => x.WeekBookingSlots.GroupBy(s => s.Day));
        

            List<AvailableSlots> availableSlots = new List<AvailableSlots>();

            foreach (var group in weekBookingSlots.GroupBy(x=>x.Key))
            {
                TimePeriodCollection collection = new TimePeriodCollection();
                var availableSlot = new AvailableSlots { Day = group.Key };

                var startDayTime = model.Persons.Max(x => x.DayStartTime);
                var endDayTime = model.Persons.Min(x => x.DayEndTime);

                CalendarTimeRange searchLimits = new CalendarTimeRange(Convert.ToDateTime(startDayTime), Convert.ToDateTime(endDayTime));

                foreach (var participant in group.SelectMany(x => x))
                {
                    foreach (var bookingSlot in participant.BookedSlots)
                    {
                        TimeRange range = new TimeRange(Convert.ToDateTime(bookingSlot.StartTime), Convert.ToDateTime(bookingSlot.EndTime));
                        collection.Add(range);
                    }
                }

                TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>(new TimeCalendar());

                availableSlot.AvailableTimePeriods = gapCalculator.GetGaps(collection, searchLimits);

                availableSlots.Add(availableSlot);
            }


            return availableSlots;
        }

        /// <summary>
        /// Get the available time slots with duration
        /// </summary>
        public List<AvailableSlots> GetTimeSlotsWithDuration(List<AvailableSlots> availableSlots, int duration )
        {
            if (availableSlots != null)
            {
                foreach (var availableSlot in availableSlots)
                {
                    List<string> timePeriods = new List<string>();

                    foreach (var timePeriod in availableSlot.AvailableTimePeriods)
                    {
                        if (Math.Round(timePeriod.Duration.TotalMinutes) >= duration)
                        {
                            if ((timePeriod.End - timePeriod.Start).TotalMinutes > duration)
                            {
                                var startTime = timePeriod.Start;
                                while (Math.Round((timePeriod.End - startTime).TotalMinutes) >= duration)
                                {
                                    timePeriods.Add($"({startTime:HH\\:mm})({startTime.AddMinutes(duration):HH\\:mm})");
                                    startTime = startTime.AddMinutes(duration);
                                }
                            }
                            else
                                timePeriods.Add($"({timePeriod.Start:HH\\:mm})({timePeriod.End:HH\\:mm})");
                        }


                    }
                    availableSlot.TimePeriods = timePeriods;
                }

            }
            return availableSlots;
        }
        
    }
}
