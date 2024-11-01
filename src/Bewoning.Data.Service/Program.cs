using Rvig.Base.App;
using System.Collections.Generic;
using System;
using Rvig.BrpApi.Bewoningen.Services;
using Rvig.Data.Bewoningen.Mappers;
using Rvig.Data.Bewoningen.Repositories;
using Rvig.Data.Bewoningen.Services;
using Rvig.BrpApi.Bewoningen.Interfaces;
using Rvig.BrpApi.Bewoningen.Validation.RequestModelValidators;
using Microsoft.AspNetCore.Builder;

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