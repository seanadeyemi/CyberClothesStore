using Store.DomainN;
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

        IUserRepository<IClothAppUser> Users { get; }



        int Commit();
    }
}
