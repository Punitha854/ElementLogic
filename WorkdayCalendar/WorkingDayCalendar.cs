namespace WorkdayCalendar
{
    // This class acts as a main singleton class with one single Instance. Resourecs locking also enabled here
    public class WorkingDayCalendar
    {
        private object lockObject = new();

        public readonly int workingHours = 8;
        public List<DateTime> RecurringHolidays { get; set; } = new();
        public List<DateTime> PublicHolidays { get; set; } = new();
        public DateTime selectedDateTime { get; set; } = DateTime.Now;

        public DateTime WorkingDateStarts { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 08, 0, 0);
        public DateTime WorkingDateEnds { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0);

        // Recurring holiday collection list is added and verified for duplicates
        public List<DateTime> addRecurringDays(DateTime givenDate)
        {
            if (!RecurringHolidays.Contains(givenDate))
                this.RecurringHolidays.Add(givenDate);
            return RecurringHolidays;
        }

        // Public holiday collection list is added and verified for duplicates
        public List<DateTime> addPublicHolidays(DateTime givenDate)
        {
            if (!PublicHolidays.Contains(givenDate))
                this.PublicHolidays.Add(givenDate);
            return PublicHolidays;
        }
        //This function return the working day from the given date and count 
        public DateTime getResultDatetime(double workingDays)//, int currentHour = 0, int currentMinute = 0)
        {
            DateTime result = this.selectedDateTime;
            // Resources are locked for multiple access
            lock (lockObject)
            {
                int days = Convert.ToInt32(Math.Truncate(workingDays));
                double hours = (Math.Abs(workingDays) - Math.Abs(days)) * workingHours;
                int currentHour = this.selectedDateTime.Hour;
                bool temp = (this.selectedDateTime.Hour > WorkingDateEnds.Hour) || this.selectedDateTime.Hour < WorkingDateStarts.Hour;
                int currentMinute = temp ? 0 : this.selectedDateTime.Minute;
                double minute = currentMinute;
                if ((hours - (int)hours) > 0)
                    minute += (hours - (int)hours) * 60;
                bool IsNegative = false;
                if (days < 0) IsNegative = true;


                DateTime currentDateTime = new DateTime(2024, 1, 1, currentHour, currentMinute, 0);
                DateTime currentDateTime1 = new DateTime(2024, 1, 1, WorkingDateStarts.Hour, 0, 0);
                DateTime currentDateTime2 = new DateTime(2024, 1, 1, WorkingDateEnds.Hour, 0, 0);

                if (currentDateTime <= currentDateTime2 && currentDateTime >= currentDateTime1)
                {
                    currentHour += (int)hours;
                    if (currentHour >= WorkingDateEnds.Hour)
                    {
                        hours = currentHour - WorkingDateEnds.Hour;
                        if (hours > 0 || minute > 0) days++;
                    }
                }
                else
                {
                    if (currentDateTime.Hour >= WorkingDateEnds.Hour && currentDateTime.Hour <= 24)
                    {
                        if (IsNegative)
                            days--;
                        else days++;
                    }
                }

                hours = Math.Abs(hours) + WorkingDateStarts.Hour; int i = 0;

                while (i < Math.Abs(days))
                {
                    if (!(selectedDateTime.DayOfWeek == DayOfWeek.Sunday
                        || selectedDateTime.DayOfWeek == DayOfWeek.Saturday
                        || IsRecurringHolidaysExists(selectedDateTime.Date)
                        || this.PublicHolidays.Contains(selectedDateTime.Date)))
                    {
                        i++;
                    }
                    selectedDateTime = IsNegative ? selectedDateTime.AddDays(-1) : selectedDateTime.AddDays(1);
                }
                if (IsNegative)
                {
                    if (currentHour >= WorkingDateEnds.Hour)
                    {
                        if (hours > 0 || minute > 0)
                            selectedDateTime = selectedDateTime.AddDays(1);
                    }
                    hours = WorkingDateStarts.Hour + (WorkingDateEnds.Hour - hours);
                    if (minute != 0) minute = 60 - (int)minute;

                }

                result = new DateTime(selectedDateTime.Year, selectedDateTime.Month, selectedDateTime.Day,
                                     (int)hours, (int)minute, 0);
            }
            return result;
        }
        
        //Since recurring Holidays is common for all the year it is also added as public holiday will compare only the monthe and the day
        private bool IsRecurringHolidaysExists(DateTime selectedDateTime)
        {
            foreach (var holiday in this.RecurringHolidays)
            {
                if (holiday.Month == selectedDateTime.Month &&
                    holiday.Day == selectedDateTime.Day)
                    return true;
            }
            return false;
        }


    }
}

