using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Models;
using HotelService.Models.Base;
using Xunit;
using ServiceItem = HotelService.Models.Base.Service;

namespace HotelService.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            // Arrange - create some test products
            var p1 = new ServiceItem { Id = 1, Title = "P1", Cost = 200.00M};
            var p2 = new ServiceItem { Id = 2, Title = "P2", Cost = 400.00M };
            // Arrange - create a new cart
            var target = new ShoppingCart();
            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            Request[] results = target.Lines.ToArray();
            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Service);
            Assert.Equal(p2, results[1].Service);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            var p1 = new ServiceItem { Id = 1, Title = "P1", Cost = 200.00M };
            var p2 = new ServiceItem { Id = 2, Title = "P2", Cost = 400.00M };
            // Arrange - create a new cart
            var target = new ShoppingCart();
            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            Request[] results = target.Lines
                .OrderBy(c => c.Service.Id).ToArray();
            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            // Arrange - create some test products
            var p1 = new ServiceItem { Id = 1, Title = "P1", Cost = 200.00M };
            var p2 = new ServiceItem { Id = 2, Title = "P2", Cost = 400.00M };
            var p3 = new ServiceItem { Id = 3, Title = "P2", Cost = 400.00M };
            // Arrange - create a new cart
            var target = new ShoppingCart();
            // Arrange - add some products to the cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);
            // Act
            target.RemoveLine(p2);
            // Assert
            Assert.Empty(target.Lines.Where(c => c.Service == p2));
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            var p1 = new ServiceItem { Id = 1, Title = "P1", Cost = 200.00M };
            var p2 = new ServiceItem { Id = 2, Title = "P2", Cost = 400.00M };
            // Arrange - create a new cart
            var target = new ShoppingCart();
            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();
            // Assert
            Assert.Equal(600M, result);
        }

        [Fact]
        public void Can_Clear_Contents()
        {
            // Arrange - create some test products
            var p1 = new ServiceItem { Id = 1, Title = "P1", Cost = 200.00M };
            var p2 = new ServiceItem { Id = 2, Title = "P2", Cost = 400.00M };
            // Arrange - create a new cart
            var target = new ShoppingCart();
            // Arrange - add some items
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            // Act - reset the cart
            target.Clear();
            // Assert
            Assert.Empty(target.Lines);
        }
    }
}
