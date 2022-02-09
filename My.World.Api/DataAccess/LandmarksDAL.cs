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
	public class LandmarksDAL : BaseDAL
	{

		public LandmarksDAL()
		{
		}

		public  LandmarksDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteLandmarksData(LandmarksModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Landmarks` WHERE id = @id";
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

		public LandmarksModel GetLandmarksData(LandmarksModel Data)
		{
			LandmarksModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Landmarks` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new LandmarksModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    LandmarksModel landmarks = new LandmarksModel();
					landmarks.Colors = dr["Colors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Colors"]);
					landmarks.Country = dr["Country"] == DBNull.Value ? default(String) : Convert.ToString(dr["Country"]);
					landmarks.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					landmarks.Creation_story = dr["Creation_story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creation_story"]);
					landmarks.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					landmarks.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					landmarks.Established_year = dr["Established_year"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Established_year"]);
					landmarks.Flora = dr["Flora"] == DBNull.Value ? default(String) : Convert.ToString(dr["Flora"]);
					landmarks.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					landmarks.Materials = dr["Materials"] == DBNull.Value ? default(String) : Convert.ToString(dr["Materials"]);
					landmarks.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					landmarks.Nearby_towns = dr["Nearby_towns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Nearby_towns"]);
					landmarks.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					landmarks.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					landmarks.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					landmarks.Size = dr["Size"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Size"]);
					landmarks.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					landmarks.Type_of_landmark = dr["Type_of_landmark"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_landmark"]);
					landmarks.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					landmarks.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					landmarks.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);

					_return_value = landmarks;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<LandmarksModel> GetAllLandmarksForUserID(long userId)
		{
			List<LandmarksModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Landmarks` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<LandmarksModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						LandmarksModel landmarks = new LandmarksModel();
					landmarks.Colors = dr["Colors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Colors"]);
					landmarks.Country = dr["Country"] == DBNull.Value ? default(String) : Convert.ToString(dr["Country"]);
					landmarks.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					landmarks.Creation_story = dr["Creation_story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creation_story"]);
					landmarks.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					landmarks.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					landmarks.Established_year = dr["Established_year"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Established_year"]);
					landmarks.Flora = dr["Flora"] == DBNull.Value ? default(String) : Convert.ToString(dr["Flora"]);
					landmarks.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					landmarks.Materials = dr["Materials"] == DBNull.Value ? default(String) : Convert.ToString(dr["Materials"]);
					landmarks.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					landmarks.Nearby_towns = dr["Nearby_towns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Nearby_towns"]);
					landmarks.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					landmarks.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					landmarks.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					landmarks.Size = dr["Size"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Size"]);
					landmarks.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					landmarks.Type_of_landmark = dr["Type_of_landmark"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_landmark"]);
					landmarks.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					landmarks.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					landmarks.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(landmarks.id, "landmarks");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    landmarks.object_id = first.object_id;
						    landmarks.object_name = first.object_name;
						}

						_return_value.Add(landmarks);
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

		public string AddLandmarksData(LandmarksModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Landmarks`(`Colors`,`Country`,`created_at`,`Creation_story`,`Creatures`,`Description`,`Established_year`,`Flora`,`Materials`,`Name`,`Nearby_towns`,`Notes`,`Other_Names`,`Private_Notes`,`Size`,`Tags`,`Type_of_landmark`,`Universe`,`updated_at`,`user_id`) VALUES(@Colors,@Country,@created_at,@Creation_story,@Creatures,@Description,@Established_year,@Flora,@Materials,@Name,@Nearby_towns,@Notes,@Other_Names,@Private_Notes,@Size,@Tags,@Type_of_landmark,@Universe,@updated_at,@user_id)";
				dbContext.AddInParameter(dbContext.cmd, "@Colors", Data.Colors);
				dbContext.AddInParameter(dbContext.cmd, "@Country", Data.Country);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Creation_story", Data.Creation_story);
				dbContext.AddInParameter(dbContext.cmd, "@Creatures", Data.Creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Established_year", Data.Established_year);
				dbContext.AddInParameter(dbContext.cmd, "@Flora", Data.Flora);
				dbContext.AddInParameter(dbContext.cmd, "@Materials", Data.Materials);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Nearby_towns", Data.Nearby_towns);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Other_Names", Data.Other_Names);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Size", Data.Size);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Type_of_landmark", Data.Type_of_landmark);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
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

	}
}
