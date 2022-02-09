using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IReligionsApiService
	{

		ReligionsModel GetReligions(ReligionsModel model);

		string DeleteReligions(ReligionsModel model);

		List<ReligionsModel> GetAllReligions(long UserId);

		ResponseModel<string> SaveReligion(ReligionsModel model);

	}
}
