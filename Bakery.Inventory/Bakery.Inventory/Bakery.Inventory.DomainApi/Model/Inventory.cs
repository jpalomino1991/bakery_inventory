﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Text;

namespace Bakery.Inventory.DomainApi.Model
{
    public class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        //public virtual Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        public String Invoice { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        //public User User { get; set; }
        public String Location { get; set; }
    }
}
