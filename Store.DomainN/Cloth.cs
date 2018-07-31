using Store.Domain;

using System;
using System.Collections.Generic;
using System.Text;


namespace Store.Domain
{

    public class Cloth : Entity, IAggregateRoot, ICloth
    {
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        
        public string LongDescription { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string ImageThumbnailUrl { get; set; }

        public bool IsFavorite { get; set; }

        public int InStock { get; set; }

        public virtual Category Category { get; set; }
      }
}
