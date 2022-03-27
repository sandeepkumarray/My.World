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
	[Route("groups")]
	public class GroupsController : Controller
	{
		public readonly IGroupsApiService _iGroupsApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContentTypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public GroupsController(IGroupsApiService iGroupsApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContentTypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
_iGroupsApiService = iGroupsApiService;
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
			var groups = _iGroupsApiService.GetAllGroups(accountID);
			groups.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Groups.png");
			    }
			});
			return View(groups);

		}

		[HttpGet]
		[Route("{Id}/edit")]
		public IActionResult ViewGroups(string Id)
		{
			GroupsModel model = new GroupsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["GroupID"] = Id;
			ViewData["GroupID"] = Id;
			HttpContext.Session.SetString("GroupID", Id);
			
			GroupsViewModel groupsViewModel = new GroupsViewModel(_iObjectBucketApiService);
			groupsViewModel.groupsModel = _iGroupsApiService.GetGroups(model);
			if (groupsViewModel.groupsModel == null)
				return RedirectToAction("Index", "NotFound");
			groupsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			groupsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "groups");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Groups" });
			groupsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			groupsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			groupsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "groups");
			groupsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = groupsViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			groupsViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(groupsViewModel); 

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult PreviewGroups(string Id)
		{
			GroupsModel model = new GroupsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["GroupID"] = Id;
			ViewData["GroupID"] = Id;
			HttpContext.Session.SetString("GroupID", Id);
			
			GroupsViewModel groupsViewModel = new GroupsViewModel(_iObjectBucketApiService);
			groupsViewModel.groupsModel = _iGroupsApiService.GetGroups(model);
			if (groupsViewModel.groupsModel == null)
			return RedirectToAction("Index", "NotFound");
			groupsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(groupsViewModel.groupsModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			groupsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "groups");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Groups" });
			groupsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			groupsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			groupsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "groups");
			groupsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			return View(groupsViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteGroups(string Id)
		{
			GroupsModel model = new GroupsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iGroupsApiService.DeleteGroups(model);
			return RedirectToAction("Index");

		}

		private void TransformData(GroupsModel model)
		{
			if (model != null)
			{
				
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Other_Names  = model.Other_Names == null ? model.Other_Names : model.Other_Names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Subgroups  = model.Subgroups == null ? model.Subgroups : model.Subgroups.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Supergroups  = model.Supergroups == null ? model.Supergroups : model.Supergroups.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Sistergroups  = model.Sistergroups == null ? model.Sistergroups : model.Sistergroups.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Organization_structure  = model.Organization_structure == null ? model.Organization_structure : model.Organization_structure.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Leaders  = model.Leaders == null ? model.Leaders : model.Leaders.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Creatures  = model.Creatures == null ? model.Creatures : model.Creatures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Members  = model.Members == null ? model.Members : model.Members.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Offices  = model.Offices == null ? model.Offices : model.Offices.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Locations  = model.Locations == null ? model.Locations : model.Locations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Headquarters  = model.Headquarters == null ? model.Headquarters : model.Headquarters.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Motivations  = model.Motivations == null ? model.Motivations : model.Motivations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Traditions  = model.Traditions == null ? model.Traditions : model.Traditions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Risks  = model.Risks == null ? model.Risks : model.Risks.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Obstacles  = model.Obstacles == null ? model.Obstacles : model.Obstacles.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Goals  = model.Goals == null ? model.Goals : model.Goals.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Clients  = model.Clients == null ? model.Clients : model.Clients.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Allies  = model.Allies == null ? model.Allies : model.Allies.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Enemies  = model.Enemies == null ? model.Enemies : model.Enemies.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Rivals  = model.Rivals == null ? model.Rivals : model.Rivals.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Suppliers  = model.Suppliers == null ? model.Suppliers : model.Suppliers.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Inventory  = model.Inventory == null ? model.Inventory : model.Inventory.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Equipment  = model.Equipment == null ? model.Equipment : model.Equipment.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Key_items  = model.Key_items == null ? model.Key_items : model.Key_items.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_notes  = model.Private_notes == null ? model.Private_notes : model.Private_notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
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
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
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
			var GroupID = Convert.ToInt64(obj["GroupID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
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
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
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
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
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
			var GroupID = Convert.ToInt64(obj["GroupID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSubgroups")]
		public IActionResult SaveSubgroups(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Subgroups";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSupergroups")]
		public IActionResult SaveSupergroups(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Supergroups";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSistergroups")]
		public IActionResult SaveSistergroups(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Sistergroups";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOrganization_structure")]
		public IActionResult SaveOrganization_structure(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Organization_structure";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
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
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
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
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMembers")]
		public IActionResult SaveMembers(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Members";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOffices")]
		public IActionResult SaveOffices(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Offices";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
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
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveHeadquarters")]
		public IActionResult SaveHeadquarters(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Headquarters";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMotivations")]
		public IActionResult SaveMotivations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Motivations";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
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
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRisks")]
		public IActionResult SaveRisks(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Risks";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveObstacles")]
		public IActionResult SaveObstacles(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Obstacles";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGoals")]
		public IActionResult SaveGoals(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Goals";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveClients")]
		public IActionResult SaveClients(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Clients";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveAllies")]
		public IActionResult SaveAllies(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Allies";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEnemies")]
		public IActionResult SaveEnemies(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Enemies";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRivals")]
		public IActionResult SaveRivals(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Rivals";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSuppliers")]
		public IActionResult SaveSuppliers(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Suppliers";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveInventory")]
		public IActionResult SaveInventory(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Inventory";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEquipment")]
		public IActionResult SaveEquipment(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Equipment";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveKey_items")]
		public IActionResult SaveKey_items(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Key_items";
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
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
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
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
			var GroupID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			GroupsModel model = new GroupsModel();
			model.id = GroupID;
			model._id = GroupID;
			model.column_type = type;
			model.column_value = value;
			response = _iGroupsApiService.SaveGroup(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("GroupID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "groups");
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
								contentObjectAttachmentModel.content_type = "groups";
			
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
			string content_Id = HttpContext.Session.GetString("GroupID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "groups";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewGroups", "groups", new { id = content_Id }, "Gallery_panel");

		}

	}
}
