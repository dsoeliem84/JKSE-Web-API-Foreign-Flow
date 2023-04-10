using JKSE_Web_API.Data;
using JKSE_Web_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using s.Models;
using System.Drawing.Drawing2D;

namespace JKSE_Web_API.Controllers
{

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

        [HttpPost(Name ="Add")]
        public async Task<ActionResult> Add(DateTime pDateInputData, TypeDataFlow pTypeDataFlow, List<ParamForeignFlowCVS> pDataForeignFlow)
        {
            try
            {
               List<ForeignFlow> lstDataForeignFlow = new List<ForeignFlow>();

                foreach (ParamForeignFlowCVS dataParam in pDataForeignFlow)
                {
                    var data = new ForeignFlow()
                    {
                        DateData = DateTime.Now.Date,
                        TickerCode = dataParam.TickerCode,
                        TypeFlow = (int)pTypeDataFlow,
                        ValueTotal = Convert.ToInt64(dataParam.ValueTotal.Replace(",", "")),
                        VolumeBuy = Convert.ToInt32(dataParam.VolumeBuy.Replace(",", "")),
                        VolumeTotal = Convert.ToInt32(dataParam.VolumeTotal.Replace(",", "")),
                        VolumeSell = Convert.ToInt32(dataParam.VolumeSell.Replace(",", "")),
                        DominationRatio = 0,
                        NetRatioVolume = 0,
                    };

                    lstDataForeignFlow.Add(data);
                }

                _context.Entry(lstDataForeignFlow).State = EntityState.Detached;
                await _context.ForeignFlow.AddRangeAsync(lstDataForeignFlow);
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

    public enum TypeDataFlow
    {
        inflow = 1,
        outflow = -1
    }
}
