using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IVehiclesApiService
	{

		VehiclesModel GetVehicles(VehiclesModel model);

		string DeleteVehicles(VehiclesModel model);

		List<VehiclesModel> GetAllVehicles(long UserId);

		ResponseModel<string> SaveVehicle(VehiclesModel model);

	}
}
