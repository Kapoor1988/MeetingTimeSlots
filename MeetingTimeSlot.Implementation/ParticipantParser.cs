using MeetingTimeSlot.Core.Constants;
using MeetingTimeSlot.Core.Models;
using MeetingTimeSlots.Contracts;
using MeetingTimeSlots.Core.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MeetingTimeSlot.Implementation
{
    public class ParticipantParser : IParticipantParser
    {
         private readonly Regex regexGetTime = new Regex(@"\(.*?\)");
        private string regex = @"^[\w]+\=\((?:[01]?[0-9]|2[0-3]):[0-5][0-9]\)\((?:[01]?[0-9]|2[0-3]):[0-5][0-9]\)$";
        private string regexPersonLine = @"^[\w]+\=.*$";
        private string regexDay = @"\bday\=[0-6]$";
        /// <summary>
        /// Parse the file
        /// </summary>
        public ParticipantModel Parser(string fileContent)
        {                    
            ParticipantModel model = new ParticipantModel();
            PersonModel person = new PersonModel();
            WeekBookingSlotModel weekDay = new WeekBookingSlotModel();
            var lines = fileContent.Split('\n');
            try
            {
                if (int.TryParse(lines[0], out int noOfParticipants))
                     model.NoOfParticipants = noOfParticipants;
                else
                    throw new Exception(ErrorMsgConstants.ERRORMSG_FILE_PARSER_FIRST_LINE);

                foreach (var line in lines.Skip(1))
                {
                    if (Regex.IsMatch(line.Replace("\r", ""), regexPersonLine) && !line.Contains("day="))
                    {
                        //this is person line
                        if (Regex.IsMatch(line.Replace("\r", ""), regex))
                        {
                            person = GetPerson(line);
                            model.Persons.Add(person);
                            weekDay = new WeekBookingSlotModel();
                        }
                       else
                            throw new Exception(ErrorMsgConstants.ERRORMSG_FILE_PARSER); 
                       
                    }
                    else if (line.Contains("day="))
                    {
                        // this is a day line
                        if (Regex.IsMatch(line.Replace("\r", ""), regexDay))
                        {
                            weekDay = GetWeekDay(line);
                            person.WeekBookingSlots.Add(weekDay);

                        }
                        else
                        {
                            throw new Exception(ErrorMsgConstants.ERRORMSG_FILE_PARSER);
                        }
                    }
                    else
                    {
                       GetBookingSlots(line.Replace("\r", ""), weekDay);
                    }
                }

                if(model.NoOfParticipants != model.Persons.Count())
                {
                    throw new Exception(ErrorMsgConstants.ERRORMSG_FILE_PARSER_NO_OF_PARTICIPANTS);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
            return model;
        }


        private void GetBookingSlots(string line, WeekBookingSlotModel weekDay)
        {
           var regexTime = @"\((?:[01]?[0-9]|2[0-3]):[0-5][0-9]\)\((?:[01]?[0-9]|2[0-3]):[0-5][0-9]\)$";
            if(weekDay.Day <= 0)
            {
                throw new Exception(ErrorMsgConstants.ERRORMSG_FILE_PARSER);
            }
            if (Regex.IsMatch(line.Replace("\r", ""), regexTime))
            {
                MatchCollection matches = regexGetTime.Matches(line);
                if (matches != null)
                {
                    var bookedSlotTime = new BookedSlotModel
                    {
                        StartTime = matches[0].Value.Replace("(", "").Replace(")", ""),
                        EndTime = matches[1].Value.Replace("(", "").Replace(")", "")
                    };
                    weekDay.BookedSlots.Add(bookedSlotTime);
                }
            }
            else
            {
                throw new Exception(ErrorMsgConstants.ERRORMSG_FILE_PARSER);
            }
           
        }

        private PersonModel GetPerson(string line)
        {
            var person = new PersonModel();
            var personLine = line.Split("=");
            person.PersonId = personLine[0];

            var matches = regexGetTime.Matches(personLine[1]);
            person.DayStartTime = matches[0].Value.Replace("(", "").Replace(")", "");
            person.DayEndTime = matches[1].Value.Replace("(", "").Replace(")", "");

            return person;
        }
        private WeekBookingSlotModel GetWeekDay(string line)
        {
            var weekDay = new WeekBookingSlotModel();
            var dayLine = line.Split("=");
            weekDay.Day = Convert.ToInt32(dayLine[1]);
            
            return weekDay;
        }
    }
}
