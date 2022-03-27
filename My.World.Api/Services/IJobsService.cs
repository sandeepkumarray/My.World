using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IJobsService
	{

		ResponseModel<string> AddJobsData(JobsModel Data);

		ResponseModel<JobsModel> GetJobsData(JobsModel Data);

		ResponseModel<string> DeleteJobsData(JobsModel Data);

		ResponseModel<List<JobsModel >> GetAllJobsForUserID(long userId);

		ResponseModel<string> SaveJob(JobsModel Data);

		ResponseModel<string> UpdateJobsData(JobsModel Data);

	}
}
