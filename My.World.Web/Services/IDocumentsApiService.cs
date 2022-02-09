using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.World.Api.Models;
using System.Web;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IDocumentsApiService
	{

		Int64 Adddocuments(DocumentsModel model);

		string Updatedocuments(DocumentsModel model);

		DocumentsModel Getdocuments(DocumentsModel model);

		string Deletedocuments(DocumentsModel model);

		List<DocumentsModel> GetAlldocuments(long User_Id);
		ResponseModel<string> Savedocuments(DocumentsModel model);
		List<DocumentsModel> GetAllFolderDocuments(long User_Id, long FolderId);
	}
}
