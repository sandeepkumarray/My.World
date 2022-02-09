using Microsoft.AspNetCore.Http;
using My.World.Api.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace My.World.Api.DataAccess
{
    public class BaseDAL
    {
        public DBContext dbContext = null;
        public BaseDAL()
        {

        }

        public string GetStringDataFromByteArray(byte[] data)
        {
            string return_value = null;
            if (data != null)
            {
                return_value = Encoding.Default.GetString(data);
            }
            return return_value;
        }

        public string SaveData(BaseModel Data)
        {
            string _return_value = string.Empty;
            string model_Type = Data.GetType().Name.Replace("Model", "");
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "UPDATE `" + model_Type + "` SET `" + Data.column_type + "` = '" + Data.column_value + "' ,updated_at=@updated_at WHERE id = @id";

                dbContext.AddInParameter(dbContext.cmd, "@id", Data._id);
                dbContext.AddInParameter(dbContext.cmd, "@updated_at", DateTime.Now);

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