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
	public class CountriesDAL : BaseDAL
	{

		public CountriesDAL()
		{
		}

		public  CountriesDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteCountriesData(CountriesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Countries` WHERE id = @id";
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

		public CountriesModel GetCountriesData(CountriesModel Data)
		{
			CountriesModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Countries` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new CountriesModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    CountriesModel countries = new CountriesModel();
					countries.Architecture = dr["Architecture"] == DBNull.Value ? default(String) : Convert.ToString(dr["Architecture"]);
					countries.Area = dr["Area"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Area"]);
					countries.Bordering_countries = dr["Bordering_countries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Bordering_countries"]);
					countries.Climate = dr["Climate"] == DBNull.Value ? default(String) : Convert.ToString(dr["Climate"]);
					countries.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					countries.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					countries.Crops = dr["Crops"] == DBNull.Value ? default(String) : Convert.ToString(dr["Crops"]);
					countries.Currency = dr["Currency"] == DBNull.Value ? default(String) : Convert.ToString(dr["Currency"]);
					countries.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					countries.Education = dr["Education"] == DBNull.Value ? default(String) : Convert.ToString(dr["Education"]);
					countries.Established_year = dr["Established_year"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Established_year"]);
					countries.Flora = dr["Flora"] == DBNull.Value ? default(String) : Convert.ToString(dr["Flora"]);
					countries.Founding_story = dr["Founding_story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Founding_story"]);
					countries.Governments = dr["Governments"] == DBNull.Value ? default(String) : Convert.ToString(dr["Governments"]);
					countries.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					countries.Landmarks = dr["Landmarks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Landmarks"]);
					countries.Languages = dr["Languages"] == DBNull.Value ? default(String) : Convert.ToString(dr["Languages"]);
					countries.Laws = dr["Laws"] == DBNull.Value ? default(String) : Convert.ToString(dr["Laws"]);
					countries.Locations = dr["Locations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Locations"]);
					countries.Music = dr["Music"] == DBNull.Value ? default(String) : Convert.ToString(dr["Music"]);
					countries.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					countries.Notable_wars = dr["Notable_wars"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_wars"]);
					countries.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					countries.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					countries.Pop_culture = dr["Pop_culture"] == DBNull.Value ? default(String) : Convert.ToString(dr["Pop_culture"]);
					countries.Population = dr["Population"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Population"]);
					countries.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					countries.Religions = dr["Religions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Religions"]);
					countries.Social_hierarchy = dr["Social_hierarchy"] == DBNull.Value ? default(String) : Convert.ToString(dr["Social_hierarchy"]);
					countries.Sports = dr["Sports"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sports"]);
					countries.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					countries.Towns = dr["Towns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Towns"]);
					countries.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					countries.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					countries.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);

					_return_value = countries;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<CountriesModel> GetAllCountriesForUserID(long userId)
		{
			List<CountriesModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Countries` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<CountriesModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						CountriesModel countries = new CountriesModel();
					countries.Architecture = dr["Architecture"] == DBNull.Value ? default(String) : Convert.ToString(dr["Architecture"]);
					countries.Area = dr["Area"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Area"]);
					countries.Bordering_countries = dr["Bordering_countries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Bordering_countries"]);
					countries.Climate = dr["Climate"] == DBNull.Value ? default(String) : Convert.ToString(dr["Climate"]);
					countries.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					countries.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					countries.Crops = dr["Crops"] == DBNull.Value ? default(String) : Convert.ToString(dr["Crops"]);
					countries.Currency = dr["Currency"] == DBNull.Value ? default(String) : Convert.ToString(dr["Currency"]);
					countries.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					countries.Education = dr["Education"] == DBNull.Value ? default(String) : Convert.ToString(dr["Education"]);
					countries.Established_year = dr["Established_year"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Established_year"]);
					countries.Flora = dr["Flora"] == DBNull.Value ? default(String) : Convert.ToString(dr["Flora"]);
					countries.Founding_story = dr["Founding_story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Founding_story"]);
					countries.Governments = dr["Governments"] == DBNull.Value ? default(String) : Convert.ToString(dr["Governments"]);
					countries.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					countries.Landmarks = dr["Landmarks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Landmarks"]);
					countries.Languages = dr["Languages"] == DBNull.Value ? default(String) : Convert.ToString(dr["Languages"]);
					countries.Laws = dr["Laws"] == DBNull.Value ? default(String) : Convert.ToString(dr["Laws"]);
					countries.Locations = dr["Locations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Locations"]);
					countries.Music = dr["Music"] == DBNull.Value ? default(String) : Convert.ToString(dr["Music"]);
					countries.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					countries.Notable_wars = dr["Notable_wars"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_wars"]);
					countries.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					countries.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					countries.Pop_culture = dr["Pop_culture"] == DBNull.Value ? default(String) : Convert.ToString(dr["Pop_culture"]);
					countries.Population = dr["Population"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Population"]);
					countries.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					countries.Religions = dr["Religions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Religions"]);
					countries.Social_hierarchy = dr["Social_hierarchy"] == DBNull.Value ? default(String) : Convert.ToString(dr["Social_hierarchy"]);
					countries.Sports = dr["Sports"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sports"]);
					countries.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					countries.Towns = dr["Towns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Towns"]);
					countries.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					countries.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					countries.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(countries.id, "countries");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    countries.object_id = first.object_id;
						    countries.object_name = first.object_name;
						}

						_return_value.Add(countries);
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

		public string AddCountriesData(CountriesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Countries`(`Architecture`,`Area`,`Bordering_countries`,`Climate`,`created_at`,`Creatures`,`Crops`,`Currency`,`Description`,`Education`,`Established_year`,`Flora`,`Founding_story`,`Governments`,`Landmarks`,`Languages`,`Laws`,`Locations`,`Music`,`Name`,`Notable_wars`,`Notes`,`Other_Names`,`Pop_culture`,`Population`,`Private_Notes`,`Religions`,`Social_hierarchy`,`Sports`,`Tags`,`Towns`,`Universe`,`updated_at`,`user_id`) VALUES(@Architecture,@Area,@Bordering_countries,@Climate,@created_at,@Creatures,@Crops,@Currency,@Description,@Education,@Established_year,@Flora,@Founding_story,@Governments,@Landmarks,@Languages,@Laws,@Locations,@Music,@Name,@Notable_wars,@Notes,@Other_Names,@Pop_culture,@Population,@Private_Notes,@Religions,@Social_hierarchy,@Sports,@Tags,@Towns,@Universe,@updated_at,@user_id)";
				dbContext.AddInParameter(dbContext.cmd, "@Architecture", Data.Architecture);
				dbContext.AddInParameter(dbContext.cmd, "@Area", Data.Area);
				dbContext.AddInParameter(dbContext.cmd, "@Bordering_countries", Data.Bordering_countries);
				dbContext.AddInParameter(dbContext.cmd, "@Climate", Data.Climate);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Creatures", Data.Creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Crops", Data.Crops);
				dbContext.AddInParameter(dbContext.cmd, "@Currency", Data.Currency);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Education", Data.Education);
				dbContext.AddInParameter(dbContext.cmd, "@Established_year", Data.Established_year);
				dbContext.AddInParameter(dbContext.cmd, "@Flora", Data.Flora);
				dbContext.AddInParameter(dbContext.cmd, "@Founding_story", Data.Founding_story);
				dbContext.AddInParameter(dbContext.cmd, "@Governments", Data.Governments);
				dbContext.AddInParameter(dbContext.cmd, "@Landmarks", Data.Landmarks);
				dbContext.AddInParameter(dbContext.cmd, "@Languages", Data.Languages);
				dbContext.AddInParameter(dbContext.cmd, "@Laws", Data.Laws);
				dbContext.AddInParameter(dbContext.cmd, "@Locations", Data.Locations);
				dbContext.AddInParameter(dbContext.cmd, "@Music", Data.Music);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_wars", Data.Notable_wars);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Other_Names", Data.Other_Names);
				dbContext.AddInParameter(dbContext.cmd, "@Pop_culture", Data.Pop_culture);
				dbContext.AddInParameter(dbContext.cmd, "@Population", Data.Population);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Religions", Data.Religions);
				dbContext.AddInParameter(dbContext.cmd, "@Social_hierarchy", Data.Social_hierarchy);
				dbContext.AddInParameter(dbContext.cmd, "@Sports", Data.Sports);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Towns", Data.Towns);
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
