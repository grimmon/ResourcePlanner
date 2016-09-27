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

namespace ResourcePlanner.Services.Controllers
{
    [RoutePrefix("/api/resource")]
    public class ResourceController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get(DateTime? StartDateParam = null, DateTime? EndDateParam = null)
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

            var access = new MockDataAccess(ConfigurationManager.ConnectionStrings["RPDBConnectionString"].ConnectionString,
                                                Int32.Parse(ConfigurationManager.AppSettings["DBTimeout"]));
            ResourcePage resourcePage;

            try
            {
                resourcePage = access.GetResourcePage(StartDate, EndDate);
            }
            catch (Exception ex)
            {
                throw;
            }

            return Ok(resourcePage);
        }
    }
}
