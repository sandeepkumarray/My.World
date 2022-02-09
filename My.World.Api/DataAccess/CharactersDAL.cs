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
	public class CharactersDAL : BaseDAL
	{

		public CharactersDAL()
		{
		}

		public  CharactersDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteCharactersData(CharactersModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Characters` WHERE id = @id";
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

		public CharactersModel GetCharactersData(CharactersModel Data)
		{
			CharactersModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Characters` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new CharactersModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    CharactersModel characters = new CharactersModel();
					characters.Age = dr["Age"] == DBNull.Value ? default(String) : Convert.ToString(dr["Age"]);
					characters.Aliases = dr["Aliases"] == DBNull.Value ? default(String) : Convert.ToString(dr["Aliases"]);
					characters.archived_at = dr["archived_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["archived_at"]);
					characters.Background = dr["Background"] == DBNull.Value ? default(String) : Convert.ToString(dr["Background"]);
					characters.Birthday = dr["Birthday"] == DBNull.Value ? default(String) : Convert.ToString(dr["Birthday"]);
					characters.Birthplace = dr["Birthplace"] == DBNull.Value ? default(String) : Convert.ToString(dr["Birthplace"]);
					characters.Bodytype = dr["Bodytype"] == DBNull.Value ? default(String) : Convert.ToString(dr["Bodytype"]);
					characters.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					characters.deleted_at = dr["deleted_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["deleted_at"]);
					characters.Education = dr["Education"] == DBNull.Value ? default(String) : Convert.ToString(dr["Education"]);
					characters.Eyecolor = dr["Eyecolor"] == DBNull.Value ? default(String) : Convert.ToString(dr["Eyecolor"]);
					characters.Facialhair = dr["Facialhair"] == DBNull.Value ? default(String) : Convert.ToString(dr["Facialhair"]);
					characters.Fave_animal = dr["Fave_animal"] == DBNull.Value ? default(String) : Convert.ToString(dr["Fave_animal"]);
					characters.Fave_color = dr["Fave_color"] == DBNull.Value ? default(String) : Convert.ToString(dr["Fave_color"]);
					characters.Fave_food = dr["Fave_food"] == DBNull.Value ? default(String) : Convert.ToString(dr["Fave_food"]);
					characters.Fave_possession = dr["Fave_possession"] == DBNull.Value ? default(String) : Convert.ToString(dr["Fave_possession"]);
					characters.Fave_weapon = dr["Fave_weapon"] == DBNull.Value ? default(String) : Convert.ToString(dr["Fave_weapon"]);
					characters.Favorite = dr["Favorite"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["Favorite"]);
					characters.Flaws = dr["Flaws"] == DBNull.Value ? default(String) : Convert.ToString(dr["Flaws"]);
					characters.Gender = dr["Gender"] == DBNull.Value ? default(String) : Convert.ToString(dr["Gender"]);
					characters.Haircolor = dr["Haircolor"] == DBNull.Value ? default(String) : Convert.ToString(dr["Haircolor"]);
					characters.Hairstyle = dr["Hairstyle"] == DBNull.Value ? default(String) : Convert.ToString(dr["Hairstyle"]);
					characters.Height = dr["Height"] == DBNull.Value ? default(String) : Convert.ToString(dr["Height"]);
					characters.Hobbies = dr["Hobbies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Hobbies"]);
					characters.id = dr["id"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["id"]);
					characters.Identmarks = dr["Identmarks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Identmarks"]);
					characters.Mannerisms = dr["Mannerisms"] == DBNull.Value ? default(String) : Convert.ToString(dr["Mannerisms"]);
					characters.Motivations = dr["Motivations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Motivations"]);
					characters.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					characters.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					characters.Occupation = dr["Occupation"] == DBNull.Value ? default(String) : Convert.ToString(dr["Occupation"]);
					characters.Personality_type = dr["Personality_type"] == DBNull.Value ? default(String) : Convert.ToString(dr["Personality_type"]);
					characters.Pets = dr["Pets"] == DBNull.Value ? default(String) : Convert.ToString(dr["Pets"]);
					characters.Politics = dr["Politics"] == DBNull.Value ? default(String) : Convert.ToString(dr["Politics"]);
					characters.Prejudices = dr["Prejudices"] == DBNull.Value ? default(String) : Convert.ToString(dr["Prejudices"]);
					characters.Privacy = dr["Privacy"] == DBNull.Value ? default(String) : Convert.ToString(dr["Privacy"]);
					characters.Private_notes = dr["Private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_notes"]);
					characters.Race = dr["Race"] == DBNull.Value ? default(String) : Convert.ToString(dr["Race"]);
					characters.Religion = dr["Religion"] == DBNull.Value ? default(String) : Convert.ToString(dr["Religion"]);
					characters.Role = dr["Role"] == DBNull.Value ? default(String) : Convert.ToString(dr["Role"]);
					characters.Skintone = dr["Skintone"] == DBNull.Value ? default(String) : Convert.ToString(dr["Skintone"]);
					characters.Talents = dr["Talents"] == DBNull.Value ? default(String) : Convert.ToString(dr["Talents"]);
					characters.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					characters.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					characters.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					characters.Weight = dr["Weight"] == DBNull.Value ? default(String) : Convert.ToString(dr["Weight"]);

					_return_value = characters;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<CharactersModel> GetAllCharactersForUserID(long userId)
		{
			List<CharactersModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Characters` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<CharactersModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						CharactersModel characters = new CharactersModel();
					characters.Age = dr["Age"] == DBNull.Value ? default(String) : Convert.ToString(dr["Age"]);
					characters.Aliases = dr["Aliases"] == DBNull.Value ? default(String) : Convert.ToString(dr["Aliases"]);
					characters.archived_at = dr["archived_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["archived_at"]);
					characters.Background = dr["Background"] == DBNull.Value ? default(String) : Convert.ToString(dr["Background"]);
					characters.Birthday = dr["Birthday"] == DBNull.Value ? default(String) : Convert.ToString(dr["Birthday"]);
					characters.Birthplace = dr["Birthplace"] == DBNull.Value ? default(String) : Convert.ToString(dr["Birthplace"]);
					characters.Bodytype = dr["Bodytype"] == DBNull.Value ? default(String) : Convert.ToString(dr["Bodytype"]);
					characters.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					characters.deleted_at = dr["deleted_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["deleted_at"]);
					characters.Education = dr["Education"] == DBNull.Value ? default(String) : Convert.ToString(dr["Education"]);
					characters.Eyecolor = dr["Eyecolor"] == DBNull.Value ? default(String) : Convert.ToString(dr["Eyecolor"]);
					characters.Facialhair = dr["Facialhair"] == DBNull.Value ? default(String) : Convert.ToString(dr["Facialhair"]);
					characters.Fave_animal = dr["Fave_animal"] == DBNull.Value ? default(String) : Convert.ToString(dr["Fave_animal"]);
					characters.Fave_color = dr["Fave_color"] == DBNull.Value ? default(String) : Convert.ToString(dr["Fave_color"]);
					characters.Fave_food = dr["Fave_food"] == DBNull.Value ? default(String) : Convert.ToString(dr["Fave_food"]);
					characters.Fave_possession = dr["Fave_possession"] == DBNull.Value ? default(String) : Convert.ToString(dr["Fave_possession"]);
					characters.Fave_weapon = dr["Fave_weapon"] == DBNull.Value ? default(String) : Convert.ToString(dr["Fave_weapon"]);
					characters.Favorite = dr["Favorite"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["Favorite"]);
					characters.Flaws = dr["Flaws"] == DBNull.Value ? default(String) : Convert.ToString(dr["Flaws"]);
					characters.Gender = dr["Gender"] == DBNull.Value ? default(String) : Convert.ToString(dr["Gender"]);
					characters.Haircolor = dr["Haircolor"] == DBNull.Value ? default(String) : Convert.ToString(dr["Haircolor"]);
					characters.Hairstyle = dr["Hairstyle"] == DBNull.Value ? default(String) : Convert.ToString(dr["Hairstyle"]);
					characters.Height = dr["Height"] == DBNull.Value ? default(String) : Convert.ToString(dr["Height"]);
					characters.Hobbies = dr["Hobbies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Hobbies"]);
					characters.id = dr["id"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["id"]);
					characters.Identmarks = dr["Identmarks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Identmarks"]);
					characters.Mannerisms = dr["Mannerisms"] == DBNull.Value ? default(String) : Convert.ToString(dr["Mannerisms"]);
					characters.Motivations = dr["Motivations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Motivations"]);
					characters.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					characters.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					characters.Occupation = dr["Occupation"] == DBNull.Value ? default(String) : Convert.ToString(dr["Occupation"]);
					characters.Personality_type = dr["Personality_type"] == DBNull.Value ? default(String) : Convert.ToString(dr["Personality_type"]);
					characters.Pets = dr["Pets"] == DBNull.Value ? default(String) : Convert.ToString(dr["Pets"]);
					characters.Politics = dr["Politics"] == DBNull.Value ? default(String) : Convert.ToString(dr["Politics"]);
					characters.Prejudices = dr["Prejudices"] == DBNull.Value ? default(String) : Convert.ToString(dr["Prejudices"]);
					characters.Privacy = dr["Privacy"] == DBNull.Value ? default(String) : Convert.ToString(dr["Privacy"]);
					characters.Private_notes = dr["Private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_notes"]);
					characters.Race = dr["Race"] == DBNull.Value ? default(String) : Convert.ToString(dr["Race"]);
					characters.Religion = dr["Religion"] == DBNull.Value ? default(String) : Convert.ToString(dr["Religion"]);
					characters.Role = dr["Role"] == DBNull.Value ? default(String) : Convert.ToString(dr["Role"]);
					characters.Skintone = dr["Skintone"] == DBNull.Value ? default(String) : Convert.ToString(dr["Skintone"]);
					characters.Talents = dr["Talents"] == DBNull.Value ? default(String) : Convert.ToString(dr["Talents"]);
					characters.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					characters.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					characters.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					characters.Weight = dr["Weight"] == DBNull.Value ? default(String) : Convert.ToString(dr["Weight"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(characters.id, "characters");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    characters.object_id = first.object_id;
						    characters.object_name = first.object_name;
						}

						_return_value.Add(characters);
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

		public string AddCharactersData(CharactersModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Characters`(`Age`,`Aliases`,`archived_at`,`Background`,`Birthday`,`Birthplace`,`Bodytype`,`created_at`,`deleted_at`,`Education`,`Eyecolor`,`Facialhair`,`Fave_animal`,`Fave_color`,`Fave_food`,`Fave_possession`,`Fave_weapon`,`Favorite`,`Flaws`,`Gender`,`Haircolor`,`Hairstyle`,`Height`,`Hobbies`,`Identmarks`,`Mannerisms`,`Motivations`,`Name`,`Notes`,`Occupation`,`Personality_type`,`Pets`,`Politics`,`Prejudices`,`Privacy`,`Private_notes`,`Race`,`Religion`,`Role`,`Skintone`,`Talents`,`Universe`,`updated_at`,`user_id`,`Weight`) VALUES(@Age,@Aliases,@archived_at,@Background,@Birthday,@Birthplace,@Bodytype,@created_at,@deleted_at,@Education,@Eyecolor,@Facialhair,@Fave_animal,@Fave_color,@Fave_food,@Fave_possession,@Fave_weapon,@Favorite,@Flaws,@Gender,@Haircolor,@Hairstyle,@Height,@Hobbies,@Identmarks,@Mannerisms,@Motivations,@Name,@Notes,@Occupation,@Personality_type,@Pets,@Politics,@Prejudices,@Privacy,@Private_notes,@Race,@Religion,@Role,@Skintone,@Talents,@Universe,@updated_at,@user_id,@Weight)";
				dbContext.AddInParameter(dbContext.cmd, "@Age", Data.Age);
				dbContext.AddInParameter(dbContext.cmd, "@Aliases", Data.Aliases);
				dbContext.AddInParameter(dbContext.cmd, "@archived_at", Data.archived_at);
				dbContext.AddInParameter(dbContext.cmd, "@Background", Data.Background);
				dbContext.AddInParameter(dbContext.cmd, "@Birthday", Data.Birthday);
				dbContext.AddInParameter(dbContext.cmd, "@Birthplace", Data.Birthplace);
				dbContext.AddInParameter(dbContext.cmd, "@Bodytype", Data.Bodytype);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@deleted_at", Data.deleted_at);
				dbContext.AddInParameter(dbContext.cmd, "@Education", Data.Education);
				dbContext.AddInParameter(dbContext.cmd, "@Eyecolor", Data.Eyecolor);
				dbContext.AddInParameter(dbContext.cmd, "@Facialhair", Data.Facialhair);
				dbContext.AddInParameter(dbContext.cmd, "@Fave_animal", Data.Fave_animal);
				dbContext.AddInParameter(dbContext.cmd, "@Fave_color", Data.Fave_color);
				dbContext.AddInParameter(dbContext.cmd, "@Fave_food", Data.Fave_food);
				dbContext.AddInParameter(dbContext.cmd, "@Fave_possession", Data.Fave_possession);
				dbContext.AddInParameter(dbContext.cmd, "@Fave_weapon", Data.Fave_weapon);
				dbContext.AddInParameter(dbContext.cmd, "@Favorite", Data.Favorite);
				dbContext.AddInParameter(dbContext.cmd, "@Flaws", Data.Flaws);
				dbContext.AddInParameter(dbContext.cmd, "@Gender", Data.Gender);
				dbContext.AddInParameter(dbContext.cmd, "@Haircolor", Data.Haircolor);
				dbContext.AddInParameter(dbContext.cmd, "@Hairstyle", Data.Hairstyle);
				dbContext.AddInParameter(dbContext.cmd, "@Height", Data.Height);
				dbContext.AddInParameter(dbContext.cmd, "@Hobbies", Data.Hobbies);
				dbContext.AddInParameter(dbContext.cmd, "@Identmarks", Data.Identmarks);
				dbContext.AddInParameter(dbContext.cmd, "@Mannerisms", Data.Mannerisms);
				dbContext.AddInParameter(dbContext.cmd, "@Motivations", Data.Motivations);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Occupation", Data.Occupation);
				dbContext.AddInParameter(dbContext.cmd, "@Personality_type", Data.Personality_type);
				dbContext.AddInParameter(dbContext.cmd, "@Pets", Data.Pets);
				dbContext.AddInParameter(dbContext.cmd, "@Politics", Data.Politics);
				dbContext.AddInParameter(dbContext.cmd, "@Prejudices", Data.Prejudices);
				dbContext.AddInParameter(dbContext.cmd, "@Privacy", Data.Privacy);
				dbContext.AddInParameter(dbContext.cmd, "@Private_notes", Data.Private_notes);
				dbContext.AddInParameter(dbContext.cmd, "@Race", Data.Race);
				dbContext.AddInParameter(dbContext.cmd, "@Religion", Data.Religion);
				dbContext.AddInParameter(dbContext.cmd, "@Role", Data.Role);
				dbContext.AddInParameter(dbContext.cmd, "@Skintone", Data.Skintone);
				dbContext.AddInParameter(dbContext.cmd, "@Talents", Data.Talents);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
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
