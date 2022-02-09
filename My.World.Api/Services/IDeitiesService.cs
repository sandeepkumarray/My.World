using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IDeitiesService
	{

		ResponseModel<string> AddDeitiesData(DeitiesModel Data);

		ResponseModel<DeitiesModel> GetDeitiesData(DeitiesModel Data);

		ResponseModel<string> DeleteDeitiesData(DeitiesModel Data);

		ResponseModel<List<DeitiesModel >> GetAllDeitiesForUserID(long userId);

		ResponseModel<string> SaveDeitie(DeitiesModel Data);

	}
}
