using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IContentObjectService
	{

		ResponseModel<string> AddContentObjectData(ContentObjectModel Data);

		ResponseModel<ContentObjectModel> GetContentObjectData(ContentObjectModel Data);

		ResponseModel<string> DeleteContentObjectData(ContentObjectModel Data);

		ResponseModel<List<ContentObjectModel >> GetAllContentObjectForUserID(long userId);

		ResponseModel<string> SaveContentobjec(ContentObjectModel Data);

	}
}
