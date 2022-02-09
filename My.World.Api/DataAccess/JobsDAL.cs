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
	public class JobsDAL : BaseDAL
	{

		public JobsDAL()
		{
		}

		public  JobsDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteJobsData(JobsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Jobs` WHERE id = @id";
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

		public JobsModel GetJobsData(JobsModel Data)
		{
			JobsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Jobs` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new JobsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    JobsModel jobs = new JobsModel();
					jobs.Alternate_names = dr["Alternate_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Alternate_names"]);
					jobs.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					jobs.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					jobs.Education = dr["Education"] == DBNull.Value ? default(String) : Convert.ToString(dr["Education"]);
					jobs.Experience = dr["Experience"] == DBNull.Value ? default(String) : Convert.ToString(dr["Experience"]);
					jobs.Field = dr["Field"] == DBNull.Value ? default(String) : Convert.ToString(dr["Field"]);
					jobs.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					jobs.Initial_goal = dr["Initial_goal"] == DBNull.Value ? default(String) : Convert.ToString(dr["Initial_goal"]);
					jobs.Job_origin = dr["Job_origin"] == DBNull.Value ? default(String) : Convert.ToString(dr["Job_origin"]);
					jobs.Long_term_risks = dr["Long_term_risks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Long_term_risks"]);
					jobs.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					jobs.Notable_figures = dr["Notable_figures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_figures"]);
					jobs.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					jobs.Occupational_hazards = dr["Occupational_hazards"] == DBNull.Value ? default(String) : Convert.ToString(dr["Occupational_hazards"]);
					jobs.Pay_rate = dr["Pay_rate"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Pay_rate"]);
					jobs.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					jobs.Promotions = dr["Promotions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Promotions"]);
					jobs.Ranks = dr["Ranks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Ranks"]);
					jobs.Similar_jobs = dr["Similar_jobs"] == DBNull.Value ? default(String) : Convert.ToString(dr["Similar_jobs"]);
					jobs.Specializations = dr["Specializations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Specializations"]);
					jobs.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					jobs.Time_off = dr["Time_off"] == DBNull.Value ? default(String) : Convert.ToString(dr["Time_off"]);
					jobs.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					jobs.Training = dr["Training"] == DBNull.Value ? default(String) : Convert.ToString(dr["Training"]);
					jobs.Type_of_job = dr["Type_of_job"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_job"]);
					jobs.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					jobs.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					jobs.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					jobs.Vehicles = dr["Vehicles"] == DBNull.Value ? default(String) : Convert.ToString(dr["Vehicles"]);
					jobs.Work_hours = dr["Work_hours"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Work_hours"]);

					_return_value = jobs;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<JobsModel> GetAllJobsForUserID(long userId)
		{
			List<JobsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Jobs` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<JobsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						JobsModel jobs = new JobsModel();
					jobs.Alternate_names = dr["Alternate_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Alternate_names"]);
					jobs.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					jobs.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					jobs.Education = dr["Education"] == DBNull.Value ? default(String) : Convert.ToString(dr["Education"]);
					jobs.Experience = dr["Experience"] == DBNull.Value ? default(String) : Convert.ToString(dr["Experience"]);
					jobs.Field = dr["Field"] == DBNull.Value ? default(String) : Convert.ToString(dr["Field"]);
					jobs.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					jobs.Initial_goal = dr["Initial_goal"] == DBNull.Value ? default(String) : Convert.ToString(dr["Initial_goal"]);
					jobs.Job_origin = dr["Job_origin"] == DBNull.Value ? default(String) : Convert.ToString(dr["Job_origin"]);
					jobs.Long_term_risks = dr["Long_term_risks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Long_term_risks"]);
					jobs.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					jobs.Notable_figures = dr["Notable_figures"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notable_figures"]);
					jobs.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					jobs.Occupational_hazards = dr["Occupational_hazards"] == DBNull.Value ? default(String) : Convert.ToString(dr["Occupational_hazards"]);
					jobs.Pay_rate = dr["Pay_rate"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Pay_rate"]);
					jobs.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					jobs.Promotions = dr["Promotions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Promotions"]);
					jobs.Ranks = dr["Ranks"] == DBNull.Value ? default(String) : Convert.ToString(dr["Ranks"]);
					jobs.Similar_jobs = dr["Similar_jobs"] == DBNull.Value ? default(String) : Convert.ToString(dr["Similar_jobs"]);
					jobs.Specializations = dr["Specializations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Specializations"]);
					jobs.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					jobs.Time_off = dr["Time_off"] == DBNull.Value ? default(String) : Convert.ToString(dr["Time_off"]);
					jobs.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					jobs.Training = dr["Training"] == DBNull.Value ? default(String) : Convert.ToString(dr["Training"]);
					jobs.Type_of_job = dr["Type_of_job"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_job"]);
					jobs.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					jobs.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					jobs.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					jobs.Vehicles = dr["Vehicles"] == DBNull.Value ? default(String) : Convert.ToString(dr["Vehicles"]);
					jobs.Work_hours = dr["Work_hours"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Work_hours"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(jobs.id, "jobs");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    jobs.object_id = first.object_id;
						    jobs.object_name = first.object_name;
						}

						_return_value.Add(jobs);
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

		public string AddJobsData(JobsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Jobs`(`Alternate_names`,`created_at`,`Description`,`Education`,`Experience`,`Field`,`Initial_goal`,`Job_origin`,`Long_term_risks`,`Name`,`Notable_figures`,`Notes`,`Occupational_hazards`,`Pay_rate`,`Private_Notes`,`Promotions`,`Ranks`,`Similar_jobs`,`Specializations`,`Tags`,`Time_off`,`Traditions`,`Training`,`Type_of_job`,`Universe`,`updated_at`,`user_id`,`Vehicles`,`Work_hours`) VALUES(@Alternate_names,@created_at,@Description,@Education,@Experience,@Field,@Initial_goal,@Job_origin,@Long_term_risks,@Name,@Notable_figures,@Notes,@Occupational_hazards,@Pay_rate,@Private_Notes,@Promotions,@Ranks,@Similar_jobs,@Specializations,@Tags,@Time_off,@Traditions,@Training,@Type_of_job,@Universe,@updated_at,@user_id,@Vehicles,@Work_hours)";
				dbContext.AddInParameter(dbContext.cmd, "@Alternate_names", Data.Alternate_names);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Education", Data.Education);
				dbContext.AddInParameter(dbContext.cmd, "@Experience", Data.Experience);
				dbContext.AddInParameter(dbContext.cmd, "@Field", Data.Field);
				dbContext.AddInParameter(dbContext.cmd, "@Initial_goal", Data.Initial_goal);
				dbContext.AddInParameter(dbContext.cmd, "@Job_origin", Data.Job_origin);
				dbContext.AddInParameter(dbContext.cmd, "@Long_term_risks", Data.Long_term_risks);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notable_figures", Data.Notable_figures);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Occupational_hazards", Data.Occupational_hazards);
				dbContext.AddInParameter(dbContext.cmd, "@Pay_rate", Data.Pay_rate);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Promotions", Data.Promotions);
				dbContext.AddInParameter(dbContext.cmd, "@Ranks", Data.Ranks);
				dbContext.AddInParameter(dbContext.cmd, "@Similar_jobs", Data.Similar_jobs);
				dbContext.AddInParameter(dbContext.cmd, "@Specializations", Data.Specializations);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Time_off", Data.Time_off);
				dbContext.AddInParameter(dbContext.cmd, "@Traditions", Data.Traditions);
				dbContext.AddInParameter(dbContext.cmd, "@Training", Data.Training);
				dbContext.AddInParameter(dbContext.cmd, "@Type_of_job", Data.Type_of_job);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Vehicles", Data.Vehicles);
				dbContext.AddInParameter(dbContext.cmd, "@Work_hours", Data.Work_hours);
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
