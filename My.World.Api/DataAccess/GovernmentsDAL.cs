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
	public class GovernmentsDAL : BaseDAL
	{

		public GovernmentsDAL()
		{
		}

		public  GovernmentsDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteGovernmentsData(GovernmentsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Governments` WHERE id = @id";
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

		public GovernmentsModel GetGovernmentsData(GovernmentsModel Data)
		{
			GovernmentsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Governments` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new GovernmentsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    GovernmentsModel governments = new GovernmentsModel();
					governments.Airforce = dr["Airforce"] == DBNull.Value ? default(String) : Convert.ToString(dr["Airforce"]);
					governments.Approval_Ratings = dr["Approval_Ratings"] == DBNull.Value ? default(String) : Convert.ToString(dr["Approval_Ratings"]);
					governments.Checks_And_Balances = dr["Checks_And_Balances"] == DBNull.Value ? default(String) : Convert.ToString(dr["Checks_And_Balances"]);
					governments.Civilian_Life = dr["Civilian_Life"] == DBNull.Value ? default(String) : Convert.ToString(dr["Civilian_Life"]);
					governments.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					governments.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					governments.Criminal_System = dr["Criminal_System"] == DBNull.Value ? default(String) : Convert.ToString(dr["Criminal_System"]);
					governments.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					governments.Electoral_Process = dr["Electoral_Process"] == DBNull.Value ? default(String) : Convert.ToString(dr["Electoral_Process"]);
					governments.Flag_Design_Story = dr["Flag_Design_Story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Flag_Design_Story"]);
					governments.Founding_Story = dr["Founding_Story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Founding_Story"]);
					governments.Geocultural = dr["Geocultural"] == DBNull.Value ? default(String) : Convert.ToString(dr["Geocultural"]);
					governments.Groups = dr["Groups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Groups"]);
					governments.Holidays = dr["Holidays"] == DBNull.Value ? default(String) : Convert.ToString(dr["Holidays"]);
					governments.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					governments.Immigration = dr["Immigration"] == DBNull.Value ? default(String) : Convert.ToString(dr["Immigration"]);
					governments.International_Relations = dr["International_Relations"] == DBNull.Value ? default(String) : Convert.ToString(dr["International_Relations"]);
					governments.Items = dr["Items"] == DBNull.Value ? default(String) : Convert.ToString(dr["Items"]);
					governments.Jobs = dr["Jobs"] == DBNull.Value ? default(String) : Convert.ToString(dr["Jobs"]);
					governments.Laws = dr["Laws"] == DBNull.Value ? default(String) : Convert.ToString(dr["Laws"]);
					governments.Leaders = dr["Leaders"] == DBNull.Value ? default(String) : Convert.ToString(dr["Leaders"]);
					governments.Military = dr["Military"] == DBNull.Value ? default(String) : Convert.ToString(dr["Military"]);
					governments.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					governments.Navy = dr["Navy"] == DBNull.Value ? default(String) : Convert.ToString(dr["Navy"]);
					governments.Notable_Wars = dr["Notable_Wars"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_Wars"]);
					governments.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					governments.Political_figures = dr["Political_figures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Political_figures"]);
					governments.Power_Source = dr["Power_Source"] == DBNull.Value ? default(String) : Convert.ToString(dr["Power_Source"]);
					governments.Power_Structure = dr["Power_Structure"] == DBNull.Value ? default(String) : Convert.ToString(dr["Power_Structure"]);
					governments.Privacy_Ideologies = dr["Privacy_Ideologies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Privacy_Ideologies"]);
					governments.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					governments.Socioeconomical = dr["Socioeconomical"] == DBNull.Value ? default(String) : Convert.ToString(dr["Socioeconomical"]);
					governments.Sociopolitical = dr["Sociopolitical"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sociopolitical"]);
					governments.Space_Program = dr["Space_Program"] == DBNull.Value ? default(String) : Convert.ToString(dr["Space_Program"]);
					governments.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					governments.Technologies = dr["Technologies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Technologies"]);
					governments.Term_Lengths = dr["Term_Lengths"] == DBNull.Value ? default(String) : Convert.ToString(dr["Term_Lengths"]);
					governments.Type_Of_Government = dr["Type_Of_Government"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_Of_Government"]);
					governments.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					governments.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					governments.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					governments.Vehicles = dr["Vehicles"] == DBNull.Value ? default(String) : Convert.ToString(dr["Vehicles"]);

					_return_value = governments;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<GovernmentsModel> GetAllGovernmentsForUserID(long userId)
		{
			List<GovernmentsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Governments` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<GovernmentsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						GovernmentsModel governments = new GovernmentsModel();
					governments.Airforce = dr["Airforce"] == DBNull.Value ? default(String) : Convert.ToString(dr["Airforce"]);
					governments.Approval_Ratings = dr["Approval_Ratings"] == DBNull.Value ? default(String) : Convert.ToString(dr["Approval_Ratings"]);
					governments.Checks_And_Balances = dr["Checks_And_Balances"] == DBNull.Value ? default(String) : Convert.ToString(dr["Checks_And_Balances"]);
					governments.Civilian_Life = dr["Civilian_Life"] == DBNull.Value ? default(String) : Convert.ToString(dr["Civilian_Life"]);
					governments.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					governments.Creatures = dr["Creatures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Creatures"]);
					governments.Criminal_System = dr["Criminal_System"] == DBNull.Value ? default(String) : Convert.ToString(dr["Criminal_System"]);
					governments.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					governments.Electoral_Process = dr["Electoral_Process"] == DBNull.Value ? default(String) : Convert.ToString(dr["Electoral_Process"]);
					governments.Flag_Design_Story = dr["Flag_Design_Story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Flag_Design_Story"]);
					governments.Founding_Story = dr["Founding_Story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Founding_Story"]);
					governments.Geocultural = dr["Geocultural"] == DBNull.Value ? default(String) : Convert.ToString(dr["Geocultural"]);
					governments.Groups = dr["Groups"] == DBNull.Value ? default(String) : Convert.ToString(dr["Groups"]);
					governments.Holidays = dr["Holidays"] == DBNull.Value ? default(String) : Convert.ToString(dr["Holidays"]);
					governments.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					governments.Immigration = dr["Immigration"] == DBNull.Value ? default(String) : Convert.ToString(dr["Immigration"]);
					governments.International_Relations = dr["International_Relations"] == DBNull.Value ? default(String) : Convert.ToString(dr["International_Relations"]);
					governments.Items = dr["Items"] == DBNull.Value ? default(String) : Convert.ToString(dr["Items"]);
					governments.Jobs = dr["Jobs"] == DBNull.Value ? default(String) : Convert.ToString(dr["Jobs"]);
					governments.Laws = dr["Laws"] == DBNull.Value ? default(String) : Convert.ToString(dr["Laws"]);
					governments.Leaders = dr["Leaders"] == DBNull.Value ? default(String) : Convert.ToString(dr["Leaders"]);
					governments.Military = dr["Military"] == DBNull.Value ? default(String) : Convert.ToString(dr["Military"]);
					governments.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					governments.Navy = dr["Navy"] == DBNull.Value ? default(String) : Convert.ToString(dr["Navy"]);
					governments.Notable_Wars = dr["Notable_Wars"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_Wars"]);
					governments.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					governments.Political_figures = dr["Political_figures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Political_figures"]);
					governments.Power_Source = dr["Power_Source"] == DBNull.Value ? default(String) : Convert.ToString(dr["Power_Source"]);
					governments.Power_Structure = dr["Power_Structure"] == DBNull.Value ? default(String) : Convert.ToString(dr["Power_Structure"]);
					governments.Privacy_Ideologies = dr["Privacy_Ideologies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Privacy_Ideologies"]);
					governments.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					governments.Socioeconomical = dr["Socioeconomical"] == DBNull.Value ? default(String) : Convert.ToString(dr["Socioeconomical"]);
					governments.Sociopolitical = dr["Sociopolitical"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sociopolitical"]);
					governments.Space_Program = dr["Space_Program"] == DBNull.Value ? default(String) : Convert.ToString(dr["Space_Program"]);
					governments.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					governments.Technologies = dr["Technologies"] == DBNull.Value ? default(String) : Convert.ToString(dr["Technologies"]);
					governments.Term_Lengths = dr["Term_Lengths"] == DBNull.Value ? default(String) : Convert.ToString(dr["Term_Lengths"]);
					governments.Type_Of_Government = dr["Type_Of_Government"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_Of_Government"]);
					governments.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					governments.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					governments.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					governments.Vehicles = dr["Vehicles"] == DBNull.Value ? default(String) : Convert.ToString(dr["Vehicles"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(governments.id, "governments");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    governments.object_id = first.object_id;
						    governments.object_name = first.object_name;
						}

						_return_value.Add(governments);
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

		public string AddGovernmentsData(GovernmentsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Governments`(`Airforce`,`Approval_Ratings`,`Checks_And_Balances`,`Civilian_Life`,`created_at`,`Creatures`,`Criminal_System`,`Description`,`Electoral_Process`,`Flag_Design_Story`,`Founding_Story`,`Geocultural`,`Groups`,`Holidays`,`Immigration`,`International_Relations`,`Items`,`Jobs`,`Laws`,`Leaders`,`Military`,`Name`,`Navy`,`Notable_Wars`,`Notes`,`Political_figures`,`Power_Source`,`Power_Structure`,`Privacy_Ideologies`,`Private_Notes`,`Socioeconomical`,`Sociopolitical`,`Space_Program`,`Tags`,`Technologies`,`Term_Lengths`,`Type_Of_Government`,`Universe`,`updated_at`,`user_id`,`Vehicles`) VALUES(@Airforce,@Approval_Ratings,@Checks_And_Balances,@Civilian_Life,@created_at,@Creatures,@Criminal_System,@Description,@Electoral_Process,@Flag_Design_Story,@Founding_Story,@Geocultural,@Groups,@Holidays,@Immigration,@International_Relations,@Items,@Jobs,@Laws,@Leaders,@Military,@Name,@Navy,@Notable_Wars,@Notes,@Political_figures,@Power_Source,@Power_Structure,@Privacy_Ideologies,@Private_Notes,@Socioeconomical,@Sociopolitical,@Space_Program,@Tags,@Technologies,@Term_Lengths,@Type_Of_Government,@Universe,@updated_at,@user_id,@Vehicles)";
				dbContext.AddInParameter(dbContext.cmd, "@Airforce", Data.Airforce);
				dbContext.AddInParameter(dbContext.cmd, "@Approval_Ratings", Data.Approval_Ratings);
				dbContext.AddInParameter(dbContext.cmd, "@Checks_And_Balances", Data.Checks_And_Balances);
				dbContext.AddInParameter(dbContext.cmd, "@Civilian_Life", Data.Civilian_Life);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Creatures", Data.Creatures);
				dbContext.AddInParameter(dbContext.cmd, "@Criminal_System", Data.Criminal_System);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Electoral_Process", Data.Electoral_Process);
				dbContext.AddInParameter(dbContext.cmd, "@Flag_Design_Story", Data.Flag_Design_Story);
				dbContext.AddInParameter(dbContext.cmd, "@Founding_Story", Data.Founding_Story);
				dbContext.AddInParameter(dbContext.cmd, "@Geocultural", Data.Geocultural);
				dbContext.AddInParameter(dbContext.cmd, "@Groups", Data.Groups);
				dbContext.AddInParameter(dbContext.cmd, "@Holidays", Data.Holidays);
				dbContext.AddInParameter(dbContext.cmd, "@Immigration", Data.Immigration);
				dbContext.AddInParameter(dbContext.cmd, "@International_Relations", Data.International_Relations);
				dbContext.AddInParameter(dbContext.cmd, "@Items", Data.Items);
				dbContext.AddInParameter(dbContext.cmd, "@Jobs", Data.Jobs);
				dbContext.AddInParameter(dbContext.cmd, "@Laws", Data.Laws);
				dbContext.AddInParameter(dbContext.cmd, "@Leaders", Data.Leaders);
				dbContext.AddInParameter(dbContext.cmd, "@Military", Data.Military);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Navy", Data.Navy);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_Wars", Data.Notable_Wars);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Political_figures", Data.Political_figures);
				dbContext.AddInParameter(dbContext.cmd, "@Power_Source", Data.Power_Source);
				dbContext.AddInParameter(dbContext.cmd, "@Power_Structure", Data.Power_Structure);
				dbContext.AddInParameter(dbContext.cmd, "@Privacy_Ideologies", Data.Privacy_Ideologies);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Socioeconomical", Data.Socioeconomical);
				dbContext.AddInParameter(dbContext.cmd, "@Sociopolitical", Data.Sociopolitical);
				dbContext.AddInParameter(dbContext.cmd, "@Space_Program", Data.Space_Program);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Technologies", Data.Technologies);
				dbContext.AddInParameter(dbContext.cmd, "@Term_Lengths", Data.Term_Lengths);
				dbContext.AddInParameter(dbContext.cmd, "@Type_Of_Government", Data.Type_Of_Government);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
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
