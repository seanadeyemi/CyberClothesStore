using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain
{
    public interface IOrderRepository : IRepository<IOrder>
    {
        void CreateOrder(IOrder order);
    }
}
