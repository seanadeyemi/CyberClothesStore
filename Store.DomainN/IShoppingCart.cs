using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DomainN
{
    public interface IShoppingCart<T, T2>  where T : class where T2 : class
    {
        void AddItem(T entity, int quantity);

        void RemoveItem(T entity);

       IEnumerable<T2> Items { get;}


        decimal CalculateTotalAmount();

        void Clear();
    }
}
