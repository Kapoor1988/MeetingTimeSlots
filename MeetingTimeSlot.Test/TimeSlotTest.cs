using MeetingTimeSlot.Contracts;
using MeetingTimeSlot.Implementation;
using MeetingTimeSlots.Contracts;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace MeetingTimeSlot.Test
{
    public class TimeSlotTest : BaseTest
	{
		private string filePath;
		private IParticipantParser _participantParser;
		private ITimeSlots _timeSlots;

		[SetUp]
		public void SetUp()
		{
			var projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
			FilePath = Path.Combine(projectDir.FullName,"TestFiles");

			_participantParser = new ParticipantParser();
			_timeSlots= new TimeSlots();


		}
		
		[Test]
		public void TimeSlot_FileContent_AvailableSlot()
		{
			//Arrange
			string fileContent = ReadTestFile("participant_list.txt");

			//Act
			var model = _participantParser.Parser(fileContent);
			var result = _timeSlots.GetAvailableSlots(model);

			var availableTimePeriods1 = result.Where(x => x.Day == 1).Select(x => x.AvailableTimePeriods).FirstOrDefault();
			//Assert
			Assert.AreEqual(3, availableTimePeriods1.Count());

		}
		[Test]
		public void TimeSlot_FileContent_AvailableSlot_With_Duration()
		{
			//Arrange
			string fileContent = ReadTestFile("participant_list.txt");

			//Act
			var model = _participantParser.Parser(fileContent);
			var availableSlots = _timeSlots.GetAvailableSlots(model);
			var result = _timeSlots.GetTimeSlotsWithDuration(availableSlots,  30);

			var timePeriod = result.Select(x => x.AvailableTimePeriods).FirstOrDefault();
			//Assert
			Assert.AreEqual(3, timePeriod.Count());

		}
	}
}
