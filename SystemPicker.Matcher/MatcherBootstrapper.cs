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

        public static void BootstrapMatcherData(NamedSectorStorage sectorStorage, NamedSystemStorage systemStorage)
        {
            // Not made async, as it'll be called in Startup.Configure, which is a sync method.
            NamedSystemFinder.SetSystems(systemStorage.GetAllSystems().GetAwaiter().GetResult());
            NamedSectorFinder.SetSectors(sectorStorage.GetAllSectors().GetAwaiter().GetResult());
        }
        
        public static async Task BootstrapMatcherDataAsync(NamedSectorStorage sectorStorage, NamedSystemStorage systemStorage)
        {
            NamedSystemFinder.SetSystems(await systemStorage.GetAllSystems());
            NamedSectorFinder.SetSectors(await sectorStorage.GetAllSectors());
        }
    }
}