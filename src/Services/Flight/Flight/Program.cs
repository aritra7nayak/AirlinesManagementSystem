using Flight.Business;
using Flight.Infrastructure;
using Flight.MessageService;
using Flight.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var rabbitMQSection = builder.Configuration.GetSection("RabbitMQ");
builder.Services.Configure<RabbitMQOptions>(rabbitMQSection);
var connectionString = builder.Configuration.GetConnectionString("FlightConnectionString");
builder.Services.AddDbContext<FlightContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddControllers();
builder.Services.AddScoped<AeroplaneService>();
builder.Services.AddScoped<AeroplaneRepository>();
builder.Services.AddScoped<RabbitMQService>();

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
