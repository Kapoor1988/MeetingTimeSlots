using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MeetingTimeSlot.Test
{
    public class BaseTest
    {
		public string FilePath { get; protected set; }
		protected string ReadTestFile(string filename)
		{
			var filePath = Path.Combine(FilePath, filename);
			if (!File.Exists(filePath))
				return null;

			return File.ReadAllText(filePath);
		}
	}
}
