using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JKSE_Web_API.Models
{
    public class DailyStats
    {
        [Key]
        [Column(TypeName = "Date")]
        public DateTime DateData { get; set; }

        [Column(TypeName = "Decimal (8,3)")]
        public decimal IndexPrice { get; set; }

        [Column(TypeName = "Decimal (8,3)")]
        public decimal VolumeTransaction { get; set; }

        [Column(TypeName = "Decimal (8,3)")]
        public decimal Turnover { get; set; }

        [Column(TypeName = "Decimal (8,3)")]
        public decimal ForeignNetBuy { get; set; }
    }
}
