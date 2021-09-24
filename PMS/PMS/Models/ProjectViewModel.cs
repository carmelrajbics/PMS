using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class ProjectViewModel
    {
            public List<ProjectModel> ProjectInfo { get; set; }
            public ProjectModel Project { get; set; }
            public List<Document> Document { get; set; }
            public ProjectModel Documents { get; set; }

        }
        public class ProjectModel
        {
            public int ProjectId { get; set; }
            public string ProjectTitle { get; set; }
            public string ProjectScope { get; set; }
            public string Document { get; set; }
            public string StartDateEndDate { get; set; }
            public int HoursTaken { get; set; }
            public string MentorName { get; set; }

        }
        public class Document
        {
            public int DocumentId { get; set; }
            public string DocumentName { get; set; }
        }
    
}
