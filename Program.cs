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

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

await app.UseOcelot();

app.Run();

