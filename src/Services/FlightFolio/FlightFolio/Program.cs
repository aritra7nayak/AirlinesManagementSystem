using FlightFolio.Business;
using FlightFolio.Infrastructure;
using FlightFolio.MessageService;
using FlightFolio.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var rabbitMQSection = builder.Configuration.GetSection("RabbitMQ");
builder.Services.Configure<RabbitMQOptions>(rabbitMQSection);
var connectionString = builder.Configuration.GetConnectionString("FlightConnectionString");
builder.Services.AddDbContext<FlightFolioContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// In Startup.cs
builder.Services.AddSingleton<RabbitMQConsumerHostedServiceFactory>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<RabbitMQConsumerHostedServiceFactory>().Create());
builder.Services.AddScoped<FlightFolio.MessageService.RabbitMQReceiveMessageService>();
builder.Services.AddScoped<RabbitMQConsumerHostedService>();



builder.Services.AddControllers();
builder.Services.AddScoped<AeroplaneService>();
builder.Services.AddScoped<AeroplaneRepository>();
//builder.Services.AddSingleton<RabbitMQReceiveMessageService>();
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
