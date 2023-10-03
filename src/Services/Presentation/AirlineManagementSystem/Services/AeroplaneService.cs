using AirlineManagementSystem.DTOs;
using System.Text.Json;
using System.Net.Http.Json;

namespace AirlineManagementSystem.Services
{
    public class AeroplaneService : IAeroplaneService
    {
        public readonly HttpClient _httpClient;

        public AeroplaneService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<AeroplaneDto>> GetAeroplanes()
        {
            try
            {


                var response = await _httpClient.GetAsync("/api/aeroplanes");

                if (response.IsSuccessStatusCode)
                {
                    // Ensure that the response content is treated as JSON
                    var aeroplanes = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<IEnumerable<AeroplaneDto>>(aeroplanes);
                }
                else
                {
                    // Handle the error here, such as logging or throwing an exception.
                    // You can also return an empty list or null as per your error handling strategy.
                    return new List<AeroplaneDto>();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
