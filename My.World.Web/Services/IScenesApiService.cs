using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IScenesApiService
	{

		ScenesModel GetScenes(ScenesModel model);

		string DeleteScenes(ScenesModel model);

		List<ScenesModel> GetAllScenes(long UserId);

		ResponseModel<string> SaveScene(ScenesModel model);

	}
}
