using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsStore.Models;
using SportsStore.Pages;
using System.Text;
using System.Text.Json;
using Xunit;

namespace SportsStore.Tests
{
    public class CartPageTests
    {
        [Fact]
        public void Can_Load_Cart()
        {
            //Arrange
            Product p1 = new Product { Name = "P1", ProductID = 1 };
            Product p2 = new Product { Name = "P2", ProductID = 2 };

            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                p1,
                p2
            }.AsQueryable<Product>());

            //create cart
            Cart TestCart = new Cart();
            TestCart.AddItem(p1, 2);
            TestCart.AddItem(p2, 1);

            //Action
            CartModel cartModel = new CartModel(mock.Object, TestCart);
            cartModel.OnGet("myUrl");

            //Assert
            Assert.Equal(2, cartModel?.cart.Lines.Count());
            Assert.Equal("myUrl", cartModel?.ReturnUrl);
        }

        [Fact]
        public void Can_Update_Cart()
        {
            // Arrange
            // - create a mock repository
            Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();
            mockRepo.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1" }
                }).AsQueryable<Product>());
            Cart? testCart = new Cart();

            // Action
            CartModel cartModel = new CartModel(mockRepo.Object, testCart);
            cartModel.OnPost(1, "myUrl");

            //Assert
            Assert.Single(testCart.Lines);
            Assert.Equal("P1", testCart.Lines.First().Product.Name);
            Assert.Equal(1, testCart.Lines.First().Product.ProductID);
        }
    }
}
