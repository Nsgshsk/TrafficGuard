using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TrafficGuard.Models
{
    public partial class Accident
    {
        [Key]
        public int Id { get; set; }
        [Column("Location_Id")]
        public int LocationId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }
        public int? NumVehicles { get; set; }
        [StringLength(255)]
        public string? Description { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? PathToFiles { get; set; }
        public int TrustWorthyRating { get; set; }

        [ForeignKey("LocationId")]
        [InverseProperty("Accidents")]
        public virtual Location Location { get; set; } = null!;
    }
}
