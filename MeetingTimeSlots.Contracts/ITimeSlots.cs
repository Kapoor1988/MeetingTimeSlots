using Itenso.TimePeriod;
using MeetingTimeSlot.Core.Models;
using MeetingTimeSlots.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingTimeSlot.Contracts
{
    public interface ITimeSlots
    {
        List<AvailableSlots> GetAvailableSlots(ParticipantModel model);
        List<AvailableSlots> GetTimeSlotsWithDuration(List<AvailableSlots> availableSlots, int duration);
    }
}
