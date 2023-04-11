using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace JKSE_Web_API.Models
{
    [NotMapped]
    public class ParamForeignFlowCVS
    {
        public string? TickerCode { get; set; }
          
        public string? VolumeTotal { get; set; }

        public string? ValueTotal { get; set; }

        public string? VolumeBuy { get; set; }

        public string? VolumeSell { get; set; }
    }
}
