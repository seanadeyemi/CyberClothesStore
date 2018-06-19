using ClassLibrary1;
using Store.Domain;
using Store.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store.Logic
{
    public class ClothRepository : Repository<ICloth>, IClothRepository
    {
      ///  private readonly ClothDbContext _DbContext;
        public ClothRepository(ClothDbContext context) : base(context)
        {
          //  _DbContext = context;
        }

        public IEnumerable<ICloth> Clothes
        {
            get { return GetAll(); }
        }

        public IEnumerable<ICloth> FavoriteClothes

        {
            get { return this.Find(c => c.IsFavorite == true); }


        }
        public IEnumerable<ICloth> GetCheapestClothes

        {
            get { return this.Find(c => c.Price < 500).Take(6).OrderBy(c => c.Price); }


        }


    }
}
