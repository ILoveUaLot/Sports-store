using Xunit;
using System.Linq;
using Moq;
using SportsStore.Models;
namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            //Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1"};
            Product p2 = new Product { ProductID =2, Name="P2"};

            //Arrange - create a new cart
            Cart target = new Cart();

            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            //Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            //Arrange - create some products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            //Arrange - create cart
            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 3);
            cart.AddItem(p3, 5);

            //Act
            cart.RemoveLine(p1);
            cart.RemoveLine(p2);
            
            //Assert
            Assert.Empty(cart.Lines.Where(c=>c.Product == p1 && c.Product==p2));
            Assert.Equal(1,cart.Lines.Count());
        }

        [Fact]
        public void Can_Calculate_Cart_Total()
        {
            //Arrange - create some products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 10M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 5M };
            Product p3 = new Product { ProductID = 3, Name = "P3", Price = 15M };

            //Arrange - create cart
            Cart cart = new Cart();
            cart.AddItem(p1, 4);
            cart.AddItem(p2, 1);
            cart.AddItem(p3, 3);
            cart.AddItem(p1, 3);

            //Act
            decimal result = cart.ComputeTotalValue();

            //Assert
        }
    }
}
