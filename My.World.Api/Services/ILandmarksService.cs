using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface ILandmarksService
	{

		ResponseModel<string> AddLandmarksData(LandmarksModel Data);

		ResponseModel<LandmarksModel> GetLandmarksData(LandmarksModel Data);

		ResponseModel<string> DeleteLandmarksData(LandmarksModel Data);

		ResponseModel<List<LandmarksModel >> GetAllLandmarksForUserID(long userId);

		ResponseModel<string> SaveLandmark(LandmarksModel Data);

		ResponseModel<string> UpdateLandmarksData(LandmarksModel Data);

	}
}
