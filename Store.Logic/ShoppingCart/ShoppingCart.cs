using Store.Domain;
using Store.DomainN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store.Logic
{
    public class ShoppingCart : IShoppingCart<Cloth, OrderDetail>
    {
        private List<OrderDetail> orderCollection = new List<OrderDetail>();


        public void AddItem(Cloth cloth, int quantity)
        {
            OrderDetail line = orderCollection.Where(c => c.Cloth.Id == cloth.Id).FirstOrDefault();

            if(line == null)
            {

              
                orderCollection.Add(new OrderDetail { Cloth = cloth, ClothId = cloth.Id, Price = cloth.Price, Quantity = quantity });
                
            }
            else
            {
                line.Quantity += quantity;
            }

        }

        public void RemoveItem(Cloth cloth)
        {
            orderCollection.RemoveAll(c => c.Cloth.Id == cloth.Id);
        }

        public IEnumerable<OrderDetail> Items
        {
            get { return orderCollection; }
        }



        public decimal CalculateTotalAmount()
        {
            

            return orderCollection.Sum(c => c.Price * c.Quantity);
        }

        public void Clear()
        {
            orderCollection.Clear();
        }
    }
}
