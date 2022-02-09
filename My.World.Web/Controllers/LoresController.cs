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
	[Route("lores")]
	public class LoresController : Controller
	{
		public readonly ILoresApiService _iLoresApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public LoresController(ILoresApiService iLoresApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iLoresApiService = iLoresApiService;
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
			var lores = _iLoresApiService.GetAllLores(accountID);
			lores.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Lores.png");
			    }
			});
			return View(lores);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewLores(string Id)
		{
			LoresModel model = new LoresModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["LoreID"] = Id;
			ViewData["LoreID"] = Id;
			HttpContext.Session.SetString("LoreID", Id);
			
			LoresViewModel loresViewModel = new LoresViewModel(_iObjectBucketApiService);
			loresViewModel.loresModel = _iLoresApiService.GetLores(model);
			if (loresViewModel.loresModel == null)
				return RedirectToAction("Index", "NotFound");
			loresViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			loresViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "lores");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Lores" });
			loresViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			loresViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			loresViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "lores");
			return View(loresViewModel);

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewLores(string Id)
		{
			LoresModel model = new LoresModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["LoreID"] = Id;
			ViewData["LoreID"] = Id;
			HttpContext.Session.SetString("LoreID", Id);
			
			LoresViewModel loresViewModel = new LoresViewModel(_iObjectBucketApiService);
			loresViewModel.loresModel = _iLoresApiService.GetLores(model);
			if (loresViewModel.loresModel == null)
			return RedirectToAction("Index", "NotFound");
			loresViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(loresViewModel.loresModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			loresViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "lores");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Lores" });
			loresViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			loresViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			loresViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "lores");
			return View(loresViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteLores(string Id)
		{
			LoresModel model = new LoresModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iLoresApiService.DeleteLores(model);
			return RedirectToAction("Index");

		}

		public void TransformData(LoresModel model)
		{
			if (model != null)
			{
				
				model.Background_information  = model.Background_information == null ? model.Background_information : model.Background_information.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Believability  = model.Believability == null ? model.Believability : model.Believability.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Believers  = model.Believers == null ? model.Believers : model.Believers.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Buildings  = model.Buildings == null ? model.Buildings : model.Buildings.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Characters  = model.Characters == null ? model.Characters : model.Characters.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Conditions  = model.Conditions == null ? model.Conditions : model.Conditions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Continents  = model.Continents == null ? model.Continents : model.Continents.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Countries  = model.Countries == null ? model.Countries : model.Countries.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Created_phrases  = model.Created_phrases == null ? model.Created_phrases : model.Created_phrases.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Created_traditions  = model.Created_traditions == null ? model.Created_traditions : model.Created_traditions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Creatures  = model.Creatures == null ? model.Creatures : model.Creatures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Criticism  = model.Criticism == null ? model.Criticism : model.Criticism.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Date_recorded  = model.Date_recorded == null ? model.Date_recorded : model.Date_recorded.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Deities  = model.Deities == null ? model.Deities : model.Deities.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Dialect  = model.Dialect == null ? model.Dialect : model.Dialect.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Evolution_over_time  = model.Evolution_over_time == null ? model.Evolution_over_time : model.Evolution_over_time.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.False_parts  = model.False_parts == null ? model.False_parts : model.False_parts.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Floras  = model.Floras == null ? model.Floras : model.Floras.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Foods  = model.Foods == null ? model.Foods : model.Foods.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Full_text  = model.Full_text == null ? model.Full_text : model.Full_text.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Genre  = model.Genre == null ? model.Genre : model.Genre.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Geographical_variations  = model.Geographical_variations == null ? model.Geographical_variations : model.Geographical_variations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Governments  = model.Governments == null ? model.Governments : model.Governments.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Groups  = model.Groups == null ? model.Groups : model.Groups.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Historical_context  = model.Historical_context == null ? model.Historical_context : model.Historical_context.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Hoaxes  = model.Hoaxes == null ? model.Hoaxes : model.Hoaxes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Impact  = model.Impact == null ? model.Impact : model.Impact.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Important_translations  = model.Important_translations == null ? model.Important_translations : model.Important_translations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Influence_on_modern_times  = model.Influence_on_modern_times == null ? model.Influence_on_modern_times : model.Influence_on_modern_times.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Inspirations  = model.Inspirations == null ? model.Inspirations : model.Inspirations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Interpretations  = model.Interpretations == null ? model.Interpretations : model.Interpretations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Jobs  = model.Jobs == null ? model.Jobs : model.Jobs.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Landmarks  = model.Landmarks == null ? model.Landmarks : model.Landmarks.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Magic  = model.Magic == null ? model.Magic : model.Magic.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Media_adaptations  = model.Media_adaptations == null ? model.Media_adaptations : model.Media_adaptations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Morals  = model.Morals == null ? model.Morals : model.Morals.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Motivations  = model.Motivations == null ? model.Motivations : model.Motivations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Original_author  = model.Original_author == null ? model.Original_author : model.Original_author.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Original_languages  = model.Original_languages == null ? model.Original_languages : model.Original_languages.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Original_telling  = model.Original_telling == null ? model.Original_telling : model.Original_telling.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Planets  = model.Planets == null ? model.Planets : model.Planets.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_Notes  = model.Private_Notes == null ? model.Private_Notes : model.Private_Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Propagation_method  = model.Propagation_method == null ? model.Propagation_method : model.Propagation_method.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Races  = model.Races == null ? model.Races : model.Races.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Reception  = model.Reception == null ? model.Reception : model.Reception.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Related_lores  = model.Related_lores == null ? model.Related_lores : model.Related_lores.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Religions  = model.Religions == null ? model.Religions : model.Religions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Schools  = model.Schools == null ? model.Schools : model.Schools.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Source  = model.Source == null ? model.Source : model.Source.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Sports  = model.Sports == null ? model.Sports : model.Sports.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Structure  = model.Structure == null ? model.Structure : model.Structure.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Subjects  = model.Subjects == null ? model.Subjects : model.Subjects.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Summary  = model.Summary == null ? model.Summary : model.Summary.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Symbolisms  = model.Symbolisms == null ? model.Symbolisms : model.Symbolisms.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Technologies  = model.Technologies == null ? model.Technologies : model.Technologies.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Time_period  = model.Time_period == null ? model.Time_period : model.Time_period.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tone  = model.Tone == null ? model.Tone : model.Tone.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Towns  = model.Towns == null ? model.Towns : model.Towns.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Traditions  = model.Traditions == null ? model.Traditions : model.Traditions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Translation_variations  = model.Translation_variations == null ? model.Translation_variations : model.Translation_variations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.True_parts  = model.True_parts == null ? model.True_parts : model.True_parts.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Type  = model.Type == null ? model.Type : model.Type.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Variations  = model.Variations == null ? model.Variations : model.Variations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Vehicles  = model.Vehicles == null ? model.Vehicles : model.Vehicles.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
			}

		}

		#region Save Properties Methods
		[HttpPost]
		[Route("{id}/SaveBackground_information")]
		public IActionResult SaveBackground_information(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Background_information";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveBelievability")]
		public IActionResult SaveBelievability(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Believability";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveBelievers")]
		public IActionResult SaveBelievers(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Believers";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveBuildings")]
		public IActionResult SaveBuildings(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Buildings";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCharacters")]
		public IActionResult SaveCharacters(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Characters";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveContinents")]
		public IActionResult SaveContinents(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Continents";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCreated_phrases")]
		public IActionResult SaveCreated_phrases(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Created_phrases";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCreated_traditions")]
		public IActionResult SaveCreated_traditions(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Created_traditions";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCriticism")]
		public IActionResult SaveCriticism(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Criticism";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveDate_recorded")]
		public IActionResult SaveDate_recorded(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Date_recorded";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveDialect")]
		public IActionResult SaveDialect(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Dialect";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEvolution_over_time")]
		public IActionResult SaveEvolution_over_time(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Evolution_over_time";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFalse_parts")]
		public IActionResult SaveFalse_parts(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "False_parts";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFoods")]
		public IActionResult SaveFoods(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Foods";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFull_text")]
		public IActionResult SaveFull_text(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Full_text";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGenre")]
		public IActionResult SaveGenre(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Genre";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGeographical_variations")]
		public IActionResult SaveGeographical_variations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Geographical_variations";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGovernments")]
		public IActionResult SaveGovernments(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Governments";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveHistorical_context")]
		public IActionResult SaveHistorical_context(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Historical_context";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveHoaxes")]
		public IActionResult SaveHoaxes(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Hoaxes";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveImpact")]
		public IActionResult SaveImpact(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Impact";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveImportant_translations")]
		public IActionResult SaveImportant_translations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Important_translations";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveInfluence_on_modern_times")]
		public IActionResult SaveInfluence_on_modern_times(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Influence_on_modern_times";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveInspirations")]
		public IActionResult SaveInspirations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Inspirations";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveInterpretations")]
		public IActionResult SaveInterpretations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Interpretations";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveJobs")]
		public IActionResult SaveJobs(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Jobs";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveLandmarks")]
		public IActionResult SaveLandmarks(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Landmarks";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMedia_adaptations")]
		public IActionResult SaveMedia_adaptations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Media_adaptations";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMorals")]
		public IActionResult SaveMorals(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Morals";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMotivations")]
		public IActionResult SaveMotivations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Motivations";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(obj["LoreID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOriginal_author")]
		public IActionResult SaveOriginal_author(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Original_author";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOriginal_languages")]
		public IActionResult SaveOriginal_languages(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Original_languages";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOriginal_telling")]
		public IActionResult SaveOriginal_telling(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Original_telling";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePlanets")]
		public IActionResult SavePlanets(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Planets";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePropagation_method")]
		public IActionResult SavePropagation_method(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Propagation_method";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRaces")]
		public IActionResult SaveRaces(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Races";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveReception")]
		public IActionResult SaveReception(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Reception";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRelated_lores")]
		public IActionResult SaveRelated_lores(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Related_lores";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSchools")]
		public IActionResult SaveSchools(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Schools";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSource")]
		public IActionResult SaveSource(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Source";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSports")]
		public IActionResult SaveSports(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Sports";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveStructure")]
		public IActionResult SaveStructure(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Structure";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSubjects")]
		public IActionResult SaveSubjects(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Subjects";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTechnologies")]
		public IActionResult SaveTechnologies(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Technologies";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTime_period")]
		public IActionResult SaveTime_period(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Time_period";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTone")]
		public IActionResult SaveTone(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Tone";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTranslation_variations")]
		public IActionResult SaveTranslation_variations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Translation_variations";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTrue_parts")]
		public IActionResult SaveTrue_parts(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "True_parts";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveType")]
		public IActionResult SaveType(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Type";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(obj["LoreID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
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
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveVehicles")]
		public IActionResult SaveVehicles(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Vehicles";
			var LoreID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LoresModel model = new LoresModel();
			model.id = LoreID;
			model._id = LoreID;
			model.column_type = type;
			model.column_value = value;
			response = _iLoresApiService.SaveLore(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("LoresID");
			
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
			                contentObjectAttachmentModel.content_type = "lores";
			
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
			string content_Id = HttpContext.Session.GetString("LoresID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "lores";
			
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewLores", "lores", new { id = content_Id }, "Gallery_panel");

		}

	}
}
