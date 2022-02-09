using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class JobsService : IJobsService
	{
		public DBContext dbContext;


		public JobsService()
		{
		}

		public  JobsService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddJobsData(JobsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                JobsDAL JobsDalobj = new JobsDAL(dbContext);
                string value = JobsDalobj.AddJobsData(Data);
                return_value.Value = value;
                return_value.Message = "Success";
                return_value.HttpStatusCode = "200";
                return_value.IsSuccess = true;
            }
            catch (Exception)
            {
                throw;
            }
            return return_value;

		}

		public ResponseModel<JobsModel> GetJobsData(JobsModel Data)
		{
			ResponseModel<JobsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<JobsModel>();
                JobsDAL JobsDalobj = new JobsDAL(dbContext);
                JobsModel value = JobsDalobj.GetJobsData(Data);
                return_value.Value = value;
                return_value.Message = "Success";
                return_value.HttpStatusCode = "200";
                return_value.IsSuccess = true;
            }
            catch (Exception)
            {
                throw;
            }
            return return_value;

		}

		public ResponseModel<string> DeleteJobsData(JobsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                JobsDAL JobsDalobj = new JobsDAL(dbContext);
                string value = JobsDalobj.DeleteJobsData(Data);
                return_value.Value = value;
                return_value.Message = "Success";
                return_value.HttpStatusCode = "200";
                return_value.IsSuccess = true;
            }
            catch (Exception)
            {
                throw;
            }
            return return_value;

		}

		public ResponseModel<List<JobsModel >> GetAllJobsForUserID(long userId)
		{
			ResponseModel<List<JobsModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<JobsModel >>();
                JobsDAL JobsDalobj = new JobsDAL(dbContext);
                List<JobsModel> value = JobsDalobj.GetAllJobsForUserID(userId);
                return_value.Value = value;
                return_value.Message = "Success";
                return_value.HttpStatusCode = "200";
                return_value.IsSuccess = true;
            }
            catch (Exception)
            {
                throw;
            }
            return return_value;

		}

		public ResponseModel<string> SaveJob(JobsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                JobsDAL JobsDalobj = new JobsDAL(dbContext);
                string value = JobsDalobj.SaveData(Data);
                return_value.Value = value;
                return_value.Message = "Success";
                return_value.HttpStatusCode = "200";
                return_value.IsSuccess = true;
            }
            catch (Exception)
            {
                throw;
            }
            return return_value;

		}

	}
}
