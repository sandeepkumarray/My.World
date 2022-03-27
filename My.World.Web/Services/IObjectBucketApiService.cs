using My.World.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
    public interface IObjectBucketApiService : IObjectStorage, IUserContentBucketApiService, IObjectStorageKeysApiService, IContentObjectApiService, IContentObjectAttachmentApiService
    {
        public ObjectStorageKeysModel objectStorageKeysModel { get; set; }
        List<ContentObjectModel> GetAllContentObjectAttachments(long content_id, string content_type);
    }
}
