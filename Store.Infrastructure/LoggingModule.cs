using Ninject.Modules;
using NLog;
using Store.Infrastructure.Interfaces;
using Store.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure
{
    public class LoggingModule : NinjectModule
    {
        public override void Load()
        {
            ILoggingService logger = GetLoggingService();
            Bind<ILoggingService>().ToConstant(logger);
        }

        private ILoggingService GetLoggingService()
        {
            ILoggingService logger = (ILoggingService)LogManager.GetLogger("NLogLogger", typeof(LoggingService));
            return logger;
        }
    }
}
