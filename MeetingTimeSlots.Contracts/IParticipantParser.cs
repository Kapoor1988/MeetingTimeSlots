using MeetingTimeSlots.Core.Models;
using System;

namespace MeetingTimeSlots.Contracts
{
    public interface IParticipantParser
    {
        ParticipantModel Parser(string filePath);
    }
}
