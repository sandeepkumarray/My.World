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
	public class CreaturesDAL : BaseDAL
	{

		public CreaturesDAL()
		{
		}

		public  CreaturesDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteCreaturesData(CreaturesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Creatures` WHERE id = @id";
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

		public CreaturesModel GetCreaturesData(CreaturesModel Data)
		{
			CreaturesModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Creatures` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new CreaturesModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    CreaturesModel creatures = new CreaturesModel();
					creatures.Aggressiveness = dr["Aggressiveness"] == DBNull.Value ? default(String) : Convert.ToString(dr["Aggressiveness"]);
					creatures.Ancestors = dr["Ancestors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Ancestors"]);
					creatures.Class = dr["Class"] == DBNull.Value ? default(String) : Convert.ToString(dr["Class"]);
					creatures.Color = dr["Color"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Color"]);
					creatures.Competitors = dr["Competitors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Competitors"]);
					creatures.Conditions = dr["Conditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Conditions"]);
					creatures.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					creatures.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					creatures.Evolutionary_drive = dr["Evolutionary_drive"] == DBNull.Value ? default(String) : Convert.ToString(dr["Evolutionary_drive"]);
					creatures.Family = dr["Family"] == DBNull.Value ? default(String) : Convert.ToString(dr["Family"]);
					creatures.Food_sources = dr["Food_sources"] == DBNull.Value ? default(String) : Convert.ToString(dr["Food_sources"]);
					creatures.Genus = dr["Genus"] == DBNull.Value ? default(String) : Convert.ToString(dr["Genus"]);
					creatures.Habitats = dr["Habitats"] == DBNull.Value ? default(String) : Convert.ToString(dr["Habitats"]);
					creatures.Height = dr["Height"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Height"]);
					creatures.Herding_patterns = dr["Herding_patterns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Herding_patterns"]);
					creatures.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					creatures.Materials = dr["Materials"] == DBNull.Value ? default(String) : Convert.ToString(dr["Materials"]);
					creatures.Mating_ritual = dr["Mating_ritual"] == DBNull.Value ? default(String) : Convert.ToString(dr["Mating_ritual"]);
					creatures.Maximum_speed = dr["Maximum_speed"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Maximum_speed"]);
					creatures.Method_of_attack = dr["Method_of_attack"] == DBNull.Value ? default(String) : Convert.ToString(dr["Method_of_attack"]);
					creatures.Methods_of_defense = dr["Methods_of_defense"] == DBNull.Value ? default(String) : Convert.ToString(dr["Methods_of_defense"]);
					creatures.Migratory_patterns = dr["Migratory_patterns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Migratory_patterns"]);
					creatures.Mortality_rate = dr["Mortality_rate"] == DBNull.Value ? default(String) : Convert.ToString(dr["Mortality_rate"]);
					creatures.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					creatures.Notable_features = dr["Notable_features"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_features"]);
					creatures.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					creatures.Offspring_care = dr["Offspring_care"] == DBNull.Value ? default(String) : Convert.ToString(dr["Offspring_care"]);
					creatures.Order = dr["Order"] == DBNull.Value ? default(String) : Convert.ToString(dr["Order"]);
					creatures.Parental_instincts = dr["Parental_instincts"] == DBNull.Value ? default(String) : Convert.ToString(dr["Parental_instincts"]);
					creatures.Phylum = dr["Phylum"] == DBNull.Value ? default(String) : Convert.ToString(dr["Phylum"]);
					creatures.Predators = dr["Predators"] == DBNull.Value ? default(String) : Convert.ToString(dr["Predators"]);
					creatures.Predictions = dr["Predictions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Predictions"]);
					creatures.Preferred_habitat = dr["Preferred_habitat"] == DBNull.Value ? default(String) : Convert.ToString(dr["Preferred_habitat"]);
					creatures.Prey = dr["Prey"] == DBNull.Value ? default(String) : Convert.ToString(dr["Prey"]);
					creatures.Private_notes = dr["Private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_notes"]);
					creatures.Related_creatures = dr["Related_creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Related_creatures"]);
					creatures.Reproduction = dr["Reproduction"] == DBNull.Value ? default(String) : Convert.ToString(dr["Reproduction"]);
					creatures.Reproduction_age = dr["Reproduction_age"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Reproduction_age"]);
					creatures.Reproduction_frequency = dr["Reproduction_frequency"] == DBNull.Value ? default(String) : Convert.ToString(dr["Reproduction_frequency"]);
					creatures.Requirements = dr["Requirements"] == DBNull.Value ? default(String) : Convert.ToString(dr["Requirements"]);
					creatures.Shape = dr["Shape"] == DBNull.Value ? default(String) : Convert.ToString(dr["Shape"]);
					creatures.Similar_creatures = dr["Similar_creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Similar_creatures"]);
					creatures.Size = dr["Size"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Size"]);
					creatures.Sounds = dr["Sounds"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sounds"]);
					creatures.Species = dr["Species"] == DBNull.Value ? default(String) : Convert.ToString(dr["Species"]);
					creatures.Spoils = dr["Spoils"] == DBNull.Value ? default(String) : Convert.ToString(dr["Spoils"]);
					creatures.Strengths = dr["Strengths"] == DBNull.Value ? default(String) : Convert.ToString(dr["Strengths"]);
					creatures.Strongest_sense = dr["Strongest_sense"] == DBNull.Value ? default(String) : Convert.ToString(dr["Strongest_sense"]);
					creatures.Symbolisms = dr["Symbolisms"] == DBNull.Value ? default(String) : Convert.ToString(dr["Symbolisms"]);
					creatures.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					creatures.Tradeoffs = dr["Tradeoffs"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tradeoffs"]);
					creatures.Type_of_creature = dr["Type_of_creature"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_creature"]);
					creatures.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					creatures.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					creatures.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					creatures.Variations = dr["Variations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Variations"]);
					creatures.Vestigial_features = dr["Vestigial_features"] == DBNull.Value ? default(String) : Convert.ToString(dr["Vestigial_features"]);
					creatures.Weakest_sense = dr["Weakest_sense"] == DBNull.Value ? default(String) : Convert.ToString(dr["Weakest_sense"]);
					creatures.Weaknesses = dr["Weaknesses"] == DBNull.Value ? default(String) : Convert.ToString(dr["Weaknesses"]);
					creatures.Weight = dr["Weight"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Weight"]);

					_return_value = creatures;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<CreaturesModel> GetAllCreaturesForUserID(long userId)
		{
			List<CreaturesModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Creatures` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<CreaturesModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						CreaturesModel creatures = new CreaturesModel();
					creatures.Aggressiveness = dr["Aggressiveness"] == DBNull.Value ? default(String) : Convert.ToString(dr["Aggressiveness"]);
					creatures.Ancestors = dr["Ancestors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Ancestors"]);
					creatures.Class = dr["Class"] == DBNull.Value ? default(String) : Convert.ToString(dr["Class"]);
					creatures.Color = dr["Color"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Color"]);
					creatures.Competitors = dr["Competitors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Competitors"]);
					creatures.Conditions = dr["Conditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Conditions"]);
					creatures.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					creatures.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					creatures.Evolutionary_drive = dr["Evolutionary_drive"] == DBNull.Value ? default(String) : Convert.ToString(dr["Evolutionary_drive"]);
					creatures.Family = dr["Family"] == DBNull.Value ? default(String) : Convert.ToString(dr["Family"]);
					creatures.Food_sources = dr["Food_sources"] == DBNull.Value ? default(String) : Convert.ToString(dr["Food_sources"]);
					creatures.Genus = dr["Genus"] == DBNull.Value ? default(String) : Convert.ToString(dr["Genus"]);
					creatures.Habitats = dr["Habitats"] == DBNull.Value ? default(String) : Convert.ToString(dr["Habitats"]);
					creatures.Height = dr["Height"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Height"]);
					creatures.Herding_patterns = dr["Herding_patterns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Herding_patterns"]);
					creatures.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					creatures.Materials = dr["Materials"] == DBNull.Value ? default(String) : Convert.ToString(dr["Materials"]);
					creatures.Mating_ritual = dr["Mating_ritual"] == DBNull.Value ? default(String) : Convert.ToString(dr["Mating_ritual"]);
					creatures.Maximum_speed = dr["Maximum_speed"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Maximum_speed"]);
					creatures.Method_of_attack = dr["Method_of_attack"] == DBNull.Value ? default(String) : Convert.ToString(dr["Method_of_attack"]);
					creatures.Methods_of_defense = dr["Methods_of_defense"] == DBNull.Value ? default(String) : Convert.ToString(dr["Methods_of_defense"]);
					creatures.Migratory_patterns = dr["Migratory_patterns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Migratory_patterns"]);
					creatures.Mortality_rate = dr["Mortality_rate"] == DBNull.Value ? default(String) : Convert.ToString(dr["Mortality_rate"]);
					creatures.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					creatures.Notable_features = dr["Notable_features"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_features"]);
					creatures.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					creatures.Offspring_care = dr["Offspring_care"] == DBNull.Value ? default(String) : Convert.ToString(dr["Offspring_care"]);
					creatures.Order = dr["Order"] == DBNull.Value ? default(String) : Convert.ToString(dr["Order"]);
					creatures.Parental_instincts = dr["Parental_instincts"] == DBNull.Value ? default(String) : Convert.ToString(dr["Parental_instincts"]);
					creatures.Phylum = dr["Phylum"] == DBNull.Value ? default(String) : Convert.ToString(dr["Phylum"]);
					creatures.Predators = dr["Predators"] == DBNull.Value ? default(String) : Convert.ToString(dr["Predators"]);
					creatures.Predictions = dr["Predictions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Predictions"]);
					creatures.Preferred_habitat = dr["Preferred_habitat"] == DBNull.Value ? default(String) : Convert.ToString(dr["Preferred_habitat"]);
					creatures.Prey = dr["Prey"] == DBNull.Value ? default(String) : Convert.ToString(dr["Prey"]);
					creatures.Private_notes = dr["Private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_notes"]);
					creatures.Related_creatures = dr["Related_creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Related_creatures"]);
					creatures.Reproduction = dr["Reproduction"] == DBNull.Value ? default(String) : Convert.ToString(dr["Reproduction"]);
					creatures.Reproduction_age = dr["Reproduction_age"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Reproduction_age"]);
					creatures.Reproduction_frequency = dr["Reproduction_frequency"] == DBNull.Value ? default(String) : Convert.ToString(dr["Reproduction_frequency"]);
					creatures.Requirements = dr["Requirements"] == DBNull.Value ? default(String) : Convert.ToString(dr["Requirements"]);
					creatures.Shape = dr["Shape"] == DBNull.Value ? default(String) : Convert.ToString(dr["Shape"]);
					creatures.Similar_creatures = dr["Similar_creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Similar_creatures"]);
					creatures.Size = dr["Size"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Size"]);
					creatures.Sounds = dr["Sounds"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sounds"]);
					creatures.Species = dr["Species"] == DBNull.Value ? default(String) : Convert.ToString(dr["Species"]);
					creatures.Spoils = dr["Spoils"] == DBNull.Value ? default(String) : Convert.ToString(dr["Spoils"]);
					creatures.Strengths = dr["Strengths"] == DBNull.Value ? default(String) : Convert.ToString(dr["Strengths"]);
					creatures.Strongest_sense = dr["Strongest_sense"] == DBNull.Value ? default(String) : Convert.ToString(dr["Strongest_sense"]);
					creatures.Symbolisms = dr["Symbolisms"] == DBNull.Value ? default(String) : Convert.ToString(dr["Symbolisms"]);
					creatures.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					creatures.Tradeoffs = dr["Tradeoffs"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tradeoffs"]);
					creatures.Type_of_creature = dr["Type_of_creature"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_creature"]);
					creatures.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					creatures.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					creatures.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					creatures.Variations = dr["Variations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Variations"]);
					creatures.Vestigial_features = dr["Vestigial_features"] == DBNull.Value ? default(String) : Convert.ToString(dr["Vestigial_features"]);
					creatures.Weakest_sense = dr["Weakest_sense"] == DBNull.Value ? default(String) : Convert.ToString(dr["Weakest_sense"]);
					creatures.Weaknesses = dr["Weaknesses"] == DBNull.Value ? default(String) : Convert.ToString(dr["Weaknesses"]);
					creatures.Weight = dr["Weight"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Weight"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(creatures.id, "creatures");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    creatures.object_id = first.object_id;
						    creatures.object_name = first.object_name;
						}

						_return_value.Add(creatures);
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

		public string AddCreaturesData(CreaturesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Creatures`(`Aggressiveness`,`Ancestors`,`Class`,`Color`,`Competitors`,`Conditions`,`created_at`,`Description`,`Evolutionary_drive`,`Family`,`Food_sources`,`Genus`,`Habitats`,`Height`,`Herding_patterns`,`Materials`,`Mating_ritual`,`Maximum_speed`,`Method_of_attack`,`Methods_of_defense`,`Migratory_patterns`,`Mortality_rate`,`Name`,`Notable_features`,`Notes`,`Offspring_care`,`Order`,`Parental_instincts`,`Phylum`,`Predators`,`Predictions`,`Preferred_habitat`,`Prey`,`Private_notes`,`Related_creatures`,`Reproduction`,`Reproduction_age`,`Reproduction_frequency`,`Requirements`,`Shape`,`Similar_creatures`,`Size`,`Sounds`,`Species`,`Spoils`,`Strengths`,`Strongest_sense`,`Symbolisms`,`Tags`,`Tradeoffs`,`Type_of_creature`,`Universe`,`updated_at`,`user_id`,`Variations`,`Vestigial_features`,`Weakest_sense`,`Weaknesses`,`Weight`) VALUES(@Aggressiveness,@Ancestors,@Class,@Color,@Competitors,@Conditions,@created_at,@Description,@Evolutionary_drive,@Family,@Food_sources,@Genus,@Habitats,@Height,@Herding_patterns,@Materials,@Mating_ritual,@Maximum_speed,@Method_of_attack,@Methods_of_defense,@Migratory_patterns,@Mortality_rate,@Name,@Notable_features,@Notes,@Offspring_care,@Order,@Parental_instincts,@Phylum,@Predators,@Predictions,@Preferred_habitat,@Prey,@Private_notes,@Related_creatures,@Reproduction,@Reproduction_age,@Reproduction_frequency,@Requirements,@Shape,@Similar_creatures,@Size,@Sounds,@Species,@Spoils,@Strengths,@Strongest_sense,@Symbolisms,@Tags,@Tradeoffs,@Type_of_creature,@Universe,@updated_at,@user_id,@Variations,@Vestigial_features,@Weakest_sense,@Weaknesses,@Weight)";
				dbContext.AddInParameter(dbContext.cmd, "@Aggressiveness", Data.Aggressiveness);
				dbContext.AddInParameter(dbContext.cmd, "@Ancestors", Data.Ancestors);
				dbContext.AddInParameter(dbContext.cmd, "@Class", Data.Class);
				dbContext.AddInParameter(dbContext.cmd, "@Color", Data.Color);
				dbContext.AddInParameter(dbContext.cmd, "@Competitors", Data.Competitors);
				dbContext.AddInParameter(dbContext.cmd, "@Conditions", Data.Conditions);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Evolutionary_drive", Data.Evolutionary_drive);
				dbContext.AddInParameter(dbContext.cmd, "@Family", Data.Family);
				dbContext.AddInParameter(dbContext.cmd, "@Food_sources", Data.Food_sources);
				dbContext.AddInParameter(dbContext.cmd, "@Genus", Data.Genus);
				dbContext.AddInParameter(dbContext.cmd, "@Habitats", Data.Habitats);
				dbContext.AddInParameter(dbContext.cmd, "@Height", Data.Height);
				dbContext.AddInParameter(dbContext.cmd, "@Herding_patterns", Data.Herding_patterns);
				dbContext.AddInParameter(dbContext.cmd, "@Materials", Data.Materials);
				dbContext.AddInParameter(dbContext.cmd, "@Mating_ritual", Data.Mating_ritual);
				dbContext.AddInParameter(dbContext.cmd, "@Maximum_speed", Data.Maximum_speed);
				dbContext.AddInParameter(dbContext.cmd, "@Method_of_attack", Data.Method_of_attack);
				dbContext.AddInParameter(dbContext.cmd, "@Methods_of_defense", Data.Methods_of_defense);
				dbContext.AddInParameter(dbContext.cmd, "@Migratory_patterns", Data.Migratory_patterns);
				dbContext.AddInParameter(dbContext.cmd, "@Mortality_rate", Data.Mortality_rate);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_features", Data.Notable_features);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Offspring_care", Data.Offspring_care);
				dbContext.AddInParameter(dbContext.cmd, "@Order", Data.Order);
				dbContext.AddInParameter(dbContext.cmd, "@Parental_instincts", Data.Parental_instincts);
				dbContext.AddInParameter(dbContext.cmd, "@Phylum", Data.Phylum);
				dbContext.AddInParameter(dbContext.cmd, "@Predators", Data.Predators);
				dbContext.AddInParameter(dbContext.cmd, "@Predictions", Data.Predictions);
				dbContext.AddInParameter(dbContext.cmd, "@Preferred_habitat", Data.Preferred_habitat);
				dbContext.AddInParameter(dbContext.cmd, "@Prey", Data.Prey);
				dbContext.AddInParameter(dbContext.cmd, "@Private_notes", Data.Private_notes);
				dbContext.AddInParameter(dbContext.cmd, "@Related_creatures", Data.Related_creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Reproduction", Data.Reproduction);
				dbContext.AddInParameter(dbContext.cmd, "@Reproduction_age", Data.Reproduction_age);
				dbContext.AddInParameter(dbContext.cmd, "@Reproduction_frequency", Data.Reproduction_frequency);
				dbContext.AddInParameter(dbContext.cmd, "@Requirements", Data.Requirements);
				dbContext.AddInParameter(dbContext.cmd, "@Shape", Data.Shape);
				dbContext.AddInParameter(dbContext.cmd, "@Similar_creatures", Data.Similar_creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Size", Data.Size);
				dbContext.AddInParameter(dbContext.cmd, "@Sounds", Data.Sounds);
				dbContext.AddInParameter(dbContext.cmd, "@Species", Data.Species);
				dbContext.AddInParameter(dbContext.cmd, "@Spoils", Data.Spoils);
				dbContext.AddInParameter(dbContext.cmd, "@Strengths", Data.Strengths);
				dbContext.AddInParameter(dbContext.cmd, "@Strongest_sense", Data.Strongest_sense);
				dbContext.AddInParameter(dbContext.cmd, "@Symbolisms", Data.Symbolisms);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Tradeoffs", Data.Tradeoffs);
				dbContext.AddInParameter(dbContext.cmd, "@Type_of_creature", Data.Type_of_creature);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Variations", Data.Variations);
				dbContext.AddInParameter(dbContext.cmd, "@Vestigial_features", Data.Vestigial_features);
				dbContext.AddInParameter(dbContext.cmd, "@Weakest_sense", Data.Weakest_sense);
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

		public string UpdateCreaturesData(CreaturesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "UPDATE creatures SET Aggressiveness = @Aggressiveness,Ancestors = @Ancestors,Class = @Class,Color = @Color,Competitors = @Competitors,Conditions = @Conditions,created_at = @created_at,Description = @Description,Evolutionary_drive = @Evolutionary_drive,Family = @Family,Food_sources = @Food_sources,Genus = @Genus,Habitats = @Habitats,Height = @Height,Herding_patterns = @Herding_patterns,Materials = @Materials,Mating_ritual = @Mating_ritual,Maximum_speed = @Maximum_speed,Method_of_attack = @Method_of_attack,Methods_of_defense = @Methods_of_defense,Migratory_patterns = @Migratory_patterns,Mortality_rate = @Mortality_rate,Name = @Name,Notable_features = @Notable_features,Notes = @Notes,Offspring_care = @Offspring_care,Order = @Order,Parental_instincts = @Parental_instincts,Phylum = @Phylum,Predators = @Predators,Predictions = @Predictions,Preferred_habitat = @Preferred_habitat,Prey = @Prey,Private_notes = @Private_notes,Related_creatures = @Related_creatures,Reproduction = @Reproduction,Reproduction_age = @Reproduction_age,Reproduction_frequency = @Reproduction_frequency,Requirements = @Requirements,Shape = @Shape,Similar_creatures = @Similar_creatures,Size = @Size,Sounds = @Sounds,Species = @Species,Spoils = @Spoils,Strengths = @Strengths,Strongest_sense = @Strongest_sense,Symbolisms = @Symbolisms,Tags = @Tags,Tradeoffs = @Tradeoffs,Type_of_creature = @Type_of_creature,Universe = @Universe,updated_at = @updated_at,user_id = @user_id,Variations = @Variations,Vestigial_features = @Vestigial_features,Weakest_sense = @Weakest_sense,Weaknesses = @Weaknesses,Weight = @Weight WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@Aggressiveness", Data.Aggressiveness);
				dbContext.AddInParameter(dbContext.cmd, "@Ancestors", Data.Ancestors);
				dbContext.AddInParameter(dbContext.cmd, "@Class", Data.Class);
				dbContext.AddInParameter(dbContext.cmd, "@Color", Data.Color);
				dbContext.AddInParameter(dbContext.cmd, "@Competitors", Data.Competitors);
				dbContext.AddInParameter(dbContext.cmd, "@Conditions", Data.Conditions);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Evolutionary_drive", Data.Evolutionary_drive);
				dbContext.AddInParameter(dbContext.cmd, "@Family", Data.Family);
				dbContext.AddInParameter(dbContext.cmd, "@Food_sources", Data.Food_sources);
				dbContext.AddInParameter(dbContext.cmd, "@Genus", Data.Genus);
				dbContext.AddInParameter(dbContext.cmd, "@Habitats", Data.Habitats);
				dbContext.AddInParameter(dbContext.cmd, "@Height", Data.Height);
				dbContext.AddInParameter(dbContext.cmd, "@Herding_patterns", Data.Herding_patterns);
				dbContext.AddInParameter(dbContext.cmd, "@Materials", Data.Materials);
				dbContext.AddInParameter(dbContext.cmd, "@Mating_ritual", Data.Mating_ritual);
				dbContext.AddInParameter(dbContext.cmd, "@Maximum_speed", Data.Maximum_speed);
				dbContext.AddInParameter(dbContext.cmd, "@Method_of_attack", Data.Method_of_attack);
				dbContext.AddInParameter(dbContext.cmd, "@Methods_of_defense", Data.Methods_of_defense);
				dbContext.AddInParameter(dbContext.cmd, "@Migratory_patterns", Data.Migratory_patterns);
				dbContext.AddInParameter(dbContext.cmd, "@Mortality_rate", Data.Mortality_rate);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_features", Data.Notable_features);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Offspring_care", Data.Offspring_care);
				dbContext.AddInParameter(dbContext.cmd, "@Order", Data.Order);
				dbContext.AddInParameter(dbContext.cmd, "@Parental_instincts", Data.Parental_instincts);
				dbContext.AddInParameter(dbContext.cmd, "@Phylum", Data.Phylum);
				dbContext.AddInParameter(dbContext.cmd, "@Predators", Data.Predators);
				dbContext.AddInParameter(dbContext.cmd, "@Predictions", Data.Predictions);
				dbContext.AddInParameter(dbContext.cmd, "@Preferred_habitat", Data.Preferred_habitat);
				dbContext.AddInParameter(dbContext.cmd, "@Prey", Data.Prey);
				dbContext.AddInParameter(dbContext.cmd, "@Private_notes", Data.Private_notes);
				dbContext.AddInParameter(dbContext.cmd, "@Related_creatures", Data.Related_creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Reproduction", Data.Reproduction);
				dbContext.AddInParameter(dbContext.cmd, "@Reproduction_age", Data.Reproduction_age);
				dbContext.AddInParameter(dbContext.cmd, "@Reproduction_frequency", Data.Reproduction_frequency);
				dbContext.AddInParameter(dbContext.cmd, "@Requirements", Data.Requirements);
				dbContext.AddInParameter(dbContext.cmd, "@Shape", Data.Shape);
				dbContext.AddInParameter(dbContext.cmd, "@Similar_creatures", Data.Similar_creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Size", Data.Size);
				dbContext.AddInParameter(dbContext.cmd, "@Sounds", Data.Sounds);
				dbContext.AddInParameter(dbContext.cmd, "@Species", Data.Species);
				dbContext.AddInParameter(dbContext.cmd, "@Spoils", Data.Spoils);
				dbContext.AddInParameter(dbContext.cmd, "@Strengths", Data.Strengths);
				dbContext.AddInParameter(dbContext.cmd, "@Strongest_sense", Data.Strongest_sense);
				dbContext.AddInParameter(dbContext.cmd, "@Symbolisms", Data.Symbolisms);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Tradeoffs", Data.Tradeoffs);
				dbContext.AddInParameter(dbContext.cmd, "@Type_of_creature", Data.Type_of_creature);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Variations", Data.Variations);
				dbContext.AddInParameter(dbContext.cmd, "@Vestigial_features", Data.Vestigial_features);
				dbContext.AddInParameter(dbContext.cmd, "@Weakest_sense", Data.Weakest_sense);
				dbContext.AddInParameter(dbContext.cmd, "@Weaknesses", Data.Weaknesses);
				dbContext.AddInParameter(dbContext.cmd, "@Weight", Data.Weight);
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
