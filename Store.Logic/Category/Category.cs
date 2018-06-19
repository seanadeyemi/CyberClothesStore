using Store.Domain;
using Store.Logic.Common;
using System;
using System.Collections.Generic;
using System.Text;


namespace Store.Logic
{
    public class Category : Entity, IAggregateRoot, ICategory
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Cloth> Clothes { get; set; }

    }
}
