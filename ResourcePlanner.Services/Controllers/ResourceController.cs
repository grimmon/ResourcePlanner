using ResourcePlanner.Services.DataAccess;
using ResourcePlanner.Services.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static ResourcePlanner.Services.Enums.Enums;

namespace ResourcePlanner.Services.Controllers
{
    [RoutePrefix("/api/resource")]
    public class ResourceController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get(int pageSize, int pageNum, TimeAggregation agg= TimeAggregation.Weekly, SortOrder sort = SortOrder.LastName,  string city = "", string market = "", string region = "", string orgUnit = "", string practice = "", string position = "", DateTime? StartDateParam = null, DateTime? EndDateParam = null)
        {
            DateTime StartDate;
            DateTime EndDate;
            if (StartDateParam == null || EndDateParam == null)
            {
                StartDate = DateTime.Now.AddMonths(-1);
                EndDate = DateTime.Now.AddMonths(1);
            }
            else
            {
                StartDate = StartDateParam.Value;
                EndDate = EndDateParam.Value;
            }

            var pageParams = new ResourceQuery()
            {
                Aggregation = agg,
                Sort = sort,
                PageSize = pageSize,
                PageNum = pageNum,
                City = Array.ConvertAll(city.Split(','), s=> int.Parse(s)),
                OrgUnit = Array.ConvertAll(orgUnit.Split(','), s => int.Parse(s)),
                Market = Array.ConvertAll(market.Split(','), s => int.Parse(s)),
                Region = Array.ConvertAll(region.Split(','), s => int.Parse(s)),
                Position = Array.ConvertAll(position.Split(','), s => int.Parse(s)),
                Practice = Array.ConvertAll(practice.Split(','), s => int.Parse(s)),
                StartDate = StartDate,
                EndDate = EndDate

            };
            
#if Mock
            var access = new MockDataAccess(ConfigurationManager.ConnectionStrings["RPDBConnectionString"].ConnectionString,
                                                Int32.Parse(ConfigurationManager.AppSettings["DBTimeout"]));
#else
            var access = new ResourceDataAccess(ConfigurationManager.ConnectionStrings["RPDBConnectionString"].ConnectionString,
                                                Int32.Parse(ConfigurationManager.AppSettings["DBTimeout"]));
#endif

            ResourcePage resourcePage;

            try
            {
                resourcePage = access.GetResourcePage(pageParams);
            }
            catch (Exception ex)
            {
                throw;
            }

            return Ok(resourcePage);
        }
    }
}
