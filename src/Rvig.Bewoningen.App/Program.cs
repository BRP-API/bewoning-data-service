using Rvig.Base.App;
using System.Collections.Generic;
using System;
using Rvig.HaalCentraalApi.Bewoningen.Services;
using Rvig.Data.Bewoningen.Mappers;
using Rvig.Data.Bewoningen.Repositories;
using Rvig.Data.Bewoningen.Services;
using Rvig.HaalCentraalApi.Bewoningen.Interfaces;
using Rvig.HaalCentraalApi.Bewoning.Validation.RequestModelValidators;
using Rvig.HaalCentraalApi.Bewoningen.Validation.RequestModelValidators;
using Microsoft.AspNetCore.Builder;

var servicesDictionary = new Dictionary<Type, Type>
{
	// Data
	{ typeof(IRvigBewoningenRepo), typeof(RvigBewoningenRepo) },
	{ typeof(IRvigBewoningenBewonerRepo), typeof(RvigBewoningenBewonerRepo) },
	{ typeof(IRvIGDataBewoningenMapper), typeof(RvIGDataBewoningenMapper) },
	{ typeof(IGetAndMapGbaBewoningenService), typeof(GetAndMapGbaBewoningenService) },

	// API
	{ typeof(IGbaBewoningenApiService), typeof(GbaBewoningenApiService) }
};

var validatorList = new List<Type>
{
	typeof(BewoningMetPeildatumValidator),
	typeof(BewoningMetPeriodeValidator)
};

// This is used to give configurable options to deactive the authorization layer. This was determined by the Haal Centraal crew and the RvIG to be required.
// The reason for this requirement has to do with Kubernetes and a multi pod setup where another pod is responsible for authorizations therefore making this a sessionless API.
static bool UseAuthorizationLayer(WebApplicationBuilder builder)
{
	// For now Bewoningen does not support this and until it does we will enforce this API to always have the authorization layer active.
	_ = bool.TryParse(builder.Configuration["ProtocolleringAuthorization:UseAuthorizationChecks"], out bool useAuthorizationChecks);

	return useAuthorizationChecks;
}

RvigBaseApp.Init(servicesDictionary, validatorList, UseAuthorizationLayer, "BRP Bewoning API");