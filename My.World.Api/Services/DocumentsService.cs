using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.World.Api.Models;
using My.World.Api.DataAccess;
using System.Web;

namespace My.World.Api.Services
{
	public class DocumentsService : IDocumentsService
	{
		public DBContext dbContext { get; set; }


		public DocumentsService()
		{
		}

		public  DocumentsService(DBContext dbContext)
		{
			this.dbContext = dbContext;

		}

		public ResponseModel<string> AdddocumentsData(DocumentsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                DocumentsDAL DocumentsDALobj = new DocumentsDAL(dbContext);
                string value = DocumentsDALobj.AdddocumentsData(Data);
                return_value.Value = value;
                return_value.Message = "Success";
                return_value.HttpStatusCode = "200";
                return_value.IsSuccess = true;
            }
            catch (Exception)
            {

                throw;
            }
            return return_value;

		}

		public ResponseModel<string> UpdatedocumentsData(DocumentsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                DocumentsDAL DocumentsDALobj = new DocumentsDAL(dbContext);
                string value = DocumentsDALobj.UpdatedocumentsData(Data);
                return_value.Value = value;
                return_value.Message = "Success";
                return_value.HttpStatusCode = "200";
                return_value.IsSuccess = true;
            }
            catch (Exception)
            {

                throw;
            }
            return return_value;

		}

		public ResponseModel<DocumentsModel> GetdocumentsData(DocumentsModel Data)
		{
			ResponseModel<DocumentsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<DocumentsModel>();
                DocumentsDAL DocumentsDALobj = new DocumentsDAL(dbContext);
                DocumentsModel value = DocumentsDALobj.GetdocumentsData(Data);
                return_value.Value = value;
                return_value.Message = "Success";
                return_value.HttpStatusCode = "200";
                return_value.IsSuccess = true;
            }
            catch (Exception)
            {

                throw;
            }
            return return_value;

		}

		public ResponseModel<string> DeletedocumentsData(DocumentsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                DocumentsDAL DocumentsDALobj = new DocumentsDAL(dbContext);
                string value = DocumentsDALobj.DeletedocumentsData(Data);
                return_value.Value = value;
                return_value.Message = "Success";
                return_value.HttpStatusCode = "200";
                return_value.IsSuccess = true;
            }
            catch (Exception)
            {

                throw;
            }
            return return_value;

		}

		public ResponseModel<List<DocumentsModel>> GetAlldocumentsData(long userId)
		{
			ResponseModel<List<DocumentsModel>> return_value = null;
            try
            {
                return_value = new ResponseModel<List<DocumentsModel>>();
                DocumentsDAL DocumentsDALobj = new DocumentsDAL(dbContext);
                List<DocumentsModel> value = DocumentsDALobj.SelectAlldocumentsData(userId);
                return_value.Value = value;
                return_value.Message = "Success";
                return_value.HttpStatusCode = "200";
                return_value.IsSuccess = true;
            }
            catch (Exception)
            {

                throw;
            }
            return return_value;

		}

        public ResponseModel<List<DocumentsModel>> GetAllFolderDocumentsData(long userId, long folderId)
        {
            ResponseModel<List<DocumentsModel>> return_value = null;
            try
            {
                return_value = new ResponseModel<List<DocumentsModel>>();
                DocumentsDAL DocumentsDALobj = new DocumentsDAL(dbContext);
                List<DocumentsModel> value = DocumentsDALobj.GetAllFolderDocumentsData(userId, folderId);
                return_value.Value = value;
                return_value.Message = "Success";
                return_value.HttpStatusCode = "200";
                return_value.IsSuccess = true;
            }
            catch (Exception)
            {

                throw;
            }
            return return_value;

        }

        public ResponseModel<string> Savedocuments(DocumentsModel Data)
        {
            ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                DocumentsDAL DocumentsDALobj = new DocumentsDAL(dbContext);
                string value = DocumentsDALobj.SaveData(Data);
                return_value.Value = value;
                return_value.Message = "Success";
                return_value.HttpStatusCode = "200";
                return_value.IsSuccess = true;
            }
            catch (Exception)
            {

                throw;
            }
            return return_value;

        }

    }
}
