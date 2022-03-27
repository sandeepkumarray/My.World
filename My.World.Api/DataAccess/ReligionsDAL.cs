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
	public class ReligionsDAL : BaseDAL
	{

		public ReligionsDAL()
		{
		}

		public  ReligionsDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteReligionsData(ReligionsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Religions` WHERE id = @id";
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

		public ReligionsModel GetReligionsData(ReligionsModel Data)
		{
			ReligionsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Religions` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new ReligionsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    ReligionsModel religions = new ReligionsModel();
					religions.Artifacts = dr["Artifacts"] == DBNull.Value ? default(String) : Convert.ToString(dr["Artifacts"]);
					religions.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					religions.Deities = dr["Deities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Deities"]);
					religions.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					religions.Holidays = dr["Holidays"] == DBNull.Value ? default(String) : Convert.ToString(dr["Holidays"]);
					religions.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					religions.Initiation_process = dr["Initiation_process"] == DBNull.Value ? default(String) : Convert.ToString(dr["Initiation_process"]);
					religions.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					religions.Notable_figures = dr["Notable_figures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_figures"]);
					religions.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					religions.Obligations = dr["Obligations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Obligations"]);
					religions.Origin_story = dr["Origin_story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Origin_story"]);
					religions.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					religions.Places_of_worship = dr["Places_of_worship"] == DBNull.Value ? default(String) : Convert.ToString(dr["Places_of_worship"]);
					religions.Practicing_locations = dr["Practicing_locations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Practicing_locations"]);
					religions.Practicing_races = dr["Practicing_races"] == DBNull.Value ? default(String) : Convert.ToString(dr["Practicing_races"]);
					religions.Private_notes = dr["Private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_notes"]);
					religions.Prophecies = dr["Prophecies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Prophecies"]);
					religions.Rituals = dr["Rituals"] == DBNull.Value ? default(String) : Convert.ToString(dr["Rituals"]);
					religions.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					religions.Teachings = dr["Teachings"] == DBNull.Value ? default(String) : Convert.ToString(dr["Teachings"]);
					religions.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					religions.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					religions.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					religions.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					religions.Vision_of_paradise = dr["Vision_of_paradise"] == DBNull.Value ? default(String) : Convert.ToString(dr["Vision_of_paradise"]);
					religions.Worship_services = dr["Worship_services"] == DBNull.Value ? default(String) : Convert.ToString(dr["Worship_services"]);

					_return_value = religions;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<ReligionsModel> GetAllReligionsForUserID(long userId)
		{
			List<ReligionsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Religions` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<ReligionsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						ReligionsModel religions = new ReligionsModel();
					religions.Artifacts = dr["Artifacts"] == DBNull.Value ? default(String) : Convert.ToString(dr["Artifacts"]);
					religions.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					religions.Deities = dr["Deities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Deities"]);
					religions.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					religions.Holidays = dr["Holidays"] == DBNull.Value ? default(String) : Convert.ToString(dr["Holidays"]);
					religions.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					religions.Initiation_process = dr["Initiation_process"] == DBNull.Value ? default(String) : Convert.ToString(dr["Initiation_process"]);
					religions.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					religions.Notable_figures = dr["Notable_figures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_figures"]);
					religions.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					religions.Obligations = dr["Obligations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Obligations"]);
					religions.Origin_story = dr["Origin_story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Origin_story"]);
					religions.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					religions.Places_of_worship = dr["Places_of_worship"] == DBNull.Value ? default(String) : Convert.ToString(dr["Places_of_worship"]);
					religions.Practicing_locations = dr["Practicing_locations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Practicing_locations"]);
					religions.Practicing_races = dr["Practicing_races"] == DBNull.Value ? default(String) : Convert.ToString(dr["Practicing_races"]);
					religions.Private_notes = dr["Private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_notes"]);
					religions.Prophecies = dr["Prophecies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Prophecies"]);
					religions.Rituals = dr["Rituals"] == DBNull.Value ? default(String) : Convert.ToString(dr["Rituals"]);
					religions.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					religions.Teachings = dr["Teachings"] == DBNull.Value ? default(String) : Convert.ToString(dr["Teachings"]);
					religions.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					religions.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					religions.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					religions.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					religions.Vision_of_paradise = dr["Vision_of_paradise"] == DBNull.Value ? default(String) : Convert.ToString(dr["Vision_of_paradise"]);
					religions.Worship_services = dr["Worship_services"] == DBNull.Value ? default(String) : Convert.ToString(dr["Worship_services"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(religions.id, "religions");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    religions.object_id = first.object_id;
						    religions.object_name = first.object_name;
						}

						_return_value.Add(religions);
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

		public string AddReligionsData(ReligionsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Religions`(`Artifacts`,`created_at`,`Deities`,`Description`,`Holidays`,`Initiation_process`,`Name`,`Notable_figures`,`Notes`,`Obligations`,`Origin_story`,`Other_Names`,`Places_of_worship`,`Practicing_locations`,`Practicing_races`,`Private_notes`,`Prophecies`,`Rituals`,`Tags`,`Teachings`,`Traditions`,`Universe`,`updated_at`,`user_id`,`Vision_of_paradise`,`Worship_services`) VALUES(@Artifacts,@created_at,@Deities,@Description,@Holidays,@Initiation_process,@Name,@Notable_figures,@Notes,@Obligations,@Origin_story,@Other_Names,@Places_of_worship,@Practicing_locations,@Practicing_races,@Private_notes,@Prophecies,@Rituals,@Tags,@Teachings,@Traditions,@Universe,@updated_at,@user_id,@Vision_of_paradise,@Worship_services)";
				dbContext.AddInParameter(dbContext.cmd, "@Artifacts", Data.Artifacts);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Deities", Data.Deities);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Holidays", Data.Holidays);
				dbContext.AddInParameter(dbContext.cmd, "@Initiation_process", Data.Initiation_process);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_figures", Data.Notable_figures);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Obligations", Data.Obligations);
				dbContext.AddInParameter(dbContext.cmd, "@Origin_story", Data.Origin_story);
				dbContext.AddInParameter(dbContext.cmd, "@Other_Names", Data.Other_Names);
				dbContext.AddInParameter(dbContext.cmd, "@Places_of_worship", Data.Places_of_worship);
				dbContext.AddInParameter(dbContext.cmd, "@Practicing_locations", Data.Practicing_locations);
				dbContext.AddInParameter(dbContext.cmd, "@Practicing_races", Data.Practicing_races);
				dbContext.AddInParameter(dbContext.cmd, "@Private_notes", Data.Private_notes);
				dbContext.AddInParameter(dbContext.cmd, "@Prophecies", Data.Prophecies);
				dbContext.AddInParameter(dbContext.cmd, "@Rituals", Data.Rituals);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Teachings", Data.Teachings);
				dbContext.AddInParameter(dbContext.cmd, "@Traditions", Data.Traditions);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Vision_of_paradise", Data.Vision_of_paradise);
				dbContext.AddInParameter(dbContext.cmd, "@Worship_services", Data.Worship_services);
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

		public string UpdateReligionsData(ReligionsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "UPDATE religions SET Artifacts = @Artifacts,created_at = @created_at,Deities = @Deities,Description = @Description,Holidays = @Holidays,Initiation_process = @Initiation_process,Name = @Name,Notable_figures = @Notable_figures,Notes = @Notes,Obligations = @Obligations,Origin_story = @Origin_story,Other_Names = @Other_Names,Places_of_worship = @Places_of_worship,Practicing_locations = @Practicing_locations,Practicing_races = @Practicing_races,Private_notes = @Private_notes,Prophecies = @Prophecies,Rituals = @Rituals,Tags = @Tags,Teachings = @Teachings,Traditions = @Traditions,Universe = @Universe,updated_at = @updated_at,user_id = @user_id,Vision_of_paradise = @Vision_of_paradise,Worship_services = @Worship_services WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@Artifacts", Data.Artifacts);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Deities", Data.Deities);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Holidays", Data.Holidays);
				dbContext.AddInParameter(dbContext.cmd, "@Initiation_process", Data.Initiation_process);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_figures", Data.Notable_figures);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Obligations", Data.Obligations);
				dbContext.AddInParameter(dbContext.cmd, "@Origin_story", Data.Origin_story);
				dbContext.AddInParameter(dbContext.cmd, "@Other_Names", Data.Other_Names);
				dbContext.AddInParameter(dbContext.cmd, "@Places_of_worship", Data.Places_of_worship);
				dbContext.AddInParameter(dbContext.cmd, "@Practicing_locations", Data.Practicing_locations);
				dbContext.AddInParameter(dbContext.cmd, "@Practicing_races", Data.Practicing_races);
				dbContext.AddInParameter(dbContext.cmd, "@Private_notes", Data.Private_notes);
				dbContext.AddInParameter(dbContext.cmd, "@Prophecies", Data.Prophecies);
				dbContext.AddInParameter(dbContext.cmd, "@Rituals", Data.Rituals);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Teachings", Data.Teachings);
				dbContext.AddInParameter(dbContext.cmd, "@Traditions", Data.Traditions);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Vision_of_paradise", Data.Vision_of_paradise);
				dbContext.AddInParameter(dbContext.cmd, "@Worship_services", Data.Worship_services);
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
