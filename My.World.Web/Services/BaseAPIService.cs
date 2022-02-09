using My.World.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
    public class BaseAPIService
    {
        public string MyWorldApiUrl { get; set; }
        public MinioPolicy minioPolicy { get; set; }

        public BaseAPIService()
        {
            MyWorldApiUrl = ConfigurationManager.AppSettings.Get("MyWorldApiUrl");
            SetUpMinioPolicy();
        }

        public string GetContentTypeJsonTemplate(string content_type)
        {
            string result = string.Empty;
            string templateFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "json_ui");
            string jsonContent = File.ReadAllText(templateFolder + content_type + ".json");

            return result;
        }

        public List<UniversesModel> GetAllUniverses(long userId)
        {
            List<UniversesModel> universesModel = new List<UniversesModel>();
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetAllUniversesForUserID/" + userId;

            client.ServiceMethod = Method.GET;
            string jsonResult = client.GETAsync();
            ResponseModel<List<UniversesModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<UniversesModel>>>(jsonResult);
            universesModel = response.Value;
            return universesModel;
        }

        public void SetUpMinioPolicy()
        {
            minioPolicy = new MinioPolicy();
            minioPolicy.Version = "2012-10-17";
            minioPolicy.Statement = new List<Statement>();

            Statement ResourceOne = new Statement();
            ResourceOne.Effect = "Allow";
            ResourceOne.Principal = new Principal();
            ResourceOne.Principal.AWS = new List<string>();
            ResourceOne.Principal.AWS.Add("*");
            ResourceOne.Resource = new List<string>();
            ResourceOne.Resource.Add("arn:aws:s3:::{bucketName}");
            ResourceOne.Action = new List<string>();
            ResourceOne.Action.Add("s3:ListBucket");

            Statement ResourceTwo = new Statement();
            ResourceTwo.Effect = "Allow";
            ResourceTwo.Principal = new Principal();
            ResourceTwo.Principal.AWS = new List<string>();
            ResourceTwo.Principal.AWS.Add("*");
            ResourceTwo.Resource = new List<string>();
            ResourceTwo.Resource.Add("arn:aws:s3:::{bucketName}/*");
            ResourceTwo.Action = new List<string>();
            ResourceTwo.Action.Add("s3:ListMultipartUploadParts");
            ResourceTwo.Action.Add("s3:PutObject");
            ResourceTwo.Action.Add("s3:AbortMultipartUpload");
            ResourceTwo.Action.Add("s3:DeleteObject");
            ResourceTwo.Action.Add("s3:GetObject");

            minioPolicy.Statement.Add(ResourceOne);
            minioPolicy.Statement.Add(ResourceTwo);
        }
    }
}
