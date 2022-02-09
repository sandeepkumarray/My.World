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
	public class ConditionsDAL : BaseDAL
	{

		public ConditionsDAL()
		{
		}

		public  ConditionsDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteConditionsData(ConditionsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Conditions` WHERE id = @id";
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

		public ConditionsModel GetConditionsData(ConditionsModel Data)
		{
			ConditionsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Conditions` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new ConditionsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    ConditionsModel conditions = new ConditionsModel();
					conditions.Alternate_names = dr["Alternate_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Alternate_names"]);
					conditions.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					conditions.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					conditions.Diagnostic_method = dr["Diagnostic_method"] == DBNull.Value ? default(String) : Convert.ToString(dr["Diagnostic_method"]);
					conditions.Duration = dr["Duration"] == DBNull.Value ? default(String) : Convert.ToString(dr["Duration"]);
					conditions.Environmental_factors = dr["Environmental_factors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Environmental_factors"]);
					conditions.Epidemiology = dr["Epidemiology"] == DBNull.Value ? default(String) : Convert.ToString(dr["Epidemiology"]);
					conditions.Evolution = dr["Evolution"] == DBNull.Value ? default(String) : Convert.ToString(dr["Evolution"]);
					conditions.Genetic_factors = dr["Genetic_factors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Genetic_factors"]);
					conditions.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					conditions.Immunization = dr["Immunization"] == DBNull.Value ? default(String) : Convert.ToString(dr["Immunization"]);
					conditions.Lifestyle_factors = dr["Lifestyle_factors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Lifestyle_factors"]);
					conditions.Medication = dr["Medication"] == DBNull.Value ? default(String) : Convert.ToString(dr["Medication"]);
					conditions.Mental_effects = dr["Mental_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Mental_effects"]);
					conditions.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					conditions.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					conditions.Origin = dr["Origin"] == DBNull.Value ? default(String) : Convert.ToString(dr["Origin"]);
					conditions.Prevention = dr["Prevention"] == DBNull.Value ? default(String) : Convert.ToString(dr["Prevention"]);
					conditions.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					conditions.Prognosis = dr["Prognosis"] == DBNull.Value ? default(String) : Convert.ToString(dr["Prognosis"]);
					conditions.Rarity = dr["Rarity"] == DBNull.Value ? default(String) : Convert.ToString(dr["Rarity"]);
					conditions.Specialty_Field = dr["Specialty_Field"] == DBNull.Value ? default(String) : Convert.ToString(dr["Specialty_Field"]);
					conditions.Symbolism = dr["Symbolism"] == DBNull.Value ? default(String) : Convert.ToString(dr["Symbolism"]);
					conditions.Symptoms = dr["Symptoms"] == DBNull.Value ? default(String) : Convert.ToString(dr["Symptoms"]);
					conditions.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					conditions.Transmission = dr["Transmission"] == DBNull.Value ? default(String) : Convert.ToString(dr["Transmission"]);
					conditions.Treatment = dr["Treatment"] == DBNull.Value ? default(String) : Convert.ToString(dr["Treatment"]);
					conditions.Type_of_condition = dr["Type_of_condition"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_condition"]);
					conditions.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					conditions.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					conditions.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					conditions.Variations = dr["Variations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Variations"]);
					conditions.Visual_effects = dr["Visual_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Visual_effects"]);

					_return_value = conditions;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<ConditionsModel> GetAllConditionsForUserID(long userId)
		{
			List<ConditionsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Conditions` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<ConditionsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						ConditionsModel conditions = new ConditionsModel();
					conditions.Alternate_names = dr["Alternate_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Alternate_names"]);
					conditions.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					conditions.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					conditions.Diagnostic_method = dr["Diagnostic_method"] == DBNull.Value ? default(String) : Convert.ToString(dr["Diagnostic_method"]);
					conditions.Duration = dr["Duration"] == DBNull.Value ? default(String) : Convert.ToString(dr["Duration"]);
					conditions.Environmental_factors = dr["Environmental_factors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Environmental_factors"]);
					conditions.Epidemiology = dr["Epidemiology"] == DBNull.Value ? default(String) : Convert.ToString(dr["Epidemiology"]);
					conditions.Evolution = dr["Evolution"] == DBNull.Value ? default(String) : Convert.ToString(dr["Evolution"]);
					conditions.Genetic_factors = dr["Genetic_factors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Genetic_factors"]);
					conditions.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					conditions.Immunization = dr["Immunization"] == DBNull.Value ? default(String) : Convert.ToString(dr["Immunization"]);
					conditions.Lifestyle_factors = dr["Lifestyle_factors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Lifestyle_factors"]);
					conditions.Medication = dr["Medication"] == DBNull.Value ? default(String) : Convert.ToString(dr["Medication"]);
					conditions.Mental_effects = dr["Mental_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Mental_effects"]);
					conditions.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					conditions.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					conditions.Origin = dr["Origin"] == DBNull.Value ? default(String) : Convert.ToString(dr["Origin"]);
					conditions.Prevention = dr["Prevention"] == DBNull.Value ? default(String) : Convert.ToString(dr["Prevention"]);
					conditions.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					conditions.Prognosis = dr["Prognosis"] == DBNull.Value ? default(String) : Convert.ToString(dr["Prognosis"]);
					conditions.Rarity = dr["Rarity"] == DBNull.Value ? default(String) : Convert.ToString(dr["Rarity"]);
					conditions.Specialty_Field = dr["Specialty_Field"] == DBNull.Value ? default(String) : Convert.ToString(dr["Specialty_Field"]);
					conditions.Symbolism = dr["Symbolism"] == DBNull.Value ? default(String) : Convert.ToString(dr["Symbolism"]);
					conditions.Symptoms = dr["Symptoms"] == DBNull.Value ? default(String) : Convert.ToString(dr["Symptoms"]);
					conditions.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					conditions.Transmission = dr["Transmission"] == DBNull.Value ? default(String) : Convert.ToString(dr["Transmission"]);
					conditions.Treatment = dr["Treatment"] == DBNull.Value ? default(String) : Convert.ToString(dr["Treatment"]);
					conditions.Type_of_condition = dr["Type_of_condition"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_condition"]);
					conditions.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					conditions.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					conditions.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					conditions.Variations = dr["Variations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Variations"]);
					conditions.Visual_effects = dr["Visual_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Visual_effects"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(conditions.id, "conditions");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    conditions.object_id = first.object_id;
						    conditions.object_name = first.object_name;
						}

						_return_value.Add(conditions);
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

		public string AddConditionsData(ConditionsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Conditions`(`Alternate_names`,`created_at`,`Description`,`Diagnostic_method`,`Duration`,`Environmental_factors`,`Epidemiology`,`Evolution`,`Genetic_factors`,`Immunization`,`Lifestyle_factors`,`Medication`,`Mental_effects`,`Name`,`Notes`,`Origin`,`Prevention`,`Private_Notes`,`Prognosis`,`Rarity`,`Specialty_Field`,`Symbolism`,`Symptoms`,`Tags`,`Transmission`,`Treatment`,`Type_of_condition`,`Universe`,`updated_at`,`user_id`,`Variations`,`Visual_effects`) VALUES(@Alternate_names,@created_at,@Description,@Diagnostic_method,@Duration,@Environmental_factors,@Epidemiology,@Evolution,@Genetic_factors,@Immunization,@Lifestyle_factors,@Medication,@Mental_effects,@Name,@Notes,@Origin,@Prevention,@Private_Notes,@Prognosis,@Rarity,@Specialty_Field,@Symbolism,@Symptoms,@Tags,@Transmission,@Treatment,@Type_of_condition,@Universe,@updated_at,@user_id,@Variations,@Visual_effects)";
				dbContext.AddInParameter(dbContext.cmd, "@Alternate_names", Data.Alternate_names);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Diagnostic_method", Data.Diagnostic_method);
				dbContext.AddInParameter(dbContext.cmd, "@Duration", Data.Duration);
				dbContext.AddInParameter(dbContext.cmd, "@Environmental_factors", Data.Environmental_factors);
				dbContext.AddInParameter(dbContext.cmd, "@Epidemiology", Data.Epidemiology);
				dbContext.AddInParameter(dbContext.cmd, "@Evolution", Data.Evolution);
				dbContext.AddInParameter(dbContext.cmd, "@Genetic_factors", Data.Genetic_factors);
				dbContext.AddInParameter(dbContext.cmd, "@Immunization", Data.Immunization);
				dbContext.AddInParameter(dbContext.cmd, "@Lifestyle_factors", Data.Lifestyle_factors);
				dbContext.AddInParameter(dbContext.cmd, "@Medication", Data.Medication);
				dbContext.AddInParameter(dbContext.cmd, "@Mental_effects", Data.Mental_effects);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Origin", Data.Origin);
				dbContext.AddInParameter(dbContext.cmd, "@Prevention", Data.Prevention);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Prognosis", Data.Prognosis);
				dbContext.AddInParameter(dbContext.cmd, "@Rarity", Data.Rarity);
				dbContext.AddInParameter(dbContext.cmd, "@Specialty_Field", Data.Specialty_Field);
				dbContext.AddInParameter(dbContext.cmd, "@Symbolism", Data.Symbolism);
				dbContext.AddInParameter(dbContext.cmd, "@Symptoms", Data.Symptoms);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Transmission", Data.Transmission);
				dbContext.AddInParameter(dbContext.cmd, "@Treatment", Data.Treatment);
				dbContext.AddInParameter(dbContext.cmd, "@Type_of_condition", Data.Type_of_condition);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Variations", Data.Variations);
				dbContext.AddInParameter(dbContext.cmd, "@Visual_effects", Data.Visual_effects);
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
