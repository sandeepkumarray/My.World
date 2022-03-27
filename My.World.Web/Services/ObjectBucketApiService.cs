using Minio;
using My.World.Api.Models;
using My.World.Web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
    public class ObjectBucketApiService : BaseAPIService, IObjectBucketApiService
    {
        MinioClient minioClient;
        public ObjectStorageKeysModel objectStorageKeysModel { get; set; }

        public string DeleteObjectStorageKeys(ObjectStorageKeysModel model)
        {
            throw new NotImplementedException();
        }

        public string DeleteUserContentBucket(UserContentBucketModel model)
        {
            throw new NotImplementedException();
        }

        public List<ObjectStorageKeysModel> GetAllObjectStorageKeys(long UserId)
        {
            throw new NotImplementedException();
        }

        public List<UserContentBucketModel> GetAllUserContentBucket(long UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<string>> GetObject(ContentObjectModel model)
        {
            ResponseModel<string> response = new ResponseModel<string>();
            StatObjectArgs objectStatArgs = new StatObjectArgs()
                                                   .WithBucket(objectStorageKeysModel.bucketName)
                                                   .WithObject(model.object_name);
            var statObject = await minioClient.StatObjectAsync(objectStatArgs);
            response.Value = null;
            return response;
        }

        public ResponseModel<string> SaveObjectStorageKey(ObjectStorageKeysModel model)
        {
            throw new NotImplementedException();
        }

        public ResponseModel<string> SaveUserContentBucke(UserContentBucketModel model)
        {
            throw new NotImplementedException();
        }

        public ObjectStorageKeysModel GetObjectStorageKeys(ObjectStorageKeysModel model)
        {
            ObjectStorageKeysModel objectstoragekeysModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetObjectStorageKeys";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<ObjectStorageKeysModel> response = JsonConvert.DeserializeObject<ResponseModel<ObjectStorageKeysModel>>(jsonResult);
            objectstoragekeysModel = response.Value;
            return objectstoragekeysModel;

        }

        public string AddUserContentBucket(UserContentBucketModel model)
        {
            string usercontentbucketModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "AddUserContentBucket";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            usercontentbucketModel = response.Value;
            return usercontentbucketModel;

        }

        public UserContentBucketModel GetUserContentBucket(UserContentBucketModel model)
        {
            UserContentBucketModel usercontentbucketModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetUserContentBucket";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<UserContentBucketModel> response = JsonConvert.DeserializeObject<ResponseModel<UserContentBucketModel>>(jsonResult);
            usercontentbucketModel = response.Value;
            return usercontentbucketModel;

        }

        public async void SetObjectStorageSecrets(long accountID)
        {
            var userContentBucketModel = GetUserContentBucket(new UserContentBucketModel() { user_id = accountID });
            objectStorageKeysModel = GetObjectStorageKeys(new ObjectStorageKeysModel() { id = 1 });
            objectStorageKeysModel.bucketName = userContentBucketModel.bucket_Name;

            minioClient = new MinioClient()
                                       .WithEndpoint(objectStorageKeysModel.endpoint)
                                       .WithCredentials(objectStorageKeysModel.accessKey, objectStorageKeysModel.secretKey)
                                       .Build();
            //Check bucket exists
            var bktExistArgs = new BucketExistsArgs()
                                               .WithBucket(objectStorageKeysModel.bucketName);
            bool found = minioClient.BucketExistsAsync(bktExistArgs).Result;
            //Create bucket
            if (!found)
            {
                var mkBktArgs = new MakeBucketArgs()
                                            .WithBucket(objectStorageKeysModel.bucketName) 
                                            .WithLocation(objectStorageKeysModel.location);

                await minioClient.MakeBucketAsync(mkBktArgs);

                string policyJson = JsonConvert.SerializeObject(minioPolicy);
                policyJson = policyJson.Replace("{bucketName}", objectStorageKeysModel.bucketName);

                await minioClient.SetPolicyAsync(objectStorageKeysModel.bucketName, policyJson);

                var args = new GetPolicyArgs()
                                  .WithBucket(objectStorageKeysModel.bucketName);
                try
                {
                    string existpolicyJson = await minioClient.GetPolicyAsync(args);
                }
                catch (Exception)
                {

                }
            }
        }

        public async Task<ResponseModel<string>> UploadObject(ContentObjectModel model)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            string objectName = Path.GetFileName(model.object_name);
            string contentType = Utility.GetcontentType(objectName);
            objectName = model.bucket_folder + "/" + objectName;
            try
            {
                //var GetObjectresponse = await GetObject(model);
                PutObjectArgs putObjectArgs = new PutObjectArgs()
                                                             .WithBucket(objectStorageKeysModel.bucketName)
                                                             .WithObject(objectName)
                                                             .WithStreamData(model.file)
                                                             .WithContentType(model.object_type);

                minioClient.PutObjectAsync(objectStorageKeysModel.bucketName, objectName, model.file, model.object_size, model.object_type);

                model.created_at = DateTime.Now;
                model.file = null;

                var object_id = AddContentObject(model);

                //minio.PutObjectAsync(putObjectArgs);
                response.Value = object_id;
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        public async Task<ResponseModel<string>> DeleteObject(ContentObjectModel model)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            RemoveObjectArgs args = new RemoveObjectArgs()
                                      .WithBucket(objectStorageKeysModel.bucketName)
                                      .WithObject(model.object_name);
            

            await minioClient.RemoveObjectAsync(args);
            return response;
        }

        public string AddContentObject(ContentObjectModel model)
        {
            string contentobjectModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "AddContentObject";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            contentobjectModel = response.Value;
            return contentobjectModel;

        }

        public ContentObjectModel GetContentObject(ContentObjectModel model)
        {
            ContentObjectModel contentobjectModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetContentObject";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<ContentObjectModel> response = JsonConvert.DeserializeObject<ResponseModel<ContentObjectModel>>(jsonResult);
            contentobjectModel = response.Value;
            return contentobjectModel;

        }

        public string DeleteContentObject(ContentObjectModel model)
        {
            string contentobjectModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "DeleteContentObject";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            contentobjectModel = response.Value;
            return contentobjectModel;

        }
        public List<ContentObjectModel> GetAllContentObject(long UserId)
        {
            throw new NotImplementedException();
        }

        public ResponseModel<string> SaveContentObject(ContentObjectModel model)
        {
            throw new NotImplementedException();
        }

        public string AddContentObjectAttachment(ContentObjectAttachmentModel model)
        {
            string contentobjectattachmentModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "AddContentObjectAttachment";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            contentobjectattachmentModel = response.Value;
            return contentobjectattachmentModel;

        }

        public ContentObjectAttachmentModel GetContentObjectAttachment(ContentObjectAttachmentModel model)
        {
            ContentObjectAttachmentModel contentobjectattachmentModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetContentObjectAttachment";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<ContentObjectAttachmentModel> response = JsonConvert.DeserializeObject<ResponseModel<ContentObjectAttachmentModel>>(jsonResult);
            contentobjectattachmentModel = response.Value;
            return contentobjectattachmentModel;

        }

        public string DeleteContentObjectAttachment(ContentObjectAttachmentModel model)
        {
            string contentobjectattachmentModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "DeleteContentObjectAttachment";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            contentobjectattachmentModel = response.Value;
            return contentobjectattachmentModel;

        }

        public List<ContentObjectAttachmentModel> GetAllContentObjectAttachment(long UserId)
        {
            throw new NotImplementedException();
        }

        public ResponseModel<string> SaveContentObjectAttachmen(ContentObjectAttachmentModel model)
        {
            throw new NotImplementedException();
        }

        public List<ContentObjectModel> GetAllContentObjectAttachments(long content_id, string content_type)
        {
            List<ContentObjectModel> contentObjectModel = new List<ContentObjectModel>();
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetAllContentObjectAttachments/" + content_id + "/" + content_type;
            client.ServiceMethod = Method.GET;
            string jsonResult = client.GETAsync();
            ResponseModel<List<ContentObjectModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<ContentObjectModel>>>(jsonResult);
            contentObjectModel = response.Value;
            return contentObjectModel;
        }
    }
}
