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
	[Route("countries")]
	public class CountriesController : Controller
	{
		public readonly ICountriesApiService _iCountriesApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public CountriesController(ICountriesApiService iCountriesApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iCountriesApiService = iCountriesApiService;
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
			var countries = _iCountriesApiService.GetAllCountries(accountID);
			countries.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Countries.png");
			    }
			});
			return View(countries);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewCountries(string Id)
		{
			CountriesModel model = new CountriesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["CountrieID"] = Id;
			ViewData["CountrieID"] = Id;
			HttpContext.Session.SetString("CountrieID", Id);
			
			CountriesViewModel countriesViewModel = new CountriesViewModel(_iObjectBucketApiService);
			countriesViewModel.countriesModel = _iCountriesApiService.GetCountries(model);
			if (countriesViewModel.countriesModel == null)
				return RedirectToAction("Index", "NotFound");
			countriesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			countriesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "countries");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Countries" });
			countriesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			countriesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			countriesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "countries");
			countriesViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = countriesViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			countriesViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(countriesViewModel); 

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewCountries(string Id)
		{
			CountriesModel model = new CountriesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["CountrieID"] = Id;
			ViewData["CountrieID"] = Id;
			HttpContext.Session.SetString("CountrieID", Id);
			
			CountriesViewModel countriesViewModel = new CountriesViewModel(_iObjectBucketApiService);
			countriesViewModel.countriesModel = _iCountriesApiService.GetCountries(model);
			if (countriesViewModel.countriesModel == null)
			return RedirectToAction("Index", "NotFound");
			countriesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(countriesViewModel.countriesModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			countriesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "countries");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Countries" });
			countriesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			countriesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			countriesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "countries");
			return View(countriesViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteCountries(string Id)
		{
			CountriesModel model = new CountriesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iCountriesApiService.DeleteCountries(model);
			return RedirectToAction("Index");

		}

		public void TransformData(CountriesModel model)
		{
			if (model != null)
			{
				
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Other_Names  = model.Other_Names == null ? model.Other_Names : model.Other_Names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Landmarks  = model.Landmarks == null ? model.Landmarks : model.Landmarks.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Locations  = model.Locations == null ? model.Locations : model.Locations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Towns  = model.Towns == null ? model.Towns : model.Towns.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Bordering_countries  = model.Bordering_countries == null ? model.Bordering_countries : model.Bordering_countries.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Education  = model.Education == null ? model.Education : model.Education.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Governments  = model.Governments == null ? model.Governments : model.Governments.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Religions  = model.Religions == null ? model.Religions : model.Religions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Languages  = model.Languages == null ? model.Languages : model.Languages.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Sports  = model.Sports == null ? model.Sports : model.Sports.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Architecture  = model.Architecture == null ? model.Architecture : model.Architecture.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Music  = model.Music == null ? model.Music : model.Music.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Pop_culture  = model.Pop_culture == null ? model.Pop_culture : model.Pop_culture.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Laws  = model.Laws == null ? model.Laws : model.Laws.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Currency  = model.Currency == null ? model.Currency : model.Currency.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Social_hierarchy  = model.Social_hierarchy == null ? model.Social_hierarchy : model.Social_hierarchy.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Crops  = model.Crops == null ? model.Crops : model.Crops.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Climate  = model.Climate == null ? model.Climate : model.Climate.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Creatures  = model.Creatures == null ? model.Creatures : model.Creatures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Flora  = model.Flora == null ? model.Flora : model.Flora.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notable_wars  = model.Notable_wars == null ? model.Notable_wars : model.Notable_wars.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Founding_story  = model.Founding_story == null ? model.Founding_story : model.Founding_story.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
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
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(obj["CountrieID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(obj["CountrieID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveLocations")]
		public IActionResult SaveLocations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Locations";
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTowns")]
		public IActionResult SaveTowns(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Towns";
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveBordering_countries")]
		public IActionResult SaveBordering_countries(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Bordering_countries";
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveReligions")]
		public IActionResult SaveReligions(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Religions";
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMusic")]
		public IActionResult SaveMusic(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Music";
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePop_culture")]
		public IActionResult SavePop_culture(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Pop_culture";
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCurrency")]
		public IActionResult SaveCurrency(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Currency";
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSocial_hierarchy")]
		public IActionResult SaveSocial_hierarchy(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Social_hierarchy";
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SavePopulation")]
		public IActionResult SavePopulation()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Population";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var CountrieID = Convert.ToInt64(obj["CountrieID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(obj["CountrieID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveClimate")]
		public IActionResult SaveClimate(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Climate";
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(obj["CountrieID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNotable_wars")]
		public IActionResult SaveNotable_wars(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Notable_wars";
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
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
			var CountrieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CountriesModel model = new CountriesModel();
			model.id = CountrieID;
			model._id = CountrieID;
			model.column_type = type;
			model.column_value = value;
			response = _iCountriesApiService.SaveCountrie(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("CountrieID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "countries");
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
								contentObjectAttachmentModel.content_type = "countries";
			
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
			string content_Id = HttpContext.Session.GetString("CountrieID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "countries";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewCountries", "countries", new { id = content_Id }, "Gallery_panel");

		}

	}
}
