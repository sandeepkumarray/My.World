using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IContentObjectAttachmentService
	{

		ResponseModel<string> AddContentObjectAttachmentData(ContentObjectAttachmentModel Data);

		ResponseModel<ContentObjectAttachmentModel> GetContentObjectAttachmentData(ContentObjectAttachmentModel Data);

		ResponseModel<string> DeleteContentObjectAttachmentData(ContentObjectAttachmentModel Data);

		ResponseModel<List<ContentObjectAttachmentModel >> GetAllContentObjectAttachmentForUserID(long userId);

		ResponseModel<string> SaveContentobjectattachmen(ContentObjectAttachmentModel Data);

	}
}
