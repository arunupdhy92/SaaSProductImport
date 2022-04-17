using Microsoft.Extensions.DependencyInjection;
using SaaSProductImport.Interface;
using SaaSProductImport.Service;
using System.Threading.Tasks;

namespace SaaSProductImport
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = ConfigureServices(new ServiceCollection());
            
            var serviceProvider = services.BuildServiceProvider();

            //trigger actual code
            await serviceProvider.GetService<StartApplication>().Start(args);
        }

        /// <summary>
        /// Configure services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProductImportService, ProductImportService>();
            services.AddTransient<StartApplication>();
            return services;
        }
    }
}
