namespace Bewoning.Data.Providers;
public class DateTimeTodayProvider : ICurrentDateTimeProvider
{
    public DateTime Today()
    {
        return DateTime.Today;
    }
}