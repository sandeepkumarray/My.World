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
    public class UsersDal : BaseDAL
    {

        public UsersDal()
        {
        }

        public UsersDal(DBContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public string SignupUser(UsersModel Data)
        {
            string _return_value = string.Empty;

            try
            {
                dbContext.cmd = new MySqlCommand();

                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "signup_User";
                dbContext.cmd.CommandType = CommandType.StoredProcedure;

                #region assign Parameters
                dbContext.AddInParameter(dbContext.cmd, "email", Data.email, true);
                dbContext.AddInParameter(dbContext.cmd, "encrypted_password", Utility.Encrypt(Data.password), true);
                dbContext.AddOutParameter(dbContext.cmd, "id", MySqlDbType.Int32, 0);

                #endregion

                dbContext.cmd.ExecuteNonQuery();
                int ID = dbContext.cmd.Parameters["id"].Value == DBNull.Value ? 0 : Convert.ToInt32(dbContext.cmd.Parameters["id"].Value);

                if (ID > 0)
                {
                    dbContext.cmd = new MySqlCommand();

                    dbContext.cmd.Connection = dbContext.GetConnection();
                    dbContext.cmd.CommandText = "create_user_template";
                    dbContext.cmd.CommandType = CommandType.StoredProcedure;

                    dbContext.AddInParameter(dbContext.cmd, "p_user_id", ID, true);
                    dbContext.cmd.ExecuteNonQuery();
                }
                _return_value = Convert.ToString(ID);
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;
        }

        public UsersModel LoginUser(UsersModel Data)
        {
            UsersModel _return_value = null;

            try
            {
                //UserModel usr = new UserModel();
                //string usrjson = usr.ToJson();
                dbContext.cmd = new MySqlCommand();

                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "login_User";
                dbContext.cmd.CommandType = CommandType.StoredProcedure;

                #region assign Parameters
                dbContext.AddInParameter(dbContext.cmd, "p_username", Data.username, true);
                dbContext.AddInParameter(dbContext.cmd, "p_encrypted_password", Utility.Encrypt(Data.password), true);
                dbContext.AddOutParameter(dbContext.cmd, "p_id", MySqlDbType.Int32, 0);

                #endregion

                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new UsersModel();
                    DataTable dt = ds.Tables[0];

                    DataRow dr = dt.Rows[0];
                    _return_value.age = dr["age"] == DBNull.Value ? default(String) : Convert.ToString(dr["age"]);
                    _return_value.bio = dr["bio"] == DBNull.Value ? default(String) : Convert.ToString(dr["bio"]);
                    _return_value.community_features_enabled = dr["community_features_enabled"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["community_features_enabled"]);
                    _return_value.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
                    _return_value.current_sign_in_at = dr["current_sign_in_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["current_sign_in_at"]);
                    _return_value.current_sign_in_ip = dr["current_sign_in_ip"] == DBNull.Value ? default(String) : Convert.ToString(dr["current_sign_in_ip"]);
                    _return_value.dark_mode_enabled = dr["dark_mode_enabled"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["dark_mode_enabled"]);
                    _return_value.deleted_at = dr["deleted_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["deleted_at"]);
                    _return_value.email = dr["email"] == DBNull.Value ? default(String) : Convert.ToString(dr["email"]);
                    _return_value.email_updates = dr["email_updates"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["email_updates"]);
                    _return_value.email_confirm = dr["email_confirm"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["email_confirm"]);
                    _return_value.encrypted_password = dr["encrypted_password"] == DBNull.Value ? default(String) : Convert.ToString(dr["encrypted_password"]);
                    _return_value.favorite_author = dr["favorite_author"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_author"]);
                    _return_value.favorite_book = dr["favorite_book"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_book"]);
                    _return_value.favorite_genre = dr["favorite_genre"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_genre"]);
                    _return_value.favorite_page_type = dr["favorite_page_type"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_page_type"]);
                    _return_value.favorite_quote = dr["favorite_quote"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_quote"]);
                    _return_value.fluid_preference = dr["fluid_preference"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["fluid_preference"]);
                    _return_value.forum_administrator = dr["forum_administrator"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["forum_administrator"]);
                    _return_value.forum_moderator = dr["forum_moderator"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["forum_moderator"]);
                    _return_value.forums_badge_text = dr["forums_badge_text"] == DBNull.Value ? default(String) : Convert.ToString(dr["forums_badge_text"]);
                    _return_value.gender = dr["gender"] == DBNull.Value ? default(String) : Convert.ToString(dr["gender"]);
                    _return_value.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
                    _return_value.inspirations = dr["inspirations"] == DBNull.Value ? default(String) : Convert.ToString(dr["inspirations"]);
                    _return_value.interests = dr["interests"] == DBNull.Value ? default(String) : Convert.ToString(dr["interests"]);
                    _return_value.keyboard_shortcuts_preference = dr["keyboard_shortcuts_preference"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["keyboard_shortcuts_preference"]);
                    _return_value.last_sign_in_at = dr["last_sign_in_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["last_sign_in_at"]);
                    _return_value.last_sign_in_ip = dr["last_sign_in_ip"] == DBNull.Value ? default(String) : Convert.ToString(dr["last_sign_in_ip"]);
                    _return_value.location = dr["location"] == DBNull.Value ? default(String) : Convert.ToString(dr["location"]);
                    _return_value.name = dr["name"] == DBNull.Value ? default(String) : Convert.ToString(dr["name"]);
                    _return_value.notification_updates = dr["notification_updates"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["notification_updates"]);
                    _return_value.occupation = dr["occupation"] == DBNull.Value ? default(String) : Convert.ToString(dr["occupation"]);
                    _return_value.old_password = dr["old_password"] == DBNull.Value ? default(String) : Convert.ToString(dr["old_password"]);
                    _return_value.other_names = dr["other_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["other_names"]);
                    _return_value.site_template = dr["site_template"] == DBNull.Value ? default(String) : Convert.ToString(dr["site_template"]);
                    _return_value.private_profile = dr["private_profile"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["private_profile"]);
                    _return_value.remember_created_at = dr["remember_created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["remember_created_at"]);
                    _return_value.reset_password_sent_at = dr["reset_password_sent_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["reset_password_sent_at"]);
                    _return_value.reset_password_token = dr["reset_password_token"] == DBNull.Value ? default(String) : Convert.ToString(dr["reset_password_token"]);
                    _return_value.secure_code = dr["secure_code"] == DBNull.Value ? default(String) : Convert.ToString(dr["secure_code"]);
                    _return_value.selected_billing_plan_id = dr["selected_billing_plan_id"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["selected_billing_plan_id"]);
                    _return_value.sign_in_count = dr["sign_in_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["sign_in_count"]);
                    _return_value.site_administrator = dr["site_administrator"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["site_administrator"]);
                    _return_value.stripe_customer_id = dr["stripe_customer_id"] == DBNull.Value ? default(String) : Convert.ToString(dr["stripe_customer_id"]);
                    _return_value.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
                    _return_value.upload_bandwidth_kb = dr["upload_bandwidth_kb"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["upload_bandwidth_kb"]);
                    _return_value.username = dr["username"] == DBNull.Value ? default(String) : Convert.ToString(dr["username"]);
                    _return_value.website = dr["website"] == DBNull.Value ? default(String) : Convert.ToString(dr["website"]);

                    _return_value.user_plan = GetUserPlan(_return_value.id);
                    _return_value.content_template = GetUserContentTemplate(_return_value.id);

                }
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;
        }

        public string AddUsersData(UsersModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "INSERT INTO Users(age,bio,community_features_enabled,created_at,current_sign_in_at,current_sign_in_ip,dark_mode_enabled,deleted_at,email,email_updates,encrypted_password,favorite_author,favorite_book,favorite_genre,favorite_page_type,favorite_quote,fluid_preference,forum_administrator,forum_moderator,forums_badge_text,gender,id,inspirations,interests,keyboard_shortcuts_preference,last_sign_in_at,last_sign_in_ip,location,name,notification_updates,occupation,old_password,other_names,site_template,private_profile,remember_created_at,reset_password_sent_at,reset_password_token,secure_code,selected_billing_plan_id,sign_in_count,site_administrator,stripe_customer_id,updated_at,upload_bandwidth_kb,username,website) VALUES(@age,@bio,@community_features_enabled,@created_at,@current_sign_in_at,@current_sign_in_ip,@dark_mode_enabled,@deleted_at,@email,@email_updates,@encrypted_password,@favorite_author,@favorite_book,@favorite_genre,@favorite_page_type,@favorite_quote,@fluid_preference,@forum_administrator,@forum_moderator,@forums_badge_text,@gender,@id,@inspirations,@interests,@keyboard_shortcuts_preference,@last_sign_in_at,@last_sign_in_ip,@location,@name,@notification_updates,@occupation,@old_password,@other_names,@site_template,@private_profile,@remember_created_at,@reset_password_sent_at,@reset_password_token,@secure_code,@selected_billing_plan_id,@sign_in_count,@site_administrator,@stripe_customer_id,@updated_at,@upload_bandwidth_kb,@username,@website)";
                dbContext.AddInParameter(dbContext.cmd, "@age", Data.age);
                dbContext.AddInParameter(dbContext.cmd, "@bio", Data.bio);
                dbContext.AddInParameter(dbContext.cmd, "@community_features_enabled", Data.community_features_enabled);
                dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
                dbContext.AddInParameter(dbContext.cmd, "@current_sign_in_at", Data.current_sign_in_at);
                dbContext.AddInParameter(dbContext.cmd, "@current_sign_in_ip", Data.current_sign_in_ip);
                dbContext.AddInParameter(dbContext.cmd, "@dark_mode_enabled", Data.dark_mode_enabled);
                dbContext.AddInParameter(dbContext.cmd, "@deleted_at", Data.deleted_at);
                dbContext.AddInParameter(dbContext.cmd, "@email", Data.email);
                dbContext.AddInParameter(dbContext.cmd, "@email_updates", Data.email_updates);
                dbContext.AddInParameter(dbContext.cmd, "@encrypted_password", Data.encrypted_password);
                dbContext.AddInParameter(dbContext.cmd, "@favorite_author", Data.favorite_author);
                dbContext.AddInParameter(dbContext.cmd, "@favorite_book", Data.favorite_book);
                dbContext.AddInParameter(dbContext.cmd, "@favorite_genre", Data.favorite_genre);
                dbContext.AddInParameter(dbContext.cmd, "@favorite_page_type", Data.favorite_page_type);
                dbContext.AddInParameter(dbContext.cmd, "@favorite_quote", Data.favorite_quote);
                dbContext.AddInParameter(dbContext.cmd, "@fluid_preference", Data.fluid_preference);
                dbContext.AddInParameter(dbContext.cmd, "@forum_administrator", Data.forum_administrator);
                dbContext.AddInParameter(dbContext.cmd, "@forum_moderator", Data.forum_moderator);
                dbContext.AddInParameter(dbContext.cmd, "@forums_badge_text", Data.forums_badge_text);
                dbContext.AddInParameter(dbContext.cmd, "@gender", Data.gender);
                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
                dbContext.AddInParameter(dbContext.cmd, "@inspirations", Data.inspirations);
                dbContext.AddInParameter(dbContext.cmd, "@interests", Data.interests);
                dbContext.AddInParameter(dbContext.cmd, "@keyboard_shortcuts_preference", Data.keyboard_shortcuts_preference);
                dbContext.AddInParameter(dbContext.cmd, "@last_sign_in_at", Data.last_sign_in_at);
                dbContext.AddInParameter(dbContext.cmd, "@last_sign_in_ip", Data.last_sign_in_ip);
                dbContext.AddInParameter(dbContext.cmd, "@location", Data.location);
                dbContext.AddInParameter(dbContext.cmd, "@name", Data.name);
                dbContext.AddInParameter(dbContext.cmd, "@notification_updates", Data.notification_updates);
                dbContext.AddInParameter(dbContext.cmd, "@occupation", Data.occupation);
                dbContext.AddInParameter(dbContext.cmd, "@old_password", Data.old_password);
                dbContext.AddInParameter(dbContext.cmd, "@other_names", Data.other_names);
                dbContext.AddInParameter(dbContext.cmd, "@site_template", Data.site_template);
                dbContext.AddInParameter(dbContext.cmd, "@private_profile", Data.private_profile);
                dbContext.AddInParameter(dbContext.cmd, "@remember_created_at", Data.remember_created_at);
                dbContext.AddInParameter(dbContext.cmd, "@reset_password_sent_at", Data.reset_password_sent_at);
                dbContext.AddInParameter(dbContext.cmd, "@reset_password_token", Data.reset_password_token);
                dbContext.AddInParameter(dbContext.cmd, "@secure_code", Data.secure_code);
                dbContext.AddInParameter(dbContext.cmd, "@selected_billing_plan_id", Data.selected_billing_plan_id);
                dbContext.AddInParameter(dbContext.cmd, "@sign_in_count", Data.sign_in_count);
                dbContext.AddInParameter(dbContext.cmd, "@site_administrator", Data.site_administrator);
                dbContext.AddInParameter(dbContext.cmd, "@stripe_customer_id", Data.stripe_customer_id);
                dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
                dbContext.AddInParameter(dbContext.cmd, "@upload_bandwidth_kb", Data.upload_bandwidth_kb);
                dbContext.AddInParameter(dbContext.cmd, "@username", Data.username);
                dbContext.AddInParameter(dbContext.cmd, "@website", Data.website);
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

        public string DeleteUsersData(UsersModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "DELETE FROM Users WHERE id = @id";
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

        public UsersModel GetUsersData(UsersModel Data)
        {
            UsersModel _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM Users WHERE id = @id";
                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new UsersModel();
                    DataTable dt = ds.Tables[0];

                    DataRow dr = dt.Rows[0];

                    UsersModel users = new UsersModel();
                    users.age = dr["age"] == DBNull.Value ? default(String) : Convert.ToString(dr["age"]);
                    users.bio = dr["bio"] == DBNull.Value ? default(String) : Convert.ToString(dr["bio"]);
                    users.community_features_enabled = dr["community_features_enabled"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["community_features_enabled"]);
                    users.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
                    users.current_sign_in_at = dr["current_sign_in_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["current_sign_in_at"]);
                    users.current_sign_in_ip = dr["current_sign_in_ip"] == DBNull.Value ? default(String) : Convert.ToString(dr["current_sign_in_ip"]);
                    users.dark_mode_enabled = dr["dark_mode_enabled"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["dark_mode_enabled"]);
                    users.deleted_at = dr["deleted_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["deleted_at"]);
                    users.email = dr["email"] == DBNull.Value ? default(String) : Convert.ToString(dr["email"]);
                    users.email_updates = dr["email_updates"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["email_updates"]);
                    users.email_confirm = dr["email_confirm"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["email_confirm"]);
                    users.encrypted_password = dr["encrypted_password"] == DBNull.Value ? default(String) : Convert.ToString(dr["encrypted_password"]);
                    users.favorite_author = dr["favorite_author"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_author"]);
                    users.favorite_book = dr["favorite_book"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_book"]);
                    users.favorite_genre = dr["favorite_genre"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_genre"]);
                    users.favorite_page_type = dr["favorite_page_type"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_page_type"]);
                    users.favorite_quote = dr["favorite_quote"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_quote"]);
                    users.fluid_preference = dr["fluid_preference"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["fluid_preference"]);
                    users.forum_administrator = dr["forum_administrator"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["forum_administrator"]);
                    users.forum_moderator = dr["forum_moderator"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["forum_moderator"]);
                    users.forums_badge_text = dr["forums_badge_text"] == DBNull.Value ? default(String) : Convert.ToString(dr["forums_badge_text"]);
                    users.gender = dr["gender"] == DBNull.Value ? default(String) : Convert.ToString(dr["gender"]);
                    users.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
                    users.inspirations = dr["inspirations"] == DBNull.Value ? default(String) : Convert.ToString(dr["inspirations"]);
                    users.interests = dr["interests"] == DBNull.Value ? default(String) : Convert.ToString(dr["interests"]);
                    users.keyboard_shortcuts_preference = dr["keyboard_shortcuts_preference"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["keyboard_shortcuts_preference"]);
                    users.last_sign_in_at = dr["last_sign_in_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["last_sign_in_at"]);
                    users.last_sign_in_ip = dr["last_sign_in_ip"] == DBNull.Value ? default(String) : Convert.ToString(dr["last_sign_in_ip"]);
                    users.location = dr["location"] == DBNull.Value ? default(String) : Convert.ToString(dr["location"]);
                    users.name = dr["name"] == DBNull.Value ? default(String) : Convert.ToString(dr["name"]);
                    users.notification_updates = dr["notification_updates"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["notification_updates"]);
                    users.occupation = dr["occupation"] == DBNull.Value ? default(String) : Convert.ToString(dr["occupation"]);
                    users.old_password = dr["old_password"] == DBNull.Value ? default(String) : Convert.ToString(dr["old_password"]);
                    users.other_names = dr["other_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["other_names"]);
                    users.site_template = dr["site_template"] == DBNull.Value ? default(String) : Convert.ToString(dr["site_template"]);
                    users.private_profile = dr["private_profile"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["private_profile"]);
                    users.remember_created_at = dr["remember_created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["remember_created_at"]);
                    users.reset_password_sent_at = dr["reset_password_sent_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["reset_password_sent_at"]);
                    users.reset_password_token = dr["reset_password_token"] == DBNull.Value ? default(String) : Convert.ToString(dr["reset_password_token"]);
                    users.secure_code = dr["secure_code"] == DBNull.Value ? default(String) : Convert.ToString(dr["secure_code"]);
                    users.selected_billing_plan_id = dr["selected_billing_plan_id"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["selected_billing_plan_id"]);
                    users.sign_in_count = dr["sign_in_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["sign_in_count"]);
                    users.site_administrator = dr["site_administrator"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["site_administrator"]);
                    users.stripe_customer_id = dr["stripe_customer_id"] == DBNull.Value ? default(String) : Convert.ToString(dr["stripe_customer_id"]);
                    users.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
                    users.upload_bandwidth_kb = dr["upload_bandwidth_kb"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["upload_bandwidth_kb"]);
                    users.username = dr["username"] == DBNull.Value ? default(String) : Convert.ToString(dr["username"]);
                    users.website = dr["website"] == DBNull.Value ? default(String) : Convert.ToString(dr["website"]);

                    users.user_plan = GetUserPlan(users.id);

                    _return_value = users;
                }
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;

        }

        public UsersModel GetUsersDataByEmail(UsersModel Data)
        {
            UsersModel _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM Users WHERE email = @email";
                dbContext.AddInParameter(dbContext.cmd, "@email", Data.email);
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new UsersModel();
                    DataTable dt = ds.Tables[0];

                    DataRow dr = dt.Rows[0];

                    UsersModel users = new UsersModel();
                    users.age = dr["age"] == DBNull.Value ? default(String) : Convert.ToString(dr["age"]);
                    users.bio = dr["bio"] == DBNull.Value ? default(String) : Convert.ToString(dr["bio"]);
                    users.community_features_enabled = dr["community_features_enabled"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["community_features_enabled"]);
                    users.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
                    users.current_sign_in_at = dr["current_sign_in_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["current_sign_in_at"]);
                    users.current_sign_in_ip = dr["current_sign_in_ip"] == DBNull.Value ? default(String) : Convert.ToString(dr["current_sign_in_ip"]);
                    users.dark_mode_enabled = dr["dark_mode_enabled"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["dark_mode_enabled"]);
                    users.deleted_at = dr["deleted_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["deleted_at"]);
                    users.email = dr["email"] == DBNull.Value ? default(String) : Convert.ToString(dr["email"]);
                    users.email_updates = dr["email_updates"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["email_updates"]);
                    users.email_confirm = dr["email_confirm"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["email_confirm"]);
                    users.encrypted_password = dr["encrypted_password"] == DBNull.Value ? default(String) : Convert.ToString(dr["encrypted_password"]);
                    users.favorite_author = dr["favorite_author"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_author"]);
                    users.favorite_book = dr["favorite_book"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_book"]);
                    users.favorite_genre = dr["favorite_genre"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_genre"]);
                    users.favorite_page_type = dr["favorite_page_type"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_page_type"]);
                    users.favorite_quote = dr["favorite_quote"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_quote"]);
                    users.fluid_preference = dr["fluid_preference"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["fluid_preference"]);
                    users.forum_administrator = dr["forum_administrator"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["forum_administrator"]);
                    users.forum_moderator = dr["forum_moderator"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["forum_moderator"]);
                    users.forums_badge_text = dr["forums_badge_text"] == DBNull.Value ? default(String) : Convert.ToString(dr["forums_badge_text"]);
                    users.gender = dr["gender"] == DBNull.Value ? default(String) : Convert.ToString(dr["gender"]);
                    users.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
                    users.inspirations = dr["inspirations"] == DBNull.Value ? default(String) : Convert.ToString(dr["inspirations"]);
                    users.interests = dr["interests"] == DBNull.Value ? default(String) : Convert.ToString(dr["interests"]);
                    users.keyboard_shortcuts_preference = dr["keyboard_shortcuts_preference"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["keyboard_shortcuts_preference"]);
                    users.last_sign_in_at = dr["last_sign_in_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["last_sign_in_at"]);
                    users.last_sign_in_ip = dr["last_sign_in_ip"] == DBNull.Value ? default(String) : Convert.ToString(dr["last_sign_in_ip"]);
                    users.location = dr["location"] == DBNull.Value ? default(String) : Convert.ToString(dr["location"]);
                    users.name = dr["name"] == DBNull.Value ? default(String) : Convert.ToString(dr["name"]);
                    users.notification_updates = dr["notification_updates"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["notification_updates"]);
                    users.occupation = dr["occupation"] == DBNull.Value ? default(String) : Convert.ToString(dr["occupation"]);
                    users.old_password = dr["old_password"] == DBNull.Value ? default(String) : Convert.ToString(dr["old_password"]);
                    users.other_names = dr["other_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["other_names"]);
                    users.site_template = dr["site_template"] == DBNull.Value ? default(String) : Convert.ToString(dr["site_template"]);
                    users.private_profile = dr["private_profile"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["private_profile"]);
                    users.remember_created_at = dr["remember_created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["remember_created_at"]);
                    users.reset_password_sent_at = dr["reset_password_sent_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["reset_password_sent_at"]);
                    users.reset_password_token = dr["reset_password_token"] == DBNull.Value ? default(String) : Convert.ToString(dr["reset_password_token"]);
                    users.secure_code = dr["secure_code"] == DBNull.Value ? default(String) : Convert.ToString(dr["secure_code"]);
                    users.selected_billing_plan_id = dr["selected_billing_plan_id"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["selected_billing_plan_id"]);
                    users.sign_in_count = dr["sign_in_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["sign_in_count"]);
                    users.site_administrator = dr["site_administrator"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["site_administrator"]);
                    users.stripe_customer_id = dr["stripe_customer_id"] == DBNull.Value ? default(String) : Convert.ToString(dr["stripe_customer_id"]);
                    users.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
                    users.upload_bandwidth_kb = dr["upload_bandwidth_kb"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["upload_bandwidth_kb"]);
                    users.username = dr["username"] == DBNull.Value ? default(String) : Convert.ToString(dr["username"]);
                    users.website = dr["website"] == DBNull.Value ? default(String) : Convert.ToString(dr["website"]);

                    _return_value = users;
                }
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;

        }

        public UsersModel GetUsersDataByCode(UsersModel Data)
        {
            UsersModel _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM Users WHERE secure_code = @secure_code";
                dbContext.AddInParameter(dbContext.cmd, "@secure_code", Data.secure_code);
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new UsersModel();
                    DataTable dt = ds.Tables[0];

                    DataRow dr = dt.Rows[0];

                    UsersModel users = new UsersModel();
                    users.age = dr["age"] == DBNull.Value ? default(String) : Convert.ToString(dr["age"]);
                    users.bio = dr["bio"] == DBNull.Value ? default(String) : Convert.ToString(dr["bio"]);
                    users.community_features_enabled = dr["community_features_enabled"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["community_features_enabled"]);
                    users.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
                    users.current_sign_in_at = dr["current_sign_in_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["current_sign_in_at"]);
                    users.current_sign_in_ip = dr["current_sign_in_ip"] == DBNull.Value ? default(String) : Convert.ToString(dr["current_sign_in_ip"]);
                    users.dark_mode_enabled = dr["dark_mode_enabled"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["dark_mode_enabled"]);
                    users.deleted_at = dr["deleted_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["deleted_at"]);
                    users.email = dr["email"] == DBNull.Value ? default(String) : Convert.ToString(dr["email"]);
                    users.email_updates = dr["email_updates"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["email_updates"]);
                    users.email_confirm = dr["email_confirm"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["email_confirm"]);
                    users.encrypted_password = dr["encrypted_password"] == DBNull.Value ? default(String) : Convert.ToString(dr["encrypted_password"]);
                    users.favorite_author = dr["favorite_author"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_author"]);
                    users.favorite_book = dr["favorite_book"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_book"]);
                    users.favorite_genre = dr["favorite_genre"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_genre"]);
                    users.favorite_page_type = dr["favorite_page_type"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_page_type"]);
                    users.favorite_quote = dr["favorite_quote"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_quote"]);
                    users.fluid_preference = dr["fluid_preference"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["fluid_preference"]);
                    users.forum_administrator = dr["forum_administrator"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["forum_administrator"]);
                    users.forum_moderator = dr["forum_moderator"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["forum_moderator"]);
                    users.forums_badge_text = dr["forums_badge_text"] == DBNull.Value ? default(String) : Convert.ToString(dr["forums_badge_text"]);
                    users.gender = dr["gender"] == DBNull.Value ? default(String) : Convert.ToString(dr["gender"]);
                    users.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
                    users.inspirations = dr["inspirations"] == DBNull.Value ? default(String) : Convert.ToString(dr["inspirations"]);
                    users.interests = dr["interests"] == DBNull.Value ? default(String) : Convert.ToString(dr["interests"]);
                    users.keyboard_shortcuts_preference = dr["keyboard_shortcuts_preference"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["keyboard_shortcuts_preference"]);
                    users.last_sign_in_at = dr["last_sign_in_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["last_sign_in_at"]);
                    users.last_sign_in_ip = dr["last_sign_in_ip"] == DBNull.Value ? default(String) : Convert.ToString(dr["last_sign_in_ip"]);
                    users.location = dr["location"] == DBNull.Value ? default(String) : Convert.ToString(dr["location"]);
                    users.name = dr["name"] == DBNull.Value ? default(String) : Convert.ToString(dr["name"]);
                    users.notification_updates = dr["notification_updates"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["notification_updates"]);
                    users.occupation = dr["occupation"] == DBNull.Value ? default(String) : Convert.ToString(dr["occupation"]);
                    users.old_password = dr["old_password"] == DBNull.Value ? default(String) : Convert.ToString(dr["old_password"]);
                    users.other_names = dr["other_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["other_names"]);
                    users.site_template = dr["site_template"] == DBNull.Value ? default(String) : Convert.ToString(dr["site_template"]);
                    users.private_profile = dr["private_profile"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["private_profile"]);
                    users.remember_created_at = dr["remember_created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["remember_created_at"]);
                    users.reset_password_sent_at = dr["reset_password_sent_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["reset_password_sent_at"]);
                    users.reset_password_token = dr["reset_password_token"] == DBNull.Value ? default(String) : Convert.ToString(dr["reset_password_token"]);
                    users.secure_code = dr["secure_code"] == DBNull.Value ? default(String) : Convert.ToString(dr["secure_code"]);
                    users.selected_billing_plan_id = dr["selected_billing_plan_id"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["selected_billing_plan_id"]);
                    users.sign_in_count = dr["sign_in_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["sign_in_count"]);
                    users.site_administrator = dr["site_administrator"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["site_administrator"]);
                    users.stripe_customer_id = dr["stripe_customer_id"] == DBNull.Value ? default(String) : Convert.ToString(dr["stripe_customer_id"]);
                    users.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
                    users.upload_bandwidth_kb = dr["upload_bandwidth_kb"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["upload_bandwidth_kb"]);
                    users.username = dr["username"] == DBNull.Value ? default(String) : Convert.ToString(dr["username"]);
                    users.website = dr["website"] == DBNull.Value ? default(String) : Convert.ToString(dr["website"]);

                    _return_value = users;
                }
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;

        }

        public List<UsersModel> SelectAllUsersData()
        {
            List<UsersModel> _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM Users;";
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new List<UsersModel>();
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        UsersModel users = new UsersModel();
                        users.age = dr["age"] == DBNull.Value ? default(String) : Convert.ToString(dr["age"]);
                        users.bio = dr["bio"] == DBNull.Value ? default(String) : Convert.ToString(dr["bio"]);
                        users.community_features_enabled = dr["community_features_enabled"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["community_features_enabled"]);
                        users.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
                        users.current_sign_in_at = dr["current_sign_in_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["current_sign_in_at"]);
                        users.current_sign_in_ip = dr["current_sign_in_ip"] == DBNull.Value ? default(String) : Convert.ToString(dr["current_sign_in_ip"]);
                        users.dark_mode_enabled = dr["dark_mode_enabled"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["dark_mode_enabled"]);
                        users.deleted_at = dr["deleted_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["deleted_at"]);
                        users.email = dr["email"] == DBNull.Value ? default(String) : Convert.ToString(dr["email"]);
                        users.email_updates = dr["email_updates"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["email_updates"]);
                        users.email_confirm = dr["email_confirm"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["email_confirm"]);
                        users.encrypted_password = dr["encrypted_password"] == DBNull.Value ? default(String) : Convert.ToString(dr["encrypted_password"]);
                        users.favorite_author = dr["favorite_author"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_author"]);
                        users.favorite_book = dr["favorite_book"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_book"]);
                        users.favorite_genre = dr["favorite_genre"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_genre"]);
                        users.favorite_page_type = dr["favorite_page_type"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_page_type"]);
                        users.favorite_quote = dr["favorite_quote"] == DBNull.Value ? default(String) : Convert.ToString(dr["favorite_quote"]);
                        users.fluid_preference = dr["fluid_preference"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["fluid_preference"]);
                        users.forum_administrator = dr["forum_administrator"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["forum_administrator"]);
                        users.forum_moderator = dr["forum_moderator"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["forum_moderator"]);
                        users.forums_badge_text = dr["forums_badge_text"] == DBNull.Value ? default(String) : Convert.ToString(dr["forums_badge_text"]);
                        users.gender = dr["gender"] == DBNull.Value ? default(String) : Convert.ToString(dr["gender"]);
                        users.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
                        users.inspirations = dr["inspirations"] == DBNull.Value ? default(String) : Convert.ToString(dr["inspirations"]);
                        users.interests = dr["interests"] == DBNull.Value ? default(String) : Convert.ToString(dr["interests"]);
                        users.keyboard_shortcuts_preference = dr["keyboard_shortcuts_preference"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["keyboard_shortcuts_preference"]);
                        users.last_sign_in_at = dr["last_sign_in_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["last_sign_in_at"]);
                        users.last_sign_in_ip = dr["last_sign_in_ip"] == DBNull.Value ? default(String) : Convert.ToString(dr["last_sign_in_ip"]);
                        users.location = dr["location"] == DBNull.Value ? default(String) : Convert.ToString(dr["location"]);
                        users.name = dr["name"] == DBNull.Value ? default(String) : Convert.ToString(dr["name"]);
                        users.notification_updates = dr["notification_updates"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["notification_updates"]);
                        users.occupation = dr["occupation"] == DBNull.Value ? default(String) : Convert.ToString(dr["occupation"]);
                        users.old_password = dr["old_password"] == DBNull.Value ? default(String) : Convert.ToString(dr["old_password"]);
                        users.other_names = dr["other_names"] == DBNull.Value ? default(String) : Convert.ToString(dr["other_names"]);
                        users.site_template = dr["site_template"] == DBNull.Value ? default(String) : Convert.ToString(dr["site_template"]);
                        users.private_profile = dr["private_profile"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["private_profile"]);
                        users.remember_created_at = dr["remember_created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["remember_created_at"]);
                        users.reset_password_sent_at = dr["reset_password_sent_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["reset_password_sent_at"]);
                        users.reset_password_token = dr["reset_password_token"] == DBNull.Value ? default(String) : Convert.ToString(dr["reset_password_token"]);
                        users.secure_code = dr["secure_code"] == DBNull.Value ? default(String) : Convert.ToString(dr["secure_code"]);
                        users.selected_billing_plan_id = dr["selected_billing_plan_id"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["selected_billing_plan_id"]);
                        users.sign_in_count = dr["sign_in_count"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["sign_in_count"]);
                        users.site_administrator = dr["site_administrator"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["site_administrator"]);
                        users.stripe_customer_id = dr["stripe_customer_id"] == DBNull.Value ? default(String) : Convert.ToString(dr["stripe_customer_id"]);
                        users.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
                        users.upload_bandwidth_kb = dr["upload_bandwidth_kb"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["upload_bandwidth_kb"]);
                        users.username = dr["username"] == DBNull.Value ? default(String) : Convert.ToString(dr["username"]);
                        users.website = dr["website"] == DBNull.Value ? default(String) : Convert.ToString(dr["website"]);

                        _return_value.Add(users);
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

        public string UpdateUsersData(UsersModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "UPDATE Users SET age = @age,bio = @bio,community_features_enabled = @community_features_enabled,created_at = @created_at,current_sign_in_at = @current_sign_in_at,current_sign_in_ip = @current_sign_in_ip,dark_mode_enabled = @dark_mode_enabled,deleted_at = @deleted_at,email = @email,email_updates = @email_updates,encrypted_password = @encrypted_password,favorite_author = @favorite_author,favorite_book = @favorite_book,favorite_genre = @favorite_genre,favorite_page_type = @favorite_page_type,favorite_quote = @favorite_quote,fluid_preference = @fluid_preference,forum_administrator = @forum_administrator,forum_moderator = @forum_moderator,forums_badge_text = @forums_badge_text,gender = @gender,id = @id,inspirations = @inspirations,interests = @interests,keyboard_shortcuts_preference = @keyboard_shortcuts_preference,last_sign_in_at = @last_sign_in_at,last_sign_in_ip = @last_sign_in_ip,location = @location,name = @name,notification_updates = @notification_updates,occupation = @occupation,old_password = @old_password,other_names = @other_names,site_template = @site_template,private_profile = @private_profile,remember_created_at = @remember_created_at,reset_password_sent_at = @reset_password_sent_at,reset_password_token = @reset_password_token,secure_code = @secure_code,selected_billing_plan_id = @selected_billing_plan_id,sign_in_count = @sign_in_count,site_administrator = @site_administrator,stripe_customer_id = @stripe_customer_id,updated_at = @updated_at,upload_bandwidth_kb = @upload_bandwidth_kb,username = @username,website = @website WHERE id = @id";
                dbContext.AddInParameter(dbContext.cmd, "@age", Data.age);
                dbContext.AddInParameter(dbContext.cmd, "@bio", Data.bio);
                dbContext.AddInParameter(dbContext.cmd, "@community_features_enabled", Data.community_features_enabled);
                dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
                dbContext.AddInParameter(dbContext.cmd, "@current_sign_in_at", Data.current_sign_in_at);
                dbContext.AddInParameter(dbContext.cmd, "@current_sign_in_ip", Data.current_sign_in_ip);
                dbContext.AddInParameter(dbContext.cmd, "@dark_mode_enabled", Data.dark_mode_enabled);
                dbContext.AddInParameter(dbContext.cmd, "@deleted_at", Data.deleted_at);
                dbContext.AddInParameter(dbContext.cmd, "@email", Data.email);
                dbContext.AddInParameter(dbContext.cmd, "@email_updates", Data.email_updates);
                dbContext.AddInParameter(dbContext.cmd, "@encrypted_password", Data.encrypted_password);
                dbContext.AddInParameter(dbContext.cmd, "@favorite_author", Data.favorite_author);
                dbContext.AddInParameter(dbContext.cmd, "@favorite_book", Data.favorite_book);
                dbContext.AddInParameter(dbContext.cmd, "@favorite_genre", Data.favorite_genre);
                dbContext.AddInParameter(dbContext.cmd, "@favorite_page_type", Data.favorite_page_type);
                dbContext.AddInParameter(dbContext.cmd, "@favorite_quote", Data.favorite_quote);
                dbContext.AddInParameter(dbContext.cmd, "@fluid_preference", Data.fluid_preference);
                dbContext.AddInParameter(dbContext.cmd, "@forum_administrator", Data.forum_administrator);
                dbContext.AddInParameter(dbContext.cmd, "@forum_moderator", Data.forum_moderator);
                dbContext.AddInParameter(dbContext.cmd, "@forums_badge_text", Data.forums_badge_text);
                dbContext.AddInParameter(dbContext.cmd, "@gender", Data.gender);
                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
                dbContext.AddInParameter(dbContext.cmd, "@inspirations", Data.inspirations);
                dbContext.AddInParameter(dbContext.cmd, "@interests", Data.interests);
                dbContext.AddInParameter(dbContext.cmd, "@keyboard_shortcuts_preference", Data.keyboard_shortcuts_preference);
                dbContext.AddInParameter(dbContext.cmd, "@last_sign_in_at", Data.last_sign_in_at);
                dbContext.AddInParameter(dbContext.cmd, "@last_sign_in_ip", Data.last_sign_in_ip);
                dbContext.AddInParameter(dbContext.cmd, "@location", Data.location);
                dbContext.AddInParameter(dbContext.cmd, "@name", Data.name);
                dbContext.AddInParameter(dbContext.cmd, "@notification_updates", Data.notification_updates);
                dbContext.AddInParameter(dbContext.cmd, "@occupation", Data.occupation);
                dbContext.AddInParameter(dbContext.cmd, "@old_password", Data.old_password);
                dbContext.AddInParameter(dbContext.cmd, "@other_names", Data.other_names);
                dbContext.AddInParameter(dbContext.cmd, "@site_template", Data.site_template);
                dbContext.AddInParameter(dbContext.cmd, "@private_profile", Data.private_profile);
                dbContext.AddInParameter(dbContext.cmd, "@remember_created_at", Data.remember_created_at);
                dbContext.AddInParameter(dbContext.cmd, "@reset_password_sent_at", Data.reset_password_sent_at);
                dbContext.AddInParameter(dbContext.cmd, "@reset_password_token", Data.reset_password_token);
                dbContext.AddInParameter(dbContext.cmd, "@secure_code", Data.secure_code);
                dbContext.AddInParameter(dbContext.cmd, "@selected_billing_plan_id", Data.selected_billing_plan_id);
                dbContext.AddInParameter(dbContext.cmd, "@sign_in_count", Data.sign_in_count);
                dbContext.AddInParameter(dbContext.cmd, "@site_administrator", Data.site_administrator);
                dbContext.AddInParameter(dbContext.cmd, "@stripe_customer_id", Data.stripe_customer_id);
                dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
                dbContext.AddInParameter(dbContext.cmd, "@upload_bandwidth_kb", Data.upload_bandwidth_kb);
                dbContext.AddInParameter(dbContext.cmd, "@username", Data.username);
                dbContext.AddInParameter(dbContext.cmd, "@website", Data.website);
                _return_value = Convert.ToString(dbContext.cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;

        }

        public string UpdateUsersProfileData(UsersModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "UPDATE Users SET bio = @bio,community_features_enabled = @community_features_enabled,dark_mode_enabled = @dark_mode_enabled,forums_badge_text = @forums_badge_text,inspirations = @inspirations,interests = @interests,private_profile = @private_profile,website = @website WHERE id = @id";
                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
                dbContext.AddInParameter(dbContext.cmd, "@bio", Data.bio);
                dbContext.AddInParameter(dbContext.cmd, "@community_features_enabled", Data.community_features_enabled);
                dbContext.AddInParameter(dbContext.cmd, "@dark_mode_enabled", Data.dark_mode_enabled);
                dbContext.AddInParameter(dbContext.cmd, "@forums_badge_text", Data.forums_badge_text);
                dbContext.AddInParameter(dbContext.cmd, "@inspirations", Data.inspirations);
                dbContext.AddInParameter(dbContext.cmd, "@interests", Data.interests);
                dbContext.AddInParameter(dbContext.cmd, "@private_profile", Data.private_profile);
                dbContext.AddInParameter(dbContext.cmd, "@website", Data.website);
                _return_value = Convert.ToString(dbContext.cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;

        }

        public string UpdateUsersAccountData(UsersModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "UPDATE Users SET age = @age,email = @email,email_updates = @email_updates,favorite_author = @favorite_author,favorite_book = @favorite_book,favorite_genre = @favorite_genre,favorite_page_type = @favorite_page_type,favorite_quote = @favorite_quote,fluid_preference = @fluid_preference,forum_administrator = @forum_administrator,forum_moderator = @forum_moderator,forums_badge_text = @forums_badge_text,gender = @gender,inspirations = @inspirations,interests = @interests,location = @location,name = @name,notification_updates = @notification_updates,occupation = @occupation,other_names = @other_names WHERE id = @id";
                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
                dbContext.AddInParameter(dbContext.cmd, "@age", Data.age);
                dbContext.AddInParameter(dbContext.cmd, "@email", Data.email);
                dbContext.AddInParameter(dbContext.cmd, "@email_updates", Data.email_updates);
                dbContext.AddInParameter(dbContext.cmd, "@favorite_author", Data.favorite_author);
                dbContext.AddInParameter(dbContext.cmd, "@favorite_book", Data.favorite_book);
                dbContext.AddInParameter(dbContext.cmd, "@favorite_genre", Data.favorite_genre);
                dbContext.AddInParameter(dbContext.cmd, "@favorite_page_type", Data.favorite_page_type);
                dbContext.AddInParameter(dbContext.cmd, "@favorite_quote", Data.favorite_quote);
                dbContext.AddInParameter(dbContext.cmd, "@fluid_preference", Data.fluid_preference);
                dbContext.AddInParameter(dbContext.cmd, "@forum_administrator", Data.forum_administrator);
                dbContext.AddInParameter(dbContext.cmd, "@forum_moderator", Data.forum_moderator);
                dbContext.AddInParameter(dbContext.cmd, "@forums_badge_text", Data.forums_badge_text);
                dbContext.AddInParameter(dbContext.cmd, "@gender", Data.gender);
                dbContext.AddInParameter(dbContext.cmd, "@inspirations", Data.inspirations);
                dbContext.AddInParameter(dbContext.cmd, "@interests", Data.interests);
                dbContext.AddInParameter(dbContext.cmd, "@location", Data.location);
                dbContext.AddInParameter(dbContext.cmd, "@name", Data.name);
                dbContext.AddInParameter(dbContext.cmd, "@notification_updates", Data.notification_updates);
                dbContext.AddInParameter(dbContext.cmd, "@occupation", Data.occupation);
                dbContext.AddInParameter(dbContext.cmd, "@other_names", Data.other_names);
                _return_value = Convert.ToString(dbContext.cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;

        }

        public string UpdateUsersPasswordData(UsersModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "UPDATE Users SET encrypted_password = @encrypted_password WHERE id = @id";
                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
                dbContext.AddInParameter(dbContext.cmd, "encrypted_password", Utility.Encrypt(Data.confirm_password), true);
                _return_value = Convert.ToString(dbContext.cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;

        }

        public string UpdateUsersSignInData(UsersModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "UPDATE Users SET last_sign_in_at = @last_sign_in_at, " +
                    "last_sign_in_ip = @last_sign_in_ip, " +
                    "current_sign_in_ip = @current_sign_in_ip, " +
                    "sign_in_count = @sign_in_count " +
                    "WHERE id = @id";

                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
                dbContext.AddInParameter(dbContext.cmd, "@last_sign_in_at", Data.last_sign_in_at);
                dbContext.AddInParameter(dbContext.cmd, "@last_sign_in_ip", Data.last_sign_in_ip);
                dbContext.AddInParameter(dbContext.cmd, "@current_sign_in_ip", Data.current_sign_in_ip);
                dbContext.AddInParameter(dbContext.cmd, "@sign_in_count", Data.sign_in_count + 1);

                _return_value = Convert.ToString(dbContext.cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;

        }

        public UsersModel UpdateUsersEmailConfirmData(UsersModel Data)
        {
            UsersModel _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "UPDATE Users SET email_confirm = @email_confirm WHERE secure_code = @secure_code";

                dbContext.AddInParameter(dbContext.cmd, "@secure_code", Data.secure_code);
                dbContext.AddInParameter(dbContext.cmd, "@email_confirm", Data.email_confirm);

                string _value = Convert.ToString(dbContext.cmd.ExecuteNonQuery());

                if (_value == "1")
                {
                    return GetUsersDataByCode(Data);
                }
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;

        }

        public string UpdateUsersSecureCodeData(UsersModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "UPDATE Users SET secure_code = @secure_code WHERE id = @id";

                dbContext.AddInParameter(dbContext.cmd, "@secure_code", Data.secure_code);
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

        public ContentPlansModel GetUserPlan(long user_id)
        {
            ContentPlansModel _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT cp.* FROM user_plan up join content_plans cp on up.plan_id = cp.id WHERE user_id = " + user_id;
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new ContentPlansModel();
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        _return_value = new ContentPlansModel();
                        _return_value.id = dr["id"] == DBNull.Value ? default(Int32) : Convert.ToInt32(dr["id"]);
                        _return_value.name = dr["name"] == DBNull.Value ? default(String) : Convert.ToString(dr["name"]);
                        _return_value.available = dr["available"] == DBNull.Value ? default(Boolean) : Convert.ToBoolean(dr["available"]);
                        _return_value.monthly_cents = dr["monthly_cents"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["monthly_cents"]);
                        _return_value.plan_template = dr["plan_template"] == DBNull.Value ? default(String) : Convert.ToString(dr["plan_template"]);
                        _return_value.plan_description = dr["plan_description"] == DBNull.Value ? default(String) : Convert.ToString(dr["plan_description"]);
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

        public string UpdateUsersPlan(UsersModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "DELETE FROM user_plan WHERE user_id = @id";
                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);

                _return_value = Convert.ToString(dbContext.cmd.ExecuteNonQuery());

                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "INSERT INTO user_plan(user_id, plan_id) VALUES (@user_id, @plan_id)";
                dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.id);
                dbContext.AddInParameter(dbContext.cmd, "@plan_id", Data.user_plan.id);

                _return_value = Convert.ToString(dbContext.cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                _return_value = null;
                throw;
            }

            return _return_value;

        }

        public ContentTemplateModel GetUserContentTemplate(long user_id)
        {
            ContentTemplateModel _return_value = null;
            try
            {
                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "SELECT * FROM `user_content_template` WHERE user_id = " + user_id;
                
                DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
                
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    _return_value = new ContentTemplateModel();
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        string template = dr["template"] == DBNull.Value ? default(String) : GetStringDataFromByteArray((byte[])(dr["template"]));
                        _return_value = new ContentTemplateModel();
                        _return_value = JsonConvert.DeserializeObject<ContentTemplateModel>(template);
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

        public string UpdateUsersContentTemplate(UsersModel Data)
        {
            string _return_value = string.Empty;
            try
            {
                var jsonTemplate = JsonConvert.SerializeObject(Data.content_template, Formatting.Indented);

                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "DELETE FROM user_content_template WHERE user_id = @id";
                dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);

                _return_value = Convert.ToString(dbContext.cmd.ExecuteNonQuery());

                dbContext.cmd = new MySqlCommand();
                dbContext.cmd.Connection = dbContext.GetConnection();
                dbContext.cmd.CommandText = "INSERT INTO user_content_template(user_id, template) VALUES (@user_id, @template)";
                dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.id);
                dbContext.AddInParameter(dbContext.cmd, "@template", jsonTemplate);

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
