using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IContenttypesApiService
	{

		ContentTypesModel GetContentTypes(ContentTypesModel model);

		string DeleteContentTypes(ContentTypesModel model);

		List<ContentTypesModel> GetAllContentTypes(long UserId);

		ResponseModel<string> SaveContentType(ContentTypesModel model);

	}
}
