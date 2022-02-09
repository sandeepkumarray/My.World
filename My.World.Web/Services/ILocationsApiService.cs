using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface ILocationsApiService
	{

		LocationsModel GetLocations(LocationsModel model);

		string DeleteLocations(LocationsModel model);

		List<LocationsModel> GetAllLocations(long UserId);

		ResponseModel<string> SaveLocation(LocationsModel model);

	}
}
