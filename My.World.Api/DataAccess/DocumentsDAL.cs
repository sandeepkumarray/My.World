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
    public class DocumentsDAL : BaseDAL
    {

        public DocumentsDAL()
        {
        }

        public DocumentsDAL(DBContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public string AdddocumentsData(DocumentsModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "INSERT INTO documents(body, title, user_id, folder_id) VALUES(@body,@title,@user_id,@folder_id)";
                dbContext.AddInParameter(dbContext.cmd, "@body", Data.body);
                dbContext.AddInParameter(dbContext.cmd, "@title", Data.title);
                dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
                dbContext.AddInParameter(dbContext.cmd, "@folder_id", Data.folder_id);
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

        public string DeletedocumentsData(DocumentsModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "DELETE FROM documents WHERE id = @id";
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

        public DocumentsModel GetdocumentsData(DocumentsModel Data)
        {
            DocumentsModel _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM documents WHERE id = @id";
                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new DocumentsModel();
                    DataTable dt = ds.Tables[0];

                    DataRow dr = dt.Rows[0];

                    DocumentsModel documents = new DocumentsModel();
                    documents.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);

                    documents.body = dr["body"] == DBNull.Value ? default(String) : Convert.ToString(dr["body"]);
                    documents.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
                    documents.deleted_at = dr["deleted_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["deleted_at"]);
                    documents.favorite = dr["favorite"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["favorite"]);
                    documents.notes_text = dr["notes_text"] == DBNull.Value ? default(String) : Convert.ToString(dr["notes_text"]);
                    documents.privacy = dr["privacy"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["privacy"]);
                    documents.synopsis = dr["synopsis"] == DBNull.Value ? default(String) : Convert.ToString(dr["synopsis"]);
                    documents.title = dr["title"] == DBNull.Value ? default(String) : Convert.ToString(dr["title"]);
                    documents.universe_id = dr["universe_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["universe_id"]);
                    documents.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
                    documents.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
                    documents.cached_word_count = dr["cached_word_count"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["cached_word_count"]);
                    documents.folder_id = dr["folder_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["folder_id"]);

                    _return_value = documents;
                }
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;

        }

        //public string SavedocumentsData(DocumentsModel Data)
        //{
        //    string _return_value = string.Empty;
        //    try
        //    {
        //        dbContext.cmd = new MySqlCommand();
        //        dbContext.cmd.Connection = dbContext.GetConnection();
        //        dbContext.cmd.CommandText = "UPDATE documents SET " + Data.type + " = '" + Data.value + "', updated_at = now() WHERE id = @id";

        //        dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);

        //        _return_value = Convert.ToString(dbContext.cmd.ExecuteNonQuery());
        //    }
        //    catch (Exception ex)
        //    {
        //        _return_value = null;
        //        throw;
        //    }

        //    return _return_value;
        //}

        public List<DocumentsModel> SelectAlldocumentsData(long User_Id)
        {
            List<DocumentsModel> _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM documents WHERE user_id = @id";

                dbContext.AddInParameter(dbContext.cmd, "@id", User_Id);

                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new List<DocumentsModel>();
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        DocumentsModel documents = new DocumentsModel();
                        documents.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
                        documents.body = dr["body"] == DBNull.Value ? default(String) : Convert.ToString(dr["body"]);
                        documents.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
                        documents.deleted_at = dr["deleted_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["deleted_at"]);
                        documents.favorite = dr["favorite"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["favorite"]);
                        documents.notes_text = dr["notes_text"] == DBNull.Value ? default(String) : Convert.ToString(dr["notes_text"]);
                        documents.privacy = dr["privacy"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["privacy"]);
                        documents.synopsis = dr["synopsis"] == DBNull.Value ? default(String) : Convert.ToString(dr["synopsis"]);
                        documents.title = dr["title"] == DBNull.Value ? default(String) : Convert.ToString(dr["title"]);
                        documents.universe_id = dr["universe_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["universe_id"]);
                        documents.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
                        documents.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
                        documents.cached_word_count = dr["cached_word_count"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["cached_word_count"]);
                        documents.folder_id = dr["folder_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["folder_id"]);

                        _return_value.Add(documents);
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

        public List<DocumentsModel> GetAllFolderDocumentsData(long User_Id, long FolderId)
        {
            List<DocumentsModel> _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM documents WHERE user_id = @id and folder_id = @folder_id";

                dbContext.AddInParameter(dbContext.cmd, "@id", User_Id);
                dbContext.AddInParameter(dbContext.cmd, "@folder_id", FolderId);

                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new List<DocumentsModel>();
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        DocumentsModel documents = new DocumentsModel();
                        documents.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
                        documents.body = dr["body"] == DBNull.Value ? default(String) : Convert.ToString(dr["body"]);
                        documents.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
                        documents.deleted_at = dr["deleted_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["deleted_at"]);
                        documents.favorite = dr["favorite"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["favorite"]);
                        documents.notes_text = dr["notes_text"] == DBNull.Value ? default(String) : Convert.ToString(dr["notes_text"]);
                        documents.privacy = dr["privacy"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["privacy"]);
                        documents.synopsis = dr["synopsis"] == DBNull.Value ? default(String) : Convert.ToString(dr["synopsis"]);
                        documents.title = dr["title"] == DBNull.Value ? default(String) : Convert.ToString(dr["title"]);
                        documents.universe_id = dr["universe_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["universe_id"]);
                        documents.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
                        documents.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
                        documents.cached_word_count = dr["cached_word_count"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["cached_word_count"]);
                        documents.folder_id = dr["folder_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["folder_id"]);

                        _return_value.Add(documents);
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

        public string UpdatedocumentsData(DocumentsModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "UPDATE documents SET body = @body,created_at = @created_at,deleted_at = @deleted_at,favorite = @favorite,notes_text = @notes_text,privacy = @privacy,synopsis = @synopsis," +
                    "title = @title,universe_id = @universe_id,updated_at = @updated_at,user_id = @user_id,folder_id = @folder_id,cached_word_count = @cached_word_count  WHERE id = @id";
                dbContext.AddInParameter(dbContext.cmd, "@body", Data.body);
                dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
                dbContext.AddInParameter(dbContext.cmd, "@deleted_at", Data.deleted_at);
                dbContext.AddInParameter(dbContext.cmd, "@favorite", Data.favorite);
                dbContext.AddInParameter(dbContext.cmd, "@notes_text", Data.notes_text);
                dbContext.AddInParameter(dbContext.cmd, "@privacy", Data.privacy);
                dbContext.AddInParameter(dbContext.cmd, "@synopsis", Data.synopsis);
                dbContext.AddInParameter(dbContext.cmd, "@title", Data.title);
                dbContext.AddInParameter(dbContext.cmd, "@universe_id", Data.universe_id);
                dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
                dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
                dbContext.AddInParameter(dbContext.cmd, "@folder_id", Data.folder_id);
                dbContext.AddInParameter(dbContext.cmd, "@cached_word_count", Data.cached_word_count);
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
