using JKSE_Web_API.Data;
using JKSE_Web_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JKSE_Web_API.Controllers
{
    public enum TypeDataFlow
    {
        inflow = 1,
        outflow = -1
    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JkseForeignFlowController : ControllerBase
    {     

        private readonly JkseDataContext _context;

        public JkseForeignFlowController(JkseDataContext context)
        {
            this._context = context;
        }

        [HttpGet (Name = "GetDailyData")]
        public async Task<ActionResult<List<ForeignFlow>>> GetDailyData()
        {
            try
            {
                var dailyData = _context.ForeignFlow.Where(x => x.DateData == DateTime.Now.Date);

                return Ok(dailyData);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name ="LoadData")]
        public async Task<ActionResult> LoadData(DateTime pDateInputData, TypeDataFlow pTypeDataFlow, List<ParamForeignFlowCVS> lstDataForeignFlow)
        {
            try
            {

                var dataWithSameDate = _context.ForeignFlow.Where(x => x.DateData == DateTime.Now.Date);
                _context.ForeignFlow.RemoveRange(dataWithSameDate);
                await _context.SaveChangesAsync();

                List<ForeignFlow> lstData = new List<ForeignFlow>();
                
                int volBuy = 1;
                int volSell = 1;
                int volTotal = 1;
                long valTotal = 1;
                

                foreach (ParamForeignFlowCVS dataParam in lstDataForeignFlow)
                {
                    if (!dataParam.TickerCode.ToUpper().Contains("-W"))
                    {
                        volBuy = Convert.ToInt32(dataParam.VolumeBuy.Replace(",", ""));
                        volSell = Convert.ToInt32(dataParam.VolumeSell.Replace(",", ""));
                        volTotal = Convert.ToInt32(dataParam.VolumeTotal.Replace(",", ""));
                        valTotal = Convert.ToInt64(dataParam.ValueTotal.Replace(",", ""));

                        if(volTotal == 0) { volTotal = 1; }
                        if(volSell == 0) { volSell = 1; }

                        var data = new ForeignFlow()
                        {
                            DateData = DateTime.Now.Date,
                            TickerCode = dataParam.TickerCode,
                            TypeFlow = (int)pTypeDataFlow,
                            ValueTotal = valTotal,
                            VolumeBuy = volBuy,
                            VolumeTotal = volTotal,
                            VolumeSell = volSell,
                            DominationRatio = Convert.ToDecimal(volBuy) / Convert.ToDecimal(volTotal),
                            NetRatioVolume = Convert.ToDecimal(volBuy) / Convert.ToDecimal(volSell),
                        };

                        lstData.Add(data);
                    }                   
                    
                }
                await _context.ForeignFlow.AddRangeAsync(lstData);
                await _context.SaveChangesAsync();

                return Ok("Success added foreign flow data");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpPost(Name = "GetReportForeignFlow")]
        public async Task<ActionResult> GetReportForeignFlow(DateTime pDateInputData, TypeDataFlow pTypeDataFlow, List<ParamForeignFlowCVS> lstDataForeignFlow)
        {
            try
            {

                var dataWithSameDate = _context.ForeignFlow.Where(x => x.DateData == DateTime.Now.Date);
                _context.ForeignFlow.RemoveRange(dataWithSameDate);
                await _context.SaveChangesAsync();

                List<ForeignFlow> lstData = new List<ForeignFlow>();

                int volBuy = 1;
                int volSell = 1;
                int volTotal = 1;
                long valTotal = 1;


                foreach (ParamForeignFlowCVS dataParam in lstDataForeignFlow)
                {
                    if (!dataParam.TickerCode.ToUpper().Contains("-W"))
                    {
                        volBuy = Convert.ToInt32(dataParam.VolumeBuy.Replace(",", ""));
                        volSell = Convert.ToInt32(dataParam.VolumeSell.Replace(",", ""));
                        volTotal = Convert.ToInt32(dataParam.VolumeTotal.Replace(",", ""));
                        valTotal = Convert.ToInt64(dataParam.ValueTotal.Replace(",", ""));

                        if (volTotal == 0) { volTotal = 1; }
                        if (volSell == 0) { volSell = 1; }

                        var data = new ForeignFlow()
                        {
                            DateData = DateTime.Now.Date,
                            TickerCode = dataParam.TickerCode,
                            TypeFlow = (int)pTypeDataFlow,
                            ValueTotal = valTotal,
                            VolumeBuy = volBuy,
                            VolumeTotal = volTotal,
                            VolumeSell = volSell,
                            DominationRatio = Convert.ToDecimal(volBuy) / Convert.ToDecimal(volTotal),
                            NetRatioVolume = Convert.ToDecimal(volBuy) / Convert.ToDecimal(volSell),
                        };

                        lstData.Add(data);
                    }

                }
                await _context.ForeignFlow.AddRangeAsync(lstData);
                await _context.SaveChangesAsync();

                return Ok("Success added foreign flow data");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete(Name = "Delete")]
        public async Task<ActionResult> Delete(DateTime dateData)
        {
            try
            {
                var dataWithSameDate = _context.ForeignFlow.Where(x => x.DateData == dateData.Date);
                _context.ForeignFlow.RemoveRange(dataWithSameDate);
                await _context.SaveChangesAsync();

                return Ok("Success delete foreign flow data for date");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message); 
            }
            
        }
    }

    
}
