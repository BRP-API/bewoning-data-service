using Brp.Shared.DtoMappers.CommonDtos;

namespace Brp.Shared.DtoMappers.Interfaces;

public interface IAdressering
{
    string Adresregel1 { get; set; }
    string Adresregel2 { get; set; }
    string Adresregel3 { get; set; }
    Waardetabel Land { get; set; }
}
