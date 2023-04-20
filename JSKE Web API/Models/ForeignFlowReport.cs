using System.ComponentModel.DataAnnotations.Schema;

namespace JKSE_Web_API.Models
{
    [NotMapped]
    public class ForeignFlowReport
    {
        public string? TickerCode { get; set; }
        public int TotalDays { get; set; }
        public long VolumeTotal { get; set; }
        public int AccumulationValue { get; set; }
        public int NetBuyVolume { get; set; }
        public int NetSellVolume { get; set; }
        public int NetForeignVolume { get; set; }
        public string? RatioNetFlowText { get; set; }
        public string? VolatilityLevelText { get; set; }
        public decimal RatioNetFlow { get; set; }
        public decimal VolatilityLevel { get; set; }
    }
}