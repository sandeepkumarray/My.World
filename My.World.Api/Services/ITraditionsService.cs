using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface ITraditionsService
	{

		ResponseModel<string> AddTraditionsData(TraditionsModel Data);

		ResponseModel<TraditionsModel> GetTraditionsData(TraditionsModel Data);

		ResponseModel<string> DeleteTraditionsData(TraditionsModel Data);

		ResponseModel<List<TraditionsModel >> GetAllTraditionsForUserID(long userId);

		ResponseModel<string> SaveTradition(TraditionsModel Data);

		ResponseModel<string> UpdateTraditionsData(TraditionsModel Data);

	}
}
