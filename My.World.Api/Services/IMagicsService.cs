using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IMagicsService
	{

		ResponseModel<string> AddMagicsData(MagicsModel Data);

		ResponseModel<MagicsModel> GetMagicsData(MagicsModel Data);

		ResponseModel<string> DeleteMagicsData(MagicsModel Data);

		ResponseModel<List<MagicsModel >> GetAllMagicsForUserID(long userId);

		ResponseModel<string> SaveMagic(MagicsModel Data);

	}
}
