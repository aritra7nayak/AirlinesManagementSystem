using AirlineManagementSystem.DTOs;
using System.Text.Json;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace AirlineManagementSystem.Services
{
    public class AeroplaneService : IAeroplaneService
    {
        private readonly ILogger<AeroplaneService> _logger;
        public readonly HttpClient _httpClient;

        public AeroplaneService(HttpClient httpClient, ILogger<AeroplaneService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<AeroplaneDto>> GetAeroplanes()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/aeroplanes");

                if (response.IsSuccessStatusCode)
                {
                    var aeroplanesJson = await response.Content.ReadAsStringAsync();

                    // Use Newtonsoft.Json for deserialization
                    var aeroplanes = JsonConvert.DeserializeObject<IEnumerable<AeroplaneDto>>(aeroplanesJson);

                    return aeroplanes;
                }
                else
                {
                    // Log the error details
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Failed to retrieve data: {response.StatusCode} - {errorContent}");
                    return new List<AeroplaneDto>();
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the HTTP request exception
                _logger.LogError($"HTTP request failed: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Log other exceptions
                _logger.LogError($"An error occurred: {ex.Message}");
                throw;
            }
        }

    }
}
