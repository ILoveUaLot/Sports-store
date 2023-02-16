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
            //Create mock page context and session
            Mock<ISession> mockSession = new Mock<ISession>();
            byte[] data =
                Encoding.UTF8.GetBytes(JsonSerializer.Serialize(TestCart));
            mockSession.Setup(c => c.TryGetValue(It.IsAny<string>(), out data));

            Mock<HttpContext> mockContext = new Mock<HttpContext>();
            mockContext.Setup(c=> c.Session).Returns(mockSession.Object);

            //Action
            CartModel cartModel = new CartModel(mock.Object)
            {
                PageContext = new PageContext(new ActionContext
                {
                    HttpContext = mockContext.Object,
                    RouteData = new RouteData(),
                    ActionDescriptor = new PageActionDescriptor()
                })
            };
            cartModel.OnGet("myUrl");
        }
    }
}
