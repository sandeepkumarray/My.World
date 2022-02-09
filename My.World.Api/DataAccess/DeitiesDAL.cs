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
	public class DeitiesDAL : BaseDAL
	{

		public DeitiesDAL()
		{
		}

		public  DeitiesDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteDeitiesData(DeitiesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Deities` WHERE id = @id";
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

		public DeitiesModel GetDeitiesData(DeitiesModel Data)
		{
			DeitiesModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Deities` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new DeitiesModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    DeitiesModel deities = new DeitiesModel();
					deities.Abilities = dr["Abilities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Abilities"]);
					deities.Children = dr["Children"] == DBNull.Value ? default(String) : Convert.ToString(dr["Children"]);
					deities.Conditions = dr["Conditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Conditions"]);
					deities.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					deities.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					deities.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					deities.Elements = dr["Elements"] == DBNull.Value ? default(String) : Convert.ToString(dr["Elements"]);
					deities.Family_History = dr["Family_History"] == DBNull.Value ? default(String) : Convert.ToString(dr["Family_History"]);
					deities.Floras = dr["Floras"] == DBNull.Value ? default(String) : Convert.ToString(dr["Floras"]);
					deities.Height = dr["Height"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Height"]);
					deities.Human_Interaction = dr["Human_Interaction"] == DBNull.Value ? default(String) : Convert.ToString(dr["Human_Interaction"]);
					deities.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					deities.Life_Story = dr["Life_Story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Life_Story"]);
					deities.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					deities.Notable_Events = dr["Notable_Events"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_Events"]);
					deities.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					deities.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					deities.Parents = dr["Parents"] == DBNull.Value ? default(String) : Convert.ToString(dr["Parents"]);
					deities.Partners = dr["Partners"] == DBNull.Value ? default(String) : Convert.ToString(dr["Partners"]);
					deities.Physical_Description = dr["Physical_Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Physical_Description"]);
					deities.Prayers = dr["Prayers"] == DBNull.Value ? default(String) : Convert.ToString(dr["Prayers"]);
					deities.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					deities.Related_landmarks = dr["Related_landmarks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Related_landmarks"]);
					deities.Related_races = dr["Related_races"] == DBNull.Value ? default(String) : Convert.ToString(dr["Related_races"]);
					deities.Related_towns = dr["Related_towns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Related_towns"]);
					deities.Relics = dr["Relics"] == DBNull.Value ? default(String) : Convert.ToString(dr["Relics"]);
					deities.Religions = dr["Religions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Religions"]);
					deities.Rituals = dr["Rituals"] == DBNull.Value ? default(String) : Convert.ToString(dr["Rituals"]);
					deities.Siblings = dr["Siblings"] == DBNull.Value ? default(String) : Convert.ToString(dr["Siblings"]);
					deities.Strengths = dr["Strengths"] == DBNull.Value ? default(String) : Convert.ToString(dr["Strengths"]);
					deities.Symbols = dr["Symbols"] == DBNull.Value ? default(String) : Convert.ToString(dr["Symbols"]);
					deities.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					deities.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					deities.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					deities.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					deities.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					deities.Weaknesses = dr["Weaknesses"] == DBNull.Value ? default(String) : Convert.ToString(dr["Weaknesses"]);
					deities.Weight = dr["Weight"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Weight"]);

					_return_value = deities;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<DeitiesModel> GetAllDeitiesForUserID(long userId)
		{
			List<DeitiesModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Deities` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<DeitiesModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						DeitiesModel deities = new DeitiesModel();
					deities.Abilities = dr["Abilities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Abilities"]);
					deities.Children = dr["Children"] == DBNull.Value ? default(String) : Convert.ToString(dr["Children"]);
					deities.Conditions = dr["Conditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Conditions"]);
					deities.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					deities.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					deities.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					deities.Elements = dr["Elements"] == DBNull.Value ? default(String) : Convert.ToString(dr["Elements"]);
					deities.Family_History = dr["Family_History"] == DBNull.Value ? default(String) : Convert.ToString(dr["Family_History"]);
					deities.Floras = dr["Floras"] == DBNull.Value ? default(String) : Convert.ToString(dr["Floras"]);
					deities.Height = dr["Height"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Height"]);
					deities.Human_Interaction = dr["Human_Interaction"] == DBNull.Value ? default(String) : Convert.ToString(dr["Human_Interaction"]);
					deities.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					deities.Life_Story = dr["Life_Story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Life_Story"]);
					deities.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					deities.Notable_Events = dr["Notable_Events"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_Events"]);
					deities.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					deities.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					deities.Parents = dr["Parents"] == DBNull.Value ? default(String) : Convert.ToString(dr["Parents"]);
					deities.Partners = dr["Partners"] == DBNull.Value ? default(String) : Convert.ToString(dr["Partners"]);
					deities.Physical_Description = dr["Physical_Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Physical_Description"]);
					deities.Prayers = dr["Prayers"] == DBNull.Value ? default(String) : Convert.ToString(dr["Prayers"]);
					deities.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					deities.Related_landmarks = dr["Related_landmarks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Related_landmarks"]);
					deities.Related_races = dr["Related_races"] == DBNull.Value ? default(String) : Convert.ToString(dr["Related_races"]);
					deities.Related_towns = dr["Related_towns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Related_towns"]);
					deities.Relics = dr["Relics"] == DBNull.Value ? default(String) : Convert.ToString(dr["Relics"]);
					deities.Religions = dr["Religions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Religions"]);
					deities.Rituals = dr["Rituals"] == DBNull.Value ? default(String) : Convert.ToString(dr["Rituals"]);
					deities.Siblings = dr["Siblings"] == DBNull.Value ? default(String) : Convert.ToString(dr["Siblings"]);
					deities.Strengths = dr["Strengths"] == DBNull.Value ? default(String) : Convert.ToString(dr["Strengths"]);
					deities.Symbols = dr["Symbols"] == DBNull.Value ? default(String) : Convert.ToString(dr["Symbols"]);
					deities.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					deities.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					deities.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					deities.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					deities.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					deities.Weaknesses = dr["Weaknesses"] == DBNull.Value ? default(String) : Convert.ToString(dr["Weaknesses"]);
					deities.Weight = dr["Weight"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Weight"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(deities.id, "deities");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    deities.object_id = first.object_id;
						    deities.object_name = first.object_name;
						}

						_return_value.Add(deities);
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

		public string AddDeitiesData(DeitiesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Deities`(`Abilities`,`Children`,`Conditions`,`created_at`,`Creatures`,`Description`,`Elements`,`Family_History`,`Floras`,`Height`,`Human_Interaction`,`Life_Story`,`Name`,`Notable_Events`,`Notes`,`Other_Names`,`Parents`,`Partners`,`Physical_Description`,`Prayers`,`Private_Notes`,`Related_landmarks`,`Related_races`,`Related_towns`,`Relics`,`Religions`,`Rituals`,`Siblings`,`Strengths`,`Symbols`,`Tags`,`Traditions`,`Universe`,`updated_at`,`user_id`,`Weaknesses`,`Weight`) VALUES(@Abilities,@Children,@Conditions,@created_at,@Creatures,@Description,@Elements,@Family_History,@Floras,@Height,@Human_Interaction,@Life_Story,@Name,@Notable_Events,@Notes,@Other_Names,@Parents,@Partners,@Physical_Description,@Prayers,@Private_Notes,@Related_landmarks,@Related_races,@Related_towns,@Relics,@Religions,@Rituals,@Siblings,@Strengths,@Symbols,@Tags,@Traditions,@Universe,@updated_at,@user_id,@Weaknesses,@Weight)";
				dbContext.AddInParameter(dbContext.cmd, "@Abilities", Data.Abilities);
				dbContext.AddInParameter(dbContext.cmd, "@Children", Data.Children);
				dbContext.AddInParameter(dbContext.cmd, "@Conditions", Data.Conditions);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Creatures", Data.Creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Elements", Data.Elements);
				dbContext.AddInParameter(dbContext.cmd, "@Family_History", Data.Family_History);
				dbContext.AddInParameter(dbContext.cmd, "@Floras", Data.Floras);
				dbContext.AddInParameter(dbContext.cmd, "@Height", Data.Height);
				dbContext.AddInParameter(dbContext.cmd, "@Human_Interaction", Data.Human_Interaction);
				dbContext.AddInParameter(dbContext.cmd, "@Life_Story", Data.Life_Story);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_Events", Data.Notable_Events);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Other_Names", Data.Other_Names);
				dbContext.AddInParameter(dbContext.cmd, "@Parents", Data.Parents);
				dbContext.AddInParameter(dbContext.cmd, "@Partners", Data.Partners);
				dbContext.AddInParameter(dbContext.cmd, "@Physical_Description", Data.Physical_Description);
				dbContext.AddInParameter(dbContext.cmd, "@Prayers", Data.Prayers);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Related_landmarks", Data.Related_landmarks);
				dbContext.AddInParameter(dbContext.cmd, "@Related_races", Data.Related_races);
				dbContext.AddInParameter(dbContext.cmd, "@Related_towns", Data.Related_towns);
				dbContext.AddInParameter(dbContext.cmd, "@Relics", Data.Relics);
				dbContext.AddInParameter(dbContext.cmd, "@Religions", Data.Religions);
				dbContext.AddInParameter(dbContext.cmd, "@Rituals", Data.Rituals);
				dbContext.AddInParameter(dbContext.cmd, "@Siblings", Data.Siblings);
				dbContext.AddInParameter(dbContext.cmd, "@Strengths", Data.Strengths);
				dbContext.AddInParameter(dbContext.cmd, "@Symbols", Data.Symbols);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Traditions", Data.Traditions);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Weaknesses", Data.Weaknesses);
				dbContext.AddInParameter(dbContext.cmd, "@Weight", Data.Weight);
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
