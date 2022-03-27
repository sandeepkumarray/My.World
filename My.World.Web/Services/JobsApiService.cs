using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace My.World.Web.Services
{
	public class JobsApiService : BaseAPIService, IJobsApiService
	{

		public string AddJobs(JobsModel model)
		{
			string jobsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Jobs/AddJobs";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			jobsModel = response.Value;
			return jobsModel;

		}

		public JobsModel GetJobs(JobsModel model)
		{
			JobsModel jobsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Jobs/GetJobs";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<JobsModel> response = JsonConvert.DeserializeObject<ResponseModel<JobsModel>>(jsonResult);
			jobsModel = response.Value;
			return jobsModel;

		}

		public string DeleteJobs(JobsModel model)
		{
			string jobsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Jobs/DeleteJobs";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			jobsModel = response.Value;
			return jobsModel;

		}

		public List<JobsModel> GetAllJobs(long UserId)
		{
			List<JobsModel> jobsModel = new List<JobsModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Jobs/GetAllJobs/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<JobsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<JobsModel>>>(jsonResult);
			jobsModel = response.Value;
			return jobsModel;

		}

		public ResponseModel<string> SaveJob(JobsModel model)
		{
			string jobsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Jobs/SaveJob";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
