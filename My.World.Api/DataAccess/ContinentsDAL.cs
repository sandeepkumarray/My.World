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
	public class ContinentsDAL : BaseDAL
	{

		public ContinentsDAL()
		{
		}

		public  ContinentsDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteContinentsData(ContinentsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Continents` WHERE id = @id";
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

		public ContinentsModel GetContinentsData(ContinentsModel Data)
		{
			ContinentsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Continents` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new ContinentsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    ContinentsModel continents = new ContinentsModel();
					continents.Architecture = dr["Architecture"] == DBNull.Value ? default(String) : Convert.ToString(dr["Architecture"]);
					continents.Area = dr["Area"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Area"]);
					continents.Bodies_of_water = dr["Bodies_of_water"] == DBNull.Value ? default(String) : Convert.ToString(dr["Bodies_of_water"]);
					continents.Countries = dr["Countries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Countries"]);
					continents.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					continents.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					continents.Crops = dr["Crops"] == DBNull.Value ? default(String) : Convert.ToString(dr["Crops"]);
					continents.Demonym = dr["Demonym"] == DBNull.Value ? default(String) : Convert.ToString(dr["Demonym"]);
					continents.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					continents.Discovery = dr["Discovery"] == DBNull.Value ? default(String) : Convert.ToString(dr["Discovery"]);
					continents.Economy = dr["Economy"] == DBNull.Value ? default(String) : Convert.ToString(dr["Economy"]);
					continents.Floras = dr["Floras"] == DBNull.Value ? default(String) : Convert.ToString(dr["Floras"]);
					continents.Formation = dr["Formation"] == DBNull.Value ? default(String) : Convert.ToString(dr["Formation"]);
					continents.Governments = dr["Governments"] == DBNull.Value ? default(String) : Convert.ToString(dr["Governments"]);
					continents.Humidity = dr["Humidity"] == DBNull.Value ? default(String) : Convert.ToString(dr["Humidity"]);
					continents.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					continents.Landmarks = dr["Landmarks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Landmarks"]);
					continents.Languages = dr["Languages"] == DBNull.Value ? default(String) : Convert.ToString(dr["Languages"]);
					continents.Local_name = dr["Local_name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Local_name"]);
					continents.Mineralogy = dr["Mineralogy"] == DBNull.Value ? default(String) : Convert.ToString(dr["Mineralogy"]);
					continents.Natural_disasters = dr["Natural_disasters"] == DBNull.Value ? default(String) : Convert.ToString(dr["Natural_disasters"]);
					continents.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					continents.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					continents.Politics = dr["Politics"] == DBNull.Value ? default(String) : Convert.ToString(dr["Politics"]);
					continents.Popular_foods = dr["Popular_foods"] == DBNull.Value ? default(String) : Convert.ToString(dr["Popular_foods"]);
					continents.Population = dr["Population"] == DBNull.Value ? default(String) : Convert.ToString(dr["Population"]);
					continents.Precipitation = dr["Precipitation"] == DBNull.Value ? default(String) : Convert.ToString(dr["Precipitation"]);
					continents.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					continents.Regional_advantages = dr["Regional_advantages"] == DBNull.Value ? default(String) : Convert.ToString(dr["Regional_advantages"]);
					continents.Regional_disadvantages = dr["Regional_disadvantages"] == DBNull.Value ? default(String) : Convert.ToString(dr["Regional_disadvantages"]);
					continents.Reputation = dr["Reputation"] == DBNull.Value ? default(String) : Convert.ToString(dr["Reputation"]);
					continents.Ruins = dr["Ruins"] == DBNull.Value ? default(String) : Convert.ToString(dr["Ruins"]);
					continents.Seasons = dr["Seasons"] == DBNull.Value ? default(String) : Convert.ToString(dr["Seasons"]);
					continents.Shape = dr["Shape"] == DBNull.Value ? default(String) : Convert.ToString(dr["Shape"]);
					continents.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					continents.Temperature = dr["Temperature"] == DBNull.Value ? default(String) : Convert.ToString(dr["Temperature"]);
					continents.Topography = dr["Topography"] == DBNull.Value ? default(String) : Convert.ToString(dr["Topography"]);
					continents.Tourism = dr["Tourism"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tourism"]);
					continents.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					continents.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					continents.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					continents.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					continents.Wars = dr["Wars"] == DBNull.Value ? default(String) : Convert.ToString(dr["Wars"]);
					continents.Winds = dr["Winds"] == DBNull.Value ? default(String) : Convert.ToString(dr["Winds"]);

					_return_value = continents;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<ContinentsModel> GetAllContinentsForUserID(long userId)
		{
			List<ContinentsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Continents` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<ContinentsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						ContinentsModel continents = new ContinentsModel();
					continents.Architecture = dr["Architecture"] == DBNull.Value ? default(String) : Convert.ToString(dr["Architecture"]);
					continents.Area = dr["Area"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Area"]);
					continents.Bodies_of_water = dr["Bodies_of_water"] == DBNull.Value ? default(String) : Convert.ToString(dr["Bodies_of_water"]);
					continents.Countries = dr["Countries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Countries"]);
					continents.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					continents.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					continents.Crops = dr["Crops"] == DBNull.Value ? default(String) : Convert.ToString(dr["Crops"]);
					continents.Demonym = dr["Demonym"] == DBNull.Value ? default(String) : Convert.ToString(dr["Demonym"]);
					continents.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					continents.Discovery = dr["Discovery"] == DBNull.Value ? default(String) : Convert.ToString(dr["Discovery"]);
					continents.Economy = dr["Economy"] == DBNull.Value ? default(String) : Convert.ToString(dr["Economy"]);
					continents.Floras = dr["Floras"] == DBNull.Value ? default(String) : Convert.ToString(dr["Floras"]);
					continents.Formation = dr["Formation"] == DBNull.Value ? default(String) : Convert.ToString(dr["Formation"]);
					continents.Governments = dr["Governments"] == DBNull.Value ? default(String) : Convert.ToString(dr["Governments"]);
					continents.Humidity = dr["Humidity"] == DBNull.Value ? default(String) : Convert.ToString(dr["Humidity"]);
					continents.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					continents.Landmarks = dr["Landmarks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Landmarks"]);
					continents.Languages = dr["Languages"] == DBNull.Value ? default(String) : Convert.ToString(dr["Languages"]);
					continents.Local_name = dr["Local_name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Local_name"]);
					continents.Mineralogy = dr["Mineralogy"] == DBNull.Value ? default(String) : Convert.ToString(dr["Mineralogy"]);
					continents.Natural_disasters = dr["Natural_disasters"] == DBNull.Value ? default(String) : Convert.ToString(dr["Natural_disasters"]);
					continents.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					continents.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					continents.Politics = dr["Politics"] == DBNull.Value ? default(String) : Convert.ToString(dr["Politics"]);
					continents.Popular_foods = dr["Popular_foods"] == DBNull.Value ? default(String) : Convert.ToString(dr["Popular_foods"]);
					continents.Population = dr["Population"] == DBNull.Value ? default(String) : Convert.ToString(dr["Population"]);
					continents.Precipitation = dr["Precipitation"] == DBNull.Value ? default(String) : Convert.ToString(dr["Precipitation"]);
					continents.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					continents.Regional_advantages = dr["Regional_advantages"] == DBNull.Value ? default(String) : Convert.ToString(dr["Regional_advantages"]);
					continents.Regional_disadvantages = dr["Regional_disadvantages"] == DBNull.Value ? default(String) : Convert.ToString(dr["Regional_disadvantages"]);
					continents.Reputation = dr["Reputation"] == DBNull.Value ? default(String) : Convert.ToString(dr["Reputation"]);
					continents.Ruins = dr["Ruins"] == DBNull.Value ? default(String) : Convert.ToString(dr["Ruins"]);
					continents.Seasons = dr["Seasons"] == DBNull.Value ? default(String) : Convert.ToString(dr["Seasons"]);
					continents.Shape = dr["Shape"] == DBNull.Value ? default(String) : Convert.ToString(dr["Shape"]);
					continents.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					continents.Temperature = dr["Temperature"] == DBNull.Value ? default(String) : Convert.ToString(dr["Temperature"]);
					continents.Topography = dr["Topography"] == DBNull.Value ? default(String) : Convert.ToString(dr["Topography"]);
					continents.Tourism = dr["Tourism"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tourism"]);
					continents.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					continents.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					continents.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					continents.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					continents.Wars = dr["Wars"] == DBNull.Value ? default(String) : Convert.ToString(dr["Wars"]);
					continents.Winds = dr["Winds"] == DBNull.Value ? default(String) : Convert.ToString(dr["Winds"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(continents.id, "continents");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    continents.object_id = first.object_id;
						    continents.object_name = first.object_name;
						}

						_return_value.Add(continents);
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

		public string AddContinentsData(ContinentsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Continents`(`Architecture`,`Area`,`Bodies_of_water`,`Countries`,`created_at`,`Creatures`,`Crops`,`Demonym`,`Description`,`Discovery`,`Economy`,`Floras`,`Formation`,`Governments`,`Humidity`,`Landmarks`,`Languages`,`Local_name`,`Mineralogy`,`Natural_disasters`,`Notes`,`Other_Names`,`Politics`,`Popular_foods`,`Population`,`Precipitation`,`Private_Notes`,`Regional_advantages`,`Regional_disadvantages`,`Reputation`,`Ruins`,`Seasons`,`Shape`,`Tags`,`Temperature`,`Topography`,`Tourism`,`Traditions`,`Universe`,`updated_at`,`user_id`,`Wars`,`Winds`) VALUES(@Architecture,@Area,@Bodies_of_water,@Countries,@created_at,@Creatures,@Crops,@Demonym,@Description,@Discovery,@Economy,@Floras,@Formation,@Governments,@Humidity,@Landmarks,@Languages,@Local_name,@Mineralogy,@Natural_disasters,@Notes,@Other_Names,@Politics,@Popular_foods,@Population,@Precipitation,@Private_Notes,@Regional_advantages,@Regional_disadvantages,@Reputation,@Ruins,@Seasons,@Shape,@Tags,@Temperature,@Topography,@Tourism,@Traditions,@Universe,@updated_at,@user_id,@Wars,@Winds)";
				dbContext.AddInParameter(dbContext.cmd, "@Architecture", Data.Architecture);
				dbContext.AddInParameter(dbContext.cmd, "@Area", Data.Area);
				dbContext.AddInParameter(dbContext.cmd, "@Bodies_of_water", Data.Bodies_of_water);
				dbContext.AddInParameter(dbContext.cmd, "@Countries", Data.Countries);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Creatures", Data.Creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Crops", Data.Crops);
				dbContext.AddInParameter(dbContext.cmd, "@Demonym", Data.Demonym);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Discovery", Data.Discovery);
				dbContext.AddInParameter(dbContext.cmd, "@Economy", Data.Economy);
				dbContext.AddInParameter(dbContext.cmd, "@Floras", Data.Floras);
				dbContext.AddInParameter(dbContext.cmd, "@Formation", Data.Formation);
				dbContext.AddInParameter(dbContext.cmd, "@Governments", Data.Governments);
				dbContext.AddInParameter(dbContext.cmd, "@Humidity", Data.Humidity);
				dbContext.AddInParameter(dbContext.cmd, "@Landmarks", Data.Landmarks);
				dbContext.AddInParameter(dbContext.cmd, "@Languages", Data.Languages);
				dbContext.AddInParameter(dbContext.cmd, "@Local_name", Data.Local_name);
				dbContext.AddInParameter(dbContext.cmd, "@Mineralogy", Data.Mineralogy);
				dbContext.AddInParameter(dbContext.cmd, "@Natural_disasters", Data.Natural_disasters);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Other_Names", Data.Other_Names);
				dbContext.AddInParameter(dbContext.cmd, "@Politics", Data.Politics);
				dbContext.AddInParameter(dbContext.cmd, "@Popular_foods", Data.Popular_foods);
				dbContext.AddInParameter(dbContext.cmd, "@Population", Data.Population);
				dbContext.AddInParameter(dbContext.cmd, "@Precipitation", Data.Precipitation);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Regional_advantages", Data.Regional_advantages);
				dbContext.AddInParameter(dbContext.cmd, "@Regional_disadvantages", Data.Regional_disadvantages);
				dbContext.AddInParameter(dbContext.cmd, "@Reputation", Data.Reputation);
				dbContext.AddInParameter(dbContext.cmd, "@Ruins", Data.Ruins);
				dbContext.AddInParameter(dbContext.cmd, "@Seasons", Data.Seasons);
				dbContext.AddInParameter(dbContext.cmd, "@Shape", Data.Shape);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Temperature", Data.Temperature);
				dbContext.AddInParameter(dbContext.cmd, "@Topography", Data.Topography);
				dbContext.AddInParameter(dbContext.cmd, "@Tourism", Data.Tourism);
				dbContext.AddInParameter(dbContext.cmd, "@Traditions", Data.Traditions);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Wars", Data.Wars);
				dbContext.AddInParameter(dbContext.cmd, "@Winds", Data.Winds);
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

		public string UpdateContinentsData(ContinentsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "UPDATE continents SET Architecture = @Architecture,Area = @Area,Bodies_of_water = @Bodies_of_water,Countries = @Countries,created_at = @created_at,Creatures = @Creatures,Crops = @Crops,Demonym = @Demonym,Description = @Description,Discovery = @Discovery,Economy = @Economy,Floras = @Floras,Formation = @Formation,Governments = @Governments,Humidity = @Humidity,Landmarks = @Landmarks,Languages = @Languages,Local_name = @Local_name,Mineralogy = @Mineralogy,Natural_disasters = @Natural_disasters,Notes = @Notes,Other_Names = @Other_Names,Politics = @Politics,Popular_foods = @Popular_foods,Population = @Population,Precipitation = @Precipitation,Private_Notes = @Private_Notes,Regional_advantages = @Regional_advantages,Regional_disadvantages = @Regional_disadvantages,Reputation = @Reputation,Ruins = @Ruins,Seasons = @Seasons,Shape = @Shape,Tags = @Tags,Temperature = @Temperature,Topography = @Topography,Tourism = @Tourism,Traditions = @Traditions,Universe = @Universe,updated_at = @updated_at,user_id = @user_id,Wars = @Wars,Winds = @Winds WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@Architecture", Data.Architecture);
				dbContext.AddInParameter(dbContext.cmd, "@Area", Data.Area);
				dbContext.AddInParameter(dbContext.cmd, "@Bodies_of_water", Data.Bodies_of_water);
				dbContext.AddInParameter(dbContext.cmd, "@Countries", Data.Countries);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Creatures", Data.Creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Crops", Data.Crops);
				dbContext.AddInParameter(dbContext.cmd, "@Demonym", Data.Demonym);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Discovery", Data.Discovery);
				dbContext.AddInParameter(dbContext.cmd, "@Economy", Data.Economy);
				dbContext.AddInParameter(dbContext.cmd, "@Floras", Data.Floras);
				dbContext.AddInParameter(dbContext.cmd, "@Formation", Data.Formation);
				dbContext.AddInParameter(dbContext.cmd, "@Governments", Data.Governments);
				dbContext.AddInParameter(dbContext.cmd, "@Humidity", Data.Humidity);
				dbContext.AddInParameter(dbContext.cmd, "@Landmarks", Data.Landmarks);
				dbContext.AddInParameter(dbContext.cmd, "@Languages", Data.Languages);
				dbContext.AddInParameter(dbContext.cmd, "@Local_name", Data.Local_name);
				dbContext.AddInParameter(dbContext.cmd, "@Mineralogy", Data.Mineralogy);
				dbContext.AddInParameter(dbContext.cmd, "@Natural_disasters", Data.Natural_disasters);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Other_Names", Data.Other_Names);
				dbContext.AddInParameter(dbContext.cmd, "@Politics", Data.Politics);
				dbContext.AddInParameter(dbContext.cmd, "@Popular_foods", Data.Popular_foods);
				dbContext.AddInParameter(dbContext.cmd, "@Population", Data.Population);
				dbContext.AddInParameter(dbContext.cmd, "@Precipitation", Data.Precipitation);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Regional_advantages", Data.Regional_advantages);
				dbContext.AddInParameter(dbContext.cmd, "@Regional_disadvantages", Data.Regional_disadvantages);
				dbContext.AddInParameter(dbContext.cmd, "@Reputation", Data.Reputation);
				dbContext.AddInParameter(dbContext.cmd, "@Ruins", Data.Ruins);
				dbContext.AddInParameter(dbContext.cmd, "@Seasons", Data.Seasons);
				dbContext.AddInParameter(dbContext.cmd, "@Shape", Data.Shape);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Temperature", Data.Temperature);
				dbContext.AddInParameter(dbContext.cmd, "@Topography", Data.Topography);
				dbContext.AddInParameter(dbContext.cmd, "@Tourism", Data.Tourism);
				dbContext.AddInParameter(dbContext.cmd, "@Traditions", Data.Traditions);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Wars", Data.Wars);
				dbContext.AddInParameter(dbContext.cmd, "@Winds", Data.Winds);
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
