using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using My.World.Api.DataAccess;
using My.World.Api.Models;
using My.World.Api.Services;

namespace My.World.Api.Controllers
{
	[ApiController]
	[Route("Content")]
	public class ContentController : ControllerBase
	{
		public DBContext _dbContext;

		public readonly ILogger <ContentController> _logger;


		public ContentController(IServiceProvider services,ILogger<ContentController> logger)
		{
			_dbContext = services.GetService(typeof(DBContext)) as DBContext;
			_logger = logger;

		}

		#region Buildings
		[HttpPost]
		[Route("Buildings/AddBuildings")]
		public IActionResult AddBuildings(BuildingsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var BuildingsService = new BuildingsService(_dbContext);
                responseModel = BuildingsService.AddBuildingsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Buildings/GetBuildings")]
		public IActionResult GetBuildings(BuildingsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<BuildingsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var BuildingsService = new BuildingsService(_dbContext);
                responseModel = BuildingsService.GetBuildingsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Buildings/DeleteBuildings")]
		public IActionResult DeleteBuildings(BuildingsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var BuildingsService = new BuildingsService(_dbContext);
                responseModel = BuildingsService.DeleteBuildingsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Buildings/GetAllBuildings/{userId}")]
		public IActionResult GetAllBuildingsForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<BuildingsModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var BuildingsService = new BuildingsService(_dbContext);
                responseModel = BuildingsService.GetAllBuildingsForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Buildings/UpdateBuildings")]
		public IActionResult UpdateBuildings(BuildingsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var BuildingsService = new BuildingsService(_dbContext);
                responseModel = BuildingsService.UpdateBuildingsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Buildings/SaveBuilding")]
		public IActionResult SaveBuilding(BuildingsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var BuildingsService = new BuildingsService(_dbContext);
                responseModel = BuildingsService.SaveBuilding(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Buildings

		#region Characters
		[HttpPost]
		[Route("Characters/AddCharacters")]
		public IActionResult AddCharacters(CharactersModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CharactersService = new CharactersService(_dbContext);
                responseModel = CharactersService.AddCharactersData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Characters/GetCharacters")]
		public IActionResult GetCharacters(CharactersModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<CharactersModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CharactersService = new CharactersService(_dbContext);
                responseModel = CharactersService.GetCharactersData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Characters/DeleteCharacters")]
		public IActionResult DeleteCharacters(CharactersModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CharactersService = new CharactersService(_dbContext);
                responseModel = CharactersService.DeleteCharactersData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Characters/GetAllCharacters/{userId}")]
		public IActionResult GetAllCharactersForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<CharactersModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CharactersService = new CharactersService(_dbContext);
                responseModel = CharactersService.GetAllCharactersForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Characters/UpdateCharacters")]
		public IActionResult UpdateCharacters(CharactersModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CharactersService = new CharactersService(_dbContext);
                responseModel = CharactersService.UpdateCharactersData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Characters/SaveCharacter")]
		public IActionResult SaveCharacter(CharactersModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CharactersService = new CharactersService(_dbContext);
                responseModel = CharactersService.SaveCharacter(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Characters

		#region Conditions
		[HttpPost]
		[Route("Conditions/AddConditions")]
		public IActionResult AddConditions(ConditionsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ConditionsService = new ConditionsService(_dbContext);
                responseModel = ConditionsService.AddConditionsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Conditions/GetConditions")]
		public IActionResult GetConditions(ConditionsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<ConditionsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ConditionsService = new ConditionsService(_dbContext);
                responseModel = ConditionsService.GetConditionsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Conditions/DeleteConditions")]
		public IActionResult DeleteConditions(ConditionsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ConditionsService = new ConditionsService(_dbContext);
                responseModel = ConditionsService.DeleteConditionsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Conditions/GetAllConditions/{userId}")]
		public IActionResult GetAllConditionsForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<ConditionsModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ConditionsService = new ConditionsService(_dbContext);
                responseModel = ConditionsService.GetAllConditionsForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Conditions/UpdateConditions")]
		public IActionResult UpdateConditions(ConditionsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ConditionsService = new ConditionsService(_dbContext);
                responseModel = ConditionsService.UpdateConditionsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Conditions/SaveCondition")]
		public IActionResult SaveCondition(ConditionsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ConditionsService = new ConditionsService(_dbContext);
                responseModel = ConditionsService.SaveCondition(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Conditions

		#region Continents
		[HttpPost]
		[Route("Continents/AddContinents")]
		public IActionResult AddContinents(ContinentsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ContinentsService = new ContinentsService(_dbContext);
                responseModel = ContinentsService.AddContinentsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Continents/GetContinents")]
		public IActionResult GetContinents(ContinentsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<ContinentsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ContinentsService = new ContinentsService(_dbContext);
                responseModel = ContinentsService.GetContinentsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Continents/DeleteContinents")]
		public IActionResult DeleteContinents(ContinentsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ContinentsService = new ContinentsService(_dbContext);
                responseModel = ContinentsService.DeleteContinentsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Continents/GetAllContinents/{userId}")]
		public IActionResult GetAllContinentsForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<ContinentsModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ContinentsService = new ContinentsService(_dbContext);
                responseModel = ContinentsService.GetAllContinentsForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Continents/UpdateContinents")]
		public IActionResult UpdateContinents(ContinentsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ContinentsService = new ContinentsService(_dbContext);
                responseModel = ContinentsService.UpdateContinentsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Continents/SaveContinent")]
		public IActionResult SaveContinent(ContinentsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ContinentsService = new ContinentsService(_dbContext);
                responseModel = ContinentsService.SaveContinent(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Continents

		#region Countries
		[HttpPost]
		[Route("Countries/AddCountries")]
		public IActionResult AddCountries(CountriesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CountriesService = new CountriesService(_dbContext);
                responseModel = CountriesService.AddCountriesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Countries/GetCountries")]
		public IActionResult GetCountries(CountriesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<CountriesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CountriesService = new CountriesService(_dbContext);
                responseModel = CountriesService.GetCountriesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Countries/DeleteCountries")]
		public IActionResult DeleteCountries(CountriesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CountriesService = new CountriesService(_dbContext);
                responseModel = CountriesService.DeleteCountriesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Countries/GetAllCountries/{userId}")]
		public IActionResult GetAllCountriesForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<CountriesModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CountriesService = new CountriesService(_dbContext);
                responseModel = CountriesService.GetAllCountriesForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Countries/UpdateCountries")]
		public IActionResult UpdateCountries(CountriesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CountriesService = new CountriesService(_dbContext);
                responseModel = CountriesService.UpdateCountriesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Countries/SaveCountrie")]
		public IActionResult SaveCountrie(CountriesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CountriesService = new CountriesService(_dbContext);
                responseModel = CountriesService.SaveCountrie(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Countries

		#region Creatures
		[HttpPost]
		[Route("Creatures/AddCreatures")]
		public IActionResult AddCreatures(CreaturesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CreaturesService = new CreaturesService(_dbContext);
                responseModel = CreaturesService.AddCreaturesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Creatures/GetCreatures")]
		public IActionResult GetCreatures(CreaturesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<CreaturesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CreaturesService = new CreaturesService(_dbContext);
                responseModel = CreaturesService.GetCreaturesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Creatures/DeleteCreatures")]
		public IActionResult DeleteCreatures(CreaturesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CreaturesService = new CreaturesService(_dbContext);
                responseModel = CreaturesService.DeleteCreaturesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Creatures/GetAllCreatures/{userId}")]
		public IActionResult GetAllCreaturesForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<CreaturesModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CreaturesService = new CreaturesService(_dbContext);
                responseModel = CreaturesService.GetAllCreaturesForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Creatures/UpdateCreatures")]
		public IActionResult UpdateCreatures(CreaturesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CreaturesService = new CreaturesService(_dbContext);
                responseModel = CreaturesService.UpdateCreaturesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Creatures/SaveCreature")]
		public IActionResult SaveCreature(CreaturesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CreaturesService = new CreaturesService(_dbContext);
                responseModel = CreaturesService.SaveCreature(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Creatures

		#region Deities
		[HttpPost]
		[Route("Deities/AddDeities")]
		public IActionResult AddDeities(DeitiesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var DeitiesService = new DeitiesService(_dbContext);
                responseModel = DeitiesService.AddDeitiesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Deities/GetDeities")]
		public IActionResult GetDeities(DeitiesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<DeitiesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var DeitiesService = new DeitiesService(_dbContext);
                responseModel = DeitiesService.GetDeitiesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Deities/DeleteDeities")]
		public IActionResult DeleteDeities(DeitiesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var DeitiesService = new DeitiesService(_dbContext);
                responseModel = DeitiesService.DeleteDeitiesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Deities/GetAllDeities/{userId}")]
		public IActionResult GetAllDeitiesForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<DeitiesModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var DeitiesService = new DeitiesService(_dbContext);
                responseModel = DeitiesService.GetAllDeitiesForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Deities/UpdateDeities")]
		public IActionResult UpdateDeities(DeitiesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var DeitiesService = new DeitiesService(_dbContext);
                responseModel = DeitiesService.UpdateDeitiesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Deities/SaveDeitie")]
		public IActionResult SaveDeitie(DeitiesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var DeitiesService = new DeitiesService(_dbContext);
                responseModel = DeitiesService.SaveDeitie(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Deities

		#region Floras
		[HttpPost]
		[Route("Floras/AddFloras")]
		public IActionResult AddFloras(FlorasModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FlorasService = new FlorasService(_dbContext);
                responseModel = FlorasService.AddFlorasData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Floras/GetFloras")]
		public IActionResult GetFloras(FlorasModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<FlorasModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FlorasService = new FlorasService(_dbContext);
                responseModel = FlorasService.GetFlorasData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Floras/DeleteFloras")]
		public IActionResult DeleteFloras(FlorasModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FlorasService = new FlorasService(_dbContext);
                responseModel = FlorasService.DeleteFlorasData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Floras/GetAllFloras/{userId}")]
		public IActionResult GetAllFlorasForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<FlorasModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FlorasService = new FlorasService(_dbContext);
                responseModel = FlorasService.GetAllFlorasForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Floras/UpdateFloras")]
		public IActionResult UpdateFloras(FlorasModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FlorasService = new FlorasService(_dbContext);
                responseModel = FlorasService.UpdateFlorasData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Floras/SaveFlora")]
		public IActionResult SaveFlora(FlorasModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FlorasService = new FlorasService(_dbContext);
                responseModel = FlorasService.SaveFlora(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Floras

		#region Foods
		[HttpPost]
		[Route("Foods/AddFoods")]
		public IActionResult AddFoods(FoodsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FoodsService = new FoodsService(_dbContext);
                responseModel = FoodsService.AddFoodsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Foods/GetFoods")]
		public IActionResult GetFoods(FoodsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<FoodsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FoodsService = new FoodsService(_dbContext);
                responseModel = FoodsService.GetFoodsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Foods/DeleteFoods")]
		public IActionResult DeleteFoods(FoodsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FoodsService = new FoodsService(_dbContext);
                responseModel = FoodsService.DeleteFoodsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Foods/GetAllFoods/{userId}")]
		public IActionResult GetAllFoodsForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<FoodsModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FoodsService = new FoodsService(_dbContext);
                responseModel = FoodsService.GetAllFoodsForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Foods/UpdateFoods")]
		public IActionResult UpdateFoods(FoodsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FoodsService = new FoodsService(_dbContext);
                responseModel = FoodsService.UpdateFoodsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Foods/SaveFood")]
		public IActionResult SaveFood(FoodsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FoodsService = new FoodsService(_dbContext);
                responseModel = FoodsService.SaveFood(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Foods

		#region Governments
		[HttpPost]
		[Route("Governments/AddGovernments")]
		public IActionResult AddGovernments(GovernmentsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var GovernmentsService = new GovernmentsService(_dbContext);
                responseModel = GovernmentsService.AddGovernmentsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Governments/GetGovernments")]
		public IActionResult GetGovernments(GovernmentsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<GovernmentsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var GovernmentsService = new GovernmentsService(_dbContext);
                responseModel = GovernmentsService.GetGovernmentsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Governments/DeleteGovernments")]
		public IActionResult DeleteGovernments(GovernmentsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var GovernmentsService = new GovernmentsService(_dbContext);
                responseModel = GovernmentsService.DeleteGovernmentsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Governments/GetAllGovernments/{userId}")]
		public IActionResult GetAllGovernmentsForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<GovernmentsModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var GovernmentsService = new GovernmentsService(_dbContext);
                responseModel = GovernmentsService.GetAllGovernmentsForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Governments/UpdateGovernments")]
		public IActionResult UpdateGovernments(GovernmentsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var GovernmentsService = new GovernmentsService(_dbContext);
                responseModel = GovernmentsService.UpdateGovernmentsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Governments/SaveGovernment")]
		public IActionResult SaveGovernment(GovernmentsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var GovernmentsService = new GovernmentsService(_dbContext);
                responseModel = GovernmentsService.SaveGovernment(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Governments

		#region Groups
		[HttpPost]
		[Route("Groups/AddGroups")]
		public IActionResult AddGroups(GroupsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var GroupsService = new GroupsService(_dbContext);
                responseModel = GroupsService.AddGroupsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Groups/GetGroups")]
		public IActionResult GetGroups(GroupsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<GroupsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var GroupsService = new GroupsService(_dbContext);
                responseModel = GroupsService.GetGroupsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Groups/DeleteGroups")]
		public IActionResult DeleteGroups(GroupsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var GroupsService = new GroupsService(_dbContext);
                responseModel = GroupsService.DeleteGroupsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Groups/GetAllGroups/{userId}")]
		public IActionResult GetAllGroupsForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<GroupsModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var GroupsService = new GroupsService(_dbContext);
                responseModel = GroupsService.GetAllGroupsForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Groups/UpdateGroups")]
		public IActionResult UpdateGroups(GroupsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var GroupsService = new GroupsService(_dbContext);
                responseModel = GroupsService.UpdateGroupsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Groups/SaveGroup")]
		public IActionResult SaveGroup(GroupsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var GroupsService = new GroupsService(_dbContext);
                responseModel = GroupsService.SaveGroup(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Groups

		#region Items
		[HttpPost]
		[Route("Items/AddItems")]
		public IActionResult AddItems(ItemsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ItemsService = new ItemsService(_dbContext);
                responseModel = ItemsService.AddItemsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Items/GetItems")]
		public IActionResult GetItems(ItemsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<ItemsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ItemsService = new ItemsService(_dbContext);
                responseModel = ItemsService.GetItemsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Items/DeleteItems")]
		public IActionResult DeleteItems(ItemsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ItemsService = new ItemsService(_dbContext);
                responseModel = ItemsService.DeleteItemsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Items/GetAllItems/{userId}")]
		public IActionResult GetAllItemsForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<ItemsModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ItemsService = new ItemsService(_dbContext);
                responseModel = ItemsService.GetAllItemsForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Items/UpdateItems")]
		public IActionResult UpdateItems(ItemsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ItemsService = new ItemsService(_dbContext);
                responseModel = ItemsService.UpdateItemsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Items/SaveItem")]
		public IActionResult SaveItem(ItemsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ItemsService = new ItemsService(_dbContext);
                responseModel = ItemsService.SaveItem(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Items

		#region Jobs
		[HttpPost]
		[Route("Jobs/AddJobs")]
		public IActionResult AddJobs(JobsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var JobsService = new JobsService(_dbContext);
                responseModel = JobsService.AddJobsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Jobs/GetJobs")]
		public IActionResult GetJobs(JobsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<JobsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var JobsService = new JobsService(_dbContext);
                responseModel = JobsService.GetJobsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Jobs/DeleteJobs")]
		public IActionResult DeleteJobs(JobsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var JobsService = new JobsService(_dbContext);
                responseModel = JobsService.DeleteJobsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Jobs/GetAllJobs/{userId}")]
		public IActionResult GetAllJobsForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<JobsModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var JobsService = new JobsService(_dbContext);
                responseModel = JobsService.GetAllJobsForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Jobs/UpdateJobs")]
		public IActionResult UpdateJobs(JobsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var JobsService = new JobsService(_dbContext);
                responseModel = JobsService.UpdateJobsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Jobs/SaveJob")]
		public IActionResult SaveJob(JobsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var JobsService = new JobsService(_dbContext);
                responseModel = JobsService.SaveJob(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Jobs

		#region Landmarks
		[HttpPost]
		[Route("Landmarks/AddLandmarks")]
		public IActionResult AddLandmarks(LandmarksModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LandmarksService = new LandmarksService(_dbContext);
                responseModel = LandmarksService.AddLandmarksData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Landmarks/GetLandmarks")]
		public IActionResult GetLandmarks(LandmarksModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<LandmarksModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LandmarksService = new LandmarksService(_dbContext);
                responseModel = LandmarksService.GetLandmarksData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Landmarks/DeleteLandmarks")]
		public IActionResult DeleteLandmarks(LandmarksModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LandmarksService = new LandmarksService(_dbContext);
                responseModel = LandmarksService.DeleteLandmarksData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Landmarks/GetAllLandmarks/{userId}")]
		public IActionResult GetAllLandmarksForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<LandmarksModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LandmarksService = new LandmarksService(_dbContext);
                responseModel = LandmarksService.GetAllLandmarksForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Landmarks/UpdateLandmarks")]
		public IActionResult UpdateLandmarks(LandmarksModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LandmarksService = new LandmarksService(_dbContext);
                responseModel = LandmarksService.UpdateLandmarksData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Landmarks/SaveLandmark")]
		public IActionResult SaveLandmark(LandmarksModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LandmarksService = new LandmarksService(_dbContext);
                responseModel = LandmarksService.SaveLandmark(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Landmarks

		#region Languages
		[HttpPost]
		[Route("Languages/AddLanguages")]
		public IActionResult AddLanguages(LanguagesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LanguagesService = new LanguagesService(_dbContext);
                responseModel = LanguagesService.AddLanguagesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Languages/GetLanguages")]
		public IActionResult GetLanguages(LanguagesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<LanguagesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LanguagesService = new LanguagesService(_dbContext);
                responseModel = LanguagesService.GetLanguagesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Languages/DeleteLanguages")]
		public IActionResult DeleteLanguages(LanguagesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LanguagesService = new LanguagesService(_dbContext);
                responseModel = LanguagesService.DeleteLanguagesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Languages/GetAllLanguages/{userId}")]
		public IActionResult GetAllLanguagesForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<LanguagesModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LanguagesService = new LanguagesService(_dbContext);
                responseModel = LanguagesService.GetAllLanguagesForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Languages/UpdateLanguages")]
		public IActionResult UpdateLanguages(LanguagesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LanguagesService = new LanguagesService(_dbContext);
                responseModel = LanguagesService.UpdateLanguagesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Languages/SaveLanguage")]
		public IActionResult SaveLanguage(LanguagesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LanguagesService = new LanguagesService(_dbContext);
                responseModel = LanguagesService.SaveLanguage(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Languages

		#region Locations
		[HttpPost]
		[Route("Locations/AddLocations")]
		public IActionResult AddLocations(LocationsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LocationsService = new LocationsService(_dbContext);
                responseModel = LocationsService.AddLocationsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Locations/GetLocations")]
		public IActionResult GetLocations(LocationsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<LocationsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LocationsService = new LocationsService(_dbContext);
                responseModel = LocationsService.GetLocationsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Locations/DeleteLocations")]
		public IActionResult DeleteLocations(LocationsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LocationsService = new LocationsService(_dbContext);
                responseModel = LocationsService.DeleteLocationsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Locations/GetAllLocations/{userId}")]
		public IActionResult GetAllLocationsForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<LocationsModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LocationsService = new LocationsService(_dbContext);
                responseModel = LocationsService.GetAllLocationsForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Locations/UpdateLocations")]
		public IActionResult UpdateLocations(LocationsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LocationsService = new LocationsService(_dbContext);
                responseModel = LocationsService.UpdateLocationsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Locations/SaveLocation")]
		public IActionResult SaveLocation(LocationsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LocationsService = new LocationsService(_dbContext);
                responseModel = LocationsService.SaveLocation(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Locations

		#region Lores
		[HttpPost]
		[Route("Lores/AddLores")]
		public IActionResult AddLores(LoresModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LoresService = new LoresService(_dbContext);
                responseModel = LoresService.AddLoresData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Lores/GetLores")]
		public IActionResult GetLores(LoresModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<LoresModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LoresService = new LoresService(_dbContext);
                responseModel = LoresService.GetLoresData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Lores/DeleteLores")]
		public IActionResult DeleteLores(LoresModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LoresService = new LoresService(_dbContext);
                responseModel = LoresService.DeleteLoresData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Lores/GetAllLores/{userId}")]
		public IActionResult GetAllLoresForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<LoresModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LoresService = new LoresService(_dbContext);
                responseModel = LoresService.GetAllLoresForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Lores/UpdateLores")]
		public IActionResult UpdateLores(LoresModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LoresService = new LoresService(_dbContext);
                responseModel = LoresService.UpdateLoresData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Lores/SaveLore")]
		public IActionResult SaveLore(LoresModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LoresService = new LoresService(_dbContext);
                responseModel = LoresService.SaveLore(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Lores

		#region Magics
		[HttpPost]
		[Route("Magics/AddMagics")]
		public IActionResult AddMagics(MagicsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var MagicsService = new MagicsService(_dbContext);
                responseModel = MagicsService.AddMagicsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Magics/GetMagics")]
		public IActionResult GetMagics(MagicsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<MagicsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var MagicsService = new MagicsService(_dbContext);
                responseModel = MagicsService.GetMagicsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Magics/DeleteMagics")]
		public IActionResult DeleteMagics(MagicsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var MagicsService = new MagicsService(_dbContext);
                responseModel = MagicsService.DeleteMagicsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Magics/GetAllMagics/{userId}")]
		public IActionResult GetAllMagicsForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<MagicsModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var MagicsService = new MagicsService(_dbContext);
                responseModel = MagicsService.GetAllMagicsForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Magics/UpdateMagics")]
		public IActionResult UpdateMagics(MagicsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var MagicsService = new MagicsService(_dbContext);
                responseModel = MagicsService.UpdateMagicsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Magics/SaveMagic")]
		public IActionResult SaveMagic(MagicsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var MagicsService = new MagicsService(_dbContext);
                responseModel = MagicsService.SaveMagic(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
        #endregion Magics

        #region Organizations
        [HttpPost]
        [Route("Organizations/AddOrganizations")]
        public IActionResult AddOrganizations(OrganizationsModel model)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var OrganizationsService = new OrganizationsService(_dbContext);
                responseModel = OrganizationsService.AddOrganizationsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("Organizations/GetOrganizations")]
        public IActionResult GetOrganizations(OrganizationsModel model)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<OrganizationsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var OrganizationsService = new OrganizationsService(_dbContext);
                responseModel = OrganizationsService.GetOrganizationsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("Organizations/DeleteOrganizations")]
        public IActionResult DeleteOrganizations(OrganizationsModel model)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var OrganizationsService = new OrganizationsService(_dbContext);
                responseModel = OrganizationsService.DeleteOrganizationsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpGet]
        [Route("Organizations/GetAllOrganizations/{userId}")]
        public IActionResult GetAllOrganizationsForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<OrganizationsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var OrganizationsService = new OrganizationsService(_dbContext);
                responseModel = OrganizationsService.GetAllOrganizationsForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("Organizations/UpdateOrganizations")]
        public IActionResult UpdateOrganizations(OrganizationsModel model)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var OrganizationsService = new OrganizationsService(_dbContext);
                responseModel = OrganizationsService.UpdateOrganizationsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("Organizations/SaveOrganization")]
        public IActionResult SaveOrganization(OrganizationsModel model)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var OrganizationsService = new OrganizationsService(_dbContext);
                responseModel = OrganizationsService.SaveOrganization(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }
        #endregion Organizations

        #region Planets
        [HttpPost]
		[Route("Planets/AddPlanets")]
		public IActionResult AddPlanets(PlanetsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var PlanetsService = new PlanetsService(_dbContext);
                responseModel = PlanetsService.AddPlanetsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Planets/GetPlanets")]
		public IActionResult GetPlanets(PlanetsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<PlanetsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var PlanetsService = new PlanetsService(_dbContext);
                responseModel = PlanetsService.GetPlanetsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Planets/DeletePlanets")]
		public IActionResult DeletePlanets(PlanetsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var PlanetsService = new PlanetsService(_dbContext);
                responseModel = PlanetsService.DeletePlanetsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Planets/GetAllPlanets/{userId}")]
		public IActionResult GetAllPlanetsForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<PlanetsModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var PlanetsService = new PlanetsService(_dbContext);
                responseModel = PlanetsService.GetAllPlanetsForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Planets/UpdatePlanets")]
		public IActionResult UpdatePlanets(PlanetsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var PlanetsService = new PlanetsService(_dbContext);
                responseModel = PlanetsService.UpdatePlanetsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Planets/SavePlanet")]
		public IActionResult SavePlanet(PlanetsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var PlanetsService = new PlanetsService(_dbContext);
                responseModel = PlanetsService.SavePlanet(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Planets

		#region Races
		[HttpPost]
		[Route("Races/AddRaces")]
		public IActionResult AddRaces(RacesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var RacesService = new RacesService(_dbContext);
                responseModel = RacesService.AddRacesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Races/GetRaces")]
		public IActionResult GetRaces(RacesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<RacesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var RacesService = new RacesService(_dbContext);
                responseModel = RacesService.GetRacesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Races/DeleteRaces")]
		public IActionResult DeleteRaces(RacesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var RacesService = new RacesService(_dbContext);
                responseModel = RacesService.DeleteRacesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Races/GetAllRaces/{userId}")]
		public IActionResult GetAllRacesForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<RacesModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var RacesService = new RacesService(_dbContext);
                responseModel = RacesService.GetAllRacesForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Races/UpdateRaces")]
		public IActionResult UpdateRaces(RacesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var RacesService = new RacesService(_dbContext);
                responseModel = RacesService.UpdateRacesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Races/SaveRace")]
		public IActionResult SaveRace(RacesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var RacesService = new RacesService(_dbContext);
                responseModel = RacesService.SaveRace(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Races

		#region Religions
		[HttpPost]
		[Route("Religions/AddReligions")]
		public IActionResult AddReligions(ReligionsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ReligionsService = new ReligionsService(_dbContext);
                responseModel = ReligionsService.AddReligionsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Religions/GetReligions")]
		public IActionResult GetReligions(ReligionsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<ReligionsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ReligionsService = new ReligionsService(_dbContext);
                responseModel = ReligionsService.GetReligionsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Religions/DeleteReligions")]
		public IActionResult DeleteReligions(ReligionsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ReligionsService = new ReligionsService(_dbContext);
                responseModel = ReligionsService.DeleteReligionsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Religions/GetAllReligions/{userId}")]
		public IActionResult GetAllReligionsForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<ReligionsModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ReligionsService = new ReligionsService(_dbContext);
                responseModel = ReligionsService.GetAllReligionsForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Religions/UpdateReligions")]
		public IActionResult UpdateReligions(ReligionsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ReligionsService = new ReligionsService(_dbContext);
                responseModel = ReligionsService.UpdateReligionsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Religions/SaveReligion")]
		public IActionResult SaveReligion(ReligionsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ReligionsService = new ReligionsService(_dbContext);
                responseModel = ReligionsService.SaveReligion(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Religions

		#region Scenes
		[HttpPost]
		[Route("Scenes/AddScenes")]
		public IActionResult AddScenes(ScenesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ScenesService = new ScenesService(_dbContext);
                responseModel = ScenesService.AddScenesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Scenes/GetScenes")]
		public IActionResult GetScenes(ScenesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<ScenesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ScenesService = new ScenesService(_dbContext);
                responseModel = ScenesService.GetScenesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Scenes/DeleteScenes")]
		public IActionResult DeleteScenes(ScenesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ScenesService = new ScenesService(_dbContext);
                responseModel = ScenesService.DeleteScenesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Scenes/GetAllScenes/{userId}")]
		public IActionResult GetAllScenesForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<ScenesModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ScenesService = new ScenesService(_dbContext);
                responseModel = ScenesService.GetAllScenesForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Scenes/UpdateScenes")]
		public IActionResult UpdateScenes(ScenesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ScenesService = new ScenesService(_dbContext);
                responseModel = ScenesService.UpdateScenesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Scenes/SaveScene")]
		public IActionResult SaveScene(ScenesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ScenesService = new ScenesService(_dbContext);
                responseModel = ScenesService.SaveScene(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Scenes

		#region Sports
		[HttpPost]
		[Route("Sports/AddSports")]
		public IActionResult AddSports(SportsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var SportsService = new SportsService(_dbContext);
                responseModel = SportsService.AddSportsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Sports/GetSports")]
		public IActionResult GetSports(SportsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<SportsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var SportsService = new SportsService(_dbContext);
                responseModel = SportsService.GetSportsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Sports/DeleteSports")]
		public IActionResult DeleteSports(SportsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var SportsService = new SportsService(_dbContext);
                responseModel = SportsService.DeleteSportsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Sports/GetAllSports/{userId}")]
		public IActionResult GetAllSportsForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<SportsModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var SportsService = new SportsService(_dbContext);
                responseModel = SportsService.GetAllSportsForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Sports/UpdateSports")]
		public IActionResult UpdateSports(SportsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var SportsService = new SportsService(_dbContext);
                responseModel = SportsService.UpdateSportsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Sports/SaveSport")]
		public IActionResult SaveSport(SportsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var SportsService = new SportsService(_dbContext);
                responseModel = SportsService.SaveSport(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Sports

		#region Technologies
		[HttpPost]
		[Route("Technologies/AddTechnologies")]
		public IActionResult AddTechnologies(TechnologiesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TechnologiesService = new TechnologiesService(_dbContext);
                responseModel = TechnologiesService.AddTechnologiesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Technologies/GetTechnologies")]
		public IActionResult GetTechnologies(TechnologiesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<TechnologiesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TechnologiesService = new TechnologiesService(_dbContext);
                responseModel = TechnologiesService.GetTechnologiesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Technologies/DeleteTechnologies")]
		public IActionResult DeleteTechnologies(TechnologiesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TechnologiesService = new TechnologiesService(_dbContext);
                responseModel = TechnologiesService.DeleteTechnologiesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Technologies/GetAllTechnologies/{userId}")]
		public IActionResult GetAllTechnologiesForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<TechnologiesModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TechnologiesService = new TechnologiesService(_dbContext);
                responseModel = TechnologiesService.GetAllTechnologiesForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Technologies/UpdateTechnologies")]
		public IActionResult UpdateTechnologies(TechnologiesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TechnologiesService = new TechnologiesService(_dbContext);
                responseModel = TechnologiesService.UpdateTechnologiesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Technologies/SaveTechnologie")]
		public IActionResult SaveTechnologie(TechnologiesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TechnologiesService = new TechnologiesService(_dbContext);
                responseModel = TechnologiesService.SaveTechnologie(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Technologies

		#region Towns
		[HttpPost]
		[Route("Towns/AddTowns")]
		public IActionResult AddTowns(TownsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TownsService = new TownsService(_dbContext);
                responseModel = TownsService.AddTownsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Towns/GetTowns")]
		public IActionResult GetTowns(TownsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<TownsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TownsService = new TownsService(_dbContext);
                responseModel = TownsService.GetTownsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Towns/DeleteTowns")]
		public IActionResult DeleteTowns(TownsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TownsService = new TownsService(_dbContext);
                responseModel = TownsService.DeleteTownsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Towns/GetAllTowns/{userId}")]
		public IActionResult GetAllTownsForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<TownsModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TownsService = new TownsService(_dbContext);
                responseModel = TownsService.GetAllTownsForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Towns/UpdateTowns")]
		public IActionResult UpdateTowns(TownsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TownsService = new TownsService(_dbContext);
                responseModel = TownsService.UpdateTownsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Towns/SaveTown")]
		public IActionResult SaveTown(TownsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TownsService = new TownsService(_dbContext);
                responseModel = TownsService.SaveTown(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Towns

		#region Traditions
		[HttpPost]
		[Route("Traditions/AddTraditions")]
		public IActionResult AddTraditions(TraditionsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TraditionsService = new TraditionsService(_dbContext);
                responseModel = TraditionsService.AddTraditionsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Traditions/GetTraditions")]
		public IActionResult GetTraditions(TraditionsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<TraditionsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TraditionsService = new TraditionsService(_dbContext);
                responseModel = TraditionsService.GetTraditionsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Traditions/DeleteTraditions")]
		public IActionResult DeleteTraditions(TraditionsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TraditionsService = new TraditionsService(_dbContext);
                responseModel = TraditionsService.DeleteTraditionsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Traditions/GetAllTraditions/{userId}")]
		public IActionResult GetAllTraditionsForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<TraditionsModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TraditionsService = new TraditionsService(_dbContext);
                responseModel = TraditionsService.GetAllTraditionsForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Traditions/UpdateTraditions")]
		public IActionResult UpdateTraditions(TraditionsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TraditionsService = new TraditionsService(_dbContext);
                responseModel = TraditionsService.UpdateTraditionsData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Traditions/SaveTradition")]
		public IActionResult SaveTradition(TraditionsModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TraditionsService = new TraditionsService(_dbContext);
                responseModel = TraditionsService.SaveTradition(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Traditions

		#region Universes
		[HttpPost]
		[Route("Universes/AddUniverses")]
		public IActionResult AddUniverses(UniversesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var UniversesService = new UniversesService(_dbContext);
                responseModel = UniversesService.AddUniversesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Universes/GetUniverses")]
		public IActionResult GetUniverses(UniversesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<UniversesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var UniversesService = new UniversesService(_dbContext);
                responseModel = UniversesService.GetUniversesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Universes/DeleteUniverses")]
		public IActionResult DeleteUniverses(UniversesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var UniversesService = new UniversesService(_dbContext);
                responseModel = UniversesService.DeleteUniversesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Universes/GetAllUniverses/{userId}")]
		public IActionResult GetAllUniversesForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<UniversesModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var UniversesService = new UniversesService(_dbContext);
                responseModel = UniversesService.GetAllUniversesForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Universes/UpdateUniverses")]
		public IActionResult UpdateUniverses(UniversesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var UniversesService = new UniversesService(_dbContext);
                responseModel = UniversesService.UpdateUniversesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Universes/SaveUniverse")]
		public IActionResult SaveUniverse(UniversesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var UniversesService = new UniversesService(_dbContext);
                responseModel = UniversesService.SaveUniverse(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Universes

		#region Vehicles
		[HttpPost]
		[Route("Vehicles/AddVehicles")]
		public IActionResult AddVehicles(VehiclesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var VehiclesService = new VehiclesService(_dbContext);
                responseModel = VehiclesService.AddVehiclesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Vehicles/GetVehicles")]
		public IActionResult GetVehicles(VehiclesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<VehiclesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var VehiclesService = new VehiclesService(_dbContext);
                responseModel = VehiclesService.GetVehiclesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Vehicles/DeleteVehicles")]
		public IActionResult DeleteVehicles(VehiclesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var VehiclesService = new VehiclesService(_dbContext);
                responseModel = VehiclesService.DeleteVehiclesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpGet]
		[Route("Vehicles/GetAllVehicles/{userId}")]
		public IActionResult GetAllVehiclesForUserID(long userId)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<List<VehiclesModel >>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var VehiclesService = new VehiclesService(_dbContext);
                responseModel = VehiclesService.GetAllVehiclesForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Vehicles/UpdateVehicles")]
		public IActionResult UpdateVehicles(VehiclesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var VehiclesService = new VehiclesService(_dbContext);
                responseModel = VehiclesService.UpdateVehiclesData(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}

		[HttpPost]
		[Route("Vehicles/SaveVehicle")]
		public IActionResult SaveVehicle(VehiclesModel model)
		{
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var VehiclesService = new VehiclesService(_dbContext);
                responseModel = VehiclesService.SaveVehicle(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

		}
		#endregion Vehicles

	}
}
