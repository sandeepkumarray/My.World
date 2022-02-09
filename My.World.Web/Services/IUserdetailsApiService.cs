using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IUserdetailsApiService
	{

		string AddUserDetails(UserDetailsModel model);

		string UpdateUserDetails(UserDetailsModel model);

		UserDetailsModel GetUserDetails(UserDetailsModel model);

		string DeleteUserDetails(UserDetailsModel model);

		List<UserDetailsModel> GetAllUserDetails();

	}
}
