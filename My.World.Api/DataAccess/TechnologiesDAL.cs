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
	public class TechnologiesDAL : BaseDAL
	{

		public TechnologiesDAL()
		{
		}

		public  TechnologiesDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteTechnologiesData(TechnologiesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Technologies` WHERE id = @id";
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

		public TechnologiesModel GetTechnologiesData(TechnologiesModel Data)
		{
			TechnologiesModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Technologies` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new TechnologiesModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    TechnologiesModel technologies = new TechnologiesModel();
					technologies.Characters = dr["Characters"] == DBNull.Value ? default(String) : Convert.ToString(dr["Characters"]);
					technologies.Child_technologies = dr["Child_technologies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Child_technologies"]);
					technologies.Colors = dr["Colors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Colors"]);
					technologies.Cost = dr["Cost"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Cost"]);
					technologies.Countries = dr["Countries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Countries"]);
					technologies.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					technologies.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					technologies.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					technologies.Groups = dr["Groups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Groups"]);
					technologies.How_It_Works = dr["How_It_Works"] == DBNull.Value ? default(String) : Convert.ToString(dr["How_It_Works"]);
					technologies.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					technologies.Magic_effects = dr["Magic_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Magic_effects"]);
					technologies.Manufacturing_Process = dr["Manufacturing_Process"] == DBNull.Value ? default(String) : Convert.ToString(dr["Manufacturing_Process"]);
					technologies.Materials = dr["Materials"] == DBNull.Value ? default(String) : Convert.ToString(dr["Materials"]);
					technologies.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					technologies.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					technologies.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					technologies.Parent_technologies = dr["Parent_technologies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Parent_technologies"]);
					technologies.Physical_Description = dr["Physical_Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Physical_Description"]);
					technologies.Planets = dr["Planets"] == DBNull.Value ? default(String) : Convert.ToString(dr["Planets"]);
					technologies.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					technologies.Purpose = dr["Purpose"] == DBNull.Value ? default(String) : Convert.ToString(dr["Purpose"]);
					technologies.Rarity = dr["Rarity"] == DBNull.Value ? default(String) : Convert.ToString(dr["Rarity"]);
					technologies.Related_technologies = dr["Related_technologies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Related_technologies"]);
					technologies.Resources_Used = dr["Resources_Used"] == DBNull.Value ? default(String) : Convert.ToString(dr["Resources_Used"]);
					technologies.Sales_Process = dr["Sales_Process"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sales_Process"]);
					technologies.Size = dr["Size"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Size"]);
					technologies.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					technologies.Towns = dr["Towns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Towns"]);
					technologies.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					technologies.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					technologies.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					technologies.Weight = dr["Weight"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Weight"]);

					_return_value = technologies;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<TechnologiesModel> GetAllTechnologiesForUserID(long userId)
		{
			List<TechnologiesModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Technologies` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<TechnologiesModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						TechnologiesModel technologies = new TechnologiesModel();
					technologies.Characters = dr["Characters"] == DBNull.Value ? default(String) : Convert.ToString(dr["Characters"]);
					technologies.Child_technologies = dr["Child_technologies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Child_technologies"]);
					technologies.Colors = dr["Colors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Colors"]);
					technologies.Cost = dr["Cost"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Cost"]);
					technologies.Countries = dr["Countries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Countries"]);
					technologies.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					technologies.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					technologies.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					technologies.Groups = dr["Groups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Groups"]);
					technologies.How_It_Works = dr["How_It_Works"] == DBNull.Value ? default(String) : Convert.ToString(dr["How_It_Works"]);
					technologies.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					technologies.Magic_effects = dr["Magic_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Magic_effects"]);
					technologies.Manufacturing_Process = dr["Manufacturing_Process"] == DBNull.Value ? default(String) : Convert.ToString(dr["Manufacturing_Process"]);
					technologies.Materials = dr["Materials"] == DBNull.Value ? default(String) : Convert.ToString(dr["Materials"]);
					technologies.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					technologies.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					technologies.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					technologies.Parent_technologies = dr["Parent_technologies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Parent_technologies"]);
					technologies.Physical_Description = dr["Physical_Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Physical_Description"]);
					technologies.Planets = dr["Planets"] == DBNull.Value ? default(String) : Convert.ToString(dr["Planets"]);
					technologies.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					technologies.Purpose = dr["Purpose"] == DBNull.Value ? default(String) : Convert.ToString(dr["Purpose"]);
					technologies.Rarity = dr["Rarity"] == DBNull.Value ? default(String) : Convert.ToString(dr["Rarity"]);
					technologies.Related_technologies = dr["Related_technologies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Related_technologies"]);
					technologies.Resources_Used = dr["Resources_Used"] == DBNull.Value ? default(String) : Convert.ToString(dr["Resources_Used"]);
					technologies.Sales_Process = dr["Sales_Process"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sales_Process"]);
					technologies.Size = dr["Size"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Size"]);
					technologies.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					technologies.Towns = dr["Towns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Towns"]);
					technologies.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					technologies.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					technologies.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					technologies.Weight = dr["Weight"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Weight"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(technologies.id, "technologies");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    technologies.object_id = first.object_id;
						    technologies.object_name = first.object_name;
						}

						_return_value.Add(technologies);
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

		public string AddTechnologiesData(TechnologiesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Technologies`(`Characters`,`Child_technologies`,`Colors`,`Cost`,`Countries`,`created_at`,`Creatures`,`Description`,`Groups`,`How_It_Works`,`Magic_effects`,`Manufacturing_Process`,`Materials`,`Name`,`Notes`,`Other_Names`,`Parent_technologies`,`Physical_Description`,`Planets`,`Private_Notes`,`Purpose`,`Rarity`,`Related_technologies`,`Resources_Used`,`Sales_Process`,`Size`,`Tags`,`Towns`,`Universe`,`updated_at`,`user_id`,`Weight`) VALUES(@Characters,@Child_technologies,@Colors,@Cost,@Countries,@created_at,@Creatures,@Description,@Groups,@How_It_Works,@Magic_effects,@Manufacturing_Process,@Materials,@Name,@Notes,@Other_Names,@Parent_technologies,@Physical_Description,@Planets,@Private_Notes,@Purpose,@Rarity,@Related_technologies,@Resources_Used,@Sales_Process,@Size,@Tags,@Towns,@Universe,@updated_at,@user_id,@Weight)";
				dbContext.AddInParameter(dbContext.cmd, "@Characters", Data.Characters);
				dbContext.AddInParameter(dbContext.cmd, "@Child_technologies", Data.Child_technologies);
				dbContext.AddInParameter(dbContext.cmd, "@Colors", Data.Colors);
				dbContext.AddInParameter(dbContext.cmd, "@Cost", Data.Cost);
				dbContext.AddInParameter(dbContext.cmd, "@Countries", Data.Countries);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Creatures", Data.Creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Groups", Data.Groups);
				dbContext.AddInParameter(dbContext.cmd, "@How_It_Works", Data.How_It_Works);
				dbContext.AddInParameter(dbContext.cmd, "@Magic_effects", Data.Magic_effects);
				dbContext.AddInParameter(dbContext.cmd, "@Manufacturing_Process", Data.Manufacturing_Process);
				dbContext.AddInParameter(dbContext.cmd, "@Materials", Data.Materials);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Other_Names", Data.Other_Names);
				dbContext.AddInParameter(dbContext.cmd, "@Parent_technologies", Data.Parent_technologies);
				dbContext.AddInParameter(dbContext.cmd, "@Physical_Description", Data.Physical_Description);
				dbContext.AddInParameter(dbContext.cmd, "@Planets", Data.Planets);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Purpose", Data.Purpose);
				dbContext.AddInParameter(dbContext.cmd, "@Rarity", Data.Rarity);
				dbContext.AddInParameter(dbContext.cmd, "@Related_technologies", Data.Related_technologies);
				dbContext.AddInParameter(dbContext.cmd, "@Resources_Used", Data.Resources_Used);
				dbContext.AddInParameter(dbContext.cmd, "@Sales_Process", Data.Sales_Process);
				dbContext.AddInParameter(dbContext.cmd, "@Size", Data.Size);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Towns", Data.Towns);
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
