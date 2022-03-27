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
	public class GroupsDAL : BaseDAL
	{

		public GroupsDAL()
		{
		}

		public  GroupsDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteGroupsData(GroupsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Groups` WHERE id = @id";
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

		public GroupsModel GetGroupsData(GroupsModel Data)
		{
			GroupsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Groups` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new GroupsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    GroupsModel groups = new GroupsModel();
					groups.Allies = dr["Allies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Allies"]);
					groups.Clients = dr["Clients"] == DBNull.Value ? default(String) : Convert.ToString(dr["Clients"]);
					groups.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					groups.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					groups.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					groups.Enemies = dr["Enemies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Enemies"]);
					groups.Equipment = dr["Equipment"] == DBNull.Value ? default(String) : Convert.ToString(dr["Equipment"]);
					groups.Goals = dr["Goals"] == DBNull.Value ? default(String) : Convert.ToString(dr["Goals"]);
					groups.Headquarters = dr["Headquarters"] == DBNull.Value ? default(String) : Convert.ToString(dr["Headquarters"]);
					groups.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					groups.Inventory = dr["Inventory"] == DBNull.Value ? default(String) : Convert.ToString(dr["Inventory"]);
					groups.Key_items = dr["Key_items"] == DBNull.Value ? default(String) : Convert.ToString(dr["Key_items"]);
					groups.Leaders = dr["Leaders"] == DBNull.Value ? default(String) : Convert.ToString(dr["Leaders"]);
					groups.Locations = dr["Locations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Locations"]);
					groups.Members = dr["Members"] == DBNull.Value ? default(String) : Convert.ToString(dr["Members"]);
					groups.Motivations = dr["Motivations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Motivations"]);
					groups.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					groups.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					groups.Obstacles = dr["Obstacles"] == DBNull.Value ? default(String) : Convert.ToString(dr["Obstacles"]);
					groups.Offices = dr["Offices"] == DBNull.Value ? default(String) : Convert.ToString(dr["Offices"]);
					groups.Organization_structure = dr["Organization_structure"] == DBNull.Value ? default(String) : Convert.ToString(dr["Organization_structure"]);
					groups.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					groups.Private_notes = dr["Private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_notes"]);
					groups.Risks = dr["Risks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Risks"]);
					groups.Rivals = dr["Rivals"] == DBNull.Value ? default(String) : Convert.ToString(dr["Rivals"]);
					groups.Sistergroups = dr["Sistergroups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sistergroups"]);
					groups.Subgroups = dr["Subgroups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Subgroups"]);
					groups.Supergroups = dr["Supergroups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Supergroups"]);
					groups.Suppliers = dr["Suppliers"] == DBNull.Value ? default(String) : Convert.ToString(dr["Suppliers"]);
					groups.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					groups.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					groups.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					groups.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					groups.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);

					_return_value = groups;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<GroupsModel> GetAllGroupsForUserID(long userId)
		{
			List<GroupsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Groups` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<GroupsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						GroupsModel groups = new GroupsModel();
					groups.Allies = dr["Allies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Allies"]);
					groups.Clients = dr["Clients"] == DBNull.Value ? default(String) : Convert.ToString(dr["Clients"]);
					groups.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					groups.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					groups.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					groups.Enemies = dr["Enemies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Enemies"]);
					groups.Equipment = dr["Equipment"] == DBNull.Value ? default(String) : Convert.ToString(dr["Equipment"]);
					groups.Goals = dr["Goals"] == DBNull.Value ? default(String) : Convert.ToString(dr["Goals"]);
					groups.Headquarters = dr["Headquarters"] == DBNull.Value ? default(String) : Convert.ToString(dr["Headquarters"]);
					groups.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					groups.Inventory = dr["Inventory"] == DBNull.Value ? default(String) : Convert.ToString(dr["Inventory"]);
					groups.Key_items = dr["Key_items"] == DBNull.Value ? default(String) : Convert.ToString(dr["Key_items"]);
					groups.Leaders = dr["Leaders"] == DBNull.Value ? default(String) : Convert.ToString(dr["Leaders"]);
					groups.Locations = dr["Locations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Locations"]);
					groups.Members = dr["Members"] == DBNull.Value ? default(String) : Convert.ToString(dr["Members"]);
					groups.Motivations = dr["Motivations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Motivations"]);
					groups.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					groups.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					groups.Obstacles = dr["Obstacles"] == DBNull.Value ? default(String) : Convert.ToString(dr["Obstacles"]);
					groups.Offices = dr["Offices"] == DBNull.Value ? default(String) : Convert.ToString(dr["Offices"]);
					groups.Organization_structure = dr["Organization_structure"] == DBNull.Value ? default(String) : Convert.ToString(dr["Organization_structure"]);
					groups.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					groups.Private_notes = dr["Private_notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_notes"]);
					groups.Risks = dr["Risks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Risks"]);
					groups.Rivals = dr["Rivals"] == DBNull.Value ? default(String) : Convert.ToString(dr["Rivals"]);
					groups.Sistergroups = dr["Sistergroups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sistergroups"]);
					groups.Subgroups = dr["Subgroups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Subgroups"]);
					groups.Supergroups = dr["Supergroups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Supergroups"]);
					groups.Suppliers = dr["Suppliers"] == DBNull.Value ? default(String) : Convert.ToString(dr["Suppliers"]);
					groups.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					groups.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					groups.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					groups.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					groups.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(groups.id, "groups");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    groups.object_id = first.object_id;
						    groups.object_name = first.object_name;
						}

						_return_value.Add(groups);
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

		public string AddGroupsData(GroupsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Groups`(`Allies`,`Clients`,`created_at`,`Creatures`,`Description`,`Enemies`,`Equipment`,`Goals`,`Headquarters`,`Inventory`,`Key_items`,`Leaders`,`Locations`,`Members`,`Motivations`,`Name`,`Notes`,`Obstacles`,`Offices`,`Organization_structure`,`Other_Names`,`Private_notes`,`Risks`,`Rivals`,`Sistergroups`,`Subgroups`,`Supergroups`,`Suppliers`,`Tags`,`Traditions`,`Universe`,`updated_at`,`user_id`) VALUES(@Allies,@Clients,@created_at,@Creatures,@Description,@Enemies,@Equipment,@Goals,@Headquarters,@Inventory,@Key_items,@Leaders,@Locations,@Members,@Motivations,@Name,@Notes,@Obstacles,@Offices,@Organization_structure,@Other_Names,@Private_notes,@Risks,@Rivals,@Sistergroups,@Subgroups,@Supergroups,@Suppliers,@Tags,@Traditions,@Universe,@updated_at,@user_id)";
				dbContext.AddInParameter(dbContext.cmd, "@Allies", Data.Allies);
				dbContext.AddInParameter(dbContext.cmd, "@Clients", Data.Clients);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Creatures", Data.Creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Enemies", Data.Enemies);
				dbContext.AddInParameter(dbContext.cmd, "@Equipment", Data.Equipment);
				dbContext.AddInParameter(dbContext.cmd, "@Goals", Data.Goals);
				dbContext.AddInParameter(dbContext.cmd, "@Headquarters", Data.Headquarters);
				dbContext.AddInParameter(dbContext.cmd, "@Inventory", Data.Inventory);
				dbContext.AddInParameter(dbContext.cmd, "@Key_items", Data.Key_items);
				dbContext.AddInParameter(dbContext.cmd, "@Leaders", Data.Leaders);
				dbContext.AddInParameter(dbContext.cmd, "@Locations", Data.Locations);
				dbContext.AddInParameter(dbContext.cmd, "@Members", Data.Members);
				dbContext.AddInParameter(dbContext.cmd, "@Motivations", Data.Motivations);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Obstacles", Data.Obstacles);
				dbContext.AddInParameter(dbContext.cmd, "@Offices", Data.Offices);
				dbContext.AddInParameter(dbContext.cmd, "@Organization_structure", Data.Organization_structure);
				dbContext.AddInParameter(dbContext.cmd, "@Other_Names", Data.Other_Names);
				dbContext.AddInParameter(dbContext.cmd, "@Private_notes", Data.Private_notes);
				dbContext.AddInParameter(dbContext.cmd, "@Risks", Data.Risks);
				dbContext.AddInParameter(dbContext.cmd, "@Rivals", Data.Rivals);
				dbContext.AddInParameter(dbContext.cmd, "@Sistergroups", Data.Sistergroups);
				dbContext.AddInParameter(dbContext.cmd, "@Subgroups", Data.Subgroups);
				dbContext.AddInParameter(dbContext.cmd, "@Supergroups", Data.Supergroups);
				dbContext.AddInParameter(dbContext.cmd, "@Suppliers", Data.Suppliers);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Traditions", Data.Traditions);
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

		public string UpdateGroupsData(GroupsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "UPDATE groups SET Allies = @Allies,Clients = @Clients,created_at = @created_at,Creatures = @Creatures,Description = @Description,Enemies = @Enemies,Equipment = @Equipment,Goals = @Goals,Headquarters = @Headquarters,Inventory = @Inventory,Key_items = @Key_items,Leaders = @Leaders,Locations = @Locations,Members = @Members,Motivations = @Motivations,Name = @Name,Notes = @Notes,Obstacles = @Obstacles,Offices = @Offices,Organization_structure = @Organization_structure,Other_Names = @Other_Names,Private_notes = @Private_notes,Risks = @Risks,Rivals = @Rivals,Sistergroups = @Sistergroups,Subgroups = @Subgroups,Supergroups = @Supergroups,Suppliers = @Suppliers,Tags = @Tags,Traditions = @Traditions,Universe = @Universe,updated_at = @updated_at,user_id = @user_id WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@Allies", Data.Allies);
				dbContext.AddInParameter(dbContext.cmd, "@Clients", Data.Clients);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Creatures", Data.Creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Enemies", Data.Enemies);
				dbContext.AddInParameter(dbContext.cmd, "@Equipment", Data.Equipment);
				dbContext.AddInParameter(dbContext.cmd, "@Goals", Data.Goals);
				dbContext.AddInParameter(dbContext.cmd, "@Headquarters", Data.Headquarters);
				dbContext.AddInParameter(dbContext.cmd, "@Inventory", Data.Inventory);
				dbContext.AddInParameter(dbContext.cmd, "@Key_items", Data.Key_items);
				dbContext.AddInParameter(dbContext.cmd, "@Leaders", Data.Leaders);
				dbContext.AddInParameter(dbContext.cmd, "@Locations", Data.Locations);
				dbContext.AddInParameter(dbContext.cmd, "@Members", Data.Members);
				dbContext.AddInParameter(dbContext.cmd, "@Motivations", Data.Motivations);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Obstacles", Data.Obstacles);
				dbContext.AddInParameter(dbContext.cmd, "@Offices", Data.Offices);
				dbContext.AddInParameter(dbContext.cmd, "@Organization_structure", Data.Organization_structure);
				dbContext.AddInParameter(dbContext.cmd, "@Other_Names", Data.Other_Names);
				dbContext.AddInParameter(dbContext.cmd, "@Private_notes", Data.Private_notes);
				dbContext.AddInParameter(dbContext.cmd, "@Risks", Data.Risks);
				dbContext.AddInParameter(dbContext.cmd, "@Rivals", Data.Rivals);
				dbContext.AddInParameter(dbContext.cmd, "@Sistergroups", Data.Sistergroups);
				dbContext.AddInParameter(dbContext.cmd, "@Subgroups", Data.Subgroups);
				dbContext.AddInParameter(dbContext.cmd, "@Supergroups", Data.Supergroups);
				dbContext.AddInParameter(dbContext.cmd, "@Suppliers", Data.Suppliers);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Traditions", Data.Traditions);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
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
