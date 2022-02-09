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
    public class ContentObjectDAL : BaseDAL
    {

        public ContentObjectDAL()
        {
        }

        public ContentObjectDAL(DBContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public string DeleteContentObjectData(ContentObjectModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "DELETE FROM `content_object` WHERE object_id = @object_id";
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

        public ContentObjectModel GetContentObjectData(ContentObjectModel Data)
        {
            ContentObjectModel _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM `content_object` WHERE object_id = @object_id";
                dbContext.AddInParameter(dbContext.cmd, "@object_id", Data.object_id);
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new ContentObjectModel();
                    DataTable dt = ds.Tables[0];

                    DataRow dr = dt.Rows[0];

                    ContentObjectModel contentobject = new ContentObjectModel();
                    contentobject.object_id = dr["object_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["object_id"]);
                    contentobject.object_name = dr["object_name"] == DBNull.Value ? default(String) : Convert.ToString(dr["object_name"]);
                    contentobject.object_type = dr["object_type"] == DBNull.Value ? default(String) : Convert.ToString(dr["object_type"]);
                    contentobject.object_size = dr["object_size"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["object_size"]);
                    contentobject.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);

                    _return_value = contentobject;
                }
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;

        }

        public bool CheckIfExistContentObjectData(ContentObjectModel Data)
        {
            bool _return_value = false;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM `content_object` WHERE object_name = @object_name";
                dbContext.AddInParameter(dbContext.cmd, "@object_name", Data.object_name);
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                    {
                        _return_value = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _return_value;

        }

        public List<ContentObjectModel> GetAllContentObjectForUserID(long userId)
        {
            List<ContentObjectModel> _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM `content_object` where user_id = @user_id;";
                dbContext.AddInParameter(dbContext.cmd, "@user_id", userId);
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new List<ContentObjectModel>();
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        ContentObjectModel contentobject = new ContentObjectModel();
                        contentobject.object_id = dr["object_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["object_id"]);
                        contentobject.object_name = dr["object_name"] == DBNull.Value ? default(String) : Convert.ToString(dr["object_name"]);
                        contentobject.object_type = dr["object_type"] == DBNull.Value ? default(String) : Convert.ToString(dr["object_type"]);
                        contentobject.object_size = dr["object_size"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["object_size"]);
                        contentobject.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);

                        _return_value.Add(contentobject);
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

        public string AddContentObjectData(ContentObjectModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                if (CheckIfExistContentObjectData(Data) == false)
                {
                    dbContext.cmd = new MySqlCommand();
                    dbContext.cmd.Connection = dbContext.GetConnection();
                    dbContext.cmd.CommandText = "INSERT INTO `content_object`(`object_id`,`object_name`,`object_type`,`object_size`,`created_at`) VALUES(@object_id,@object_name,@object_type,@object_size,@created_at)";
                    dbContext.AddInParameter(dbContext.cmd, "@object_id", Data.object_id);
                    dbContext.AddInParameter(dbContext.cmd, "@object_name", Data.object_name);
                    dbContext.AddInParameter(dbContext.cmd, "@object_type", Data.object_type);
                    dbContext.AddInParameter(dbContext.cmd, "@object_size", Data.object_size);
                    dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
                    dbContext.cmd.ExecuteNonQuery();
                    _return_value = Convert.ToString(dbContext.cmd.LastInsertedId);
                }
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;

        }

        public List<ContentObjectModel> GetAllContentObjectAttachments(long content_id, string content_type)
        {
            List<ContentObjectModel> _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM `content_object_attachment` coa " +
                                            " JOIN `content_object` co on coa.object_id = co.object_id" +
                                            " WHERE coa.content_id = @content_id AND coa.content_type = @content_type;";
                dbContext.AddInParameter(dbContext.cmd, "@content_id", content_id);
                dbContext.AddInParameter(dbContext.cmd, "@content_type", content_type);
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);

                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new List<ContentObjectModel>();
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        ContentObjectModel contentobject = new ContentObjectModel();
                        contentobject.object_id = dr["object_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["object_id"]);
                        contentobject.object_name = dr["object_name"] == DBNull.Value ? default(String) : Convert.ToString(dr["object_name"]);
                        contentobject.object_type = dr["object_type"] == DBNull.Value ? default(String) : Convert.ToString(dr["object_type"]);
                        contentobject.object_size = dr["object_size"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["object_size"]);
                        contentobject.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);

                        _return_value.Add(contentobject);
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

    }
}
