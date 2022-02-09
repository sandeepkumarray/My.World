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
	[Route("landmarks")]
	public class LandmarksController : Controller
	{
		public readonly ILandmarksApiService _iLandmarksApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public LandmarksController(ILandmarksApiService iLandmarksApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iLandmarksApiService = iLandmarksApiService;
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
			var landmarks = _iLandmarksApiService.GetAllLandmarks(accountID);
			landmarks.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Landmarks.png");
			    }
			});
			return View(landmarks);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewLandmarks(string Id)
		{
			LandmarksModel model = new LandmarksModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["LandmarkID"] = Id;
			ViewData["LandmarkID"] = Id;
			HttpContext.Session.SetString("LandmarkID", Id);
			
			LandmarksViewModel landmarksViewModel = new LandmarksViewModel(_iObjectBucketApiService);
			landmarksViewModel.landmarksModel = _iLandmarksApiService.GetLandmarks(model);
			if (landmarksViewModel.landmarksModel == null)
				return RedirectToAction("Index", "NotFound");
			landmarksViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			landmarksViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "landmarks");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Landmarks" });
			landmarksViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			landmarksViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			landmarksViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "landmarks");
			return View(landmarksViewModel);

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewLandmarks(string Id)
		{
			LandmarksModel model = new LandmarksModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["LandmarkID"] = Id;
			ViewData["LandmarkID"] = Id;
			HttpContext.Session.SetString("LandmarkID", Id);
			
			LandmarksViewModel landmarksViewModel = new LandmarksViewModel(_iObjectBucketApiService);
			landmarksViewModel.landmarksModel = _iLandmarksApiService.GetLandmarks(model);
			if (landmarksViewModel.landmarksModel == null)
			return RedirectToAction("Index", "NotFound");
			landmarksViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(landmarksViewModel.landmarksModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			landmarksViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "landmarks");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Landmarks" });
			landmarksViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			landmarksViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			landmarksViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "landmarks");
			return View(landmarksViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteLandmarks(string Id)
		{
			LandmarksModel model = new LandmarksModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iLandmarksApiService.DeleteLandmarks(model);
			return RedirectToAction("Index");

		}

		public void TransformData(LandmarksModel model)
		{
			if (model != null)
			{
				
				model.Colors  = model.Colors == null ? model.Colors : model.Colors.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Country  = model.Country == null ? model.Country : model.Country.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Creation_story  = model.Creation_story == null ? model.Creation_story : model.Creation_story.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Creatures  = model.Creatures == null ? model.Creatures : model.Creatures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Flora  = model.Flora == null ? model.Flora : model.Flora.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Materials  = model.Materials == null ? model.Materials : model.Materials.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Nearby_towns  = model.Nearby_towns == null ? model.Nearby_towns : model.Nearby_towns.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Other_Names  = model.Other_Names == null ? model.Other_Names : model.Other_Names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_Notes  = model.Private_Notes == null ? model.Private_Notes : model.Private_Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Type_of_landmark  = model.Type_of_landmark == null ? model.Type_of_landmark : model.Type_of_landmark.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
			}

		}

		#region Save Properties Methods
		[HttpPost]
		[Route("{id}/SaveColors")]
		public IActionResult SaveColors(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Colors";
			var LandmarkID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCountry")]
		public IActionResult SaveCountry(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Country";
			var LandmarkID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCreation_story")]
		public IActionResult SaveCreation_story(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Creation_story";
			var LandmarkID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
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
			var LandmarkID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
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
			var LandmarkID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
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
			var LandmarkID = Convert.ToInt64(obj["LandmarkID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
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
			var LandmarkID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
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
			var LandmarkID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
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
			var LandmarkID = Convert.ToInt64(obj["LandmarkID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNearby_towns")]
		public IActionResult SaveNearby_towns(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Nearby_towns";
			var LandmarkID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
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
			var LandmarkID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
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
			var LandmarkID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
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
			var LandmarkID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
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
			var LandmarkID = Convert.ToInt64(obj["LandmarkID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
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
			var LandmarkID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveType_of_landmark")]
		public IActionResult SaveType_of_landmark(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Type_of_landmark";
			var LandmarkID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
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
			var LandmarkID = Convert.ToInt64(obj["LandmarkID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			LandmarksModel model = new LandmarksModel();
			model.id = LandmarkID;
			model._id = LandmarkID;
			model.column_type = type;
			model.column_value = value;
			response = _iLandmarksApiService.SaveLandmark(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("LandmarksID");
			
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
			                contentObjectAttachmentModel.content_type = "landmarks";
			
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
			string content_Id = HttpContext.Session.GetString("LandmarksID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "landmarks";
			
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewLandmarks", "landmarks", new { id = content_Id }, "Gallery_panel");

		}

	}
}
