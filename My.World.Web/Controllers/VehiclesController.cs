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
	[Route("vehicles")]
	public class VehiclesController : Controller
	{
		public readonly IVehiclesApiService _iVehiclesApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public VehiclesController(IVehiclesApiService iVehiclesApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iVehiclesApiService = iVehiclesApiService;
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
			var vehicles = _iVehiclesApiService.GetAllVehicles(accountID);
			vehicles.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Vehicles.png");
			    }
			});
			return View(vehicles);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewVehicles(string Id)
		{
			VehiclesModel model = new VehiclesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["VehicleID"] = Id;
			ViewData["VehicleID"] = Id;
			HttpContext.Session.SetString("VehicleID", Id);
			
			VehiclesViewModel vehiclesViewModel = new VehiclesViewModel(_iObjectBucketApiService);
			vehiclesViewModel.vehiclesModel = _iVehiclesApiService.GetVehicles(model);
			if (vehiclesViewModel.vehiclesModel == null)
				return RedirectToAction("Index", "NotFound");
			vehiclesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			vehiclesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "vehicles");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Vehicles" });
			vehiclesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			vehiclesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			vehiclesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "vehicles");
			vehiclesViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = vehiclesViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			vehiclesViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(vehiclesViewModel); 

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewVehicles(string Id)
		{
			VehiclesModel model = new VehiclesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["VehicleID"] = Id;
			ViewData["VehicleID"] = Id;
			HttpContext.Session.SetString("VehicleID", Id);
			
			VehiclesViewModel vehiclesViewModel = new VehiclesViewModel(_iObjectBucketApiService);
			vehiclesViewModel.vehiclesModel = _iVehiclesApiService.GetVehicles(model);
			if (vehiclesViewModel.vehiclesModel == null)
			return RedirectToAction("Index", "NotFound");
			vehiclesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(vehiclesViewModel.vehiclesModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			vehiclesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "vehicles");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Vehicles" });
			vehiclesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			vehiclesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			vehiclesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "vehicles");
			return View(vehiclesViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteVehicles(string Id)
		{
			VehiclesModel model = new VehiclesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iVehiclesApiService.DeleteVehicles(model);
			return RedirectToAction("Index");

		}

		public void TransformData(VehiclesModel model)
		{
			if (model != null)
			{
				
				model.Type_of_vehicle  = model.Type_of_vehicle == null ? model.Type_of_vehicle : model.Type_of_vehicle.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Alternate_names  = model.Alternate_names == null ? model.Alternate_names : model.Alternate_names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Materials  = model.Materials == null ? model.Materials : model.Materials.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Designer  = model.Designer == null ? model.Designer : model.Designer.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Colors  = model.Colors == null ? model.Colors : model.Colors.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Distance  = model.Distance == null ? model.Distance : model.Distance.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Features  = model.Features == null ? model.Features : model.Features.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Safety  = model.Safety == null ? model.Safety : model.Safety.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Variants  = model.Variants == null ? model.Variants : model.Variants.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Manufacturer  = model.Manufacturer == null ? model.Manufacturer : model.Manufacturer.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Owner  = model.Owner == null ? model.Owner : model.Owner.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
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
			var VehicleID = Convert.ToInt64(obj["VehicleID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveType_of_vehicle")]
		public IActionResult SaveType_of_vehicle(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Type_of_vehicle";
			var VehicleID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveAlternate_names")]
		public IActionResult SaveAlternate_names(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Alternate_names";
			var VehicleID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
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
			var VehicleID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
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
			var VehicleID = Convert.ToInt64(obj["VehicleID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
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
			var VehicleID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveDimensions")]
		public IActionResult SaveDimensions()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Dimensions";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var VehicleID = Convert.ToInt64(obj["VehicleID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveSize")]
		public IActionResult SaveSize()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Size";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var VehicleID = Convert.ToInt64(obj["VehicleID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveDoors")]
		public IActionResult SaveDoors()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Doors";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var VehicleID = Convert.ToInt64(obj["VehicleID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMaterials")]
		public IActionResult SaveMaterials(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Materials";
			var VehicleID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveDesigner")]
		public IActionResult SaveDesigner(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Designer";
			var VehicleID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveWindows")]
		public IActionResult SaveWindows()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Windows";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var VehicleID = Convert.ToInt64(obj["VehicleID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveColors")]
		public IActionResult SaveColors(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Colors";
			var VehicleID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveDistance")]
		public IActionResult SaveDistance(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Distance";
			var VehicleID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFeatures")]
		public IActionResult SaveFeatures(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Features";
			var VehicleID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSafety")]
		public IActionResult SaveSafety(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Safety";
			var VehicleID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveFuel")]
		public IActionResult SaveFuel()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Fuel";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var VehicleID = Convert.ToInt64(obj["VehicleID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveSpeed")]
		public IActionResult SaveSpeed()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Speed";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var VehicleID = Convert.ToInt64(obj["VehicleID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveVariants")]
		public IActionResult SaveVariants(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Variants";
			var VehicleID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveManufacturer")]
		public IActionResult SaveManufacturer(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Manufacturer";
			var VehicleID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveCosts")]
		public IActionResult SaveCosts()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Costs";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var VehicleID = Convert.ToInt64(obj["VehicleID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveWeight")]
		public IActionResult SaveWeight()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Weight";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var VehicleID = Convert.ToInt64(obj["VehicleID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
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
			var VehicleID = Convert.ToInt64(obj["VehicleID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOwner")]
		public IActionResult SaveOwner(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Owner";
			var VehicleID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
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
			var VehicleID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
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
			var VehicleID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			VehiclesModel model = new VehiclesModel();
			model.id = VehicleID;
			model._id = VehicleID;
			model.column_type = type;
			model.column_value = value;
			response = _iVehiclesApiService.SaveVehicle(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("VehicleID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "vehicles");
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
								contentObjectAttachmentModel.content_type = "vehicles";
			
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
			string content_Id = HttpContext.Session.GetString("VehicleID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "vehicles";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewVehicles", "vehicles", new { id = content_Id }, "Gallery_panel");

		}

	}
}
