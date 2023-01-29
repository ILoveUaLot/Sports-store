namespace SportsStore.Tests;
using Xunit;
using SportsStore;
using Moq;
using SportsStore.Models;

public class HomeControllerTest
{
    [Fact]
    public void Can_Use_Repository()
    {
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns((new Product[]
        {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"}
        }).AsQueryable<Product>());
    }
}