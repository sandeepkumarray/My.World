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
	public class VehiclesDAL : BaseDAL
	{

		public VehiclesDAL()
		{
		}

		public  VehiclesDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteVehiclesData(VehiclesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Vehicles` WHERE id = @id";
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

		public VehiclesModel GetVehiclesData(VehiclesModel Data)
		{
			VehiclesModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Vehicles` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new VehiclesModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    VehiclesModel vehicles = new VehiclesModel();
					vehicles.Alternate_names = dr["Alternate_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Alternate_names"]);
					vehicles.Colors = dr["Colors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Colors"]);
					vehicles.Costs = dr["Costs"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Costs"]);
					vehicles.Country = dr["Country"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Country"]);
					vehicles.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					vehicles.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					vehicles.Designer = dr["Designer"] == DBNull.Value ? default(String) : Convert.ToString(dr["Designer"]);
					vehicles.Dimensions = dr["Dimensions"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Dimensions"]);
					vehicles.Distance = dr["Distance"] == DBNull.Value ? default(String) : Convert.ToString(dr["Distance"]);
					vehicles.Doors = dr["Doors"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Doors"]);
					vehicles.Features = dr["Features"] == DBNull.Value ? default(String) : Convert.ToString(dr["Features"]);
					vehicles.Fuel = dr["Fuel"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Fuel"]);
					vehicles.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					vehicles.Manufacturer = dr["Manufacturer"] == DBNull.Value ? default(String) : Convert.ToString(dr["Manufacturer"]);
					vehicles.Materials = dr["Materials"] == DBNull.Value ? default(String) : Convert.ToString(dr["Materials"]);
					vehicles.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					vehicles.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					vehicles.Owner = dr["Owner"] == DBNull.Value ? default(String) : Convert.ToString(dr["Owner"]);
					vehicles.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					vehicles.Safety = dr["Safety"] == DBNull.Value ? default(String) : Convert.ToString(dr["Safety"]);
					vehicles.Size = dr["Size"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Size"]);
					vehicles.Speed = dr["Speed"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Speed"]);
					vehicles.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					vehicles.Type_of_vehicle = dr["Type_of_vehicle"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_vehicle"]);
					vehicles.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					vehicles.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					vehicles.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					vehicles.Variants = dr["Variants"] == DBNull.Value ? default(String) : Convert.ToString(dr["Variants"]);
					vehicles.Weight = dr["Weight"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Weight"]);
					vehicles.Windows = dr["Windows"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Windows"]);

					_return_value = vehicles;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<VehiclesModel> GetAllVehiclesForUserID(long userId)
		{
			List<VehiclesModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Vehicles` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<VehiclesModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						VehiclesModel vehicles = new VehiclesModel();
					vehicles.Alternate_names = dr["Alternate_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Alternate_names"]);
					vehicles.Colors = dr["Colors"] == DBNull.Value ? default(String) : Convert.ToString(dr["Colors"]);
					vehicles.Costs = dr["Costs"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Costs"]);
					vehicles.Country = dr["Country"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Country"]);
					vehicles.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					vehicles.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					vehicles.Designer = dr["Designer"] == DBNull.Value ? default(String) : Convert.ToString(dr["Designer"]);
					vehicles.Dimensions = dr["Dimensions"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Dimensions"]);
					vehicles.Distance = dr["Distance"] == DBNull.Value ? default(String) : Convert.ToString(dr["Distance"]);
					vehicles.Doors = dr["Doors"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Doors"]);
					vehicles.Features = dr["Features"] == DBNull.Value ? default(String) : Convert.ToString(dr["Features"]);
					vehicles.Fuel = dr["Fuel"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Fuel"]);
					vehicles.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					vehicles.Manufacturer = dr["Manufacturer"] == DBNull.Value ? default(String) : Convert.ToString(dr["Manufacturer"]);
					vehicles.Materials = dr["Materials"] == DBNull.Value ? default(String) : Convert.ToString(dr["Materials"]);
					vehicles.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					vehicles.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					vehicles.Owner = dr["Owner"] == DBNull.Value ? default(String) : Convert.ToString(dr["Owner"]);
					vehicles.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					vehicles.Safety = dr["Safety"] == DBNull.Value ? default(String) : Convert.ToString(dr["Safety"]);
					vehicles.Size = dr["Size"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Size"]);
					vehicles.Speed = dr["Speed"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Speed"]);
					vehicles.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					vehicles.Type_of_vehicle = dr["Type_of_vehicle"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_vehicle"]);
					vehicles.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					vehicles.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					vehicles.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					vehicles.Variants = dr["Variants"] == DBNull.Value ? default(String) : Convert.ToString(dr["Variants"]);
					vehicles.Weight = dr["Weight"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Weight"]);
					vehicles.Windows = dr["Windows"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Windows"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(vehicles.id, "vehicles");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    vehicles.object_id = first.object_id;
						    vehicles.object_name = first.object_name;
						}

						_return_value.Add(vehicles);
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

		public string AddVehiclesData(VehiclesModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Vehicles`(`Alternate_names`,`Colors`,`Costs`,`Country`,`created_at`,`Description`,`Designer`,`Dimensions`,`Distance`,`Doors`,`Features`,`Fuel`,`Manufacturer`,`Materials`,`Name`,`Notes`,`Owner`,`Private_Notes`,`Safety`,`Size`,`Speed`,`Tags`,`Type_of_vehicle`,`Universe`,`updated_at`,`user_id`,`Variants`,`Weight`,`Windows`) VALUES(@Alternate_names,@Colors,@Costs,@Country,@created_at,@Description,@Designer,@Dimensions,@Distance,@Doors,@Features,@Fuel,@Manufacturer,@Materials,@Name,@Notes,@Owner,@Private_Notes,@Safety,@Size,@Speed,@Tags,@Type_of_vehicle,@Universe,@updated_at,@user_id,@Variants,@Weight,@Windows)";
				dbContext.AddInParameter(dbContext.cmd, "@Alternate_names", Data.Alternate_names);
				dbContext.AddInParameter(dbContext.cmd, "@Colors", Data.Colors);
				dbContext.AddInParameter(dbContext.cmd, "@Costs", Data.Costs);
				dbContext.AddInParameter(dbContext.cmd, "@Country", Data.Country);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Designer", Data.Designer);
				dbContext.AddInParameter(dbContext.cmd, "@Dimensions", Data.Dimensions);
				dbContext.AddInParameter(dbContext.cmd, "@Distance", Data.Distance);
				dbContext.AddInParameter(dbContext.cmd, "@Doors", Data.Doors);
				dbContext.AddInParameter(dbContext.cmd, "@Features", Data.Features);
				dbContext.AddInParameter(dbContext.cmd, "@Fuel", Data.Fuel);
				dbContext.AddInParameter(dbContext.cmd, "@Manufacturer", Data.Manufacturer);
				dbContext.AddInParameter(dbContext.cmd, "@Materials", Data.Materials);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Owner", Data.Owner);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Safety", Data.Safety);
				dbContext.AddInParameter(dbContext.cmd, "@Size", Data.Size);
				dbContext.AddInParameter(dbContext.cmd, "@Speed", Data.Speed);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Type_of_vehicle", Data.Type_of_vehicle);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Variants", Data.Variants);
				dbContext.AddInParameter(dbContext.cmd, "@Weight", Data.Weight);
				dbContext.AddInParameter(dbContext.cmd, "@Windows", Data.Windows);
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
