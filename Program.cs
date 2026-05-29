using Microsoft.EntityFrameworkCore;
using AhfHivApi.Data;
using AhfHivApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddEndpointsApiExplorer();

// SQLite database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=ahf_hiv.db"));

// Register services
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IHivStatsService, HivStatsService>();
builder.Services.AddScoped<IClimateService, ClimateService>();
builder.Services.AddScoped<IProgrammeService, ProgrammeService>();

// CORS — allow Vue dashboard to call this API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVue", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",
            "https://vue-ahf-portfolio.netlify.app"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Seed database on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    await DataSeeder.SeedAsync(context);
}

app.UseCors("AllowVue");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();