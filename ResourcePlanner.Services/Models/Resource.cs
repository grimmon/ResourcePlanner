using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourcePlanner.Services.Models
{
    public class ResourcePage
    {
        public string TimeScale { get; set; }
        public List<string> TimePeriods { get; set; }
        public List<Resource> Resources{ get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
    }

    public class DetailPage
    {
        public string TimeScale { get; set; }
        public List<string> TimePeriods { get; set; }
        public ResourceInfo ResourceInfo { get; set; }
        public List<Project> Projects { get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
    }
    public class Resource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string City { get; set; }
        public List<Assignment> Assignments { get; set; }
    }

    public class ResourceInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Practice { get; set; }
        public string Position { get; set; }
        public string OrgUnit { get; set; }
        public string Region { get; set; }
        public string Market { get; set; }
        public string City { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
    }

    public class Assignment
    {
        public string ProjectName { get; set; }
        public string TimePeriod { get; set; }
        public double ForecastHours { get; set; }
        public double ActualHours{ get; set; }
    }

    public class Project
    {
        public string ProjectName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WBSElement { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }
        public string OpportunityOwnerFirstName { get; set; }
        public string OpportunityOwnerLastName { get; set; }
        public string ProjectManagerFirstName { get; set; }
        public string ProjectManagerLastName { get; set; }
        public List<Assignment> Assignments { get; set; }

    }


}