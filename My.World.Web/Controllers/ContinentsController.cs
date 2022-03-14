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
	[Route("continents")]
	public class ContinentsController : Controller
	{
		public readonly IContinentsApiService _iContinentsApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public ContinentsController(IContinentsApiService iContinentsApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iContinentsApiService = iContinentsApiService;
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
			var continents = _iContinentsApiService.GetAllContinents(accountID);
			continents.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Continents.png");
			    }
			});
			return View(continents);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewContinents(string Id)
		{
			ContinentsModel model = new ContinentsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["ContinentID"] = Id;
			ViewData["ContinentID"] = Id;
			HttpContext.Session.SetString("ContinentID", Id);
			
			ContinentsViewModel continentsViewModel = new ContinentsViewModel(_iObjectBucketApiService);
			continentsViewModel.continentsModel = _iContinentsApiService.GetContinents(model);
			if (continentsViewModel.continentsModel == null)
				return RedirectToAction("Index", "NotFound");
			continentsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			continentsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "continents");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Continents" });
			continentsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			continentsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			continentsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "continents");
			continentsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = continentsViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			continentsViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(continentsViewModel); 

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewContinents(string Id)
		{
			ContinentsModel model = new ContinentsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["ContinentID"] = Id;
			ViewData["ContinentID"] = Id;
			HttpContext.Session.SetString("ContinentID", Id);
			
			ContinentsViewModel continentsViewModel = new ContinentsViewModel(_iObjectBucketApiService);
			continentsViewModel.continentsModel = _iContinentsApiService.GetContinents(model);
			if (continentsViewModel.continentsModel == null)
			return RedirectToAction("Index", "NotFound");
			continentsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(continentsViewModel.continentsModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			continentsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "continents");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Continents" });
			continentsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			continentsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			continentsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "continents");
			return View(continentsViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteContinents(string Id)
		{
			ContinentsModel model = new ContinentsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iContinentsApiService.DeleteContinents(model);
			return RedirectToAction("Index");

		}

		public void TransformData(ContinentsModel model)
		{
			if (model != null)
			{
				
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Other_Names  = model.Other_Names == null ? model.Other_Names : model.Other_Names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Local_name  = model.Local_name == null ? model.Local_name : model.Local_name.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Regional_disadvantages  = model.Regional_disadvantages == null ? model.Regional_disadvantages : model.Regional_disadvantages.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Regional_advantages  = model.Regional_advantages == null ? model.Regional_advantages : model.Regional_advantages.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Landmarks  = model.Landmarks == null ? model.Landmarks : model.Landmarks.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Bodies_of_water  = model.Bodies_of_water == null ? model.Bodies_of_water : model.Bodies_of_water.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Mineralogy  = model.Mineralogy == null ? model.Mineralogy : model.Mineralogy.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Topography  = model.Topography == null ? model.Topography : model.Topography.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Population  = model.Population == null ? model.Population : model.Population.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Shape  = model.Shape == null ? model.Shape : model.Shape.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Popular_foods  = model.Popular_foods == null ? model.Popular_foods : model.Popular_foods.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Governments  = model.Governments == null ? model.Governments : model.Governments.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Traditions  = model.Traditions == null ? model.Traditions : model.Traditions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Languages  = model.Languages == null ? model.Languages : model.Languages.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Countries  = model.Countries == null ? model.Countries : model.Countries.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Reputation  = model.Reputation == null ? model.Reputation : model.Reputation.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Architecture  = model.Architecture == null ? model.Architecture : model.Architecture.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tourism  = model.Tourism == null ? model.Tourism : model.Tourism.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Economy  = model.Economy == null ? model.Economy : model.Economy.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Politics  = model.Politics == null ? model.Politics : model.Politics.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Demonym  = model.Demonym == null ? model.Demonym : model.Demonym.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Floras  = model.Floras == null ? model.Floras : model.Floras.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Creatures  = model.Creatures == null ? model.Creatures : model.Creatures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Crops  = model.Crops == null ? model.Crops : model.Crops.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Natural_disasters  = model.Natural_disasters == null ? model.Natural_disasters : model.Natural_disasters.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Winds  = model.Winds == null ? model.Winds : model.Winds.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Precipitation  = model.Precipitation == null ? model.Precipitation : model.Precipitation.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Humidity  = model.Humidity == null ? model.Humidity : model.Humidity.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Seasons  = model.Seasons == null ? model.Seasons : model.Seasons.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Temperature  = model.Temperature == null ? model.Temperature : model.Temperature.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Ruins  = model.Ruins == null ? model.Ruins : model.Ruins.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Wars  = model.Wars == null ? model.Wars : model.Wars.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Discovery  = model.Discovery == null ? model.Discovery : model.Discovery.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Formation  = model.Formation == null ? model.Formation : model.Formation.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_Notes  = model.Private_Notes == null ? model.Private_Notes : model.Private_Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
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
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
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
			var ContinentID = Convert.ToInt64(obj["ContinentID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
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
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
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
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveLocal_name")]
		public IActionResult SaveLocal_name(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Local_name";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRegional_disadvantages")]
		public IActionResult SaveRegional_disadvantages(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Regional_disadvantages";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRegional_advantages")]
		public IActionResult SaveRegional_advantages(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Regional_advantages";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
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
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveBodies_of_water")]
		public IActionResult SaveBodies_of_water(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Bodies_of_water";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMineralogy")]
		public IActionResult SaveMineralogy(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Mineralogy";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTopography")]
		public IActionResult SaveTopography(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Topography";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePopulation")]
		public IActionResult SavePopulation(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Population";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveShape")]
		public IActionResult SaveShape(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Shape";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveArea")]
		public IActionResult SaveArea()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Area";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var ContinentID = Convert.ToInt64(obj["ContinentID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePopular_foods")]
		public IActionResult SavePopular_foods(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Popular_foods";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
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
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
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
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
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
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCountries")]
		public IActionResult SaveCountries(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Countries";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveReputation")]
		public IActionResult SaveReputation(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Reputation";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveArchitecture")]
		public IActionResult SaveArchitecture(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Architecture";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTourism")]
		public IActionResult SaveTourism(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Tourism";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEconomy")]
		public IActionResult SaveEconomy(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Economy";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
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
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveDemonym")]
		public IActionResult SaveDemonym(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Demonym";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFloras")]
		public IActionResult SaveFloras(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Floras";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
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
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCrops")]
		public IActionResult SaveCrops(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Crops";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNatural_disasters")]
		public IActionResult SaveNatural_disasters(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Natural_disasters";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveWinds")]
		public IActionResult SaveWinds(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Winds";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePrecipitation")]
		public IActionResult SavePrecipitation(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Precipitation";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveHumidity")]
		public IActionResult SaveHumidity(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Humidity";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSeasons")]
		public IActionResult SaveSeasons(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Seasons";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTemperature")]
		public IActionResult SaveTemperature(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Temperature";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRuins")]
		public IActionResult SaveRuins(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Ruins";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveWars")]
		public IActionResult SaveWars(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Wars";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveDiscovery")]
		public IActionResult SaveDiscovery(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Discovery";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFormation")]
		public IActionResult SaveFormation(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Formation";
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
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
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
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
			var ContinentID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ContinentsModel model = new ContinentsModel();
			model.id = ContinentID;
			model._id = ContinentID;
			model.column_type = type;
			model.column_value = value;
			response = _iContinentsApiService.SaveContinent(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("ContinentID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "continents");
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
								contentObjectAttachmentModel.content_type = "continents";
			
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
			string content_Id = HttpContext.Session.GetString("ContinentID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "continents";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewContinents", "continents", new { id = content_Id }, "Gallery_panel");

		}

	}
}
