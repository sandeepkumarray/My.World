using Minio;
using My.World.Api.Models;
using My.World.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
    public class ObjectStorage : IObjectStorage
    {
        //string endpoint = "192.168.0.2:9000";
        //string accessKey = "myworldadmin";
        //string secretKey = "myworldadmin";
        //string bucketName = "my-world-main";
        //string location = "in-south-1";

        ObjectStorageKeysModel objectStorageKeysModel = new ObjectStorageKeysModel();
        public ResponseModel<string> GetObject(string ObjectID)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            try
            {
                var minio = new MinioClient(objectStorageKeysModel.endpoint, objectStorageKeysModel.accessKey, objectStorageKeysModel.secretKey);
                var stat = minio.GetObjectAsync(objectStorageKeysModel.bucketName, ObjectID, ObjectID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        public void SetObjectStorageSecrets(IObjectstoragekeysApiService _iObjectstoragekeysApiService)
        {
            objectStorageKeysModel = _iObjectstoragekeysApiService.GetObjectStorageKeys(new ObjectStorageKeysModel() { id = 1 });
        }

        public ResponseModel<string> UploadObject(ContentObjectModel model)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            string objectName = Path.GetFileName(model.object_name);
            string contentType = Utility.GetcontentType(objectName);

            try
            {
                var minio = new MinioClient()
                                       .WithEndpoint(objectStorageKeysModel.endpoint)
                                       .WithCredentials(objectStorageKeysModel.accessKey, objectStorageKeysModel.secretKey)
                                       .Build();
                var bktExistArgs = new BucketExistsArgs()
                                                .WithBucket(objectStorageKeysModel.bucketName);
                bool found = minio.BucketExistsAsync(bktExistArgs).Result;
                if (!found)
                {
                    var mkBktArgs = new MakeBucketArgs()
                                                .WithBucket(objectStorageKeysModel.bucketName)
                                                .WithLocation(objectStorageKeysModel.location);
                    minio.MakeBucketAsync(mkBktArgs);
                }
                PutObjectArgs putObjectArgs = new PutObjectArgs()
                                                        .WithBucket(objectStorageKeysModel.bucketName)
                                                        .WithObject(objectName)
                                                        .WithStreamData(model.file)
                                                        .WithContentType(model.object_type);

                minio.PutObjectAsync(objectStorageKeysModel.bucketName, objectName, model.file, model.object_size, model.object_type);
                
                //minio.PutObjectAsync(putObjectArgs);
                response.Value = "Successfully uploaded " + objectName;
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }
    }
}
