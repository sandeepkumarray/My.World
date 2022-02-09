using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.DataAccess;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public class ItemsService : IItemsService
	{
		public DBContext dbContext { get; set; }


		public ItemsService()
		{
		}

		public  ItemsService(DBContext dbContext)
		{
			this.dbContext = dbContext;

		}

		public ResponseModel<ItemsModel> GetItemsData(ItemsModel Data)
		{
			ResponseModel<ItemsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<ItemsModel>();
                ItemsDAL ItemsDalobj = new ItemsDAL(dbContext);
                ItemsModel value = ItemsDalobj.GetItemsData(Data);
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

		public ResponseModel<string> DeleteItemsData(ItemsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ItemsDAL ItemsDalobj = new ItemsDAL(dbContext);
                string value = ItemsDalobj.DeleteItemsData(Data);
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

		public ResponseModel<List<ItemsModel>> GetAllItemsData(long userId)
		{
			ResponseModel<List<ItemsModel>> return_value = null;
            try
            {
                return_value = new ResponseModel<List<ItemsModel>>();
                ItemsDAL ItemsDalobj = new ItemsDAL(dbContext);
                List<ItemsModel> value = ItemsDalobj.GetAllItemsForUserID(userId);
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

        public ResponseModel<string> AddItemsData(ItemsModel Data)
        {
            ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ItemsDAL ItemsDalobj = new ItemsDAL(dbContext);
                string value = ItemsDalobj.AddItemsData(Data);
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

        public ResponseModel<string> SaveItem(ItemsModel Data)
        {
            ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ItemsDAL ItemsDalobj = new ItemsDAL(dbContext);
                string value = ItemsDalobj.SaveData(Data);
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
