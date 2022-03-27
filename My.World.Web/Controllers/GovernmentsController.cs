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
using System.Web;

namespace My.World.Web.Controllers
{
	[Authorize]
	[Route("governments")]
	public class GovernmentsController : Controller
	{
		public readonly IGovernmentsApiService _iGovernmentsApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContentTypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public GovernmentsController(IGovernmentsApiService iGovernmentsApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContentTypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
_iGovernmentsApiService = iGovernmentsApiService;
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

		public IActionResult Index()
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			string imageFormat = HttpContext.Session.GetString("ContentImageUrlFormat");
			var governments = _iGovernmentsApiService.GetAllGovernments(accountID);
			governments.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Governments.png");
			    }
			});
			return View(governments);

		}

		[HttpGet]
		[Route("{Id}/edit")]
		public IActionResult ViewGovernments(string Id)
		{
			GovernmentsModel model = new GovernmentsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["GovernmentID"] = Id;
			ViewData["GovernmentID"] = Id;
			HttpContext.Session.SetString("GovernmentID", Id);
			
			GovernmentsViewModel governmentsViewModel = new GovernmentsViewModel(_iObjectBucketApiService);
			governmentsViewModel.governmentsModel = _iGovernmentsApiService.GetGovernments(model);
			if (governmentsViewModel.governmentsModel == null)
				return RedirectToAction("Index", "NotFound");
			governmentsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			governmentsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "governments");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Governments" });
			governmentsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			governmentsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			governmentsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "governments");
			governmentsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = governmentsViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			governmentsViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(governmentsViewModel); 

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult PreviewGovernments(string Id)
		{
			GovernmentsModel model = new GovernmentsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["GovernmentID"] = Id;
			ViewData["GovernmentID"] = Id;
			HttpContext.Session.SetString("GovernmentID", Id);
			
			GovernmentsViewModel governmentsViewModel = new GovernmentsViewModel(_iObjectBucketApiService);
			governmentsViewModel.governmentsModel = _iGovernmentsApiService.GetGovernments(model);
			if (governmentsViewModel.governmentsModel == null)
			return RedirectToAction("Index", "NotFound");
			governmentsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(governmentsViewModel.governmentsModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			governmentsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "governments");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Governments" });
			governmentsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			governmentsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			governmentsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "governments");
			governmentsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			return View(governmentsViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteGovernments(string Id)
		{
			GovernmentsModel model = new GovernmentsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iGovernmentsApiService.DeleteGovernments(model);
			return RedirectToAction("Index");

		}

		private void TransformData(GovernmentsModel model)
		{
			if (model != null)
			{
				
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Checks_And_Balances  = model.Checks_And_Balances == null ? model.Checks_And_Balances : model.Checks_And_Balances.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Jobs  = model.Jobs == null ? model.Jobs : model.Jobs.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Type_Of_Government  = model.Type_Of_Government == null ? model.Type_Of_Government : model.Type_Of_Government.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Power_Structure  = model.Power_Structure == null ? model.Power_Structure : model.Power_Structure.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Power_Source  = model.Power_Source == null ? model.Power_Source : model.Power_Source.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Privacy_Ideologies  = model.Privacy_Ideologies == null ? model.Privacy_Ideologies : model.Privacy_Ideologies.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Sociopolitical  = model.Sociopolitical == null ? model.Sociopolitical : model.Sociopolitical.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Socioeconomical  = model.Socioeconomical == null ? model.Socioeconomical : model.Socioeconomical.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Geocultural  = model.Geocultural == null ? model.Geocultural : model.Geocultural.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Laws  = model.Laws == null ? model.Laws : model.Laws.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Immigration  = model.Immigration == null ? model.Immigration : model.Immigration.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Term_Lengths  = model.Term_Lengths == null ? model.Term_Lengths : model.Term_Lengths.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Electoral_Process  = model.Electoral_Process == null ? model.Electoral_Process : model.Electoral_Process.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Criminal_System  = model.Criminal_System == null ? model.Criminal_System : model.Criminal_System.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.International_Relations  = model.International_Relations == null ? model.International_Relations : model.International_Relations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Civilian_Life  = model.Civilian_Life == null ? model.Civilian_Life : model.Civilian_Life.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Approval_Ratings  = model.Approval_Ratings == null ? model.Approval_Ratings : model.Approval_Ratings.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Space_Program  = model.Space_Program == null ? model.Space_Program : model.Space_Program.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Leaders  = model.Leaders == null ? model.Leaders : model.Leaders.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Groups  = model.Groups == null ? model.Groups : model.Groups.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Political_figures  = model.Political_figures == null ? model.Political_figures : model.Political_figures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Military  = model.Military == null ? model.Military : model.Military.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Navy  = model.Navy == null ? model.Navy : model.Navy.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Airforce  = model.Airforce == null ? model.Airforce : model.Airforce.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notable_Wars  = model.Notable_Wars == null ? model.Notable_Wars : model.Notable_Wars.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Founding_Story  = model.Founding_Story == null ? model.Founding_Story : model.Founding_Story.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Flag_Design_Story  = model.Flag_Design_Story == null ? model.Flag_Design_Story : model.Flag_Design_Story.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Holidays  = model.Holidays == null ? model.Holidays : model.Holidays.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Vehicles  = model.Vehicles == null ? model.Vehicles : model.Vehicles.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Items  = model.Items == null ? model.Items : model.Items.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Technologies  = model.Technologies == null ? model.Technologies : model.Technologies.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Creatures  = model.Creatures == null ? model.Creatures : model.Creatures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_Notes  = model.Private_Notes == null ? model.Private_Notes : model.Private_Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
			}

		}

		#region Save Properties Methods
		[HttpPost]
		[Route("SaveName")]
		public IActionResult SaveName()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Name";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var GovernmentID = Convert.ToInt64(obj["GovernmentID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
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
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
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
			var GovernmentID = Convert.ToInt64(obj["GovernmentID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
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
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveChecks_And_Balances")]
		public IActionResult SaveChecks_And_Balances(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Checks_And_Balances";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveJobs")]
		public IActionResult SaveJobs(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Jobs";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveType_Of_Government")]
		public IActionResult SaveType_Of_Government(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Type_Of_Government";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePower_Structure")]
		public IActionResult SavePower_Structure(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Power_Structure";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePower_Source")]
		public IActionResult SavePower_Source(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Power_Source";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePrivacy_Ideologies")]
		public IActionResult SavePrivacy_Ideologies(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Privacy_Ideologies";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSociopolitical")]
		public IActionResult SaveSociopolitical(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Sociopolitical";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSocioeconomical")]
		public IActionResult SaveSocioeconomical(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Socioeconomical";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGeocultural")]
		public IActionResult SaveGeocultural(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Geocultural";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveLaws")]
		public IActionResult SaveLaws(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Laws";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveImmigration")]
		public IActionResult SaveImmigration(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Immigration";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTerm_Lengths")]
		public IActionResult SaveTerm_Lengths(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Term_Lengths";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveElectoral_Process")]
		public IActionResult SaveElectoral_Process(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Electoral_Process";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCriminal_System")]
		public IActionResult SaveCriminal_System(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Criminal_System";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveInternational_Relations")]
		public IActionResult SaveInternational_Relations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "International_Relations";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCivilian_Life")]
		public IActionResult SaveCivilian_Life(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Civilian_Life";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveApproval_Ratings")]
		public IActionResult SaveApproval_Ratings(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Approval_Ratings";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSpace_Program")]
		public IActionResult SaveSpace_Program(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Space_Program";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveLeaders")]
		public IActionResult SaveLeaders(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Leaders";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGroups")]
		public IActionResult SaveGroups(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Groups";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePolitical_figures")]
		public IActionResult SavePolitical_figures(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Political_figures";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMilitary")]
		public IActionResult SaveMilitary(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Military";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNavy")]
		public IActionResult SaveNavy(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Navy";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveAirforce")]
		public IActionResult SaveAirforce(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Airforce";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNotable_Wars")]
		public IActionResult SaveNotable_Wars(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Notable_Wars";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFounding_Story")]
		public IActionResult SaveFounding_Story(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Founding_Story";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFlag_Design_Story")]
		public IActionResult SaveFlag_Design_Story(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Flag_Design_Story";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveHolidays")]
		public IActionResult SaveHolidays(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Holidays";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
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
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveItems")]
		public IActionResult SaveItems(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Items";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTechnologies")]
		public IActionResult SaveTechnologies(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Technologies";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCreatures")]
		public IActionResult SaveCreatures(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Creatures";
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
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
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
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
			var GovernmentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GovernmentsModel model = new GovernmentsModel();
			model.id = GovernmentID;
			model._id = GovernmentID;
			model.column_type = type;
			model.column_value = value;
			response = _iGovernmentsApiService.SaveGovernment(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("GovernmentID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "governments");
			var existing_total_size = ContentObjectModelList.Sum(f => f.object_size);
			
			var rq_files = Request.Form.Files;
			var upload_file_size = rq_files.Sum(f => f.Length);
			var total_size = upload_file_size + existing_total_size;
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			
			if (total_size <= AllowedTotalContentSize)
			{
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
							model.bucket_folder = _config.GetValue<string>("BucketEnv");
			
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
								contentObjectAttachmentModel.content_type = "governments";
			
								_iObjectBucketApiService.AddContentObjectAttachment(contentObjectAttachmentModel);
							}
						}
					}
				}
			}
			else
			{
				return BadRequest(new { message = "You have Exceeded the maximum allowed size of 50 MB per content to upload images." });
			}
			return Ok();

		}

		[Route("DeleteAttachment")]
		public IActionResult DeleteAttachment(long objectId,string objectName)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("GovernmentID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "governments";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewGovernments", "governments", new { id = content_Id }, "Gallery_panel");

		}

	}
}
