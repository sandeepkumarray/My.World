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
	[Route("characters")]
	public class CharactersController : Controller
	{
		public readonly ICharactersApiService _iCharactersApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public CharactersController(ICharactersApiService iCharactersApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iCharactersApiService = iCharactersApiService;
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
			var characters = _iCharactersApiService.GetAllCharacters(accountID);
			characters.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Characters.png");
			    }
			});
			return View(characters);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewCharacters(string Id)
		{
			CharactersModel model = new CharactersModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["CharacterID"] = Id;
			ViewData["CharacterID"] = Id;
			HttpContext.Session.SetString("CharacterID", Id);
			
			CharactersViewModel charactersViewModel = new CharactersViewModel(_iObjectBucketApiService);
			charactersViewModel.charactersModel = _iCharactersApiService.GetCharacters(model);
			if (charactersViewModel.charactersModel == null)
				return RedirectToAction("Index", "NotFound");
			charactersViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			charactersViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "characters");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Characters" });
			charactersViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			charactersViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			charactersViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "characters");
			return View(charactersViewModel);

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewCharacters(string Id)
		{
			CharactersModel model = new CharactersModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["CharacterID"] = Id;
			ViewData["CharacterID"] = Id;
			HttpContext.Session.SetString("CharacterID", Id);
			
			CharactersViewModel charactersViewModel = new CharactersViewModel(_iObjectBucketApiService);
			charactersViewModel.charactersModel = _iCharactersApiService.GetCharacters(model);
			if (charactersViewModel.charactersModel == null)
			return RedirectToAction("Index", "NotFound");
			charactersViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(charactersViewModel.charactersModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			charactersViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "characters");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Characters" });
			charactersViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			charactersViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			charactersViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "characters");
			return View(charactersViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteCharacters(string Id)
		{
			CharactersModel model = new CharactersModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iCharactersApiService.DeleteCharacters(model);
			return RedirectToAction("Index");

		}

		public void TransformData(CharactersModel model)
		{
			if (model != null)
			{
				
				model.Age  = model.Age == null ? model.Age : model.Age.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Aliases  = model.Aliases == null ? model.Aliases : model.Aliases.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Background  = model.Background == null ? model.Background : model.Background.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Birthday  = model.Birthday == null ? model.Birthday : model.Birthday.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Birthplace  = model.Birthplace == null ? model.Birthplace : model.Birthplace.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Bodytype  = model.Bodytype == null ? model.Bodytype : model.Bodytype.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Education  = model.Education == null ? model.Education : model.Education.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Eyecolor  = model.Eyecolor == null ? model.Eyecolor : model.Eyecolor.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Facialhair  = model.Facialhair == null ? model.Facialhair : model.Facialhair.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Fave_animal  = model.Fave_animal == null ? model.Fave_animal : model.Fave_animal.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Fave_color  = model.Fave_color == null ? model.Fave_color : model.Fave_color.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Fave_food  = model.Fave_food == null ? model.Fave_food : model.Fave_food.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Fave_possession  = model.Fave_possession == null ? model.Fave_possession : model.Fave_possession.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Fave_weapon  = model.Fave_weapon == null ? model.Fave_weapon : model.Fave_weapon.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Flaws  = model.Flaws == null ? model.Flaws : model.Flaws.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Gender  = model.Gender == null ? model.Gender : model.Gender.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Haircolor  = model.Haircolor == null ? model.Haircolor : model.Haircolor.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Hairstyle  = model.Hairstyle == null ? model.Hairstyle : model.Hairstyle.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Height  = model.Height == null ? model.Height : model.Height.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Hobbies  = model.Hobbies == null ? model.Hobbies : model.Hobbies.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Identmarks  = model.Identmarks == null ? model.Identmarks : model.Identmarks.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Mannerisms  = model.Mannerisms == null ? model.Mannerisms : model.Mannerisms.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Motivations  = model.Motivations == null ? model.Motivations : model.Motivations.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Occupation  = model.Occupation == null ? model.Occupation : model.Occupation.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Personality_type  = model.Personality_type == null ? model.Personality_type : model.Personality_type.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Pets  = model.Pets == null ? model.Pets : model.Pets.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Politics  = model.Politics == null ? model.Politics : model.Politics.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Prejudices  = model.Prejudices == null ? model.Prejudices : model.Prejudices.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Privacy  = model.Privacy == null ? model.Privacy : model.Privacy.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_notes  = model.Private_notes == null ? model.Private_notes : model.Private_notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Race  = model.Race == null ? model.Race : model.Race.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Religion  = model.Religion == null ? model.Religion : model.Religion.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Role  = model.Role == null ? model.Role : model.Role.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Skintone  = model.Skintone == null ? model.Skintone : model.Skintone.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Talents  = model.Talents == null ? model.Talents : model.Talents.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Weight  = model.Weight == null ? model.Weight : model.Weight.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
			}

		}

		#region Save Properties Methods
		[HttpPost]
		[Route("{id}/SaveAge")]
		public IActionResult SaveAge(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Age";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveAliases")]
		public IActionResult SaveAliases(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Aliases";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveBackground")]
		public IActionResult SaveBackground(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Background";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveBirthday")]
		public IActionResult SaveBirthday(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Birthday";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveBirthplace")]
		public IActionResult SaveBirthplace(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Birthplace";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveBodytype")]
		public IActionResult SaveBodytype(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Bodytype";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEducation")]
		public IActionResult SaveEducation(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Education";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveEyecolor")]
		public IActionResult SaveEyecolor(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Eyecolor";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFacialhair")]
		public IActionResult SaveFacialhair(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Facialhair";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFave_animal")]
		public IActionResult SaveFave_animal(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Fave_animal";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFave_color")]
		public IActionResult SaveFave_color(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Fave_color";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFave_food")]
		public IActionResult SaveFave_food(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Fave_food";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFave_possession")]
		public IActionResult SaveFave_possession(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Fave_possession";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFave_weapon")]
		public IActionResult SaveFave_weapon(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Fave_weapon";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("SaveFavorite")]
		public IActionResult SaveFavorite()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Favorite";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var CharacterID = Convert.ToInt64(obj["CharacterID"].Value);
			var value = Convert.ToBoolean(obj["value"].Value);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = Convert.ToInt32(value);
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveFlaws")]
		public IActionResult SaveFlaws(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Flaws";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGender")]
		public IActionResult SaveGender(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Gender";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveHaircolor")]
		public IActionResult SaveHaircolor(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Haircolor";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveHairstyle")]
		public IActionResult SaveHairstyle(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Hairstyle";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveHeight")]
		public IActionResult SaveHeight(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Height";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveHobbies")]
		public IActionResult SaveHobbies(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Hobbies";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveIdentmarks")]
		public IActionResult SaveIdentmarks(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Identmarks";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveMannerisms")]
		public IActionResult SaveMannerisms(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Mannerisms";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
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
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
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
			var CharacterID = Convert.ToInt64(obj["CharacterID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
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
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveOccupation")]
		public IActionResult SaveOccupation(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Occupation";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePersonality_type")]
		public IActionResult SavePersonality_type(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Personality_type";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePets")]
		public IActionResult SavePets(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Pets";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePolitics")]
		public IActionResult SavePolitics(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Politics";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePrejudices")]
		public IActionResult SavePrejudices(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Prejudices";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePrivacy")]
		public IActionResult SavePrivacy(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Privacy";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
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
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRace")]
		public IActionResult SaveRace(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Race";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveReligion")]
		public IActionResult SaveReligion(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Religion";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRole")]
		public IActionResult SaveRole(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Role";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSkintone")]
		public IActionResult SaveSkintone(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Skintone";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTalents")]
		public IActionResult SaveTalents(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Talents";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
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
			var CharacterID = Convert.ToInt64(obj["CharacterID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveWeight")]
		public IActionResult SaveWeight(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Weight";
			var CharacterID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			CharactersModel model = new CharactersModel();
			model.id = CharacterID;
			model._id = CharacterID;
			model.column_type = type;
			model.column_value = value;
			response = _iCharactersApiService.SaveCharacter(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("CharactersID");
			
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
			                contentObjectAttachmentModel.content_type = "characters";
			
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
			string content_Id = HttpContext.Session.GetString("CharactersID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "characters";
			
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewCharacters", "characters", new { id = content_Id }, "Gallery_panel");

		}

	}
}
