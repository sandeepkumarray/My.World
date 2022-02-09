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
    public class FoldersDAL : BaseDAL
    {

        public FoldersDAL()
        {
        }

        public FoldersDAL(DBContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public string AddfoldersData(FoldersModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "INSERT INTO folders(context,created_at,id,parent_folder_id,title,updated_at,user_id) VALUES(@context,@created_at,@id,@parent_folder_id,@title,@updated_at,@user_id)";
                dbContext.AddInParameter(dbContext.cmd, "@context", Data.context);
                dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
                dbContext.AddInParameter(dbContext.cmd, "@parent_folder_id", Data.parent_folder_id);
                dbContext.AddInParameter(dbContext.cmd, "@title", Data.title);
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

        public string DeletefoldersData(FoldersModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "DELETE FROM folders WHERE id = @id";
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

        public FoldersModel GetfoldersData(FoldersModel Data)
        {
            FoldersModel _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM folders WHERE id = @id";
                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new FoldersModel();
                    DataTable dt = ds.Tables[0];

                    DataRow dr = dt.Rows[0];

                    FoldersModel folders = new FoldersModel();
                    folders.context = dr["context"] == DBNull.Value ? default(String) : Convert.ToString(dr["context"]);
                    folders.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
                    folders.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
                    folders.parent_folder_id = dr["parent_folder_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["parent_folder_id"]);
                    folders.title = dr["title"] == DBNull.Value ? default(String) : Convert.ToString(dr["title"]);
                    folders.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
                    folders.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);

                    _return_value = folders;
                }
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;

        }

        internal List<FoldersModel> GetAllChildFoldersData(long folderId)
        {

            List<FoldersModel> _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM folders where parent_folder_id = @parent_folder_id";

                dbContext.AddInParameter(dbContext.cmd, "@parent_folder_id", folderId);
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new List<FoldersModel>();
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        FoldersModel folders = new FoldersModel();
                        folders.context = dr["context"] == DBNull.Value ? default(String) : Convert.ToString(dr["context"]);
                        folders.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
                        folders.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
                        folders.parent_folder_id = dr["parent_folder_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["parent_folder_id"]);
                        folders.title = dr["title"] == DBNull.Value ? default(String) : Convert.ToString(dr["title"]);
                        folders.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
                        folders.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);

                        _return_value.Add(folders);
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

        public List<FoldersModel> SelectAllfoldersData(long User_Id)
        {
            List<FoldersModel> _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM folders WHERE parent_folder_id is null or parent_folder_id <=0 and user_id = @id";

                dbContext.AddInParameter(dbContext.cmd, "@id", User_Id);
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new List<FoldersModel>();
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        FoldersModel folders = new FoldersModel();
                        folders.context = dr["context"] == DBNull.Value ? default(String) : Convert.ToString(dr["context"]);
                        folders.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
                        folders.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
                        folders.parent_folder_id = dr["parent_folder_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["parent_folder_id"]);
                        folders.title = dr["title"] == DBNull.Value ? default(String) : Convert.ToString(dr["title"]);
                        folders.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
                        folders.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);

                        _return_value.Add(folders);
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

        public List<FoldersModel> GetEligibleParentFoldersData(long User_Id, long FolderId)
        {
            List<FoldersModel> _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM `folders` WHERE id != @folderid and (`parent_folder_id` != @folderid or `parent_folder_id` is null) and user_id = @id";

                dbContext.AddInParameter(dbContext.cmd, "@id", User_Id);
                dbContext.AddInParameter(dbContext.cmd, "@folderid", FolderId);
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new List<FoldersModel>();
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        FoldersModel folders = new FoldersModel();
                        folders.context = dr["context"] == DBNull.Value ? default(String) : Convert.ToString(dr["context"]);
                        folders.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
                        folders.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
                        folders.parent_folder_id = dr["parent_folder_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["parent_folder_id"]);
                        folders.title = dr["title"] == DBNull.Value ? default(String) : Convert.ToString(dr["title"]);
                        folders.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
                        folders.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);

                        _return_value.Add(folders);
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

        public string UpdatefoldersData(FoldersModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "UPDATE folders SET parent_folder_id = @parent_folder_id,title = @title,updated_at = NOW() WHERE id = @id";
                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
                dbContext.AddInParameter(dbContext.cmd, "@parent_folder_id", Data.parent_folder_id);
                dbContext.AddInParameter(dbContext.cmd, "@title", Data.title);
                dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
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
