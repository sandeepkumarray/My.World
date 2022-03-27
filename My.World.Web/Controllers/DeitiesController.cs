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
	[Route("deities")]
	public class DeitiesController : Controller
	{
		public readonly IDeitiesApiService _iDeitiesApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContentTypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public DeitiesController(IDeitiesApiService iDeitiesApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContentTypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
_iDeitiesApiService = iDeitiesApiService;
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
			var deities = _iDeitiesApiService.GetAllDeities(accountID);
			deities.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Deities.png");
			    }
			});
			return View(deities);

		}

		[HttpGet]
		[Route("{Id}/edit")]
		public IActionResult ViewDeities(string Id)
		{
			DeitiesModel model = new DeitiesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["DeitieID"] = Id;
			ViewData["DeitieID"] = Id;
			HttpContext.Session.SetString("DeitieID", Id);
			
			DeitiesViewModel deitiesViewModel = new DeitiesViewModel(_iObjectBucketApiService);
			deitiesViewModel.deitiesModel = _iDeitiesApiService.GetDeities(model);
			if (deitiesViewModel.deitiesModel == null)
				return RedirectToAction("Index", "NotFound");
			deitiesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			deitiesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "deities");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Deities" });
			deitiesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			deitiesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			deitiesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "deities");
			deitiesViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = deitiesViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			deitiesViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(deitiesViewModel); 

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult PreviewDeities(string Id)
		{
			DeitiesModel model = new DeitiesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["DeitieID"] = Id;
			ViewData["DeitieID"] = Id;
			HttpContext.Session.SetString("DeitieID", Id);
			
			DeitiesViewModel deitiesViewModel = new DeitiesViewModel(_iObjectBucketApiService);
			deitiesViewModel.deitiesModel = _iDeitiesApiService.GetDeities(model);
			if (deitiesViewModel.deitiesModel == null)
			return RedirectToAction("Index", "NotFound");
			deitiesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(deitiesViewModel.deitiesModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			deitiesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "deities");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Deities" });
			deitiesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			deitiesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			deitiesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "deities");
			deitiesViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			return View(deitiesViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteDeities(string Id)
		{
			DeitiesModel model = new DeitiesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iDeitiesApiService.DeleteDeities(model);
			return RedirectToAction("Index");

		}

		private void TransformData(DeitiesModel model)
		{
			if (model != null)
			{
				
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Other_Names  = model.Other_Names == null ? model.Other_Names : model.Other_Names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Physical_Description  = model.Physical_Description == null ? model.Physical_Description : model.Physical_Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Children  = model.Children == null ? model.Children : model.Children.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Parents  = model.Parents == null ? model.Parents : model.Parents.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Partners  = model.Partners == null ? model.Partners : model.Partners.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Siblings  = model.Siblings == null ? model.Siblings : model.Siblings.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Floras  = model.Floras == null ? model.Floras : model.Floras.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Relics  = model.Relics == null ? model.Relics : model.Relics.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Religions  = model.Religions == null ? model.Religions : model.Religions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Creatures  = model.Creatures == null ? model.Creatures : model.Creatures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Elements  = model.Elements == null ? model.Elements : model.Elements.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Symbols  = model.Symbols == null ? model.Symbols : model.Symbols.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Abilities  = model.Abilities == null ? model.Abilities : model.Abilities.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Conditions  = model.Conditions == null ? model.Conditions : model.Conditions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Strengths  = model.Strengths == null ? model.Strengths : model.Strengths.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Weaknesses  = model.Weaknesses == null ? model.Weaknesses : model.Weaknesses.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Human_Interaction  = model.Human_Interaction == null ? model.Human_Interaction : model.Human_Interaction.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Related_towns  = model.Related_towns == null ? model.Related_towns : model.Related_towns.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Related_races  = model.Related_races == null ? model.Related_races : model.Related_races.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Related_landmarks  = model.Related_landmarks == null ? model.Related_landmarks : model.Related_landmarks.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Prayers  = model.Prayers == null ? model.Prayers : model.Prayers.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Rituals  = model.Rituals == null ? model.Rituals : model.Rituals.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Traditions  = model.Traditions == null ? model.Traditions : model.Traditions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Family_History  = model.Family_History == null ? model.Family_History : model.Family_History.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notable_Events  = model.Notable_Events == null ? model.Notable_Events : model.Notable_Events.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Life_Story  = model.Life_Story == null ? model.Life_Story : model.Life_Story.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_Notes  = model.Private_Notes == null ? model.Private_Notes : model.Private_Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
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
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
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
			var DeitieID = Convert.ToInt64(obj["DeitieID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
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
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
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
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
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
			var DeitieID = Convert.ToInt64(obj["DeitieID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveHeight")]
		public IActionResult SaveHeight()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Height";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var DeitieID = Convert.ToInt64(obj["DeitieID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePhysical_Description")]
		public IActionResult SavePhysical_Description(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Physical_Description";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
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
			var DeitieID = Convert.ToInt64(obj["DeitieID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveChildren")]
		public IActionResult SaveChildren(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Children";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveParents")]
		public IActionResult SaveParents(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Parents";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePartners")]
		public IActionResult SavePartners(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Partners";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSiblings")]
		public IActionResult SaveSiblings(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Siblings";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFloras")]
		public IActionResult SaveFloras(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Floras";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRelics")]
		public IActionResult SaveRelics(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Relics";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
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
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
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
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveElements")]
		public IActionResult SaveElements(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Elements";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSymbols")]
		public IActionResult SaveSymbols(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Symbols";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveAbilities")]
		public IActionResult SaveAbilities(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Abilities";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
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
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveStrengths")]
		public IActionResult SaveStrengths(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Strengths";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveWeaknesses")]
		public IActionResult SaveWeaknesses(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Weaknesses";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveHuman_Interaction")]
		public IActionResult SaveHuman_Interaction(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Human_Interaction";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRelated_towns")]
		public IActionResult SaveRelated_towns(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Related_towns";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRelated_races")]
		public IActionResult SaveRelated_races(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Related_races";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRelated_landmarks")]
		public IActionResult SaveRelated_landmarks(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Related_landmarks";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePrayers")]
		public IActionResult SavePrayers(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Prayers";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRituals")]
		public IActionResult SaveRituals(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Rituals";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
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
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFamily_History")]
		public IActionResult SaveFamily_History(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Family_History";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNotable_Events")]
		public IActionResult SaveNotable_Events(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Notable_Events";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveLife_Story")]
		public IActionResult SaveLife_Story(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Life_Story";
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
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
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
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
			var DeitieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			DeitiesModel model = new DeitiesModel();
			model.id = DeitieID;
			model._id = DeitieID;
			model.column_type = type;
			model.column_value = value;
			response = _iDeitiesApiService.SaveDeitie(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("DeitieID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "deities");
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
								contentObjectAttachmentModel.content_type = "deities";
			
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
			string content_Id = HttpContext.Session.GetString("DeitieID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "deities";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewDeities", "deities", new { id = content_Id }, "Gallery_panel");

		}

	}
}
