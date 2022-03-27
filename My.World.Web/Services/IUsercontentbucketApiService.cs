using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IUserContentBucketApiService
	{
		string AddUserContentBucket(UserContentBucketModel model);
		UserContentBucketModel GetUserContentBucket(UserContentBucketModel model);

		string DeleteUserContentBucket(UserContentBucketModel model);

		List<UserContentBucketModel> GetAllUserContentBucket(long UserId);

		ResponseModel<string> SaveUserContentBucke(UserContentBucketModel model);

	}
}
