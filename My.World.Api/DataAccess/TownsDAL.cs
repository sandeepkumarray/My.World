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
	public class TownsDAL : BaseDAL
	{

		public TownsDAL()
		{
		}

		public  TownsDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteTownsData(TownsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Towns` WHERE id = @id";
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

		public TownsModel GetTownsData(TownsModel Data)
		{
			TownsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Towns` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new TownsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    TownsModel towns = new TownsModel();
					towns.Buildings = dr["Buildings"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Buildings"]);
					towns.Busy_areas = dr["Busy_areas"] == DBNull.Value ? default(String) : Convert.ToString(dr["Busy_areas"]);
					towns.Citizens = dr["Citizens"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Citizens"]);
					towns.Country = dr["Country"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Country"]);
					towns.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					towns.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					towns.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					towns.Energy_sources = dr["Energy_sources"] == DBNull.Value ? default(String) : Convert.ToString(dr["Energy_sources"]);
					towns.Established_year = dr["Established_year"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Established_year"]);
					towns.Flora = dr["Flora"] == DBNull.Value ? default(String) : Convert.ToString(dr["Flora"]);
					towns.Food_sources = dr["Food_sources"] == DBNull.Value ? default(String) : Convert.ToString(dr["Food_sources"]);
					towns.Founding_story = dr["Founding_story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Founding_story"]);
					towns.Groups = dr["Groups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Groups"]);
					towns.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					towns.Landmarks = dr["Landmarks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Landmarks"]);
					towns.Languages = dr["Languages"] == DBNull.Value ? default(String) : Convert.ToString(dr["Languages"]);
					towns.Laws = dr["Laws"] == DBNull.Value ? default(String) : Convert.ToString(dr["Laws"]);
					towns.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					towns.Neighborhoods = dr["Neighborhoods"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Neighborhoods"]);
					towns.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					towns.Other_names = dr["Other_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_names"]);
					towns.Politics = dr["Politics"] == DBNull.Value ? default(String) : Convert.ToString(dr["Politics"]);
					towns.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					towns.Recycling = dr["Recycling"] == DBNull.Value ? default(String) : Convert.ToString(dr["Recycling"]);
					towns.Sports = dr["Sports"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sports"]);
					towns.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					towns.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					towns.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					towns.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					towns.Waste = dr["Waste"] == DBNull.Value ? default(String) : Convert.ToString(dr["Waste"]);

					_return_value = towns;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<TownsModel> GetAllTownsForUserID(long userId)
		{
			List<TownsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Towns` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<TownsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						TownsModel towns = new TownsModel();
					towns.Buildings = dr["Buildings"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Buildings"]);
					towns.Busy_areas = dr["Busy_areas"] == DBNull.Value ? default(String) : Convert.ToString(dr["Busy_areas"]);
					towns.Citizens = dr["Citizens"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Citizens"]);
					towns.Country = dr["Country"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Country"]);
					towns.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					towns.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					towns.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					towns.Energy_sources = dr["Energy_sources"] == DBNull.Value ? default(String) : Convert.ToString(dr["Energy_sources"]);
					towns.Established_year = dr["Established_year"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Established_year"]);
					towns.Flora = dr["Flora"] == DBNull.Value ? default(String) : Convert.ToString(dr["Flora"]);
					towns.Food_sources = dr["Food_sources"] == DBNull.Value ? default(String) : Convert.ToString(dr["Food_sources"]);
					towns.Founding_story = dr["Founding_story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Founding_story"]);
					towns.Groups = dr["Groups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Groups"]);
					towns.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					towns.Landmarks = dr["Landmarks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Landmarks"]);
					towns.Languages = dr["Languages"] == DBNull.Value ? default(String) : Convert.ToString(dr["Languages"]);
					towns.Laws = dr["Laws"] == DBNull.Value ? default(String) : Convert.ToString(dr["Laws"]);
					towns.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					towns.Neighborhoods = dr["Neighborhoods"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Neighborhoods"]);
					towns.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					towns.Other_names = dr["Other_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_names"]);
					towns.Politics = dr["Politics"] == DBNull.Value ? default(String) : Convert.ToString(dr["Politics"]);
					towns.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					towns.Recycling = dr["Recycling"] == DBNull.Value ? default(String) : Convert.ToString(dr["Recycling"]);
					towns.Sports = dr["Sports"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sports"]);
					towns.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					towns.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					towns.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					towns.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					towns.Waste = dr["Waste"] == DBNull.Value ? default(String) : Convert.ToString(dr["Waste"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(towns.id, "towns");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    towns.object_id = first.object_id;
						    towns.object_name = first.object_name;
						}

						_return_value.Add(towns);
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

		public string AddTownsData(TownsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Towns`(`Buildings`,`Busy_areas`,`Citizens`,`Country`,`created_at`,`Creatures`,`Description`,`Energy_sources`,`Established_year`,`Flora`,`Food_sources`,`Founding_story`,`Groups`,`Landmarks`,`Languages`,`Laws`,`Name`,`Neighborhoods`,`Notes`,`Other_names`,`Politics`,`Private_Notes`,`Recycling`,`Sports`,`Tags`,`Universe`,`updated_at`,`user_id`,`Waste`) VALUES(@Buildings,@Busy_areas,@Citizens,@Country,@created_at,@Creatures,@Description,@Energy_sources,@Established_year,@Flora,@Food_sources,@Founding_story,@Groups,@Landmarks,@Languages,@Laws,@Name,@Neighborhoods,@Notes,@Other_names,@Politics,@Private_Notes,@Recycling,@Sports,@Tags,@Universe,@updated_at,@user_id,@Waste)";
				dbContext.AddInParameter(dbContext.cmd, "@Buildings", Data.Buildings);
				dbContext.AddInParameter(dbContext.cmd, "@Busy_areas", Data.Busy_areas);
				dbContext.AddInParameter(dbContext.cmd, "@Citizens", Data.Citizens);
				dbContext.AddInParameter(dbContext.cmd, "@Country", Data.Country);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Creatures", Data.Creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Energy_sources", Data.Energy_sources);
				dbContext.AddInParameter(dbContext.cmd, "@Established_year", Data.Established_year);
				dbContext.AddInParameter(dbContext.cmd, "@Flora", Data.Flora);
				dbContext.AddInParameter(dbContext.cmd, "@Food_sources", Data.Food_sources);
				dbContext.AddInParameter(dbContext.cmd, "@Founding_story", Data.Founding_story);
				dbContext.AddInParameter(dbContext.cmd, "@Groups", Data.Groups);
				dbContext.AddInParameter(dbContext.cmd, "@Landmarks", Data.Landmarks);
				dbContext.AddInParameter(dbContext.cmd, "@Languages", Data.Languages);
				dbContext.AddInParameter(dbContext.cmd, "@Laws", Data.Laws);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Neighborhoods", Data.Neighborhoods);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Other_names", Data.Other_names);
				dbContext.AddInParameter(dbContext.cmd, "@Politics", Data.Politics);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Recycling", Data.Recycling);
				dbContext.AddInParameter(dbContext.cmd, "@Sports", Data.Sports);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Waste", Data.Waste);
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

		public string UpdateTownsData(TownsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "UPDATE towns SET Buildings = @Buildings,Busy_areas = @Busy_areas,Citizens = @Citizens,Country = @Country,created_at = @created_at,Creatures = @Creatures,Description = @Description,Energy_sources = @Energy_sources,Established_year = @Established_year,Flora = @Flora,Food_sources = @Food_sources,Founding_story = @Founding_story,Groups = @Groups,Landmarks = @Landmarks,Languages = @Languages,Laws = @Laws,Name = @Name,Neighborhoods = @Neighborhoods,Notes = @Notes,Other_names = @Other_names,Politics = @Politics,Private_Notes = @Private_Notes,Recycling = @Recycling,Sports = @Sports,Tags = @Tags,Universe = @Universe,updated_at = @updated_at,user_id = @user_id,Waste = @Waste WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@Buildings", Data.Buildings);
				dbContext.AddInParameter(dbContext.cmd, "@Busy_areas", Data.Busy_areas);
				dbContext.AddInParameter(dbContext.cmd, "@Citizens", Data.Citizens);
				dbContext.AddInParameter(dbContext.cmd, "@Country", Data.Country);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Creatures", Data.Creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Energy_sources", Data.Energy_sources);
				dbContext.AddInParameter(dbContext.cmd, "@Established_year", Data.Established_year);
				dbContext.AddInParameter(dbContext.cmd, "@Flora", Data.Flora);
				dbContext.AddInParameter(dbContext.cmd, "@Food_sources", Data.Food_sources);
				dbContext.AddInParameter(dbContext.cmd, "@Founding_story", Data.Founding_story);
				dbContext.AddInParameter(dbContext.cmd, "@Groups", Data.Groups);
				dbContext.AddInParameter(dbContext.cmd, "@Landmarks", Data.Landmarks);
				dbContext.AddInParameter(dbContext.cmd, "@Languages", Data.Languages);
				dbContext.AddInParameter(dbContext.cmd, "@Laws", Data.Laws);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Neighborhoods", Data.Neighborhoods);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Other_names", Data.Other_names);
				dbContext.AddInParameter(dbContext.cmd, "@Politics", Data.Politics);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Recycling", Data.Recycling);
				dbContext.AddInParameter(dbContext.cmd, "@Sports", Data.Sports);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Waste", Data.Waste);
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
