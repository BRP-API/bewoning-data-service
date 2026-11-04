namespace Brp.Shared.DtoMappers.BrpApiDtos;

public interface IDatumPlaatsLandDto
{
    AbstractDatum Datum { get; set; }
    CommonDtos.Waardetabel Land { get; set; }

    CommonDtos.Waardetabel Plaats { get; set; }
}
