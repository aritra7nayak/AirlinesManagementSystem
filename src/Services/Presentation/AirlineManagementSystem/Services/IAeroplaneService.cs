using AirlineManagementSystem.DTOs;

namespace AirlineManagementSystem.Services
{
    public interface IAeroplaneService
    {
        Task<IEnumerable<AeroplaneDto>> GetAeroplanes();
    }
}
