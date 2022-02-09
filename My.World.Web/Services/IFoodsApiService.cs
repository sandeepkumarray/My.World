using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IFoodsApiService
	{

		FoodsModel GetFoods(FoodsModel model);

		string DeleteFoods(FoodsModel model);

		List<FoodsModel> GetAllFoods(long UserId);

		ResponseModel<string> SaveFood(FoodsModel model);

	}
}
