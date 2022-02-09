using My.World.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
    public interface IObjectStorage
    {
        Task<ResponseModel<string>> UploadObject(ContentObjectModel model);
        Task<ResponseModel<string>> GetObject(ContentObjectModel model);
        Task<ResponseModel<string>> DeleteObject(ContentObjectModel model);
        void SetObjectStorageSecrets(long accountID);

    }
}
