using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IGroupsService
	{

		ResponseModel<string> AddGroupsData(GroupsModel Data);

		ResponseModel<GroupsModel> GetGroupsData(GroupsModel Data);

		ResponseModel<string> DeleteGroupsData(GroupsModel Data);

		ResponseModel<List<GroupsModel >> GetAllGroupsForUserID(long userId);

		ResponseModel<string> SaveGroup(GroupsModel Data);

		ResponseModel<string> UpdateGroupsData(GroupsModel Data);

	}
}
