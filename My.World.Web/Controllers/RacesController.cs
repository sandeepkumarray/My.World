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
	[Route("races")]
	public class RacesController : Controller
	{
		public readonly IRacesApiService _iRacesApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public RacesController(IRacesApiService iRacesApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iRacesApiService = iRacesApiService;
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
			var races = _iRacesApiService.GetAllRaces(accountID);
			races.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Races.png");
			    }
			});
			return View(races);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewRaces(string Id)
		{
			RacesModel model = new RacesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["RaceID"] = Id;
			ViewData["RaceID"] = Id;
			HttpContext.Session.SetString("RaceID", Id);
			
			RacesViewModel racesViewModel = new RacesViewModel(_iObjectBucketApiService);
			racesViewModel.racesModel = _iRacesApiService.GetRaces(model);
			if (racesViewModel.racesModel == null)
				return RedirectToAction("Index", "NotFound");
			racesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			racesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "races");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Races" });
			racesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			racesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			racesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "races");
			racesViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = racesViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			racesViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(racesViewModel); 

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewRaces(string Id)
		{
			RacesModel model = new RacesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["RaceID"] = Id;
			ViewData["RaceID"] = Id;
			HttpContext.Session.SetString("RaceID", Id);
			
			RacesViewModel racesViewModel = new RacesViewModel(_iObjectBucketApiService);
			racesViewModel.racesModel = _iRacesApiService.GetRaces(model);
			if (racesViewModel.racesModel == null)
			return RedirectToAction("Index", "NotFound");
			racesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(racesViewModel.racesModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			racesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "races");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Races" });
			racesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			racesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			racesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "races");
			return View(racesViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteRaces(string Id)
		{
			RacesModel model = new RacesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iRacesApiService.DeleteRaces(model);
			return RedirectToAction("Index");

		}

		public void TransformData(RacesModel model)
		{
			if (model != null)
			{
				
				model.Other_Names  = model.Other_Names == null ? model.Other_Names : model.Other_Names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notable_features  = model.Notable_features == null ? model.Notable_features : model.Notable_features.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Physical_variance  = model.Physical_variance == null ? model.Physical_variance : model.Physical_variance.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Typical_clothing  = model.Typical_clothing == null ? model.Typical_clothing : model.Typical_clothing.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Skin_colors  = model.Skin_colors == null ? model.Skin_colors : model.Skin_colors.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Weaknesses  = model.Weaknesses == null ? model.Weaknesses : model.Weaknesses.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Conditions  = model.Conditions == null ? model.Conditions : model.Conditions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Strengths  = model.Strengths == null ? model.Strengths : model.Strengths.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Favorite_foods  = model.Favorite_foods == null ? model.Favorite_foods : model.Favorite_foods.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Famous_figures  = model.Famous_figures == null ? model.Famous_figures : model.Famous_figures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Traditions  = model.Traditions == null ? model.Traditions : model.Traditions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Beliefs  = model.Beliefs == null ? model.Beliefs : model.Beliefs.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Governments  = model.Governments == null ? model.Governments : model.Governments.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Technologies  = model.Technologies == null ? model.Technologies : model.Technologies.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Occupations  = model.Occupations == null ? model.Occupations : model.Occupations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Economics  = model.Economics == null ? model.Economics : model.Economics.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notable_events  = model.Notable_events == null ? model.Notable_events : model.Notable_events.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_notes  = model.Private_notes == null ? model.Private_notes : model.Private_notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
			}

		}

		#region Save Properties Methods
		[HttpPost]
		[Route("{id}/SaveOther_Names")]
		public IActionResult SaveOther_Names(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Other_Names";
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
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
			var RaceID = Convert.ToInt64(obj["RaceID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
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
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
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
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
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
			var RaceID = Convert.ToInt64(obj["RaceID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveGeneral_weight")]
		public IActionResult SaveGeneral_weight()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "General_weight";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var RaceID = Convert.ToInt64(obj["RaceID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNotable_features")]
		public IActionResult SaveNotable_features(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Notable_features";
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePhysical_variance")]
		public IActionResult SavePhysical_variance(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Physical_variance";
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTypical_clothing")]
		public IActionResult SaveTypical_clothing(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Typical_clothing";
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveBody_shape")]
		public IActionResult SaveBody_shape()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Body_shape";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var RaceID = Convert.ToInt64(obj["RaceID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSkin_colors")]
		public IActionResult SaveSkin_colors(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Skin_colors";
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveGeneral_height")]
		public IActionResult SaveGeneral_height()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "General_height";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var RaceID = Convert.ToInt64(obj["RaceID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveWeaknesses")]
		public IActionResult SaveWeaknesses(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Weaknesses";
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveConditions")]
		public IActionResult SaveConditions(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Conditions";
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveStrengths")]
		public IActionResult SaveStrengths(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Strengths";
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFavorite_foods")]
		public IActionResult SaveFavorite_foods(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Favorite_foods";
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFamous_figures")]
		public IActionResult SaveFamous_figures(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Famous_figures";
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
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
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveBeliefs")]
		public IActionResult SaveBeliefs(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Beliefs";
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGovernments")]
		public IActionResult SaveGovernments(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Governments";
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
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
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOccupations")]
		public IActionResult SaveOccupations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Occupations";
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEconomics")]
		public IActionResult SaveEconomics(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Economics";
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNotable_events")]
		public IActionResult SaveNotable_events(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Notable_events";
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
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
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
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
			var RaceID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			RacesModel model = new RacesModel();
			model.id = RaceID;
			model._id = RaceID;
			model.column_type = type;
			model.column_value = value;
			response = _iRacesApiService.SaveRace(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("RaceID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "races");
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
								contentObjectAttachmentModel.content_type = "races";
			
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
			string content_Id = HttpContext.Session.GetString("RaceID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "races";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewRaces", "races", new { id = content_Id }, "Gallery_panel");

		}

	}
}
