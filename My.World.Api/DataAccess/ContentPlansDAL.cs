using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using My.World.Api.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace My.World.Api.DataAccess
{
    public class ContentPlansDAL : BaseDAL
    {

        public ContentPlansDAL()
        {
        }

        public ContentPlansDAL(DBContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public string AddContentPlansData(ContentPlansModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "INSERT INTO Content_Plans(id,name,plan_template,plan_description,buildings_count,characters_count,conditions_count,continents_count,countries_count,creatures_count,deities_count,floras_count,foods_count,governments_count,groups_count,items_count,jobs_count,landmarks_count,languages_count,locations_count,lores_count,magics_count,planets_count,races_count,religions_count,scenes_count,sports_count,technologies_count,towns_count,traditions_count,universes_count,vehicles_count,created_by,created_date) VALUES(@id,@name,@plan_template,@plan_description,@buildings_count,@characters_count,@conditions_count,@continents_count,@countries_count,@creatures_count,@deities_count,@floras_count,@foods_count,@governments_count,@groups_count,@items_count,@jobs_count,@landmarks_count,@languages_count,@locations_count,@lores_count,@magics_count,@planets_count,@races_count,@religions_count,@scenes_count,@sports_count,@technologies_count,@towns_count,@traditions_count,@universes_count,@vehicles_count,@created_by,@created_date)";
                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
                dbContext.AddInParameter(dbContext.cmd, "@name", Data.name);
                dbContext.AddInParameter(dbContext.cmd, "@plan_template", Data.plan_template);
                dbContext.AddInParameter(dbContext.cmd, "@plan_description", Data.plan_description);
                dbContext.AddInParameter(dbContext.cmd, "@buildings_count", Data.buildings_count);
                dbContext.AddInParameter(dbContext.cmd, "@characters_count", Data.characters_count);
                dbContext.AddInParameter(dbContext.cmd, "@conditions_count", Data.conditions_count);
                dbContext.AddInParameter(dbContext.cmd, "@continents_count", Data.continents_count);
                dbContext.AddInParameter(dbContext.cmd, "@countries_count", Data.countries_count);
                dbContext.AddInParameter(dbContext.cmd, "@creatures_count", Data.creatures_count);
                dbContext.AddInParameter(dbContext.cmd, "@deities_count", Data.deities_count);
                dbContext.AddInParameter(dbContext.cmd, "@floras_count", Data.floras_count);
                dbContext.AddInParameter(dbContext.cmd, "@foods_count", Data.foods_count);
                dbContext.AddInParameter(dbContext.cmd, "@governments_count", Data.governments_count);
                dbContext.AddInParameter(dbContext.cmd, "@groups_count", Data.groups_count);
                dbContext.AddInParameter(dbContext.cmd, "@items_count", Data.items_count);
                dbContext.AddInParameter(dbContext.cmd, "@jobs_count", Data.jobs_count);
                dbContext.AddInParameter(dbContext.cmd, "@landmarks_count", Data.landmarks_count);
                dbContext.AddInParameter(dbContext.cmd, "@languages_count", Data.languages_count);
                dbContext.AddInParameter(dbContext.cmd, "@locations_count", Data.locations_count);
                dbContext.AddInParameter(dbContext.cmd, "@lores_count", Data.lores_count);
                dbContext.AddInParameter(dbContext.cmd, "@magics_count", Data.magics_count);
                dbContext.AddInParameter(dbContext.cmd, "@planets_count", Data.planets_count);
                dbContext.AddInParameter(dbContext.cmd, "@races_count", Data.races_count);
                dbContext.AddInParameter(dbContext.cmd, "@religions_count", Data.religions_count);
                dbContext.AddInParameter(dbContext.cmd, "@scenes_count", Data.scenes_count);
                dbContext.AddInParameter(dbContext.cmd, "@sports_count", Data.sports_count);
                dbContext.AddInParameter(dbContext.cmd, "@technologies_count", Data.technologies_count);
                dbContext.AddInParameter(dbContext.cmd, "@towns_count", Data.towns_count);
                dbContext.AddInParameter(dbContext.cmd, "@traditions_count", Data.traditions_count);
                dbContext.AddInParameter(dbContext.cmd, "@universes_count", Data.universes_count);
                dbContext.AddInParameter(dbContext.cmd, "@vehicles_count", Data.vehicles_count);
                dbContext.AddInParameter(dbContext.cmd, "@created_by", Data.created_by);
                dbContext.AddInParameter(dbContext.cmd, "@created_date", Data.created_date);
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

        public string DeleteContentPlansData(ContentPlansModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "DELETE FROM Content_Plans WHERE id = @id";
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

        public ContentPlansModel GetContentPlansData(ContentPlansModel Data)
        {
            ContentPlansModel _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM Content_Plans WHERE id = @id";
                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new ContentPlansModel();
                    DataTable dt = ds.Tables[0];

                    DataRow dr = dt.Rows[0];

                    ContentPlansModel contentplans = new ContentPlansModel();
                    contentplans.id = dr["id"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["id"]);
                    contentplans.name = dr["name"] == DBNull.Value ? default(String) : Convert.ToString(dr["name"]);
                    contentplans.plan_template = dr["plan_template"] == DBNull.Value ? default(String) : Convert.ToString(dr["plan_template"]);
                    contentplans.plan_description = dr["plan_description"] == DBNull.Value ? default(String) : Convert.ToString(dr["plan_description"]);
                    contentplans.buildings_count = dr["buildings_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["buildings_count"]);
                    contentplans.characters_count = dr["characters_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["characters_count"]);
                    contentplans.conditions_count = dr["conditions_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["conditions_count"]);
                    contentplans.continents_count = dr["continents_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["continents_count"]);
                    contentplans.countries_count = dr["countries_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["countries_count"]);
                    contentplans.creatures_count = dr["creatures_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["creatures_count"]);
                    contentplans.deities_count = dr["deities_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["deities_count"]);
                    contentplans.floras_count = dr["floras_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["floras_count"]);
                    contentplans.foods_count = dr["foods_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["foods_count"]);
                    contentplans.governments_count = dr["governments_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["governments_count"]);
                    contentplans.groups_count = dr["groups_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["groups_count"]);
                    contentplans.items_count = dr["items_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["items_count"]);
                    contentplans.jobs_count = dr["jobs_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["jobs_count"]);
                    contentplans.landmarks_count = dr["landmarks_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["landmarks_count"]);
                    contentplans.languages_count = dr["languages_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["languages_count"]);
                    contentplans.locations_count = dr["locations_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["locations_count"]);
                    contentplans.lores_count = dr["lores_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["lores_count"]);
                    contentplans.magics_count = dr["magics_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["magics_count"]);
                    contentplans.planets_count = dr["planets_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["planets_count"]);
                    contentplans.races_count = dr["races_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["races_count"]);
                    contentplans.religions_count = dr["religions_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["religions_count"]);
                    contentplans.scenes_count = dr["scenes_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["scenes_count"]);
                    contentplans.sports_count = dr["sports_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["sports_count"]);
                    contentplans.technologies_count = dr["technologies_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["technologies_count"]);
                    contentplans.towns_count = dr["towns_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["towns_count"]);
                    contentplans.traditions_count = dr["traditions_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["traditions_count"]);
                    contentplans.universes_count = dr["universes_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["universes_count"]);
                    contentplans.vehicles_count = dr["vehicles_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["vehicles_count"]);
                    contentplans.created_by = dr["created_by"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["created_by"]);
                    contentplans.created_date = dr["created_date"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_date"]);

                    _return_value = contentplans;
                }
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;

        }

        public List<ContentPlansModel> SelectAllContentPlansData()
        {
            List<ContentPlansModel> _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM Content_Plans;";
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new List<ContentPlansModel>();
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        ContentPlansModel contentplans = new ContentPlansModel();
                        contentplans.id = dr["id"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["id"]);
                        contentplans.name = dr["name"] == DBNull.Value ? default(String) : Convert.ToString(dr["name"]);
                        contentplans.available = dr["available"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["available"]);
                        contentplans.monthly_cents = dr["monthly_cents"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["monthly_cents"]);
                        contentplans.plan_template = dr["plan_template"] == DBNull.Value ? default(String) : Convert.ToString(dr["plan_template"]);
                        contentplans.plan_description = dr["plan_description"] == DBNull.Value ? default(String) : Convert.ToString(dr["plan_description"]);
                        contentplans.buildings_count = dr["buildings_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["buildings_count"]);
                        contentplans.characters_count = dr["characters_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["characters_count"]);
                        contentplans.conditions_count = dr["conditions_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["conditions_count"]);
                        contentplans.continents_count = dr["continents_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["continents_count"]);
                        contentplans.countries_count = dr["countries_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["countries_count"]);
                        contentplans.creatures_count = dr["creatures_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["creatures_count"]);
                        contentplans.deities_count = dr["deities_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["deities_count"]);
                        contentplans.floras_count = dr["floras_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["floras_count"]);
                        contentplans.foods_count = dr["foods_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["foods_count"]);
                        contentplans.governments_count = dr["governments_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["governments_count"]);
                        contentplans.groups_count = dr["groups_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["groups_count"]);
                        contentplans.items_count = dr["items_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["items_count"]);
                        contentplans.jobs_count = dr["jobs_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["jobs_count"]);
                        contentplans.landmarks_count = dr["landmarks_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["landmarks_count"]);
                        contentplans.languages_count = dr["languages_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["languages_count"]);
                        contentplans.locations_count = dr["locations_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["locations_count"]);
                        contentplans.lores_count = dr["lores_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["lores_count"]);
                        contentplans.magics_count = dr["magics_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["magics_count"]);
                        contentplans.planets_count = dr["planets_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["planets_count"]);
                        contentplans.races_count = dr["races_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["races_count"]);
                        contentplans.religions_count = dr["religions_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["religions_count"]);
                        contentplans.scenes_count = dr["scenes_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["scenes_count"]);
                        contentplans.sports_count = dr["sports_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["sports_count"]);
                        contentplans.technologies_count = dr["technologies_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["technologies_count"]);
                        contentplans.towns_count = dr["towns_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["towns_count"]);
                        contentplans.traditions_count = dr["traditions_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["traditions_count"]);
                        contentplans.universes_count = dr["universes_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["universes_count"]);
                        contentplans.vehicles_count = dr["vehicles_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["vehicles_count"]);
                        contentplans.created_by = dr["created_by"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["created_by"]);
                        contentplans.created_date = dr["created_date"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_date"]);

                        _return_value.Add(contentplans);
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

        public string UpdateContentPlansData(ContentPlansModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "UPDATE Content_Plans SET id = @id,name = @name,plan_template = @plan_template,plan_description = @plan_description,buildings_count = @buildings_count,characters_count = @characters_count,conditions_count = @conditions_count,continents_count = @continents_count,countries_count = @countries_count,creatures_count = @creatures_count,deities_count = @deities_count,floras_count = @floras_count,foods_count = @foods_count,governments_count = @governments_count,groups_count = @groups_count,items_count = @items_count,jobs_count = @jobs_count,landmarks_count = @landmarks_count,languages_count = @languages_count,locations_count = @locations_count,lores_count = @lores_count,magics_count = @magics_count,planets_count = @planets_count,races_count = @races_count,religions_count = @religions_count,scenes_count = @scenes_count,sports_count = @sports_count,technologies_count = @technologies_count,towns_count = @towns_count,traditions_count = @traditions_count,universes_count = @universes_count,vehicles_count = @vehicles_count,created_by = @created_by,created_date = @created_date WHERE id = @id";
                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
                dbContext.AddInParameter(dbContext.cmd, "@name", Data.name);
                dbContext.AddInParameter(dbContext.cmd, "@plan_template", Data.plan_template);
                dbContext.AddInParameter(dbContext.cmd, "@plan_description", Data.plan_description);
                dbContext.AddInParameter(dbContext.cmd, "@buildings_count", Data.buildings_count);
                dbContext.AddInParameter(dbContext.cmd, "@characters_count", Data.characters_count);
                dbContext.AddInParameter(dbContext.cmd, "@conditions_count", Data.conditions_count);
                dbContext.AddInParameter(dbContext.cmd, "@continents_count", Data.continents_count);
                dbContext.AddInParameter(dbContext.cmd, "@countries_count", Data.countries_count);
                dbContext.AddInParameter(dbContext.cmd, "@creatures_count", Data.creatures_count);
                dbContext.AddInParameter(dbContext.cmd, "@deities_count", Data.deities_count);
                dbContext.AddInParameter(dbContext.cmd, "@floras_count", Data.floras_count);
                dbContext.AddInParameter(dbContext.cmd, "@foods_count", Data.foods_count);
                dbContext.AddInParameter(dbContext.cmd, "@governments_count", Data.governments_count);
                dbContext.AddInParameter(dbContext.cmd, "@groups_count", Data.groups_count);
                dbContext.AddInParameter(dbContext.cmd, "@items_count", Data.items_count);
                dbContext.AddInParameter(dbContext.cmd, "@jobs_count", Data.jobs_count);
                dbContext.AddInParameter(dbContext.cmd, "@landmarks_count", Data.landmarks_count);
                dbContext.AddInParameter(dbContext.cmd, "@languages_count", Data.languages_count);
                dbContext.AddInParameter(dbContext.cmd, "@locations_count", Data.locations_count);
                dbContext.AddInParameter(dbContext.cmd, "@lores_count", Data.lores_count);
                dbContext.AddInParameter(dbContext.cmd, "@magics_count", Data.magics_count);
                dbContext.AddInParameter(dbContext.cmd, "@planets_count", Data.planets_count);
                dbContext.AddInParameter(dbContext.cmd, "@races_count", Data.races_count);
                dbContext.AddInParameter(dbContext.cmd, "@religions_count", Data.religions_count);
                dbContext.AddInParameter(dbContext.cmd, "@scenes_count", Data.scenes_count);
                dbContext.AddInParameter(dbContext.cmd, "@sports_count", Data.sports_count);
                dbContext.AddInParameter(dbContext.cmd, "@technologies_count", Data.technologies_count);
                dbContext.AddInParameter(dbContext.cmd, "@towns_count", Data.towns_count);
                dbContext.AddInParameter(dbContext.cmd, "@traditions_count", Data.traditions_count);
                dbContext.AddInParameter(dbContext.cmd, "@universes_count", Data.universes_count);
                dbContext.AddInParameter(dbContext.cmd, "@vehicles_count", Data.vehicles_count);
                dbContext.AddInParameter(dbContext.cmd, "@created_by", Data.created_by);
                dbContext.AddInParameter(dbContext.cmd, "@created_date", Data.created_date);
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
