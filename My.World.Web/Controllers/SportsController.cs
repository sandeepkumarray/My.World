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
	[Route("sports")]
	public class SportsController : Controller
	{
		public readonly ISportsApiService _iSportsApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContentTypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public SportsController(ISportsApiService iSportsApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContentTypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
_iSportsApiService = iSportsApiService;
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
			var sports = _iSportsApiService.GetAllSports(accountID);
			sports.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Sports.png");
			    }
			});
			return View(sports);

		}

		[HttpGet]
		[Route("{Id}/edit")]
		public IActionResult ViewSports(string Id)
		{
			SportsModel model = new SportsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["SportID"] = Id;
			ViewData["SportID"] = Id;
			HttpContext.Session.SetString("SportID", Id);
			
			SportsViewModel sportsViewModel = new SportsViewModel(_iObjectBucketApiService);
			sportsViewModel.sportsModel = _iSportsApiService.GetSports(model);
			if (sportsViewModel.sportsModel == null)
				return RedirectToAction("Index", "NotFound");
			sportsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			sportsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "sports");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Sports" });
			sportsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			sportsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			sportsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "sports");
			sportsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = sportsViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			sportsViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(sportsViewModel); 

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult PreviewSports(string Id)
		{
			SportsModel model = new SportsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["SportID"] = Id;
			ViewData["SportID"] = Id;
			HttpContext.Session.SetString("SportID", Id);
			
			SportsViewModel sportsViewModel = new SportsViewModel(_iObjectBucketApiService);
			sportsViewModel.sportsModel = _iSportsApiService.GetSports(model);
			if (sportsViewModel.sportsModel == null)
			return RedirectToAction("Index", "NotFound");
			sportsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(sportsViewModel.sportsModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			sportsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "sports");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Sports" });
			sportsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			sportsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			sportsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "sports");
			sportsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			return View(sportsViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteSports(string Id)
		{
			SportsModel model = new SportsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iSportsApiService.DeleteSports(model);
			return RedirectToAction("Index");

		}

		private void TransformData(SportsModel model)
		{
			if (model != null)
			{
				
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Nicknames  = model.Nicknames == null ? model.Nicknames : model.Nicknames.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.How_to_win  = model.How_to_win == null ? model.How_to_win : model.How_to_win.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Penalties  = model.Penalties == null ? model.Penalties : model.Penalties.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Scoring  = model.Scoring == null ? model.Scoring : model.Scoring.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Equipment  = model.Equipment == null ? model.Equipment : model.Equipment.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Play_area  = model.Play_area == null ? model.Play_area : model.Play_area.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Most_important_muscles  = model.Most_important_muscles == null ? model.Most_important_muscles : model.Most_important_muscles.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Common_injuries  = model.Common_injuries == null ? model.Common_injuries : model.Common_injuries.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Strategies  = model.Strategies == null ? model.Strategies : model.Strategies.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Rules  = model.Rules == null ? model.Rules : model.Rules.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Traditions  = model.Traditions == null ? model.Traditions : model.Traditions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Teams  = model.Teams == null ? model.Teams : model.Teams.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Countries  = model.Countries == null ? model.Countries : model.Countries.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Players  = model.Players == null ? model.Players : model.Players.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Popularity  = model.Popularity == null ? model.Popularity : model.Popularity.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Merchandise  = model.Merchandise == null ? model.Merchandise : model.Merchandise.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Uniforms  = model.Uniforms == null ? model.Uniforms : model.Uniforms.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Famous_games  = model.Famous_games == null ? model.Famous_games : model.Famous_games.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Evolution  = model.Evolution == null ? model.Evolution : model.Evolution.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Creators  = model.Creators == null ? model.Creators : model.Creators.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Origin_story  = model.Origin_story == null ? model.Origin_story : model.Origin_story.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
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
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
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
			var SportID = Convert.ToInt64(obj["SportID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNicknames")]
		public IActionResult SaveNicknames(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Nicknames";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
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
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
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
			var SportID = Convert.ToInt64(obj["SportID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveHow_to_win")]
		public IActionResult SaveHow_to_win(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "How_to_win";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePenalties")]
		public IActionResult SavePenalties(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Penalties";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveScoring")]
		public IActionResult SaveScoring(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Scoring";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveNumber_of_players")]
		public IActionResult SaveNumber_of_players()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Number_of_players";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var SportID = Convert.ToInt64(obj["SportID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEquipment")]
		public IActionResult SaveEquipment(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Equipment";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePlay_area")]
		public IActionResult SavePlay_area(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Play_area";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMost_important_muscles")]
		public IActionResult SaveMost_important_muscles(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Most_important_muscles";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCommon_injuries")]
		public IActionResult SaveCommon_injuries(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Common_injuries";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveStrategies")]
		public IActionResult SaveStrategies(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Strategies";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SavePositions")]
		public IActionResult SavePositions()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Positions";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var SportID = Convert.ToInt64(obj["SportID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveGame_time")]
		public IActionResult SaveGame_time()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Game_time";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var SportID = Convert.ToInt64(obj["SportID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRules")]
		public IActionResult SaveRules(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Rules";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
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
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTeams")]
		public IActionResult SaveTeams(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Teams";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
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
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePlayers")]
		public IActionResult SavePlayers(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Players";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePopularity")]
		public IActionResult SavePopularity(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Popularity";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMerchandise")]
		public IActionResult SaveMerchandise(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Merchandise";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveUniforms")]
		public IActionResult SaveUniforms(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Uniforms";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFamous_games")]
		public IActionResult SaveFamous_games(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Famous_games";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
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
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCreators")]
		public IActionResult SaveCreators(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Creators";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOrigin_story")]
		public IActionResult SaveOrigin_story(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Origin_story";
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
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
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
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
			var SportID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			SportsModel model = new SportsModel();
			model.id = SportID;
			model._id = SportID;
			model.column_type = type;
			model.column_value = value;
			response = _iSportsApiService.SaveSport(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("SportID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "sports");
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
								contentObjectAttachmentModel.content_type = "sports";
			
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
			string content_Id = HttpContext.Session.GetString("SportID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "sports";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewSports", "sports", new { id = content_Id }, "Gallery_panel");

		}

	}
}
