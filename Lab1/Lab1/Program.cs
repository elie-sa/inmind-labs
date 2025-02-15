using Lab1;
using Lab1.Filters;
using Lab1.Middleware;
using Lab1.Services.User;
using Lab1.Services.Date;
using Lab1.Services.Library;
using Lab1.Services.ObjectMapper;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IUserService, UserService>();

// Adding the ObjectMapperService which was used for the Reflection & Generic Type exercise
builder.Services.AddScoped<IObjectMapperService, ObjectMapperService>();

// Adding the LibraryService which was used for the LINQ exercises
builder.Services.AddScoped<ILibraryService, LibraryService>();

// Logging middleware: singleton used to keep the logger lifetime throughout the whole application running
builder.Services.AddScoped<IDateService, DateService>();

// Error Handling Middleware injected using the custom ExceptionHandlingMiddleware implementing the IExceptionHandler
builder.Services.AddExceptionHandler<ExceptionHandlingMiddleware>();

builder.Services.AddSingleton<RequestLoggingMiddleware>();
builder.Services.AddControllers(options =>
    {
        options.Filters.Add<LoggingActionFilter>();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Added the middleware "ExceptionHandlingMiddleware" to the pipeline
// I am throwing the exceptions using the services (UserService & DataService) and catching them in the ExceptionHandlingMiddleware 
// and returning the appropriate status code and message (sent using exception.Message via the pipeline)
// was catching the exceptions in the controller in lab 1 instead
app.UseExceptionHandler( _ => { });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<RequestLoggingMiddleware>();

app.Run();