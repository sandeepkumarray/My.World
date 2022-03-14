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
	[Route("traditions")]
	public class TraditionsController : Controller
	{
		public readonly ITraditionsApiService _iTraditionsApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public TraditionsController(ITraditionsApiService iTraditionsApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iTraditionsApiService = iTraditionsApiService;
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
			var traditions = _iTraditionsApiService.GetAllTraditions(accountID);
			traditions.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Traditions.png");
			    }
			});
			return View(traditions);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewTraditions(string Id)
		{
			TraditionsModel model = new TraditionsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["TraditionID"] = Id;
			ViewData["TraditionID"] = Id;
			HttpContext.Session.SetString("TraditionID", Id);
			
			TraditionsViewModel traditionsViewModel = new TraditionsViewModel(_iObjectBucketApiService);
			traditionsViewModel.traditionsModel = _iTraditionsApiService.GetTraditions(model);
			if (traditionsViewModel.traditionsModel == null)
				return RedirectToAction("Index", "NotFound");
			traditionsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			traditionsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "traditions");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Traditions" });
			traditionsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			traditionsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			traditionsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "traditions");
			traditionsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = traditionsViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			traditionsViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(traditionsViewModel); 

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewTraditions(string Id)
		{
			TraditionsModel model = new TraditionsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["TraditionID"] = Id;
			ViewData["TraditionID"] = Id;
			HttpContext.Session.SetString("TraditionID", Id);
			
			TraditionsViewModel traditionsViewModel = new TraditionsViewModel(_iObjectBucketApiService);
			traditionsViewModel.traditionsModel = _iTraditionsApiService.GetTraditions(model);
			if (traditionsViewModel.traditionsModel == null)
			return RedirectToAction("Index", "NotFound");
			traditionsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(traditionsViewModel.traditionsModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			traditionsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "traditions");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Traditions" });
			traditionsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			traditionsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			traditionsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "traditions");
			return View(traditionsViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteTraditions(string Id)
		{
			TraditionsModel model = new TraditionsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iTraditionsApiService.DeleteTraditions(model);
			return RedirectToAction("Index");

		}

		public void TransformData(TraditionsModel model)
		{
			if (model != null)
			{
				
				model.Alternate_names  = model.Alternate_names == null ? model.Alternate_names : model.Alternate_names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Type_of_tradition  = model.Type_of_tradition == null ? model.Type_of_tradition : model.Type_of_tradition.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Countries  = model.Countries == null ? model.Countries : model.Countries.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Dates  = model.Dates == null ? model.Dates : model.Dates.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Groups  = model.Groups == null ? model.Groups : model.Groups.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Towns  = model.Towns == null ? model.Towns : model.Towns.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Gifts  = model.Gifts == null ? model.Gifts : model.Gifts.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Food  = model.Food == null ? model.Food : model.Food.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Symbolism  = model.Symbolism == null ? model.Symbolism : model.Symbolism.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Games  = model.Games == null ? model.Games : model.Games.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Activities  = model.Activities == null ? model.Activities : model.Activities.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Etymology  = model.Etymology == null ? model.Etymology : model.Etymology.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Origin  = model.Origin == null ? model.Origin : model.Origin.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Significance  = model.Significance == null ? model.Significance : model.Significance.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Religions  = model.Religions == null ? model.Religions : model.Religions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notable_events  = model.Notable_events == null ? model.Notable_events : model.Notable_events.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
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
			var TraditionID = Convert.ToInt64(obj["TraditionID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
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
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
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
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
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
			var TraditionID = Convert.ToInt64(obj["TraditionID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
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
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveType_of_tradition")]
		public IActionResult SaveType_of_tradition(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Type_of_tradition";
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
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
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveDates")]
		public IActionResult SaveDates(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Dates";
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGroups")]
		public IActionResult SaveGroups(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Groups";
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
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
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGifts")]
		public IActionResult SaveGifts(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Gifts";
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFood")]
		public IActionResult SaveFood(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Food";
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSymbolism")]
		public IActionResult SaveSymbolism(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Symbolism";
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGames")]
		public IActionResult SaveGames(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Games";
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveActivities")]
		public IActionResult SaveActivities(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Activities";
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEtymology")]
		public IActionResult SaveEtymology(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Etymology";
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOrigin")]
		public IActionResult SaveOrigin(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Origin";
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSignificance")]
		public IActionResult SaveSignificance(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Significance";
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
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
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
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
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
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
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
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
			var TraditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TraditionsModel model = new TraditionsModel();
			model.id = TraditionID;
			model._id = TraditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iTraditionsApiService.SaveTradition(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("TraditionID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "traditions");
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
								contentObjectAttachmentModel.content_type = "traditions";
			
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
			string content_Id = HttpContext.Session.GetString("TraditionID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "traditions";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewTraditions", "traditions", new { id = content_Id }, "Gallery_panel");

		}

	}
}
