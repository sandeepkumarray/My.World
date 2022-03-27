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
	[Route("universes")]
	public class UniversesController : Controller
	{
		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContentTypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public UniversesController(IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContentTypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
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
			var universes = _iUniversesApiService.GetAllUniverses(accountID);
			universes.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Universes.png");
			    }
			});
			return View(universes);

		}

		[HttpGet]
		[Route("{Id}/edit")]
		public IActionResult ViewUniverses(string Id)
		{
			UniversesModel model = new UniversesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["UniverseID"] = Id;
			ViewData["UniverseID"] = Id;
			HttpContext.Session.SetString("UniverseID", Id);
			
			UniversesViewModel universesViewModel = new UniversesViewModel(_iObjectBucketApiService);
			universesViewModel.universesModel = _iUniversesApiService.GetUniverses(model);
			if (universesViewModel.universesModel == null)
				return RedirectToAction("Index", "NotFound");
			universesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			universesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "universes");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Universes" });
			universesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			universesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			universesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "universes");
			universesViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = universesViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			universesViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(universesViewModel); 

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult PreviewUniverses(string Id)
		{
			UniversesModel model = new UniversesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["UniverseID"] = Id;
			ViewData["UniverseID"] = Id;
			HttpContext.Session.SetString("UniverseID", Id);
			
			UniversesViewModel universesViewModel = new UniversesViewModel(_iObjectBucketApiService);
			universesViewModel.universesModel = _iUniversesApiService.GetUniverses(model);
			if (universesViewModel.universesModel == null)
			return RedirectToAction("Index", "NotFound");
			universesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(universesViewModel.universesModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			universesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "universes");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Universes" });
			universesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			universesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			universesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "universes");
			universesViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			return View(universesViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteUniverses(string Id)
		{
			UniversesModel model = new UniversesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iUniversesApiService.DeleteUniverses(model);
			return RedirectToAction("Index");

		}

		private void TransformData(UniversesModel model)
		{
			if (model != null)
			{
				
				model.description  = model.description == null ? model.description : model.description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.history  = model.history == null ? model.history : model.history.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.notes  = model.notes == null ? model.notes : model.notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.private_notes  = model.private_notes == null ? model.private_notes : model.private_notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.laws_of_physics  = model.laws_of_physics == null ? model.laws_of_physics : model.laws_of_physics.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.magic_system  = model.magic_system == null ? model.magic_system : model.magic_system.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.technology  = model.technology == null ? model.technology : model.technology.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.genre  = model.genre == null ? model.genre : model.genre.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.page_type  = model.page_type == null ? model.page_type : model.page_type.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
			}

		}

		#region Save Properties Methods
		[HttpPost]
		[Route("Savename")]
		public IActionResult Savename()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "name";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var UniverseID = Convert.ToInt64(obj["UniverseID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Savedescription")]
		public IActionResult Savedescription(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "description";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Savehistory")]
		public IActionResult Savehistory(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "history";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Savenotes")]
		public IActionResult Savenotes(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "notes";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Saveprivate_notes")]
		public IActionResult Saveprivate_notes(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "private_notes";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("Saveprivacy")]
		public IActionResult Saveprivacy()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "privacy";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var UniverseID = Convert.ToInt64(obj["UniverseID"].Value);
			var value = Convert.ToBoolean(obj["value"].Value);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = Convert.ToInt32(value);
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Savelaws_of_physics")]
		public IActionResult Savelaws_of_physics(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "laws_of_physics";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Savemagic_system")]
		public IActionResult Savemagic_system(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "magic_system";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Savetechnology")]
		public IActionResult Savetechnology(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "technology";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Savegenre")]
		public IActionResult Savegenre(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "genre";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Savepage_type")]
		public IActionResult Savepage_type(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "page_type";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("Savefavorite")]
		public IActionResult Savefavorite()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "favorite";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var UniverseID = Convert.ToInt64(obj["UniverseID"].Value);
			var value = Convert.ToBoolean(obj["value"].Value);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = Convert.ToInt32(value);
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("UniverseID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "universes");
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
								contentObjectAttachmentModel.content_type = "universes";
			
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
			string content_Id = HttpContext.Session.GetString("UniverseID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "universes";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewUniverses", "universes", new { id = content_Id }, "Gallery_panel");

		}

	}
}
