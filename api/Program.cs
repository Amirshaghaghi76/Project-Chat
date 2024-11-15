var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddRepositoryService();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();