using System;

namespace QOnT.classes
{
  public static class DateTimeExtensions
  {
    public static DateTime GetFirstDayOfWeek(this DateTime sourceDateTime)
    {
      var daysAhead = (DayOfWeek.Sunday - (int)sourceDateTime.DayOfWeek);

      sourceDateTime = sourceDateTime.AddDays((int)daysAhead);

      return sourceDateTime;
    }

    public static DateTime GetLastDayOfWeek(this DateTime sourceDateTime)
    {
      var daysAhead = DayOfWeek.Saturday - (int)sourceDateTime.DayOfWeek;

      sourceDateTime = sourceDateTime.AddDays((int)daysAhead);

      return sourceDateTime;
    }
  }
}