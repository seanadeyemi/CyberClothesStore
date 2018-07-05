using Store.Domain;
using Store.Logic.Common;
using System;
using System.Collections.Generic;
using System.Text;


namespace Store.Logic
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

        public int CategoryId { get; set; }

        public virtual ICategory Category { get; set; }
      }
}
