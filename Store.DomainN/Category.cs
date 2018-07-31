using Store.Domain;

using System;
using System.Collections.Generic;
using System.Text;


namespace Store.Domain
{
    public class Category : Entity, IAggregateRoot, ICategory
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Cloth> Clothes { get; set; }

    }
}
