using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.World.Api.Models;
using My.World.Api.DataAccess;
using System.Web;

namespace My.World.Api.Services
{
	public interface IDocumentsService
	{

		ResponseModel<string> AdddocumentsData(DocumentsModel Data);

		ResponseModel<string> UpdatedocumentsData(DocumentsModel Data);

		ResponseModel<DocumentsModel> GetdocumentsData(DocumentsModel Data);

		ResponseModel<string> DeletedocumentsData(DocumentsModel Data);

		ResponseModel<List<DocumentsModel>> GetAlldocumentsData(long User_Id);

	}
}
