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
	public class ContentTypesDAL : BaseDAL
	{

		public ContentTypesDAL()
		{
		}

		public  ContentTypesDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteContentTypesData(ContentTypesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `ContentTypes` WHERE id = @id";
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

		public ContentTypesModel GetContentTypesData(ContentTypesModel Data)
		{
			ContentTypesModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `content_types` WHERE name = @name";
				dbContext.AddInParameter(dbContext.cmd, "@name", Data.name);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new ContentTypesModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    ContentTypesModel contenttypes = new ContentTypesModel();
					contenttypes.created_by = dr["created_by"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["created_by"]);
					contenttypes.created_date = dr["created_date"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_date"]);
					contenttypes.icon = dr["icon"] == DBNull.Value ? default(String) : Convert.ToString(dr["icon"]);
					contenttypes.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					contenttypes.name = dr["name"] == DBNull.Value ? default(String) : Convert.ToString(dr["name"]);
					contenttypes.primary_color = dr["primary_color"] == DBNull.Value ? default(String) : Convert.ToString(dr["primary_color"]);
					contenttypes.sec_color = dr["sec_color"] == DBNull.Value ? default(String) : Convert.ToString(dr["sec_color"]);

					_return_value = contenttypes;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<ContentTypesModel> GetAllContentTypesForUserID(long userId)
		{
			List<ContentTypesModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `content_types`;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<ContentTypesModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						ContentTypesModel contenttypes = new ContentTypesModel();
					contenttypes.created_by = dr["created_by"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["created_by"]);
					contenttypes.created_date = dr["created_date"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_date"]);
					contenttypes.icon = dr["icon"] == DBNull.Value ? default(String) : Convert.ToString(dr["icon"]);
					contenttypes.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					contenttypes.name = dr["name"] == DBNull.Value ? default(String) : Convert.ToString(dr["name"]);
					contenttypes.primary_color = dr["primary_color"] == DBNull.Value ? default(String) : Convert.ToString(dr["primary_color"]);
					contenttypes.sec_color = dr["sec_color"] == DBNull.Value ? default(String) : Convert.ToString(dr["sec_color"]);

						_return_value.Add(contenttypes);
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

		public string AddContentTypesData(ContentTypesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `ContentTypes`(`created_by`,`created_date`,`icon`,`name`,`primary_color`,`sec_color`) VALUES(@created_by,@created_date,@icon,@name,@primary_color,@sec_color)";
				dbContext.AddInParameter(dbContext.cmd, "@created_by", Data.created_by);
				dbContext.AddInParameter(dbContext.cmd, "@created_date", Data.created_date);
				dbContext.AddInParameter(dbContext.cmd, "@icon", Data.icon);
				dbContext.AddInParameter(dbContext.cmd, "@name", Data.name);
				dbContext.AddInParameter(dbContext.cmd, "@primary_color", Data.primary_color);
				dbContext.AddInParameter(dbContext.cmd, "@sec_color", Data.sec_color);
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
