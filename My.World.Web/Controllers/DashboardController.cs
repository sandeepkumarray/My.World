using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My.World.Api.Models;
using My.World.Web.Models;
using My.World.Web.Services;
using My.World.Web.ViewModel;
using Newtonsoft.Json;

namespace My.World.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardApiService _dashboardApiService;
        private readonly IUsersApiService _usersApiService;
        private readonly IContentplansApiService _contentPlansService;

        public DashboardController(IDashboardApiService dashboardApiService, IUsersApiService usersApiService,
            IContentplansApiService contentPlansService)
        {
            _dashboardApiService = dashboardApiService;
            _usersApiService = usersApiService;
            _contentPlansService = contentPlansService;
        }

        [Authorize]
        public IActionResult Index()
        {
            string userID = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value;
            DashboardViewModel viewModel = new DashboardViewModel();

            UsersModel Userdata = new UsersModel();
            Userdata.id = Convert.ToInt64(userID);
            var account = _usersApiService.GetUsers(Userdata);

            SiteTemplateModel siteTemplateModel = JsonConvert.DeserializeObject<SiteTemplateModel>(account.user_plan.plan_template);
            DashboardModel dashboardModel = new DashboardModel();
            dashboardModel.User_Id = Convert.ToInt64(userID);

            dashboardModel = _dashboardApiService.GetDashboard(dashboardModel);
            viewModel = new DashboardViewModel(dashboardModel, siteTemplateModel);
            viewModel.ContentPlans = _contentPlansService.GetAllContentPlans();
            viewModel.ContentTypesList = _dashboardApiService.GetAllContentTypes();
            viewModel.DashboardRecentList = _dashboardApiService.GetRecentsData(Userdata.id);
            viewModel.GetDashboardFromPlans(dashboardModel);
            viewModel.GetDashboardCreateList(dashboardModel);
            viewModel.UserPlan = viewModel.ContentPlans.Find(p => p.id == account.user_plan.id);

            HttpContext.Session.SetObject(AppConstants.USERPLANDETAILS, viewModel.UserPlan);
            return View(viewModel);
        }

        [HttpGet]
        [Route("CreateItem")]
        public IActionResult CreateItem(string content_type)
        {
            try
            {
                UsersModel Userdata = new UsersModel();
                Userdata.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
                var account = _usersApiService.GetUsers(Userdata);

                if (account == null)
                {
                    ModelState.AddModelError("Error", "User not logged in.");
                    return RedirectToAction("Login", "Account");
                }

                SiteTemplateModel siteTemplateModel = JsonConvert.DeserializeObject<SiteTemplateModel>(account.user_plan.plan_template);
                DashboardModel dashboardModel = new DashboardModel();
                dashboardModel.User_Id = Convert.ToInt32(Userdata.id);
                dashboardModel = _dashboardApiService.GetDashboard(dashboardModel);
                DashboardViewModel dashboardViewModel = new DashboardViewModel(dashboardModel, siteTemplateModel);
                dashboardViewModel.ContentPlans = _contentPlansService.GetAllContentPlans();
                dashboardViewModel.ContentTypesList = _dashboardApiService.GetAllContentTypes();
                dashboardViewModel.DashboardRecentList = _dashboardApiService.GetRecentsData(Userdata.id);
                var userPlanDetails = dashboardViewModel.ContentPlans.Find(p => p.id == account.user_plan.id);

                HttpContext.Session.SetObject(AppConstants.USERPLANDETAILS, userPlanDetails);

                int planItemCount = Convert.ToInt32(userPlanDetails[content_type.ToLower() + "_count"]);
                int itemCount = Convert.ToInt32(dashboardModel[content_type.ToLower() + "_total"]);

                dashboardViewModel.ContentPlans = dashboardViewModel.ContentPlans.FindAll(p => p.monthly_cents > account.user_plan.monthly_cents );
                dashboardViewModel.GetDashboardCreateList(dashboardModel);
                if (itemCount < planItemCount || account.user_plan.name == "Unlimited")
                {
                    long id = _dashboardApiService.CreateItem(content_type, account);
                    return RedirectToAction("View" + content_type, content_type, new { id = id });
                }
                else
                {
                    string Message = "You have Exceeded the maximum allowed content for " + content_type + ".";
                    ViewBag.JavaScriptFunction = string.Format("showModal('{0}');", Message);
                    return View("Index", dashboardViewModel);
                }
            }
            catch
            {
                return View();
            }
        }

        public IActionResult CreateItem()
        {
            return View();
        }
    }
}
