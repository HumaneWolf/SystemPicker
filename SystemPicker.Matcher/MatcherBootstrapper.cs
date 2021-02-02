using System;
using System.Threading.Tasks;
using SystemPicker.Matcher.Finders;
using SystemPicker.Matcher.Storage;
using SystemPicker.Matcher.SystemApis;
using Microsoft.Extensions.DependencyInjection;

namespace SystemPicker.Matcher
{
    public static class MatcherBootstrapper
    {
        public static IServiceCollection AddSystemMatcher(this IServiceCollection services)
        {
            services.AddTransient<ISystemApi, RandomSystemApi>();
            services.AddTransient<TextMatcher>();

            services.AddTransient<NamedSystemStorage>();
            services.AddTransient<NamedSectorStorage>();

            return services;
        }

        public static IServiceProvider LoadMatcherData(this IServiceProvider serviceProvider)
        {
            var sectorStorage = serviceProvider.GetRequiredService<NamedSectorStorage>();
            var systemStorage = serviceProvider.GetRequiredService<NamedSystemStorage>();
            // Not made async, as it'll be called in Startup.Configure, which is a sync method.
            NamedSystemFinder.SetSystems(systemStorage.GetAllSystems().GetAwaiter().GetResult());
            NamedSectorFinder.SetSectors(sectorStorage.GetAllSectors().GetAwaiter().GetResult());

            return serviceProvider;
        }
        
        public static async Task<IServiceProvider> LoadMatcherDataAsync(this IServiceProvider serviceProvider)
        {
            var sectorStorage = serviceProvider.GetRequiredService<NamedSectorStorage>();
            var systemStorage = serviceProvider.GetRequiredService<NamedSystemStorage>();
            NamedSystemFinder.SetSystems(await systemStorage.GetAllSystems());
            NamedSectorFinder.SetSectors(await sectorStorage.GetAllSectors());

            return serviceProvider;
        }
    }
}