using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Data;
using PMS.Framework;
using PMS.Models;
using static PMS.Framework.CandidateInfo;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository projectRepository;
        ProjectViewModel objModel = new ProjectViewModel();

        public ProjectController(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public IActionResult Index()
        {
            objModel = LoadProjects();
            return View(objModel);
        }
        public IActionResult ProjectInfo()
        {
            DataValue dv = new DataValue();
            dv.Add(ProjectInfoVariables.ActionId, 1, EnumCommand.DataType.Int);
            objModel = projectRepository.GetProjectInfo(dv);
            return View(objModel);
        }
        public IActionResult SaveProjectInfo(string ProjectId)
        {

            ResultArgs result = new ResultArgs();
            DataValue dv = new DataValue();
            if (Convert.ToInt32(ProjectId) > 0)
            {
                dv.Add(MessageCatalog.ProjectProperty.ActionId, 3, EnumCommand.DataType.Int);
                dv.Add(MessageCatalog.ProjectProperty.ProjectId, ProjectId, EnumCommand.DataType.Int);
                objModel = projectRepository.UpdateProjectInfo(dv);
                // objDocument.DocumentName = Document();
            }
            //else
            //{

            //    DocumentInfo Document = new DocumentInfo();
            //    Document.AvailableDocument = "";
            //    Document.DocumentId = Document;
            //    Document.DocumentName = Document();
            //}

            return View(objModel);
        }
        public IActionResult SaveProjectsInfo(ProjectModel Project)
        {
            ResultArgs result = new ResultArgs();
            DataTable Data = new DataTable();
            DataValue dv = new DataValue();
            if (Convert.ToInt64(Project.ProjectId) > 0)
            {
                dv.Add(MessageCatalog.ProjectProperty.IsInsert, 1, EnumCommand.DataType.Int);
                dv.Add(MessageCatalog.ProjectProperty.ProjectId, Project.ProjectId, EnumCommand.DataType.Int);

            }
            else
            {
                dv.Add(MessageCatalog.ProjectProperty.IsInsert, 0, EnumCommand.DataType.Int);
                dv.Add(MessageCatalog.ProjectProperty.ProjectId, Project.ProjectId, EnumCommand.DataType.Int);

            }
            dv.Add(MessageCatalog.ProjectProperty.ActionId, 2, EnumCommand.DataType.Int);

            dv.Add(MessageCatalog.ProjectProperty.ProjectTitle, Project.ProjectTitle, EnumCommand.DataType.Varchar);
            dv.Add(MessageCatalog.ProjectProperty.ProjectScope, Project.ProjectScope, EnumCommand.DataType.Varchar);
            dv.Add(MessageCatalog.ProjectProperty.Document, Project.Document, EnumCommand.DataType.Varchar);
            dv.Add(MessageCatalog.ProjectProperty.StartDateEndDate, Project.StartDateEndDate, EnumCommand.DataType.DateTime);
            dv.Add(MessageCatalog.ProjectProperty.HoursTaken, Project.HoursTaken, EnumCommand.DataType.Int);
            dv.Add(MessageCatalog.ProjectProperty.MentorName, Project.MentorName, EnumCommand.DataType.Varchar);


            result = projectRepository.SaveProjectInfo(dv);
            if (result.Success)
            {
                TempData["ToastrMessage"] = string.Format(MessageCatalog.ToastrStyle.Toastr_Style, "Saved Successfully.", "ProjectInfo", "success");
            }
            else
            {
                TempData["ToastrMessage"] = string.Format(MessageCatalog.ToastrStyle.Toastr_Style, "Something Went Worng.", "ProjectInfo ", "info");

            }

            return View("Index", LoadProjects());

        }
        public IActionResult Save(ProjectModel Project)
        {
            string va = ViewBag.CandidateId;
            if (ModelState.IsValid)
            {
                string ProjectTitle = Project.ProjectTitle;
                string ProjectScope = Project.ProjectScope;
                string Document = Project.Document;
                string StartDateEndDate = Project.StartDateEndDate;
                int HoursTaken = Project.HoursTaken;
                string MentorName = Project.MentorName;
            }
            return RedirectToAction("ProjectInfo");
        }
        public IActionResult Delete(int ProjectId)
        {
            ResultArgs result = new ResultArgs();            DataValue dv = new DataValue();            dv.Add(MessageCatalog.ProjectProperty.ActionId, 4, EnumCommand.DataType.Int);            dv.Add(MessageCatalog.ProjectProperty.ProjectId, ProjectId, EnumCommand.DataType.Int);            projectRepository.DeleteProjectInfo(dv);

            if (result.Success)
            {
                TempData["ToastrMessage"] = string.Format(MessageCatalog.ToastrStyle.Toastr_Style, "Deleted Successfully.", "ProjectInfo", "success");
            }
            else
            {
                TempData["ToastrMessage"] = string.Format(MessageCatalog.ToastrStyle.Toastr_Style, "Deleted Successfully.", "ProjectInfo ", "error");

            }
            return RedirectToAction("ProjectInfo");
        }
        public IActionResult UpdateProjectInfo(int CandidateId)
        {

            DataValue dv = new DataValue();
            dv.Add(ProjectInfoVariables.ProjectId, 1, EnumCommand.DataType.Int32);
            objModel = projectRepository.UpdateProjectInfo(dv);
            var Update = objModel.ProjectInfo.Where(a => a.ProjectId == CandidateId).ToList().First();
            objModel.Project = Update;
            return View("SaveCandidateInfo", objModel);
        }

        private ProjectViewModel LoadProjects()
        {
            DataValue dv = new DataValue();
            dv.Add(ProjectInfoVariables.ActionId, 1, EnumCommand.DataType.Int);
            return projectRepository.GetProjectInfo(dv);
        }
    }
}
