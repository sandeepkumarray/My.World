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
	[Route("locations")]
	public class LocationsController : Controller
	{
		public readonly ILocationsApiService _iLocationsApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public LocationsController(ILocationsApiService iLocationsApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iLocationsApiService = iLocationsApiService;
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
			var locations = _iLocationsApiService.GetAllLocations(accountID);
			locations.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Locations.png");
			    }
			});
			return View(locations);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewLocations(string Id)
		{
			LocationsModel model = new LocationsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["LocationID"] = Id;
			ViewData["LocationID"] = Id;
			HttpContext.Session.SetString("LocationID", Id);
			
			LocationsViewModel locationsViewModel = new LocationsViewModel(_iObjectBucketApiService);
			locationsViewModel.locationsModel = _iLocationsApiService.GetLocations(model);
			if (locationsViewModel.locationsModel == null)
				return RedirectToAction("Index", "NotFound");
			locationsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			locationsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "locations");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Locations" });
			locationsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			locationsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			locationsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "locations");
			locationsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = locationsViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			locationsViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(locationsViewModel); 

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewLocations(string Id)
		{
			LocationsModel model = new LocationsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["LocationID"] = Id;
			ViewData["LocationID"] = Id;
			HttpContext.Session.SetString("LocationID", Id);
			
			LocationsViewModel locationsViewModel = new LocationsViewModel(_iObjectBucketApiService);
			locationsViewModel.locationsModel = _iLocationsApiService.GetLocations(model);
			if (locationsViewModel.locationsModel == null)
			return RedirectToAction("Index", "NotFound");
			locationsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(locationsViewModel.locationsModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			locationsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "locations");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Locations" });
			locationsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			locationsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			locationsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "locations");
			return View(locationsViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteLocations(string Id)
		{
			LocationsModel model = new LocationsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iLocationsApiService.DeleteLocations(model);
			return RedirectToAction("Index");

		}

		public void TransformData(LocationsModel model)
		{
			if (model != null)
			{
				
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Type  = model.Type == null ? model.Type : model.Type.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Leaders  = model.Leaders == null ? model.Leaders : model.Leaders.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Language  = model.Language == null ? model.Language : model.Language.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Currency  = model.Currency == null ? model.Currency : model.Currency.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Motto  = model.Motto == null ? model.Motto : model.Motto.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Sports  = model.Sports == null ? model.Sports : model.Sports.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Laws  = model.Laws == null ? model.Laws : model.Laws.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Spoken_Languages  = model.Spoken_Languages == null ? model.Spoken_Languages : model.Spoken_Languages.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Largest_cities  = model.Largest_cities == null ? model.Largest_cities : model.Largest_cities.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notable_cities  = model.Notable_cities == null ? model.Notable_cities : model.Notable_cities.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Capital_cities  = model.Capital_cities == null ? model.Capital_cities : model.Capital_cities.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Landmarks  = model.Landmarks == null ? model.Landmarks : model.Landmarks.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Crops  = model.Crops == null ? model.Crops : model.Crops.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Located_at  = model.Located_at == null ? model.Located_at : model.Located_at.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Climate  = model.Climate == null ? model.Climate : model.Climate.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notable_Wars  = model.Notable_Wars == null ? model.Notable_Wars : model.Notable_Wars.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Founding_Story  = model.Founding_Story == null ? model.Founding_Story : model.Founding_Story.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_Notes  = model.Private_Notes == null ? model.Private_Notes : model.Private_Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
			}

		}

		#region Save Properties Methods
		[HttpPost]
		[Route("{id}/SaveDescription")]
		public IActionResult SaveDescription(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Description";
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
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
			var LocationID = Convert.ToInt64(obj["LocationID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
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
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
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
			var LocationID = Convert.ToInt64(obj["LocationID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveType")]
		public IActionResult SaveType(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Type";
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
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
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveLanguage")]
		public IActionResult SaveLanguage(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Language";
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
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
			var LocationID = Convert.ToInt64(obj["LocationID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
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
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMotto")]
		public IActionResult SaveMotto(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Motto";
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
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
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
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
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSpoken_Languages")]
		public IActionResult SaveSpoken_Languages(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Spoken_Languages";
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveLargest_cities")]
		public IActionResult SaveLargest_cities(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Largest_cities";
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNotable_cities")]
		public IActionResult SaveNotable_cities(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Notable_cities";
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCapital_cities")]
		public IActionResult SaveCapital_cities(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Capital_cities";
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
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
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
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
			var LocationID = Convert.ToInt64(obj["LocationID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
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
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveLocated_at")]
		public IActionResult SaveLocated_at(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Located_at";
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
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
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
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
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
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
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveEstablished_Year")]
		public IActionResult SaveEstablished_Year()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Established_Year";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var LocationID = Convert.ToInt64(obj["LocationID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
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
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
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
			var LocationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LocationsModel model = new LocationsModel();
			model.id = LocationID;
			model._id = LocationID;
			model.column_type = type;
			model.column_value = value;
			response = _iLocationsApiService.SaveLocation(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("LocationID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "locations");
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
								contentObjectAttachmentModel.content_type = "locations";
			
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
			string content_Id = HttpContext.Session.GetString("LocationID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "locations";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewLocations", "locations", new { id = content_Id }, "Gallery_panel");

		}

	}
}
