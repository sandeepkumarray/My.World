using My.World.Api.DataAccess;
using My.World.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Api.Services
{
    public class ContentPlansService
    {
        public DBContext dbContext { get; set; }

        public ContentPlansService(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ResponseModel<List<ContentPlansModel>> GetAllContentPlans()
        {
            ResponseModel<List<ContentPlansModel>> return_value = null;
            try
            {
                return_value = new ResponseModel<List<ContentPlansModel>>();
                ContentPlansDAL ContentPlansDALObj = new ContentPlansDAL(dbContext);
                List<ContentPlansModel> value = ContentPlansDALObj.SelectAllContentPlansData();
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
