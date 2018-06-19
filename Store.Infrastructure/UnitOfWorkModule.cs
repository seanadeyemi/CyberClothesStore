using Ninject;
using Ninject.Modules;
using Store.Domain;
using Store.Infrastructure.Interfaces;
using Store.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure
{
    public class UnitOfWorkModule : NinjectModule
    {
        public override void Load()
        {
            // Get config service
            //var configService = Kernel.Get<IConfigService>();
            //configService.MyConnection
            // Bind repositories
            Bind<IUnitOfWork>().To<UnitOfWork>();
            //.WithConstructorArgument("DbContext", new ClothDbContext());
                
            //Bind<IProductRepository>().To<ProductRepository>()
            //    .WithConstructorArgument("connectionString", configService.MyConnection);
        }
    }
}
