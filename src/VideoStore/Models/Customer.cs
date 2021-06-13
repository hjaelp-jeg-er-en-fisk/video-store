using System;
using System.Collections;
using System.Collections.Generic;

namespace VideoStore.Models
{
    public class Customer
	{
        public string Name { get; set; }
        public List<Rental> Rentals { get; set; }
	}
}
