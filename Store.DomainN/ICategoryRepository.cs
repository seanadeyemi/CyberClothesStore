using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain
{
    public interface ICategoryRepository : IRepository<Category>
    {
        //IEnumerable<Category> Categories{ get; }
    }
}
