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
	public class UniversesDAL : BaseDAL
	{

		public UniversesDAL()
		{
		}

		public  UniversesDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteUniversesData(UniversesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Universes` WHERE id = @id";
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

		public UniversesModel GetUniversesData(UniversesModel Data)
		{
			UniversesModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Universes` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new UniversesModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    UniversesModel universes = new UniversesModel();
					universes.archived_at = dr["archived_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["archived_at"]);
					universes.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					universes.deleted_at = dr["deleted_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["deleted_at"]);
					universes.description = dr["description"] == DBNull.Value ? default(String) : GetStringDataFromByteArray((byte[])(dr["description"]));
					universes.favorite = dr["favorite"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["favorite"]);
					universes.genre = dr["genre"] == DBNull.Value ? default(String) : Convert.ToString(dr["genre"]);
					universes.history = dr["history"] == DBNull.Value ? default(String) : GetStringDataFromByteArray((byte[])(dr["history"]));
					universes.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					universes.laws_of_physics = dr["laws_of_physics"] == DBNull.Value ? default(String) : Convert.ToString(dr["laws_of_physics"]);
					universes.magic_system = dr["magic_system"] == DBNull.Value ? default(String) : Convert.ToString(dr["magic_system"]);
					universes.name = dr["name"] == DBNull.Value ? default(String) : Convert.ToString(dr["name"]);
					universes.notes = dr["notes"] == DBNull.Value ? default(String) : GetStringDataFromByteArray((byte[])(dr["notes"]));
					universes.page_type = dr["page_type"] == DBNull.Value ? default(String) : Convert.ToString(dr["page_type"]);
					universes.privacy = dr["privacy"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["privacy"]);
					universes.private_notes = dr["private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["private_notes"]);
					universes.technology = dr["technology"] == DBNull.Value ? default(String) : Convert.ToString(dr["technology"]);
					universes.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					universes.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);

					_return_value = universes;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<UniversesModel> GetAllUniversesForUserID(long userId)
		{
			List<UniversesModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Universes` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<UniversesModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						UniversesModel universes = new UniversesModel();
					universes.archived_at = dr["archived_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["archived_at"]);
					universes.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					universes.deleted_at = dr["deleted_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["deleted_at"]);
					universes.description = dr["description"] == DBNull.Value ? default(String) : GetStringDataFromByteArray((byte[])(dr["description"]));
					universes.favorite = dr["favorite"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["favorite"]);
					universes.genre = dr["genre"] == DBNull.Value ? default(String) : Convert.ToString(dr["genre"]);
					universes.history = dr["history"] == DBNull.Value ? default(String) : GetStringDataFromByteArray((byte[])(dr["history"]));
					universes.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					universes.laws_of_physics = dr["laws_of_physics"] == DBNull.Value ? default(String) : Convert.ToString(dr["laws_of_physics"]);
					universes.magic_system = dr["magic_system"] == DBNull.Value ? default(String) : Convert.ToString(dr["magic_system"]);
					universes.name = dr["name"] == DBNull.Value ? default(String) : Convert.ToString(dr["name"]);
					universes.notes = dr["notes"] == DBNull.Value ? default(String) : GetStringDataFromByteArray((byte[])(dr["notes"]));
					universes.page_type = dr["page_type"] == DBNull.Value ? default(String) : Convert.ToString(dr["page_type"]);
					universes.privacy = dr["privacy"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["privacy"]);
					universes.private_notes = dr["private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["private_notes"]);
					universes.technology = dr["technology"] == DBNull.Value ? default(String) : Convert.ToString(dr["technology"]);
					universes.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					universes.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(universes.id, "universes");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    universes.object_id = first.object_id;
						    universes.object_name = first.object_name;
						}

						_return_value.Add(universes);
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

		public string AddUniversesData(UniversesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Universes`(`archived_at`,`created_at`,`deleted_at`,`description`,`favorite`,`genre`,`history`,`laws_of_physics`,`magic_system`,`name`,`notes`,`page_type`,`privacy`,`private_notes`,`technology`,`updated_at`,`user_id`) VALUES(@archived_at,@created_at,@deleted_at,@description,@favorite,@genre,@history,@laws_of_physics,@magic_system,@name,@notes,@page_type,@privacy,@private_notes,@technology,@updated_at,@user_id)";
				dbContext.AddInParameter(dbContext.cmd, "@archived_at", Data.archived_at);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@deleted_at", Data.deleted_at);
				dbContext.AddInParameter(dbContext.cmd, "@description", Data.description);
				dbContext.AddInParameter(dbContext.cmd, "@favorite", Data.favorite);
				dbContext.AddInParameter(dbContext.cmd, "@genre", Data.genre);
				dbContext.AddInParameter(dbContext.cmd, "@history", Data.history);
				dbContext.AddInParameter(dbContext.cmd, "@laws_of_physics", Data.laws_of_physics);
				dbContext.AddInParameter(dbContext.cmd, "@magic_system", Data.magic_system);
				dbContext.AddInParameter(dbContext.cmd, "@name", Data.name);
				dbContext.AddInParameter(dbContext.cmd, "@notes", Data.notes);
				dbContext.AddInParameter(dbContext.cmd, "@page_type", Data.page_type);
				dbContext.AddInParameter(dbContext.cmd, "@privacy", Data.privacy);
				dbContext.AddInParameter(dbContext.cmd, "@private_notes", Data.private_notes);
				dbContext.AddInParameter(dbContext.cmd, "@technology", Data.technology);
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

		public string UpdateUniversesData(UniversesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "UPDATE universes SET archived_at = @archived_at,created_at = @created_at,deleted_at = @deleted_at,description = @description,favorite = @favorite,genre = @genre,history = @history,laws_of_physics = @laws_of_physics,magic_system = @magic_system,name = @name,notes = @notes,page_type = @page_type,privacy = @privacy,private_notes = @private_notes,technology = @technology,updated_at = @updated_at,user_id = @user_id WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@archived_at", Data.archived_at);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@deleted_at", Data.deleted_at);
				dbContext.AddInParameter(dbContext.cmd, "@description", Data.description);
				dbContext.AddInParameter(dbContext.cmd, "@favorite", Data.favorite);
				dbContext.AddInParameter(dbContext.cmd, "@genre", Data.genre);
				dbContext.AddInParameter(dbContext.cmd, "@history", Data.history);
				dbContext.AddInParameter(dbContext.cmd, "@laws_of_physics", Data.laws_of_physics);
				dbContext.AddInParameter(dbContext.cmd, "@magic_system", Data.magic_system);
				dbContext.AddInParameter(dbContext.cmd, "@name", Data.name);
				dbContext.AddInParameter(dbContext.cmd, "@notes", Data.notes);
				dbContext.AddInParameter(dbContext.cmd, "@page_type", Data.page_type);
				dbContext.AddInParameter(dbContext.cmd, "@privacy", Data.privacy);
				dbContext.AddInParameter(dbContext.cmd, "@private_notes", Data.private_notes);
				dbContext.AddInParameter(dbContext.cmd, "@technology", Data.technology);
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
