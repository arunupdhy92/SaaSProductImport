using System.Threading.Tasks;

namespace SaaSProductImport.Interface
{
    public interface IProductImportService
    {
        Task<bool> ImportProduct(string command);
    }
}
