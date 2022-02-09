using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using My.World.Api.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace My.World.Api.DataAccess
{
	public class UserDetailsDal : BaseDAL
	{

		public UserDetailsDal()
		{
		}

		public  UserDetailsDal(DBContext dbContext)
		{
			this.dbContext = dbContext;

		}

		public string AddUserDetailsData(UserDetailsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO UserDetails(created_at,last_seen_at,latest_activity_at,moderation_state,moderation_state_changed_at,posts_count,topics_count,updated_at,user_id) VALUES(@created_at,@last_seen_at,@latest_activity_at,@moderation_state,@moderation_state_changed_at,@posts_count,@topics_count,@updated_at,@user_id)";
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@last_seen_at", Data.last_seen_at);
				dbContext.AddInParameter(dbContext.cmd, "@latest_activity_at", Data.latest_activity_at);
				dbContext.AddInParameter(dbContext.cmd, "@moderation_state", Data.moderation_state);
				dbContext.AddInParameter(dbContext.cmd, "@moderation_state_changed_at", Data.moderation_state_changed_at);
				dbContext.AddInParameter(dbContext.cmd, "@posts_count", Data.posts_count);
				dbContext.AddInParameter(dbContext.cmd, "@topics_count", Data.topics_count);
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

		public string DeleteUserDetailsData(UserDetailsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM UserDetails WHERE id = @id";
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

		public UserDetailsModel GetUserDetailsData(UserDetailsModel Data)
		{
			UserDetailsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM UserDetails WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new UserDetailsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    UserDetailsModel userdetails = new UserDetailsModel();
					userdetails.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					userdetails.last_seen_at = dr["last_seen_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["last_seen_at"]);
					userdetails.latest_activity_at = dr["latest_activity_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["latest_activity_at"]);
					userdetails.moderation_state = dr["moderation_state"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["moderation_state"]);
					userdetails.moderation_state_changed_at = dr["moderation_state_changed_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["moderation_state_changed_at"]);
					userdetails.posts_count = dr["posts_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["posts_count"]);
					userdetails.topics_count = dr["topics_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["topics_count"]);
					userdetails.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					userdetails.user_id = dr["user_id"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["user_id"]);

					_return_value = userdetails;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<UserDetailsModel> SelectAllUserDetailsData()
		{
			List < UserDetailsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM UserDetails;";
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<UserDetailsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						UserDetailsModel userdetails = new UserDetailsModel();
					userdetails.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					userdetails.last_seen_at = dr["last_seen_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["last_seen_at"]);
					userdetails.latest_activity_at = dr["latest_activity_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["latest_activity_at"]);
					userdetails.moderation_state = dr["moderation_state"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["moderation_state"]);
					userdetails.moderation_state_changed_at = dr["moderation_state_changed_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["moderation_state_changed_at"]);
					userdetails.posts_count = dr["posts_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["posts_count"]);
					userdetails.topics_count = dr["topics_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["topics_count"]);
					userdetails.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					userdetails.user_id = dr["user_id"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["user_id"]);

						_return_value.Add(userdetails);
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

		public string UpdateUserDetailsData(UserDetailsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "UPDATE UserDetails SET created_at = @created_at,last_seen_at = @last_seen_at,latest_activity_at = @latest_activity_at,moderation_state = @moderation_state,moderation_state_changed_at = @moderation_state_changed_at,posts_count = @posts_count,topics_count = @topics_count,updated_at = @updated_at,user_id = @user_id WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@last_seen_at", Data.last_seen_at);
				dbContext.AddInParameter(dbContext.cmd, "@latest_activity_at", Data.latest_activity_at);
				dbContext.AddInParameter(dbContext.cmd, "@moderation_state", Data.moderation_state);
				dbContext.AddInParameter(dbContext.cmd, "@moderation_state_changed_at", Data.moderation_state_changed_at);
				dbContext.AddInParameter(dbContext.cmd, "@posts_count", Data.posts_count);
				dbContext.AddInParameter(dbContext.cmd, "@topics_count", Data.topics_count);
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
