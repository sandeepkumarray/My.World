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
	[Route("floras")]
	public class FlorasController : Controller
	{
		public readonly IFlorasApiService _iFlorasApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public FlorasController(IFlorasApiService iFlorasApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iFlorasApiService = iFlorasApiService;
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
			var floras = _iFlorasApiService.GetAllFloras(accountID);
			floras.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Floras.png");
			    }
			});
			return View(floras);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewFloras(string Id)
		{
			FlorasModel model = new FlorasModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["FloraID"] = Id;
			ViewData["FloraID"] = Id;
			HttpContext.Session.SetString("FloraID", Id);
			
			FlorasViewModel florasViewModel = new FlorasViewModel(_iObjectBucketApiService);
			florasViewModel.florasModel = _iFlorasApiService.GetFloras(model);
			if (florasViewModel.florasModel == null)
				return RedirectToAction("Index", "NotFound");
			florasViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			florasViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "floras");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Floras" });
			florasViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			florasViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			florasViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "floras");
			florasViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = florasViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			florasViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(florasViewModel); 

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewFloras(string Id)
		{
			FlorasModel model = new FlorasModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["FloraID"] = Id;
			ViewData["FloraID"] = Id;
			HttpContext.Session.SetString("FloraID", Id);
			
			FlorasViewModel florasViewModel = new FlorasViewModel(_iObjectBucketApiService);
			florasViewModel.florasModel = _iFlorasApiService.GetFloras(model);
			if (florasViewModel.florasModel == null)
			return RedirectToAction("Index", "NotFound");
			florasViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(florasViewModel.florasModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			florasViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "floras");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Floras" });
			florasViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			florasViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			florasViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "floras");
			return View(florasViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteFloras(string Id)
		{
			FlorasModel model = new FlorasModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iFlorasApiService.DeleteFloras(model);
			return RedirectToAction("Index");

		}

		public void TransformData(FlorasModel model)
		{
			if (model != null)
			{
				
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Other_Names  = model.Other_Names == null ? model.Other_Names : model.Other_Names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Order  = model.Order == null ? model.Order : model.Order.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Related_flora  = model.Related_flora == null ? model.Related_flora : model.Related_flora.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Genus  = model.Genus == null ? model.Genus : model.Genus.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Family  = model.Family == null ? model.Family : model.Family.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Size  = model.Size == null ? model.Size : model.Size.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Smell  = model.Smell == null ? model.Smell : model.Smell.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Taste  = model.Taste == null ? model.Taste : model.Taste.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Colorings  = model.Colorings == null ? model.Colorings : model.Colorings.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Fruits  = model.Fruits == null ? model.Fruits : model.Fruits.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Magical_effects  = model.Magical_effects == null ? model.Magical_effects : model.Magical_effects.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Material_uses  = model.Material_uses == null ? model.Material_uses : model.Material_uses.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Medicinal_purposes  = model.Medicinal_purposes == null ? model.Medicinal_purposes : model.Medicinal_purposes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Berries  = model.Berries == null ? model.Berries : model.Berries.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Nuts  = model.Nuts == null ? model.Nuts : model.Nuts.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Seeds  = model.Seeds == null ? model.Seeds : model.Seeds.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Seasonality  = model.Seasonality == null ? model.Seasonality : model.Seasonality.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Locations  = model.Locations == null ? model.Locations : model.Locations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Reproduction  = model.Reproduction == null ? model.Reproduction : model.Reproduction.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Eaten_by  = model.Eaten_by == null ? model.Eaten_by : model.Eaten_by.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_Notes  = model.Private_Notes == null ? model.Private_Notes : model.Private_Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
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
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
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
			var FloraID = Convert.ToInt64(obj["FloraID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
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
			var FloraID = Convert.ToInt64(obj["FloraID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
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
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
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
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOrder")]
		public IActionResult SaveOrder(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Order";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRelated_flora")]
		public IActionResult SaveRelated_flora(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Related_flora";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGenus")]
		public IActionResult SaveGenus(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Genus";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFamily")]
		public IActionResult SaveFamily(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Family";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSize")]
		public IActionResult SaveSize(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Size";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSmell")]
		public IActionResult SaveSmell(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Smell";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTaste")]
		public IActionResult SaveTaste(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Taste";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveColorings")]
		public IActionResult SaveColorings(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Colorings";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFruits")]
		public IActionResult SaveFruits(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Fruits";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
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
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMaterial_uses")]
		public IActionResult SaveMaterial_uses(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Material_uses";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMedicinal_purposes")]
		public IActionResult SaveMedicinal_purposes(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Medicinal_purposes";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveBerries")]
		public IActionResult SaveBerries(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Berries";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNuts")]
		public IActionResult SaveNuts(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Nuts";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSeeds")]
		public IActionResult SaveSeeds(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Seeds";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSeasonality")]
		public IActionResult SaveSeasonality(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Seasonality";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
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
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveReproduction")]
		public IActionResult SaveReproduction(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Reproduction";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEaten_by")]
		public IActionResult SaveEaten_by(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Eaten_by";
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
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
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
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
			var FloraID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FlorasModel model = new FlorasModel();
			model.id = FloraID;
			model._id = FloraID;
			model.column_type = type;
			model.column_value = value;
			response = _iFlorasApiService.SaveFlora(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("FloraID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "floras");
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
								contentObjectAttachmentModel.content_type = "floras";
			
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
			string content_Id = HttpContext.Session.GetString("FloraID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "floras";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewFloras", "floras", new { id = content_Id }, "Gallery_panel");

		}

	}
}
