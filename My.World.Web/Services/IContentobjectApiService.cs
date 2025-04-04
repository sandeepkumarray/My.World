using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IContentObjectApiService
	{
		string AddContentObject(ContentObjectModel model);
		ContentObjectModel GetContentObject(ContentObjectModel model);

		string DeleteContentObject(ContentObjectModel model);

		List<ContentObjectModel> GetAllContentObject(long UserId);

		ResponseModel<string> SaveContentObject(ContentObjectModel model);

	}
}
