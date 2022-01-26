using MeetingTimeSlot.Core.Constants;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MeetingTimeSlots
{
    public static class MeetingTimeSlotsValidator
    {
        public static List<string> UserInputValidator(string[] args)
        {
            List<string> errors = new List<string>();

            if (args.Count() == 0 || args.Count() > 3)
                errors.Add(ErrorMsgConstants.ERRORMSG_ARGUMENTS_INVALID);
            else if (args[0] != null)
            {
                var filePath = args[0];
                if (!File.Exists(filePath))
                {
                    errors.Add(ErrorMsgConstants.ERRORMSG_FILE_NOT_EXISTS);
                }

                if (args.Count() >= 2)
                {
                    if (!int.TryParse(args[1], out _))
                    {
                        errors.Add(ErrorMsgConstants.ERRORMSG_DURATION_INVALID);
                    }
                }
                if (args.Count() == 3)
                {
                    if (!int.TryParse(args[2], out _))
                    {
                        errors.Add(ErrorMsgConstants.ERRORMSG_NO_OF_SLOTS_INVALID);
                    }
                }
            }
            return errors;


        }
    }
}
