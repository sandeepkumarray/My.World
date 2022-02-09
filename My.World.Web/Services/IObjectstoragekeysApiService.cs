using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IObjectstoragekeysApiService
	{

		ObjectStorageKeysModel GetObjectStorageKeys(ObjectStorageKeysModel model);

		string DeleteObjectStorageKeys(ObjectStorageKeysModel model);

		List<ObjectStorageKeysModel> GetAllObjectStorageKeys(long UserId);

		ResponseModel<string> SaveObjectStorageKey(ObjectStorageKeysModel model);

	}
}
