using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface ILocationsService
	{

		ResponseModel<string> AddLocationsData(LocationsModel Data);

		ResponseModel<LocationsModel> GetLocationsData(LocationsModel Data);

		ResponseModel<string> DeleteLocationsData(LocationsModel Data);

		ResponseModel<List<LocationsModel >> GetAllLocationsForUserID(long userId);

		ResponseModel<string> SaveLocation(LocationsModel Data);

		ResponseModel<string> UpdateLocationsData(LocationsModel Data);

	}
}
