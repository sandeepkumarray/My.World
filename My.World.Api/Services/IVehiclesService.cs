using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IVehiclesService
	{

		ResponseModel<string> AddVehiclesData(VehiclesModel Data);

		ResponseModel<VehiclesModel> GetVehiclesData(VehiclesModel Data);

		ResponseModel<string> DeleteVehiclesData(VehiclesModel Data);

		ResponseModel<List<VehiclesModel >> GetAllVehiclesForUserID(long userId);

		ResponseModel<string> SaveVehicle(VehiclesModel Data);

	}
}
