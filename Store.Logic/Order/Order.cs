using Store.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Logic
{
    public class Order : Entity
    {
        public List<OrderDetail> OrderItems { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string Country { get; set; }

        public decimal OrderTotal { get; set; }

        public DateTime DatePlaced { get; set; }


    }

   
}
