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
	public class FoodsDAL : BaseDAL
	{

		public FoodsDAL()
		{
		}

		public  FoodsDAL(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public string DeleteFoodsData(FoodsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "DELETE FROM `Foods` WHERE id = @id";
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

		public FoodsModel GetFoodsData(FoodsModel Data)
		{
			FoodsModel _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Foods` WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@id", Data.id);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
				    _return_value = new FoodsModel();
				    DataTable dt = ds.Tables[0];
				
				    DataRow dr = dt.Rows[0];
				
				    FoodsModel foods = new FoodsModel();
					foods.Color = dr["Color"] == DBNull.Value ? default(String) : Convert.ToString(dr["Color"]);
					foods.Conditions = dr["Conditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Conditions"]);
					foods.Cooking_method = dr["Cooking_method"] == DBNull.Value ? default(String) : Convert.ToString(dr["Cooking_method"]);
					foods.Cost = dr["Cost"] == DBNull.Value ? default(String) : Convert.ToString(dr["Cost"]);
					foods.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					foods.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					foods.Flavor = dr["Flavor"] == DBNull.Value ? default(String) : Convert.ToString(dr["Flavor"]);
					foods.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					foods.Ingredients = dr["Ingredients"] == DBNull.Value ? default(String) : Convert.ToString(dr["Ingredients"]);
					foods.Meal = dr["Meal"] == DBNull.Value ? default(String) : Convert.ToString(dr["Meal"]);
					foods.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					foods.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					foods.Nutrition = dr["Nutrition"] == DBNull.Value ? default(String) : Convert.ToString(dr["Nutrition"]);
					foods.Origin_story = dr["Origin_story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Origin_story"]);
					foods.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					foods.Place_of_origin = dr["Place_of_origin"] == DBNull.Value ? default(String) : Convert.ToString(dr["Place_of_origin"]);
					foods.Preparation = dr["Preparation"] == DBNull.Value ? default(String) : Convert.ToString(dr["Preparation"]);
					foods.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					foods.Rarity = dr["Rarity"] == DBNull.Value ? default(String) : Convert.ToString(dr["Rarity"]);
					foods.Related_foods = dr["Related_foods"] == DBNull.Value ? default(String) : Convert.ToString(dr["Related_foods"]);
					foods.Reputation = dr["Reputation"] == DBNull.Value ? default(String) : Convert.ToString(dr["Reputation"]);
					foods.Scent = dr["Scent"] == DBNull.Value ? default(String) : Convert.ToString(dr["Scent"]);
					foods.Serving = dr["Serving"] == DBNull.Value ? default(String) : Convert.ToString(dr["Serving"]);
					foods.Shelf_life = dr["Shelf_life"] == DBNull.Value ? default(String) : Convert.ToString(dr["Shelf_life"]);
					foods.Side_effects = dr["Side_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Side_effects"]);
					foods.Size = dr["Size"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Size"]);
					foods.Smell = dr["Smell"] == DBNull.Value ? default(String) : Convert.ToString(dr["Smell"]);
					foods.Sold_by = dr["Sold_by"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sold_by"]);
					foods.Spices = dr["Spices"] == DBNull.Value ? default(String) : Convert.ToString(dr["Spices"]);
					foods.Symbolisms = dr["Symbolisms"] == DBNull.Value ? default(String) : Convert.ToString(dr["Symbolisms"]);
					foods.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					foods.Texture = dr["Texture"] == DBNull.Value ? default(String) : Convert.ToString(dr["Texture"]);
					foods.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					foods.Type_of_food = dr["Type_of_food"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_food"]);
					foods.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					foods.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					foods.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					foods.Utensils_needed = dr["Utensils_needed"] == DBNull.Value ? default(String) : Convert.ToString(dr["Utensils_needed"]);
					foods.Variations = dr["Variations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Variations"]);
					foods.Yield = dr["Yield"] == DBNull.Value ? default(String) : Convert.ToString(dr["Yield"]);

					_return_value = foods;
				}
			}
			catch (Exception ex)
			{
			    _return_value = null;
			    throw;
			}
			
			return _return_value;

		}

		public List<FoodsModel> GetAllFoodsForUserID(long userId)
		{
			List<FoodsModel> _return_value = null;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "SELECT * FROM `Foods` where user_id = @user_id;";
				dbContext.AddInParameter(dbContext.cmd, "@user_id",userId);
				DataSet ds = dbContext.ExecuteDataSet(dbContext.cmd);
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					_return_value = new List<FoodsModel>();
					DataTable dt = ds.Tables[0];
				
					foreach (DataRow dr in dt.Rows)
					{
						FoodsModel foods = new FoodsModel();
					foods.Color = dr["Color"] == DBNull.Value ? default(String) : Convert.ToString(dr["Color"]);
					foods.Conditions = dr["Conditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Conditions"]);
					foods.Cooking_method = dr["Cooking_method"] == DBNull.Value ? default(String) : Convert.ToString(dr["Cooking_method"]);
					foods.Cost = dr["Cost"] == DBNull.Value ? default(String) : Convert.ToString(dr["Cost"]);
					foods.created_at = dr["created_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["created_at"]);
					foods.Description = dr["Description"] == DBNull.Value ? default(String) : Convert.ToString(dr["Description"]);
					foods.Flavor = dr["Flavor"] == DBNull.Value ? default(String) : Convert.ToString(dr["Flavor"]);
					foods.id = dr["id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["id"]);
					foods.Ingredients = dr["Ingredients"] == DBNull.Value ? default(String) : Convert.ToString(dr["Ingredients"]);
					foods.Meal = dr["Meal"] == DBNull.Value ? default(String) : Convert.ToString(dr["Meal"]);
					foods.Name = dr["Name"] == DBNull.Value ? default(String) : Convert.ToString(dr["Name"]);
					foods.Notes = dr["Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Notes"]);
					foods.Nutrition = dr["Nutrition"] == DBNull.Value ? default(String) : Convert.ToString(dr["Nutrition"]);
					foods.Origin_story = dr["Origin_story"] == DBNull.Value ? default(String) : Convert.ToString(dr["Origin_story"]);
					foods.Other_Names = dr["Other_Names"] == DBNull.Value ? default(String) : Convert.ToString(dr["Other_Names"]);
					foods.Place_of_origin = dr["Place_of_origin"] == DBNull.Value ? default(String) : Convert.ToString(dr["Place_of_origin"]);
					foods.Preparation = dr["Preparation"] == DBNull.Value ? default(String) : Convert.ToString(dr["Preparation"]);
					foods.Private_Notes = dr["Private_Notes"] == DBNull.Value ? default(String) : Convert.ToString(dr["Private_Notes"]);
					foods.Rarity = dr["Rarity"] == DBNull.Value ? default(String) : Convert.ToString(dr["Rarity"]);
					foods.Related_foods = dr["Related_foods"] == DBNull.Value ? default(String) : Convert.ToString(dr["Related_foods"]);
					foods.Reputation = dr["Reputation"] == DBNull.Value ? default(String) : Convert.ToString(dr["Reputation"]);
					foods.Scent = dr["Scent"] == DBNull.Value ? default(String) : Convert.ToString(dr["Scent"]);
					foods.Serving = dr["Serving"] == DBNull.Value ? default(String) : Convert.ToString(dr["Serving"]);
					foods.Shelf_life = dr["Shelf_life"] == DBNull.Value ? default(String) : Convert.ToString(dr["Shelf_life"]);
					foods.Side_effects = dr["Side_effects"] == DBNull.Value ? default(String) : Convert.ToString(dr["Side_effects"]);
					foods.Size = dr["Size"] == DBNull.Value ? default(Double) : Convert.ToDouble(dr["Size"]);
					foods.Smell = dr["Smell"] == DBNull.Value ? default(String) : Convert.ToString(dr["Smell"]);
					foods.Sold_by = dr["Sold_by"] == DBNull.Value ? default(String) : Convert.ToString(dr["Sold_by"]);
					foods.Spices = dr["Spices"] == DBNull.Value ? default(String) : Convert.ToString(dr["Spices"]);
					foods.Symbolisms = dr["Symbolisms"] == DBNull.Value ? default(String) : Convert.ToString(dr["Symbolisms"]);
					foods.Tags = dr["Tags"] == DBNull.Value ? default(String) : Convert.ToString(dr["Tags"]);
					foods.Texture = dr["Texture"] == DBNull.Value ? default(String) : Convert.ToString(dr["Texture"]);
					foods.Traditions = dr["Traditions"] == DBNull.Value ? default(String) : Convert.ToString(dr["Traditions"]);
					foods.Type_of_food = dr["Type_of_food"] == DBNull.Value ? default(String) : Convert.ToString(dr["Type_of_food"]);
					foods.Universe = dr["Universe"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["Universe"]);
					foods.updated_at = dr["updated_at"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dr["updated_at"]);
					foods.user_id = dr["user_id"] == DBNull.Value ? default(Int64) : Convert.ToInt64(dr["user_id"]);
					foods.Utensils_needed = dr["Utensils_needed"] == DBNull.Value ? default(String) : Convert.ToString(dr["Utensils_needed"]);
					foods.Variations = dr["Variations"] == DBNull.Value ? default(String) : Convert.ToString(dr["Variations"]);
					foods.Yield = dr["Yield"] == DBNull.Value ? default(String) : Convert.ToString(dr["Yield"]);


						var contentObjectList = new ContentObjectDAL(dbContext).GetAllContentObjectAttachments(foods.id, "foods");
						if (contentObjectList != null && contentObjectList.Count > 0)
						{
						    var first = contentObjectList[0];
						    foods.object_id = first.object_id;
						    foods.object_name = first.object_name;
						}

						_return_value.Add(foods);
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

		public string AddFoodsData(FoodsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "INSERT INTO `Foods`(`Color`,`Conditions`,`Cooking_method`,`Cost`,`created_at`,`Description`,`Flavor`,`Ingredients`,`Meal`,`Name`,`Notes`,`Nutrition`,`Origin_story`,`Other_Names`,`Place_of_origin`,`Preparation`,`Private_Notes`,`Rarity`,`Related_foods`,`Reputation`,`Scent`,`Serving`,`Shelf_life`,`Side_effects`,`Size`,`Smell`,`Sold_by`,`Spices`,`Symbolisms`,`Tags`,`Texture`,`Traditions`,`Type_of_food`,`Universe`,`updated_at`,`user_id`,`Utensils_needed`,`Variations`,`Yield`) VALUES(@Color,@Conditions,@Cooking_method,@Cost,@created_at,@Description,@Flavor,@Ingredients,@Meal,@Name,@Notes,@Nutrition,@Origin_story,@Other_Names,@Place_of_origin,@Preparation,@Private_Notes,@Rarity,@Related_foods,@Reputation,@Scent,@Serving,@Shelf_life,@Side_effects,@Size,@Smell,@Sold_by,@Spices,@Symbolisms,@Tags,@Texture,@Traditions,@Type_of_food,@Universe,@updated_at,@user_id,@Utensils_needed,@Variations,@Yield)";
				dbContext.AddInParameter(dbContext.cmd, "@Color", Data.Color);
				dbContext.AddInParameter(dbContext.cmd, "@Conditions", Data.Conditions);
				dbContext.AddInParameter(dbContext.cmd, "@Cooking_method", Data.Cooking_method);
				dbContext.AddInParameter(dbContext.cmd, "@Cost", Data.Cost);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Flavor", Data.Flavor);
				dbContext.AddInParameter(dbContext.cmd, "@Ingredients", Data.Ingredients);
				dbContext.AddInParameter(dbContext.cmd, "@Meal", Data.Meal);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Nutrition", Data.Nutrition);
				dbContext.AddInParameter(dbContext.cmd, "@Origin_story", Data.Origin_story);
				dbContext.AddInParameter(dbContext.cmd, "@Other_Names", Data.Other_Names);
				dbContext.AddInParameter(dbContext.cmd, "@Place_of_origin", Data.Place_of_origin);
				dbContext.AddInParameter(dbContext.cmd, "@Preparation", Data.Preparation);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Rarity", Data.Rarity);
				dbContext.AddInParameter(dbContext.cmd, "@Related_foods", Data.Related_foods);
				dbContext.AddInParameter(dbContext.cmd, "@Reputation", Data.Reputation);
				dbContext.AddInParameter(dbContext.cmd, "@Scent", Data.Scent);
				dbContext.AddInParameter(dbContext.cmd, "@Serving", Data.Serving);
				dbContext.AddInParameter(dbContext.cmd, "@Shelf_life", Data.Shelf_life);
				dbContext.AddInParameter(dbContext.cmd, "@Side_effects", Data.Side_effects);
				dbContext.AddInParameter(dbContext.cmd, "@Size", Data.Size);
				dbContext.AddInParameter(dbContext.cmd, "@Smell", Data.Smell);
				dbContext.AddInParameter(dbContext.cmd, "@Sold_by", Data.Sold_by);
				dbContext.AddInParameter(dbContext.cmd, "@Spices", Data.Spices);
				dbContext.AddInParameter(dbContext.cmd, "@Symbolisms", Data.Symbolisms);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Texture", Data.Texture);
				dbContext.AddInParameter(dbContext.cmd, "@Traditions", Data.Traditions);
				dbContext.AddInParameter(dbContext.cmd, "@Type_of_food", Data.Type_of_food);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Utensils_needed", Data.Utensils_needed);
				dbContext.AddInParameter(dbContext.cmd, "@Variations", Data.Variations);
				dbContext.AddInParameter(dbContext.cmd, "@Yield", Data.Yield);
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

		public string UpdateFoodsData(FoodsModel Data)
		{
			string _return_value = string.Empty;
			try
			{
				dbContext.cmd = new MySqlCommand();
				dbContext.cmd.Connection = dbContext.GetConnection();
				dbContext.cmd.CommandText = "UPDATE foods SET Color = @Color,Conditions = @Conditions,Cooking_method = @Cooking_method,Cost = @Cost,created_at = @created_at,Description = @Description,Flavor = @Flavor,Ingredients = @Ingredients,Meal = @Meal,Name = @Name,Notes = @Notes,Nutrition = @Nutrition,Origin_story = @Origin_story,Other_Names = @Other_Names,Place_of_origin = @Place_of_origin,Preparation = @Preparation,Private_Notes = @Private_Notes,Rarity = @Rarity,Related_foods = @Related_foods,Reputation = @Reputation,Scent = @Scent,Serving = @Serving,Shelf_life = @Shelf_life,Side_effects = @Side_effects,Size = @Size,Smell = @Smell,Sold_by = @Sold_by,Spices = @Spices,Symbolisms = @Symbolisms,Tags = @Tags,Texture = @Texture,Traditions = @Traditions,Type_of_food = @Type_of_food,Universe = @Universe,updated_at = @updated_at,user_id = @user_id,Utensils_needed = @Utensils_needed,Variations = @Variations,Yield = @Yield WHERE id = @id";
				dbContext.AddInParameter(dbContext.cmd, "@Color", Data.Color);
				dbContext.AddInParameter(dbContext.cmd, "@Conditions", Data.Conditions);
				dbContext.AddInParameter(dbContext.cmd, "@Cooking_method", Data.Cooking_method);
				dbContext.AddInParameter(dbContext.cmd, "@Cost", Data.Cost);
				dbContext.AddInParameter(dbContext.cmd, "@created_at", Data.created_at);
				dbContext.AddInParameter(dbContext.cmd, "@Description", Data.Description);
				dbContext.AddInParameter(dbContext.cmd, "@Flavor", Data.Flavor);
				dbContext.AddInParameter(dbContext.cmd, "@Ingredients", Data.Ingredients);
				dbContext.AddInParameter(dbContext.cmd, "@Meal", Data.Meal);
				dbContext.AddInParameter(dbContext.cmd, "@Name", Data.Name);
				dbContext.AddInParameter(dbContext.cmd, "@Notes", Data.Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Nutrition", Data.Nutrition);
				dbContext.AddInParameter(dbContext.cmd, "@Origin_story", Data.Origin_story);
				dbContext.AddInParameter(dbContext.cmd, "@Other_Names", Data.Other_Names);
				dbContext.AddInParameter(dbContext.cmd, "@Place_of_origin", Data.Place_of_origin);
				dbContext.AddInParameter(dbContext.cmd, "@Preparation", Data.Preparation);
				dbContext.AddInParameter(dbContext.cmd, "@Private_Notes", Data.Private_Notes);
				dbContext.AddInParameter(dbContext.cmd, "@Rarity", Data.Rarity);
				dbContext.AddInParameter(dbContext.cmd, "@Related_foods", Data.Related_foods);
				dbContext.AddInParameter(dbContext.cmd, "@Reputation", Data.Reputation);
				dbContext.AddInParameter(dbContext.cmd, "@Scent", Data.Scent);
				dbContext.AddInParameter(dbContext.cmd, "@Serving", Data.Serving);
				dbContext.AddInParameter(dbContext.cmd, "@Shelf_life", Data.Shelf_life);
				dbContext.AddInParameter(dbContext.cmd, "@Side_effects", Data.Side_effects);
				dbContext.AddInParameter(dbContext.cmd, "@Size", Data.Size);
				dbContext.AddInParameter(dbContext.cmd, "@Smell", Data.Smell);
				dbContext.AddInParameter(dbContext.cmd, "@Sold_by", Data.Sold_by);
				dbContext.AddInParameter(dbContext.cmd, "@Spices", Data.Spices);
				dbContext.AddInParameter(dbContext.cmd, "@Symbolisms", Data.Symbolisms);
				dbContext.AddInParameter(dbContext.cmd, "@Tags", Data.Tags);
				dbContext.AddInParameter(dbContext.cmd, "@Texture", Data.Texture);
				dbContext.AddInParameter(dbContext.cmd, "@Traditions", Data.Traditions);
				dbContext.AddInParameter(dbContext.cmd, "@Type_of_food", Data.Type_of_food);
				dbContext.AddInParameter(dbContext.cmd, "@Universe", Data.Universe);
				dbContext.AddInParameter(dbContext.cmd, "@updated_at", Data.updated_at);
				dbContext.AddInParameter(dbContext.cmd, "@user_id", Data.user_id);
				dbContext.AddInParameter(dbContext.cmd, "@Utensils_needed", Data.Utensils_needed);
				dbContext.AddInParameter(dbContext.cmd, "@Variations", Data.Variations);
				dbContext.AddInParameter(dbContext.cmd, "@Yield", Data.Yield);
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
