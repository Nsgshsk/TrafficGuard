using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrafficGuard.Models
{
    public class AccidentReport
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "decimal(9, 6)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:G8}")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(9, 6)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:G8}")]
        public decimal Longitude { get; set; }
        public Location Location { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public int? NumVehicles { get; set; }
        [StringLength(255)]
        public string? Description { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? PathToFiles { get; set; }
        public int TrustWorthyRating { get; } = 0;
    }
}
