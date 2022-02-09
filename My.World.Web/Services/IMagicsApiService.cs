using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IMagicsApiService
	{

		MagicsModel GetMagics(MagicsModel model);

		string DeleteMagics(MagicsModel model);

		List<MagicsModel> GetAllMagics(long UserId);

		ResponseModel<string> SaveMagic(MagicsModel model);

	}
}
