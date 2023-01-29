namespace SportsStore.Tests;
using Xunit;
using SportsStore;
using Moq;
using SportsStore.Models;
using SportsStore.Controllers;
using Microsoft.AspNetCore.Mvc;

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

        HomeController controller = new HomeController(mock.Object);

        //Act
        IEnumerable<Product>? result = (controller.Index() as ViewResult)?.ViewData.Model 
            as IEnumerable<Product>;

        //Assert
        Product[] prodArray = result?.ToArray() ?? Array.Empty<Product>();

        Assert.True(prodArray.Length== 2);
        Assert.Equal("P1", prodArray[0].Name);
        Assert.Equal("P2", prodArray[1].Name);
    }
}