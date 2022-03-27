using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class OrganizationsService : IOrganizationsService
	{
		public DBContext dbContext;


		public OrganizationsService()
		{
		}

		public  OrganizationsService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddOrganizationsData(OrganizationsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                OrganizationsDAL OrganizationsDalobj = new OrganizationsDAL(dbContext);
                string value = OrganizationsDalobj.AddOrganizationsData(Data);
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

		public ResponseModel<OrganizationsModel> GetOrganizationsData(OrganizationsModel Data)
		{
			ResponseModel<OrganizationsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<OrganizationsModel>();
                OrganizationsDAL OrganizationsDalobj = new OrganizationsDAL(dbContext);
                OrganizationsModel value = OrganizationsDalobj.GetOrganizationsData(Data);
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

		public ResponseModel<string> DeleteOrganizationsData(OrganizationsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                OrganizationsDAL OrganizationsDalobj = new OrganizationsDAL(dbContext);
                string value = OrganizationsDalobj.DeleteOrganizationsData(Data);
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

		public ResponseModel<List<OrganizationsModel >> GetAllOrganizationsForUserID(long userId)
		{
			ResponseModel<List<OrganizationsModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<OrganizationsModel >>();
                OrganizationsDAL OrganizationsDalobj = new OrganizationsDAL(dbContext);
                List<OrganizationsModel> value = OrganizationsDalobj.GetAllOrganizationsForUserID(userId);
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

		public ResponseModel<string> SaveOrganization(OrganizationsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                OrganizationsDAL OrganizationsDalobj = new OrganizationsDAL(dbContext);
                string value = OrganizationsDalobj.SaveData(Data);
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

		public ResponseModel<string> UpdateOrganizationsData(OrganizationsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                OrganizationsDAL OrganizationsDalobj = new OrganizationsDAL(dbContext);
                string value = OrganizationsDalobj.UpdateOrganizationsData(Data);
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
