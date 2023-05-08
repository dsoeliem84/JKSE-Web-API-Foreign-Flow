using JKSE_Web_API.Data;
using JKSE_Web_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.Common;
using System.Data;
using Dapper;
using JKSE_Web_API.Data.Enum;

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

        [HttpGet(Name = "GetDailyStats")]
        public async Task<ActionResult<List<DailyStats>>> GetDailyStats(DateTime dateData, int recordNo)
        {
            try
            {
                recordNo = (recordNo == 0) ? 10 : recordNo;

                var dailyStats = _context.DailyStats
                    .Where(x => x.DateData <= dateData.Date)
                    .OrderByDescending(x => x.DateData)
                    .Take(recordNo);

                if (dailyStats.Count() > 0)
                    return Ok(dailyStats);
                else
                    return Ok("No current daily stats data");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Name = "GetForeignFlowData")]
        public async Task<ActionResult<List<ForeignFlow>>> GetForeignFlowData(DateTime dateData, int recordNo)
        {
            try
            {
                recordNo = (recordNo == 0) ? 10 : recordNo;

                var dailyData = _context.ForeignFlow
                    .Where(x => x.DateData == dateData.Date && x.ValueTotal > 10000000)
                    .OrderByDescending(x => x.NetRatioVolume)
                    .Take(recordNo);

                if (dailyData.Count() > 0)
                    return Ok(dailyData);
                else
                    return Ok("No current foreign flow data");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Name = "GetReportForeignFlow")]
        public async Task<ActionResult> GetReportForeignFlow(TypeDataFlow pTypeDataFlow, DateTime pStartDate, DateTime pEndDate, long pValueTransaction1, long pValueTransaction2, decimal pRatioNetFlow, int pTotalDays)
        {
            try
            {
                //using dapper for easier call stored proc that return join tables"

                using (var conn = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    //Set up DynamicParameters object to pass parameters  
                    DynamicParameters dParams = new DynamicParameters();
                    dParams.Add("pTypeFlow", (int)pTypeDataFlow);
                    dParams.Add("pStartDate", pStartDate);
                    dParams.Add("pEndDate", pEndDate);
                    dParams.Add("pValueTransaction1", pValueTransaction1);
                    dParams.Add("pValueTransaction2", pValueTransaction2);
                    dParams.Add("pRatioNetFlow", pRatioNetFlow);
                    dParams.Add("pTotalDays", pTotalDays);

                    //Execute stored procedure and map the returned result to a Customer object  
                    var foreignFlowData = await conn.QueryAsync<ForeignFlowReport>("SP_REPORT_INFLOW_OUTFLOW", dParams, commandType: CommandType.StoredProcedure);

                    return Ok(foreignFlowData);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost(Name = "AddDailyStats")]
        public async Task<ActionResult> AddDailyStats(DateTime pDateInputData, decimal pIndexPrice, decimal pVolumeTransaction, decimal pTurnover, decimal pForeignNetBuy)
        {
            try
            {
                var dataWithSameDate = _context.DailyStats.Where(x => x.DateData == pDateInputData);
                _context.DailyStats.RemoveRange(dataWithSameDate);
                await _context.SaveChangesAsync();

                DailyStats dailyStats = new DailyStats();

                dailyStats.DateData = pDateInputData;
                dailyStats.IndexPrice = pIndexPrice;
                dailyStats.VolumeTransaction = pVolumeTransaction;
                dailyStats.Turnover = pTurnover;
                dailyStats.ForeignNetBuy = pForeignNetBuy;

                _context.DailyStats.Add(dailyStats);
                await _context.SaveChangesAsync();

                return Ok("Success added daily stats data");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "LoadData")]
        public async Task<ActionResult> LoadData(DateTime pDateInputData, TypeDataFlow pTypeDataFlow, List<ParamForeignFlowCVS> lstDataForeignFlow)
        {
            try
            {

                var dataWithSameDate = _context.ForeignFlow.Where(x => (x.DateData == DateTime.Now.Date && x.TypeFlow == (int)pTypeDataFlow));
                _context.ForeignFlow.RemoveRange(dataWithSameDate);
                await _context.SaveChangesAsync();

                List<ForeignFlow> lstData = new List<ForeignFlow>();

                int volBuy = 1;
                int volSell = 1;
                int volTotal = 1;
                long valTotal = 1;


                foreach (ParamForeignFlowCVS dataParam in lstDataForeignFlow)
                {
                    if (!dataParam.TickerCode.ToUpper().Contains("-W") && !dataParam.TickerCode.ToUpper().Contains("R-"))
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
