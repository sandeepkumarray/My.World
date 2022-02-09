using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface ICountriesApiService
	{

		CountriesModel GetCountries(CountriesModel model);

		string DeleteCountries(CountriesModel model);

		List<CountriesModel> GetAllCountries(long UserId);

		ResponseModel<string> SaveCountrie(CountriesModel model);

	}
}
