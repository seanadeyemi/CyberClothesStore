using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain
{
    public class ShoppingCartItem : Entity
    {
        public ICloth Cloth { get; set; }


        public string ShoppingCartId { get; set; }



    }
}
