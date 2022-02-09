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
	public class LoresDAL : BaseDAL
	{

		public LoresDAL()
		{
		}

		public  LoresDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteLoresData(LoresModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Lores` WHERE id = @id";
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

		public LoresModel GetLoresData(LoresModel Data)
		{
			LoresModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Lores` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new LoresModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    LoresModel lores = new LoresModel();
					lores.Background_information = dr["Background_information"] == DBNull.Value ? default(String) : Convert.ToString(dr["Background_information"]);
					lores.Believability = dr["Believability"] == DBNull.Value ? default(String) : Convert.ToString(dr["Believability"]);
					lores.Believers = dr["Believers"] == DBNull.Value ? default(String) : Convert.ToString(dr["Believers"]);
					lores.Buildings = dr["Buildings"] == DBNull.Value ? default(String) : Convert.ToString(dr["Buildings"]);
					lores.Characters = dr["Characters"] == DBNull.Value ? default(String) : Convert.ToString(dr["Characters"]);
					lores.Conditions = dr["Conditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Conditions"]);
					lores.Continents = dr["Continents"] == DBNull.Value ? default(String) : Convert.ToString(dr["Continents"]);
					lores.Countries = dr["Countries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Countries"]);
					lores.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					lores.Created_phrases = dr["Created_phrases"] == DBNull.Value ? default(String) : Convert.ToString(dr["Created_phrases"]);
					lores.Created_traditions = dr["Created_traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Created_traditions"]);
					lores.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					lores.Criticism = dr["Criticism"] == DBNull.Value ? default(String) : Convert.ToString(dr["Criticism"]);
					lores.Date_recorded = dr["Date_recorded"] == DBNull.Value ? default(String) : Convert.ToString(dr["Date_recorded"]);
					lores.Deities = dr["Deities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Deities"]);
					lores.Dialect = dr["Dialect"] == DBNull.Value ? default(String) : Convert.ToString(dr["Dialect"]);
					lores.Evolution_over_time = dr["Evolution_over_time"] == DBNull.Value ? default(String) : Convert.ToString(dr["Evolution_over_time"]);
					lores.False_parts = dr["False_parts"] == DBNull.Value ? default(String) : Convert.ToString(dr["False_parts"]);
					lores.Floras = dr["Floras"] == DBNull.Value ? default(String) : Convert.ToString(dr["Floras"]);
					lores.Foods = dr["Foods"] == DBNull.Value ? default(String) : Convert.ToString(dr["Foods"]);
					lores.Full_text = dr["Full_text"] == DBNull.Value ? default(String) : Convert.ToString(dr["Full_text"]);
					lores.Genre = dr["Genre"] == DBNull.Value ? default(String) : Convert.ToString(dr["Genre"]);
					lores.Geographical_variations = dr["Geographical_variations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Geographical_variations"]);
					lores.Governments = dr["Governments"] == DBNull.Value ? default(String) : Convert.ToString(dr["Governments"]);
					lores.Groups = dr["Groups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Groups"]);
					lores.Historical_context = dr["Historical_context"] == DBNull.Value ? default(String) : Convert.ToString(dr["Historical_context"]);
					lores.Hoaxes = dr["Hoaxes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Hoaxes"]);
					lores.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					lores.Impact = dr["Impact"] == DBNull.Value ? default(String) : Convert.ToString(dr["Impact"]);
					lores.Important_translations = dr["Important_translations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Important_translations"]);
					lores.Influence_on_modern_times = dr["Influence_on_modern_times"] == DBNull.Value ? default(String) : Convert.ToString(dr["Influence_on_modern_times"]);
					lores.Inspirations = dr["Inspirations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Inspirations"]);
					lores.Interpretations = dr["Interpretations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Interpretations"]);
					lores.Jobs = dr["Jobs"] == DBNull.Value ? default(String) : Convert.ToString(dr["Jobs"]);
					lores.Landmarks = dr["Landmarks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Landmarks"]);
					lores.Magic = dr["Magic"] == DBNull.Value ? default(String) : Convert.ToString(dr["Magic"]);
					lores.Media_adaptations = dr["Media_adaptations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Media_adaptations"]);
					lores.Morals = dr["Morals"] == DBNull.Value ? default(String) : Convert.ToString(dr["Morals"]);
					lores.Motivations = dr["Motivations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Motivations"]);
					lores.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					lores.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					lores.Original_author = dr["Original_author"] == DBNull.Value ? default(String) : Convert.ToString(dr["Original_author"]);
					lores.Original_languages = dr["Original_languages"] == DBNull.Value ? default(String) : Convert.ToString(dr["Original_languages"]);
					lores.Original_telling = dr["Original_telling"] == DBNull.Value ? default(String) : Convert.ToString(dr["Original_telling"]);
					lores.Planets = dr["Planets"] == DBNull.Value ? default(String) : Convert.ToString(dr["Planets"]);
					lores.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					lores.Propagation_method = dr["Propagation_method"] == DBNull.Value ? default(String) : Convert.ToString(dr["Propagation_method"]);
					lores.Races = dr["Races"] == DBNull.Value ? default(String) : Convert.ToString(dr["Races"]);
					lores.Reception = dr["Reception"] == DBNull.Value ? default(String) : Convert.ToString(dr["Reception"]);
					lores.Related_lores = dr["Related_lores"] == DBNull.Value ? default(String) : Convert.ToString(dr["Related_lores"]);
					lores.Religions = dr["Religions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Religions"]);
					lores.Schools = dr["Schools"] == DBNull.Value ? default(String) : Convert.ToString(dr["Schools"]);
					lores.Source = dr["Source"] == DBNull.Value ? default(String) : Convert.ToString(dr["Source"]);
					lores.Sports = dr["Sports"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sports"]);
					lores.Structure = dr["Structure"] == DBNull.Value ? default(String) : Convert.ToString(dr["Structure"]);
					lores.Subjects = dr["Subjects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Subjects"]);
					lores.Summary = dr["Summary"] == DBNull.Value ? default(String) : Convert.ToString(dr["Summary"]);
					lores.Symbolisms = dr["Symbolisms"] == DBNull.Value ? default(String) : Convert.ToString(dr["Symbolisms"]);
					lores.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					lores.Technologies = dr["Technologies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Technologies"]);
					lores.Time_period = dr["Time_period"] == DBNull.Value ? default(String) : Convert.ToString(dr["Time_period"]);
					lores.Tone = dr["Tone"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tone"]);
					lores.Towns = dr["Towns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Towns"]);
					lores.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					lores.Translation_variations = dr["Translation_variations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Translation_variations"]);
					lores.True_parts = dr["True_parts"] == DBNull.Value ? default(String) : Convert.ToString(dr["True_parts"]);
					lores.Type = dr["Type"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type"]);
					lores.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					lores.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					lores.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					lores.Variations = dr["Variations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Variations"]);
					lores.Vehicles = dr["Vehicles"] == DBNull.Value ? default(String) : Convert.ToString(dr["Vehicles"]);

					_return_value = lores;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<LoresModel> GetAllLoresForUserID(long userId)
		{
			List<LoresModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Lores` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<LoresModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						LoresModel lores = new LoresModel();
					lores.Background_information = dr["Background_information"] == DBNull.Value ? default(String) : Convert.ToString(dr["Background_information"]);
					lores.Believability = dr["Believability"] == DBNull.Value ? default(String) : Convert.ToString(dr["Believability"]);
					lores.Believers = dr["Believers"] == DBNull.Value ? default(String) : Convert.ToString(dr["Believers"]);
					lores.Buildings = dr["Buildings"] == DBNull.Value ? default(String) : Convert.ToString(dr["Buildings"]);
					lores.Characters = dr["Characters"] == DBNull.Value ? default(String) : Convert.ToString(dr["Characters"]);
					lores.Conditions = dr["Conditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Conditions"]);
					lores.Continents = dr["Continents"] == DBNull.Value ? default(String) : Convert.ToString(dr["Continents"]);
					lores.Countries = dr["Countries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Countries"]);
					lores.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					lores.Created_phrases = dr["Created_phrases"] == DBNull.Value ? default(String) : Convert.ToString(dr["Created_phrases"]);
					lores.Created_traditions = dr["Created_traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Created_traditions"]);
					lores.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					lores.Criticism = dr["Criticism"] == DBNull.Value ? default(String) : Convert.ToString(dr["Criticism"]);
					lores.Date_recorded = dr["Date_recorded"] == DBNull.Value ? default(String) : Convert.ToString(dr["Date_recorded"]);
					lores.Deities = dr["Deities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Deities"]);
					lores.Dialect = dr["Dialect"] == DBNull.Value ? default(String) : Convert.ToString(dr["Dialect"]);
					lores.Evolution_over_time = dr["Evolution_over_time"] == DBNull.Value ? default(String) : Convert.ToString(dr["Evolution_over_time"]);
					lores.False_parts = dr["False_parts"] == DBNull.Value ? default(String) : Convert.ToString(dr["False_parts"]);
					lores.Floras = dr["Floras"] == DBNull.Value ? default(String) : Convert.ToString(dr["Floras"]);
					lores.Foods = dr["Foods"] == DBNull.Value ? default(String) : Convert.ToString(dr["Foods"]);
					lores.Full_text = dr["Full_text"] == DBNull.Value ? default(String) : Convert.ToString(dr["Full_text"]);
					lores.Genre = dr["Genre"] == DBNull.Value ? default(String) : Convert.ToString(dr["Genre"]);
					lores.Geographical_variations = dr["Geographical_variations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Geographical_variations"]);
					lores.Governments = dr["Governments"] == DBNull.Value ? default(String) : Convert.ToString(dr["Governments"]);
					lores.Groups = dr["Groups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Groups"]);
					lores.Historical_context = dr["Historical_context"] == DBNull.Value ? default(String) : Convert.ToString(dr["Historical_context"]);
					lores.Hoaxes = dr["Hoaxes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Hoaxes"]);
					lores.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					lores.Impact = dr["Impact"] == DBNull.Value ? default(String) : Convert.ToString(dr["Impact"]);
					lores.Important_translations = dr["Important_translations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Important_translations"]);
					lores.Influence_on_modern_times = dr["Influence_on_modern_times"] == DBNull.Value ? default(String) : Convert.ToString(dr["Influence_on_modern_times"]);
					lores.Inspirations = dr["Inspirations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Inspirations"]);
					lores.Interpretations = dr["Interpretations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Interpretations"]);
					lores.Jobs = dr["Jobs"] == DBNull.Value ? default(String) : Convert.ToString(dr["Jobs"]);
					lores.Landmarks = dr["Landmarks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Landmarks"]);
					lores.Magic = dr["Magic"] == DBNull.Value ? default(String) : Convert.ToString(dr["Magic"]);
					lores.Media_adaptations = dr["Media_adaptations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Media_adaptations"]);
					lores.Morals = dr["Morals"] == DBNull.Value ? default(String) : Convert.ToString(dr["Morals"]);
					lores.Motivations = dr["Motivations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Motivations"]);
					lores.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					lores.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					lores.Original_author = dr["Original_author"] == DBNull.Value ? default(String) : Convert.ToString(dr["Original_author"]);
					lores.Original_languages = dr["Original_languages"] == DBNull.Value ? default(String) : Convert.ToString(dr["Original_languages"]);
					lores.Original_telling = dr["Original_telling"] == DBNull.Value ? default(String) : Convert.ToString(dr["Original_telling"]);
					lores.Planets = dr["Planets"] == DBNull.Value ? default(String) : Convert.ToString(dr["Planets"]);
					lores.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					lores.Propagation_method = dr["Propagation_method"] == DBNull.Value ? default(String) : Convert.ToString(dr["Propagation_method"]);
					lores.Races = dr["Races"] == DBNull.Value ? default(String) : Convert.ToString(dr["Races"]);
					lores.Reception = dr["Reception"] == DBNull.Value ? default(String) : Convert.ToString(dr["Reception"]);
					lores.Related_lores = dr["Related_lores"] == DBNull.Value ? default(String) : Convert.ToString(dr["Related_lores"]);
					lores.Religions = dr["Religions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Religions"]);
					lores.Schools = dr["Schools"] == DBNull.Value ? default(String) : Convert.ToString(dr["Schools"]);
					lores.Source = dr["Source"] == DBNull.Value ? default(String) : Convert.ToString(dr["Source"]);
					lores.Sports = dr["Sports"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sports"]);
					lores.Structure = dr["Structure"] == DBNull.Value ? default(String) : Convert.ToString(dr["Structure"]);
					lores.Subjects = dr["Subjects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Subjects"]);
					lores.Summary = dr["Summary"] == DBNull.Value ? default(String) : Convert.ToString(dr["Summary"]);
					lores.Symbolisms = dr["Symbolisms"] == DBNull.Value ? default(String) : Convert.ToString(dr["Symbolisms"]);
					lores.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					lores.Technologies = dr["Technologies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Technologies"]);
					lores.Time_period = dr["Time_period"] == DBNull.Value ? default(String) : Convert.ToString(dr["Time_period"]);
					lores.Tone = dr["Tone"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tone"]);
					lores.Towns = dr["Towns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Towns"]);
					lores.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					lores.Translation_variations = dr["Translation_variations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Translation_variations"]);
					lores.True_parts = dr["True_parts"] == DBNull.Value ? default(String) : Convert.ToString(dr["True_parts"]);
					lores.Type = dr["Type"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type"]);
					lores.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					lores.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					lores.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					lores.Variations = dr["Variations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Variations"]);
					lores.Vehicles = dr["Vehicles"] == DBNull.Value ? default(String) : Convert.ToString(dr["Vehicles"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(lores.id, "lores");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    lores.object_id = first.object_id;
						    lores.object_name = first.object_name;
						}

						_return_value.Add(lores);
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

		public string AddLoresData(LoresModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Lores`(`Background_information`,`Believability`,`Believers`,`Buildings`,`Characters`,`Conditions`,`Continents`,`Countries`,`created_at`,`Created_phrases`,`Created_traditions`,`Creatures`,`Criticism`,`Date_recorded`,`Deities`,`Dialect`,`Evolution_over_time`,`False_parts`,`Floras`,`Foods`,`Full_text`,`Genre`,`Geographical_variations`,`Governments`,`Groups`,`Historical_context`,`Hoaxes`,`Impact`,`Important_translations`,`Influence_on_modern_times`,`Inspirations`,`Interpretations`,`Jobs`,`Landmarks`,`Magic`,`Media_adaptations`,`Morals`,`Motivations`,`Name`,`Notes`,`Original_author`,`Original_languages`,`Original_telling`,`Planets`,`Private_Notes`,`Propagation_method`,`Races`,`Reception`,`Related_lores`,`Religions`,`Schools`,`Source`,`Sports`,`Structure`,`Subjects`,`Summary`,`Symbolisms`,`Tags`,`Technologies`,`Time_period`,`Tone`,`Towns`,`Traditions`,`Translation_variations`,`True_parts`,`Type`,`Universe`,`updated_at`,`user_id`,`Variations`,`Vehicles`) VALUES(@Background_information,@Believability,@Believers,@Buildings,@Characters,@Conditions,@Continents,@Countries,@created_at,@Created_phrases,@Created_traditions,@Creatures,@Criticism,@Date_recorded,@Deities,@Dialect,@Evolution_over_time,@False_parts,@Floras,@Foods,@Full_text,@Genre,@Geographical_variations,@Governments,@Groups,@Historical_context,@Hoaxes,@Impact,@Important_translations,@Influence_on_modern_times,@Inspirations,@Interpretations,@Jobs,@Landmarks,@Magic,@Media_adaptations,@Morals,@Motivations,@Name,@Notes,@Original_author,@Original_languages,@Original_telling,@Planets,@Private_Notes,@Propagation_method,@Races,@Reception,@Related_lores,@Religions,@Schools,@Source,@Sports,@Structure,@Subjects,@Summary,@Symbolisms,@Tags,@Technologies,@Time_period,@Tone,@Towns,@Traditions,@Translation_variations,@True_parts,@Type,@Universe,@updated_at,@user_id,@Variations,@Vehicles)";
				dbContext.AddInParameter(dbContext.cmd, "@Background_information", Data.Background_information);
				dbContext.AddInParameter(dbContext.cmd, "@Believability", Data.Believability);
				dbContext.AddInParameter(dbContext.cmd, "@Believers", Data.Believers);
				dbContext.AddInParameter(dbContext.cmd, "@Buildings", Data.Buildings);
				dbContext.AddInParameter(dbContext.cmd, "@Characters", Data.Characters);
				dbContext.AddInParameter(dbContext.cmd, "@Conditions", Data.Conditions);
				dbContext.AddInParameter(dbContext.cmd, "@Continents", Data.Continents);
				dbContext.AddInParameter(dbContext.cmd, "@Countries", Data.Countries);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Created_phrases", Data.Created_phrases);
				dbContext.AddInParameter(dbContext.cmd, "@Created_traditions", Data.Created_traditions);
				dbContext.AddInParameter(dbContext.cmd, "@Creatures", Data.Creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Criticism", Data.Criticism);
				dbContext.AddInParameter(dbContext.cmd, "@Date_recorded", Data.Date_recorded);
				dbContext.AddInParameter(dbContext.cmd, "@Deities", Data.Deities);
				dbContext.AddInParameter(dbContext.cmd, "@Dialect", Data.Dialect);
				dbContext.AddInParameter(dbContext.cmd, "@Evolution_over_time", Data.Evolution_over_time);
				dbContext.AddInParameter(dbContext.cmd, "@False_parts", Data.False_parts);
				dbContext.AddInParameter(dbContext.cmd, "@Floras", Data.Floras);
				dbContext.AddInParameter(dbContext.cmd, "@Foods", Data.Foods);
				dbContext.AddInParameter(dbContext.cmd, "@Full_text", Data.Full_text);
				dbContext.AddInParameter(dbContext.cmd, "@Genre", Data.Genre);
				dbContext.AddInParameter(dbContext.cmd, "@Geographical_variations", Data.Geographical_variations);
				dbContext.AddInParameter(dbContext.cmd, "@Governments", Data.Governments);
				dbContext.AddInParameter(dbContext.cmd, "@Groups", Data.Groups);
				dbContext.AddInParameter(dbContext.cmd, "@Historical_context", Data.Historical_context);
				dbContext.AddInParameter(dbContext.cmd, "@Hoaxes", Data.Hoaxes);
				dbContext.AddInParameter(dbContext.cmd, "@Impact", Data.Impact);
				dbContext.AddInParameter(dbContext.cmd, "@Important_translations", Data.Important_translations);
				dbContext.AddInParameter(dbContext.cmd, "@Influence_on_modern_times", Data.Influence_on_modern_times);
				dbContext.AddInParameter(dbContext.cmd, "@Inspirations", Data.Inspirations);
				dbContext.AddInParameter(dbContext.cmd, "@Interpretations", Data.Interpretations);
				dbContext.AddInParameter(dbContext.cmd, "@Jobs", Data.Jobs);
				dbContext.AddInParameter(dbContext.cmd, "@Landmarks", Data.Landmarks);
				dbContext.AddInParameter(dbContext.cmd, "@Magic", Data.Magic);
				dbContext.AddInParameter(dbContext.cmd, "@Media_adaptations", Data.Media_adaptations);
				dbContext.AddInParameter(dbContext.cmd, "@Morals", Data.Morals);
				dbContext.AddInParameter(dbContext.cmd, "@Motivations", Data.Motivations);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Original_author", Data.Original_author);
				dbContext.AddInParameter(dbContext.cmd, "@Original_languages", Data.Original_languages);
				dbContext.AddInParameter(dbContext.cmd, "@Original_telling", Data.Original_telling);
				dbContext.AddInParameter(dbContext.cmd, "@Planets", Data.Planets);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Propagation_method", Data.Propagation_method);
				dbContext.AddInParameter(dbContext.cmd, "@Races", Data.Races);
				dbContext.AddInParameter(dbContext.cmd, "@Reception", Data.Reception);
				dbContext.AddInParameter(dbContext.cmd, "@Related_lores", Data.Related_lores);
				dbContext.AddInParameter(dbContext.cmd, "@Religions", Data.Religions);
				dbContext.AddInParameter(dbContext.cmd, "@Schools", Data.Schools);
				dbContext.AddInParameter(dbContext.cmd, "@Source", Data.Source);
				dbContext.AddInParameter(dbContext.cmd, "@Sports", Data.Sports);
				dbContext.AddInParameter(dbContext.cmd, "@Structure", Data.Structure);
				dbContext.AddInParameter(dbContext.cmd, "@Subjects", Data.Subjects);
				dbContext.AddInParameter(dbContext.cmd, "@Summary", Data.Summary);
				dbContext.AddInParameter(dbContext.cmd, "@Symbolisms", Data.Symbolisms);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Technologies", Data.Technologies);
				dbContext.AddInParameter(dbContext.cmd, "@Time_period", Data.Time_period);
				dbContext.AddInParameter(dbContext.cmd, "@Tone", Data.Tone);
				dbContext.AddInParameter(dbContext.cmd, "@Towns", Data.Towns);
				dbContext.AddInParameter(dbContext.cmd, "@Traditions", Data.Traditions);
				dbContext.AddInParameter(dbContext.cmd, "@Translation_variations", Data.Translation_variations);
				dbContext.AddInParameter(dbContext.cmd, "@True_parts", Data.True_parts);
				dbContext.AddInParameter(dbContext.cmd, "@Type", Data.Type);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Variations", Data.Variations);
				dbContext.AddInParameter(dbContext.cmd, "@Vehicles", Data.Vehicles);
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
