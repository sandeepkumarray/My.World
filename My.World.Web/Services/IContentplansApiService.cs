using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IContentPlansApiService
	{

		ContentPlansModel GetContentPlans(ContentPlansModel model);

		string DeleteContentPlans(ContentPlansModel model);

		List<ContentPlansModel> GetAllContentPlans();

		ResponseModel<string> SaveContentPlan(ContentPlansModel model);

	}
}
