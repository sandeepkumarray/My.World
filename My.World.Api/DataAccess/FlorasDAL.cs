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
	public class FlorasDAL : BaseDAL
	{

		public FlorasDAL()
		{
		}

		public  FlorasDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteFlorasData(FlorasModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Floras` WHERE id = @id";
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

		public FlorasModel GetFlorasData(FlorasModel Data)
		{
			FlorasModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Floras` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new FlorasModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    FlorasModel floras = new FlorasModel();
					floras.Berries = dr["Berries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Berries"]);
					floras.Colorings = dr["Colorings"] == DBNull.Value ? default(String) : Convert.ToString(dr["Colorings"]);
					floras.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					floras.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					floras.Eaten_by = dr["Eaten_by"] == DBNull.Value ? default(String) : Convert.ToString(dr["Eaten_by"]);
					floras.Family = dr["Family"] == DBNull.Value ? default(String) : Convert.ToString(dr["Family"]);
					floras.Fruits = dr["Fruits"] == DBNull.Value ? default(String) : Convert.ToString(dr["Fruits"]);
					floras.Genus = dr["Genus"] == DBNull.Value ? default(String) : Convert.ToString(dr["Genus"]);
					floras.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					floras.Locations = dr["Locations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Locations"]);
					floras.Magical_effects = dr["Magical_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Magical_effects"]);
					floras.Material_uses = dr["Material_uses"] == DBNull.Value ? default(String) : Convert.ToString(dr["Material_uses"]);
					floras.Medicinal_purposes = dr["Medicinal_purposes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Medicinal_purposes"]);
					floras.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					floras.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					floras.Nuts = dr["Nuts"] == DBNull.Value ? default(String) : Convert.ToString(dr["Nuts"]);
					floras.Order = dr["Order"] == DBNull.Value ? default(String) : Convert.ToString(dr["Order"]);
					floras.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					floras.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					floras.Related_flora = dr["Related_flora"] == DBNull.Value ? default(String) : Convert.ToString(dr["Related_flora"]);
					floras.Reproduction = dr["Reproduction"] == DBNull.Value ? default(String) : Convert.ToString(dr["Reproduction"]);
					floras.Seasonality = dr["Seasonality"] == DBNull.Value ? default(String) : Convert.ToString(dr["Seasonality"]);
					floras.Seeds = dr["Seeds"] == DBNull.Value ? default(String) : Convert.ToString(dr["Seeds"]);
					floras.Size = dr["Size"] == DBNull.Value ? default(String) : Convert.ToString(dr["Size"]);
					floras.Smell = dr["Smell"] == DBNull.Value ? default(String) : Convert.ToString(dr["Smell"]);
					floras.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					floras.Taste = dr["Taste"] == DBNull.Value ? default(String) : Convert.ToString(dr["Taste"]);
					floras.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					floras.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					floras.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);

					_return_value = floras;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<FlorasModel> GetAllFlorasForUserID(long userId)
		{
			List<FlorasModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Floras` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<FlorasModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						FlorasModel floras = new FlorasModel();
					floras.Berries = dr["Berries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Berries"]);
					floras.Colorings = dr["Colorings"] == DBNull.Value ? default(String) : Convert.ToString(dr["Colorings"]);
					floras.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					floras.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					floras.Eaten_by = dr["Eaten_by"] == DBNull.Value ? default(String) : Convert.ToString(dr["Eaten_by"]);
					floras.Family = dr["Family"] == DBNull.Value ? default(String) : Convert.ToString(dr["Family"]);
					floras.Fruits = dr["Fruits"] == DBNull.Value ? default(String) : Convert.ToString(dr["Fruits"]);
					floras.Genus = dr["Genus"] == DBNull.Value ? default(String) : Convert.ToString(dr["Genus"]);
					floras.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					floras.Locations = dr["Locations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Locations"]);
					floras.Magical_effects = dr["Magical_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Magical_effects"]);
					floras.Material_uses = dr["Material_uses"] == DBNull.Value ? default(String) : Convert.ToString(dr["Material_uses"]);
					floras.Medicinal_purposes = dr["Medicinal_purposes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Medicinal_purposes"]);
					floras.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					floras.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					floras.Nuts = dr["Nuts"] == DBNull.Value ? default(String) : Convert.ToString(dr["Nuts"]);
					floras.Order = dr["Order"] == DBNull.Value ? default(String) : Convert.ToString(dr["Order"]);
					floras.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					floras.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					floras.Related_flora = dr["Related_flora"] == DBNull.Value ? default(String) : Convert.ToString(dr["Related_flora"]);
					floras.Reproduction = dr["Reproduction"] == DBNull.Value ? default(String) : Convert.ToString(dr["Reproduction"]);
					floras.Seasonality = dr["Seasonality"] == DBNull.Value ? default(String) : Convert.ToString(dr["Seasonality"]);
					floras.Seeds = dr["Seeds"] == DBNull.Value ? default(String) : Convert.ToString(dr["Seeds"]);
					floras.Size = dr["Size"] == DBNull.Value ? default(String) : Convert.ToString(dr["Size"]);
					floras.Smell = dr["Smell"] == DBNull.Value ? default(String) : Convert.ToString(dr["Smell"]);
					floras.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					floras.Taste = dr["Taste"] == DBNull.Value ? default(String) : Convert.ToString(dr["Taste"]);
					floras.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					floras.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					floras.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(floras.id, "floras");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    floras.object_id = first.object_id;
						    floras.object_name = first.object_name;
						}

						_return_value.Add(floras);
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

		public string AddFlorasData(FlorasModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Floras`(`Berries`,`Colorings`,`created_at`,`Description`,`Eaten_by`,`Family`,`Fruits`,`Genus`,`Locations`,`Magical_effects`,`Material_uses`,`Medicinal_purposes`,`Name`,`Notes`,`Nuts`,`Order`,`Other_Names`,`Private_Notes`,`Related_flora`,`Reproduction`,`Seasonality`,`Seeds`,`Size`,`Smell`,`Tags`,`Taste`,`Universe`,`updated_at`,`user_id`) VALUES(@Berries,@Colorings,@created_at,@Description,@Eaten_by,@Family,@Fruits,@Genus,@Locations,@Magical_effects,@Material_uses,@Medicinal_purposes,@Name,@Notes,@Nuts,@Order,@Other_Names,@Private_Notes,@Related_flora,@Reproduction,@Seasonality,@Seeds,@Size,@Smell,@Tags,@Taste,@Universe,@updated_at,@user_id)";
				dbContext.AddInParameter(dbContext.cmd, "@Berries", Data.Berries);
				dbContext.AddInParameter(dbContext.cmd, "@Colorings", Data.Colorings);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Eaten_by", Data.Eaten_by);
				dbContext.AddInParameter(dbContext.cmd, "@Family", Data.Family);
				dbContext.AddInParameter(dbContext.cmd, "@Fruits", Data.Fruits);
				dbContext.AddInParameter(dbContext.cmd, "@Genus", Data.Genus);
				dbContext.AddInParameter(dbContext.cmd, "@Locations", Data.Locations);
				dbContext.AddInParameter(dbContext.cmd, "@Magical_effects", Data.Magical_effects);
				dbContext.AddInParameter(dbContext.cmd, "@Material_uses", Data.Material_uses);
				dbContext.AddInParameter(dbContext.cmd, "@Medicinal_purposes", Data.Medicinal_purposes);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Nuts", Data.Nuts);
				dbContext.AddInParameter(dbContext.cmd, "@Order", Data.Order);
				dbContext.AddInParameter(dbContext.cmd, "@Other_Names", Data.Other_Names);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Related_flora", Data.Related_flora);
				dbContext.AddInParameter(dbContext.cmd, "@Reproduction", Data.Reproduction);
				dbContext.AddInParameter(dbContext.cmd, "@Seasonality", Data.Seasonality);
				dbContext.AddInParameter(dbContext.cmd, "@Seeds", Data.Seeds);
				dbContext.AddInParameter(dbContext.cmd, "@Size", Data.Size);
				dbContext.AddInParameter(dbContext.cmd, "@Smell", Data.Smell);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Taste", Data.Taste);
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

	}
}
