using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IObjectStorageKeysService
	{

		ResponseModel<string> AddObjectStorageKeysData(ObjectStorageKeysModel Data);

		ResponseModel<ObjectStorageKeysModel> GetObjectStorageKeysData(ObjectStorageKeysModel Data);

		ResponseModel<string> DeleteObjectStorageKeysData(ObjectStorageKeysModel Data);

		ResponseModel<List<ObjectStorageKeysModel >> GetAllObjectStorageKeysForUserID(long userId);

		ResponseModel<string> SaveObjectstoragekey(ObjectStorageKeysModel Data);

	}
}
