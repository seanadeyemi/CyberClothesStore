using Ninject.Modules;
using Store.Infrastructure.Interfaces;
using Store.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure
{
    public class ConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfigService>().To<ConfigService>();
        }
    }
}
