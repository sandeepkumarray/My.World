using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IContentobjectattachmentApiService
	{
		string AddContentObjectAttachment(ContentObjectAttachmentModel model);
		ContentObjectAttachmentModel GetContentObjectAttachment(ContentObjectAttachmentModel model);

		string DeleteContentObjectAttachment(ContentObjectAttachmentModel model);

		List<ContentObjectAttachmentModel> GetAllContentObjectAttachment(long UserId);

		ResponseModel<string> SaveContentObjectAttachmen(ContentObjectAttachmentModel model);

	}
}
