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
	[Route("languages")]
	public class LanguagesController : Controller
	{
		public readonly ILanguagesApiService _iLanguagesApiService;

		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;

		public readonly IObjectBucketApiService _iObjectBucketApiService;

		public readonly IConfiguration _config;


		public LanguagesController(ILanguagesApiService iLanguagesApiService,IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService,IContenttypesApiService iContenttypesApiService,IObjectBucketApiService iObjectBucketApiService,IConfiguration config)
		{
			_iLanguagesApiService = iLanguagesApiService;
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
			var languages = _iLanguagesApiService.GetAllLanguages(accountID);
			languages.ForEach(b =>
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
			          .Replace("{objectName}", "cards/Languages.png");
			    }
			});
			return View(languages);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewLanguages(string Id)
		{
			LanguagesModel model = new LanguagesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["LanguageID"] = Id;
			ViewData["LanguageID"] = Id;
			HttpContext.Session.SetString("LanguageID", Id);
			
			LanguagesViewModel languagesViewModel = new LanguagesViewModel(_iObjectBucketApiService);
			languagesViewModel.languagesModel = _iLanguagesApiService.GetLanguages(model);
			if (languagesViewModel.languagesModel == null)
				return RedirectToAction("Index", "NotFound");
			languagesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			languagesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "languages");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Languages" });
			languagesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			languagesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			languagesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "languages");
			languagesViewModel.ContentObjectModelList.ForEach(o => 
			{
			    var publicUrl = "http://" + _iObjectBucketApiService.objectStorageKeysModel.endpoint
			                + '/' + _iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + _config.GetValue<string>("BucketEnv") + '/' + o.object_name; 
			    o.file_url = HttpUtility.UrlPathEncode(publicUrl); 
			}); 
			var existing_total_size = languagesViewModel.ContentObjectModelList.Sum(f => f.object_size);
			var AllowedTotalContentSize = Convert.ToInt64(HttpContext.Session.GetString("AllowedTotalContentSize"));
			var remainingSize = AllowedTotalContentSize - existing_total_size;
			languagesViewModel.RemainingContentSize = Helpers.Utility.SizeSuffix(remainingSize);
			return View(languagesViewModel); 

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewLanguages(string Id)
		{
			LanguagesModel model = new LanguagesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["LanguageID"] = Id;
			ViewData["LanguageID"] = Id;
			HttpContext.Session.SetString("LanguageID", Id);
			
			LanguagesViewModel languagesViewModel = new LanguagesViewModel(_iObjectBucketApiService);
			languagesViewModel.languagesModel = _iLanguagesApiService.GetLanguages(model);
			if (languagesViewModel.languagesModel == null)
			return RedirectToAction("Index", "NotFound");
			languagesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(languagesViewModel.languagesModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			languagesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "languages");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Languages" });
			languagesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			languagesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			_iObjectBucketApiService.SetObjectStorageSecrets(model.user_id);
			languagesViewModel.ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(Id), "languages");
			return View(languagesViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteLanguages(string Id)
		{
			LanguagesModel model = new LanguagesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iLanguagesApiService.DeleteLanguages(model);
			return RedirectToAction("Index");

		}

		public void TransformData(LanguagesModel model)
		{
			if (model != null)
			{
				
				model.Tags  = model.Tags == null ? model.Tags : model.Tags.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Other_Names  = model.Other_Names == null ? model.Other_Names : model.Other_Names.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Typology  = model.Typology == null ? model.Typology : model.Typology.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Dialectical_information  = model.Dialectical_information == null ? model.Dialectical_information : model.Dialectical_information.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Register  = model.Register == null ? model.Register : model.Register.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.History  = model.History == null ? model.History : model.History.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Evolution  = model.Evolution == null ? model.Evolution : model.Evolution.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Gestures  = model.Gestures == null ? model.Gestures : model.Gestures.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Phonology  = model.Phonology == null ? model.Phonology : model.Phonology.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Grammar  = model.Grammar == null ? model.Grammar : model.Grammar.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Please  = model.Please == null ? model.Please : model.Please.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Trade  = model.Trade == null ? model.Trade : model.Trade.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Family  = model.Family == null ? model.Family : model.Family.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Body_parts  = model.Body_parts == null ? model.Body_parts : model.Body_parts.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.No_words  = model.No_words == null ? model.No_words : model.No_words.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Yes_words  = model.Yes_words == null ? model.Yes_words : model.Yes_words.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Sorry  = model.Sorry == null ? model.Sorry : model.Sorry.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.You_are_welcome  = model.You_are_welcome == null ? model.You_are_welcome : model.You_are_welcome.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Thank_you  = model.Thank_you == null ? model.Thank_you : model.Thank_you.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Goodbyes  = model.Goodbyes == null ? model.Goodbyes : model.Goodbyes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Greetings  = model.Greetings == null ? model.Greetings : model.Greetings.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Pronouns  = model.Pronouns == null ? model.Pronouns : model.Pronouns.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Numbers  = model.Numbers == null ? model.Numbers : model.Numbers.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Quantifiers  = model.Quantifiers == null ? model.Quantifiers : model.Quantifiers.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Determiners  = model.Determiners == null ? model.Determiners : model.Determiners.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Notes  = model.Notes == null ? model.Notes : model.Notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.Private_notes  = model.Private_notes == null ? model.Private_notes : model.Private_notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
			}

		}

		#region Save Properties Methods
		[HttpPost]
		[Route("SaveUniverse")]
		public IActionResult SaveUniverse()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Universe";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var LanguageID = Convert.ToInt64(obj["LanguageID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
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
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
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
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
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
			var LanguageID = Convert.ToInt64(obj["LanguageID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTypology")]
		public IActionResult SaveTypology(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Typology";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveDialectical_information")]
		public IActionResult SaveDialectical_information(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Dialectical_information";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveRegister")]
		public IActionResult SaveRegister(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Register";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveHistory")]
		public IActionResult SaveHistory(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "History";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
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
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGestures")]
		public IActionResult SaveGestures(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Gestures";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePhonology")]
		public IActionResult SavePhonology(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Phonology";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGrammar")]
		public IActionResult SaveGrammar(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Grammar";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePlease")]
		public IActionResult SavePlease(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Please";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveTrade")]
		public IActionResult SaveTrade(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Trade";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
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
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveBody_parts")]
		public IActionResult SaveBody_parts(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Body_parts";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNo_words")]
		public IActionResult SaveNo_words(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "No_words";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveYes_words")]
		public IActionResult SaveYes_words(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Yes_words";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveSorry")]
		public IActionResult SaveSorry(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Sorry";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveYou_are_welcome")]
		public IActionResult SaveYou_are_welcome(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "You_are_welcome";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveThank_you")]
		public IActionResult SaveThank_you(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Thank_you";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGoodbyes")]
		public IActionResult SaveGoodbyes(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Goodbyes";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveGreetings")]
		public IActionResult SaveGreetings(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Greetings";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SavePronouns")]
		public IActionResult SavePronouns(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Pronouns";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveNumbers")]
		public IActionResult SaveNumbers(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Numbers";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveQuantifiers")]
		public IActionResult SaveQuantifiers(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Quantifiers";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/SaveDeterminers")]
		public IActionResult SaveDeterminers(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "Determiners";
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
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
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
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
			var LanguageID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			LanguagesModel model = new LanguagesModel();
			model.id = LanguageID;
			model._id = LanguageID;
			model.column_type = type;
			model.column_value = value;
			response = _iLanguagesApiService.SaveLanguage(model);
			return Json(response);

		}
		#endregion 

		[HttpPost]
		[Route("UploadAttachment")]
		public IActionResult UploadAttachment(List<IFormFile> files)
		{
			var accountID = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			string content_Id = HttpContext.Session.GetString("LanguageID");
			
			var ContentObjectModelList = _iObjectBucketApiService.GetAllContentObjectAttachments(Convert.ToInt64(content_Id), "languages");
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
								contentObjectAttachmentModel.content_type = "languages";
			
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
			string content_Id = HttpContext.Session.GetString("LanguageID");
			
			ContentObjectAttachmentModel contentObjectAttachmentModel = new ContentObjectAttachmentModel();
			contentObjectAttachmentModel.object_id = objectId;
			contentObjectAttachmentModel.content_id = Convert.ToInt64(content_Id);
			contentObjectAttachmentModel.content_type = "languages";
			
			var bucket_folder = _config.GetValue<string>("BucketEnv");
			ContentObjectModel contentObjectModel = new ContentObjectModel();
			contentObjectModel.object_id = objectId;
			contentObjectModel.object_name = bucket_folder + " / " + objectName;
			
			_iObjectBucketApiService.SetObjectStorageSecrets(accountID);
			_iObjectBucketApiService.DeleteObject(contentObjectModel);
			_iObjectBucketApiService.DeleteContentObjectAttachment(contentObjectAttachmentModel);
			_iObjectBucketApiService.DeleteContentObject(contentObjectModel);
			
			return RedirectToAction("ViewLanguages", "languages", new { id = content_Id }, "Gallery_panel");

		}

	}
}
