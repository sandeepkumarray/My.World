using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface ICreaturesService
	{

		ResponseModel<string> AddCreaturesData(CreaturesModel Data);

		ResponseModel<CreaturesModel> GetCreaturesData(CreaturesModel Data);

		ResponseModel<string> DeleteCreaturesData(CreaturesModel Data);

		ResponseModel<List<CreaturesModel >> GetAllCreaturesForUserID(long userId);

		ResponseModel<string> SaveCreature(CreaturesModel Data);

		ResponseModel<string> UpdateCreaturesData(CreaturesModel Data);

	}
}
