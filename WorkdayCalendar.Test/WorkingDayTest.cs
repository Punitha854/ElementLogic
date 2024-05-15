namespace WorkdayCalendar.Test
{
    [TestClass]
    public class WorkingDayTest
    {
        WorkingDayCalendar _workDayCalendar = new WorkingDayCalendar();
        double workingDays = 1.5;


        [TestInitialize]
        public void Initialize()
        {
            _workDayCalendar.selectedDateTime = DateTime.Now;
            _workDayCalendar.PublicHolidays = new List<DateTime> { };
            _workDayCalendar.RecurringHolidays = new List<DateTime> { };
        }
        [TestMethod]
        public void InHourInMinute()
        {
            //Arrange         
            _workDayCalendar.selectedDateTime = new DateTime(2024, 5, 13, 08, 10, 0);            
            var result1 = new DateTime(2024, 5, 14, 12, 10, 0);
           
            //Act
            var result = _workDayCalendar.getResultDatetime(workingDays);
            
            //Assert
            Assert.AreEqual(result, result1);
        }

        [TestMethod]
        public void ExceedsByHour()
        {
           //Arrange
            _workDayCalendar.selectedDateTime = new DateTime(2024,5,13, 16, 00, 0);
            var result1 = new DateTime(2024,5,15, 12, 0, 0);

            //Act
            var result = _workDayCalendar.getResultDatetime(workingDays);

            //Assert
            Assert.AreEqual(result, result1);
        }

        [TestMethod]
        public void ExceedsByMinutes()
        {
            //Arrange
            workingDays = 1.25;
            var result1 = new DateTime(2024,5,15,09,07, 0);
            _workDayCalendar.selectedDateTime = new DateTime(2024,5,13, 15, 07, 0);

            //Act
            var result = _workDayCalendar.getResultDatetime(workingDays);

            //Assert
            Assert.AreEqual(result, result1);
        }

        [TestMethod]
        public void ExceedsByMinutes1()
        {
            //Arrange
            workingDays = 2;
            var result1 = new DateTime(2024, 5, 20, 08, 00, 0);
            _workDayCalendar.selectedDateTime = new DateTime(2024, 5, 15, 21, 05, 0);

            //Act
            var result = _workDayCalendar.getResultDatetime(workingDays);

            //Assert
            Assert.AreEqual(result, result1);
        }
       
    }
}

