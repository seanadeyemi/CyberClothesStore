using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Interfaces
{
    public interface IShoppingCartService<T> where T : class
    {
        void AddItem(T entity, int quantity);

        void RemoveItem(T entity);

        //IEnumerable<OrderDetail> Items { get;}


        decimal CalculateTotalAmount();

        void Clear();
    }
}
