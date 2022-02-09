using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My.World.Api.Models;
using My.World.Web.Services;
using My.World.Web.ViewModel;

namespace My.World.Web.Controllers
{
    [Authorize]
    [Route("Folder")]
    public class FolderController : Controller
    {
        IDocumentsApiService _documentsApiService = null;
        IFoldersApiService _foldersApiService = null;
        private readonly IUsersApiService _usersApiService;

        public FolderController(IUsersApiService usersApiService, IDocumentsApiService documentsApiService, IFoldersApiService foldersApiService)
        {
            _documentsApiService = documentsApiService;
            _foldersApiService = foldersApiService;
            _usersApiService = usersApiService;
        }

        // GET: FolderController/Create
        [Route("Create")]
        public ActionResult Create()
        {
            UsersModel Userdata = new UsersModel();
            Userdata.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);

            long id = _foldersApiService.Addfolders(new FoldersModel()
            {
                title = "New Folder",
                user_id = Userdata.id
            });
            return RedirectToAction("Edit", new { id = id });
        }

        [Route("Create/{folderId}")]
        public IActionResult Create(long folderId)
        {
            UsersModel Userdata = new UsersModel();
            Userdata.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
            long id = _foldersApiService.Addfolders(new FoldersModel()
            {
                title = "New Folder",
                parent_folder_id = folderId,
                user_id = Userdata.id
            });
            return RedirectToAction("Edit", new { Id = id });
        }

        // GET: FolderController/Edit/5
        [Route("{id}")]
        public ActionResult Edit(int id)
        {
            UsersModel Userdata = new UsersModel();
            Userdata.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);

            DocumentViewModel docViewModel = new DocumentViewModel();
            docViewModel.Documents = _documentsApiService.GetAllFolderDocuments(Userdata.id, id);
            var folder = _foldersApiService.Getfolders(new FoldersModel() { id = id });
            docViewModel.FolderId = id;
            docViewModel.FolderTitle = folder.title;
            docViewModel.ChildFolders = _foldersApiService.GetAllChildFolders(id);
            return View(docViewModel);
        }

        // GET: FolderController/Edit/5
        [Route("Delete/{id}")]
        public ActionResult Delete(int Id)
        {
            UsersModel Userdata = new UsersModel();
            Userdata.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
            
            var childFolders = _foldersApiService.GetAllChildFolders(Id);

            if(childFolders != null && childFolders.Count > 0)
            {
                string Message = "Folder cannot be deleted. Remove the child folders.";
                TempData["ErrorMessageModal"] = string.Format("ShowErrorPopUp('{0}');", Message);
                return RedirectToAction("Edit", new { id = Id });
            }

            _foldersApiService.Deletefolders(new FoldersModel() { id = Id });
            return RedirectToAction("Index", "Document");
        }

        [Route("EditFolder")]
        [HttpPost]
        public IActionResult EditFolder(IFormCollection collection)
        {
            FoldersModel foldersModel = new FoldersModel();
            foldersModel.id = Convert.ToInt64(collection["folderId"]);
            foldersModel.title = collection["FoldersModel.title"].ToString();
            foldersModel.parent_folder_id = Convert.ToInt64(collection["FoldersModel.parent_folder_id"].ToString());
            _foldersApiService.Updatefolders(foldersModel);

            return RedirectToAction("Edit", new { id = foldersModel.id });
        }

        [Route("Details/{id}")]
        public IActionResult ShowFolder(Int64 id)
        {
            var Userid = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
            FoldersViewModel viewModel = new FoldersViewModel();
            viewModel.FoldersModel = new FoldersModel();
            viewModel.FoldersList = _foldersApiService.GetEligibleParentFolders(Userid, id);
            viewModel.FoldersList.Insert(0, new FoldersModel() { id = 0, title = "None" });
            viewModel.FoldersModel = _foldersApiService.Getfolders(new FoldersModel() { id = id });

            return PartialView("_EditFolderPartial", viewModel);
        }
    }
}
