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
	[Route("magics")]
	public class MagicsController : Controller
	{
		public readonly IMagicsApiService _iMagicsApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContentTypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public MagicsController(IMagicsApiService iMagicsApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContentTypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
_iMagicsApiService = iMagicsApiService;
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
			var magics = _iMagicsApiService.GetAllMagics(accountID);
			magics.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Magics.png");
			    }
			});
			return View(magics);

		}

		[HttpGet]
		[Route("{Id}/edit")]
		public IActionResult ViewMagics(string Id)
		{
			MagicsModel model = new MagicsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["MagicID"] = Id;
			ViewData["MagicID"] = Id;
			HttpContext.Session.SetString("MagicID", Id);
			
			MagicsViewModel magicsViewModel = new MagicsViewModel(_iObjectBucketApiService);
			magicsViewModel.magicsModel = _iMagicsApiService.GetMagics(model);
			if (magicsViewModel.magicsModel == null)
				return RedirectToAction("Index", "NotFound");
			magicsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			magicsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "magics");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Magics" });
			magicsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			magicsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			magicsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "magics");
			magicsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = magicsViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			magicsViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(magicsViewModel); 

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult PreviewMagics(string Id)
		{
			MagicsModel model = new MagicsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["MagicID"] = Id;
			ViewData["MagicID"] = Id;
			HttpContext.Session.SetString("MagicID", Id);
			
			MagicsViewModel magicsViewModel = new MagicsViewModel(_iObjectBucketApiService);
			magicsViewModel.magicsModel = _iMagicsApiService.GetMagics(model);
			if (magicsViewModel.magicsModel == null)
			return RedirectToAction("Index", "NotFound");
			magicsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(magicsViewModel.magicsModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			magicsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "magics");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Magics" });
			magicsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			magicsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			magicsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "magics");
			magicsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			return View(magicsViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteMagics(string Id)
		{
			MagicsModel model = new MagicsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iMagicsApiService.DeleteMagics(model);
			return RedirectToAction("Index");

		}

		private void TransformData(MagicsModel model)
		{
			if (model != null)
			{
				
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Type_of_magic  = model.Type_of_magic == null ? model.Type_of_magic : model.Type_of_magic.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Effects  = model.Effects == null ? model.Effects : model.Effects.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Visuals  = model.Visuals == null ? model.Visuals : model.Visuals.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Aftereffects  = model.Aftereffects == null ? model.Aftereffects : model.Aftereffects.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Conditions  = model.Conditions == null ? model.Conditions : model.Conditions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Negative_effects  = model.Negative_effects == null ? model.Negative_effects : model.Negative_effects.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Neutral_effects  = model.Neutral_effects == null ? model.Neutral_effects : model.Neutral_effects.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Positive_effects  = model.Positive_effects == null ? model.Positive_effects : model.Positive_effects.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Deities  = model.Deities == null ? model.Deities : model.Deities.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Element  = model.Element == null ? model.Element : model.Element.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Materials_required  = model.Materials_required == null ? model.Materials_required : model.Materials_required.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Skills_required  = model.Skills_required == null ? model.Skills_required : model.Skills_required.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Education  = model.Education == null ? model.Education : model.Education.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Resource_costs  = model.Resource_costs == null ? model.Resource_costs : model.Resource_costs.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Limitations  = model.Limitations == null ? model.Limitations : model.Limitations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_notes  = model.Private_notes == null ? model.Private_notes : model.Private_notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
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
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
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
			var MagicID = Convert.ToInt64(obj["MagicID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
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
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveType_of_magic")]
		public IActionResult SaveType_of_magic(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Type_of_magic";
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
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
			var MagicID = Convert.ToInt64(obj["MagicID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEffects")]
		public IActionResult SaveEffects(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Effects";
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveVisuals")]
		public IActionResult SaveVisuals(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Visuals";
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveAftereffects")]
		public IActionResult SaveAftereffects(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Aftereffects";
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveConditions")]
		public IActionResult SaveConditions(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Conditions";
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveScale")]
		public IActionResult SaveScale()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Scale";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var MagicID = Convert.ToInt64(obj["MagicID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNegative_effects")]
		public IActionResult SaveNegative_effects(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Negative_effects";
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNeutral_effects")]
		public IActionResult SaveNeutral_effects(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Neutral_effects";
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePositive_effects")]
		public IActionResult SavePositive_effects(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Positive_effects";
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveDeities")]
		public IActionResult SaveDeities(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Deities";
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveElement")]
		public IActionResult SaveElement(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Element";
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMaterials_required")]
		public IActionResult SaveMaterials_required(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Materials_required";
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSkills_required")]
		public IActionResult SaveSkills_required(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Skills_required";
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
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
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveResource_costs")]
		public IActionResult SaveResource_costs(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Resource_costs";
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveLimitations")]
		public IActionResult SaveLimitations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Limitations";
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
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
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
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
			var MagicID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			MagicsModel model = new MagicsModel();
			model.id = MagicID;
			model._id = MagicID;
			model.column_type = type;
			model.column_value = value;
			response = _iMagicsApiService.SaveMagic(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("MagicID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "magics");
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
								contentObjectAttachmentModel.content_type = "magics";
			
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
			string content_Id = HttpContext.Session.GetString("MagicID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "magics";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewMagics", "magics", new { id = content_Id }, "Gallery_panel");

		}

	}
}
