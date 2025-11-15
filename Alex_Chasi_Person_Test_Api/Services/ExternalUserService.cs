using Alex_Chasi_Person_Test_Api.Services.Interfaces;
using System.Text.Json;

namespace Alex_Chasi_Person_Test_Api.Services
{
    public class ExternalUserService : IExternalUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalUserService> _logger;

        public ExternalUserService(HttpClient httpClient, ILogger<ExternalUserService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<object> GetExternalUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://gorest.co.in/public/v2/users");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Error al obtener usuarios externos. Status: {response.StatusCode}");
                    throw new HttpRequestException($"Error al obtener datos del servicio externo: {response.StatusCode}");
                }

                var content = await response.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<object>(content);

                return users ?? new List<object>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al servicio externo");
                throw new Exception("No se pudo conectar con el servicio externo. Por favor, intente más tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener usuarios externos");
                throw new Exception("Ocurrió un error al procesar la solicitud.", ex);
            }
        }
    }
}