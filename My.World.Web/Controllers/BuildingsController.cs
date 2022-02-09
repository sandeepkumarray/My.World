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
	[Route("buildings")]
	public class BuildingsController : Controller
	{
		public readonly IBuildingsApiService _iBuildingsApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public BuildingsController(IBuildingsApiService iBuildingsApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iBuildingsApiService = iBuildingsApiService;
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
			var buildings = _iBuildingsApiService.GetAllBuildings(accountID);
			buildings.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Buildings.png");
			    }
			});
			return View(buildings);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewBuildings(string Id)
		{
			BuildingsModel model = new BuildingsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["BuildingID"] = Id;
			ViewData["BuildingID"] = Id;
			HttpContext.Session.SetString("BuildingID", Id);
			
			BuildingsViewModel buildingsViewModel = new BuildingsViewModel(_iObjectBucketApiService);
			buildingsViewModel.buildingsModel = _iBuildingsApiService.GetBuildings(model);
			if (buildingsViewModel.buildingsModel == null)
				return RedirectToAction("Index", "NotFound");
			buildingsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			buildingsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "buildings");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Buildings" });
			buildingsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			buildingsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			buildingsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "buildings");
			return View(buildingsViewModel);

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewBuildings(string Id)
		{
			BuildingsModel model = new BuildingsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["BuildingID"] = Id;
			ViewData["BuildingID"] = Id;
			HttpContext.Session.SetString("BuildingID", Id);
			
			BuildingsViewModel buildingsViewModel = new BuildingsViewModel(_iObjectBucketApiService);
			buildingsViewModel.buildingsModel = _iBuildingsApiService.GetBuildings(model);
			if (buildingsViewModel.buildingsModel == null)
			return RedirectToAction("Index", "NotFound");
			buildingsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(buildingsViewModel.buildingsModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			buildingsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "buildings");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Buildings" });
			buildingsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			buildingsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			buildingsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "buildings");
			return View(buildingsViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteBuildings(string Id)
		{
			BuildingsModel model = new BuildingsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iBuildingsApiService.DeleteBuildings(model);
			return RedirectToAction("Index");

		}

		public void TransformData(BuildingsModel model)
		{
			if (model != null)
			{
				
				model.Address  = model.Address == null ? model.Address : model.Address.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Affiliation  = model.Affiliation == null ? model.Affiliation : model.Affiliation.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Alternate_names  = model.Alternate_names == null ? model.Alternate_names : model.Alternate_names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Architect  = model.Architect == null ? model.Architect : model.Architect.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Architectural_style  = model.Architectural_style == null ? model.Architectural_style : model.Architectural_style.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Developer  = model.Developer == null ? model.Developer : model.Developer.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Facade  = model.Facade == null ? model.Facade : model.Facade.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notable_events  = model.Notable_events == null ? model.Notable_events : model.Notable_events.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Owner  = model.Owner == null ? model.Owner : model.Owner.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Permits  = model.Permits == null ? model.Permits : model.Permits.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_Notes  = model.Private_Notes == null ? model.Private_Notes : model.Private_Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Purpose  = model.Purpose == null ? model.Purpose : model.Purpose.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tenants  = model.Tenants == null ? model.Tenants : model.Tenants.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Type_of_building  = model.Type_of_building == null ? model.Type_of_building : model.Type_of_building.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
			}

		}

		#region Save Properties Methods
		[HttpPost]
		[Route("{id}/SaveAddress")]
		public IActionResult SaveAddress(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Address";
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveAffiliation")]
		public IActionResult SaveAffiliation(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Affiliation";
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
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
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveArchitect")]
		public IActionResult SaveArchitect(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Architect";
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveArchitectural_style")]
		public IActionResult SaveArchitectural_style(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Architectural_style";
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveCapacity")]
		public IActionResult SaveCapacity()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Capacity";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var BuildingID = Convert.ToInt64(obj["BuildingID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveConstructed_year")]
		public IActionResult SaveConstructed_year()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Constructed_year";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var BuildingID = Convert.ToInt64(obj["BuildingID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveConstruction_cost")]
		public IActionResult SaveConstruction_cost()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Construction_cost";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var BuildingID = Convert.ToInt64(obj["BuildingID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
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
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveDeveloper")]
		public IActionResult SaveDeveloper(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Developer";
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
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
			var BuildingID = Convert.ToInt64(obj["BuildingID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFacade")]
		public IActionResult SaveFacade(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Facade";
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveFloor_count")]
		public IActionResult SaveFloor_count()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Floor_count";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var BuildingID = Convert.ToInt64(obj["BuildingID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
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
			var BuildingID = Convert.ToInt64(obj["BuildingID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
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
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
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
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
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
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePermits")]
		public IActionResult SavePermits(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Permits";
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SavePrice")]
		public IActionResult SavePrice()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Price";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var BuildingID = Convert.ToInt64(obj["BuildingID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
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
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePurpose")]
		public IActionResult SavePurpose(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Purpose";
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
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
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTenants")]
		public IActionResult SaveTenants(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Tenants";
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveType_of_building")]
		public IActionResult SaveType_of_building(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Type_of_building";
			var BuildingID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
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
			var BuildingID = Convert.ToInt64(obj["BuildingID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			BuildingsModel model = new BuildingsModel();
			model.id = BuildingID;
			model._id = BuildingID;
			model.column_type = type;
			model.column_value = value;
			response = _iBuildingsApiService.SaveBuilding(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("BuildingsID");
			
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
			                contentObjectAttachmentModel.content_type = "buildings";
			
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
			string content_Id = HttpContext.Session.GetString("BuildingsID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "buildings";
			
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewBuildings", "buildings", new { id = content_Id }, "Gallery_panel");

		}

	}
}
