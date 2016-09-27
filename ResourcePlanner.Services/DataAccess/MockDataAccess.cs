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
    public class MockDataAccess
    {
        private readonly string _connectionString;
        private readonly int _timeout;

        public MockDataAccess(string connectionString, int timeout)
        {
            _connectionString = connectionString;
            _timeout = timeout;
        }


        public ResourcePage GetResourcePage(DateTime StartDate, DateTime EndDate)
        {

            var rand = new Random();
            var resourcePage = new ResourcePage()
            {
                Resources = new List<Resource>()
            };


            int resourceCount = rand.Next(50, 100);

            for(int i = 0; i < resourceCount; i++)
            {
                var resource = new Resource()
                {
                    FirstName = LoremIpsumGenerator.LoremIpsum(1,1),
                    LastName = LoremIpsumGenerator.LoremIpsum(1, 1),
                    City = LoremIpsumGenerator.LoremIpsum(1, 2),
                    Position =  LoremIpsumGenerator.LoremIpsum(2, 4),
                    Assignments = new List<Assignment>()
                };
                int assignCount = rand.Next(5, 10);
                for (int j = 0; j < assignCount; j++)
                {
                    var assignment = new Assignment();

                    assignment.TimePeriod = new Guid().ToString();
                    assignment.ForecastHours = rand.NextDouble() * 40;
                    assignment.ActualHours = rand.NextDouble() * 40;

                    resource.Assignments.Add(assignment);
                }

                resourcePage.Resources.Add(resource);
            }
            return resourcePage;
        }

        public DetailPage GetResourceDetail(int ResourceId, DateTime StartDate, DateTime EndDate)
        {
            var rand = new Random();
            var resourceInfo = new ResourceInfo();

            resourceInfo.FirstName = LoremIpsumGenerator.LoremIpsum(1, 1);
            resourceInfo.LastName = LoremIpsumGenerator.LoremIpsum(1, 1);
            resourceInfo.Practice = LoremIpsumGenerator.LoremIpsum(1, 3);
            resourceInfo.OrgUnit = LoremIpsumGenerator.LoremIpsum(1, 2);
            resourceInfo.Market = LoremIpsumGenerator.LoremIpsum(1, 2);
            resourceInfo.City = LoremIpsumGenerator.LoremIpsum(1, 2);
            resourceInfo.Position = LoremIpsumGenerator.LoremIpsum(2, 4);
            resourceInfo.ManagerFirstName = LoremIpsumGenerator.LoremIpsum(1, 1);
            resourceInfo.ManagerLastName = LoremIpsumGenerator.LoremIpsum(1, 1);

            int numProjects = rand.Next(5, 10);
            while (reader.Read())
            {
                var assignment = new Assignment();
                curr = reader.GetInt32("ProjectId");
                if (curr != prev)
                {
                    var newResource = new Project()
                    {
                        ProjectName = reader.GetNullableString("ProjectName"),
                        WBSElement = reader.GetNullableString("WBSElement"),
                        Customer = reader.GetNullableString("Customer"),
                        Description = reader.GetNullableString("Description"),
                        OpportunityOwnerFirstName = reader.GetNullableString("OpportunityOwnerFirstName"),
                        OpportunityOwnerLastName = reader.GetNullableString("OpportunityOwnerLastName"),
                        ProjectManagerFirstName = reader.GetNullableString("ProjectManagerFirstName"),
                        ProjectManagerLastName = reader.GetNullableString("ProjectManagerLastName"),
                        Assignments = new List<Assignment>()
                    };
                    projects.Add(curr, newResource);
                }
                prev = curr;

                assignment.TimePeriod = reader.GetString("TimePeriod");
                assignment.ForecastHours = reader.GetDouble("ForecastHours");
                assignment.ActualHours = reader.GetDouble("ActualHours");

                projects[curr].Assignments.Add(assignment);


            }

            var detailPage = new DetailPage()
            {
                ResourceInfo = resourceInfo,
                Projects = projects.Values.ToList()
            };

            return detailPage;
        }

    }
}