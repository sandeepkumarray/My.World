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
	public class UserContentBucketDAL : BaseDAL
	{

		public UserContentBucketDAL()
		{
		}

		public  UserContentBucketDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteUserContentBucketData(UserContentBucketModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `user_content_bucket` WHERE id = @id";
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

		public UserContentBucketModel GetUserContentBucketData(UserContentBucketModel Data)
		{
			UserContentBucketModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `user_content_bucket` WHERE user_id = @user_id";
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new UserContentBucketModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    UserContentBucketModel usercontentbucket = new UserContentBucketModel();
					usercontentbucket.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					usercontentbucket.bucket_Name = dr["bucket_Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["bucket_Name"]);
					usercontentbucket.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					usercontentbucket.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					usercontentbucket.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);

					_return_value = usercontentbucket;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<UserContentBucketModel> GetAllUserContentBucketForUserID(long userId)
		{
			List<UserContentBucketModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `user_content_bucket` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<UserContentBucketModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						UserContentBucketModel usercontentbucket = new UserContentBucketModel();
					usercontentbucket.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					usercontentbucket.bucket_Name = dr["bucket_Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["bucket_Name"]);
					usercontentbucket.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					usercontentbucket.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					usercontentbucket.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);

						_return_value.Add(usercontentbucket);
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

		public string AddUserContentBucketData(UserContentBucketModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `user_content_bucket`(`bucket_Name`,`user_id`,`Universe`,`created_at`) VALUES(@bucket_Name,@user_id,@Universe,@created_at)";
				dbContext.AddInParameter(dbContext.cmd, "@bucket_Name", Data.bucket_Name);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
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
