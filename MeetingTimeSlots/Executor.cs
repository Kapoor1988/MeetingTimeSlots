using MeetingTimeSlot.Contracts;
using MeetingTimeSlot.Core.Constants;
using MeetingTimeSlots.Contracts;
using MeetingTimeSlots.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MeetingTimeSlots
{
    public class Executor
    {
        private readonly IParticipantParser _participantParser;
        private readonly ITimeSlots _timeSlots;
        private static int _noOfSlots = 2;
        private static int _duration = 30;

        public Executor(IParticipantParser participantParser,
            ITimeSlots timeSlots)
        {
            _participantParser = participantParser;
            _timeSlots = timeSlots;
        }
        public void Execute(string[] args)
        {
            ReadInput(args);
        }

        //Get the user Input
        private void ReadInput(string[] args)
        {

            // validate the user input
            List<string> errors = MeetingTimeSlotsValidator.UserInputValidator(args);
            if (errors.Any())
            {
                foreach (var error in errors)
                    Console.WriteLine(error);

                Exit();
            }
            else
            {
                string filePath = args[0];
                if (args.Length >= 2)
                    _duration = Convert.ToInt32(args[1]);

                if (args.Length == 3)
                    _noOfSlots = Convert.ToInt32(args[2]);

                //read the file content
                var fileContent = GetFileContent(filePath);
                if(string.IsNullOrEmpty(fileContent) || string.IsNullOrWhiteSpace(fileContent))
                {
                    Console.WriteLine(ErrorMsgConstants.ERRORMSG_FILE_Empty);
                    Exit();
                }

                try
                {
                    //parse the file
                    ParticipantModel model = _participantParser.Parser(fileContent);

                    if (model != null)
                    {
                        // get all the available slots
                        var availableSlots = _timeSlots.GetAvailableSlots(model);

                        // get the time slots within the time range
                        var timeSlots = _timeSlots.GetTimeSlotsWithDuration(availableSlots, _duration);
                        if (timeSlots != null && timeSlots.SelectMany(x=>x.AvailableTimePeriods).Count() > 0)
                        {
                            Console.WriteLine("Below the available slots:");
                            foreach (var timeSlot in timeSlots)
                            {
                                Console.WriteLine($"day={timeSlot.Day}");
                                foreach (var timePeriod in timeSlot.TimePeriods.Take(_noOfSlots))
                                {
                                    Console.WriteLine(timePeriod);
                                }
                            }

                            Console.WriteLine("Please press enter to exit");
                            Exit();
                        }
                        else
                        {
                            Console.WriteLine("No time slots available");
                            Console.WriteLine("Please press enter to exit");
                            Exit();
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine(ErrorMsgConstants.ERRORMSG_FILE_PARSER);
                        Exit();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }

           
        }

        private static void Exit()
        {
            var line = Console.ReadLine();
            if (string.IsNullOrEmpty(line))
                Environment.Exit(0);
        }

        private static string GetFileContent(string filePath)
        {
            return File.ReadAllText(filePath);
        }
        
    }
}