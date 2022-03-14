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
    public class AppConfigDAL : BaseDAL
    {

        public AppConfigDAL()
        {
        }

        public AppConfigDAL(DBContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public string DeleteAppConfigData(AppConfigModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "DELETE FROM `App_Config` WHERE id = @id";
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

        public AppConfigModel GetAppConfigData(AppConfigModel Data)
        {
            AppConfigModel _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM `App_Config` WHERE id = @id";
                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new AppConfigModel();
                    DataTable dt = ds.Tables[0];

                    DataRow dr = dt.Rows[0];

                    AppConfigModel appconfig = new AppConfigModel();
                    appconfig.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
                    appconfig.key = dr["key"] == DBNull.Value ? default(String) : Convert.ToString(dr["key"]);
                    appconfig.value = dr["value"] == DBNull.Value ? default(String) : Convert.ToString(dr["value"]);
                    appconfig.isactive = dr["isactive"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["isactive"]);

                    _return_value = appconfig;
                }
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;

        }

        public List<AppConfigModel> GetAllAppConfigForUserID()
        {
            List<AppConfigModel> _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM `App_Config`;";
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new List<AppConfigModel>();
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        AppConfigModel appconfig = new AppConfigModel();
                        appconfig.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
                        appconfig.key = dr["key"] == DBNull.Value ? default(String) : Convert.ToString(dr["key"]);
                        appconfig.value = dr["value"] == DBNull.Value ? default(String) : Convert.ToString(dr["value"]);
                        appconfig.isactive = dr["isactive"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["isactive"]);

                        _return_value.Add(appconfig);
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

        public string AddAppConfigData(AppConfigModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "INSERT INTO `App_Config`(`key`,`value`,`isactive`) VALUES(@key,@value,@isactive)";
                dbContext.AddInParameter(dbContext.cmd, "@key", Data.key);
                dbContext.AddInParameter(dbContext.cmd, "@value", Data.value);
                dbContext.AddInParameter(dbContext.cmd, "@isactive", Data.isactive);
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

        public string UpdateAppConfigData(AppConfigModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "UPDATE `App_Config` set `value` = @value where `key` = @key";
                dbContext.AddInParameter(dbContext.cmd, "@key", Data.key);
                dbContext.AddInParameter(dbContext.cmd, "@value", Data.value);

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
