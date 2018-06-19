using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain
{
    public interface IClothRepository : IRepository<ICloth>
    {
        IEnumerable<ICloth> FavoriteClothes { get; }

         IEnumerable<ICloth> GetCheapestClothes{ get; }
    }
}
