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
	[Route("organizations")]
	public class OrganizationsController : Controller
	{
		public readonly IOrganizationsApiService _iOrganizationsApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContentTypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public OrganizationsController(IOrganizationsApiService iOrganizationsApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContentTypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
_iOrganizationsApiService = iOrganizationsApiService;
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
			var organizations = _iOrganizationsApiService.GetAllOrganizations(accountID);
			organizations.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Organizations.png");
			    }
			});
			return View(organizations);

		}

		[HttpGet]
		[Route("{Id}/edit")]
		public IActionResult ViewOrganizations(string Id)
		{
			OrganizationsModel model = new OrganizationsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["OrganizationID"] = Id;
			ViewData["OrganizationID"] = Id;
			HttpContext.Session.SetString("OrganizationID", Id);
			
			OrganizationsViewModel organizationsViewModel = new OrganizationsViewModel(_iObjectBucketApiService);
			organizationsViewModel.organizationsModel = _iOrganizationsApiService.GetOrganizations(model);
			if (organizationsViewModel.organizationsModel == null)
				return RedirectToAction("Index", "NotFound");
			organizationsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			organizationsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "organizations");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Organizations" });
			organizationsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			organizationsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			organizationsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "organizations");
			organizationsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = organizationsViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			organizationsViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(organizationsViewModel); 

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult PreviewOrganizations(string Id)
		{
			OrganizationsModel model = new OrganizationsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["OrganizationID"] = Id;
			ViewData["OrganizationID"] = Id;
			HttpContext.Session.SetString("OrganizationID", Id);
			
			OrganizationsViewModel organizationsViewModel = new OrganizationsViewModel(_iObjectBucketApiService);
			organizationsViewModel.organizationsModel = _iOrganizationsApiService.GetOrganizations(model);
			if (organizationsViewModel.organizationsModel == null)
			return RedirectToAction("Index", "NotFound");
			organizationsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(organizationsViewModel.organizationsModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			organizationsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "organizations");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Organizations" });
			organizationsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			organizationsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			organizationsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "organizations");
			organizationsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			return View(organizationsViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteOrganizations(string Id)
		{
			OrganizationsModel model = new OrganizationsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iOrganizationsApiService.DeleteOrganizations(model);
			return RedirectToAction("Index");

		}

		private void TransformData(OrganizationsModel model)
		{
			if (model != null)
			{
				
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Type_of_organization  = model.Type_of_organization == null ? model.Type_of_organization : model.Type_of_organization.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Alternate_names  = model.Alternate_names == null ? model.Alternate_names : model.Alternate_names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Owner  = model.Owner == null ? model.Owner : model.Owner.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Members  = model.Members == null ? model.Members : model.Members.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Purpose  = model.Purpose == null ? model.Purpose : model.Purpose.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Services  = model.Services == null ? model.Services : model.Services.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Sub_organizations  = model.Sub_organizations == null ? model.Sub_organizations : model.Sub_organizations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Super_organizations  = model.Super_organizations == null ? model.Super_organizations : model.Super_organizations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Sister_organizations  = model.Sister_organizations == null ? model.Sister_organizations : model.Sister_organizations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Organization_structure  = model.Organization_structure == null ? model.Organization_structure : model.Organization_structure.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Rival_organizations  = model.Rival_organizations == null ? model.Rival_organizations : model.Rival_organizations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Address  = model.Address == null ? model.Address : model.Address.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Offices  = model.Offices == null ? model.Offices : model.Offices.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Locations  = model.Locations == null ? model.Locations : model.Locations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Headquarters  = model.Headquarters == null ? model.Headquarters : model.Headquarters.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
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
			var OrganizationID = Convert.ToInt64(obj["OrganizationID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
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
			var OrganizationID = Convert.ToInt64(obj["OrganizationID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
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
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveType_of_organization")]
		public IActionResult SaveType_of_organization(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Type_of_organization";
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
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
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
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
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
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
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
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
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
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
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveServices")]
		public IActionResult SaveServices(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Services";
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSub_organizations")]
		public IActionResult SaveSub_organizations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Sub_organizations";
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSuper_organizations")]
		public IActionResult SaveSuper_organizations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Super_organizations";
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSister_organizations")]
		public IActionResult SaveSister_organizations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Sister_organizations";
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
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
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRival_organizations")]
		public IActionResult SaveRival_organizations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Rival_organizations";
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveAddress")]
		public IActionResult SaveAddress(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Address";
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
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
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
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
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
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
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveFormation_year")]
		public IActionResult SaveFormation_year()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Formation_year";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var OrganizationID = Convert.ToInt64(obj["OrganizationID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveClosure_year")]
		public IActionResult SaveClosure_year()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Closure_year";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var OrganizationID = Convert.ToInt64(obj["OrganizationID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
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
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
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
			var OrganizationID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			OrganizationsModel model = new OrganizationsModel();
			model.id = OrganizationID;
			model._id = OrganizationID;
			model.column_type = type;
			model.column_value = value;
			response = _iOrganizationsApiService.SaveOrganization(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("OrganizationID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "organizations");
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
								contentObjectAttachmentModel.content_type = "organizations";
			
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
			string content_Id = HttpContext.Session.GetString("OrganizationID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "organizations";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewOrganizations", "organizations", new { id = content_Id }, "Gallery_panel");

		}

	}
}
