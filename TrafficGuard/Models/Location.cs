using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TrafficGuard.Models
{
    public partial class Location
    {
        public Location()
        {
            Accidents = new HashSet<Accident>();
        }

        [Key]
        public int Id { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Longitude { get; set; }
        [Column("District_Id")]
        public int DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        [InverseProperty("Locations")]
        public virtual District District { get; set; } = null!;
        [InverseProperty("Location")]
        public virtual ICollection<Accident> Accidents { get; set; }
    }
}
