using Bewoning.Data.Service;
using Bewoning.Api.Validation.RequestModelValidators;
using Bewoning.Api.Services;
using Bewoning.Api.Interfaces;
using Bewoning.Data.Repositories;
using Bewoning.Data.Mappers;
using Bewoning.Data.Services;

var servicesDictionary = new Dictionary<Type, Type>
{
	// Data
	{ typeof(IRvigBewoningenRepo), typeof(RvigBewoningenRepo) },
	{ typeof(IRvigBewoningenBewonerRepo), typeof(RvigBewoningenBewonerRepo) },
	{ typeof(IRvIGDataBewoningenMapper), typeof(RvIGDataBewoningenMapper) },
	{ typeof(IGetAndMapGbaBewoningenService), typeof(GetAndMapGbaBewoningenService) },

	// API
	{ typeof(IGbaBewoningenApiService), typeof(GbaBewoningenApiService) },
	{ typeof(IValidatieService), typeof(ValidatieService) },
	{ typeof(IFilterService), typeof(FilterService) }
};

var validatorList = new List<Type>
{
	typeof(BewoningMetPeildatumValidator),
	typeof(BewoningMetPeriodeValidator)
};

RvigBaseApp.Init(servicesDictionary, validatorList, "BRP Bewoning API");