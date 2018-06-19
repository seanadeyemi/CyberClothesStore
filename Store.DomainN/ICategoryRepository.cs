using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain
{
    public interface ICategoryRepository : IRepository<ICategory>
    {
        IEnumerable<ICategory> Categories{ get; }
    }
}
