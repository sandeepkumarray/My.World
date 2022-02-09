using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;
using My.World.Web.Services;

namespace My.World.Web.ViewModel
{
	public class ContinentsViewModel
	{
		[JsonProperty("continentsmodel")]
		public ContinentsModel continentsModel { get; set; }

		[JsonProperty("iobjectbucketapiservice")]
		private IObjectBucketApiService iObjectBucketApiService { get; set; }

		[JsonProperty("universeslist")]
		public List<UniversesModel> UniversesList { get; set; }

		[JsonProperty("contenttemplate")]
		public Content ContentTemplate { get; set; }

		[JsonProperty("headerbackgroundcolor")]
		public string headerBackgroundColor { get; set; }

		[JsonProperty("headerforegroundcolor")]
		public string headerForegroundColor { get; set; }

		[JsonProperty("_contentobjectmodellist")]
		private List<ContentObjectModel> _contentObjectModelList { get; set; }

		[JsonProperty("contentobjectmodellist")]
		public List<ContentObjectModel> ContentObjectModelList		
		{
		    get
			{
		        return _contentObjectModelList;
		    }
		    set
			{
		        _contentObjectModelList = value;
		        if (_contentObjectModelList != null)
		        {
		            foreach (var contentObject in _contentObjectModelList)
		            {
		                var publicUrl = "http://" + iObjectBucketApiService.objectStorageKeysModel.endpoint
		                    + '/' + iObjectBucketApiService.objectStorageKeysModel.bucketName + '/' + contentObject.object_name;
		
		                contentObject.file_url = HttpUtility.UrlPathEncode(publicUrl);
		            }
		        }
		    }
		}



		public ContinentsViewModel()
		{
		}

		public ContinentsViewModel(IObjectBucketApiService _iObjectBucketApiService)
		{
			iObjectBucketApiService = _iObjectBucketApiService;

		}

	}
}
