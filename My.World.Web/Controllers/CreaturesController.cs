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
	[Route("creatures")]
	public class CreaturesController : Controller
	{
		public readonly ICreaturesApiService _iCreaturesApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public CreaturesController(ICreaturesApiService iCreaturesApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iCreaturesApiService = iCreaturesApiService;
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
			var creatures = _iCreaturesApiService.GetAllCreatures(accountID);
			creatures.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Creatures.png");
			    }
			});
			return View(creatures);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewCreatures(string Id)
		{
			CreaturesModel model = new CreaturesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["CreatureID"] = Id;
			ViewData["CreatureID"] = Id;
			HttpContext.Session.SetString("CreatureID", Id);
			
			CreaturesViewModel creaturesViewModel = new CreaturesViewModel(_iObjectBucketApiService);
			creaturesViewModel.creaturesModel = _iCreaturesApiService.GetCreatures(model);
			if (creaturesViewModel.creaturesModel == null)
				return RedirectToAction("Index", "NotFound");
			creaturesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			creaturesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "creatures");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Creatures" });
			creaturesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			creaturesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			creaturesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "creatures");
			creaturesViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = creaturesViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			creaturesViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(creaturesViewModel); 

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewCreatures(string Id)
		{
			CreaturesModel model = new CreaturesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["CreatureID"] = Id;
			ViewData["CreatureID"] = Id;
			HttpContext.Session.SetString("CreatureID", Id);
			
			CreaturesViewModel creaturesViewModel = new CreaturesViewModel(_iObjectBucketApiService);
			creaturesViewModel.creaturesModel = _iCreaturesApiService.GetCreatures(model);
			if (creaturesViewModel.creaturesModel == null)
			return RedirectToAction("Index", "NotFound");
			creaturesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(creaturesViewModel.creaturesModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			creaturesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "creatures");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Creatures" });
			creaturesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			creaturesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			creaturesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "creatures");
			return View(creaturesViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteCreatures(string Id)
		{
			CreaturesModel model = new CreaturesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iCreaturesApiService.DeleteCreatures(model);
			return RedirectToAction("Index");

		}

		public void TransformData(CreaturesModel model)
		{
			if (model != null)
			{
				
				model.Type_of_creature  = model.Type_of_creature == null ? model.Type_of_creature : model.Type_of_creature.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notable_features  = model.Notable_features == null ? model.Notable_features : model.Notable_features.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Materials  = model.Materials == null ? model.Materials : model.Materials.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Vestigial_features  = model.Vestigial_features == null ? model.Vestigial_features : model.Vestigial_features.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Shape  = model.Shape == null ? model.Shape : model.Shape.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Strongest_sense  = model.Strongest_sense == null ? model.Strongest_sense : model.Strongest_sense.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Aggressiveness  = model.Aggressiveness == null ? model.Aggressiveness : model.Aggressiveness.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Method_of_attack  = model.Method_of_attack == null ? model.Method_of_attack : model.Method_of_attack.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Methods_of_defense  = model.Methods_of_defense == null ? model.Methods_of_defense : model.Methods_of_defense.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Strengths  = model.Strengths == null ? model.Strengths : model.Strengths.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Weaknesses  = model.Weaknesses == null ? model.Weaknesses : model.Weaknesses.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Sounds  = model.Sounds == null ? model.Sounds : model.Sounds.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Spoils  = model.Spoils == null ? model.Spoils : model.Spoils.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Conditions  = model.Conditions == null ? model.Conditions : model.Conditions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Weakest_sense  = model.Weakest_sense == null ? model.Weakest_sense : model.Weakest_sense.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Herding_patterns  = model.Herding_patterns == null ? model.Herding_patterns : model.Herding_patterns.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Prey  = model.Prey == null ? model.Prey : model.Prey.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Predators  = model.Predators == null ? model.Predators : model.Predators.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Competitors  = model.Competitors == null ? model.Competitors : model.Competitors.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Migratory_patterns  = model.Migratory_patterns == null ? model.Migratory_patterns : model.Migratory_patterns.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Food_sources  = model.Food_sources == null ? model.Food_sources : model.Food_sources.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Habitats  = model.Habitats == null ? model.Habitats : model.Habitats.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Preferred_habitat  = model.Preferred_habitat == null ? model.Preferred_habitat : model.Preferred_habitat.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Similar_creatures  = model.Similar_creatures == null ? model.Similar_creatures : model.Similar_creatures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Symbolisms  = model.Symbolisms == null ? model.Symbolisms : model.Symbolisms.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Related_creatures  = model.Related_creatures == null ? model.Related_creatures : model.Related_creatures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Ancestors  = model.Ancestors == null ? model.Ancestors : model.Ancestors.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Evolutionary_drive  = model.Evolutionary_drive == null ? model.Evolutionary_drive : model.Evolutionary_drive.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tradeoffs  = model.Tradeoffs == null ? model.Tradeoffs : model.Tradeoffs.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Predictions  = model.Predictions == null ? model.Predictions : model.Predictions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Mortality_rate  = model.Mortality_rate == null ? model.Mortality_rate : model.Mortality_rate.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Offspring_care  = model.Offspring_care == null ? model.Offspring_care : model.Offspring_care.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Requirements  = model.Requirements == null ? model.Requirements : model.Requirements.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Mating_ritual  = model.Mating_ritual == null ? model.Mating_ritual : model.Mating_ritual.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Reproduction  = model.Reproduction == null ? model.Reproduction : model.Reproduction.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Reproduction_frequency  = model.Reproduction_frequency == null ? model.Reproduction_frequency : model.Reproduction_frequency.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Parental_instincts  = model.Parental_instincts == null ? model.Parental_instincts : model.Parental_instincts.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Variations  = model.Variations == null ? model.Variations : model.Variations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Phylum  = model.Phylum == null ? model.Phylum : model.Phylum.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Class  = model.Class == null ? model.Class : model.Class.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Order  = model.Order == null ? model.Order : model.Order.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Family  = model.Family == null ? model.Family : model.Family.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Genus  = model.Genus == null ? model.Genus : model.Genus.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Species  = model.Species == null ? model.Species : model.Species.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_notes  = model.Private_notes == null ? model.Private_notes : model.Private_notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
			}

		}

		#region Save Properties Methods
		[HttpPost]
		[Route("{id}/SaveType_of_creature")]
		public IActionResult SaveType_of_creature(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Type_of_creature";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(obj["CreatureID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(obj["CreatureID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(obj["CreatureID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNotable_features")]
		public IActionResult SaveNotable_features(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Notable_features";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveVestigial_features")]
		public IActionResult SaveVestigial_features(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Vestigial_features";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveColor")]
		public IActionResult SaveColor()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Color";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var CreatureID = Convert.ToInt64(obj["CreatureID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveShape")]
		public IActionResult SaveShape(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Shape";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(obj["CreatureID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(obj["CreatureID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveStrongest_sense")]
		public IActionResult SaveStrongest_sense(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Strongest_sense";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveAggressiveness")]
		public IActionResult SaveAggressiveness(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Aggressiveness";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMethod_of_attack")]
		public IActionResult SaveMethod_of_attack(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Method_of_attack";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMethods_of_defense")]
		public IActionResult SaveMethods_of_defense(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Methods_of_defense";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveMaximum_speed")]
		public IActionResult SaveMaximum_speed()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Maximum_speed";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var CreatureID = Convert.ToInt64(obj["CreatureID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSounds")]
		public IActionResult SaveSounds(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Sounds";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSpoils")]
		public IActionResult SaveSpoils(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Spoils";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveWeakest_sense")]
		public IActionResult SaveWeakest_sense(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Weakest_sense";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveHerding_patterns")]
		public IActionResult SaveHerding_patterns(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Herding_patterns";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePrey")]
		public IActionResult SavePrey(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Prey";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePredators")]
		public IActionResult SavePredators(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Predators";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCompetitors")]
		public IActionResult SaveCompetitors(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Competitors";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMigratory_patterns")]
		public IActionResult SaveMigratory_patterns(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Migratory_patterns";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFood_sources")]
		public IActionResult SaveFood_sources(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Food_sources";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveHabitats")]
		public IActionResult SaveHabitats(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Habitats";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePreferred_habitat")]
		public IActionResult SavePreferred_habitat(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Preferred_habitat";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSimilar_creatures")]
		public IActionResult SaveSimilar_creatures(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Similar_creatures";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSymbolisms")]
		public IActionResult SaveSymbolisms(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Symbolisms";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRelated_creatures")]
		public IActionResult SaveRelated_creatures(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Related_creatures";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveAncestors")]
		public IActionResult SaveAncestors(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Ancestors";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEvolutionary_drive")]
		public IActionResult SaveEvolutionary_drive(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Evolutionary_drive";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTradeoffs")]
		public IActionResult SaveTradeoffs(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Tradeoffs";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePredictions")]
		public IActionResult SavePredictions(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Predictions";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMortality_rate")]
		public IActionResult SaveMortality_rate(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Mortality_rate";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOffspring_care")]
		public IActionResult SaveOffspring_care(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Offspring_care";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveReproduction_age")]
		public IActionResult SaveReproduction_age()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Reproduction_age";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var CreatureID = Convert.ToInt64(obj["CreatureID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRequirements")]
		public IActionResult SaveRequirements(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Requirements";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMating_ritual")]
		public IActionResult SaveMating_ritual(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Mating_ritual";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveReproduction_frequency")]
		public IActionResult SaveReproduction_frequency(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Reproduction_frequency";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveParental_instincts")]
		public IActionResult SaveParental_instincts(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Parental_instincts";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePhylum")]
		public IActionResult SavePhylum(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Phylum";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveClass")]
		public IActionResult SaveClass(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Class";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSpecies")]
		public IActionResult SaveSpecies(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Species";
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
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
			var CreatureID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CreaturesModel model = new CreaturesModel();
			model.id = CreatureID;
			model._id = CreatureID;
			model.column_type = type;
			model.column_value = value;
			response = _iCreaturesApiService.SaveCreature(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("CreatureID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "creatures");
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
								contentObjectAttachmentModel.content_type = "creatures";
			
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
			string content_Id = HttpContext.Session.GetString("CreatureID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "creatures";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewCreatures", "creatures", new { id = content_Id }, "Gallery_panel");

		}

	}
}
