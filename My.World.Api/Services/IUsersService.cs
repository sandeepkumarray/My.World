using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IUsersService
	{

		ResponseModel<string> AddUsersData(UsersModel Data);

		ResponseModel<string> UpdateUsersData(UsersModel Data);

		ResponseModel<UsersModel> GetUsersData(UsersModel Data);

		ResponseModel<string> DeleteUsersData(UsersModel Data);

		ResponseModel<List<UsersModel>> GetAllUsersData();

	}
}
