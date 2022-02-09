using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IItemsService
	{
		ResponseModel<string> AddItemsData(ItemsModel Data);

		ResponseModel<ItemsModel> GetItemsData(ItemsModel Data);

		ResponseModel<string> DeleteItemsData(ItemsModel Data);

		ResponseModel<List<ItemsModel>> GetAllItemsData(long UserId);

	}
}
