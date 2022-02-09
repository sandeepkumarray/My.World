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
	public class LanguagesDAL : BaseDAL
	{

		public LanguagesDAL()
		{
		}

		public  LanguagesDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteLanguagesData(LanguagesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Languages` WHERE id = @id";
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

		public LanguagesModel GetLanguagesData(LanguagesModel Data)
		{
			LanguagesModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Languages` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new LanguagesModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    LanguagesModel languages = new LanguagesModel();
					languages.Body_parts = dr["Body_parts"] == DBNull.Value ? default(String) : Convert.ToString(dr["Body_parts"]);
					languages.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					languages.Determiners = dr["Determiners"] == DBNull.Value ? default(String) : Convert.ToString(dr["Determiners"]);
					languages.Dialectical_information = dr["Dialectical_information"] == DBNull.Value ? default(String) : Convert.ToString(dr["Dialectical_information"]);
					languages.Evolution = dr["Evolution"] == DBNull.Value ? default(String) : Convert.ToString(dr["Evolution"]);
					languages.Family = dr["Family"] == DBNull.Value ? default(String) : Convert.ToString(dr["Family"]);
					languages.Gestures = dr["Gestures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Gestures"]);
					languages.Goodbyes = dr["Goodbyes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Goodbyes"]);
					languages.Grammar = dr["Grammar"] == DBNull.Value ? default(String) : Convert.ToString(dr["Grammar"]);
					languages.Greetings = dr["Greetings"] == DBNull.Value ? default(String) : Convert.ToString(dr["Greetings"]);
					languages.History = dr["History"] == DBNull.Value ? default(String) : Convert.ToString(dr["History"]);
					languages.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					languages.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					languages.No_words = dr["No_words"] == DBNull.Value ? default(String) : Convert.ToString(dr["No_words"]);
					languages.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					languages.Numbers = dr["Numbers"] == DBNull.Value ? default(String) : Convert.ToString(dr["Numbers"]);
					languages.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					languages.Phonology = dr["Phonology"] == DBNull.Value ? default(String) : Convert.ToString(dr["Phonology"]);
					languages.Please = dr["Please"] == DBNull.Value ? default(String) : Convert.ToString(dr["Please"]);
					languages.Private_notes = dr["Private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_notes"]);
					languages.Pronouns = dr["Pronouns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Pronouns"]);
					languages.Quantifiers = dr["Quantifiers"] == DBNull.Value ? default(String) : Convert.ToString(dr["Quantifiers"]);
					languages.Register = dr["Register"] == DBNull.Value ? default(String) : Convert.ToString(dr["Register"]);
					languages.Sorry = dr["Sorry"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sorry"]);
					languages.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					languages.Thank_you = dr["Thank_you"] == DBNull.Value ? default(String) : Convert.ToString(dr["Thank_you"]);
					languages.Trade = dr["Trade"] == DBNull.Value ? default(String) : Convert.ToString(dr["Trade"]);
					languages.Typology = dr["Typology"] == DBNull.Value ? default(String) : Convert.ToString(dr["Typology"]);
					languages.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					languages.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					languages.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					languages.Yes_words = dr["Yes_words"] == DBNull.Value ? default(String) : Convert.ToString(dr["Yes_words"]);
					languages.You_are_welcome = dr["You_are_welcome"] == DBNull.Value ? default(String) : Convert.ToString(dr["You_are_welcome"]);

					_return_value = languages;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<LanguagesModel> GetAllLanguagesForUserID(long userId)
		{
			List<LanguagesModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Languages` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<LanguagesModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						LanguagesModel languages = new LanguagesModel();
					languages.Body_parts = dr["Body_parts"] == DBNull.Value ? default(String) : Convert.ToString(dr["Body_parts"]);
					languages.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					languages.Determiners = dr["Determiners"] == DBNull.Value ? default(String) : Convert.ToString(dr["Determiners"]);
					languages.Dialectical_information = dr["Dialectical_information"] == DBNull.Value ? default(String) : Convert.ToString(dr["Dialectical_information"]);
					languages.Evolution = dr["Evolution"] == DBNull.Value ? default(String) : Convert.ToString(dr["Evolution"]);
					languages.Family = dr["Family"] == DBNull.Value ? default(String) : Convert.ToString(dr["Family"]);
					languages.Gestures = dr["Gestures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Gestures"]);
					languages.Goodbyes = dr["Goodbyes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Goodbyes"]);
					languages.Grammar = dr["Grammar"] == DBNull.Value ? default(String) : Convert.ToString(dr["Grammar"]);
					languages.Greetings = dr["Greetings"] == DBNull.Value ? default(String) : Convert.ToString(dr["Greetings"]);
					languages.History = dr["History"] == DBNull.Value ? default(String) : Convert.ToString(dr["History"]);
					languages.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					languages.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					languages.No_words = dr["No_words"] == DBNull.Value ? default(String) : Convert.ToString(dr["No_words"]);
					languages.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					languages.Numbers = dr["Numbers"] == DBNull.Value ? default(String) : Convert.ToString(dr["Numbers"]);
					languages.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					languages.Phonology = dr["Phonology"] == DBNull.Value ? default(String) : Convert.ToString(dr["Phonology"]);
					languages.Please = dr["Please"] == DBNull.Value ? default(String) : Convert.ToString(dr["Please"]);
					languages.Private_notes = dr["Private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_notes"]);
					languages.Pronouns = dr["Pronouns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Pronouns"]);
					languages.Quantifiers = dr["Quantifiers"] == DBNull.Value ? default(String) : Convert.ToString(dr["Quantifiers"]);
					languages.Register = dr["Register"] == DBNull.Value ? default(String) : Convert.ToString(dr["Register"]);
					languages.Sorry = dr["Sorry"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sorry"]);
					languages.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					languages.Thank_you = dr["Thank_you"] == DBNull.Value ? default(String) : Convert.ToString(dr["Thank_you"]);
					languages.Trade = dr["Trade"] == DBNull.Value ? default(String) : Convert.ToString(dr["Trade"]);
					languages.Typology = dr["Typology"] == DBNull.Value ? default(String) : Convert.ToString(dr["Typology"]);
					languages.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					languages.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					languages.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					languages.Yes_words = dr["Yes_words"] == DBNull.Value ? default(String) : Convert.ToString(dr["Yes_words"]);
					languages.You_are_welcome = dr["You_are_welcome"] == DBNull.Value ? default(String) : Convert.ToString(dr["You_are_welcome"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(languages.id, "languages");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    languages.object_id = first.object_id;
						    languages.object_name = first.object_name;
						}

						_return_value.Add(languages);
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

		public string AddLanguagesData(LanguagesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Languages`(`Body_parts`,`created_at`,`Determiners`,`Dialectical_information`,`Evolution`,`Family`,`Gestures`,`Goodbyes`,`Grammar`,`Greetings`,`History`,`Name`,`No_words`,`Notes`,`Numbers`,`Other_Names`,`Phonology`,`Please`,`Private_notes`,`Pronouns`,`Quantifiers`,`Register`,`Sorry`,`Tags`,`Thank_you`,`Trade`,`Typology`,`Universe`,`updated_at`,`user_id`,`Yes_words`,`You_are_welcome`) VALUES(@Body_parts,@created_at,@Determiners,@Dialectical_information,@Evolution,@Family,@Gestures,@Goodbyes,@Grammar,@Greetings,@History,@Name,@No_words,@Notes,@Numbers,@Other_Names,@Phonology,@Please,@Private_notes,@Pronouns,@Quantifiers,@Register,@Sorry,@Tags,@Thank_you,@Trade,@Typology,@Universe,@updated_at,@user_id,@Yes_words,@You_are_welcome)";
				dbContext.AddInParameter(dbContext.cmd, "@Body_parts", Data.Body_parts);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Determiners", Data.Determiners);
				dbContext.AddInParameter(dbContext.cmd, "@Dialectical_information", Data.Dialectical_information);
				dbContext.AddInParameter(dbContext.cmd, "@Evolution", Data.Evolution);
				dbContext.AddInParameter(dbContext.cmd, "@Family", Data.Family);
				dbContext.AddInParameter(dbContext.cmd, "@Gestures", Data.Gestures);
				dbContext.AddInParameter(dbContext.cmd, "@Goodbyes", Data.Goodbyes);
				dbContext.AddInParameter(dbContext.cmd, "@Grammar", Data.Grammar);
				dbContext.AddInParameter(dbContext.cmd, "@Greetings", Data.Greetings);
				dbContext.AddInParameter(dbContext.cmd, "@History", Data.History);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@No_words", Data.No_words);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Numbers", Data.Numbers);
				dbContext.AddInParameter(dbContext.cmd, "@Other_Names", Data.Other_Names);
				dbContext.AddInParameter(dbContext.cmd, "@Phonology", Data.Phonology);
				dbContext.AddInParameter(dbContext.cmd, "@Please", Data.Please);
				dbContext.AddInParameter(dbContext.cmd, "@Private_notes", Data.Private_notes);
				dbContext.AddInParameter(dbContext.cmd, "@Pronouns", Data.Pronouns);
				dbContext.AddInParameter(dbContext.cmd, "@Quantifiers", Data.Quantifiers);
				dbContext.AddInParameter(dbContext.cmd, "@Register", Data.Register);
				dbContext.AddInParameter(dbContext.cmd, "@Sorry", Data.Sorry);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Thank_you", Data.Thank_you);
				dbContext.AddInParameter(dbContext.cmd, "@Trade", Data.Trade);
				dbContext.AddInParameter(dbContext.cmd, "@Typology", Data.Typology);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Yes_words", Data.Yes_words);
				dbContext.AddInParameter(dbContext.cmd, "@You_are_welcome", Data.You_are_welcome);
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
