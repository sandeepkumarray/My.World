using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My.World.Api.Models;
using My.World.Web.Services;
using My.World.Web.ViewModel;
using Newtonsoft.Json;

namespace My.World.Web.Controllers
{
    [Authorize]
    [Route("Document")]
    public class DocumentController : Controller
    {
        private readonly IDashboardApiService _dashboardApiService;
        IDocumentsApiService _documentsApiService = null;
        IFoldersApiService _foldersApiService = null;
        private readonly IUsersApiService _usersApiService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DocumentController(IUsersApiService usersApiService, IDocumentsApiService documentsApiService,
            IFoldersApiService foldersApiService, IDashboardApiService dashboardApiService, IHttpContextAccessor httpContextAccessor)
        {
            _documentsApiService = documentsApiService;
            _foldersApiService = foldersApiService;
            _usersApiService = usersApiService;
            _dashboardApiService = dashboardApiService;
            _httpContextAccessor = httpContextAccessor;

        }

        private string GetRawContent(string _rawContent)
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                return reader.ReadToEndAsync().Result;
            }
        }

        // GET: DocumentController
        public IActionResult Index()
        {
            UsersModel Userdata = new UsersModel();
            Userdata.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);

            DocumentViewModel docViewModel = new DocumentViewModel();
            docViewModel.Documents = _documentsApiService.GetAlldocuments(Userdata.id);
            docViewModel.Folders = _foldersApiService.GetAllfolders(Userdata.id);
            return View(docViewModel);
        }

        // GET: DocumentController/Details/5

        [HttpGet]
        [Route("Edit/{Id}")]
        public IActionResult Edit(string Id)
        {
            DocumentsModel model = new DocumentsModel();
            model.id = Convert.ToInt64(Id);
            ViewData["DocumentId"] = Id;
            model = _documentsApiService.Getdocuments(model);
            return View(model);
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult View(string Id)
        {
            DocumentsModel model = new DocumentsModel();
            model.id = Convert.ToInt64(Id);
            ViewData["DocumentId"] = Id;
            model = _documentsApiService.Getdocuments(model);

            model.body = model.body.Replace("[MYWORLD]", Helpers.Utility.CurrentDomain);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(model.body);
            foreach (var link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAgilityPack.HtmlNode newValue = HtmlAgilityPack.HtmlNode.CreateNode(link.InnerHtml);
                if (link.InnerHtml.Contains(':'))
                {
                    var oldValue = link.ChildNodes.First();
                    string contentLabel = oldValue.InnerHtml.Split(':')[0];
                    contentLabel = contentLabel.Replace("[", "");
                    string contentType = contentLabel.Split('-')[0];
                    string contentID = contentLabel.Split('-')[1];
                    var baseModel = _dashboardApiService.GetContentDetailsFromTypeID(contentType, contentID);
                    newValue.InnerHtml = baseModel.content_name;
                    link.ReplaceChild(newValue, oldValue);
                }
                //string hrefValue = link.GetAttributeValue("href", string.Empty);
                //if (hrefValue.Contains(':'))
                //{
                //    string contentLabel = hrefValue.Split(':')[0];
                //}
            }
            model.body = doc.DocumentNode.InnerHtml;
            return View(model);
        }

        [HttpGet]
        [Route("Delete/{Id}")]
        public IActionResult Delete(string Id)
        {
            DocumentsModel model = new DocumentsModel();
            model.id = Convert.ToInt64(Id);
            _documentsApiService.Deletedocuments(model);

            return RedirectToAction("Index");
        }

        [Route("Create")]
        public IActionResult Create()
        {
            UsersModel Userdata = new UsersModel();
            Userdata.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
            long id = _documentsApiService.Adddocuments(new DocumentsModel()
            {
                body = "",
                title = "New Document",
                user_id = Userdata.id
            });
            return RedirectToAction("Edit", new { Id = id });
        }

        [Route("Create/{folderId}")]
        public IActionResult Create(long folderId)
        {
            UsersModel Userdata = new UsersModel();
            Userdata.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
            long id = _documentsApiService.Adddocuments(new DocumentsModel()
            {
                body = "",
                title = "New Document",
                folder_id = folderId,
                user_id = Userdata.id
            });
            return RedirectToAction("Edit", new { Id = id });
        }

        [HttpPost]
        [Route("{id}/Savetitle")]
        public IActionResult SaveTitle(string id)
        {
            string _rawContent = null;
            _rawContent = GetRawContent(_rawContent);

            ResponseModel<string> response = new ResponseModel<string>();
            var type = "title";
            var DocumentId = Convert.ToInt64(id);
            var value = Convert.ToString(_rawContent);
            DocumentsModel model = new DocumentsModel();
            model.id = DocumentId;
            model._id = DocumentId;
            model.column_type = type;
            model.column_value = value;
            response = _documentsApiService.Savedocuments(model);
            return Json(response);
        }

        [HttpPost]
        [Route("{id}/Savebody")]
        public IActionResult SaveBody(string id)
        {
            string _rawContent = null;
            _rawContent = GetRawContent(_rawContent);

            ResponseModel<string> response = new ResponseModel<string>();
            var type = "body";
            var DocumentId = Convert.ToInt64(id);
            var value = Convert.ToString(_rawContent);
            DocumentsModel model = new DocumentsModel();
            model.id = DocumentId;
            model._id = DocumentId;
            model.column_type = type;
            model.column_value = value;
            response = _documentsApiService.Savedocuments(model);
            return Json(response);
        }

        [HttpPost]
        [Route("{id}/Savewordcount")]
        public IActionResult SaveWordCount(string id)
        {
            string _rawContent = null;
            _rawContent = GetRawContent(_rawContent);

            ResponseModel<string> response = new ResponseModel<string>();
            var type = "cached_word_count";
            var DocumentId = Convert.ToInt64(id);
            var value = Convert.ToString(_rawContent);
            DocumentsModel model = new DocumentsModel();
            model.id = DocumentId;
            model._id = DocumentId;
            model.column_type = type;
            model.column_value = value;
            response = _documentsApiService.Savedocuments(model);
            return Json(response);
        }

        [HttpGet]
        [Route("GetMentions")]
        public IActionResult GetMentions()
        {
            var userid = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
            var result = _dashboardApiService.GetMentionsData(userid);
            var returnValue = from m in result
                              select new
                              {
                                  id = m.id,
                                  label = m.name,
                                  link = "[MYWORLD]/" + m.content_type + "/" + m.id,
                                  value = "[" + m.content_type + "-" + m.id + ":" + m.name + "]"
                              };
            return Json(returnValue);
        }
    }
}
