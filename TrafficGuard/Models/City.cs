using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TrafficGuard.Models
{
    public partial class City
    {
        public City()
        {
            Districts = new HashSet<District>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; }

        [InverseProperty("City")]
        public virtual ICollection<District> Districts { get; set; }
    }
}
