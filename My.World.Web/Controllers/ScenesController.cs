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
	[Route("scenes")]
	public class ScenesController : Controller
	{
		public readonly IScenesApiService _iScenesApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public ScenesController(IScenesApiService iScenesApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iScenesApiService = iScenesApiService;
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
			var scenes = _iScenesApiService.GetAllScenes(accountID);
			scenes.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Scenes.png");
			    }
			});
			return View(scenes);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewScenes(string Id)
		{
			ScenesModel model = new ScenesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["SceneID"] = Id;
			ViewData["SceneID"] = Id;
			HttpContext.Session.SetString("SceneID", Id);
			
			ScenesViewModel scenesViewModel = new ScenesViewModel(_iObjectBucketApiService);
			scenesViewModel.scenesModel = _iScenesApiService.GetScenes(model);
			if (scenesViewModel.scenesModel == null)
				return RedirectToAction("Index", "NotFound");
			scenesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			scenesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "scenes");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Scenes" });
			scenesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			scenesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			scenesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "scenes");
			scenesViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = scenesViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			scenesViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(scenesViewModel); 

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewScenes(string Id)
		{
			ScenesModel model = new ScenesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["SceneID"] = Id;
			ViewData["SceneID"] = Id;
			HttpContext.Session.SetString("SceneID", Id);
			
			ScenesViewModel scenesViewModel = new ScenesViewModel(_iObjectBucketApiService);
			scenesViewModel.scenesModel = _iScenesApiService.GetScenes(model);
			if (scenesViewModel.scenesModel == null)
			return RedirectToAction("Index", "NotFound");
			scenesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(scenesViewModel.scenesModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			scenesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "scenes");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Scenes" });
			scenesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			scenesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			scenesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "scenes");
			return View(scenesViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteScenes(string Id)
		{
			ScenesModel model = new ScenesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iScenesApiService.DeleteScenes(model);
			return RedirectToAction("Index");

		}

		public void TransformData(ScenesModel model)
		{
			if (model != null)
			{
				
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Summary  = model.Summary == null ? model.Summary : model.Summary.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Items_in_scene  = model.Items_in_scene == null ? model.Items_in_scene : model.Items_in_scene.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Locations_in_scene  = model.Locations_in_scene == null ? model.Locations_in_scene : model.Locations_in_scene.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Characters_in_scene  = model.Characters_in_scene == null ? model.Characters_in_scene : model.Characters_in_scene.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Results  = model.Results == null ? model.Results : model.Results.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.What_caused_this  = model.What_caused_this == null ? model.What_caused_this : model.What_caused_this.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
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
			var SceneID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ScenesModel model = new ScenesModel();
			model.id = SceneID;
			model._id = SceneID;
			model.column_type = type;
			model.column_value = value;
			response = _iScenesApiService.SaveScene(model);
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
			var SceneID = Convert.ToInt64(obj["SceneID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			ScenesModel model = new ScenesModel();
			model.id = SceneID;
			model._id = SceneID;
			model.column_type = type;
			model.column_value = value;
			response = _iScenesApiService.SaveScene(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSummary")]
		public IActionResult SaveSummary(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Summary";
			var SceneID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ScenesModel model = new ScenesModel();
			model.id = SceneID;
			model._id = SceneID;
			model.column_type = type;
			model.column_value = value;
			response = _iScenesApiService.SaveScene(model);
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
			var SceneID = Convert.ToInt64(obj["SceneID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			ScenesModel model = new ScenesModel();
			model.id = SceneID;
			model._id = SceneID;
			model.column_type = type;
			model.column_value = value;
			response = _iScenesApiService.SaveScene(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveItems_in_scene")]
		public IActionResult SaveItems_in_scene(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Items_in_scene";
			var SceneID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ScenesModel model = new ScenesModel();
			model.id = SceneID;
			model._id = SceneID;
			model.column_type = type;
			model.column_value = value;
			response = _iScenesApiService.SaveScene(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveLocations_in_scene")]
		public IActionResult SaveLocations_in_scene(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Locations_in_scene";
			var SceneID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ScenesModel model = new ScenesModel();
			model.id = SceneID;
			model._id = SceneID;
			model.column_type = type;
			model.column_value = value;
			response = _iScenesApiService.SaveScene(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCharacters_in_scene")]
		public IActionResult SaveCharacters_in_scene(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Characters_in_scene";
			var SceneID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ScenesModel model = new ScenesModel();
			model.id = SceneID;
			model._id = SceneID;
			model.column_type = type;
			model.column_value = value;
			response = _iScenesApiService.SaveScene(model);
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
			var SceneID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ScenesModel model = new ScenesModel();
			model.id = SceneID;
			model._id = SceneID;
			model.column_type = type;
			model.column_value = value;
			response = _iScenesApiService.SaveScene(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveResults")]
		public IActionResult SaveResults(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Results";
			var SceneID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ScenesModel model = new ScenesModel();
			model.id = SceneID;
			model._id = SceneID;
			model.column_type = type;
			model.column_value = value;
			response = _iScenesApiService.SaveScene(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveWhat_caused_this")]
		public IActionResult SaveWhat_caused_this(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "What_caused_this";
			var SceneID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ScenesModel model = new ScenesModel();
			model.id = SceneID;
			model._id = SceneID;
			model.column_type = type;
			model.column_value = value;
			response = _iScenesApiService.SaveScene(model);
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
			var SceneID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ScenesModel model = new ScenesModel();
			model.id = SceneID;
			model._id = SceneID;
			model.column_type = type;
			model.column_value = value;
			response = _iScenesApiService.SaveScene(model);
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
			var SceneID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ScenesModel model = new ScenesModel();
			model.id = SceneID;
			model._id = SceneID;
			model.column_type = type;
			model.column_value = value;
			response = _iScenesApiService.SaveScene(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("SceneID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "scenes");
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
								contentObjectAttachmentModel.content_type = "scenes";
			
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
			string content_Id = HttpContext.Session.GetString("SceneID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "scenes";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewScenes", "scenes", new { id = content_Id }, "Gallery_panel");

		}

	}
}
