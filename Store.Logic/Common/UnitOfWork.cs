using Store.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Logic.Common
{
    public class UnitOfWork : IUnitOfWork
    {
     //   private readonly ClothDbContext context;
        public UnitOfWork()
        {
            //context = new ClothDbContext();
            Clothes = new MockClothRepository();// new ClothRepository(context);
            Orders = new MockOrderRepository();//new OrderRepository(context);
            Categories = new MockCategoryRepository();//new CategoryRepository(context);
        }


        public ICategoryRepository Categories { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public IClothRepository Clothes { get; private set; }

        public int Complete()
        {
            return 0;// context.SaveChanges();
        }

        public void Dispose()
        {
           
           // context.Dispose();
        }
    }
}
