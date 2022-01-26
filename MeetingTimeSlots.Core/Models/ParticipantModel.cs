using System.Collections.Generic;

namespace MeetingTimeSlots.Core.Models
{
    public class ParticipantModel
    {
        public int NoOfParticipants { get; set; }
        public List<PersonModel> Persons { get; } = new List<PersonModel>();
    }
}
