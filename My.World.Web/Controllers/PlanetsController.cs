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
	[Route("planets")]
	public class PlanetsController : Controller
	{
		public readonly IPlanetsApiService _iPlanetsApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public PlanetsController(IPlanetsApiService iPlanetsApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iPlanetsApiService = iPlanetsApiService;
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
			var planets = _iPlanetsApiService.GetAllPlanets(accountID);
			planets.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Planets.png");
			    }
			});
			return View(planets);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewPlanets(string Id)
		{
			PlanetsModel model = new PlanetsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["PlanetID"] = Id;
			ViewData["PlanetID"] = Id;
			HttpContext.Session.SetString("PlanetID", Id);
			
			PlanetsViewModel planetsViewModel = new PlanetsViewModel(_iObjectBucketApiService);
			planetsViewModel.planetsModel = _iPlanetsApiService.GetPlanets(model);
			if (planetsViewModel.planetsModel == null)
				return RedirectToAction("Index", "NotFound");
			planetsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			planetsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "planets");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Planets" });
			planetsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			planetsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			planetsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "planets");
			return View(planetsViewModel);

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewPlanets(string Id)
		{
			PlanetsModel model = new PlanetsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["PlanetID"] = Id;
			ViewData["PlanetID"] = Id;
			HttpContext.Session.SetString("PlanetID", Id);
			
			PlanetsViewModel planetsViewModel = new PlanetsViewModel(_iObjectBucketApiService);
			planetsViewModel.planetsModel = _iPlanetsApiService.GetPlanets(model);
			if (planetsViewModel.planetsModel == null)
			return RedirectToAction("Index", "NotFound");
			planetsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(planetsViewModel.planetsModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			planetsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "planets");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Planets" });
			planetsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			planetsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			planetsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "planets");
			return View(planetsViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeletePlanets(string Id)
		{
			PlanetsModel model = new PlanetsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iPlanetsApiService.DeletePlanets(model);
			return RedirectToAction("Index");

		}

		public void TransformData(PlanetsModel model)
		{
			if (model != null)
			{
				
				model.Atmosphere  = model.Atmosphere == null ? model.Atmosphere : model.Atmosphere.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Calendar_System  = model.Calendar_System == null ? model.Calendar_System : model.Calendar_System.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Climate  = model.Climate == null ? model.Climate : model.Climate.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Continents  = model.Continents == null ? model.Continents : model.Continents.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Countries  = model.Countries == null ? model.Countries : model.Countries.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Creatures  = model.Creatures == null ? model.Creatures : model.Creatures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Day_sky  = model.Day_sky == null ? model.Day_sky : model.Day_sky.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Deities  = model.Deities == null ? model.Deities : model.Deities.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.First_Inhabitants_Story  = model.First_Inhabitants_Story == null ? model.First_Inhabitants_Story : model.First_Inhabitants_Story.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Flora  = model.Flora == null ? model.Flora : model.Flora.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Groups  = model.Groups == null ? model.Groups : model.Groups.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Landmarks  = model.Landmarks == null ? model.Landmarks : model.Landmarks.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Languages  = model.Languages == null ? model.Languages : model.Languages.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Locations  = model.Locations == null ? model.Locations : model.Locations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Moons  = model.Moons == null ? model.Moons : model.Moons.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Natural_diasters  = model.Natural_diasters == null ? model.Natural_diasters : model.Natural_diasters.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Natural_Resources  = model.Natural_Resources == null ? model.Natural_Resources : model.Natural_Resources.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Nearby_planets  = model.Nearby_planets == null ? model.Nearby_planets : model.Nearby_planets.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Night_sky  = model.Night_sky == null ? model.Night_sky : model.Night_sky.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Orbit  = model.Orbit == null ? model.Orbit : model.Orbit.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_Notes  = model.Private_Notes == null ? model.Private_Notes : model.Private_Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Races  = model.Races == null ? model.Races : model.Races.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Religions  = model.Religions == null ? model.Religions : model.Religions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Seasons  = model.Seasons == null ? model.Seasons : model.Seasons.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Suns  = model.Suns == null ? model.Suns : model.Suns.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Towns  = model.Towns == null ? model.Towns : model.Towns.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Visible_Constellations  = model.Visible_Constellations == null ? model.Visible_Constellations : model.Visible_Constellations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Weather  = model.Weather == null ? model.Weather : model.Weather.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.World_History  = model.World_History == null ? model.World_History : model.World_History.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
			}

		}

		#region Save Properties Methods
		[HttpPost]
		[Route("{id}/SaveAtmosphere")]
		public IActionResult SaveAtmosphere(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Atmosphere";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCalendar_System")]
		public IActionResult SaveCalendar_System(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Calendar_System";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveClimate")]
		public IActionResult SaveClimate(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Climate";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveDay_sky")]
		public IActionResult SaveDay_sky(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Day_sky";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFirst_Inhabitants_Story")]
		public IActionResult SaveFirst_Inhabitants_Story(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "First_Inhabitants_Story";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveLanguages")]
		public IActionResult SaveLanguages(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Languages";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveLength_Of_Day")]
		public IActionResult SaveLength_Of_Day()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Length_Of_Day";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var PlanetID = Convert.ToInt64(obj["PlanetID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveLength_Of_Night")]
		public IActionResult SaveLength_Of_Night()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Length_Of_Night";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var PlanetID = Convert.ToInt64(obj["PlanetID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMoons")]
		public IActionResult SaveMoons(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Moons";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(obj["PlanetID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNatural_diasters")]
		public IActionResult SaveNatural_diasters(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Natural_diasters";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNatural_Resources")]
		public IActionResult SaveNatural_Resources(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Natural_Resources";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNearby_planets")]
		public IActionResult SaveNearby_planets(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Nearby_planets";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNight_sky")]
		public IActionResult SaveNight_sky(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Night_sky";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOrbit")]
		public IActionResult SaveOrbit(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Orbit";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SavePopulation")]
		public IActionResult SavePopulation()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Population";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var PlanetID = Convert.ToInt64(obj["PlanetID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSeasons")]
		public IActionResult SaveSeasons(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Seasons";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(obj["PlanetID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSuns")]
		public IActionResult SaveSuns(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Suns";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveSurface")]
		public IActionResult SaveSurface()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Surface";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var PlanetID = Convert.ToInt64(obj["PlanetID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveTemperature")]
		public IActionResult SaveTemperature()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Temperature";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var PlanetID = Convert.ToInt64(obj["PlanetID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
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
			var PlanetID = Convert.ToInt64(obj["PlanetID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveVisible_Constellations")]
		public IActionResult SaveVisible_Constellations(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Visible_Constellations";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveWater_Content")]
		public IActionResult SaveWater_Content()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Water_Content";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var PlanetID = Convert.ToInt64(obj["PlanetID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveWeather")]
		public IActionResult SaveWeather(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Weather";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveWorld_History")]
		public IActionResult SaveWorld_History(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "World_History";
			var PlanetID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			PlanetsModel model = new PlanetsModel();
			model.id = PlanetID;
			model._id = PlanetID;
			model.column_type = type;
			model.column_value = value;
			response = _iPlanetsApiService.SavePlanet(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("PlanetsID");
			
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
			                contentObjectAttachmentModel.content_type = "planets";
			
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
			string content_Id = HttpContext.Session.GetString("PlanetsID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "planets";
			
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewPlanets", "planets", new { id = content_Id }, "Gallery_panel");

		}

	}
}
