using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IJobsApiService
	{

		JobsModel GetJobs(JobsModel model);

		string DeleteJobs(JobsModel model);

		List<JobsModel> GetAllJobs(long UserId);

		ResponseModel<string> SaveJob(JobsModel model);

	}
}
