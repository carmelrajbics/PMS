using Microsoft.AspNetCore.Mvc;
using PMS.Data;
using PMS.Models;
using System;
using PMS.Framework;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PMS.DBEngine;

using System.Threading.Tasks;
using static PMS.Framework.CandidateInfo;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace PMS.Controllers
{

    public class CandicateController : Controller
    {
        private readonly ICandidateRepository CandidateRepository;
        CandidateViewModel objModel = new CandidateViewModel();
        public CandicateController(ICandidateRepository CandidateRepository)
        {
            this.CandidateRepository = CandidateRepository;
        }
        public IActionResult Index()
        {
            objModel = LoadCandidate();
            ViewBag.Specilization = LoadSpecilization();
            ViewBag.State = LoadStates();
            ViewBag.District = LoadDistrict();
            ViewBag.Qualification = LoadQualification();
            return View(objModel);
        }

        [HttpPost]
        public IActionResult SaveCandidateInfo(string CandidateId)
        {

            ResultArgs result = new ResultArgs();
            DataValue dv = new DataValue();
            if (Convert.ToInt32(CandidateId) > 0)
            {
                dv.Add(MessageCatalog.CandidateProperty.ActionId, 3, EnumCommand.DataType.Int);
                dv.Add(MessageCatalog.CandidateProperty.CandidateId, CandidateId, EnumCommand.DataType.Int);
                objModel = CandidateRepository.UpdateCanInfo(dv);
            }

            return View(objModel);
        }

        [HttpPost]
        public IActionResult SaveCandidatesInfo(CandidateModel Candidate)
        {
            ResultArgs result = new ResultArgs();
            DataTable Data = new DataTable();
            DataValue dv = new DataValue();
            if (Convert.ToInt64(Candidate.CandidateId) > 0)
            {
                dv.Add(MessageCatalog.CandidateProperty.IsInsert, 1, EnumCommand.DataType.Int);
                dv.Add(MessageCatalog.CandidateProperty.CandidateId, Candidate.CandidateId, EnumCommand.DataType.Int);

            }
            else
            {
                dv.Add(MessageCatalog.CandidateProperty.IsInsert, 0, EnumCommand.DataType.Int);
                dv.Add(MessageCatalog.CandidateProperty.CandidateId, Candidate.CandidateId, EnumCommand.DataType.Int);

            }
            dv.Add(MessageCatalog.CandidateProperty.ActionId, 2, EnumCommand.DataType.Int);

            dv.Add(MessageCatalog.CandidateProperty.CandidateName, Candidate.CandidateName, EnumCommand.DataType.Varchar);
            dv.Add(MessageCatalog.CandidateProperty.Qualification, Candidate.Qualification, EnumCommand.DataType.Varchar);
            dv.Add(MessageCatalog.CandidateProperty.Specilization, Candidate.Specilization, EnumCommand.DataType.Varchar);
            dv.Add(MessageCatalog.CandidateProperty.Gender, Candidate.Gender, EnumCommand.DataType.Varchar);
            dv.Add(MessageCatalog.CandidateProperty.State, Candidate.State, EnumCommand.DataType.Varchar);
            dv.Add(MessageCatalog.CandidateProperty.District, Candidate.District, EnumCommand.DataType.Varchar);
            dv.Add(MessageCatalog.CandidateProperty.Address, Candidate.Address, EnumCommand.DataType.Varchar);
            dv.Add(MessageCatalog.CandidateProperty.MobileNo, Candidate.MobileNo, EnumCommand.DataType.Varchar);
            dv.Add(MessageCatalog.CandidateProperty.Email, Candidate.Email, EnumCommand.DataType.Varchar);


            result = CandidateRepository.SaveCanInfo(dv);
            if (result.Success)
            {
                TempData["ToastrMessage"] = string.Format(MessageCatalog.ToastrStyle.Toastr_Style, "Saved Successfully.", "CanInfo", "success");
            }
            else
            {
                TempData["ToastrMessage"] = string.Format(MessageCatalog.ToastrStyle.Toastr_Style, "Saved Successfully.", "CanInfo ", "success");

            }

            return View("Index", LoadCandidate());
        }

        public IActionResult Delete(int CandidateId)
        {
            ResultArgs result = new ResultArgs();
            DataValue dv = new DataValue();
            dv.Add(MessageCatalog.CandidateProperty.ActionId, 4, EnumCommand.DataType.Int);
            dv.Add(MessageCatalog.CandidateProperty.CandidateId, CandidateId, EnumCommand.DataType.Int);
            CandidateRepository.DeleteCanInfo(dv);

            if (result.Success)
            {
                TempData["ToastrMessage"] = string.Format(MessageCatalog.ToastrStyle.Toastr_Style, "Deleted Successfully.", "CanInfo", "success");
            }
            else
            {
                TempData["ToastrMessage"] = string.Format(MessageCatalog.ToastrStyle.Toastr_Style, "Deleted Successfully.", "CanInfo ", "error");

            }
            return RedirectToAction("CanInfo");
        }
        public IActionResult UpdateCandidate(int CandidateId)
        {
            DataValue dv = new DataValue();
            dv.Add(CandidateInfoVariables.CandidateId, 1, EnumCommand.DataType.Int32);
            objModel = CandidateRepository.UpdateCanInfo(dv);
            var Update = objModel.CandidateInfo.Where(a => a.CandidateId == CandidateId).ToList().First();
            objModel.Candidate = Update;
            return View("SaveCandidateInfo", objModel);
        }

        private CandidateViewModel LoadCandidate()
        {
            DataValue dv = new DataValue();
            dv.Add(CandidateInfoVariables.ActionId, 1, EnumCommand.DataType.Int);
            return CandidateRepository.GetCandidateInfo(dv);
        }

        private List<SelectListItem> LoadSpecilization()
        {
            return CandidateRepository.GetSpecilizationInfo();
        }

        private List<SelectListItem> LoadStates()
        {
            return CandidateRepository.GetStates();
        }

        private List<SelectListItem> LoadDistrict()
        {
            return CandidateRepository.GetDistrict();
        }
        private List<SelectListItem> LoadQualification()
        {
            return CandidateRepository.GetQualification();
        }
    }
}
