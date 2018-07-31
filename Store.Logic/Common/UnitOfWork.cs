using ClassLibrary1;
using Store.Domain;
using Store.DomainN;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Logic.Common
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ClothDbContext context;
        public UnitOfWork()
        {
            context = new ClothDbContext();
            Clothes = new ClothRepository(context);// new MockClothRepository();
            Orders = new OrderRepository(context);// new MockOrderRepository();
            Categories = new CategoryRepository(context);//new MockCategoryRepository();
            Users = new UserRepository(context);
        }


        public ICategoryRepository Categories { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public IClothRepository Clothes { get; private set; }

       

        public IUserRepository<IClothAppUser> Users { get; private set; }

        public int Commit()
        {
          return context.SaveChanges();//   0;
        }

        public void Dispose()
        {
           
            context.Dispose();
        }
    }
}
