using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IUserdetailsService
	{

		ResponseModel<string> AddUserDetailsData(UserDetailsModel Data);

		ResponseModel<string> UpdateUserDetailsData(UserDetailsModel Data);

		ResponseModel<UserDetailsModel> GetUserDetailsData(UserDetailsModel Data);

		ResponseModel<string> DeleteUserDetailsData(UserDetailsModel Data);

		ResponseModel<List<UserDetailsModel>> GetAllUserDetailsData();

	}
}
