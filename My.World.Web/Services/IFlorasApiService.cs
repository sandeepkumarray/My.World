using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IFlorasApiService
	{

		FlorasModel GetFloras(FlorasModel model);

		string DeleteFloras(FlorasModel model);

		List<FlorasModel> GetAllFloras(long UserId);

		ResponseModel<string> SaveFlora(FlorasModel model);

	}
}
