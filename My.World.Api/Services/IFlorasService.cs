using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IFlorasService
	{

		ResponseModel<string> AddFlorasData(FlorasModel Data);

		ResponseModel<FlorasModel> GetFlorasData(FlorasModel Data);

		ResponseModel<string> DeleteFlorasData(FlorasModel Data);

		ResponseModel<List<FlorasModel >> GetAllFlorasForUserID(long userId);

		ResponseModel<string> SaveFlora(FlorasModel Data);

	}
}
