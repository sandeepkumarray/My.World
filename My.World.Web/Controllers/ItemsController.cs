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
	[Route("items")]
	public class ItemsController : Controller
	{
		public readonly IItemsApiService _iItemsApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public ItemsController(IItemsApiService iItemsApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iItemsApiService = iItemsApiService;
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
			var items = _iItemsApiService.GetAllItems(accountID);
			items.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Items.png");
			    }
			});
			return View(items);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewItems(string Id)
		{
			ItemsModel model = new ItemsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["ItemID"] = Id;
			ViewData["ItemID"] = Id;
			HttpContext.Session.SetString("ItemID", Id);
			
			ItemsViewModel itemsViewModel = new ItemsViewModel(_iObjectBucketApiService);
			itemsViewModel.itemsModel = _iItemsApiService.GetItems(model);
			if (itemsViewModel.itemsModel == null)
				return RedirectToAction("Index", "NotFound");
			itemsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			itemsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "items");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Items" });
			itemsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			itemsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			itemsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "items");
			itemsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = itemsViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			itemsViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(itemsViewModel); 

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewItems(string Id)
		{
			ItemsModel model = new ItemsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["ItemID"] = Id;
			ViewData["ItemID"] = Id;
			HttpContext.Session.SetString("ItemID", Id);
			
			ItemsViewModel itemsViewModel = new ItemsViewModel(_iObjectBucketApiService);
			itemsViewModel.itemsModel = _iItemsApiService.GetItems(model);
			if (itemsViewModel.itemsModel == null)
			return RedirectToAction("Index", "NotFound");
			itemsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(itemsViewModel.itemsModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			itemsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "items");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Items" });
			itemsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			itemsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			itemsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "items");
			return View(itemsViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteItems(string Id)
		{
			ItemsModel model = new ItemsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iItemsApiService.DeleteItems(model);
			return RedirectToAction("Index");

		}

		public void TransformData(ItemsModel model)
		{
			if (model != null)
			{
				
				model.Item_Type  = model.Item_Type == null ? model.Item_Type : model.Item_Type.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Materials  = model.Materials == null ? model.Materials : model.Materials.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Past_Owners  = model.Past_Owners == null ? model.Past_Owners : model.Past_Owners.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Makers  = model.Makers == null ? model.Makers : model.Makers.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Current_Owners  = model.Current_Owners == null ? model.Current_Owners : model.Current_Owners.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Original_Owners  = model.Original_Owners == null ? model.Original_Owners : model.Original_Owners.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Magical_effects  = model.Magical_effects == null ? model.Magical_effects : model.Magical_effects.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Magic  = model.Magic == null ? model.Magic : model.Magic.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Technical_effects  = model.Technical_effects == null ? model.Technical_effects : model.Technical_effects.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Technology  = model.Technology == null ? model.Technology : model.Technology.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_Notes  = model.Private_Notes == null ? model.Private_Notes : model.Private_Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
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
			var ItemID = Convert.ToInt64(obj["ItemID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveItem_Type")]
		public IActionResult SaveItem_Type(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Item_Type";
			var ItemID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
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
			var ItemID = Convert.ToInt64(obj["ItemID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
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
			var ItemID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
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
			var ItemID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
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
			var ItemID = Convert.ToInt64(obj["ItemID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
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
			var ItemID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePast_Owners")]
		public IActionResult SavePast_Owners(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Past_Owners";
			var ItemID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveYear_it_was_made")]
		public IActionResult SaveYear_it_was_made()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Year_it_was_made";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var ItemID = Convert.ToInt64(obj["ItemID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMakers")]
		public IActionResult SaveMakers(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Makers";
			var ItemID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCurrent_Owners")]
		public IActionResult SaveCurrent_Owners(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Current_Owners";
			var ItemID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOriginal_Owners")]
		public IActionResult SaveOriginal_Owners(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Original_Owners";
			var ItemID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMagical_effects")]
		public IActionResult SaveMagical_effects(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Magical_effects";
			var ItemID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMagic")]
		public IActionResult SaveMagic(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Magic";
			var ItemID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTechnical_effects")]
		public IActionResult SaveTechnical_effects(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Technical_effects";
			var ItemID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTechnology")]
		public IActionResult SaveTechnology(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Technology";
			var ItemID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
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
			var ItemID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
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
			var ItemID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ItemsModel model = new ItemsModel();
			model.id = ItemID;
			model._id = ItemID;
			model.column_type = type;
			model.column_value = value;
			response = _iItemsApiService.SaveItem(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("ItemID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "items");
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
								contentObjectAttachmentModel.content_type = "items";
			
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
			string content_Id = HttpContext.Session.GetString("ItemID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "items";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewItems", "items", new { id = content_Id }, "Gallery_panel");

		}

	}
}
