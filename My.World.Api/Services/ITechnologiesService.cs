using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface ITechnologiesService
	{

		ResponseModel<string> AddTechnologiesData(TechnologiesModel Data);

		ResponseModel<TechnologiesModel> GetTechnologiesData(TechnologiesModel Data);

		ResponseModel<string> DeleteTechnologiesData(TechnologiesModel Data);

		ResponseModel<List<TechnologiesModel >> GetAllTechnologiesForUserID(long userId);

		ResponseModel<string> SaveTechnologie(TechnologiesModel Data);

		ResponseModel<string> UpdateTechnologiesData(TechnologiesModel Data);

	}
}
