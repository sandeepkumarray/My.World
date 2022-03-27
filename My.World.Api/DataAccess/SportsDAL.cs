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
	public class SportsDAL : BaseDAL
	{

		public SportsDAL()
		{
		}

		public  SportsDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteSportsData(SportsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Sports` WHERE id = @id";
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

		public SportsModel GetSportsData(SportsModel Data)
		{
			SportsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Sports` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new SportsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    SportsModel sports = new SportsModel();
					sports.Common_injuries = dr["Common_injuries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Common_injuries"]);
					sports.Countries = dr["Countries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Countries"]);
					sports.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					sports.Creators = dr["Creators"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creators"]);
					sports.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					sports.Equipment = dr["Equipment"] == DBNull.Value ? default(String) : Convert.ToString(dr["Equipment"]);
					sports.Evolution = dr["Evolution"] == DBNull.Value ? default(String) : Convert.ToString(dr["Evolution"]);
					sports.Famous_games = dr["Famous_games"] == DBNull.Value ? default(String) : Convert.ToString(dr["Famous_games"]);
					sports.Game_time = dr["Game_time"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Game_time"]);
					sports.How_to_win = dr["How_to_win"] == DBNull.Value ? default(String) : Convert.ToString(dr["How_to_win"]);
					sports.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					sports.Merchandise = dr["Merchandise"] == DBNull.Value ? default(String) : Convert.ToString(dr["Merchandise"]);
					sports.Most_important_muscles = dr["Most_important_muscles"] == DBNull.Value ? default(String) : Convert.ToString(dr["Most_important_muscles"]);
					sports.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					sports.Nicknames = dr["Nicknames"] == DBNull.Value ? default(String) : Convert.ToString(dr["Nicknames"]);
					sports.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					sports.Number_of_players = dr["Number_of_players"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Number_of_players"]);
					sports.Origin_story = dr["Origin_story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Origin_story"]);
					sports.Penalties = dr["Penalties"] == DBNull.Value ? default(String) : Convert.ToString(dr["Penalties"]);
					sports.Play_area = dr["Play_area"] == DBNull.Value ? default(String) : Convert.ToString(dr["Play_area"]);
					sports.Players = dr["Players"] == DBNull.Value ? default(String) : Convert.ToString(dr["Players"]);
					sports.Popularity = dr["Popularity"] == DBNull.Value ? default(String) : Convert.ToString(dr["Popularity"]);
					sports.Positions = dr["Positions"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Positions"]);
					sports.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					sports.Rules = dr["Rules"] == DBNull.Value ? default(String) : Convert.ToString(dr["Rules"]);
					sports.Scoring = dr["Scoring"] == DBNull.Value ? default(String) : Convert.ToString(dr["Scoring"]);
					sports.Strategies = dr["Strategies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Strategies"]);
					sports.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					sports.Teams = dr["Teams"] == DBNull.Value ? default(String) : Convert.ToString(dr["Teams"]);
					sports.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					sports.Uniforms = dr["Uniforms"] == DBNull.Value ? default(String) : Convert.ToString(dr["Uniforms"]);
					sports.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					sports.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					sports.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);

					_return_value = sports;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<SportsModel> GetAllSportsForUserID(long userId)
		{
			List<SportsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Sports` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<SportsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						SportsModel sports = new SportsModel();
					sports.Common_injuries = dr["Common_injuries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Common_injuries"]);
					sports.Countries = dr["Countries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Countries"]);
					sports.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					sports.Creators = dr["Creators"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creators"]);
					sports.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					sports.Equipment = dr["Equipment"] == DBNull.Value ? default(String) : Convert.ToString(dr["Equipment"]);
					sports.Evolution = dr["Evolution"] == DBNull.Value ? default(String) : Convert.ToString(dr["Evolution"]);
					sports.Famous_games = dr["Famous_games"] == DBNull.Value ? default(String) : Convert.ToString(dr["Famous_games"]);
					sports.Game_time = dr["Game_time"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Game_time"]);
					sports.How_to_win = dr["How_to_win"] == DBNull.Value ? default(String) : Convert.ToString(dr["How_to_win"]);
					sports.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					sports.Merchandise = dr["Merchandise"] == DBNull.Value ? default(String) : Convert.ToString(dr["Merchandise"]);
					sports.Most_important_muscles = dr["Most_important_muscles"] == DBNull.Value ? default(String) : Convert.ToString(dr["Most_important_muscles"]);
					sports.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					sports.Nicknames = dr["Nicknames"] == DBNull.Value ? default(String) : Convert.ToString(dr["Nicknames"]);
					sports.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					sports.Number_of_players = dr["Number_of_players"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Number_of_players"]);
					sports.Origin_story = dr["Origin_story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Origin_story"]);
					sports.Penalties = dr["Penalties"] == DBNull.Value ? default(String) : Convert.ToString(dr["Penalties"]);
					sports.Play_area = dr["Play_area"] == DBNull.Value ? default(String) : Convert.ToString(dr["Play_area"]);
					sports.Players = dr["Players"] == DBNull.Value ? default(String) : Convert.ToString(dr["Players"]);
					sports.Popularity = dr["Popularity"] == DBNull.Value ? default(String) : Convert.ToString(dr["Popularity"]);
					sports.Positions = dr["Positions"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Positions"]);
					sports.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					sports.Rules = dr["Rules"] == DBNull.Value ? default(String) : Convert.ToString(dr["Rules"]);
					sports.Scoring = dr["Scoring"] == DBNull.Value ? default(String) : Convert.ToString(dr["Scoring"]);
					sports.Strategies = dr["Strategies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Strategies"]);
					sports.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					sports.Teams = dr["Teams"] == DBNull.Value ? default(String) : Convert.ToString(dr["Teams"]);
					sports.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					sports.Uniforms = dr["Uniforms"] == DBNull.Value ? default(String) : Convert.ToString(dr["Uniforms"]);
					sports.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					sports.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					sports.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(sports.id, "sports");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    sports.object_id = first.object_id;
						    sports.object_name = first.object_name;
						}

						_return_value.Add(sports);
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

		public string AddSportsData(SportsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Sports`(`Common_injuries`,`Countries`,`created_at`,`Creators`,`Description`,`Equipment`,`Evolution`,`Famous_games`,`Game_time`,`How_to_win`,`Merchandise`,`Most_important_muscles`,`Name`,`Nicknames`,`Notes`,`Number_of_players`,`Origin_story`,`Penalties`,`Play_area`,`Players`,`Popularity`,`Positions`,`Private_Notes`,`Rules`,`Scoring`,`Strategies`,`Tags`,`Teams`,`Traditions`,`Uniforms`,`Universe`,`updated_at`,`user_id`) VALUES(@Common_injuries,@Countries,@created_at,@Creators,@Description,@Equipment,@Evolution,@Famous_games,@Game_time,@How_to_win,@Merchandise,@Most_important_muscles,@Name,@Nicknames,@Notes,@Number_of_players,@Origin_story,@Penalties,@Play_area,@Players,@Popularity,@Positions,@Private_Notes,@Rules,@Scoring,@Strategies,@Tags,@Teams,@Traditions,@Uniforms,@Universe,@updated_at,@user_id)";
				dbContext.AddInParameter(dbContext.cmd, "@Common_injuries", Data.Common_injuries);
				dbContext.AddInParameter(dbContext.cmd, "@Countries", Data.Countries);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Creators", Data.Creators);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Equipment", Data.Equipment);
				dbContext.AddInParameter(dbContext.cmd, "@Evolution", Data.Evolution);
				dbContext.AddInParameter(dbContext.cmd, "@Famous_games", Data.Famous_games);
				dbContext.AddInParameter(dbContext.cmd, "@Game_time", Data.Game_time);
				dbContext.AddInParameter(dbContext.cmd, "@How_to_win", Data.How_to_win);
				dbContext.AddInParameter(dbContext.cmd, "@Merchandise", Data.Merchandise);
				dbContext.AddInParameter(dbContext.cmd, "@Most_important_muscles", Data.Most_important_muscles);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Nicknames", Data.Nicknames);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Number_of_players", Data.Number_of_players);
				dbContext.AddInParameter(dbContext.cmd, "@Origin_story", Data.Origin_story);
				dbContext.AddInParameter(dbContext.cmd, "@Penalties", Data.Penalties);
				dbContext.AddInParameter(dbContext.cmd, "@Play_area", Data.Play_area);
				dbContext.AddInParameter(dbContext.cmd, "@Players", Data.Players);
				dbContext.AddInParameter(dbContext.cmd, "@Popularity", Data.Popularity);
				dbContext.AddInParameter(dbContext.cmd, "@Positions", Data.Positions);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Rules", Data.Rules);
				dbContext.AddInParameter(dbContext.cmd, "@Scoring", Data.Scoring);
				dbContext.AddInParameter(dbContext.cmd, "@Strategies", Data.Strategies);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Teams", Data.Teams);
				dbContext.AddInParameter(dbContext.cmd, "@Traditions", Data.Traditions);
				dbContext.AddInParameter(dbContext.cmd, "@Uniforms", Data.Uniforms);
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

		public string UpdateSportsData(SportsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "UPDATE sports SET Common_injuries = @Common_injuries,Countries = @Countries,created_at = @created_at,Creators = @Creators,Description = @Description,Equipment = @Equipment,Evolution = @Evolution,Famous_games = @Famous_games,Game_time = @Game_time,How_to_win = @How_to_win,Merchandise = @Merchandise,Most_important_muscles = @Most_important_muscles,Name = @Name,Nicknames = @Nicknames,Notes = @Notes,Number_of_players = @Number_of_players,Origin_story = @Origin_story,Penalties = @Penalties,Play_area = @Play_area,Players = @Players,Popularity = @Popularity,Positions = @Positions,Private_Notes = @Private_Notes,Rules = @Rules,Scoring = @Scoring,Strategies = @Strategies,Tags = @Tags,Teams = @Teams,Traditions = @Traditions,Uniforms = @Uniforms,Universe = @Universe,updated_at = @updated_at,user_id = @user_id WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@Common_injuries", Data.Common_injuries);
				dbContext.AddInParameter(dbContext.cmd, "@Countries", Data.Countries);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Creators", Data.Creators);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Equipment", Data.Equipment);
				dbContext.AddInParameter(dbContext.cmd, "@Evolution", Data.Evolution);
				dbContext.AddInParameter(dbContext.cmd, "@Famous_games", Data.Famous_games);
				dbContext.AddInParameter(dbContext.cmd, "@Game_time", Data.Game_time);
				dbContext.AddInParameter(dbContext.cmd, "@How_to_win", Data.How_to_win);
				dbContext.AddInParameter(dbContext.cmd, "@Merchandise", Data.Merchandise);
				dbContext.AddInParameter(dbContext.cmd, "@Most_important_muscles", Data.Most_important_muscles);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Nicknames", Data.Nicknames);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Number_of_players", Data.Number_of_players);
				dbContext.AddInParameter(dbContext.cmd, "@Origin_story", Data.Origin_story);
				dbContext.AddInParameter(dbContext.cmd, "@Penalties", Data.Penalties);
				dbContext.AddInParameter(dbContext.cmd, "@Play_area", Data.Play_area);
				dbContext.AddInParameter(dbContext.cmd, "@Players", Data.Players);
				dbContext.AddInParameter(dbContext.cmd, "@Popularity", Data.Popularity);
				dbContext.AddInParameter(dbContext.cmd, "@Positions", Data.Positions);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Rules", Data.Rules);
				dbContext.AddInParameter(dbContext.cmd, "@Scoring", Data.Scoring);
				dbContext.AddInParameter(dbContext.cmd, "@Strategies", Data.Strategies);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Teams", Data.Teams);
				dbContext.AddInParameter(dbContext.cmd, "@Traditions", Data.Traditions);
				dbContext.AddInParameter(dbContext.cmd, "@Uniforms", Data.Uniforms);
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
