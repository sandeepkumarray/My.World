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
	public class RacesDAL : BaseDAL
	{

		public RacesDAL()
		{
		}

		public  RacesDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteRacesData(RacesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Races` WHERE id = @id";
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

		public RacesModel GetRacesData(RacesModel Data)
		{
			RacesModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Races` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new RacesModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    RacesModel races = new RacesModel();
					races.Beliefs = dr["Beliefs"] == DBNull.Value ? default(String) : Convert.ToString(dr["Beliefs"]);
					races.Body_shape = dr["Body_shape"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Body_shape"]);
					races.Conditions = dr["Conditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Conditions"]);
					races.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					races.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					races.Economics = dr["Economics"] == DBNull.Value ? default(String) : Convert.ToString(dr["Economics"]);
					races.Famous_figures = dr["Famous_figures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Famous_figures"]);
					races.Favorite_foods = dr["Favorite_foods"] == DBNull.Value ? default(String) : Convert.ToString(dr["Favorite_foods"]);
					races.General_height = dr["General_height"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["General_height"]);
					races.General_weight = dr["General_weight"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["General_weight"]);
					races.Governments = dr["Governments"] == DBNull.Value ? default(String) : Convert.ToString(dr["Governments"]);
					races.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					races.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					races.Notable_events = dr["Notable_events"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_events"]);
					races.Notable_features = dr["Notable_features"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_features"]);
					races.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					races.Occupations = dr["Occupations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Occupations"]);
					races.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					races.Physical_variance = dr["Physical_variance"] == DBNull.Value ? default(String) : Convert.ToString(dr["Physical_variance"]);
					races.Private_notes = dr["Private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_notes"]);
					races.Skin_colors = dr["Skin_colors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Skin_colors"]);
					races.Strengths = dr["Strengths"] == DBNull.Value ? default(String) : Convert.ToString(dr["Strengths"]);
					races.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					races.Technologies = dr["Technologies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Technologies"]);
					races.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					races.Typical_clothing = dr["Typical_clothing"] == DBNull.Value ? default(String) : Convert.ToString(dr["Typical_clothing"]);
					races.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					races.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					races.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					races.Weaknesses = dr["Weaknesses"] == DBNull.Value ? default(String) : Convert.ToString(dr["Weaknesses"]);

					_return_value = races;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<RacesModel> GetAllRacesForUserID(long userId)
		{
			List<RacesModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Races` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<RacesModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						RacesModel races = new RacesModel();
					races.Beliefs = dr["Beliefs"] == DBNull.Value ? default(String) : Convert.ToString(dr["Beliefs"]);
					races.Body_shape = dr["Body_shape"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Body_shape"]);
					races.Conditions = dr["Conditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Conditions"]);
					races.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					races.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					races.Economics = dr["Economics"] == DBNull.Value ? default(String) : Convert.ToString(dr["Economics"]);
					races.Famous_figures = dr["Famous_figures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Famous_figures"]);
					races.Favorite_foods = dr["Favorite_foods"] == DBNull.Value ? default(String) : Convert.ToString(dr["Favorite_foods"]);
					races.General_height = dr["General_height"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["General_height"]);
					races.General_weight = dr["General_weight"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["General_weight"]);
					races.Governments = dr["Governments"] == DBNull.Value ? default(String) : Convert.ToString(dr["Governments"]);
					races.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					races.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					races.Notable_events = dr["Notable_events"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_events"]);
					races.Notable_features = dr["Notable_features"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_features"]);
					races.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					races.Occupations = dr["Occupations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Occupations"]);
					races.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					races.Physical_variance = dr["Physical_variance"] == DBNull.Value ? default(String) : Convert.ToString(dr["Physical_variance"]);
					races.Private_notes = dr["Private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_notes"]);
					races.Skin_colors = dr["Skin_colors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Skin_colors"]);
					races.Strengths = dr["Strengths"] == DBNull.Value ? default(String) : Convert.ToString(dr["Strengths"]);
					races.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					races.Technologies = dr["Technologies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Technologies"]);
					races.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					races.Typical_clothing = dr["Typical_clothing"] == DBNull.Value ? default(String) : Convert.ToString(dr["Typical_clothing"]);
					races.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					races.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					races.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					races.Weaknesses = dr["Weaknesses"] == DBNull.Value ? default(String) : Convert.ToString(dr["Weaknesses"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(races.id, "races");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    races.object_id = first.object_id;
						    races.object_name = first.object_name;
						}

						_return_value.Add(races);
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

		public string AddRacesData(RacesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Races`(`Beliefs`,`Body_shape`,`Conditions`,`created_at`,`Description`,`Economics`,`Famous_figures`,`Favorite_foods`,`General_height`,`General_weight`,`Governments`,`Name`,`Notable_events`,`Notable_features`,`Notes`,`Occupations`,`Other_Names`,`Physical_variance`,`Private_notes`,`Skin_colors`,`Strengths`,`Tags`,`Technologies`,`Traditions`,`Typical_clothing`,`Universe`,`updated_at`,`user_id`,`Weaknesses`) VALUES(@Beliefs,@Body_shape,@Conditions,@created_at,@Description,@Economics,@Famous_figures,@Favorite_foods,@General_height,@General_weight,@Governments,@Name,@Notable_events,@Notable_features,@Notes,@Occupations,@Other_Names,@Physical_variance,@Private_notes,@Skin_colors,@Strengths,@Tags,@Technologies,@Traditions,@Typical_clothing,@Universe,@updated_at,@user_id,@Weaknesses)";
				dbContext.AddInParameter(dbContext.cmd, "@Beliefs", Data.Beliefs);
				dbContext.AddInParameter(dbContext.cmd, "@Body_shape", Data.Body_shape);
				dbContext.AddInParameter(dbContext.cmd, "@Conditions", Data.Conditions);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Economics", Data.Economics);
				dbContext.AddInParameter(dbContext.cmd, "@Famous_figures", Data.Famous_figures);
				dbContext.AddInParameter(dbContext.cmd, "@Favorite_foods", Data.Favorite_foods);
				dbContext.AddInParameter(dbContext.cmd, "@General_height", Data.General_height);
				dbContext.AddInParameter(dbContext.cmd, "@General_weight", Data.General_weight);
				dbContext.AddInParameter(dbContext.cmd, "@Governments", Data.Governments);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_events", Data.Notable_events);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_features", Data.Notable_features);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Occupations", Data.Occupations);
				dbContext.AddInParameter(dbContext.cmd, "@Other_Names", Data.Other_Names);
				dbContext.AddInParameter(dbContext.cmd, "@Physical_variance", Data.Physical_variance);
				dbContext.AddInParameter(dbContext.cmd, "@Private_notes", Data.Private_notes);
				dbContext.AddInParameter(dbContext.cmd, "@Skin_colors", Data.Skin_colors);
				dbContext.AddInParameter(dbContext.cmd, "@Strengths", Data.Strengths);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Technologies", Data.Technologies);
				dbContext.AddInParameter(dbContext.cmd, "@Traditions", Data.Traditions);
				dbContext.AddInParameter(dbContext.cmd, "@Typical_clothing", Data.Typical_clothing);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Weaknesses", Data.Weaknesses);
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

		public string UpdateRacesData(RacesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "UPDATE races SET Beliefs = @Beliefs,Body_shape = @Body_shape,Conditions = @Conditions,created_at = @created_at,Description = @Description,Economics = @Economics,Famous_figures = @Famous_figures,Favorite_foods = @Favorite_foods,General_height = @General_height,General_weight = @General_weight,Governments = @Governments,Name = @Name,Notable_events = @Notable_events,Notable_features = @Notable_features,Notes = @Notes,Occupations = @Occupations,Other_Names = @Other_Names,Physical_variance = @Physical_variance,Private_notes = @Private_notes,Skin_colors = @Skin_colors,Strengths = @Strengths,Tags = @Tags,Technologies = @Technologies,Traditions = @Traditions,Typical_clothing = @Typical_clothing,Universe = @Universe,updated_at = @updated_at,user_id = @user_id,Weaknesses = @Weaknesses WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@Beliefs", Data.Beliefs);
				dbContext.AddInParameter(dbContext.cmd, "@Body_shape", Data.Body_shape);
				dbContext.AddInParameter(dbContext.cmd, "@Conditions", Data.Conditions);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Economics", Data.Economics);
				dbContext.AddInParameter(dbContext.cmd, "@Famous_figures", Data.Famous_figures);
				dbContext.AddInParameter(dbContext.cmd, "@Favorite_foods", Data.Favorite_foods);
				dbContext.AddInParameter(dbContext.cmd, "@General_height", Data.General_height);
				dbContext.AddInParameter(dbContext.cmd, "@General_weight", Data.General_weight);
				dbContext.AddInParameter(dbContext.cmd, "@Governments", Data.Governments);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_events", Data.Notable_events);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_features", Data.Notable_features);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Occupations", Data.Occupations);
				dbContext.AddInParameter(dbContext.cmd, "@Other_Names", Data.Other_Names);
				dbContext.AddInParameter(dbContext.cmd, "@Physical_variance", Data.Physical_variance);
				dbContext.AddInParameter(dbContext.cmd, "@Private_notes", Data.Private_notes);
				dbContext.AddInParameter(dbContext.cmd, "@Skin_colors", Data.Skin_colors);
				dbContext.AddInParameter(dbContext.cmd, "@Strengths", Data.Strengths);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Technologies", Data.Technologies);
				dbContext.AddInParameter(dbContext.cmd, "@Traditions", Data.Traditions);
				dbContext.AddInParameter(dbContext.cmd, "@Typical_clothing", Data.Typical_clothing);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Weaknesses", Data.Weaknesses);
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
