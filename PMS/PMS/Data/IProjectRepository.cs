using PMS.DBEngine;
using PMS.Framework;
using PMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Data
{

    public interface IProjectRepository
    {
        ProjectViewModel GetProjectInfo(DataValue dv = null);
        ProjectViewModel GetProjectInfoById(DataValue dv = null);
        ProjectViewModel UpdateProjectInfo(DataValue dv = null);
        ResultArgs SaveProjectInfo(DataValue dv = null);
        ResultArgs DeleteProjectInfo(DataValue dv);
    }
    public class ProjectRepository : IProjectRepository
    {

        private string sSQL;

        public ProjectRepository(ISqlServerHanlder SQLServerHandler)
        {
            objHandler = SQLServerHandler;
        }
        private ISqlServerHanlder objHandler;
        public ProjectViewModel UpdateProjectInfo(DataValue dv)
        {
            ProjectViewModel objModel = new ProjectViewModel();
            try
            {
                ResultArgs result = new ResultArgs();
                result = EditProjectInfo(dv);
                if (result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                {
                    objModel.Project = (from DataRow dr in result.DataSource.Table.Rows
                                        select new ProjectModel
                                        {

                                            ProjectId = Convert.ToInt32(dr["ProjectId"].ToString()),
                                            ProjectTitle = dr["ProjectTitle"].ToString(),
                                            ProjectScope = dr["ProjectScope"].ToString(),
                                            Document = dr["Document"].ToString(),
                                            StartDateEndDate = dr["StartDateEndDate"].ToString(),
                                            HoursTaken = Convert.ToInt32(dr["HoursTaken"].ToString()),
                                            MentorName = dr["MentorName"].ToString()
                                        }
                 ).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objModel;

        }
        public ProjectViewModel GetProjectInfo(DataValue dv)
        {
            ProjectViewModel objModel = new ProjectViewModel();
            try
            {
                ResultArgs result = new ResultArgs();
                result = FetchProjectInfo(dv);
                if (result.DataSource != null)
                {
                    if (result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        objModel.ProjectInfo = (from DataRow dr in result.DataSource.Table.Rows
                                                select new ProjectModel
                                                {

                                                    ProjectId = Convert.ToInt32(dr["ProjectId"].ToString()),
                                                    ProjectTitle = dr["ProjectTitle"].ToString(),
                                                    ProjectScope = dr["ProjectScope"].ToString(),
                                                    Document = dr["Document"].ToString(),
                                                    StartDateEndDate = dr["StartDateEndDate"].ToString(),
                                                    HoursTaken = Convert.ToInt32(dr["HoursTaken"].ToString()),
                                                    MentorName = dr["MentorName"].ToString()
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
        public ProjectViewModel GetProjectInfoById(DataValue dv)
        {
            ProjectViewModel objModel = new ProjectViewModel();
            try
            {
                ResultArgs result = new ResultArgs();
                result = FetchProjectInfo(dv);
                if (result.DataSource != null)
                {
                    if (result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        objModel.ProjectInfo = (from DataRow dr in result.DataSource.Table.Rows
                                                select new ProjectModel
                                                {
                                                    ProjectId = Convert.ToInt32(dr["ProjectId"].ToString()),
                                                    ProjectTitle = dr["ProjectTitle"].ToString(),
                                                    ProjectScope = dr["ProjectScope"].ToString(),
                                                    Document = dr["Document"].ToString(),
                                                    StartDateEndDate = dr["StartDateEndDate"].ToString(),
                                                    HoursTaken = Convert.ToInt32(dr["HoursTaken"].ToString()),
                                                    MentorName = dr["MentorName"].ToString()

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

        public ResultArgs FetchProjectInfo(DataValue dv = null)
        {
            sSQL = "[dbo].[ProjectInfo]";
            return objHandler.FetchData(sSQL, EnumCommand.DataSource.DataTable, dv, EnumCommand.SQLType.SQLStoredProcedure);
        }
        public ResultArgs SaveProjectInfo(DataValue dv)
        {
            sSQL = "[dbo].[ProjectInfo]";
            return objHandler.ExecuteCommand(sSQL, dv, false, EnumCommand.SQLType.SQLStoredProcedure);
        }
        public ResultArgs EditProjectInfo(DataValue dv)
        {
            sSQL = "[dbo].[ProjectInfo]";
            return objHandler.FetchData(sSQL, EnumCommand.DataSource.DataTable, dv, EnumCommand.SQLType.SQLStoredProcedure);
        }
        public ResultArgs DeleteProjectInfo(DataValue dv)
        {
            sSQL = "[dbo].[ProjectInfo]";
            return objHandler.ExecuteCommand(sSQL, dv, false, EnumCommand.SQLType.SQLStoredProcedure);
        }
    }
}
