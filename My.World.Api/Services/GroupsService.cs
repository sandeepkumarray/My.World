using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class GroupsService : IGroupsService
	{
		public DBContext dbContext;


		public GroupsService()
		{
		}

		public  GroupsService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddGroupsData(GroupsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                GroupsDAL GroupsDalobj = new GroupsDAL(dbContext);
                string value = GroupsDalobj.AddGroupsData(Data);
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

		public ResponseModel<GroupsModel> GetGroupsData(GroupsModel Data)
		{
			ResponseModel<GroupsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<GroupsModel>();
                GroupsDAL GroupsDalobj = new GroupsDAL(dbContext);
                GroupsModel value = GroupsDalobj.GetGroupsData(Data);
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

		public ResponseModel<string> DeleteGroupsData(GroupsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                GroupsDAL GroupsDalobj = new GroupsDAL(dbContext);
                string value = GroupsDalobj.DeleteGroupsData(Data);
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

		public ResponseModel<List<GroupsModel >> GetAllGroupsForUserID(long userId)
		{
			ResponseModel<List<GroupsModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<GroupsModel >>();
                GroupsDAL GroupsDalobj = new GroupsDAL(dbContext);
                List<GroupsModel> value = GroupsDalobj.GetAllGroupsForUserID(userId);
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

		public ResponseModel<string> SaveGroup(GroupsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                GroupsDAL GroupsDalobj = new GroupsDAL(dbContext);
                string value = GroupsDalobj.SaveData(Data);
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

		public ResponseModel<string> UpdateGroupsData(GroupsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                GroupsDAL GroupsDalobj = new GroupsDAL(dbContext);
                string value = GroupsDalobj.UpdateGroupsData(Data);
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
