using AirlineManagementSystem.DTOs;
using AirlineManagementSystem.Services;
using Microsoft.AspNetCore.Components;

namespace AirlineManagementSystem.Pages
{
    public class AeroplaneBase : ComponentBase
    {
        [Inject]
        public IAeroplaneService AeroplaneService { get; set; }

        public IEnumerable<AeroplaneDto> Aeroplanes { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Aeroplanes = (await AeroplaneService.GetAeroplanes()).ToList();
        }

    }
}
