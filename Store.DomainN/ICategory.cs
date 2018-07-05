using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain
{
    public interface ICategory
    {
        string Name { get; set; }

        string Description { get; set; }

    }
}
