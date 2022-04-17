using Newtonsoft.Json;
using SaaSProductImport.Helper;
using SaaSProductImport.Interface;
using SaaSProductImport.Model;
using SaaSProductImport.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace SaaSProductImport.Service
{
    public class ProductImportService:IProductImportService
    {
        public readonly string Capterra = "capterra/";
        public readonly string SoftwareAdvice = "softwareadvice/";

        /// <summary>
        /// Import product from corresponding source
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<bool> ImportProduct(string command)
        {
            try
            {
                var filePath = command.Replace("import ", "Mock/").Replace(" ", "/");

                var isAllowedFile = SaasProductHelper.RequestedFileValidation(filePath);

                if(isAllowedFile)
                {
                    if(filePath.Contains(Capterra))
                    {
                        await FetchCapterraProducts(filePath);
                        return true;
                    }
                    else if(filePath.Contains(SoftwareAdvice))
                    {
                        await FetchSoftwareAdviceProducts(filePath);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Requested source not available currently!!");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Requested file type not allowed!!");
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Fetch product from Capterra Source and Assign to object
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private async Task FetchCapterraProducts(string filePath)
        {
            if(File.Exists(filePath))
            {
                List<CapterraProductDetails> productDetails = new List<CapterraProductDetails>();
                List<Product> products = new List<Product>();
                using(StreamReader data=new StreamReader(filePath))
                {
                    var deserializer = new DeserializerBuilder().Build();
                    var yamlObject = deserializer.Deserialize(data);
                    var serializer = new SerializerBuilder()
                        .JsonCompatible()
                        .Build();
                    var json = serializer.Serialize(yamlObject);
                    productDetails = JsonConvert.DeserializeObject<List<CapterraProductDetails>>(json);
                }

                foreach(var product in productDetails)
                {
                    products.Add(new Product()
                    {
                        Categories = product.Tags,
                        Name = product.Name,
                        Twitter = product.Twitter ?? "NA",
                        Source = "capterra",
                        CreatedAt = DateTime.Now.ToString()
                    });
                    Console.WriteLine($"Importing: Name: {product.Name}; Categories: {product.Tags}; Twitter: {product.Twitter ?? "NA"}");
                }
            }
            else
            {
                Console.WriteLine("Requested file not available!!");
            }
        }

        /// <summary>
        /// Fetch product from SoftwareAdvice Source and Assign to object
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private async Task FetchSoftwareAdviceProducts(string filePath)
        {
            if (File.Exists(filePath))
            {
                SAProduct productDetails = new SAProduct();
                List<Product> products = new List<Product>();
                using (StreamReader data = new StreamReader(filePath))
                {
                    var json = await data.ReadToEndAsync();
                    productDetails = JsonConvert.DeserializeObject<SAProduct>(json);
                }

                foreach (var product in productDetails.products)
                {
                    products.Add(new Product()
                    {
                        Categories = string.Join(",", product.Categories),
                        Name = product.Title,
                        Twitter = product.Twitter ?? "NA",
                        Source = "softwareadvice",
                        CreatedAt = DateTime.Now.ToString()
                    });
                    Console.WriteLine($"Importing: Name: {product.Title}; Categories: {string.Join(",", product.Categories)}; Twitter: {product.Twitter ?? "NA"}");
                }
            }
            else
            {
                Console.WriteLine("Requested file not available!!");
            }
        }
    }
}
