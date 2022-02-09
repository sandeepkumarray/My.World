
var Beliefs_editor;
var Conditions_editor;
var Description_editor;
var Economics_editor;
var Famous_figures_editor;
var Favorite_foods_editor;
var Governments_editor;
var Notable_events_editor;
var Notable_features_editor;
var Notes_editor;
var Occupations_editor;
var Other_Names_editor;
var Physical_variance_editor;
var Private_notes_editor;
var Skin_colors_editor;
var Strengths_editor;
var Tags_editor;
var Technologies_editor;
var Traditions_editor;
var Typical_clothing_editor;
var Weaknesses_editor;


$(document).ready(function () {

	Beliefs_editor = createEditor("#Beliefs");
	Conditions_editor = createEditor("#Conditions");
	Description_editor = createEditor("#Description");
	Economics_editor = createEditor("#Economics");
	Famous_figures_editor = createEditor("#Famous_figures");
	Favorite_foods_editor = createEditor("#Favorite_foods");
	Governments_editor = createEditor("#Governments");
	Notable_events_editor = createEditor("#Notable_events");
	Notable_features_editor = createEditor("#Notable_features");
	Notes_editor = createEditor("#Notes");
	Occupations_editor = createEditor("#Occupations");
	Other_Names_editor = createEditor("#Other_Names");
	Physical_variance_editor = createEditor("#Physical_variance");
	Private_notes_editor = createEditor("#Private_notes");
	Skin_colors_editor = createEditor("#Skin_colors");
	Strengths_editor = createEditor("#Strengths");
	Tags_editor = createEditor("#Tags");
	Technologies_editor = createEditor("#Technologies");
	Traditions_editor = createEditor("#Traditions");
	Typical_clothing_editor = createEditor("#Typical_clothing");
	Weaknesses_editor = createEditor("#Weaknesses");


	setBeliefsBody();
	setBody_shapeBody();
	setConditionsBody();
	setDescriptionBody();
	setEconomicsBody();
	setFamous_figuresBody();
	setFavorite_foodsBody();
	setGeneral_heightBody();
	setGeneral_weightBody();
	setGovernmentsBody();
	setNameBody();
	setNotable_eventsBody();
	setNotable_featuresBody();
	setNotesBody();
	setOccupationsBody();
	setOther_NamesBody();
	setPhysical_varianceBody();
	setPrivate_notesBody();
	setSkin_colorsBody();
	setStrengthsBody();
	setTagsBody();
	setTechnologiesBody();
	setTraditionsBody();
	setTypical_clothingBody();
	setUniverseBody();
	setWeaknessesBody();


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
