using My.World.Api.Models;
using My.World.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
    public interface IDashboardApiService
    {
        DashboardModel GetDashboard(DashboardModel model);
        Int64 CreateItem(string controller, UsersModel userAccount);
        List<MentionsModel> GetMentionsData(long UserId);
        List<DashboardRecentModel> GetRecentsData(long UserId);
        List<ContentTypesModel> GetAllContentTypes();
    }
}
