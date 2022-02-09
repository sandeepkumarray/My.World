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
	public class ObjectStorageKeysDAL : BaseDAL
	{

		public ObjectStorageKeysDAL()
		{
		}

		public  ObjectStorageKeysDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteObjectStorageKeysData(ObjectStorageKeysModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `object_storage_keys` WHERE id = @id";
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

		public ObjectStorageKeysModel GetObjectStorageKeysData(ObjectStorageKeysModel Data)
		{
			ObjectStorageKeysModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `object_storage_keys` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new ObjectStorageKeysModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    ObjectStorageKeysModel objectstoragekeys = new ObjectStorageKeysModel();
					objectstoragekeys.accessKey = dr["accessKey"] == DBNull.Value ? default(String) : Convert.ToString(dr["accessKey"]);
					objectstoragekeys.bucketName = dr["bucketName"] == DBNull.Value ? default(String) : Convert.ToString(dr["bucketName"]);
					objectstoragekeys.Created_at = dr["Created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["Created_at"]);
					objectstoragekeys.endpoint = dr["endpoint"] == DBNull.Value ? default(String) : Convert.ToString(dr["endpoint"]);
					objectstoragekeys.location = dr["location"] == DBNull.Value ? default(String) : Convert.ToString(dr["location"]);
					objectstoragekeys.secretKey = dr["secretKey"] == DBNull.Value ? default(String) : Convert.ToString(dr["secretKey"]);

					_return_value = objectstoragekeys;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<ObjectStorageKeysModel> GetAllObjectStorageKeysForUserID(long userId)
		{
			List<ObjectStorageKeysModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `object_storage_keys` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<ObjectStorageKeysModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						ObjectStorageKeysModel objectstoragekeys = new ObjectStorageKeysModel();
					objectstoragekeys.accessKey = dr["accessKey"] == DBNull.Value ? default(String) : Convert.ToString(dr["accessKey"]);
					objectstoragekeys.bucketName = dr["bucketName"] == DBNull.Value ? default(String) : Convert.ToString(dr["bucketName"]);
					objectstoragekeys.Created_at = dr["Created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["Created_at"]);
					objectstoragekeys.endpoint = dr["endpoint"] == DBNull.Value ? default(String) : Convert.ToString(dr["endpoint"]);
					objectstoragekeys.location = dr["location"] == DBNull.Value ? default(String) : Convert.ToString(dr["location"]);
					objectstoragekeys.secretKey = dr["secretKey"] == DBNull.Value ? default(String) : Convert.ToString(dr["secretKey"]);

						_return_value.Add(objectstoragekeys);
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

		public string AddObjectStorageKeysData(ObjectStorageKeysModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `object_storage_keys`(`accessKey`,`bucketName`,`Created_at`,`endpoint`,`location`,`secretKey`) VALUES(@accessKey,@bucketName,@Created_at,@endpoint,@location,@secretKey)";
				dbContext.AddInParameter(dbContext.cmd, "@accessKey", Data.accessKey);
				dbContext.AddInParameter(dbContext.cmd, "@bucketName", Data.bucketName);
				dbContext.AddInParameter(dbContext.cmd, "@Created_at", Data.Created_at);
				dbContext.AddInParameter(dbContext.cmd, "@endpoint", Data.endpoint);
				dbContext.AddInParameter(dbContext.cmd, "@location", Data.location);
				dbContext.AddInParameter(dbContext.cmd, "@secretKey", Data.secretKey);
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
