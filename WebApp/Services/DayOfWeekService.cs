using static WebApp.Types.Enums;

namespace WebApp.Services
{
    public class DayOfWeekService
    {
        public static DayOfWeekRus ConvertToDayOfWeekRus(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday: return DayOfWeekRus.Monday;
                case DayOfWeek.Tuesday: return DayOfWeekRus.Tuesday;
                case DayOfWeek.Wednesday: return DayOfWeekRus.Wednesday;
                case DayOfWeek.Thursday: return DayOfWeekRus.Thursday;
                case DayOfWeek.Friday: return DayOfWeekRus.Friday;
                case DayOfWeek.Saturday: return DayOfWeekRus.Saturday;
                case DayOfWeek.Sunday: return DayOfWeekRus.Sunday;
                default: throw new ArgumentException("Invalid day of week");
            }
        }
        public static DayOfWeek ConvertToDayOfWeek(DayOfWeekRus dayOfWeekRus)
        {
            throw new NullReferenceException();
        }

        public static string GetMonthRus(int month)
        {
            switch (month)
            {
                case 1:  return "Январь";
                case 2: return "Февраль";
                case 3: return "Март";
                case 4: return "Апрель";
                case 5: return "Май";
                case 6: return "Июнь";
                case 7: return "Июль";
                case 8: return "Август";
                case 9: return "Сентябрь";
                case 10: return "Октябрь";
                case 11: return "Ноябрь";
                case 12: return "Декабрь";
                default: return "";
            }
        }
    }
}
