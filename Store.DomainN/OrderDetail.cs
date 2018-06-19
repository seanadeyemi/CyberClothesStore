namespace Store.Domain
{
    public class OrderDetail : Entity
    {
        public int Quantity { get; set; }

        public decimal Price { get; set; }


        public int ClothId { get; set; }

        public virtual ICloth Cloth { get; set; }

        public int OrderId { get; set; }

        public virtual IOrder Order { get; set; }
    }
}