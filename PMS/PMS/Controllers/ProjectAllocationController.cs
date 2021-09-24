using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Data;
using PMS.Framework;
using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PMS.Framework.CandidateInfo;

namespace PMS.Controllers
{
    public class ProjectAllocationController : Controller
    {
        private readonly ICandidateRepository candidateRepository;
        private readonly IProjectRepository projectRepository;

        public ProjectAllocationController(ICandidateRepository CandidateRepository, IProjectRepository ProjectRepository)
        {
            candidateRepository = CandidateRepository;
            projectRepository = ProjectRepository;
        }
        public IActionResult Index()
        {
            return View(new ProjectAllocationViewModel());
        }

        [HttpPost]
        public IActionResult AllocateProject()
        {
            DataValue dvCandiate = new DataValue();
            dvCandiate.Add(CandidateInfoVariables.ActionId, 1, EnumCommand.DataType.Int);
            var candidateResult = candidateRepository.GetCandidateInfo(dvCandiate);

            DataValue dvProject = new DataValue();
            dvProject.Add(ProjectInfoVariables.ActionId, 1, EnumCommand.DataType.Int);
            var projectResult = projectRepository.GetProjectInfo(dvProject);

            if (candidateResult.CandidateInfo.Count > projectResult.ProjectInfo.Count)
            {
                ViewBag.ProjectCountLessThanCandidate = "Project count should be greater than or equal to Candidate count";
            }

            Shuffler shuffler = new Shuffler();
            shuffler.Shuffle(projectResult.ProjectInfo);
            Dictionary<int, int> allocatedProject = new Dictionary<int, int>();
            ProjectAllocationViewModel projectAllocationViewModel = new ProjectAllocationViewModel();
            projectAllocationViewModel.ProjectAllocation = new List<ProjectAllocationModel>();

            for (int i = 0; i < candidateResult.CandidateInfo.Count; i++)
            {
                ProjectAllocationModel projectAllocation = new ProjectAllocationModel();
                allocatedProject.Add(candidateResult.CandidateInfo[i].CandidateId, projectResult.ProjectInfo[i].ProjectId);
                projectAllocation.ProjectName = projectResult.ProjectInfo[i].ProjectTitle;
                projectAllocation.CandidateName = candidateResult.CandidateInfo[i].CandidateName;
                projectAllocation.MentorName = projectResult.ProjectInfo[i].MentorName;
                i++;
                projectAllocationViewModel.ProjectAllocation.Add(projectAllocation);
            }

            return View("Index", projectAllocationViewModel);
        }

        public class Shuffler
        {
            public Shuffler()
            {
                _rng = new Random();
            }

            /// <summary>Shuffles the specified array.</summary>
            /// <typeparam name="T">The type of the array elements.</typeparam>
            /// <param name="array">The array to shuffle.</param>

            public void Shuffle<T>(IList<T> array)
            {
                for (int n = array.Count; n > 1;)
                {
                    int k = _rng.Next(n);
                    --n;
                    T temp = array[n];
                    array[n] = array[k];
                    array[k] = temp;
                }
            }

            private System.Random _rng;
        }

    }
}
