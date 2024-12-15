using CareersProto;
using SubjectsProto;
using UsersServiceProto;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcClient<UserService.UserServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:5001"); // URL de tu servidor gRPC
});

builder.Services.AddGrpcClient<CareerService.CareerServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:5002"); // URL de tu servidor gRPC
});

builder.Services.AddGrpcClient<SubjectService.SubjectServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:5002"); // URL de tu servidor gRPC
});

builder.Services.AddControllers();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);


var app = builder.Build();


app.UseRouting();
app.MapControllers();

app.UseWhen(
    context =>
        context.Request.Path.StartsWithSegments("/api/auth") || 
        context.Request.Path.StartsWithSegments("/api/resources"), 
    subApp =>
    {
        subApp.UseOcelot().Wait();
    }
);

app.Run();

