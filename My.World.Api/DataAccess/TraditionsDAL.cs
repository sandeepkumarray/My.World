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
	public class TraditionsDAL : BaseDAL
	{

		public TraditionsDAL()
		{
		}

		public  TraditionsDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteTraditionsData(TraditionsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Traditions` WHERE id = @id";
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

		public TraditionsModel GetTraditionsData(TraditionsModel Data)
		{
			TraditionsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Traditions` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new TraditionsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    TraditionsModel traditions = new TraditionsModel();
					traditions.Activities = dr["Activities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Activities"]);
					traditions.Alternate_names = dr["Alternate_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Alternate_names"]);
					traditions.Countries = dr["Countries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Countries"]);
					traditions.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					traditions.Dates = dr["Dates"] == DBNull.Value ? default(String) : Convert.ToString(dr["Dates"]);
					traditions.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					traditions.Etymology = dr["Etymology"] == DBNull.Value ? default(String) : Convert.ToString(dr["Etymology"]);
					traditions.Food = dr["Food"] == DBNull.Value ? default(String) : Convert.ToString(dr["Food"]);
					traditions.Games = dr["Games"] == DBNull.Value ? default(String) : Convert.ToString(dr["Games"]);
					traditions.Gifts = dr["Gifts"] == DBNull.Value ? default(String) : Convert.ToString(dr["Gifts"]);
					traditions.Groups = dr["Groups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Groups"]);
					traditions.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					traditions.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					traditions.Notable_events = dr["Notable_events"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_events"]);
					traditions.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					traditions.Origin = dr["Origin"] == DBNull.Value ? default(String) : Convert.ToString(dr["Origin"]);
					traditions.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					traditions.Religions = dr["Religions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Religions"]);
					traditions.Significance = dr["Significance"] == DBNull.Value ? default(String) : Convert.ToString(dr["Significance"]);
					traditions.Symbolism = dr["Symbolism"] == DBNull.Value ? default(String) : Convert.ToString(dr["Symbolism"]);
					traditions.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					traditions.Towns = dr["Towns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Towns"]);
					traditions.Type_of_tradition = dr["Type_of_tradition"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_tradition"]);
					traditions.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					traditions.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					traditions.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);

					_return_value = traditions;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<TraditionsModel> GetAllTraditionsForUserID(long userId)
		{
			List<TraditionsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Traditions` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<TraditionsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						TraditionsModel traditions = new TraditionsModel();
					traditions.Activities = dr["Activities"] == DBNull.Value ? default(String) : Convert.ToString(dr["Activities"]);
					traditions.Alternate_names = dr["Alternate_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Alternate_names"]);
					traditions.Countries = dr["Countries"] == DBNull.Value ? default(String) : Convert.ToString(dr["Countries"]);
					traditions.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					traditions.Dates = dr["Dates"] == DBNull.Value ? default(String) : Convert.ToString(dr["Dates"]);
					traditions.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					traditions.Etymology = dr["Etymology"] == DBNull.Value ? default(String) : Convert.ToString(dr["Etymology"]);
					traditions.Food = dr["Food"] == DBNull.Value ? default(String) : Convert.ToString(dr["Food"]);
					traditions.Games = dr["Games"] == DBNull.Value ? default(String) : Convert.ToString(dr["Games"]);
					traditions.Gifts = dr["Gifts"] == DBNull.Value ? default(String) : Convert.ToString(dr["Gifts"]);
					traditions.Groups = dr["Groups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Groups"]);
					traditions.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					traditions.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					traditions.Notable_events = dr["Notable_events"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_events"]);
					traditions.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					traditions.Origin = dr["Origin"] == DBNull.Value ? default(String) : Convert.ToString(dr["Origin"]);
					traditions.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					traditions.Religions = dr["Religions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Religions"]);
					traditions.Significance = dr["Significance"] == DBNull.Value ? default(String) : Convert.ToString(dr["Significance"]);
					traditions.Symbolism = dr["Symbolism"] == DBNull.Value ? default(String) : Convert.ToString(dr["Symbolism"]);
					traditions.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					traditions.Towns = dr["Towns"] == DBNull.Value ? default(String) : Convert.ToString(dr["Towns"]);
					traditions.Type_of_tradition = dr["Type_of_tradition"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_tradition"]);
					traditions.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					traditions.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					traditions.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(traditions.id, "traditions");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    traditions.object_id = first.object_id;
						    traditions.object_name = first.object_name;
						}

						_return_value.Add(traditions);
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

		public string AddTraditionsData(TraditionsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Traditions`(`Activities`,`Alternate_names`,`Countries`,`created_at`,`Dates`,`Description`,`Etymology`,`Food`,`Games`,`Gifts`,`Groups`,`Name`,`Notable_events`,`Notes`,`Origin`,`Private_Notes`,`Religions`,`Significance`,`Symbolism`,`Tags`,`Towns`,`Type_of_tradition`,`Universe`,`updated_at`,`user_id`) VALUES(@Activities,@Alternate_names,@Countries,@created_at,@Dates,@Description,@Etymology,@Food,@Games,@Gifts,@Groups,@Name,@Notable_events,@Notes,@Origin,@Private_Notes,@Religions,@Significance,@Symbolism,@Tags,@Towns,@Type_of_tradition,@Universe,@updated_at,@user_id)";
				dbContext.AddInParameter(dbContext.cmd, "@Activities", Data.Activities);
				dbContext.AddInParameter(dbContext.cmd, "@Alternate_names", Data.Alternate_names);
				dbContext.AddInParameter(dbContext.cmd, "@Countries", Data.Countries);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Dates", Data.Dates);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Etymology", Data.Etymology);
				dbContext.AddInParameter(dbContext.cmd, "@Food", Data.Food);
				dbContext.AddInParameter(dbContext.cmd, "@Games", Data.Games);
				dbContext.AddInParameter(dbContext.cmd, "@Gifts", Data.Gifts);
				dbContext.AddInParameter(dbContext.cmd, "@Groups", Data.Groups);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_events", Data.Notable_events);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Origin", Data.Origin);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Religions", Data.Religions);
				dbContext.AddInParameter(dbContext.cmd, "@Significance", Data.Significance);
				dbContext.AddInParameter(dbContext.cmd, "@Symbolism", Data.Symbolism);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Towns", Data.Towns);
				dbContext.AddInParameter(dbContext.cmd, "@Type_of_tradition", Data.Type_of_tradition);
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
