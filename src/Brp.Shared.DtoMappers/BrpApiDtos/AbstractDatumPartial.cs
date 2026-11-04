namespace Brp.Shared.DtoMappers.BrpApiDtos;

public partial class AbstractDatum
{
    private static DateTime? ToDateTime(AbstractDatum datum)
    {
        return datum switch
        {
            VolledigeDatum v => v.Datum!.Value.DateTime,
            JaarDatum v => new DateTime(v.Jaar!.Value, 1, 1, 0, 0, 0, DateTimeKind.Local).AddYears(1),
            JaarMaandDatum v => new DateTime(v.Jaar!.Value, v.Maand!.Value, 1, 0, 0, 0, DateTimeKind.Local).AddMonths(1),
            _ => null
        };
    }

    public static bool operator <=(AbstractDatum d, DateTime e)
    {
        var peildatum = ToDateTime(d);
        return peildatum != null && peildatum <= e;
    }

    public static bool operator >=(AbstractDatum d, DateTime e)
    {
        var peildatum = ToDateTime(d);
        return peildatum != null && peildatum >= e;
    }

    private static (int year, int month, int day) ToYearMonthDay(AbstractDatum datum)
    {
        return datum switch
        {
            
            VolledigeDatum { Datum: {} v} => (v.Year, v.Month, v.Day), 
            JaarMaandDatum { Jaar: {} j, Maand: {} m} => (j, m, 0),
            JaarDatum { Jaar: {} j} => (j, 0, 0), 
            _ => (0, 0, 0),
        };
    }

    public static bool operator <(AbstractDatum left, AbstractDatum right)
    {
        (var leftYear, var leftMonth, var leftDay) = ToYearMonthDay(left);
        (var rightYear, var rightMonth, var rightDay) = ToYearMonthDay(right);

        return leftYear < rightYear ||
            (leftYear == rightYear && leftMonth < rightMonth) ||
            (leftYear == rightYear && leftMonth == rightMonth && leftDay < rightDay);
    }

    public static bool operator >(AbstractDatum left, AbstractDatum right)
    {
        (var leftYear, var leftMonth, var leftDay) = ToYearMonthDay(left);
        (var rightYear, var rightMonth, var rightDay) = ToYearMonthDay(right);

        return leftYear > rightYear ||
            (leftYear == rightYear && leftMonth > rightMonth) ||
            (leftYear == rightYear && leftMonth == rightMonth && leftDay > rightDay);
    }
}
