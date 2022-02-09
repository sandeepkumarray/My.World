using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
    public class UniversesService : IUniversesService
    {
        public DBContext dbContext { get; set; }

        public UniversesService(DBContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public ResponseModel<UniversesModel> GetUniversesData(UniversesModel Data)
        {
            ResponseModel<UniversesModel> return_value = null;
            try
            {
                return_value = new ResponseModel<UniversesModel>();
                UniversesDAL UniversesDALobj = new UniversesDAL(dbContext);
                UniversesModel value = UniversesDALobj.GetUniversesData(Data);
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

        public ResponseModel<string> DeleteUniversesData(UniversesModel Data)
        {
            ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UniversesDAL UniversesDALobj = new UniversesDAL(dbContext);
                string value = UniversesDALobj.DeleteUniversesData(Data);
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

        public ResponseModel<List<UniversesModel>> GetAllUniversesForUserID(long userId)
        {
            ResponseModel<List<UniversesModel>> return_value = null;
            try
            {
                return_value = new ResponseModel<List<UniversesModel>>();
                UniversesDAL UniversesDALobj = new UniversesDAL(dbContext);
                List<UniversesModel> value = (List<UniversesModel>)UniversesDALobj.GetAllUniversesForUserID(userId);
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

        public ResponseModel<string> SaveUniverse(UniversesModel Data)
        {
            ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UniversesDAL UniversesDALobj = new UniversesDAL(dbContext);
                string value = UniversesDALobj.SaveData(Data);
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

        public ResponseModel<string> AddUniversesData(UniversesModel Data)
        {
            ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UniversesDAL UniversesDALobj = new UniversesDAL(dbContext);
                string value = UniversesDALobj.AddUniversesData(Data);
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
