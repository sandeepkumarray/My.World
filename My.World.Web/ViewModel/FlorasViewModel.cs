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
	public class FlorasViewModel
	{
		[JsonProperty("florasmodel")]
		public FlorasModel florasModel { get; set; }

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
		public List<ContentObjectModel> ContentObjectModelList { get; set; }

		[JsonProperty("remainingcontentsize")]
		public string RemainingContentSize { get; set; }


		public FlorasViewModel()
		{
		}

		public FlorasViewModel(IObjectBucketApiService _iObjectBucketApiService)
		{
			iObjectBucketApiService = _iObjectBucketApiService;

		}

	}
}
