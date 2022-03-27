using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class FoodsService : IFoodsService
	{
		public DBContext dbContext;


		public FoodsService()
		{
		}

		public  FoodsService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddFoodsData(FoodsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                FoodsDAL FoodsDalobj = new FoodsDAL(dbContext);
                string value = FoodsDalobj.AddFoodsData(Data);
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

		public ResponseModel<FoodsModel> GetFoodsData(FoodsModel Data)
		{
			ResponseModel<FoodsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<FoodsModel>();
                FoodsDAL FoodsDalobj = new FoodsDAL(dbContext);
                FoodsModel value = FoodsDalobj.GetFoodsData(Data);
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

		public ResponseModel<string> DeleteFoodsData(FoodsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                FoodsDAL FoodsDalobj = new FoodsDAL(dbContext);
                string value = FoodsDalobj.DeleteFoodsData(Data);
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

		public ResponseModel<List<FoodsModel >> GetAllFoodsForUserID(long userId)
		{
			ResponseModel<List<FoodsModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<FoodsModel >>();
                FoodsDAL FoodsDalobj = new FoodsDAL(dbContext);
                List<FoodsModel> value = FoodsDalobj.GetAllFoodsForUserID(userId);
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

		public ResponseModel<string> SaveFood(FoodsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                FoodsDAL FoodsDalobj = new FoodsDAL(dbContext);
                string value = FoodsDalobj.SaveData(Data);
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

		public ResponseModel<string> UpdateFoodsData(FoodsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                FoodsDAL FoodsDalobj = new FoodsDAL(dbContext);
                string value = FoodsDalobj.UpdateFoodsData(Data);
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
