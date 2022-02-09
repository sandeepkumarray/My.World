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
	public class ContentObjectAttachmentDAL : BaseDAL
	{

		public ContentObjectAttachmentDAL()
		{
		}

		public  ContentObjectAttachmentDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteContentObjectAttachmentData(ContentObjectAttachmentModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `content_object_attachment` WHERE object_id = @object_id";
				dbContext.AddInParameter(dbContext.cmd, "@object_id", Data.object_id);
				_return_value = Convert.ToString(dbContext.cmd.ExecuteNonQuery());
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public ContentObjectAttachmentModel GetContentObjectAttachmentData(ContentObjectAttachmentModel Data)
		{
			ContentObjectAttachmentModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `content_object_attachment` WHERE object_id = @object_id";
				dbContext.AddInParameter(dbContext.cmd, "@object_id", Data.object_id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new ContentObjectAttachmentModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    ContentObjectAttachmentModel contentobjectattachment = new ContentObjectAttachmentModel();
					contentobjectattachment.object_id = dr["object_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["object_id"]);
					contentobjectattachment.content_type = dr["content_type"] == DBNull.Value ? default(String) : Convert.ToString(dr["content_type"]);
					contentobjectattachment.content_id = dr["content_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["content_id"]);

					_return_value = contentobjectattachment;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<ContentObjectAttachmentModel> GetAllContentObjectAttachmentForUserID(long userId)
		{
			List<ContentObjectAttachmentModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `content_object_attachment` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<ContentObjectAttachmentModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						ContentObjectAttachmentModel contentobjectattachment = new ContentObjectAttachmentModel();
					contentobjectattachment.object_id = dr["object_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["object_id"]);
					contentobjectattachment.content_type = dr["content_type"] == DBNull.Value ? default(String) : Convert.ToString(dr["content_type"]);
					contentobjectattachment.content_id = dr["content_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["content_id"]);

						_return_value.Add(contentobjectattachment);
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

		public string AddContentObjectAttachmentData(ContentObjectAttachmentModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `content_object_attachment`(`object_id`,`content_type`,`content_id`) VALUES(@object_id,@content_type,@content_id)";
				dbContext.AddInParameter(dbContext.cmd, "@object_id", Data.object_id);
				dbContext.AddInParameter(dbContext.cmd, "@content_type", Data.content_type);
				dbContext.AddInParameter(dbContext.cmd, "@content_id", Data.content_id);
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
