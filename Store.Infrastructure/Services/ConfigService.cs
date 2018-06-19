using Store.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Services
{
    class ConfigService : IConfigService
    {
        public string MyConnection
        {
            get
            {
                string cnString = null;
                var cnSettings = ConfigurationManager.ConnectionStrings["ClothDbContext"];
                if (cnSettings != null)
                {
                    cnString = cnSettings.ConnectionString;
                }
                return cnString;
            }
        }
    }
}
