using Ninject.Modules;
using Store.DomainN;
using Store.Infrastructure.Interfaces;
using Store.Infrastructure.Services;
using Store.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IShoppingCart<,>)).To<ShoppingCart>();//(typeof(ShoppingCart));
            Bind(typeof(IShoppingCartService<>)).To<ClothShoppingCartService>();//(typeof(ClothShoppingCartService));
        }
    }
}
