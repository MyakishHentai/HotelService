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
            var Service1 = new ServiceItem { Id = 1, Title = "P1", Cost = 200.00M};
            var Service2 = new ServiceItem { Id = 2, Title = "P2", Cost = 400.00M };
            // Arrange - create a new cart
            var Target = new ShoppingCart();
            // Act
            Target.AddItem(Service1, 1);
            Target.AddItem(Service2, 1);
            Request[] Results = Target.Lines.ToArray();
            // Assert
            Assert.Equal(2, Results.Length);
            Assert.Equal(Service1, Results[0].Service);
            Assert.Equal(Service2, Results[1].Service);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            var Service1 = new ServiceItem { Id = 1, Title = "P1", Cost = 200.00M };
            var Service2 = new ServiceItem { Id = 2, Title = "P2", Cost = 400.00M };
            // Arrange - create a new cart
            var Target = new ShoppingCart();
            // Act
            Target.AddItem(Service1, 1);
            Target.AddItem(Service2, 1);
            Target.AddItem(Service1, 10);
            Request[] Results = Target.Lines
                .OrderBy(c => c.Service.Id).ToArray();
            // Assert
            Assert.Equal(2, Results.Length);
            Assert.Equal(11, Results[0].Quantity);
            Assert.Equal(1, Results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            // Arrange - create some test products
            var Service1 = new ServiceItem { Id = 1, Title = "P1", Cost = 200.00M };
            var Service2 = new ServiceItem { Id = 2, Title = "P2", Cost = 400.00M };
            var Service3 = new ServiceItem { Id = 3, Title = "P2", Cost = 400.00M };
            // Arrange - create a new cart
            var Target = new ShoppingCart();
            // Arrange - add some products to the cart
            Target.AddItem(Service1, 1);
            Target.AddItem(Service2, 3);
            Target.AddItem(Service3, 5);
            Target.AddItem(Service2, 1);
            // Act
            Target.RemoveLine(Service2);
            // Assert
            Assert.Empty(Target.Lines.Where(c => c.Service == Service2));
            Assert.Equal(2, Target.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            var Service1 = new ServiceItem { Id = 1, Title = "P1", Cost = 200.00M };
            var Service2 = new ServiceItem { Id = 2, Title = "P2", Cost = 400.00M };
            // Arrange - create a new cart
            var Target = new ShoppingCart();
            // Act
            Target.AddItem(Service1, 1);
            Target.AddItem(Service2, 1);
            Target.AddItem(Service1, 3);
            decimal Result = Target.ComputeTotalValue();
            // Assert
            Assert.Equal(1200.00M, Result);
        }

        [Fact]
        public void Can_Clear_Contents()
        {
            // Arrange - create some test products
            var Service1 = new ServiceItem { Id = 1, Title = "P1", Cost = 200.00M };
            var Service2 = new ServiceItem { Id = 2, Title = "P2", Cost = 400.00M };
            // Arrange - create a new cart
            var Target = new ShoppingCart();
            // Arrange - add some items
            Target.AddItem(Service1, 1);
            Target.AddItem(Service2, 1);
            // Act - reset the cart
            Target.Clear();
            // Assert
            Assert.Empty(Target.Lines);
        }
    }
}
