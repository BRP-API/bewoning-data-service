namespace Rvig.Data.Bewoningen.Providers;
public class DateTimeTodayProvider : ICurrentDateTimeProvider
{
    public DateTime Today()
    {
        return DateTime.Today;
    }
}