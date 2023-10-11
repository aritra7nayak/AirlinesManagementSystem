using AirlineManagementSystem.DTOs;
using AirlineManagementSystem.Services;
using Microsoft.AspNetCore.Components;


namespace AirlineManagementSystem.Pages
{
    public class UserLoginBase : ComponentBase
    {
        [Inject]
        public UserRegistrationLogin userRegistrationLogin { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected LoginViewModel loginModel = new LoginViewModel();


        protected async Task Login()
        {
            var response = await userRegistrationLogin.LoginAsync(loginModel);

            if (response.IsSuccessStatusCode)
            {
                // Handle success, e.g., navigate to a success page
                NavigationManager.NavigateTo("/", forceLoad: false);
            }
            else
            {
                // Handle error, e.g., display error messages to the user
            }
        }
    }
}
