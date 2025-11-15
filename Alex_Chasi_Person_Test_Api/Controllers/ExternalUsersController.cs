using Alex_Chasi_Person_Test_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alex_Chasi_Person_Test_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalUsersController : ControllerBase
    {
        private readonly IExternalUserService _externalUserService;
        private readonly ILogger<ExternalUsersController> _logger;

        public ExternalUsersController(IExternalUserService externalUserService, ILogger<ExternalUsersController> logger)
        {
            _externalUserService = externalUserService;
            _logger = logger;
        }

        // GET: api/externalusers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult> GetExternalUsers()
        {
            try
            {
                var users = await _externalUserService.GetExternalUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuarios externos");
                return StatusCode(503, new
                {
                    message = "Servicio externo no disponible",
                    error = ex.Message
                });
            }
        }
    }
}