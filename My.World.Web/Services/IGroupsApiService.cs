using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IGroupsApiService
	{

		GroupsModel GetGroups(GroupsModel model);

		string DeleteGroups(GroupsModel model);

		List<GroupsModel> GetAllGroups(long UserId);

		ResponseModel<string> SaveGroup(GroupsModel model);

	}
}
