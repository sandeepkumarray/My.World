using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IContentTypesService
	{

		ResponseModel<string> AddContentTypesData(ContentTypesModel Data);

		ResponseModel<ContentTypesModel> GetContentTypesData(ContentTypesModel Data);

		ResponseModel<string> DeleteContentTypesData(ContentTypesModel Data);

		ResponseModel<List<ContentTypesModel >> GetAllContentTypesForUserID(long userId);

		ResponseModel<string> SaveContenttype(ContentTypesModel Data);

	}
}
