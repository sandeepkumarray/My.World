using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface ILoresService
	{

		ResponseModel<string> AddLoresData(LoresModel Data);

		ResponseModel<LoresModel> GetLoresData(LoresModel Data);

		ResponseModel<string> DeleteLoresData(LoresModel Data);

		ResponseModel<List<LoresModel >> GetAllLoresForUserID(long userId);

		ResponseModel<string> SaveLore(LoresModel Data);

	}
}
