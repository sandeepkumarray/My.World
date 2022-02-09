using My.World.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.ViewModel
{
    public class UniverseViewModel
    {		
		[JsonProperty("UniversesModel")]
		public List<UniversesModel> UniversesList { get; set; }
	}
}
