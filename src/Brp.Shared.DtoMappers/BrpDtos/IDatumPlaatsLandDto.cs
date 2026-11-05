namespace Brp.Shared.DtoMappers.BrpDtos;

public interface IDatumPlaatsLandDto
{
    string Datum { get; set; }
    CommonDtos.Waardetabel Land { get; set; }
    CommonDtos.Waardetabel Plaats { get; set; }
}
