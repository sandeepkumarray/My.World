using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface ILoresApiService
	{

		LoresModel GetLores(LoresModel model);

		string DeleteLores(LoresModel model);

		List<LoresModel> GetAllLores(long UserId);

		ResponseModel<string> SaveLore(LoresModel model);

	}
}
