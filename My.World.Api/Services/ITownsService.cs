using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface ITownsService
	{

		ResponseModel<string> AddTownsData(TownsModel Data);

		ResponseModel<TownsModel> GetTownsData(TownsModel Data);

		ResponseModel<string> DeleteTownsData(TownsModel Data);

		ResponseModel<List<TownsModel >> GetAllTownsForUserID(long userId);

		ResponseModel<string> SaveTown(TownsModel Data);

		ResponseModel<string> UpdateTownsData(TownsModel Data);

	}
}
