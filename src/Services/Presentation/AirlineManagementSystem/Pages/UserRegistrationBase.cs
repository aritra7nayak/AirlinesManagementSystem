using AirlineManagementSystem.DTOs;
using AirlineManagementSystem.Services;
using Microsoft.AspNetCore.Components;

namespace AirlineManagementSystem.Pages
{
    public class UserRegistrationBase : ComponentBase
    {
        [Inject]
        public UserRegistrationLogin userRegistrationLogin { get; set; }

        protected RegisterViewModel registrationModel = new RegisterViewModel();


        protected async Task Register()
        {
            var response = await userRegistrationLogin.RegisterAsync(registrationModel);

            if (response.IsSuccessStatusCode)
            {
                // Handle success, e.g., navigate to a success page
            }
            else
            {
                // Handle error, e.g., display error messages to the user
            }
        }
    }
}
