using System;
using System.Collections.Generic;
using System.Text;

namespace GoldDigger.Model
{
    public class Customer
    {
        // ATTRIBUTES

        public int CustomerId { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostCode { get; set; }
        public string Residence { get; set; }
        public string Mail { get; set; }

    }
}
