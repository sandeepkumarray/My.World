using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IUserContentBucketService
	{

		ResponseModel<string> AddUserContentBucketData(UserContentBucketModel Data);

		ResponseModel<UserContentBucketModel> GetUserContentBucketData(UserContentBucketModel Data);

		ResponseModel<string> DeleteUserContentBucketData(UserContentBucketModel Data);

		ResponseModel<List<UserContentBucketModel >> GetAllUserContentBucketForUserID(long userId);

		ResponseModel<string> SaveUsercontentbucke(UserContentBucketModel Data);

	}
}
