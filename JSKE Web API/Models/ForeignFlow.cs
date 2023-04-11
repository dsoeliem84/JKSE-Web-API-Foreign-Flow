using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JKSE_Web_API.Models
{
    [PrimaryKey(nameof(DateData), nameof(TickerCode))]
    public class ForeignFlow
    {
        [Column(TypeName = "Date")]
        public DateTime DateData { get; set; }

        [Column(TypeName = "Varchar(4)")]
        public string? TickerCode { get; set; }
        
        public int TypeFlow { get; set; }

        public int VolumeTotal { get; set; }

        public long ValueTotal { get; set; }

        public int VolumeBuy { get; set; }

        public int VolumeSell { get; set; }

        [Column(TypeName = "Decimal (8,2)")]
        public decimal NetRatioVolume { get; set; }

        [Column(TypeName = "Decimal (8,2)")]
        public decimal DominationRatio { get; set; }
    }
}