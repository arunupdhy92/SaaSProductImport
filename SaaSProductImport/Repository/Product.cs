using System.ComponentModel.DataAnnotations;

namespace SaaSProductImport.Repository
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Categories { get; set; }
        public string Name { get; set; }
        public string Twitter { get; set; }
        public string Source { get; set; }
        public string CreatedAt { get; set; }
    }
}
