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
	public class MagicsDAL : BaseDAL
	{

		public MagicsDAL()
		{
		}

		public  MagicsDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteMagicsData(MagicsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Magics` WHERE id = @id";
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

		public MagicsModel GetMagicsData(MagicsModel Data)
		{
			MagicsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Magics` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new MagicsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    MagicsModel magics = new MagicsModel();
					magics.Aftereffects = dr["Aftereffects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Aftereffects"]);
					magics.Conditions = dr["Conditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Conditions"]);
					magics.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					magics.Deities = dr["Deities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Deities"]);
					magics.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					magics.Education = dr["Education"] == DBNull.Value ? default(String) : Convert.ToString(dr["Education"]);
					magics.Effects = dr["Effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Effects"]);
					magics.Element = dr["Element"] == DBNull.Value ? default(String) : Convert.ToString(dr["Element"]);
					magics.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					magics.Limitations = dr["Limitations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Limitations"]);
					magics.Materials_required = dr["Materials_required"] == DBNull.Value ? default(String) : Convert.ToString(dr["Materials_required"]);
					magics.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					magics.Negative_effects = dr["Negative_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Negative_effects"]);
					magics.Neutral_effects = dr["Neutral_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Neutral_effects"]);
					magics.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					magics.Positive_effects = dr["Positive_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Positive_effects"]);
					magics.Private_notes = dr["Private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_notes"]);
					magics.Resource_costs = dr["Resource_costs"] == DBNull.Value ? default(String) : Convert.ToString(dr["Resource_costs"]);
					magics.Scale = dr["Scale"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Scale"]);
					magics.Skills_required = dr["Skills_required"] == DBNull.Value ? default(String) : Convert.ToString(dr["Skills_required"]);
					magics.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					magics.Type_of_magic = dr["Type_of_magic"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_magic"]);
					magics.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					magics.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					magics.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					magics.Visuals = dr["Visuals"] == DBNull.Value ? default(String) : Convert.ToString(dr["Visuals"]);

					_return_value = magics;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<MagicsModel> GetAllMagicsForUserID(long userId)
		{
			List<MagicsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Magics` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<MagicsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						MagicsModel magics = new MagicsModel();
					magics.Aftereffects = dr["Aftereffects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Aftereffects"]);
					magics.Conditions = dr["Conditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Conditions"]);
					magics.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					magics.Deities = dr["Deities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Deities"]);
					magics.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					magics.Education = dr["Education"] == DBNull.Value ? default(String) : Convert.ToString(dr["Education"]);
					magics.Effects = dr["Effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Effects"]);
					magics.Element = dr["Element"] == DBNull.Value ? default(String) : Convert.ToString(dr["Element"]);
					magics.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					magics.Limitations = dr["Limitations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Limitations"]);
					magics.Materials_required = dr["Materials_required"] == DBNull.Value ? default(String) : Convert.ToString(dr["Materials_required"]);
					magics.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					magics.Negative_effects = dr["Negative_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Negative_effects"]);
					magics.Neutral_effects = dr["Neutral_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Neutral_effects"]);
					magics.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					magics.Positive_effects = dr["Positive_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Positive_effects"]);
					magics.Private_notes = dr["Private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_notes"]);
					magics.Resource_costs = dr["Resource_costs"] == DBNull.Value ? default(String) : Convert.ToString(dr["Resource_costs"]);
					magics.Scale = dr["Scale"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Scale"]);
					magics.Skills_required = dr["Skills_required"] == DBNull.Value ? default(String) : Convert.ToString(dr["Skills_required"]);
					magics.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					magics.Type_of_magic = dr["Type_of_magic"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_magic"]);
					magics.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					magics.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					magics.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					magics.Visuals = dr["Visuals"] == DBNull.Value ? default(String) : Convert.ToString(dr["Visuals"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(magics.id, "magics");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    magics.object_id = first.object_id;
						    magics.object_name = first.object_name;
						}

						_return_value.Add(magics);
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

		public string AddMagicsData(MagicsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Magics`(`Aftereffects`,`Conditions`,`created_at`,`Deities`,`Description`,`Education`,`Effects`,`Element`,`Limitations`,`Materials_required`,`Name`,`Negative_effects`,`Neutral_effects`,`Notes`,`Positive_effects`,`Private_notes`,`Resource_costs`,`Scale`,`Skills_required`,`Tags`,`Type_of_magic`,`Universe`,`updated_at`,`user_id`,`Visuals`) VALUES(@Aftereffects,@Conditions,@created_at,@Deities,@Description,@Education,@Effects,@Element,@Limitations,@Materials_required,@Name,@Negative_effects,@Neutral_effects,@Notes,@Positive_effects,@Private_notes,@Resource_costs,@Scale,@Skills_required,@Tags,@Type_of_magic,@Universe,@updated_at,@user_id,@Visuals)";
				dbContext.AddInParameter(dbContext.cmd, "@Aftereffects", Data.Aftereffects);
				dbContext.AddInParameter(dbContext.cmd, "@Conditions", Data.Conditions);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Deities", Data.Deities);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Education", Data.Education);
				dbContext.AddInParameter(dbContext.cmd, "@Effects", Data.Effects);
				dbContext.AddInParameter(dbContext.cmd, "@Element", Data.Element);
				dbContext.AddInParameter(dbContext.cmd, "@Limitations", Data.Limitations);
				dbContext.AddInParameter(dbContext.cmd, "@Materials_required", Data.Materials_required);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Negative_effects", Data.Negative_effects);
				dbContext.AddInParameter(dbContext.cmd, "@Neutral_effects", Data.Neutral_effects);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Positive_effects", Data.Positive_effects);
				dbContext.AddInParameter(dbContext.cmd, "@Private_notes", Data.Private_notes);
				dbContext.AddInParameter(dbContext.cmd, "@Resource_costs", Data.Resource_costs);
				dbContext.AddInParameter(dbContext.cmd, "@Scale", Data.Scale);
				dbContext.AddInParameter(dbContext.cmd, "@Skills_required", Data.Skills_required);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Type_of_magic", Data.Type_of_magic);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Visuals", Data.Visuals);
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

		public string UpdateMagicsData(MagicsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "UPDATE magics SET Aftereffects = @Aftereffects,Conditions = @Conditions,created_at = @created_at,Deities = @Deities,Description = @Description,Education = @Education,Effects = @Effects,Element = @Element,Limitations = @Limitations,Materials_required = @Materials_required,Name = @Name,Negative_effects = @Negative_effects,Neutral_effects = @Neutral_effects,Notes = @Notes,Positive_effects = @Positive_effects,Private_notes = @Private_notes,Resource_costs = @Resource_costs,Scale = @Scale,Skills_required = @Skills_required,Tags = @Tags,Type_of_magic = @Type_of_magic,Universe = @Universe,updated_at = @updated_at,user_id = @user_id,Visuals = @Visuals WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@Aftereffects", Data.Aftereffects);
				dbContext.AddInParameter(dbContext.cmd, "@Conditions", Data.Conditions);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Deities", Data.Deities);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Education", Data.Education);
				dbContext.AddInParameter(dbContext.cmd, "@Effects", Data.Effects);
				dbContext.AddInParameter(dbContext.cmd, "@Element", Data.Element);
				dbContext.AddInParameter(dbContext.cmd, "@Limitations", Data.Limitations);
				dbContext.AddInParameter(dbContext.cmd, "@Materials_required", Data.Materials_required);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Negative_effects", Data.Negative_effects);
				dbContext.AddInParameter(dbContext.cmd, "@Neutral_effects", Data.Neutral_effects);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Positive_effects", Data.Positive_effects);
				dbContext.AddInParameter(dbContext.cmd, "@Private_notes", Data.Private_notes);
				dbContext.AddInParameter(dbContext.cmd, "@Resource_costs", Data.Resource_costs);
				dbContext.AddInParameter(dbContext.cmd, "@Scale", Data.Scale);
				dbContext.AddInParameter(dbContext.cmd, "@Skills_required", Data.Skills_required);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Type_of_magic", Data.Type_of_magic);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Visuals", Data.Visuals);
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
