using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My.World.Api.Models;
using My.World.Web.Services;
using Newtonsoft.Json;
using My.World.Web.ViewModel;
using Microsoft.Extensions.Configuration;

namespace My.World.Web.Controllers
{
	[Authorize]
	[Route("jobs")]
	public class JobsController : Controller
	{
		public readonly IJobsApiService _iJobsApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public JobsController(IJobsApiService iJobsApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iJobsApiService = iJobsApiService;
			_iUniversesApiService = iUniversesApiService;
			_iUsersApiService = iUsersApiService;
			_iContenttypesApiService = iContenttypesApiService;
			_iObjectBucketApiService = iObjectBucketApiService;
			_config = config;

		}

		private string GetRawContent(string _rawContent)
		{
			using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
			{
			    return reader.ReadToEndAsync().Result;
			}

		}

		[Route("Index")]
		public IActionResult Index()
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			string imageFormat = _config.GetValue<string>("ContentImageUrlFormat");
			var jobs = _iJobsApiService.GetAllJobs(accountID);
			jobs.ForEach(b =>
			{
			    if (!string.IsNullOrEmpty(b.object_name))
			    {
			        b.image_url = imageFormat
			        .Replace("{bucketName}", _iObjectBucketApiService.objectStorageKeysModel.bucketName)
			        .Replace("{objectName}", b.object_name);
			    }
			    else
			    {
			        b.image_url = imageFormat
			          .Replace("{bucketName}", "my-world-main")
			          .Replace("{objectName}", "cards/Jobs.png");
			    }
			});
			return View(jobs);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewJobs(string Id)
		{
			JobsModel model = new JobsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["JobID"] = Id;
			ViewData["JobID"] = Id;
			HttpContext.Session.SetString("JobID", Id);
			
			JobsViewModel jobsViewModel = new JobsViewModel(_iObjectBucketApiService);
			jobsViewModel.jobsModel = _iJobsApiService.GetJobs(model);
			if (jobsViewModel.jobsModel == null)
				return RedirectToAction("Index", "NotFound");
			jobsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			jobsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "jobs");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Jobs" });
			jobsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			jobsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			jobsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "jobs");
			return View(jobsViewModel);

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewJobs(string Id)
		{
			JobsModel model = new JobsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["JobID"] = Id;
			ViewData["JobID"] = Id;
			HttpContext.Session.SetString("JobID", Id);
			
			JobsViewModel jobsViewModel = new JobsViewModel(_iObjectBucketApiService);
			jobsViewModel.jobsModel = _iJobsApiService.GetJobs(model);
			if (jobsViewModel.jobsModel == null)
			return RedirectToAction("Index", "NotFound");
			jobsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(jobsViewModel.jobsModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			jobsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "jobs");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Jobs" });
			jobsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			jobsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			jobsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "jobs");
			return View(jobsViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteJobs(string Id)
		{
			JobsModel model = new JobsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iJobsApiService.DeleteJobs(model);
			return RedirectToAction("Index");

		}

		public void TransformData(JobsModel model)
		{
			if (model != null)
			{
				
				model.Alternate_names  = model.Alternate_names == null ? model.Alternate_names : model.Alternate_names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Education  = model.Education == null ? model.Education : model.Education.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Experience  = model.Experience == null ? model.Experience : model.Experience.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Field  = model.Field == null ? model.Field : model.Field.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Initial_goal  = model.Initial_goal == null ? model.Initial_goal : model.Initial_goal.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Job_origin  = model.Job_origin == null ? model.Job_origin : model.Job_origin.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Long_term_risks  = model.Long_term_risks == null ? model.Long_term_risks : model.Long_term_risks.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notable_figures  = model.Notable_figures == null ? model.Notable_figures : model.Notable_figures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Occupational_hazards  = model.Occupational_hazards == null ? model.Occupational_hazards : model.Occupational_hazards.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_Notes  = model.Private_Notes == null ? model.Private_Notes : model.Private_Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Promotions  = model.Promotions == null ? model.Promotions : model.Promotions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Ranks  = model.Ranks == null ? model.Ranks : model.Ranks.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Similar_jobs  = model.Similar_jobs == null ? model.Similar_jobs : model.Similar_jobs.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Specializations  = model.Specializations == null ? model.Specializations : model.Specializations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Time_off  = model.Time_off == null ? model.Time_off : model.Time_off.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Traditions  = model.Traditions == null ? model.Traditions : model.Traditions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Training  = model.Training == null ? model.Training : model.Training.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Type_of_job  = model.Type_of_job == null ? model.Type_of_job : model.Type_of_job.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Vehicles  = model.Vehicles == null ? model.Vehicles : model.Vehicles.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
			}

		}

		#region Save Properties Methods
		[HttpPost]
		[Route("{id}/SaveAlternate_names")]
		public IActionResult SaveAlternate_names(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Alternate_names";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveDescription")]
		public IActionResult SaveDescription(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Description";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEducation")]
		public IActionResult SaveEducation(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Education";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveExperience")]
		public IActionResult SaveExperience(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Experience";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveField")]
		public IActionResult SaveField(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Field";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveInitial_goal")]
		public IActionResult SaveInitial_goal(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Initial_goal";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveJob_origin")]
		public IActionResult SaveJob_origin(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Job_origin";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveLong_term_risks")]
		public IActionResult SaveLong_term_risks(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Long_term_risks";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveName")]
		public IActionResult SaveName()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Name";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var JobID = Convert.ToInt64(obj["JobID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNotable_figures")]
		public IActionResult SaveNotable_figures(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Notable_figures";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNotes")]
		public IActionResult SaveNotes(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Notes";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOccupational_hazards")]
		public IActionResult SaveOccupational_hazards(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Occupational_hazards";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SavePay_rate")]
		public IActionResult SavePay_rate()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Pay_rate";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var JobID = Convert.ToInt64(obj["JobID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePrivate_Notes")]
		public IActionResult SavePrivate_Notes(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Private_Notes";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePromotions")]
		public IActionResult SavePromotions(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Promotions";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRanks")]
		public IActionResult SaveRanks(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Ranks";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSimilar_jobs")]
		public IActionResult SaveSimilar_jobs(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Similar_jobs";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSpecializations")]
		public IActionResult SaveSpecializations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Specializations";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTags")]
		public IActionResult SaveTags(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Tags";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTime_off")]
		public IActionResult SaveTime_off(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Time_off";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTraditions")]
		public IActionResult SaveTraditions(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Traditions";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTraining")]
		public IActionResult SaveTraining(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Training";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveType_of_job")]
		public IActionResult SaveType_of_job(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Type_of_job";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveUniverse")]
		public IActionResult SaveUniverse()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Universe";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var JobID = Convert.ToInt64(obj["JobID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveVehicles")]
		public IActionResult SaveVehicles(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Vehicles";
			var JobID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveWork_hours")]
		public IActionResult SaveWork_hours()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Work_hours";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var JobID = Convert.ToInt64(obj["JobID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			JobsModel model = new JobsModel();
			model.id = JobID;
			model._id = JobID;
			model.column_type = type;
			model.column_value = value;
			response = _iJobsApiService.SaveJob(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("JobsID");
			
			var rq_files = Request.Form.Files;
			
			if (rq_files != null)
			{
			    foreach (var file in rq_files)
			    {
			        using (var ms = new MemoryStream())
			        {
			            ContentObjectModel model = new ContentObjectModel();
			            model.object_type = file.ContentType;
			            model.object_name = file.FileName;
			            model.object_size = file.Length;
			
			            file.CopyTo(ms);
			            model.file = ms;
			            model.file.Seek(0, 0);
			            _iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			            var response = _iObjectBucketApiService.UploadObject(model).Result;
			
			            if (!string.IsNullOrEmpty(response.Value))
			            {
			                ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			                contentObjectAttachmentModel.object_id = Convert.ToInt64(response.Value);
			                contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			                contentObjectAttachmentModel.content_type = "jobs";
			
			                _iObjectBucketApiService.AddContentObjectAttachment(contentObjectAttachmentModel);
			            }
			        }
			    }
			}
			
			return Ok();

		}

		[Route("DeleteAttachment")]
		public IActionResult DeleteAttachment(long objectId,string objectName)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("JobsID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "jobs";
			
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewJobs", "jobs", new { id = content_Id }, "Gallery_panel");

		}

	}
}
