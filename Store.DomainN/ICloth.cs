using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain
{
    public interface ICloth
    {
        int Id { get; set; }
        string Name { get; set; }

         string ShortDescription { get; set; }
        string LongDescription { get; set; }

        decimal Price { get; set; }

        string ImageUrl { get; set; }

         string ImageThumbnailUrl { get; set; }

        bool IsFavorite { get; set; }

        int InStock { get; set; }

        int CategoryId { get; set; }

        ICategory Category { get; set; }
    }
}
