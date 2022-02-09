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
	public class PlanetsDAL : BaseDAL
	{

		public PlanetsDAL()
		{
		}

		public  PlanetsDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeletePlanetsData(PlanetsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Planets` WHERE id = @id";
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

		public PlanetsModel GetPlanetsData(PlanetsModel Data)
		{
			PlanetsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Planets` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new PlanetsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    PlanetsModel planets = new PlanetsModel();
					planets.Atmosphere = dr["Atmosphere"] == DBNull.Value ? default(String) : Convert.ToString(dr["Atmosphere"]);
					planets.Calendar_System = dr["Calendar_System"] == DBNull.Value ? default(String) : Convert.ToString(dr["Calendar_System"]);
					planets.Climate = dr["Climate"] == DBNull.Value ? default(String) : Convert.ToString(dr["Climate"]);
					planets.Continents = dr["Continents"] == DBNull.Value ? default(String) : Convert.ToString(dr["Continents"]);
					planets.Countries = dr["Countries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Countries"]);
					planets.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					planets.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					planets.Day_sky = dr["Day_sky"] == DBNull.Value ? default(String) : Convert.ToString(dr["Day_sky"]);
					planets.Deities = dr["Deities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Deities"]);
					planets.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					planets.First_Inhabitants_Story = dr["First_Inhabitants_Story"] == DBNull.Value ? default(String) : Convert.ToString(dr["First_Inhabitants_Story"]);
					planets.Flora = dr["Flora"] == DBNull.Value ? default(String) : Convert.ToString(dr["Flora"]);
					planets.Groups = dr["Groups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Groups"]);
					planets.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					planets.Landmarks = dr["Landmarks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Landmarks"]);
					planets.Languages = dr["Languages"] == DBNull.Value ? default(String) : Convert.ToString(dr["Languages"]);
					planets.Length_Of_Day = dr["Length_Of_Day"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Length_Of_Day"]);
					planets.Length_Of_Night = dr["Length_Of_Night"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Length_Of_Night"]);
					planets.Locations = dr["Locations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Locations"]);
					planets.Moons = dr["Moons"] == DBNull.Value ? default(String) : Convert.ToString(dr["Moons"]);
					planets.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					planets.Natural_diasters = dr["Natural_diasters"] == DBNull.Value ? default(String) : Convert.ToString(dr["Natural_diasters"]);
					planets.Natural_Resources = dr["Natural_Resources"] == DBNull.Value ? default(String) : Convert.ToString(dr["Natural_Resources"]);
					planets.Nearby_planets = dr["Nearby_planets"] == DBNull.Value ? default(String) : Convert.ToString(dr["Nearby_planets"]);
					planets.Night_sky = dr["Night_sky"] == DBNull.Value ? default(String) : Convert.ToString(dr["Night_sky"]);
					planets.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					planets.Orbit = dr["Orbit"] == DBNull.Value ? default(String) : Convert.ToString(dr["Orbit"]);
					planets.Population = dr["Population"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Population"]);
					planets.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					planets.Races = dr["Races"] == DBNull.Value ? default(String) : Convert.ToString(dr["Races"]);
					planets.Religions = dr["Religions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Religions"]);
					planets.Seasons = dr["Seasons"] == DBNull.Value ? default(String) : Convert.ToString(dr["Seasons"]);
					planets.Size = dr["Size"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Size"]);
					planets.Suns = dr["Suns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Suns"]);
					planets.Surface = dr["Surface"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Surface"]);
					planets.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					planets.Temperature = dr["Temperature"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Temperature"]);
					planets.Towns = dr["Towns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Towns"]);
					planets.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					planets.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					planets.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					planets.Visible_Constellations = dr["Visible_Constellations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Visible_Constellations"]);
					planets.Water_Content = dr["Water_Content"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Water_Content"]);
					planets.Weather = dr["Weather"] == DBNull.Value ? default(String) : Convert.ToString(dr["Weather"]);
					planets.World_History = dr["World_History"] == DBNull.Value ? default(String) : Convert.ToString(dr["World_History"]);

					_return_value = planets;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<PlanetsModel> GetAllPlanetsForUserID(long userId)
		{
			List<PlanetsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Planets` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<PlanetsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						PlanetsModel planets = new PlanetsModel();
					planets.Atmosphere = dr["Atmosphere"] == DBNull.Value ? default(String) : Convert.ToString(dr["Atmosphere"]);
					planets.Calendar_System = dr["Calendar_System"] == DBNull.Value ? default(String) : Convert.ToString(dr["Calendar_System"]);
					planets.Climate = dr["Climate"] == DBNull.Value ? default(String) : Convert.ToString(dr["Climate"]);
					planets.Continents = dr["Continents"] == DBNull.Value ? default(String) : Convert.ToString(dr["Continents"]);
					planets.Countries = dr["Countries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Countries"]);
					planets.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					planets.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					planets.Day_sky = dr["Day_sky"] == DBNull.Value ? default(String) : Convert.ToString(dr["Day_sky"]);
					planets.Deities = dr["Deities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Deities"]);
					planets.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					planets.First_Inhabitants_Story = dr["First_Inhabitants_Story"] == DBNull.Value ? default(String) : Convert.ToString(dr["First_Inhabitants_Story"]);
					planets.Flora = dr["Flora"] == DBNull.Value ? default(String) : Convert.ToString(dr["Flora"]);
					planets.Groups = dr["Groups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Groups"]);
					planets.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					planets.Landmarks = dr["Landmarks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Landmarks"]);
					planets.Languages = dr["Languages"] == DBNull.Value ? default(String) : Convert.ToString(dr["Languages"]);
					planets.Length_Of_Day = dr["Length_Of_Day"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Length_Of_Day"]);
					planets.Length_Of_Night = dr["Length_Of_Night"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Length_Of_Night"]);
					planets.Locations = dr["Locations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Locations"]);
					planets.Moons = dr["Moons"] == DBNull.Value ? default(String) : Convert.ToString(dr["Moons"]);
					planets.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					planets.Natural_diasters = dr["Natural_diasters"] == DBNull.Value ? default(String) : Convert.ToString(dr["Natural_diasters"]);
					planets.Natural_Resources = dr["Natural_Resources"] == DBNull.Value ? default(String) : Convert.ToString(dr["Natural_Resources"]);
					planets.Nearby_planets = dr["Nearby_planets"] == DBNull.Value ? default(String) : Convert.ToString(dr["Nearby_planets"]);
					planets.Night_sky = dr["Night_sky"] == DBNull.Value ? default(String) : Convert.ToString(dr["Night_sky"]);
					planets.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					planets.Orbit = dr["Orbit"] == DBNull.Value ? default(String) : Convert.ToString(dr["Orbit"]);
					planets.Population = dr["Population"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Population"]);
					planets.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					planets.Races = dr["Races"] == DBNull.Value ? default(String) : Convert.ToString(dr["Races"]);
					planets.Religions = dr["Religions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Religions"]);
					planets.Seasons = dr["Seasons"] == DBNull.Value ? default(String) : Convert.ToString(dr["Seasons"]);
					planets.Size = dr["Size"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Size"]);
					planets.Suns = dr["Suns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Suns"]);
					planets.Surface = dr["Surface"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Surface"]);
					planets.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					planets.Temperature = dr["Temperature"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Temperature"]);
					planets.Towns = dr["Towns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Towns"]);
					planets.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					planets.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					planets.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					planets.Visible_Constellations = dr["Visible_Constellations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Visible_Constellations"]);
					planets.Water_Content = dr["Water_Content"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Water_Content"]);
					planets.Weather = dr["Weather"] == DBNull.Value ? default(String) : Convert.ToString(dr["Weather"]);
					planets.World_History = dr["World_History"] == DBNull.Value ? default(String) : Convert.ToString(dr["World_History"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(planets.id, "planets");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    planets.object_id = first.object_id;
						    planets.object_name = first.object_name;
						}

						_return_value.Add(planets);
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

		public string AddPlanetsData(PlanetsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Planets`(`Atmosphere`,`Calendar_System`,`Climate`,`Continents`,`Countries`,`created_at`,`Creatures`,`Day_sky`,`Deities`,`Description`,`First_Inhabitants_Story`,`Flora`,`Groups`,`Landmarks`,`Languages`,`Length_Of_Day`,`Length_Of_Night`,`Locations`,`Moons`,`Name`,`Natural_diasters`,`Natural_Resources`,`Nearby_planets`,`Night_sky`,`Notes`,`Orbit`,`Population`,`Private_Notes`,`Races`,`Religions`,`Seasons`,`Size`,`Suns`,`Surface`,`Tags`,`Temperature`,`Towns`,`Universe`,`updated_at`,`user_id`,`Visible_Constellations`,`Water_Content`,`Weather`,`World_History`) VALUES(@Atmosphere,@Calendar_System,@Climate,@Continents,@Countries,@created_at,@Creatures,@Day_sky,@Deities,@Description,@First_Inhabitants_Story,@Flora,@Groups,@Landmarks,@Languages,@Length_Of_Day,@Length_Of_Night,@Locations,@Moons,@Name,@Natural_diasters,@Natural_Resources,@Nearby_planets,@Night_sky,@Notes,@Orbit,@Population,@Private_Notes,@Races,@Religions,@Seasons,@Size,@Suns,@Surface,@Tags,@Temperature,@Towns,@Universe,@updated_at,@user_id,@Visible_Constellations,@Water_Content,@Weather,@World_History)";
				dbContext.AddInParameter(dbContext.cmd, "@Atmosphere", Data.Atmosphere);
				dbContext.AddInParameter(dbContext.cmd, "@Calendar_System", Data.Calendar_System);
				dbContext.AddInParameter(dbContext.cmd, "@Climate", Data.Climate);
				dbContext.AddInParameter(dbContext.cmd, "@Continents", Data.Continents);
				dbContext.AddInParameter(dbContext.cmd, "@Countries", Data.Countries);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Creatures", Data.Creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Day_sky", Data.Day_sky);
				dbContext.AddInParameter(dbContext.cmd, "@Deities", Data.Deities);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@First_Inhabitants_Story", Data.First_Inhabitants_Story);
				dbContext.AddInParameter(dbContext.cmd, "@Flora", Data.Flora);
				dbContext.AddInParameter(dbContext.cmd, "@Groups", Data.Groups);
				dbContext.AddInParameter(dbContext.cmd, "@Landmarks", Data.Landmarks);
				dbContext.AddInParameter(dbContext.cmd, "@Languages", Data.Languages);
				dbContext.AddInParameter(dbContext.cmd, "@Length_Of_Day", Data.Length_Of_Day);
				dbContext.AddInParameter(dbContext.cmd, "@Length_Of_Night", Data.Length_Of_Night);
				dbContext.AddInParameter(dbContext.cmd, "@Locations", Data.Locations);
				dbContext.AddInParameter(dbContext.cmd, "@Moons", Data.Moons);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Natural_diasters", Data.Natural_diasters);
				dbContext.AddInParameter(dbContext.cmd, "@Natural_Resources", Data.Natural_Resources);
				dbContext.AddInParameter(dbContext.cmd, "@Nearby_planets", Data.Nearby_planets);
				dbContext.AddInParameter(dbContext.cmd, "@Night_sky", Data.Night_sky);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Orbit", Data.Orbit);
				dbContext.AddInParameter(dbContext.cmd, "@Population", Data.Population);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Races", Data.Races);
				dbContext.AddInParameter(dbContext.cmd, "@Religions", Data.Religions);
				dbContext.AddInParameter(dbContext.cmd, "@Seasons", Data.Seasons);
				dbContext.AddInParameter(dbContext.cmd, "@Size", Data.Size);
				dbContext.AddInParameter(dbContext.cmd, "@Suns", Data.Suns);
				dbContext.AddInParameter(dbContext.cmd, "@Surface", Data.Surface);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Temperature", Data.Temperature);
				dbContext.AddInParameter(dbContext.cmd, "@Towns", Data.Towns);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Visible_Constellations", Data.Visible_Constellations);
				dbContext.AddInParameter(dbContext.cmd, "@Water_Content", Data.Water_Content);
				dbContext.AddInParameter(dbContext.cmd, "@Weather", Data.Weather);
				dbContext.AddInParameter(dbContext.cmd, "@World_History", Data.World_History);
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
