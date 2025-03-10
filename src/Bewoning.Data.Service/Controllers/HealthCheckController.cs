using Bewoning.Data.Service.ResponseModels.HealthCheck;
using Bewoning.Data.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bewoning.Data.Service.Controllers;

[AllowAnonymous, ApiController, Route("haalcentraal/api/health")]
public class HealthCheckController : ControllerBase
{
    private readonly IHealthCheckApiService _healthCheckApiService;

    public HealthCheckController(IHealthCheckApiService healthCheckApiService)
    {
        _healthCheckApiService = healthCheckApiService;
    }

    [HttpGet]
    [Route("check")]
    public Task<HealthCheckResult> Check()
    {
        return _healthCheckApiService.CheckConnections();
    }
}
