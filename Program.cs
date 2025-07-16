using CryptoInfoApi.Data;
using CryptoInfoApi.Repositories;
using CryptoInfoApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, true);
});

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddHttpClient<ICoindeskService, CoindeskService>();

var supportedCultures = builder.Configuration.GetSection("SupportedCultures").Get<string[]>() ?? new[] { "zh-TW", "en-US" };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var cultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
    options.DefaultRequestCulture = new RequestCulture(cultures[0]);
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// API Request/Response Logging
app.UseMiddleware<CryptoInfoApi.Middlewares.ApiLoggingMiddleware>();
app.UseRequestLocalization();
app.UseAuthorization();

app.MapControllers();

app.Run();
