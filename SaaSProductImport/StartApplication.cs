using SaaSProductImport.Helper;
using SaaSProductImport.Interface;
using System;
using System.Threading.Tasks;

namespace SaaSProductImport
{
    class StartApplication
    {
        private readonly IProductImportService _productImportService;

        public StartApplication(IProductImportService productImportService)
        {
            _productImportService = productImportService;
        }

        public async Task Start(string[] args)
        {
            try
            {
                Console.WriteLine("Enter command to import product file:");
                var command = Console.ReadLine();

                var isValidCommand = SaasProductHelper.IsValidCommand(command);

                if(isValidCommand)
                {
                    await _productImportService.ImportProduct(command);
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Bad Request!! Please try again.");
            }
        }
    }
}
