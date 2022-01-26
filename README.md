# MeetingTimeSlots
The program will provide the available slots for a given set of participants.

## How to use
Run below command
MeetingTimeSlots.exe <filepath> <timeRange> <howMany>
filepath(Manadatory):File that contains participants list with booked slots
timeRange(Not Manadatory):- It specifies the duration of time slots. Default is 30. Value is in minutes
howMany(Not Manadatory):- It specifies the number of available slots to show. Default value is 2
Example:- MeetingTimeSlots.exe c:\ participant_list.txt 30 2

##FileFormat

File should be in below format. File should start with number of participants in the file and then details for each participant

2
A=(08:00)(16:00)
day=1
(08:15)(09:15)
(10:00)(11:30)
(13:00)(15:00)
B=(09:00)(17:00)
day=1
(09:15)(09:30)
(10:00)(11:00)
(11:30)(11:45)
(12:00)(14:00)
day=2
(09:15)(09:30)
(10:00)(11:00)

## Suggestions

-	Instead of file, we will store the booking slots and participants information in the database.
-	And will provide functionality for adding more participants and booking information.
-	Currently, we are handling booking slots for days (for one week). We can enhance this to handle weeks, months and year slots.
-	Include free time before or after the slots

## Assumptions

-	If one of the day is missing in one of the participants in the list. Then we assume that person is avaliable for a day.


## Comments

-	Will add exception handling.
-	Instead of displaying generic error message while parsing file. we display more appropitae errors.
-  	Will use enum to displays day in a week instead of day number.
-   If one of the day is missing from whole file,then we are not display avaliable slot for that day.
	example
	As in the above file format, we are only displaying avaliable slot for day 1 and day 2.
  
