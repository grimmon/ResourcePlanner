using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ResourcePlanner.Services.Models;
using ResourcePlanner.Services.Utilities;
using ResourcePlanner.Services.Mapper;
using System.Data;
using System.Data.SqlClient;

namespace ResourcePlanner.Services.DataAccess
{
    public class ResourceDataAccess
    {
        private readonly string _connectionString;
        private readonly int _timeout;

        public ResourceDataAccess(string connectionString, int timeout)
        {
            _connectionString = connectionString;
            _timeout = timeout;
        }


        public ResourcePage GetResourcePage(DateTime StartDate, DateTime EndDate)
        {

            var returnValue = AdoUtility.ExecuteQuery(reader => EntityMapper.MapToResourcePage(reader),
                _connectionString,
                @"rpdb.ResourcePageSelect",
                CommandType.StoredProcedure,
                _timeout,
                CreateResourcePageParamArray(StartDate, EndDate));
            return returnValue;
        }

        public DetailPage GetResourceDetail(int ResourceId, DateTime StartDate, DateTime EndDate)
        {
            var returnValue = AdoUtility.ExecuteQuery(reader => EntityMapper.MapToResourceDetail(reader),
                _connectionString,
                @"rpdb.ResourceDetailSelect",
                CommandType.StoredProcedure,
                _timeout,
                CreateResourceDetailParamArray(ResourceId, StartDate, EndDate));
            return returnValue;
        }

        private SqlParameter[] CreateResourcePageParamArray(DateTime StartDate, DateTime EndDate)
        {
            var StartDateParam = AdoUtility.CreateSqlParameter("StartDateParam", SqlDbType.Date, StartDate);
            var EndDateParam = AdoUtility.CreateSqlParameter("EndDateParam", SqlDbType.Date, EndDate);

            return new SqlParameter[] { StartDateParam, EndDateParam };
        }

        private SqlParameter[] CreateResourceDetailParamArray(int ResourceId, DateTime StartDate, DateTime EndDate)
        {
            var StartDateParam = AdoUtility.CreateSqlParameter("StartDateParam", SqlDbType.Date, StartDate);
            var EndDateParam = AdoUtility.CreateSqlParameter("EndDateParam", SqlDbType.Date, EndDate);
            var ResourceIdParam = AdoUtility.CreateSqlParameter("ResourceId", SqlDbType.Int, ResourceId);

            return new SqlParameter[] { StartDateParam, EndDateParam, ResourceIdParam };
        }
    }
}