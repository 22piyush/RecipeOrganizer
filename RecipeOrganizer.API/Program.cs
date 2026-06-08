using RecipeOrganizer.Domain.Services;
using RecipeOrganizer.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add controller support
builder.Services.AddControllers();

// Add Swagger services -> Swagger helps to test APIs from browser UI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register basic services (Infrastructure extension missing)
// If AddInfrastructure extension method is available in your Infrastructure project,
// restore the using/assembly reference and replace the lines below with the call.
// Minimal registrations to allow the project to compile until the extension is available.
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IHealthService, HealthService>();
builder.Services.AddScoped<IAuthService, AuthService>();


// Build application
var app = builder.Build();

// Enable Swagger only in Development mode
// URL: https://localhost:xxxx/swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Enable Authentication middleware
// Checks JWT token before accessing secure APIs
app.UseAuthentication();

// Enable Authorization middleware
// Checks user roles/permissions
app.UseAuthorization();

// Map controller routes
// Connects controller APIs to application
app.MapControllers();

// Run application
app.Run();