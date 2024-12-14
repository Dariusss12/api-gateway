using api_gateway.Src.DTOs;
using UsersServiceProto;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcClient<UserService.UserServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:5001"); // URL de tu servidor gRPC
});


builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();

