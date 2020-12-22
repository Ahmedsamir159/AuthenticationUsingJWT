using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Web.Api.Core.Shared;

namespace Web.Api.Core.Domain.Entities
{
    public class Product 
    {
        [Key]
        public int Id { get; set; }


        public string Name{ get; private set; } // EF migrations require at least private setter - won't work on auto-property
        public string Number{ get; private set; }
        public int Quantity { get; private set; }
        internal Product(int id, string name, string number, int quantity)
        {
            Id = id;
            Name= name;
            Number = number;
            Quantity = quantity;
        }
    }

}
