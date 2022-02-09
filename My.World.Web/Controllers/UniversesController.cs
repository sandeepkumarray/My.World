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

namespace My.World.Web.Controllers
{
	[Authorize]
	[Route("universes")]
	public class UniversesController : Controller
	{
		public readonly IUniversesApiService _iUniversesApiService;

		public readonly IUsersApiService _iUsersApiService;

		public readonly IContenttypesApiService _iContenttypesApiService;


		public UniversesController(IUniversesApiService iUniversesApiService,IUsersApiService iUsersApiService, IContenttypesApiService iContenttypesApiService)
		{
			_iUniversesApiService = iUniversesApiService;
			_iUsersApiService = iUsersApiService;
			_iContenttypesApiService = iContenttypesApiService;
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
			var universes = _iUniversesApiService.GetAllUniverses(accountID);
			return View(universes);

		}

		[HttpGet]
		[Route("{Id}")]
		public IActionResult ViewUniverses(string Id)
		{
			UniversesModel model = new UniversesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["UniverseID"] = Id;
			ViewData["UniverseID"] = Id;
			HttpContext.Session.SetString("UniverseID", Id);
			
			UniversesViewModel universesViewModel = new UniversesViewModel();
			universesViewModel.universesModel = _iUniversesApiService.GetUniverses(model);
			if (universesViewModel.universesModel == null)
			return RedirectToAction("Index", "NotFound");
			universesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			universesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "universes");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Universes" });
			universesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			universesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			return View(universesViewModel);

		}

		[HttpGet]
		[Route("Preview/{Id}")]
		public IActionResult PreviewUniverses(string Id)
		{
			UniversesModel model = new UniversesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			TempData["UniverseID"] = Id;
			ViewData["UniverseID"] = Id;
			HttpContext.Session.SetString("UniverseID", Id);
			
			UniversesViewModel universesViewModel = new UniversesViewModel();
			universesViewModel.universesModel = _iUniversesApiService.GetUniverses(model);
			if (universesViewModel.universesModel == null)
			return RedirectToAction("Index", "NotFound");
			universesViewModel.UniversesList = _iUniversesApiService.GetAllUniverses(model.user_id);
			
			TransformData(universesViewModel.universesModel);
			var contentTemplate = _iUsersApiService.GetUsersContentTemplate(new UsersModel() { id = model.user_id });
			universesViewModel.ContentTemplate = contentTemplate.Contents.Find(c => c.content_type == "universes");
			ContentTypesModel contentTypesModel = _iContenttypesApiService.GetContentTypes(new ContentTypesModel() { name = "Universes" });
			universesViewModel.headerBackgroundColor = contentTypesModel.primary_color;
			universesViewModel.headerBackgroundColor = contentTypesModel.sec_color;
			return View(universesViewModel);

		}

		[Route("Delete/{Id}")]
		public IActionResult DeleteUniverses(string Id)
		{
			UniversesModel model = new UniversesModel();
			model.user_id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
			model.id = Convert.ToInt32(Id);
			
			var result = _iUniversesApiService.DeleteUniverses(model);
			return RedirectToAction("Index");

		}

		public void TransformData(UniversesModel model)
		{
			if (model != null)
			{
				
				model.description  = model.description == null ? model.description : model.description.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.genre  = model.genre == null ? model.genre : model.genre.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.history  = model.history == null ? model.history : model.history.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.laws_of_physics  = model.laws_of_physics == null ? model.laws_of_physics : model.laws_of_physics.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.magic_system  = model.magic_system == null ? model.magic_system : model.magic_system.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.notes  = model.notes == null ? model.notes : model.notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.page_type  = model.page_type == null ? model.page_type : model.page_type.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.private_notes  = model.private_notes == null ? model.private_notes : model.private_notes.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
				model.technology  = model.technology == null ? model.technology : model.technology.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);
			}

		}

		#region Save Properties Methods
		[HttpPost]
		[Route("{id}/Savedescription")]
		public IActionResult Savedescription(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "description";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("Savefavorite")]
		public IActionResult Savefavorite()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "favorite";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var UniverseID = Convert.ToInt64(obj["UniverseID"].Value);
			var value = Convert.ToBoolean(obj["value"].Value);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = Convert.ToInt32(value);
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Savegenre")]
		public IActionResult Savegenre(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "genre";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Savehistory")]
		public IActionResult Savehistory(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "history";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Savelaws_of_physics")]
		public IActionResult Savelaws_of_physics(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "laws_of_physics";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Savemagic_system")]
		public IActionResult Savemagic_system(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "magic_system";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("Savename")]
		public IActionResult Savename()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "name";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var UniverseID = Convert.ToInt64(obj["UniverseID"].Value);
			var value = Convert.ToString(obj["value"].Value);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Savenotes")]
		public IActionResult Savenotes(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "notes";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Savepage_type")]
		public IActionResult Savepage_type(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "page_type";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("Saveprivacy")]
		public IActionResult Saveprivacy()
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "privacy";
			dynamic obj = JsonConvert.DeserializeObject(_rawContent);
			var UniverseID = Convert.ToInt64(obj["UniverseID"].Value);
			var value = Convert.ToBoolean(obj["value"].Value);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = Convert.ToInt32(value);
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Saveprivate_notes")]
		public IActionResult Saveprivate_notes(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "private_notes";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}

		[HttpPost]
		[Route("{id}/Savetechnology")]
		public IActionResult Savetechnology(string id)
		{
			string _rawContent = null;
			_rawContent = GetRawContent(_rawContent);
			ResponseModel<string> response = new ResponseModel<string>();
			var type = "technology";
			var UniverseID = Convert.ToInt64(id);
			var value = Convert.ToString(_rawContent);
			
			UniversesModel model = new UniversesModel();
			model.id = UniverseID;
			model._id = UniverseID;
			model.column_type = type;
			model.column_value = value;
			response = _iUniversesApiService.SaveUniverse(model);
			return Json(response);

		}
		#endregion 

	}
}
