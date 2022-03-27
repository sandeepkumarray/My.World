using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IItemsApiService
	{

		ItemsModel GetItems(ItemsModel model);

		string DeleteItems(ItemsModel model);

		List<ItemsModel> GetAllItems(long UserId);

		ResponseModel<string> SaveItem(ItemsModel model);

	}
}
