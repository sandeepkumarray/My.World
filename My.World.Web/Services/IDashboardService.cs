using My.World.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
    interface IDashboardService
    {
        public Task<DashboardModel> GetDashboardData();
        public List<MentionsModel> GetMentionsData();
        public List<DashboardRecentModel> GetRecentsData();
    }
}
