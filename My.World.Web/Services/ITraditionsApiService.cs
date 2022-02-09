using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface ITraditionsApiService
	{

		TraditionsModel GetTraditions(TraditionsModel model);

		string DeleteTraditions(TraditionsModel model);

		List<TraditionsModel> GetAllTraditions(long UserId);

		ResponseModel<string> SaveTradition(TraditionsModel model);

	}
}
