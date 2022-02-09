
var Color_editor;
var Conditions_editor;
var Cooking_method_editor;
var Cost_editor;
var Description_editor;
var Flavor_editor;
var Ingredients_editor;
var Meal_editor;
var Notes_editor;
var Nutrition_editor;
var Origin_story_editor;
var Other_Names_editor;
var Place_of_origin_editor;
var Preparation_editor;
var Private_Notes_editor;
var Rarity_editor;
var Related_foods_editor;
var Reputation_editor;
var Scent_editor;
var Serving_editor;
var Shelf_life_editor;
var Side_effects_editor;
var Smell_editor;
var Sold_by_editor;
var Spices_editor;
var Symbolisms_editor;
var Tags_editor;
var Texture_editor;
var Traditions_editor;
var Type_of_food_editor;
var Utensils_needed_editor;
var Variations_editor;
var Yield_editor;


$(document).ready(function () {

	Color_editor = createEditor("#Color");
	Conditions_editor = createEditor("#Conditions");
	Cooking_method_editor = createEditor("#Cooking_method");
	Cost_editor = createEditor("#Cost");
	Description_editor = createEditor("#Description");
	Flavor_editor = createEditor("#Flavor");
	Ingredients_editor = createEditor("#Ingredients");
	Meal_editor = createEditor("#Meal");
	Notes_editor = createEditor("#Notes");
	Nutrition_editor = createEditor("#Nutrition");
	Origin_story_editor = createEditor("#Origin_story");
	Other_Names_editor = createEditor("#Other_Names");
	Place_of_origin_editor = createEditor("#Place_of_origin");
	Preparation_editor = createEditor("#Preparation");
	Private_Notes_editor = createEditor("#Private_Notes");
	Rarity_editor = createEditor("#Rarity");
	Related_foods_editor = createEditor("#Related_foods");
	Reputation_editor = createEditor("#Reputation");
	Scent_editor = createEditor("#Scent");
	Serving_editor = createEditor("#Serving");
	Shelf_life_editor = createEditor("#Shelf_life");
	Side_effects_editor = createEditor("#Side_effects");
	Smell_editor = createEditor("#Smell");
	Sold_by_editor = createEditor("#Sold_by");
	Spices_editor = createEditor("#Spices");
	Symbolisms_editor = createEditor("#Symbolisms");
	Tags_editor = createEditor("#Tags");
	Texture_editor = createEditor("#Texture");
	Traditions_editor = createEditor("#Traditions");
	Type_of_food_editor = createEditor("#Type_of_food");
	Utensils_needed_editor = createEditor("#Utensils_needed");
	Variations_editor = createEditor("#Variations");
	Yield_editor = createEditor("#Yield");


	setColorBody();
	setConditionsBody();
	setCooking_methodBody();
	setCostBody();
	setDescriptionBody();
	setFlavorBody();
	setIngredientsBody();
	setMealBody();
	setNameBody();
	setNotesBody();
	setNutritionBody();
	setOrigin_storyBody();
	setOther_NamesBody();
	setPlace_of_originBody();
	setPreparationBody();
	setPrivate_NotesBody();
	setRarityBody();
	setRelated_foodsBody();
	setReputationBody();
	setScentBody();
	setServingBody();
	setShelf_lifeBody();
	setSide_effectsBody();
	setSizeBody();
	setSmellBody();
	setSold_byBody();
	setSpicesBody();
	setSymbolismsBody();
	setTagsBody();
	setTextureBody();
	setTraditionsBody();
	setType_of_foodBody();
	setUniverseBody();
	setUtensils_neededBody();
	setVariationsBody();
	setYieldBody();


});

function createEditor(selector) {
    let quill = new Quill(selector, {
        theme: 'snow',
        modules: {
            toolbar: false,
            mention: {
                allowedChars: /^[A-Za-z\sÅÄÖåäö]*$/,
                mentionDenotationChars: ["@"],
                showDenotationChar: false,
                source: async function (searchTerm, renderList) {
                    var url = '/Document/GetMentions/' + searchTerm;

                    $.get(url, function (data) {
                        if (data != null) {
                            renderList(data.filter(person => person.value.includes(searchTerm)));
                        }
                    });
                }
            }
        },
        placeholder: selector.replace(/_/g, " "),
    });
    return quill;
}

function htmlEncode(value) {
    return $('<div/>').text(value).html();
}

function htmlDecode(value) {
    return $('<div/>').html(value).text();
}
