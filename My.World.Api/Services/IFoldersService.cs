using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.World.Api.Models;
using My.World.Api.DataAccess;
using System.Web;

namespace My.World.Api.Services
{
	public interface IFoldersService
	{

		ResponseModel<string> AddfoldersData(FoldersModel Data);

		ResponseModel<string> UpdatefoldersData(FoldersModel Data);

		ResponseModel<FoldersModel> GetfoldersData(FoldersModel Data);

		ResponseModel<string> DeletefoldersData(FoldersModel Data);

		ResponseModel<List<FoldersModel>> GetAllfoldersData(long userId);

	}
}
