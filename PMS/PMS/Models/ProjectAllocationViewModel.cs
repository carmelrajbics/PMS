using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class ProjectAllocationViewModel
    {
        public List<ProjectAllocationModel> ProjectAllocation { get; set; }
    }
    public class ProjectAllocationModel
    {
        public string ProjectName { get; set; }
        public string CandidateName { get; set; }
        public string MentorName { get; set; }
    }
}
