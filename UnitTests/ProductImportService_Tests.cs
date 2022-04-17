using SaaSProductImport.Service;
using Xunit;

namespace UnitTests
{
    public class ProductImportService_Tests
    {
        private readonly ProductImportService _productImportService;

        public ProductImportService_Tests()
        {
            _productImportService = new ProductImportService();
        }

        [Fact]
        public void ImportProduct_HappyPath()
        {
            //arrange
            string command = "import capterra feed-products/capterra.yaml";

            //act
            var result = _productImportService.ImportProduct(command);

            //assert
            Assert.True(result.Result);
        }

        [Theory]
        [InlineData("dummy feed-products/capterra.yaml")]
        [InlineData(" ")]
        public void ImportProduct_InvalidOutcome(string command)
        {
            //act
            var result = _productImportService.ImportProduct(command);

            //assert
            Assert.False(result.Result);
        }
    }
}
