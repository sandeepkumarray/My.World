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
	public class ScenesDAL : BaseDAL
	{

		public ScenesDAL()
		{
		}

		public  ScenesDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteScenesData(ScenesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Scenes` WHERE id = @id";
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

		public ScenesModel GetScenesData(ScenesModel Data)
		{
			ScenesModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Scenes` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new ScenesModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    ScenesModel scenes = new ScenesModel();
					scenes.Characters_in_scene = dr["Characters_in_scene"] == DBNull.Value ? default(String) : Convert.ToString(dr["Characters_in_scene"]);
					scenes.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					scenes.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					scenes.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					scenes.Items_in_scene = dr["Items_in_scene"] == DBNull.Value ? default(String) : Convert.ToString(dr["Items_in_scene"]);
					scenes.Locations_in_scene = dr["Locations_in_scene"] == DBNull.Value ? default(String) : Convert.ToString(dr["Locations_in_scene"]);
					scenes.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					scenes.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					scenes.Private_notes = dr["Private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_notes"]);
					scenes.Results = dr["Results"] == DBNull.Value ? default(String) : Convert.ToString(dr["Results"]);
					scenes.Summary = dr["Summary"] == DBNull.Value ? default(String) : Convert.ToString(dr["Summary"]);
					scenes.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					scenes.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					scenes.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					scenes.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					scenes.What_caused_this = dr["What_caused_this"] == DBNull.Value ? default(String) : Convert.ToString(dr["What_caused_this"]);

					_return_value = scenes;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<ScenesModel> GetAllScenesForUserID(long userId)
		{
			List<ScenesModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Scenes` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<ScenesModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						ScenesModel scenes = new ScenesModel();
					scenes.Characters_in_scene = dr["Characters_in_scene"] == DBNull.Value ? default(String) : Convert.ToString(dr["Characters_in_scene"]);
					scenes.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					scenes.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					scenes.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					scenes.Items_in_scene = dr["Items_in_scene"] == DBNull.Value ? default(String) : Convert.ToString(dr["Items_in_scene"]);
					scenes.Locations_in_scene = dr["Locations_in_scene"] == DBNull.Value ? default(String) : Convert.ToString(dr["Locations_in_scene"]);
					scenes.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					scenes.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					scenes.Private_notes = dr["Private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_notes"]);
					scenes.Results = dr["Results"] == DBNull.Value ? default(String) : Convert.ToString(dr["Results"]);
					scenes.Summary = dr["Summary"] == DBNull.Value ? default(String) : Convert.ToString(dr["Summary"]);
					scenes.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					scenes.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					scenes.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					scenes.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					scenes.What_caused_this = dr["What_caused_this"] == DBNull.Value ? default(String) : Convert.ToString(dr["What_caused_this"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(scenes.id, "scenes");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    scenes.object_id = first.object_id;
						    scenes.object_name = first.object_name;
						}

						_return_value.Add(scenes);
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

		public string AddScenesData(ScenesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Scenes`(`Characters_in_scene`,`created_at`,`Description`,`Items_in_scene`,`Locations_in_scene`,`Name`,`Notes`,`Private_notes`,`Results`,`Summary`,`Tags`,`Universe`,`updated_at`,`user_id`,`What_caused_this`) VALUES(@Characters_in_scene,@created_at,@Description,@Items_in_scene,@Locations_in_scene,@Name,@Notes,@Private_notes,@Results,@Summary,@Tags,@Universe,@updated_at,@user_id,@What_caused_this)";
				dbContext.AddInParameter(dbContext.cmd, "@Characters_in_scene", Data.Characters_in_scene);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Items_in_scene", Data.Items_in_scene);
				dbContext.AddInParameter(dbContext.cmd, "@Locations_in_scene", Data.Locations_in_scene);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Private_notes", Data.Private_notes);
				dbContext.AddInParameter(dbContext.cmd, "@Results", Data.Results);
				dbContext.AddInParameter(dbContext.cmd, "@Summary", Data.Summary);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@What_caused_this", Data.What_caused_this);
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
