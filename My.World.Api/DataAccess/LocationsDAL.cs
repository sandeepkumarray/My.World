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
	public class LocationsDAL : BaseDAL
	{

		public LocationsDAL()
		{
		}

		public  LocationsDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteLocationsData(LocationsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Locations` WHERE id = @id";
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

		public LocationsModel GetLocationsData(LocationsModel Data)
		{
			LocationsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Locations` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new LocationsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    LocationsModel locations = new LocationsModel();
					locations.Area = dr["Area"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Area"]);
					locations.Capital_cities = dr["Capital_cities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Capital_cities"]);
					locations.Climate = dr["Climate"] == DBNull.Value ? default(String) : Convert.ToString(dr["Climate"]);
					locations.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					locations.Crops = dr["Crops"] == DBNull.Value ? default(String) : Convert.ToString(dr["Crops"]);
					locations.Currency = dr["Currency"] == DBNull.Value ? default(String) : Convert.ToString(dr["Currency"]);
					locations.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					locations.Established_Year = dr["Established_Year"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Established_Year"]);
					locations.Founding_Story = dr["Founding_Story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Founding_Story"]);
					locations.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					locations.Landmarks = dr["Landmarks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Landmarks"]);
					locations.Language = dr["Language"] == DBNull.Value ? default(String) : Convert.ToString(dr["Language"]);
					locations.Largest_cities = dr["Largest_cities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Largest_cities"]);
					locations.Laws = dr["Laws"] == DBNull.Value ? default(String) : Convert.ToString(dr["Laws"]);
					locations.Leaders = dr["Leaders"] == DBNull.Value ? default(String) : Convert.ToString(dr["Leaders"]);
					locations.Located_at = dr["Located_at"] == DBNull.Value ? default(String) : Convert.ToString(dr["Located_at"]);
					locations.Motto = dr["Motto"] == DBNull.Value ? default(String) : Convert.ToString(dr["Motto"]);
					locations.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					locations.Notable_cities = dr["Notable_cities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_cities"]);
					locations.Notable_Wars = dr["Notable_Wars"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_Wars"]);
					locations.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					locations.Population = dr["Population"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Population"]);
					locations.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					locations.Spoken_Languages = dr["Spoken_Languages"] == DBNull.Value ? default(String) : Convert.ToString(dr["Spoken_Languages"]);
					locations.Sports = dr["Sports"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sports"]);
					locations.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					locations.Type = dr["Type"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type"]);
					locations.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					locations.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					locations.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);

					_return_value = locations;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<LocationsModel> GetAllLocationsForUserID(long userId)
		{
			List<LocationsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Locations` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<LocationsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						LocationsModel locations = new LocationsModel();
					locations.Area = dr["Area"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Area"]);
					locations.Capital_cities = dr["Capital_cities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Capital_cities"]);
					locations.Climate = dr["Climate"] == DBNull.Value ? default(String) : Convert.ToString(dr["Climate"]);
					locations.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					locations.Crops = dr["Crops"] == DBNull.Value ? default(String) : Convert.ToString(dr["Crops"]);
					locations.Currency = dr["Currency"] == DBNull.Value ? default(String) : Convert.ToString(dr["Currency"]);
					locations.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					locations.Established_Year = dr["Established_Year"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Established_Year"]);
					locations.Founding_Story = dr["Founding_Story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Founding_Story"]);
					locations.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					locations.Landmarks = dr["Landmarks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Landmarks"]);
					locations.Language = dr["Language"] == DBNull.Value ? default(String) : Convert.ToString(dr["Language"]);
					locations.Largest_cities = dr["Largest_cities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Largest_cities"]);
					locations.Laws = dr["Laws"] == DBNull.Value ? default(String) : Convert.ToString(dr["Laws"]);
					locations.Leaders = dr["Leaders"] == DBNull.Value ? default(String) : Convert.ToString(dr["Leaders"]);
					locations.Located_at = dr["Located_at"] == DBNull.Value ? default(String) : Convert.ToString(dr["Located_at"]);
					locations.Motto = dr["Motto"] == DBNull.Value ? default(String) : Convert.ToString(dr["Motto"]);
					locations.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					locations.Notable_cities = dr["Notable_cities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_cities"]);
					locations.Notable_Wars = dr["Notable_Wars"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_Wars"]);
					locations.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					locations.Population = dr["Population"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Population"]);
					locations.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					locations.Spoken_Languages = dr["Spoken_Languages"] == DBNull.Value ? default(String) : Convert.ToString(dr["Spoken_Languages"]);
					locations.Sports = dr["Sports"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sports"]);
					locations.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					locations.Type = dr["Type"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type"]);
					locations.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					locations.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					locations.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(locations.id, "locations");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    locations.object_id = first.object_id;
						    locations.object_name = first.object_name;
						}

						_return_value.Add(locations);
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

		public string AddLocationsData(LocationsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Locations`(`Area`,`Capital_cities`,`Climate`,`created_at`,`Crops`,`Currency`,`Description`,`Established_Year`,`Founding_Story`,`Landmarks`,`Language`,`Largest_cities`,`Laws`,`Leaders`,`Located_at`,`Motto`,`Name`,`Notable_cities`,`Notable_Wars`,`Notes`,`Population`,`Private_Notes`,`Spoken_Languages`,`Sports`,`Tags`,`Type`,`Universe`,`updated_at`,`user_id`) VALUES(@Area,@Capital_cities,@Climate,@created_at,@Crops,@Currency,@Description,@Established_Year,@Founding_Story,@Landmarks,@Language,@Largest_cities,@Laws,@Leaders,@Located_at,@Motto,@Name,@Notable_cities,@Notable_Wars,@Notes,@Population,@Private_Notes,@Spoken_Languages,@Sports,@Tags,@Type,@Universe,@updated_at,@user_id)";
				dbContext.AddInParameter(dbContext.cmd, "@Area", Data.Area);
				dbContext.AddInParameter(dbContext.cmd, "@Capital_cities", Data.Capital_cities);
				dbContext.AddInParameter(dbContext.cmd, "@Climate", Data.Climate);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Crops", Data.Crops);
				dbContext.AddInParameter(dbContext.cmd, "@Currency", Data.Currency);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Established_Year", Data.Established_Year);
				dbContext.AddInParameter(dbContext.cmd, "@Founding_Story", Data.Founding_Story);
				dbContext.AddInParameter(dbContext.cmd, "@Landmarks", Data.Landmarks);
				dbContext.AddInParameter(dbContext.cmd, "@Language", Data.Language);
				dbContext.AddInParameter(dbContext.cmd, "@Largest_cities", Data.Largest_cities);
				dbContext.AddInParameter(dbContext.cmd, "@Laws", Data.Laws);
				dbContext.AddInParameter(dbContext.cmd, "@Leaders", Data.Leaders);
				dbContext.AddInParameter(dbContext.cmd, "@Located_at", Data.Located_at);
				dbContext.AddInParameter(dbContext.cmd, "@Motto", Data.Motto);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_cities", Data.Notable_cities);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_Wars", Data.Notable_Wars);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Population", Data.Population);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Spoken_Languages", Data.Spoken_Languages);
				dbContext.AddInParameter(dbContext.cmd, "@Sports", Data.Sports);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Type", Data.Type);
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

	}
}
