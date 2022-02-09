using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface ILandmarksApiService
	{

		LandmarksModel GetLandmarks(LandmarksModel model);

		string DeleteLandmarks(LandmarksModel model);

		List<LandmarksModel> GetAllLandmarks(long UserId);

		ResponseModel<string> SaveLandmark(LandmarksModel model);

	}
}
