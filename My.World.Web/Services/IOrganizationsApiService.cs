using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IOrganizationsApiService
	{

		OrganizationsModel GetOrganizations(OrganizationsModel model);

		string DeleteOrganizations(OrganizationsModel model);

		List<OrganizationsModel> GetAllOrganizations(long UserId);

		ResponseModel<string> SaveOrganization(OrganizationsModel model);

	}
}
