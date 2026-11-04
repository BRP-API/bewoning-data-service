using Brp.Shared.DtoMappers.BrpApiDtos;

namespace Brp.Shared.DtoMappers.Interfaces;

public interface IAdresregelsInOnderzoek
{
    bool? Adresregel1 { get; set; }
    bool? Adresregel2 { get; set; }
    bool? Adresregel3 { get; set; }
    bool? Land { get; set; }
    AbstractDatum DatumIngangOnderzoekVerblijfplaats { get; set; }
}
