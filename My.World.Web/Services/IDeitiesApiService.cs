using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IDeitiesApiService
	{

		DeitiesModel GetDeities(DeitiesModel model);

		string DeleteDeities(DeitiesModel model);

		List<DeitiesModel> GetAllDeities(long UserId);

		ResponseModel<string> SaveDeitie(DeitiesModel model);

	}
}
