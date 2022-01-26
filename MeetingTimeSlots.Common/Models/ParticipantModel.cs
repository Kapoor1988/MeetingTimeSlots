using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingTimeSlots.Common.Models
{
    public class ParticipantModel
    {
        public int NoOfParticipants { get; set; }
        public List<PersonModel> Person { get; set; }
    }
}
