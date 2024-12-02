using System;
using Xunit;
using ProductCatalog;

namespace ProductCatalog.Tests
{
    public class ProductCatalogAdminTests
    {
        [Fact]
        public void AddNewProduct_ShouldSuccessfullyAddProduct_WhenDataIsValid()
        {
            // Arrange
            var repository = new ProductRepository();
            var product = new Product
            {
                Name = "Premium Coffee",
                Description = "A premium quality coffee for true connoisseurs.",
                Price = 199.99m,
                Stock = 50
            };

            // Act
            repository.AddProduct(product);
            var products = repository.GetProducts();

            // Assert
            Assert.Single(products);
            foreach (var p in products)
            {
                Assert.Equal("Premium Coffee", p.Name);
                Assert.Equal(199.99m, p.Price);
            }
        }

        [Theory]
        [InlineData("", "Description", 100, 5, "Product name cannot be empty.")]
        [InlineData("Valid Name", "", 100, 5, "Product description cannot be empty.")]
        [InlineData("Valid Name", "Description", 0, 5, "Product price must be greater than zero.")]
        [InlineData("Valid Name", "Description", 100, -1, "Product stock cannot be negative.")]
        public void AddNewProduct_ShouldThrowArgumentException_WhenDataIsInvalid(
            string name, string description, decimal price, int stock, string expectedMessage)
        {
            // Arrange
            var repository = new ProductRepository();
            var product = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                Stock = stock
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => repository.AddProduct(product));
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
