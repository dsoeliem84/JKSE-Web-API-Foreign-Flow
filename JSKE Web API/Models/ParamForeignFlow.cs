using System.Reflection.Metadata.Ecma335;

namespace s.Models
{
    public class ParamForeignFlowCVS
    {
        private string _tickerCode = "";

        public string TickerCode
        {
            get { return _tickerCode.Substring(0,4); }
            set { _tickerCode = value; }
        }
          
        public string? VolumeTotal { get; set; }

        public string? ValueTotal { get; set; }

        public string? VolumeBuy { get; set; }

        public string? VolumeSell { get; set; }
    }
}
