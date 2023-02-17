using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace TrafficGuard.Models
{
    public partial class District
    {
        public District()
        {
            Locations = new HashSet<Location>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("City_Id")]
        public int? CityId { get; set; }

        [ForeignKey("CityId")]
        [InverseProperty("Districts")]
        public virtual City City { get; set; } = null!;
        [InverseProperty("District")]
        public virtual ICollection<Location> Locations { get; set; }
    }
}
