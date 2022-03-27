using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.World.Api.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace My.World.Api.DataAccess
{
	public class OrganizationsDAL : BaseDAL
	{

		public OrganizationsDAL()
		{
		}

		public  OrganizationsDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteOrganizationsData(OrganizationsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Organizations` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				_return_value = Convert.ToString(dbContext.cmd.ExecuteNonQuery());
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public OrganizationsModel GetOrganizationsData(OrganizationsModel Data)
		{
			OrganizationsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Organizations` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new OrganizationsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    OrganizationsModel organizations = new OrganizationsModel();
					organizations.Address = dr["Address"] == DBNull.Value ? default(String) : Convert.ToString(dr["Address"]);
					organizations.Alternate_names = dr["Alternate_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Alternate_names"]);
					organizations.Closure_year = dr["Closure_year"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Closure_year"]);
					organizations.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					organizations.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					organizations.Formation_year = dr["Formation_year"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Formation_year"]);
					organizations.Headquarters = dr["Headquarters"] == DBNull.Value ? default(String) : Convert.ToString(dr["Headquarters"]);
					organizations.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					organizations.Locations = dr["Locations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Locations"]);
					organizations.Members = dr["Members"] == DBNull.Value ? default(String) : Convert.ToString(dr["Members"]);
					organizations.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					organizations.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					organizations.Offices = dr["Offices"] == DBNull.Value ? default(String) : Convert.ToString(dr["Offices"]);
					organizations.Organization_structure = dr["Organization_structure"] == DBNull.Value ? default(String) : Convert.ToString(dr["Organization_structure"]);
					organizations.Owner = dr["Owner"] == DBNull.Value ? default(String) : Convert.ToString(dr["Owner"]);
					organizations.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					organizations.Purpose = dr["Purpose"] == DBNull.Value ? default(String) : Convert.ToString(dr["Purpose"]);
					organizations.Rival_organizations = dr["Rival_organizations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Rival_organizations"]);
					organizations.Services = dr["Services"] == DBNull.Value ? default(String) : Convert.ToString(dr["Services"]);
					organizations.Sister_organizations = dr["Sister_organizations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sister_organizations"]);
					organizations.Sub_organizations = dr["Sub_organizations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sub_organizations"]);
					organizations.Super_organizations = dr["Super_organizations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Super_organizations"]);
					organizations.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					organizations.Type_of_organization = dr["Type_of_organization"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_organization"]);
					organizations.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					organizations.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					organizations.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);

					_return_value = organizations;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<OrganizationsModel> GetAllOrganizationsForUserID(long userId)
		{
			List<OrganizationsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Organizations` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<OrganizationsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						OrganizationsModel organizations = new OrganizationsModel();
					organizations.Address = dr["Address"] == DBNull.Value ? default(String) : Convert.ToString(dr["Address"]);
					organizations.Alternate_names = dr["Alternate_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Alternate_names"]);
					organizations.Closure_year = dr["Closure_year"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Closure_year"]);
					organizations.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					organizations.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					organizations.Formation_year = dr["Formation_year"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Formation_year"]);
					organizations.Headquarters = dr["Headquarters"] == DBNull.Value ? default(String) : Convert.ToString(dr["Headquarters"]);
					organizations.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					organizations.Locations = dr["Locations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Locations"]);
					organizations.Members = dr["Members"] == DBNull.Value ? default(String) : Convert.ToString(dr["Members"]);
					organizations.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					organizations.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					organizations.Offices = dr["Offices"] == DBNull.Value ? default(String) : Convert.ToString(dr["Offices"]);
					organizations.Organization_structure = dr["Organization_structure"] == DBNull.Value ? default(String) : Convert.ToString(dr["Organization_structure"]);
					organizations.Owner = dr["Owner"] == DBNull.Value ? default(String) : Convert.ToString(dr["Owner"]);
					organizations.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					organizations.Purpose = dr["Purpose"] == DBNull.Value ? default(String) : Convert.ToString(dr["Purpose"]);
					organizations.Rival_organizations = dr["Rival_organizations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Rival_organizations"]);
					organizations.Services = dr["Services"] == DBNull.Value ? default(String) : Convert.ToString(dr["Services"]);
					organizations.Sister_organizations = dr["Sister_organizations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sister_organizations"]);
					organizations.Sub_organizations = dr["Sub_organizations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sub_organizations"]);
					organizations.Super_organizations = dr["Super_organizations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Super_organizations"]);
					organizations.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					organizations.Type_of_organization = dr["Type_of_organization"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_organization"]);
					organizations.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					organizations.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					organizations.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(organizations.id, "organizations");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    organizations.object_id = first.object_id;
						    organizations.object_name = first.object_name;
						}

						_return_value.Add(organizations);
					}
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public string AddOrganizationsData(OrganizationsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Organizations`(`Address`,`Alternate_names`,`Closure_year`,`created_at`,`Description`,`Formation_year`,`Headquarters`,`Locations`,`Members`,`Name`,`Notes`,`Offices`,`Organization_structure`,`Owner`,`Private_Notes`,`Purpose`,`Rival_organizations`,`Services`,`Sister_organizations`,`Sub_organizations`,`Super_organizations`,`Tags`,`Type_of_organization`,`Universe`,`updated_at`,`user_id`) VALUES(@Address,@Alternate_names,@Closure_year,@created_at,@Description,@Formation_year,@Headquarters,@Locations,@Members,@Name,@Notes,@Offices,@Organization_structure,@Owner,@Private_Notes,@Purpose,@Rival_organizations,@Services,@Sister_organizations,@Sub_organizations,@Super_organizations,@Tags,@Type_of_organization,@Universe,@updated_at,@user_id)";
				dbContext.AddInParameter(dbContext.cmd, "@Address", Data.Address);
				dbContext.AddInParameter(dbContext.cmd, "@Alternate_names", Data.Alternate_names);
				dbContext.AddInParameter(dbContext.cmd, "@Closure_year", Data.Closure_year);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Formation_year", Data.Formation_year);
				dbContext.AddInParameter(dbContext.cmd, "@Headquarters", Data.Headquarters);
				dbContext.AddInParameter(dbContext.cmd, "@Locations", Data.Locations);
				dbContext.AddInParameter(dbContext.cmd, "@Members", Data.Members);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Offices", Data.Offices);
				dbContext.AddInParameter(dbContext.cmd, "@Organization_structure", Data.Organization_structure);
				dbContext.AddInParameter(dbContext.cmd, "@Owner", Data.Owner);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Purpose", Data.Purpose);
				dbContext.AddInParameter(dbContext.cmd, "@Rival_organizations", Data.Rival_organizations);
				dbContext.AddInParameter(dbContext.cmd, "@Services", Data.Services);
				dbContext.AddInParameter(dbContext.cmd, "@Sister_organizations", Data.Sister_organizations);
				dbContext.AddInParameter(dbContext.cmd, "@Sub_organizations", Data.Sub_organizations);
				dbContext.AddInParameter(dbContext.cmd, "@Super_organizations", Data.Super_organizations);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Type_of_organization", Data.Type_of_organization);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.cmd.ExecuteNonQuery();
				_return_value = Convert.ToString(dbContext.cmd.LastInsertedId);
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public string UpdateOrganizationsData(OrganizationsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "UPDATE organizations SET Address = @Address,Alternate_names = @Alternate_names,Closure_year = @Closure_year,created_at = @created_at,Description = @Description,Formation_year = @Formation_year,Headquarters = @Headquarters,Locations = @Locations,Members = @Members,Name = @Name,Notes = @Notes,Offices = @Offices,Organization_structure = @Organization_structure,Owner = @Owner,Private_Notes = @Private_Notes,Purpose = @Purpose,Rival_organizations = @Rival_organizations,Services = @Services,Sister_organizations = @Sister_organizations,Sub_organizations = @Sub_organizations,Super_organizations = @Super_organizations,Tags = @Tags,Type_of_organization = @Type_of_organization,Universe = @Universe,updated_at = @updated_at,user_id = @user_id WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@Address", Data.Address);
				dbContext.AddInParameter(dbContext.cmd, "@Alternate_names", Data.Alternate_names);
				dbContext.AddInParameter(dbContext.cmd, "@Closure_year", Data.Closure_year);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Formation_year", Data.Formation_year);
				dbContext.AddInParameter(dbContext.cmd, "@Headquarters", Data.Headquarters);
				dbContext.AddInParameter(dbContext.cmd, "@Locations", Data.Locations);
				dbContext.AddInParameter(dbContext.cmd, "@Members", Data.Members);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Offices", Data.Offices);
				dbContext.AddInParameter(dbContext.cmd, "@Organization_structure", Data.Organization_structure);
				dbContext.AddInParameter(dbContext.cmd, "@Owner", Data.Owner);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Purpose", Data.Purpose);
				dbContext.AddInParameter(dbContext.cmd, "@Rival_organizations", Data.Rival_organizations);
				dbContext.AddInParameter(dbContext.cmd, "@Services", Data.Services);
				dbContext.AddInParameter(dbContext.cmd, "@Sister_organizations", Data.Sister_organizations);
				dbContext.AddInParameter(dbContext.cmd, "@Sub_organizations", Data.Sub_organizations);
				dbContext.AddInParameter(dbContext.cmd, "@Super_organizations", Data.Super_organizations);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Type_of_organization", Data.Type_of_organization);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				_return_value = Convert.ToString(dbContext.cmd.ExecuteNonQuery());
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

	}
}
