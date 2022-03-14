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
	[Route("conditions")]
	public class ConditionsController : Controller
	{
		public readonly IConditionsApiService _iConditionsApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public ConditionsController(IConditionsApiService iConditionsApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iConditionsApiService = iConditionsApiService;
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
			var conditions = _iConditionsApiService.GetAllConditions(accountID);
			conditions.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Conditions.png");
			    }
			});
			return View(conditions);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewConditions(string Id)
		{
			ConditionsModel model = new ConditionsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["ConditionID"] = Id;
			ViewData["ConditionID"] = Id;
			HttpContext.Session.SetString("ConditionID", Id);
			
			ConditionsViewModel conditionsViewModel = new ConditionsViewModel(_iObjectBucketApiService);
			conditionsViewModel.conditionsModel = _iConditionsApiService.GetConditions(model);
			if (conditionsViewModel.conditionsModel == null)
				return RedirectToAction("Index", "NotFound");
			conditionsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			conditionsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "conditions");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Conditions" });
			conditionsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			conditionsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			conditionsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "conditions");
			conditionsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = conditionsViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			conditionsViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(conditionsViewModel); 

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewConditions(string Id)
		{
			ConditionsModel model = new ConditionsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["ConditionID"] = Id;
			ViewData["ConditionID"] = Id;
			HttpContext.Session.SetString("ConditionID", Id);
			
			ConditionsViewModel conditionsViewModel = new ConditionsViewModel(_iObjectBucketApiService);
			conditionsViewModel.conditionsModel = _iConditionsApiService.GetConditions(model);
			if (conditionsViewModel.conditionsModel == null)
			return RedirectToAction("Index", "NotFound");
			conditionsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(conditionsViewModel.conditionsModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			conditionsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "conditions");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Conditions" });
			conditionsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			conditionsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			conditionsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "conditions");
			return View(conditionsViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteConditions(string Id)
		{
			ConditionsModel model = new ConditionsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iConditionsApiService.DeleteConditions(model);
			return RedirectToAction("Index");

		}

		public void TransformData(ConditionsModel model)
		{
			if (model != null)
			{
				
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Type_of_condition  = model.Type_of_condition == null ? model.Type_of_condition : model.Type_of_condition.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Alternate_names  = model.Alternate_names == null ? model.Alternate_names : model.Alternate_names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Transmission  = model.Transmission == null ? model.Transmission : model.Transmission.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Genetic_factors  = model.Genetic_factors == null ? model.Genetic_factors : model.Genetic_factors.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Environmental_factors  = model.Environmental_factors == null ? model.Environmental_factors : model.Environmental_factors.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Lifestyle_factors  = model.Lifestyle_factors == null ? model.Lifestyle_factors : model.Lifestyle_factors.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Epidemiology  = model.Epidemiology == null ? model.Epidemiology : model.Epidemiology.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Duration  = model.Duration == null ? model.Duration : model.Duration.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Variations  = model.Variations == null ? model.Variations : model.Variations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Prognosis  = model.Prognosis == null ? model.Prognosis : model.Prognosis.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Symptoms  = model.Symptoms == null ? model.Symptoms : model.Symptoms.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Mental_effects  = model.Mental_effects == null ? model.Mental_effects : model.Mental_effects.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Visual_effects  = model.Visual_effects == null ? model.Visual_effects : model.Visual_effects.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Prevention  = model.Prevention == null ? model.Prevention : model.Prevention.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Treatment  = model.Treatment == null ? model.Treatment : model.Treatment.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Medication  = model.Medication == null ? model.Medication : model.Medication.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Immunization  = model.Immunization == null ? model.Immunization : model.Immunization.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Diagnostic_method  = model.Diagnostic_method == null ? model.Diagnostic_method : model.Diagnostic_method.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Symbolism  = model.Symbolism == null ? model.Symbolism : model.Symbolism.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Specialty_Field  = model.Specialty_Field == null ? model.Specialty_Field : model.Specialty_Field.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Rarity  = model.Rarity == null ? model.Rarity : model.Rarity.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Evolution  = model.Evolution == null ? model.Evolution : model.Evolution.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Origin  = model.Origin == null ? model.Origin : model.Origin.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
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
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
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
			var ConditionID = Convert.ToInt64(obj["ConditionID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
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
			var ConditionID = Convert.ToInt64(obj["ConditionID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
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
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveType_of_condition")]
		public IActionResult SaveType_of_condition(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Type_of_condition";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
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
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTransmission")]
		public IActionResult SaveTransmission(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Transmission";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGenetic_factors")]
		public IActionResult SaveGenetic_factors(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Genetic_factors";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEnvironmental_factors")]
		public IActionResult SaveEnvironmental_factors(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Environmental_factors";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveLifestyle_factors")]
		public IActionResult SaveLifestyle_factors(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Lifestyle_factors";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEpidemiology")]
		public IActionResult SaveEpidemiology(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Epidemiology";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveDuration")]
		public IActionResult SaveDuration(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Duration";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveVariations")]
		public IActionResult SaveVariations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Variations";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePrognosis")]
		public IActionResult SavePrognosis(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Prognosis";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSymptoms")]
		public IActionResult SaveSymptoms(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Symptoms";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMental_effects")]
		public IActionResult SaveMental_effects(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Mental_effects";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveVisual_effects")]
		public IActionResult SaveVisual_effects(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Visual_effects";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePrevention")]
		public IActionResult SavePrevention(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Prevention";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTreatment")]
		public IActionResult SaveTreatment(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Treatment";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMedication")]
		public IActionResult SaveMedication(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Medication";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveImmunization")]
		public IActionResult SaveImmunization(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Immunization";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveDiagnostic_method")]
		public IActionResult SaveDiagnostic_method(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Diagnostic_method";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
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
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSpecialty_Field")]
		public IActionResult SaveSpecialty_Field(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Specialty_Field";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRarity")]
		public IActionResult SaveRarity(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Rarity";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEvolution")]
		public IActionResult SaveEvolution(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Evolution";
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
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
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
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
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
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
			var ConditionID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			ConditionsModel model = new ConditionsModel();
			model.id = ConditionID;
			model._id = ConditionID;
			model.column_type = type;
			model.column_value = value;
			response = _iConditionsApiService.SaveCondition(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("ConditionID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "conditions");
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
								contentObjectAttachmentModel.content_type = "conditions";
			
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
			string content_Id = HttpContext.Session.GetString("ConditionID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "conditions";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewConditions", "conditions", new { id = content_Id }, "Gallery_panel");

		}

	}
}
