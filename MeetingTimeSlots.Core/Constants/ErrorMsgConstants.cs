namespace MeetingTimeSlot.Core.Constants
{
    public class ErrorMsgConstants
    {
        public const string ERRORMSG_FILE_Empty = "File is empty";
        public const string ERRORMSG_FILE_PARSER_FIRST_LINE = "Error in parsing the file. File should start with Number of Participant";
        public const string ERRORMSG_FILE_PARSER = "Error in parsing the file";
        public const string ERRORMSG_FILE_PARSER_NO_OF_PARTICIPANTS = "Participants detail is missing in the file";
        public const string ERRORMSG_FILE_NOT_EXISTS = "{0} file does not exists";
        public const string ERRORMSG_DURATION_INVALID = "{0} duration is invalid. Value should be integer";
        public const string ERRORMSG_NO_OF_SLOTS_INVALID = "{0} is invalid. NoOfSlots should be integer";
        public const string ERRORMSG_ARGUMENTS_INVALID = "Invalid application Arguments. Arguments should in format <file> <timeRange> <noOfSlots> ";
    }
}
