using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Translator.Application.Services;
using Translator.Application.Services.Interfaces;
using Translator.Application.Settings;
using Translator.Domain.Interfaces;
using Translator.Infrastructure.Data;
using Translator.Infrastructure.Data.Repositories;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults();

host.ConfigureServices(services =>
{
    services.AddDbContext<TranslationContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings:DefaultConnection")));


    var serviceProvider = services.BuildServiceProvider();
    var config = serviceProvider.GetRequiredService<IConfiguration>();

    services.Configure<TranslationSettings>(config)
            .AddSingleton(sp => sp.GetRequiredService<IOptions<TranslationSettings>>().Value);
    services.AddScoped<ITranslationService, TranslationService>();
    services.AddTransient<ITranslationQueryRepository, TranslationQueryRepository>();
    services.AddTransient<ITranslationCommandRepository, TranslationCommandRepository>();
});

host.Build().Run();