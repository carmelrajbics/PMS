using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class CandidateViewModel
    {
        public List<CandidateModel> CandidateInfo { get; set; }
        public CandidateModel Candidate { get; set; }
    }
    public class CandidateModel
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string Qualification { get; set; }
        public string Specilization { get; set; }
        public string Gender { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }

    }
}
