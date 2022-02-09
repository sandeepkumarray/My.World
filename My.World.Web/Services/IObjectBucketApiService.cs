using My.World.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
    public interface IObjectBucketApiService : IObjectStorage, IUsercontentbucketApiService, IObjectstoragekeysApiService, IContentobjectApiService, IContentobjectattachmentApiService
    {
        public ObjectStorageKeysModel objectStorageKeysModel { get; set; }
        List<ContentObjectModel> GetAllContentObjectAttachments(long content_id, string content_type);
    }
}
