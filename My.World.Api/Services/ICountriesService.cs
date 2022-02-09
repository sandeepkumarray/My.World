using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface ICountriesService
	{

		ResponseModel<string> AddCountriesData(CountriesModel Data);

		ResponseModel<CountriesModel> GetCountriesData(CountriesModel Data);

		ResponseModel<string> DeleteCountriesData(CountriesModel Data);

		ResponseModel<List<CountriesModel >> GetAllCountriesForUserID(long userId);

		ResponseModel<string> SaveCountrie(CountriesModel Data);

	}
}
