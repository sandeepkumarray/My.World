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
	public class ItemsDAL : BaseDAL
	{

		public ItemsDAL()
		{
		}

		public  ItemsDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteItemsData(ItemsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Items` WHERE id = @id";
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

		public ItemsModel GetItemsData(ItemsModel Data)
		{
			ItemsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Items` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new ItemsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    ItemsModel items = new ItemsModel();
					items.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					items.Current_Owners = dr["Current_Owners"] == DBNull.Value ? default(String) : Convert.ToString(dr["Current_Owners"]);
					items.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					items.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					items.Item_Type = dr["Item_Type"] == DBNull.Value ? default(String) : Convert.ToString(dr["Item_Type"]);
					items.Magic = dr["Magic"] == DBNull.Value ? default(String) : Convert.ToString(dr["Magic"]);
					items.Magical_effects = dr["Magical_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Magical_effects"]);
					items.Makers = dr["Makers"] == DBNull.Value ? default(String) : Convert.ToString(dr["Makers"]);
					items.Materials = dr["Materials"] == DBNull.Value ? default(String) : Convert.ToString(dr["Materials"]);
					items.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					items.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					items.Original_Owners = dr["Original_Owners"] == DBNull.Value ? default(String) : Convert.ToString(dr["Original_Owners"]);
					items.Past_Owners = dr["Past_Owners"] == DBNull.Value ? default(String) : Convert.ToString(dr["Past_Owners"]);
					items.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					items.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					items.Technical_effects = dr["Technical_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Technical_effects"]);
					items.Technology = dr["Technology"] == DBNull.Value ? default(String) : Convert.ToString(dr["Technology"]);
					items.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					items.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					items.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					items.Weight = dr["Weight"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Weight"]);
					items.Year_it_was_made = dr["Year_it_was_made"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Year_it_was_made"]);

					_return_value = items;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<ItemsModel> GetAllItemsForUserID(long userId)
		{
			List<ItemsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Items` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<ItemsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						ItemsModel items = new ItemsModel();
					items.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					items.Current_Owners = dr["Current_Owners"] == DBNull.Value ? default(String) : Convert.ToString(dr["Current_Owners"]);
					items.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					items.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					items.Item_Type = dr["Item_Type"] == DBNull.Value ? default(String) : Convert.ToString(dr["Item_Type"]);
					items.Magic = dr["Magic"] == DBNull.Value ? default(String) : Convert.ToString(dr["Magic"]);
					items.Magical_effects = dr["Magical_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Magical_effects"]);
					items.Makers = dr["Makers"] == DBNull.Value ? default(String) : Convert.ToString(dr["Makers"]);
					items.Materials = dr["Materials"] == DBNull.Value ? default(String) : Convert.ToString(dr["Materials"]);
					items.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					items.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					items.Original_Owners = dr["Original_Owners"] == DBNull.Value ? default(String) : Convert.ToString(dr["Original_Owners"]);
					items.Past_Owners = dr["Past_Owners"] == DBNull.Value ? default(String) : Convert.ToString(dr["Past_Owners"]);
					items.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					items.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					items.Technical_effects = dr["Technical_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Technical_effects"]);
					items.Technology = dr["Technology"] == DBNull.Value ? default(String) : Convert.ToString(dr["Technology"]);
					items.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					items.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					items.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					items.Weight = dr["Weight"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Weight"]);
					items.Year_it_was_made = dr["Year_it_was_made"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Year_it_was_made"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(items.id, "items");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    items.object_id = first.object_id;
						    items.object_name = first.object_name;
						}

						_return_value.Add(items);
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

		public string AddItemsData(ItemsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Items`(`created_at`,`Current_Owners`,`Description`,`Item_Type`,`Magic`,`Magical_effects`,`Makers`,`Materials`,`Name`,`Notes`,`Original_Owners`,`Past_Owners`,`Private_Notes`,`Tags`,`Technical_effects`,`Technology`,`Universe`,`updated_at`,`user_id`,`Weight`,`Year_it_was_made`) VALUES(@created_at,@Current_Owners,@Description,@Item_Type,@Magic,@Magical_effects,@Makers,@Materials,@Name,@Notes,@Original_Owners,@Past_Owners,@Private_Notes,@Tags,@Technical_effects,@Technology,@Universe,@updated_at,@user_id,@Weight,@Year_it_was_made)";
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Current_Owners", Data.Current_Owners);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Item_Type", Data.Item_Type);
				dbContext.AddInParameter(dbContext.cmd, "@Magic", Data.Magic);
				dbContext.AddInParameter(dbContext.cmd, "@Magical_effects", Data.Magical_effects);
				dbContext.AddInParameter(dbContext.cmd, "@Makers", Data.Makers);
				dbContext.AddInParameter(dbContext.cmd, "@Materials", Data.Materials);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Original_Owners", Data.Original_Owners);
				dbContext.AddInParameter(dbContext.cmd, "@Past_Owners", Data.Past_Owners);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Technical_effects", Data.Technical_effects);
				dbContext.AddInParameter(dbContext.cmd, "@Technology", Data.Technology);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Weight", Data.Weight);
				dbContext.AddInParameter(dbContext.cmd, "@Year_it_was_made", Data.Year_it_was_made);
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

		public string UpdateItemsData(ItemsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "UPDATE items SET created_at = @created_at,Current_Owners = @Current_Owners,Description = @Description,Item_Type = @Item_Type,Magic = @Magic,Magical_effects = @Magical_effects,Makers = @Makers,Materials = @Materials,Name = @Name,Notes = @Notes,Original_Owners = @Original_Owners,Past_Owners = @Past_Owners,Private_Notes = @Private_Notes,Tags = @Tags,Technical_effects = @Technical_effects,Technology = @Technology,Universe = @Universe,updated_at = @updated_at,user_id = @user_id,Weight = @Weight,Year_it_was_made = @Year_it_was_made WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Current_Owners", Data.Current_Owners);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Item_Type", Data.Item_Type);
				dbContext.AddInParameter(dbContext.cmd, "@Magic", Data.Magic);
				dbContext.AddInParameter(dbContext.cmd, "@Magical_effects", Data.Magical_effects);
				dbContext.AddInParameter(dbContext.cmd, "@Makers", Data.Makers);
				dbContext.AddInParameter(dbContext.cmd, "@Materials", Data.Materials);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Original_Owners", Data.Original_Owners);
				dbContext.AddInParameter(dbContext.cmd, "@Past_Owners", Data.Past_Owners);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Technical_effects", Data.Technical_effects);
				dbContext.AddInParameter(dbContext.cmd, "@Technology", Data.Technology);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Weight", Data.Weight);
				dbContext.AddInParameter(dbContext.cmd, "@Year_it_was_made", Data.Year_it_was_made);
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
