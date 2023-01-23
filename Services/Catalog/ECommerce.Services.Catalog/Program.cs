using ECommerce.Services.Catalog.DesignPatterns;
using ECommerce.Services.Catalog.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.Configure<OptionsPattern>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseOptions>(sp =>
{
    return sp.GetRequiredService<IOptions<OptionsPattern>>().Value;
});

builder.Services.AddAutoMapper(typeof(StartupBase));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
