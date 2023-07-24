namespace WebApp.Services
{
    public class DateService
    {
        public static int GetCurrentAcademicYear()
        {
            var dateNow = DateTime.Now;

            var currentYear = dateNow.Year;
            var currentMonth = dateNow.Month;

            if (currentMonth > 8)
            {
                return currentYear;
            }
            else
            {
                return currentYear - 1;
            }
        }
    }
}
