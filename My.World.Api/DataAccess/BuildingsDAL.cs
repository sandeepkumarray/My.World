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
	public class BuildingsDAL : BaseDAL
	{

		public BuildingsDAL()
		{
		}

		public  BuildingsDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteBuildingsData(BuildingsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Buildings` WHERE id = @id";
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

		public BuildingsModel GetBuildingsData(BuildingsModel Data)
		{
			BuildingsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Buildings` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new BuildingsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    BuildingsModel buildings = new BuildingsModel();
					buildings.Address = dr["Address"] == DBNull.Value ? default(String) : Convert.ToString(dr["Address"]);
					buildings.Affiliation = dr["Affiliation"] == DBNull.Value ? default(String) : Convert.ToString(dr["Affiliation"]);
					buildings.Alternate_names = dr["Alternate_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Alternate_names"]);
					buildings.Architect = dr["Architect"] == DBNull.Value ? default(String) : Convert.ToString(dr["Architect"]);
					buildings.Architectural_style = dr["Architectural_style"] == DBNull.Value ? default(String) : Convert.ToString(dr["Architectural_style"]);
					buildings.Capacity = dr["Capacity"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Capacity"]);
					buildings.Constructed_year = dr["Constructed_year"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Constructed_year"]);
					buildings.Construction_cost = dr["Construction_cost"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Construction_cost"]);
					buildings.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					buildings.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					buildings.Developer = dr["Developer"] == DBNull.Value ? default(String) : Convert.ToString(dr["Developer"]);
					buildings.Dimensions = dr["Dimensions"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Dimensions"]);
					buildings.Facade = dr["Facade"] == DBNull.Value ? default(String) : Convert.ToString(dr["Facade"]);
					buildings.Floor_count = dr["Floor_count"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Floor_count"]);
					buildings.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					buildings.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					buildings.Notable_events = dr["Notable_events"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_events"]);
					buildings.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					buildings.Owner = dr["Owner"] == DBNull.Value ? default(String) : Convert.ToString(dr["Owner"]);
					buildings.Permits = dr["Permits"] == DBNull.Value ? default(String) : Convert.ToString(dr["Permits"]);
					buildings.Price = dr["Price"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Price"]);
					buildings.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					buildings.Purpose = dr["Purpose"] == DBNull.Value ? default(String) : Convert.ToString(dr["Purpose"]);
					buildings.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					buildings.Tenants = dr["Tenants"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tenants"]);
					buildings.Type_of_building = dr["Type_of_building"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_building"]);
					buildings.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					buildings.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					buildings.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);

					_return_value = buildings;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<BuildingsModel> GetAllBuildingsForUserID(long userId)
		{
			List<BuildingsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Buildings` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<BuildingsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						BuildingsModel buildings = new BuildingsModel();
					buildings.Address = dr["Address"] == DBNull.Value ? default(String) : Convert.ToString(dr["Address"]);
					buildings.Affiliation = dr["Affiliation"] == DBNull.Value ? default(String) : Convert.ToString(dr["Affiliation"]);
					buildings.Alternate_names = dr["Alternate_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Alternate_names"]);
					buildings.Architect = dr["Architect"] == DBNull.Value ? default(String) : Convert.ToString(dr["Architect"]);
					buildings.Architectural_style = dr["Architectural_style"] == DBNull.Value ? default(String) : Convert.ToString(dr["Architectural_style"]);
					buildings.Capacity = dr["Capacity"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Capacity"]);
					buildings.Constructed_year = dr["Constructed_year"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Constructed_year"]);
					buildings.Construction_cost = dr["Construction_cost"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Construction_cost"]);
					buildings.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					buildings.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					buildings.Developer = dr["Developer"] == DBNull.Value ? default(String) : Convert.ToString(dr["Developer"]);
					buildings.Dimensions = dr["Dimensions"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Dimensions"]);
					buildings.Facade = dr["Facade"] == DBNull.Value ? default(String) : Convert.ToString(dr["Facade"]);
					buildings.Floor_count = dr["Floor_count"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Floor_count"]);
					buildings.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					buildings.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					buildings.Notable_events = dr["Notable_events"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_events"]);
					buildings.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					buildings.Owner = dr["Owner"] == DBNull.Value ? default(String) : Convert.ToString(dr["Owner"]);
					buildings.Permits = dr["Permits"] == DBNull.Value ? default(String) : Convert.ToString(dr["Permits"]);
					buildings.Price = dr["Price"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Price"]);
					buildings.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					buildings.Purpose = dr["Purpose"] == DBNull.Value ? default(String) : Convert.ToString(dr["Purpose"]);
					buildings.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					buildings.Tenants = dr["Tenants"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tenants"]);
					buildings.Type_of_building = dr["Type_of_building"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_building"]);
					buildings.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					buildings.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					buildings.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(buildings.id, "buildings");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    buildings.object_id = first.object_id;
						    buildings.object_name = first.object_name;
						}

						_return_value.Add(buildings);
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

		public string AddBuildingsData(BuildingsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Buildings`(`Address`,`Affiliation`,`Alternate_names`,`Architect`,`Architectural_style`,`Capacity`,`Constructed_year`,`Construction_cost`,`created_at`,`Description`,`Developer`,`Dimensions`,`Facade`,`Floor_count`,`Name`,`Notable_events`,`Notes`,`Owner`,`Permits`,`Price`,`Private_Notes`,`Purpose`,`Tags`,`Tenants`,`Type_of_building`,`Universe`,`updated_at`,`user_id`) VALUES(@Address,@Affiliation,@Alternate_names,@Architect,@Architectural_style,@Capacity,@Constructed_year,@Construction_cost,@created_at,@Description,@Developer,@Dimensions,@Facade,@Floor_count,@Name,@Notable_events,@Notes,@Owner,@Permits,@Price,@Private_Notes,@Purpose,@Tags,@Tenants,@Type_of_building,@Universe,@updated_at,@user_id)";
				dbContext.AddInParameter(dbContext.cmd, "@Address", Data.Address);
				dbContext.AddInParameter(dbContext.cmd, "@Affiliation", Data.Affiliation);
				dbContext.AddInParameter(dbContext.cmd, "@Alternate_names", Data.Alternate_names);
				dbContext.AddInParameter(dbContext.cmd, "@Architect", Data.Architect);
				dbContext.AddInParameter(dbContext.cmd, "@Architectural_style", Data.Architectural_style);
				dbContext.AddInParameter(dbContext.cmd, "@Capacity", Data.Capacity);
				dbContext.AddInParameter(dbContext.cmd, "@Constructed_year", Data.Constructed_year);
				dbContext.AddInParameter(dbContext.cmd, "@Construction_cost", Data.Construction_cost);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Developer", Data.Developer);
				dbContext.AddInParameter(dbContext.cmd, "@Dimensions", Data.Dimensions);
				dbContext.AddInParameter(dbContext.cmd, "@Facade", Data.Facade);
				dbContext.AddInParameter(dbContext.cmd, "@Floor_count", Data.Floor_count);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_events", Data.Notable_events);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Owner", Data.Owner);
				dbContext.AddInParameter(dbContext.cmd, "@Permits", Data.Permits);
				dbContext.AddInParameter(dbContext.cmd, "@Price", Data.Price);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Purpose", Data.Purpose);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Tenants", Data.Tenants);
				dbContext.AddInParameter(dbContext.cmd, "@Type_of_building", Data.Type_of_building);
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

		public string UpdateBuildingsData(BuildingsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "UPDATE buildings SET Address = @Address,Affiliation = @Affiliation,Alternate_names = @Alternate_names,Architect = @Architect,Architectural_style = @Architectural_style,Capacity = @Capacity,Constructed_year = @Constructed_year,Construction_cost = @Construction_cost,created_at = @created_at,Description = @Description,Developer = @Developer,Dimensions = @Dimensions,Facade = @Facade,Floor_count = @Floor_count,Name = @Name,Notable_events = @Notable_events,Notes = @Notes,Owner = @Owner,Permits = @Permits,Price = @Price,Private_Notes = @Private_Notes,Purpose = @Purpose,Tags = @Tags,Tenants = @Tenants,Type_of_building = @Type_of_building,Universe = @Universe,updated_at = @updated_at,user_id = @user_id WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@Address", Data.Address);
				dbContext.AddInParameter(dbContext.cmd, "@Affiliation", Data.Affiliation);
				dbContext.AddInParameter(dbContext.cmd, "@Alternate_names", Data.Alternate_names);
				dbContext.AddInParameter(dbContext.cmd, "@Architect", Data.Architect);
				dbContext.AddInParameter(dbContext.cmd, "@Architectural_style", Data.Architectural_style);
				dbContext.AddInParameter(dbContext.cmd, "@Capacity", Data.Capacity);
				dbContext.AddInParameter(dbContext.cmd, "@Constructed_year", Data.Constructed_year);
				dbContext.AddInParameter(dbContext.cmd, "@Construction_cost", Data.Construction_cost);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Developer", Data.Developer);
				dbContext.AddInParameter(dbContext.cmd, "@Dimensions", Data.Dimensions);
				dbContext.AddInParameter(dbContext.cmd, "@Facade", Data.Facade);
				dbContext.AddInParameter(dbContext.cmd, "@Floor_count", Data.Floor_count);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_events", Data.Notable_events);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Owner", Data.Owner);
				dbContext.AddInParameter(dbContext.cmd, "@Permits", Data.Permits);
				dbContext.AddInParameter(dbContext.cmd, "@Price", Data.Price);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Purpose", Data.Purpose);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Tenants", Data.Tenants);
				dbContext.AddInParameter(dbContext.cmd, "@Type_of_building", Data.Type_of_building);
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
