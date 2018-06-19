using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }

        IOrderRepository Orders { get; }


        IClothRepository Clothes { get; }


        int Complete();
    }
}
