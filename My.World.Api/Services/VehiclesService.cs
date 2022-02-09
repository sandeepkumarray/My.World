using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class VehiclesService : IVehiclesService
	{
		public DBContext dbContext;


		public VehiclesService()
		{
		}

		public  VehiclesService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddVehiclesData(VehiclesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                VehiclesDAL VehiclesDalobj = new VehiclesDAL(dbContext);
                string value = VehiclesDalobj.AddVehiclesData(Data);
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

		public ResponseModel<VehiclesModel> GetVehiclesData(VehiclesModel Data)
		{
			ResponseModel<VehiclesModel> return_value = null;
            try
            {
                return_value = new ResponseModel<VehiclesModel>();
                VehiclesDAL VehiclesDalobj = new VehiclesDAL(dbContext);
                VehiclesModel value = VehiclesDalobj.GetVehiclesData(Data);
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

		public ResponseModel<string> DeleteVehiclesData(VehiclesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                VehiclesDAL VehiclesDalobj = new VehiclesDAL(dbContext);
                string value = VehiclesDalobj.DeleteVehiclesData(Data);
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

		public ResponseModel<List<VehiclesModel >> GetAllVehiclesForUserID(long userId)
		{
			ResponseModel<List<VehiclesModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<VehiclesModel >>();
                VehiclesDAL VehiclesDalobj = new VehiclesDAL(dbContext);
                List<VehiclesModel> value = VehiclesDalobj.GetAllVehiclesForUserID(userId);
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

		public ResponseModel<string> SaveVehicle(VehiclesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                VehiclesDAL VehiclesDalobj = new VehiclesDAL(dbContext);
                string value = VehiclesDalobj.SaveData(Data);
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
