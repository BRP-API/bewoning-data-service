using Bewoning.Data.Service;
using Bewoning.Api.Services;
using Bewoning.Api.Interfaces;
using Bewoning.Data.Mappers;
using Bewoning.Data.Services;
using Bewoning.Data.Repositories.Bewoningen;

var servicesDictionary = new Dictionary<Type, Type>
{
	// Data
	{ typeof(IRvigBewoningenRepo), typeof(RvigBewoningenRepo) },
	{ typeof(IRvigBewoningenBewonerRepo), typeof(RvigBewoningenBewonerRepo) },
	{ typeof(IRvIGDataBewoningenMapper), typeof(RvIGDataBewoningenMapper) },
	{ typeof(IGetAndMapGbaBewoningenService), typeof(GetAndMapGbaBewoningenService) },

	// API
	{ typeof(IGbaBewoningenApiService), typeof(GbaBewoningenApiService) },
	{ typeof(IFilterService), typeof(FilterService) }
};

var validatorList = new List<Type>();

RvigBaseApp.Init(servicesDictionary, validatorList, "BRP Bewoning API");