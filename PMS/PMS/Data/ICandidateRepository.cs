using PMS.Models;
using System;
using System.Data;
using System.Linq;
using PMS.Framework;
using PMS.DBEngine;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace PMS.Data
{

    public interface ICandidateRepository
    {
        CandidateViewModel GetCandidateInfo(DataValue dv = null);
        CandidateViewModel GetCandidateInfoById(DataValue dv = null);
        ResultArgs SaveCanInfo(DataValue dv = null);
        CandidateViewModel UpdateCanInfo(DataValue dv = null);
        ResultArgs DeleteCanInfo(DataValue dv);
        List<SelectListItem> GetSpecilizationInfo();
        List<SelectListItem> GetStates();
        List<SelectListItem> GetDistrict();
        List<SelectListItem> GetQualification();

    }
    public class CandidateRepository : ICandidateRepository
    {

        private string sSQL;

        public CandidateRepository(ISqlServerHanlder SQLServerHandler)
        {
            objHandler = SQLServerHandler;
        }
        private ISqlServerHanlder objHandler;

        public CandidateViewModel UpdateCanInfo(DataValue dv)
        {
            CandidateViewModel objModel = new CandidateViewModel();
            ResultArgs result = new ResultArgs();
            result = EditCanInfo(dv);
            if (result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
            {
                objModel.Candidate = (from DataRow dr in result.DataSource.Table.Rows
                                      select new CandidateModel
                                      {

                                          CandidateId = Convert.ToInt32(dr["CandidateId"].ToString()),
                                          CandidateName = dr["CandidateName"].ToString(),
                                          Qualification = dr["Qualification"].ToString(),
                                          Specilization = dr["Specilization"].ToString(),
                                          Gender = dr["Gender"].ToString(),
                                          State = dr["State"].ToString(),
                                          District = dr["District"].ToString(),
                                          Address = dr["Address"].ToString(),
                                          MobileNo = dr["MobileNo"].ToString(),
                                          Email = dr["Email"].ToString()
                                      }
             ).FirstOrDefault();
            }

            return objModel;

        }
        public CandidateViewModel GetCandidateInfo(DataValue dv)
        {
            CandidateViewModel objModel = new CandidateViewModel();
            try
            {
                ResultArgs result = new ResultArgs();
                result = FetchCandidateInfo(dv);
                if (result.DataSource != null)
                {
                    if (result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        objModel.CandidateInfo = (from DataRow dr in result.DataSource.Table.Rows
                                                  select new CandidateModel
                                                  {
                                                      CandidateId = Convert.ToInt32(dr["CandidateId"].ToString()),
                                                      CandidateName = dr["CandidateName"].ToString(),
                                                      Qualification = dr["Qualification"].ToString(),
                                                      Specilization = dr["Specilization"].ToString(),
                                                      Gender = dr["Gender"].ToString(),
                                                      State = dr["State"].ToString(),
                                                      District = dr["District"].ToString(),
                                                      Address = dr["Address"].ToString(),
                                                      MobileNo = dr["MobileNo"].ToString(),
                                                      Email = dr["Email"].ToString()
                                                  }
                     ).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objModel;
        }
        public CandidateViewModel GetCandidateInfoById(DataValue dv)
        {
            CandidateViewModel objModel = new CandidateViewModel();
            try
            {
                ResultArgs result = new ResultArgs();
                result = FetchCandidateInfo(dv);
                if (result.DataSource != null)
                {
                    if (result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        objModel.CandidateInfo = (from DataRow dr in result.DataSource.Table.Rows
                                                  select new CandidateModel
                                                  {

                                                      CandidateId = Convert.ToInt32(dr["CandidateId"].ToString()),
                                                      CandidateName = dr["CandidateName"].ToString(),
                                                      Qualification = dr["Qualification"].ToString(),
                                                      Specilization = dr["Specilization"].ToString(),
                                                      Gender = dr["Gender"].ToString(),
                                                      State = dr["State"].ToString(),
                                                      District = dr["District"].ToString(),
                                                      Address = dr["Address"].ToString(),
                                                      MobileNo = dr["MobileNo"].ToString(),
                                                      Email = dr["Email"].ToString()

                                                  }
                     ).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objModel;
        }

        public List<SelectListItem> GetSpecilizationInfo()
        {
            List<SelectListItem> specilization = new List<SelectListItem>();
            try
            {
                ResultArgs result = new ResultArgs();
                result = FetchSpecilizationInfo();
                if (result.DataSource != null && result.DataSource.Table != null)
                {
                    foreach (DataRow item in result.DataSource.Table.Rows)
                    {
                        specilization.Add(new SelectListItem { Value = item["SpecilizationId"].ToString(), Text = item["SpecilizationName"].ToString() });
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return specilization;
        }

        public List<SelectListItem> GetDistrict()
        {
            List<SelectListItem> district = new List<SelectListItem>();
            try
            {
                ResultArgs result = new ResultArgs();
                result = FetchDistrict();
                if (result.DataSource != null && result.DataSource.Table != null)
                {
                    foreach (DataRow item in result.DataSource.Table.Rows)
                    {
                        district.Add(new SelectListItem { Value = item["DistirctId"].ToString(), Text = item["Distirctname"].ToString() });
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return district;
        }

        public List<SelectListItem> GetStates()
        {
            List<SelectListItem> states = new List<SelectListItem>();
            try
            {
                ResultArgs result = new ResultArgs();
                result = FetchSate();
                if (result.DataSource != null && result.DataSource.Table != null)
                {
                    foreach (DataRow item in result.DataSource.Table.Rows)
                    {
                        states.Add(new SelectListItem { Value = item["StateId"].ToString(), Text = item["StateName"].ToString() });
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return states;
        }

        public List<SelectListItem> GetQualification()
        {
            List<SelectListItem> qualification = new List<SelectListItem>();
            try
            {
                ResultArgs result = new ResultArgs();
                result = FetchQualification();
                if (result.DataSource != null && result.DataSource.Table != null)
                {
                    foreach (DataRow item in result.DataSource.Table.Rows)
                    {
                        qualification.Add(new SelectListItem { Value = item["QualificationId"].ToString(), Text = item["QualificationName"].ToString() });
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return qualification;
        }


        public ResultArgs FetchCandidateInfo(DataValue dv = null)
        {
            sSQL = "[dbo].[ManageCandidateInformation]";
            return objHandler.FetchData(sSQL, EnumCommand.DataSource.DataTable, dv, EnumCommand.SQLType.SQLStoredProcedure);
        }

        public ResultArgs FetchSpecilizationInfo(DataValue dv = null)
        {
            sSQL = "[dbo].[Specilization]";
            return objHandler.FetchData(sSQL, EnumCommand.DataSource.DataTable, dv, EnumCommand.SQLType.SQLStoredProcedure);
        }
        public ResultArgs FetchDistrict(DataValue dv = null)
        {
            sSQL = "[dbo].[Distirct]";
            return objHandler.FetchData(sSQL, EnumCommand.DataSource.DataTable, dv, EnumCommand.SQLType.SQLStoredProcedure);
        }
        public ResultArgs FetchSate(DataValue dv = null)
        {
            sSQL = "[dbo].[State]";
            return objHandler.FetchData(sSQL, EnumCommand.DataSource.DataTable, dv, EnumCommand.SQLType.SQLStoredProcedure);
        }

        public ResultArgs FetchQualification(DataValue dv = null)
        {
            sSQL = "[dbo].[Qualification]";
            return objHandler.FetchData(sSQL, EnumCommand.DataSource.DataTable, dv, EnumCommand.SQLType.SQLStoredProcedure);
        }

        public ResultArgs SaveCanInfo(DataValue dv)
        {
            sSQL = "[dbo].[ManageCandidateInformation]";
            return objHandler.ExecuteCommand(sSQL, dv, false, EnumCommand.SQLType.SQLStoredProcedure);        }
        public ResultArgs EditCanInfo(DataValue dv)
        {
            sSQL = "[dbo].[ManageCandidateInformation]";
            return objHandler.FetchData(sSQL, EnumCommand.DataSource.DataTable, dv, EnumCommand.SQLType.SQLStoredProcedure);        }
        public ResultArgs DeleteCanInfo(DataValue dv)
        {
            sSQL = "[dbo].[ManageCandidateInformation]";
            return objHandler.ExecuteCommand(sSQL, dv, false, EnumCommand.SQLType.SQLStoredProcedure);        }
    }
}
