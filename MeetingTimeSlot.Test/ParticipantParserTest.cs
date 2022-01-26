using MeetingTimeSlot.Implementation;
using MeetingTimeSlots.Contracts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MeetingTimeSlot.Test
{
    public class ParticipantParserTest : BaseTest
	{
		private string filePath;
		private IParticipantParser _participantParser;

		[SetUp]
		public void SetUp()
		{
			var projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
			FilePath = Path.Combine(projectDir.FullName, "TestFiles");

			_participantParser = new ParticipantParser();


		}
		[Test]
		public void ParseFile_FileContent_Success()
		{
			//Arrange
			string fileContent = ReadTestFile("participant_list.txt");

			//Act
			var result = _participantParser.Parser(fileContent);

			//Assert
			Assert.AreEqual(2, result.NoOfParticipants);
			Assert.IsTrue(result.NoOfParticipants.Equals(result.Persons.Count));

			var bookedSlotA = result.Persons.Where(x => x.PersonId == "A");
			var bookedSlotB = result.Persons.Where(x => x.PersonId == "B");
			Assert.AreEqual(3, bookedSlotA.SelectMany(x => x.WeekBookingSlots).Select(x=>x.BookedSlots.Count).FirstOrDefault());
			Assert.AreEqual(4, bookedSlotB.SelectMany(x => x.WeekBookingSlots).Select(x => x.BookedSlots.Count).FirstOrDefault());


		}
		[Test]
		public void ParseFile_FileContent_Missing_Participants()
		{
			//Arrange
			string fileContent = ReadTestFile("participant_list_missing.txt");

            try
            {
				//Act
				var result = _participantParser.Parser(fileContent);
			}
            catch (Exception ex)
            {
				//Assert
				Assert.AreEqual("Participants detail is missing in the file", ex.Message);
            }
			
		}
		[Test]
		public void ParseFile_FileContent_Missing_FirstLine()
		{
			//Arrange
			string fileContent = ReadTestFile("participant_list_missing_firstline.txt");

			try
			{
				//Act
				var result = _participantParser.Parser(fileContent);
			}
			catch (Exception ex)
			{
				//Assert
				Assert.AreEqual("Error in parsing the file. File should start with Number of Participant", ex.Message);
			}

		}
		[Test]
		public void ParseFile_FileContent_Invalid_Time()
		{
			//Arrange
			string fileContent = ReadTestFile("participant_list_invalid_time.txt");

			try
			{
				//Act
				var result = _participantParser.Parser(fileContent);
			}
			catch (Exception ex)
			{
				//Assert
				Assert.AreEqual("Error in parsing the file", ex.Message);
			}

		}
		
	}
}
