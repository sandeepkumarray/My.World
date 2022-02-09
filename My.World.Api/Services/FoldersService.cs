using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.World.Api.Models;
using My.World.Api.DataAccess;
using System.Web;

namespace My.World.Api.Services
{
	public class FoldersService : IFoldersService
	{
		public DBContext dbContext { get; set; }


		public FoldersService()
		{
		}

		public  FoldersService(DBContext dbContext)
		{
			this.dbContext = dbContext;

		}

		public ResponseModel<string> AddfoldersData(FoldersModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                FoldersDAL FoldersDALobj = new FoldersDAL(dbContext);
                string value = FoldersDALobj.AddfoldersData(Data);
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

		public ResponseModel<string> UpdatefoldersData(FoldersModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                FoldersDAL FoldersDALobj = new FoldersDAL(dbContext);
                string value = FoldersDALobj.UpdatefoldersData(Data);
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

		public ResponseModel<FoldersModel> GetfoldersData(FoldersModel Data)
		{
			ResponseModel<FoldersModel> return_value = null;
            try
            {
                return_value = new ResponseModel<FoldersModel>();
                FoldersDAL FoldersDALobj = new FoldersDAL(dbContext);
                FoldersModel value = FoldersDALobj.GetfoldersData(Data);
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

		public ResponseModel<string> DeletefoldersData(FoldersModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                FoldersDAL FoldersDALobj = new FoldersDAL(dbContext);
                string value = FoldersDALobj.DeletefoldersData(Data);
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

		public ResponseModel<List<FoldersModel>> GetAllfoldersData(long userId)
		{
			ResponseModel<List<FoldersModel>> return_value = null;
            try
            {
                return_value = new ResponseModel<List<FoldersModel>>();
                FoldersDAL FoldersDALobj = new FoldersDAL(dbContext);
                List<FoldersModel> value = FoldersDALobj.SelectAllfoldersData(userId);
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

        public ResponseModel<List<FoldersModel>> GetAllChildFoldersData(long folderId)
        {
            ResponseModel<List<FoldersModel>> return_value = null;
            try
            {
                return_value = new ResponseModel<List<FoldersModel>>();
                FoldersDAL FoldersDALobj = new FoldersDAL(dbContext);
                List<FoldersModel> value = FoldersDALobj.GetAllChildFoldersData(folderId);
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

        public ResponseModel<List<FoldersModel>> GetEligibleParentFoldersData(long userId, long folderId)
        {
            ResponseModel<List<FoldersModel>> return_value = null;
            try
            {
                return_value = new ResponseModel<List<FoldersModel>>();
                FoldersDAL FoldersDALobj = new FoldersDAL(dbContext);
                List<FoldersModel> value = FoldersDALobj.GetEligibleParentFoldersData(userId, folderId);
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
