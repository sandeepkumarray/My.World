using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IOrganizationsService
	{

		ResponseModel<string> AddOrganizationsData(OrganizationsModel Data);

		ResponseModel<OrganizationsModel> GetOrganizationsData(OrganizationsModel Data);

		ResponseModel<string> DeleteOrganizationsData(OrganizationsModel Data);

		ResponseModel<List<OrganizationsModel >> GetAllOrganizationsForUserID(long userId);

		ResponseModel<string> SaveOrganization(OrganizationsModel Data);

		ResponseModel<string> UpdateOrganizationsData(OrganizationsModel Data);

	}
}
