using Lab1;
using Lab1.Services.User;
using Lab1.Services.Date;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// I have chosen the service to be scoped since _users is a static list and singleton would affect all requests
// Transient can be inefficient because it creates too many objects
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDateService, DateService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();