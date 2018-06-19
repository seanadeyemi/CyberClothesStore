using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Interfaces
{
    public interface IConfigService
    {
        string MyConnection { get; }
    }
}
