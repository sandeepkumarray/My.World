using My.World.Api.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Api.DataAccess
{
    public class DashboardDAL : BaseDAL
    {

        public DashboardDAL()
        {
        }

        public DashboardDAL(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DashboardModel GetDashboardData(long user_Id)
        {
            DashboardModel dashboard = null;
            try
            {
                dbContext.cmd = new MySqlCommand();

                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "get_dashboard";
                dbContext.cmd.CommandType = CommandType.StoredProcedure;

                #region assign Parameters
                dbContext.AddInParameter(dbContext.cmd, "p_user_id", user_Id, true);
                #endregion


                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dashboard = new DashboardModel();
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.Rows[0];

                    dashboard.User_Id = user_Id;
                    dashboard.buildings_total = dr["buildings_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["buildings_total"]);
                    dashboard.characters_total = dr["characters_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["characters_total"]);
                    dashboard.conditions_total = dr["conditions_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["conditions_total"]);
                    dashboard.continents_total = dr["continents_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["continents_total"]);
                    dashboard.countries_total = dr["countries_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["countries_total"]);
                    dashboard.creatures_total = dr["creatures_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["creatures_total"]);
                    dashboard.deities_total = dr["deities_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["deities_total"]);
                    dashboard.floras_total = dr["floras_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["floras_total"]);
                    dashboard.foods_total = dr["foods_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["foods_total"]);
                    dashboard.governments_total = dr["governments_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["governments_total"]);
                    dashboard.groups_total = dr["groups_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["groups_total"]);
                    dashboard.items_total = dr["items_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["items_total"]);
                    dashboard.jobs_total = dr["jobs_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["jobs_total"]);
                    dashboard.landmarks_total = dr["landmarks_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["landmarks_total"]);
                    dashboard.languages_total = dr["languages_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["languages_total"]);
                    dashboard.locations_total = dr["locations_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["locations_total"]);
                    dashboard.lores_total = dr["lores_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["lores_total"]);
                    dashboard.magics_total = dr["magics_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["magics_total"]);
                    dashboard.planets_total = dr["planets_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["planets_total"]);
                    dashboard.races_total = dr["races_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["races_total"]);
                    dashboard.religions_total = dr["religions_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["religions_total"]);
                    dashboard.scenes_total = dr["scenes_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["scenes_total"]);
                    dashboard.sports_total = dr["sports_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["sports_total"]);
                    dashboard.technologies_total = dr["technologies_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["technologies_total"]);
                    dashboard.towns_total = dr["towns_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["towns_total"]);
                    dashboard.traditions_total = dr["traditions_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["traditions_total"]);
                    dashboard.universes_total = dr["universes_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["universes_total"]);
                    dashboard.vehicles_total = dr["vehicles_total"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["vehicles_total"]);


                }
            }
            catch (Exception)
            {

                throw;
            }
            return dashboard;
        }

        public List<MentionsModel> GetMentionsData(long user_Id)
        {
            List<MentionsModel> mentionsList = null;

            string query = "select id, name ,'buildings' content_type from buildings where user_id = @user_Id union all " +
                            "select id, name ,'characters' content_type from characters where user_id = @user_Id union all " +
                            "select id, name ,'conditions' content_type from conditions where user_id = @user_Id union all " +
                            "select id, Local_name name ,'continents' content_type from continents where user_id = @user_Id union all " +
                            "select id, name ,'countries' content_type from countries where user_id = @user_Id union all " +
                            "select id, name ,'creatures' content_type from creatures where user_id = @user_Id union all " +
                            "select id, name ,'deities' content_type from deities where user_id = @user_Id union all " +
                            "select id, name ,'floras' content_type from floras where user_id = @user_Id union all " +
                            "select id, name ,'foods' content_type from foods where user_id = @user_Id union all " +
                            "select id, name ,'governments' content_type from governments where user_id = @user_Id union all " +
                            "select id, name ,'my_book.groups' content_type from my_book.groups where user_id = @user_Id union all " +
                            "select id, name ,'items' content_type from items where user_id = @user_Id union all " +
                            "select id, name ,'jobs' content_type from jobs where user_id = @user_Id union all " +
                            "select id, name ,'landmarks' content_type from landmarks where user_id = @user_Id union all " +
                            "select id, name ,'languages' content_type from languages where user_id = @user_Id union all " +
                            "select id, name ,'locations' content_type from locations where user_id = @user_Id union all " +
                            "select id, name ,'lores' content_type from lores where user_id = @user_Id union all " +
                            "select id, name ,'magics' content_type from magics where user_id = @user_Id union all " +
                            "select id, name ,'planets' content_type from planets where user_id = @user_Id union all " +
                            "select id, name ,'races' content_type from races where user_id = @user_Id union all " +
                            "select id, name ,'religions' content_type from religions where user_id = @user_Id union all " +
                            "select id, name ,'scenes' content_type from scenes where user_id = @user_Id union all " +
                            "select id, name ,'sports' content_type from sports where user_id = @user_Id union all " +
                            "select id, name ,'technologies' content_type from technologies where user_id = @user_Id union all " +
                            "select id, name ,'towns' content_type from towns where user_id = @user_Id union all " +
                            "select id, name ,'traditions' content_type from traditions where user_id = @user_Id union all " +
                            "select id, name ,'universes' content_type from universes where user_id = @user_Id union all " +
                            "select id, name ,'vehicles' content_type from vehicles where user_id = @user_Id ";
            dbContext.cmd = new MySqlCommand();

            dbContext.cmd.Connection = dbContext.GetConnection();
            dbContext.cmd.CommandText = query;

            #region assign Parameters
            dbContext.AddInParameter(dbContext.cmd, "@user_Id", user_Id, true);
            #endregion


            DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                mentionsList = new List<MentionsModel>();
                DataTable dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    MentionsModel mentionModel = new MentionsModel();
                    mentionModel.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
                    mentionModel.name = dr["name"] == DBNull.Value ? default(String) : Convert.ToString(dr["name"]);
                    mentionModel.content_type = dr["content_type"] == DBNull.Value ? default(String) : Convert.ToString(dr["content_type"]);
                    mentionsList.Add(mentionModel);
                }
            }
            return mentionsList;
        }

        public List<DashboardRecentModel> GetRecentData(long user_Id)
        {
            List<DashboardRecentModel> recentsList = null;

            string query = "select tb.id,updated_at,tb.name,'buildings' content_type, ct.icon, ct.primary_color from buildings tb join content_types ct where ct.name = 'Buildings' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'characters' content_type, ct.icon, ct.primary_color from characters tb join content_types ct where ct.name = 'Characters' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'conditions' content_type, ct.icon, ct.primary_color from conditions tb join content_types ct where ct.name = 'Conditions' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.Local_name name,'continents' content_type, ct.icon, ct.primary_color from continents tb join content_types ct where ct.name = 'Continents' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'countries' content_type, ct.icon, ct.primary_color from countries tb join content_types ct where ct.name = 'Countries' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'creatures' content_type, ct.icon, ct.primary_color from creatures tb join content_types ct where ct.name = 'Creatures' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'deities' content_type, ct.icon, ct.primary_color from deities tb join content_types ct where ct.name = 'Deities' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'floras' content_type, ct.icon, ct.primary_color from floras tb join content_types ct where ct.name = 'Floras' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'foods' content_type, ct.icon, ct.primary_color from foods tb join content_types ct where ct.name = 'Foods' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'governments' content_type, ct.icon, ct.primary_color from governments tb join content_types ct where ct.name = 'Governments' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'my_book.groups' content_type, ct.icon, ct.primary_color from my_book.groups tb join content_types ct where ct.name = 'Groups' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'items' content_type, ct.icon, ct.primary_color from items tb join content_types ct where ct.name = 'Items' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'jobs' content_type, ct.icon, ct.primary_color from jobs tb join content_types ct where ct.name = 'Jobs' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'landmarks' content_type, ct.icon, ct.primary_color from landmarks tb join content_types ct where ct.name = 'Landmarks' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'languages' content_type, ct.icon, ct.primary_color from languages tb join content_types ct where ct.name = 'Languages' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'locations' content_type, ct.icon, ct.primary_color from locations tb join content_types ct where ct.name = 'Locations' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'lores' content_type, ct.icon, ct.primary_color from lores tb join content_types ct where ct.name = 'Lores' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'magics' content_type, ct.icon, ct.primary_color from magics tb join content_types ct where ct.name = 'Magics' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'planets' content_type, ct.icon, ct.primary_color from planets tb join content_types ct where ct.name = 'Planets' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'races' content_type, ct.icon, ct.primary_color from races tb join content_types ct where ct.name = 'Races' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'religions' content_type, ct.icon, ct.primary_color from religions tb join content_types ct where ct.name = 'Religions' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'scenes' content_type, ct.icon, ct.primary_color from scenes tb join content_types ct where ct.name = 'Scenes' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'sports' content_type, ct.icon, ct.primary_color from sports tb join content_types ct where ct.name = 'Sports' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'technologies' content_type, ct.icon, ct.primary_color from technologies tb join content_types ct where ct.name = 'Technologies' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'towns' content_type, ct.icon, ct.primary_color from towns tb join content_types ct where ct.name = 'Towns' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'traditions' content_type, ct.icon, ct.primary_color from traditions tb join content_types ct where ct.name = 'Traditions' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'universes' content_type, ct.icon, ct.primary_color from universes tb join content_types ct where ct.name = 'Universes' and user_id = @user_Id union all " +
                            "select tb.id,updated_at,tb.name,'vehicles' content_type, ct.icon, ct.primary_color from vehicles tb join content_types ct where ct.name = 'Vehicles' and user_id = @user_Id  ";

            dbContext.cmd = new MySqlCommand();

            dbContext.cmd.Connection = dbContext.GetConnection();
            dbContext.cmd.CommandText = query;

            #region assign Parameters
            dbContext.AddInParameter(dbContext.cmd, "@user_Id", user_Id, true);
            #endregion


            DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                recentsList = new List<DashboardRecentModel>();

                DataTable dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    DashboardRecentModel recentModel = new DashboardRecentModel();
                    recentModel.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
                    recentModel.name = dr["name"] == DBNull.Value ? default(String) : Convert.ToString(dr["name"]);
                    recentModel.content_type = dr["content_type"] == DBNull.Value ? default(String) : Convert.ToString(dr["content_type"]);
                    recentModel.icon = dr["icon"] == DBNull.Value ? default(String) : Convert.ToString(dr["icon"]);
                    recentModel.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);

                    recentModel.primary_color = dr["primary_color"] == DBNull.Value ? default(String) : Convert.ToString(dr["primary_color"]);

                    recentsList.Add(recentModel);
                }
            }
            return recentsList;
        }

        public List<ContentTypesModel> GetAllContentTypes()
        {
            List<ContentTypesModel> _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM content_types;";

                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new List<ContentTypesModel>();
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        ContentTypesModel contenttypes = new ContentTypesModel();
                        contenttypes.created_by = dr["created_by"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["created_by"]);
                        contenttypes.created_date = dr["created_date"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_date"]);
                        contenttypes.icon = dr["icon"] == DBNull.Value ? default(String) : Convert.ToString(dr["icon"]);
                        contenttypes.id = dr["id"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["id"]);
                        contenttypes.name = dr["name"] == DBNull.Value ? default(String) : Convert.ToString(dr["name"]);
                        contenttypes.primary_color = dr["primary_color"] == DBNull.Value ? default(String) : Convert.ToString(dr["primary_color"]);
                        contenttypes.sec_color = dr["sec_color"] == DBNull.Value ? default(String) : Convert.ToString(dr["sec_color"]);

                        _return_value.Add(contenttypes);
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

    }
}
