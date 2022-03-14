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
	[Route("technologies")]
	public class TechnologiesController : Controller
	{
		public readonly ITechnologiesApiService _iTechnologiesApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public TechnologiesController(ITechnologiesApiService iTechnologiesApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iTechnologiesApiService = iTechnologiesApiService;
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
			var technologies = _iTechnologiesApiService.GetAllTechnologies(accountID);
			technologies.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Technologies.png");
			    }
			});
			return View(technologies);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewTechnologies(string Id)
		{
			TechnologiesModel model = new TechnologiesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["TechnologieID"] = Id;
			ViewData["TechnologieID"] = Id;
			HttpContext.Session.SetString("TechnologieID", Id);
			
			TechnologiesViewModel technologiesViewModel = new TechnologiesViewModel(_iObjectBucketApiService);
			technologiesViewModel.technologiesModel = _iTechnologiesApiService.GetTechnologies(model);
			if (technologiesViewModel.technologiesModel == null)
				return RedirectToAction("Index", "NotFound");
			technologiesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			technologiesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "technologies");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Technologies" });
			technologiesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			technologiesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			technologiesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "technologies");
			technologiesViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = technologiesViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			technologiesViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(technologiesViewModel); 

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewTechnologies(string Id)
		{
			TechnologiesModel model = new TechnologiesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["TechnologieID"] = Id;
			ViewData["TechnologieID"] = Id;
			HttpContext.Session.SetString("TechnologieID", Id);
			
			TechnologiesViewModel technologiesViewModel = new TechnologiesViewModel(_iObjectBucketApiService);
			technologiesViewModel.technologiesModel = _iTechnologiesApiService.GetTechnologies(model);
			if (technologiesViewModel.technologiesModel == null)
			return RedirectToAction("Index", "NotFound");
			technologiesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(technologiesViewModel.technologiesModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			technologiesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "technologies");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Technologies" });
			technologiesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			technologiesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			technologiesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "technologies");
			return View(technologiesViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteTechnologies(string Id)
		{
			TechnologiesModel model = new TechnologiesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iTechnologiesApiService.DeleteTechnologies(model);
			return RedirectToAction("Index");

		}

		public void TransformData(TechnologiesModel model)
		{
			if (model != null)
			{
				
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Other_Names  = model.Other_Names == null ? model.Other_Names : model.Other_Names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Sales_Process  = model.Sales_Process == null ? model.Sales_Process : model.Sales_Process.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Materials  = model.Materials == null ? model.Materials : model.Materials.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Manufacturing_Process  = model.Manufacturing_Process == null ? model.Manufacturing_Process : model.Manufacturing_Process.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Planets  = model.Planets == null ? model.Planets : model.Planets.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Rarity  = model.Rarity == null ? model.Rarity : model.Rarity.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Creatures  = model.Creatures == null ? model.Creatures : model.Creatures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Groups  = model.Groups == null ? model.Groups : model.Groups.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Countries  = model.Countries == null ? model.Countries : model.Countries.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Towns  = model.Towns == null ? model.Towns : model.Towns.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Characters  = model.Characters == null ? model.Characters : model.Characters.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Magic_effects  = model.Magic_effects == null ? model.Magic_effects : model.Magic_effects.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Resources_Used  = model.Resources_Used == null ? model.Resources_Used : model.Resources_Used.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.How_It_Works  = model.How_It_Works == null ? model.How_It_Works : model.How_It_Works.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Purpose  = model.Purpose == null ? model.Purpose : model.Purpose.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Physical_Description  = model.Physical_Description == null ? model.Physical_Description : model.Physical_Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Colors  = model.Colors == null ? model.Colors : model.Colors.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Related_technologies  = model.Related_technologies == null ? model.Related_technologies : model.Related_technologies.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Parent_technologies  = model.Parent_technologies == null ? model.Parent_technologies : model.Parent_technologies.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Child_technologies  = model.Child_technologies == null ? model.Child_technologies : model.Child_technologies.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
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
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(obj["TechnologieID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(obj["TechnologieID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSales_Process")]
		public IActionResult SaveSales_Process(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Sales_Process";
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveManufacturing_Process")]
		public IActionResult SaveManufacturing_Process(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Manufacturing_Process";
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveCost")]
		public IActionResult SaveCost()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Cost";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var TechnologieID = Convert.ToInt64(obj["TechnologieID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMagic_effects")]
		public IActionResult SaveMagic_effects(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Magic_effects";
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveResources_Used")]
		public IActionResult SaveResources_Used(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Resources_Used";
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveHow_It_Works")]
		public IActionResult SaveHow_It_Works(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "How_It_Works";
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePurpose")]
		public IActionResult SavePurpose(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Purpose";
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(obj["TechnologieID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(obj["TechnologieID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveColors")]
		public IActionResult SaveColors(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Colors";
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRelated_technologies")]
		public IActionResult SaveRelated_technologies(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Related_technologies";
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveParent_technologies")]
		public IActionResult SaveParent_technologies(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Parent_technologies";
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveChild_technologies")]
		public IActionResult SaveChild_technologies(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Child_technologies";
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
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
			var TechnologieID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			TechnologiesModel model = new TechnologiesModel();
			model.id = TechnologieID;
			model._id = TechnologieID;
			model.column_type = type;
			model.column_value = value;
			response = _iTechnologiesApiService.SaveTechnologie(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("TechnologieID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "technologies");
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
								contentObjectAttachmentModel.content_type = "technologies";
			
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
			string content_Id = HttpContext.Session.GetString("TechnologieID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "technologies";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewTechnologies", "technologies", new { id = content_Id }, "Gallery_panel");

		}

	}
}
