//using AirlineManagementSystem.Data;
using AirlineManagementSystem.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
   .AddCookie(options =>
   {
       options.LoginPath = "/login"; // Specify your login page
       options.AccessDeniedPath = "/accessdenied"; // Specify the access denied page
   });



// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient<IAeroplaneService,AeroplaneService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5000/");
    // Additional configuration if needed, such as headers or timeouts.
});

builder.Services.AddHttpClient<UserRegistrationLogin>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5295/");
});
//builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
