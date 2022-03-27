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
	[Route("towns")]
	public class TownsController : Controller
	{
		public readonly ITownsApiService _iTownsApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContentTypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public TownsController(ITownsApiService iTownsApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContentTypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
_iTownsApiService = iTownsApiService;
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
			var towns = _iTownsApiService.GetAllTowns(accountID);
			towns.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Towns.png");
			    }
			});
			return View(towns);

		}

		[HttpGet]
		[Route("{Id}/edit")]
		public IActionResult ViewTowns(string Id)
		{
			TownsModel model = new TownsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["TownID"] = Id;
			ViewData["TownID"] = Id;
			HttpContext.Session.SetString("TownID", Id);
			
			TownsViewModel townsViewModel = new TownsViewModel(_iObjectBucketApiService);
			townsViewModel.townsModel = _iTownsApiService.GetTowns(model);
			if (townsViewModel.townsModel == null)
				return RedirectToAction("Index", "NotFound");
			townsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			townsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "towns");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Towns" });
			townsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			townsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			townsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "towns");
			townsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = townsViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			townsViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(townsViewModel); 

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult PreviewTowns(string Id)
		{
			TownsModel model = new TownsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["TownID"] = Id;
			ViewData["TownID"] = Id;
			HttpContext.Session.SetString("TownID", Id);
			
			TownsViewModel townsViewModel = new TownsViewModel(_iObjectBucketApiService);
			townsViewModel.townsModel = _iTownsApiService.GetTowns(model);
			if (townsViewModel.townsModel == null)
			return RedirectToAction("Index", "NotFound");
			townsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(townsViewModel.townsModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			townsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "towns");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Towns" });
			townsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			townsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			townsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "towns");
			townsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			return View(townsViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteTowns(string Id)
		{
			TownsModel model = new TownsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iTownsApiService.DeleteTowns(model);
			return RedirectToAction("Index");

		}

		private void TransformData(TownsModel model)
		{
			if (model != null)
			{
				
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Other_names  = model.Other_names == null ? model.Other_names : model.Other_names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Groups  = model.Groups == null ? model.Groups : model.Groups.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Busy_areas  = model.Busy_areas == null ? model.Busy_areas : model.Busy_areas.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Landmarks  = model.Landmarks == null ? model.Landmarks : model.Landmarks.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Laws  = model.Laws == null ? model.Laws : model.Laws.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Languages  = model.Languages == null ? model.Languages : model.Languages.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Flora  = model.Flora == null ? model.Flora : model.Flora.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Creatures  = model.Creatures == null ? model.Creatures : model.Creatures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Politics  = model.Politics == null ? model.Politics : model.Politics.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Sports  = model.Sports == null ? model.Sports : model.Sports.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Founding_story  = model.Founding_story == null ? model.Founding_story : model.Founding_story.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Food_sources  = model.Food_sources == null ? model.Food_sources : model.Food_sources.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Waste  = model.Waste == null ? model.Waste : model.Waste.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Energy_sources  = model.Energy_sources == null ? model.Energy_sources : model.Energy_sources.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Recycling  = model.Recycling == null ? model.Recycling : model.Recycling.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_Notes  = model.Private_Notes == null ? model.Private_Notes : model.Private_Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
			}

		}

		#region Save Properties Methods
		[HttpPost]
		[Route("SaveUniverse")]
		public IActionResult SaveUniverse()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Universe";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var TownID = Convert.ToInt64(obj["TownID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
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
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
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
			var TownID = Convert.ToInt64(obj["TownID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
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
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOther_names")]
		public IActionResult SaveOther_names(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Other_names";
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveCountry")]
		public IActionResult SaveCountry()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Country";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var TownID = Convert.ToInt64(obj["TownID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
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
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveCitizens")]
		public IActionResult SaveCitizens()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Citizens";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var TownID = Convert.ToInt64(obj["TownID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveBuildings")]
		public IActionResult SaveBuildings()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Buildings";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var TownID = Convert.ToInt64(obj["TownID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveNeighborhoods")]
		public IActionResult SaveNeighborhoods()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Neighborhoods";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var TownID = Convert.ToInt64(obj["TownID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveBusy_areas")]
		public IActionResult SaveBusy_areas(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Busy_areas";
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveLandmarks")]
		public IActionResult SaveLandmarks(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Landmarks";
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
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
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveLanguages")]
		public IActionResult SaveLanguages(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Languages";
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFlora")]
		public IActionResult SaveFlora(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Flora";
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
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
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePolitics")]
		public IActionResult SavePolitics(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Politics";
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSports")]
		public IActionResult SaveSports(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Sports";
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveEstablished_year")]
		public IActionResult SaveEstablished_year()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Established_year";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var TownID = Convert.ToInt64(obj["TownID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFounding_story")]
		public IActionResult SaveFounding_story(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Founding_story";
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFood_sources")]
		public IActionResult SaveFood_sources(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Food_sources";
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveWaste")]
		public IActionResult SaveWaste(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Waste";
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEnergy_sources")]
		public IActionResult SaveEnergy_sources(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Energy_sources";
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRecycling")]
		public IActionResult SaveRecycling(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Recycling";
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
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
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
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
			var TownID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TownsModel model = new TownsModel();
			model.id = TownID;
			model._id = TownID;
			model.column_type = type;
			model.column_value = value;
			response = _iTownsApiService.SaveTown(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("TownID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "towns");
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
								contentObjectAttachmentModel.content_type = "towns";
			
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
			string content_Id = HttpContext.Session.GetString("TownID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "towns";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewTowns", "towns", new { id = content_Id }, "Gallery_panel");

		}

	}
}
