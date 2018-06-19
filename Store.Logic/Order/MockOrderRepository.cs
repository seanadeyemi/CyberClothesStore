using Store.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using Store.Logic.Common;
using ClassLibrary1;

namespace Store.Logic
{
    public class MockOrderRepository : MockRepository<IOrder>, IOrderRepository
    {
        private readonly ClothDbContext _DbContext;

        public ShoppingCart _shoppingCart;
        public MockOrderRepository() 
        {
           
          
        }

        public IEnumerable<Order> Orders {


            get; set;


        }



        public ShoppingCart ShoppingCart { get { return _shoppingCart; } set { _shoppingCart = value; } }

        public void CreateOrder(IOrder order)
        {
            //Order _order = (Order)order;

            //_order.DatePlaced = DateTime.Now;

            //var shoppingCartItems = _shoppingCart.Items;

            //foreach (var item in shoppingCartItems)
            //{
            //    //Cloth cloth = (Cloth)item.Cloth;
            //    var orderDetail = new OrderDetail()
            //    {


            //        Quantity = item.Quantity,
            //        ClothId = item.Cloth.Id,//cloth.Id,
            //        OrderId = _order.Id,
            //        Price = item.Cloth.Price
            //    };

            //    _DbContext.OrderDetails.Add(orderDetail);
            //}

            //_DbContext.SaveChanges();
        }

    }
}
