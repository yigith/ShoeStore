using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Address
    {
        public Address()
        {
        }

        public Address(string street, string city, string state, string zipCode, string country)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
            Country = country;
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}
