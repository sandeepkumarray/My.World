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
	[Route("foods")]
	public class FoodsController : Controller
	{
		public readonly IFoodsApiService _iFoodsApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public FoodsController(IFoodsApiService iFoodsApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iFoodsApiService = iFoodsApiService;
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
			var foods = _iFoodsApiService.GetAllFoods(accountID);
			foods.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Foods.png");
			    }
			});
			return View(foods);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewFoods(string Id)
		{
			FoodsModel model = new FoodsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["FoodID"] = Id;
			ViewData["FoodID"] = Id;
			HttpContext.Session.SetString("FoodID", Id);
			
			FoodsViewModel foodsViewModel = new FoodsViewModel(_iObjectBucketApiService);
			foodsViewModel.foodsModel = _iFoodsApiService.GetFoods(model);
			if (foodsViewModel.foodsModel == null)
				return RedirectToAction("Index", "NotFound");
			foodsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			foodsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "foods");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Foods" });
			foodsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			foodsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			foodsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "foods");
			foodsViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = foodsViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			foodsViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(foodsViewModel); 

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewFoods(string Id)
		{
			FoodsModel model = new FoodsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["FoodID"] = Id;
			ViewData["FoodID"] = Id;
			HttpContext.Session.SetString("FoodID", Id);
			
			FoodsViewModel foodsViewModel = new FoodsViewModel(_iObjectBucketApiService);
			foodsViewModel.foodsModel = _iFoodsApiService.GetFoods(model);
			if (foodsViewModel.foodsModel == null)
			return RedirectToAction("Index", "NotFound");
			foodsViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(foodsViewModel.foodsModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			foodsViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "foods");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Foods" });
			foodsViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			foodsViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			foodsViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "foods");
			return View(foodsViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteFoods(string Id)
		{
			FoodsModel model = new FoodsModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iFoodsApiService.DeleteFoods(model);
			return RedirectToAction("Index");

		}

		public void TransformData(FoodsModel model)
		{
			if (model != null)
			{
				
				model.Type_of_food  = model.Type_of_food == null ? model.Type_of_food : model.Type_of_food.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Other_Names  = model.Other_Names == null ? model.Other_Names : model.Other_Names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Description  = model.Description == null ? model.Description : model.Description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Color  = model.Color == null ? model.Color : model.Color.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Variations  = model.Variations == null ? model.Variations : model.Variations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Smell  = model.Smell == null ? model.Smell : model.Smell.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Ingredients  = model.Ingredients == null ? model.Ingredients : model.Ingredients.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Preparation  = model.Preparation == null ? model.Preparation : model.Preparation.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Cooking_method  = model.Cooking_method == null ? model.Cooking_method : model.Cooking_method.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Spices  = model.Spices == null ? model.Spices : model.Spices.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Yield  = model.Yield == null ? model.Yield : model.Yield.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Shelf_life  = model.Shelf_life == null ? model.Shelf_life : model.Shelf_life.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Rarity  = model.Rarity == null ? model.Rarity : model.Rarity.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Sold_by  = model.Sold_by == null ? model.Sold_by : model.Sold_by.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Cost  = model.Cost == null ? model.Cost : model.Cost.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Flavor  = model.Flavor == null ? model.Flavor : model.Flavor.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Meal  = model.Meal == null ? model.Meal : model.Meal.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Serving  = model.Serving == null ? model.Serving : model.Serving.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Utensils_needed  = model.Utensils_needed == null ? model.Utensils_needed : model.Utensils_needed.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Texture  = model.Texture == null ? model.Texture : model.Texture.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Scent  = model.Scent == null ? model.Scent : model.Scent.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Side_effects  = model.Side_effects == null ? model.Side_effects : model.Side_effects.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Nutrition  = model.Nutrition == null ? model.Nutrition : model.Nutrition.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Conditions  = model.Conditions == null ? model.Conditions : model.Conditions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Reputation  = model.Reputation == null ? model.Reputation : model.Reputation.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Place_of_origin  = model.Place_of_origin == null ? model.Place_of_origin : model.Place_of_origin.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Origin_story  = model.Origin_story == null ? model.Origin_story : model.Origin_story.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Traditions  = model.Traditions == null ? model.Traditions : model.Traditions.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Symbolisms  = model.Symbolisms == null ? model.Symbolisms : model.Symbolisms.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Related_foods  = model.Related_foods == null ? model.Related_foods : model.Related_foods.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_Notes  = model.Private_Notes == null ? model.Private_Notes : model.Private_Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
			}

		}

		#region Save Properties Methods
		[HttpPost]
		[Route("{id}/SaveType_of_food")]
		public IActionResult SaveType_of_food(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Type_of_food";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
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
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
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
			var FoodID = Convert.ToInt64(obj["FoodID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
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
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
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
			var FoodID = Convert.ToInt64(obj["FoodID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
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
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveColor")]
		public IActionResult SaveColor(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Color";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
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
			var FoodID = Convert.ToInt64(obj["FoodID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
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
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
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
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveIngredients")]
		public IActionResult SaveIngredients(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Ingredients";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePreparation")]
		public IActionResult SavePreparation(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Preparation";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCooking_method")]
		public IActionResult SaveCooking_method(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Cooking_method";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSpices")]
		public IActionResult SaveSpices(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Spices";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveYield")]
		public IActionResult SaveYield(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Yield";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveShelf_life")]
		public IActionResult SaveShelf_life(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Shelf_life";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
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
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSold_by")]
		public IActionResult SaveSold_by(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Sold_by";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveCost")]
		public IActionResult SaveCost(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Cost";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFlavor")]
		public IActionResult SaveFlavor(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Flavor";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMeal")]
		public IActionResult SaveMeal(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Meal";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveServing")]
		public IActionResult SaveServing(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Serving";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveUtensils_needed")]
		public IActionResult SaveUtensils_needed(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Utensils_needed";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTexture")]
		public IActionResult SaveTexture(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Texture";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveScent")]
		public IActionResult SaveScent(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Scent";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSide_effects")]
		public IActionResult SaveSide_effects(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Side_effects";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNutrition")]
		public IActionResult SaveNutrition(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Nutrition";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
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
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveReputation")]
		public IActionResult SaveReputation(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Reputation";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePlace_of_origin")]
		public IActionResult SavePlace_of_origin(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Place_of_origin";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
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
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
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
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
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
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRelated_foods")]
		public IActionResult SaveRelated_foods(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Related_foods";
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
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
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
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
			var FoodID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			FoodsModel model = new FoodsModel();
			model.id = FoodID;
			model._id = FoodID;
			model.column_type = type;
			model.column_value = value;
			response = _iFoodsApiService.SaveFood(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("FoodID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "foods");
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
								contentObjectAttachmentModel.content_type = "foods";
			
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
			string content_Id = HttpContext.Session.GetString("FoodID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "foods";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewFoods", "foods", new { id = content_Id }, "Gallery_panel");

		}

	}
}
