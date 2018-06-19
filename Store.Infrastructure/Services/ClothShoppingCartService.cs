using Store.Domain;
using Store.DomainN;
using Store.Infrastructure.Interfaces;
using Store.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Services
{
    public class ClothShoppingCartService : IShoppingCartService<Cloth>
    {
        IShoppingCart<Cloth, OrderDetail> _shoppingCart;
        // private readonly ILoggingService _logger;
        //public ClothShoppingCartService():this(new ShoppingCart())
        //{

        //}

        public ClothShoppingCartService(IShoppingCart<Cloth, OrderDetail> shoppingCart)
        {
            _shoppingCart = shoppingCart;
           // _logger = logger;
        }
        public void AddItem(Cloth entity, int quantity)
        {
            _shoppingCart.AddItem(entity, quantity);
        }

        public decimal CalculateTotalAmount()
        {
           return _shoppingCart.CalculateTotalAmount();
        }

        public void Clear()
        {
            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    _logger.Error(ex, "Something happened");
            //    //throw;
            //}
            
            _shoppingCart.Clear();
        }

        public void RemoveItem(Cloth entity)
        {
            _shoppingCart.RemoveItem(entity);
        }

        public IEnumerable<OrderDetail> Items { get { return _shoppingCart.Items; } }
    }
}
