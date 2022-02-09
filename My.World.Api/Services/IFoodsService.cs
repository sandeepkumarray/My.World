using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IFoodsService
	{

		ResponseModel<string> AddFoodsData(FoodsModel Data);

		ResponseModel<FoodsModel> GetFoodsData(FoodsModel Data);

		ResponseModel<string> DeleteFoodsData(FoodsModel Data);

		ResponseModel<List<FoodsModel >> GetAllFoodsForUserID(long userId);

		ResponseModel<string> SaveFood(FoodsModel Data);

	}
}
