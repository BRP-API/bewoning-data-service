namespace Bewoning.Data.Authorisation;

[AttributeUsage(AttributeTargets.Property)]
public class RubriekElementAttribute : Attribute
{
    public string ElementNummers { get; set; }

    public RubriekElementAttribute(string nummer)
    {
        ElementNummers = nummer;
    }
}
