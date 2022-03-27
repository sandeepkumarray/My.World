using Microsoft.AspNetCore.Mvc;
using My.World.Api.Models;
using My.World.Web.Models;
using My.World.Web.Services;
using My.World.Web.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IDashboardApiService _dashboardApiService;
        public readonly IUsersApiService _iUsersApiService;
        private readonly IContentPlansApiService _contentPlansService;

        public List<DashboardItem> DashboardItemList { get; private set; }

        public MenuViewComponent(IDashboardApiService dashboardApiService, IUsersApiService iUsersApiService, IContentPlansApiService iContentplansApiService, IContentTypesApiService IContentTypesApiService)
        {
            _dashboardApiService = dashboardApiService;
            _iUsersApiService = iUsersApiService;
            _contentPlansService = iContentplansApiService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            UsersModel Userdata = new UsersModel();
            Userdata.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
            var account = _iUsersApiService.GetUsers(Userdata);
            DashboardModel dashboardModel = new DashboardModel();
            dashboardModel.User_Id = Convert.ToInt64(Userdata.id);

            SiteTemplateModel siteTemplateModel = JsonConvert.DeserializeObject<SiteTemplateModel>(account.user_plan.plan_template);
            dashboardModel = _dashboardApiService.GetDashboard(dashboardModel);
            var ContentTypesList = _dashboardApiService.GetAllContentTypes();
            var content_list = siteTemplateModel.PlanContentList.OrderBy(i => i.OrderId);
            var ContentPlans = _contentPlansService.GetAllContentPlans();

            var FreePlan = ContentPlans.Find(p => p.name == "Free");

            SiteTemplateModel freePlanTemplate = JsonConvert.DeserializeObject<SiteTemplateModel>(FreePlan.plan_template);
            IEnumerable<string> freePlanContentList = from plan in freePlanTemplate.PlanContentList
                                               select plan.name;

            DashboardItemList = new List<DashboardItem>();
            MenuViewModel menuViewModel = new MenuViewModel();
            foreach (var plan in freePlanTemplate.PlanContentList)
            {
                menuViewModel.DefaultPlanMenuList.Add(new MenuItem()
                {
                    Header = plan.name,
                    CountString = Convert.ToString(dashboardModel[plan.name.ToLower() + "_total"]),
                    Color = ContentTypesList.Find(c => c.name.ToLower() == plan.name.ToLower()).primary_color,
                    Icon = ContentTypesList.Find(c => c.name.ToLower() == plan.name.ToLower()).icon.Replace("fa-3x", "fa-2x c-sidebar-nav-icon"),
                    ItemKey = plan.name,
                    Controller = plan.name,
                    Action = "Index"
                });
            }

            foreach (var item in content_list)
            {
                if (item.name.NotIn(freePlanContentList))
                {
                    menuViewModel.PremiumPlanMenuList.Add(new MenuItem()
                    {
                        Header = item.name,
                        CountString = Convert.ToString(dashboardModel[item.name.ToLower() + "_total"]),
                        Color = ContentTypesList.Find(c => c.name.ToLower() == item.name.ToLower()).primary_color,
                        Icon = ContentTypesList.Find(c => c.name.ToLower() == item.name.ToLower()).icon.Replace("fa-3x", "fa-2x c-sidebar-nav-icon"),
                        ItemKey = item.name,
                        Controller = item.name,
                        Action = "Index"
                    });
                }
            }

            return View(menuViewModel);
        }
    }
}
