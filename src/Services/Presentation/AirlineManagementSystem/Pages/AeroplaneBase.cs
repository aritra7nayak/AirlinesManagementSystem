using AirlineManagementSystem.DTOs;
using AirlineManagementSystem.Services;
using Microsoft.AspNetCore.Components;

namespace AirlineManagementSystem.Pages
{
    public class AeroplaneBase : ComponentBase
    {
        [Inject]
        public IAeroplaneService AeroplaneService { get; set; }

        protected List<AeroplaneDto> Aeroplanes { get; set; } = new List<AeroplaneDto>();

        protected override async Task OnInitializedAsync()
        {
            Aeroplanes = (await AeroplaneService.GetAeroplanes()).ToList();
        }

    }
}
