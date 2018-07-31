using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain
{
    public interface IClothRepository : IRepository<Cloth>
    {
        IEnumerable<Cloth> FavoriteClothes { get; }

         IEnumerable<Cloth> GetCheapestClothes{ get; }
    }
}
