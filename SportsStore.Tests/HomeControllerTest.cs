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

    [Fact]
    public void Can_Paginate()
    {
        //Arrange
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns((new Product[]
        {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 3, Name = "P3"},
            new Product {ProductID = 4, Name = "P4"},
            new Product {ProductID = 5, Name = "P5"}
        }).AsQueryable<Product>());

        HomeController controller = new HomeController(mock.Object);
        controller.PageSize= 3;

        //Act
        IEnumerable<Product> result = 
            (controller.Index(2) as ViewResult)?.ViewData.Model as IEnumerable<Product> 
            ?? Enumerable.Empty<Product>();

        //Assert
        Product[] prodArray = result.ToArray();
        Assert.True(prodArray.Length==2);
        Assert.Equal("P4", prodArray[0].Name);
        Assert.Equal("P5", prodArray[1].Name);
    }
    
    [Fact]
    public void Can_Send_Pagination_View_Model()
    {
        //Arrange
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns((new Product[]
        {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 3, Name = "P3"},
            new Product {ProductID = 4, Name = "P4"},
            new Product {ProductID = 5, Name = "P5"}
        }).AsQueryable<Product>());

        //Arrange
        HomeController controller = new HomeController(mock.Object) { PageSize= 3};


    }
}