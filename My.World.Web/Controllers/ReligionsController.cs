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
	[Route("religions")]
	public class ReligionsController : Controller
	{
		public readonly IReligionsApiService _iReligionsApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public ReligionsController(IReligionsApiService iReligionsApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iReligionsApiService = iReligionsApiService;
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
			var religions = _iReligionsApiService.GetAllReligions(accountID);
			religions.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Religions.png");
			    }
			});
			return View(religions);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewReligions(string Id)
		{
			ReligionsModel model = new ReligionsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["ReligionID"] = Id;
			ViewData["ReligionID"] = Id;
			HttpContext.Session.SetString("ReligionID", Id);
			
			ReligionsViewModel religionsViewModel = new ReligionsViewModel(_iObjectBucketApiService);
			religionsViewModel.religionsModel = _iReligionsApiService.GetReligions(model);
			if (religionsViewModel.religionsModel == null)
				return RedirectToAction("Index", "NotFound");
			religionsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			religionsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "religions");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Religions" });
			religionsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			religionsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			religionsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "religions");
			religionsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = religionsViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			religionsViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(religionsViewModel); 

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewReligions(string Id)
		{
			ReligionsModel model = new ReligionsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["ReligionID"] = Id;
			ViewData["ReligionID"] = Id;
			HttpContext.Session.SetString("ReligionID", Id);
			
			ReligionsViewModel religionsViewModel = new ReligionsViewModel(_iObjectBucketApiService);
			religionsViewModel.religionsModel = _iReligionsApiService.GetReligions(model);
			if (religionsViewModel.religionsModel == null)
			return RedirectToAction("Index", "NotFound");
			religionsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(religionsViewModel.religionsModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			religionsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "religions");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Religions" });
			religionsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			religionsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			religionsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "religions");
			return View(religionsViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteReligions(string Id)
		{
			ReligionsModel model = new ReligionsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iReligionsApiService.DeleteReligions(model);
			return RedirectToAction("Index");

		}

		public void TransformData(ReligionsModel model)
		{
			if (model != null)
			{
				
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Other_Names  = model.Other_Names == null ? model.Other_Names : model.Other_Names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notable_figures  = model.Notable_figures == null ? model.Notable_figures : model.Notable_figures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Origin_story  = model.Origin_story == null ? model.Origin_story : model.Origin_story.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Artifacts  = model.Artifacts == null ? model.Artifacts : model.Artifacts.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Places_of_worship  = model.Places_of_worship == null ? model.Places_of_worship : model.Places_of_worship.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Vision_of_paradise  = model.Vision_of_paradise == null ? model.Vision_of_paradise : model.Vision_of_paradise.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Obligations  = model.Obligations == null ? model.Obligations : model.Obligations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Worship_services  = model.Worship_services == null ? model.Worship_services : model.Worship_services.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Prophecies  = model.Prophecies == null ? model.Prophecies : model.Prophecies.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Teachings  = model.Teachings == null ? model.Teachings : model.Teachings.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Deities  = model.Deities == null ? model.Deities : model.Deities.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Initiation_process  = model.Initiation_process == null ? model.Initiation_process : model.Initiation_process.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Rituals  = model.Rituals == null ? model.Rituals : model.Rituals.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Holidays  = model.Holidays == null ? model.Holidays : model.Holidays.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Traditions  = model.Traditions == null ? model.Traditions : model.Traditions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Practicing_locations  = model.Practicing_locations == null ? model.Practicing_locations : model.Practicing_locations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Practicing_races  = model.Practicing_races == null ? model.Practicing_races : model.Practicing_races.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_notes  = model.Private_notes == null ? model.Private_notes : model.Private_notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
			}

		}

		#region Save Properties Methods
		[HttpPost]
		[Route("{id}/SaveTags")]
		public IActionResult SaveTags(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Tags";
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
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
			var ReligionID = Convert.ToInt64(obj["ReligionID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
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
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOther_Names")]
		public IActionResult SaveOther_Names(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Other_Names";
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
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
			var ReligionID = Convert.ToInt64(obj["ReligionID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
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
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOrigin_story")]
		public IActionResult SaveOrigin_story(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Origin_story";
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveArtifacts")]
		public IActionResult SaveArtifacts(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Artifacts";
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePlaces_of_worship")]
		public IActionResult SavePlaces_of_worship(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Places_of_worship";
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveVision_of_paradise")]
		public IActionResult SaveVision_of_paradise(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Vision_of_paradise";
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveObligations")]
		public IActionResult SaveObligations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Obligations";
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveWorship_services")]
		public IActionResult SaveWorship_services(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Worship_services";
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveProphecies")]
		public IActionResult SaveProphecies(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Prophecies";
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTeachings")]
		public IActionResult SaveTeachings(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Teachings";
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveDeities")]
		public IActionResult SaveDeities(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Deities";
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveInitiation_process")]
		public IActionResult SaveInitiation_process(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Initiation_process";
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRituals")]
		public IActionResult SaveRituals(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Rituals";
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
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
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
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
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePracticing_locations")]
		public IActionResult SavePracticing_locations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Practicing_locations";
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePracticing_races")]
		public IActionResult SavePracticing_races(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Practicing_races";
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePrivate_notes")]
		public IActionResult SavePrivate_notes(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Private_notes";
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
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
			var ReligionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ReligionsModel model = new ReligionsModel();
			model.id = ReligionID;
			model._id = ReligionID;
			model.column_type = type;
			model.column_value = value;
			response = _iReligionsApiService.SaveReligion(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("ReligionID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "religions");
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
								contentObjectAttachmentModel.content_type = "religions";
			
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
			string content_Id = HttpContext.Session.GetString("ReligionID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "religions";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewReligions", "religions", new { id = content_Id }, "Gallery_panel");

		}

	}
}
