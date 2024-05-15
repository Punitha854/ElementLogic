namespace WorkdayCalendar.Test
{
    [TestClass]
    public class HolidaysTest
    {
        WorkingDayCalendar _workDayCalendar = new WorkingDayCalendar();
        double workingDays = 3;


        [TestInitialize]
        public void Initialize()
        {
            _workDayCalendar.selectedDateTime = DateTime.Now;
            _workDayCalendar.PublicHolidays = new List<DateTime> { new DateTime(2024, 5, 15),new DateTime(2024,05,31) };
            _workDayCalendar.addRecurringDays(new DateTime(2023, DateTime.Now.Month, 13));
            _workDayCalendar.addRecurringDays(new DateTime(1970, DateTime.Now.Month, 10));
        }
        [TestMethod]
        public void StartDateInWeekend()
        {
            //Arrange            
            _workDayCalendar.selectedDateTime = new DateTime(2024, 5, 11,21,0,0);
            var result1 = new DateTime(2024, 5, 20, 8, 0, 0);

            //ACT
            var result = _workDayCalendar.getResultDatetime(workingDays);

            //Assert
            Assert.AreEqual(result, result1);
        }

        [TestMethod]
        public void MiddleDateInWeekend()
        {
            //Arrange            
            _workDayCalendar.selectedDateTime = new DateTime(2024, 5, 10,15,0,0);
            var result1 = new DateTime(2024, 5, 17, 15, 0, 0);

            //ACT
            var result = _workDayCalendar.getResultDatetime(workingDays);

            //Assert
            Assert.AreEqual(result, result1);
        }

        [TestMethod]
        public void DatesInrecurringandPublicHolidays()
        {
            //Arrange            
            _workDayCalendar.selectedDateTime = new DateTime(2024, 5, 30);
            var result1 = new DateTime(2024, 6, 5, 8, 0, 0);

            //ACT
            var result = _workDayCalendar.getResultDatetime(workingDays);

            //Assert
            Assert.AreEqual(result, result1);
        }

        [TestMethod]
        public void DatesInrecurringandPublicHolidaysNegatives()
        {
            //Arrange            
            _workDayCalendar.selectedDateTime = new DateTime(2004, 5, 24,18,05,00);
            workingDays = -5.5;
            _workDayCalendar.PublicHolidays = new List<DateTime> { new DateTime(2004, 5, 27) };
            _workDayCalendar.RecurringHolidays = new List<DateTime> { new DateTime(2004, 05, 17) };
            var result1 = new DateTime(2004, 05, 14, 12, 00, 0);

            //ACT
            var result = _workDayCalendar.getResultDatetime(workingDays);

            //Assert
            Assert.AreEqual(result, result1);
        }

        [TestMethod]
        public void DatesInrecurringandPublicHolidaysStrange()
        {
            //Arrange            
            _workDayCalendar.selectedDateTime = new DateTime(2004, 5, 24, 08, 03, 00);           
            workingDays = 12.782709;
            _workDayCalendar.PublicHolidays = new List<DateTime> { new DateTime(2004, 5, 27) };
            _workDayCalendar.RecurringHolidays = new List<DateTime> { new DateTime(2004, 05, 17)};
            
            var result1 = new DateTime(2004, 06, 10, 14, 18, 0);

            //ACT
            var result = _workDayCalendar.getResultDatetime(workingDays);

            //Assert
            Assert.AreEqual(result, result1);
        }

        [TestMethod]
        public void DatesInrecurringandPublicHolidaysPDFEample1()
        {
            //Arrange            
            _workDayCalendar.selectedDateTime = new DateTime(2004, 5, 24, 19, 03, 00);            workingDays = 44.723656;
            workingDays = 44.723656;
            _workDayCalendar.PublicHolidays = new List<DateTime> { new DateTime(2004, 5, 27) };
            _workDayCalendar.RecurringHolidays = new List<DateTime> { new DateTime(2004, 05, 17) };
            var result1 = new DateTime(2004, 07, 27, 13, 47, 0);
           

            //ACT
            var result = _workDayCalendar.getResultDatetime(workingDays);

            //Assert
            Assert.AreEqual(result, result1);
        }
        [TestMethod]
        public void DatesInrecurringandPublicHolidaysStrange1()
        {
            //Arrange            
            _workDayCalendar.selectedDateTime = new DateTime(2004, 5, 24, 07, 03, 00);
            //workingDays = 44.723656;
            workingDays = 8.276628;
            _workDayCalendar.PublicHolidays = new List<DateTime> { new DateTime(2004, 5, 27) };
            _workDayCalendar.RecurringHolidays = new List<DateTime> { new DateTime(2004, 05, 17) };
            //var result1 = new DateTime(2004, 07, 27, 13, 47, 0);
            var result1 = new DateTime(2004, 06, 04, 10, 12, 0);

            //ACT
            var result = _workDayCalendar.getResultDatetime(workingDays);

            //Assert
            Assert.AreEqual(result, result1);
        }

        [TestMethod]
        public void DatesInrecurringandPublicHolidaysNegatives1()
        {
            //Arrange            
            _workDayCalendar.selectedDateTime = new DateTime(2004, 5, 24, 18, 03, 00);            
             workingDays = -6.7470217;
            _workDayCalendar.PublicHolidays = new List<DateTime> { new DateTime(2004, 5, 27) };
            _workDayCalendar.RecurringHolidays = new List<DateTime> { new DateTime(2004, 05, 17) };
            
            var result1 = new DateTime(2004, 05, 13, 10, 02, 0);

            //ACT
            var result = _workDayCalendar.getResultDatetime(workingDays);

            //Assert
            Assert.AreEqual(result, result1);
        }

    }
}
