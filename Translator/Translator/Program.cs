using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Translator.Application.Handlers.CommandHandlers;
using Translator.Application.Services;
using Translator.Application.Services.Interfaces;
using Translator.Domain.Interfaces;
using Translator.Domain.Interfaces.Base;
using Translator.Infrastructure.Data;
using Translator.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Options;
using Translator.Application.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<TranslationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Translator.API", Version = "v1" });
});

// Register dependencies
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
builder.Services.AddTransient<ITranslationQueryRepository, TranslationQueryRepository>();
builder.Services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
builder.Services.AddTransient<ITranslationCommandRepository, TranslationCommandRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateTranslationHandler).GetTypeInfo().Assembly));
//builder.Services.AddScoped<ITranslationService, TranslationService>();
builder.Services.AddScoped<IServiceBusService, ServiceBusService>();

builder.Services.Configure<TranslationSettings>(builder.Configuration)
            .AddSingleton(sp => sp.GetRequiredService<IOptions<TranslationSettings>>().Value);

builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];
});

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
