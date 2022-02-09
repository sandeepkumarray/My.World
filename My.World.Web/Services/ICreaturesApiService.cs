using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface ICreaturesApiService
	{

		CreaturesModel GetCreatures(CreaturesModel model);

		string DeleteCreatures(CreaturesModel model);

		List<CreaturesModel> GetAllCreatures(long UserId);

		ResponseModel<string> SaveCreature(CreaturesModel model);

	}
}
