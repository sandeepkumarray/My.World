
var Abilities_editor;
var Children_editor;
var Conditions_editor;
var Creatures_editor;
var Description_editor;
var Elements_editor;
var Family_History_editor;
var Floras_editor;
var Human_Interaction_editor;
var Life_Story_editor;
var Notable_Events_editor;
var Notes_editor;
var Other_Names_editor;
var Parents_editor;
var Partners_editor;
var Physical_Description_editor;
var Prayers_editor;
var Private_Notes_editor;
var Related_landmarks_editor;
var Related_races_editor;
var Related_towns_editor;
var Relics_editor;
var Religions_editor;
var Rituals_editor;
var Siblings_editor;
var Strengths_editor;
var Symbols_editor;
var Tags_editor;
var Traditions_editor;
var Weaknesses_editor;


$(document).ready(function () {

	Abilities_editor = createEditor("#Abilities");
	Children_editor = createEditor("#Children");
	Conditions_editor = createEditor("#Conditions");
	Creatures_editor = createEditor("#Creatures");
	Description_editor = createEditor("#Description");
	Elements_editor = createEditor("#Elements");
	Family_History_editor = createEditor("#Family_History");
	Floras_editor = createEditor("#Floras");
	Human_Interaction_editor = createEditor("#Human_Interaction");
	Life_Story_editor = createEditor("#Life_Story");
	Notable_Events_editor = createEditor("#Notable_Events");
	Notes_editor = createEditor("#Notes");
	Other_Names_editor = createEditor("#Other_Names");
	Parents_editor = createEditor("#Parents");
	Partners_editor = createEditor("#Partners");
	Physical_Description_editor = createEditor("#Physical_Description");
	Prayers_editor = createEditor("#Prayers");
	Private_Notes_editor = createEditor("#Private_Notes");
	Related_landmarks_editor = createEditor("#Related_landmarks");
	Related_races_editor = createEditor("#Related_races");
	Related_towns_editor = createEditor("#Related_towns");
	Relics_editor = createEditor("#Relics");
	Religions_editor = createEditor("#Religions");
	Rituals_editor = createEditor("#Rituals");
	Siblings_editor = createEditor("#Siblings");
	Strengths_editor = createEditor("#Strengths");
	Symbols_editor = createEditor("#Symbols");
	Tags_editor = createEditor("#Tags");
	Traditions_editor = createEditor("#Traditions");
	Weaknesses_editor = createEditor("#Weaknesses");


	setAbilitiesBody();
	setChildrenBody();
	setConditionsBody();
	setCreaturesBody();
	setDescriptionBody();
	setElementsBody();
	setFamily_HistoryBody();
	setFlorasBody();
	setHeightBody();
	setHuman_InteractionBody();
	setLife_StoryBody();
	setNameBody();
	setNotable_EventsBody();
	setNotesBody();
	setOther_NamesBody();
	setParentsBody();
	setPartnersBody();
	setPhysical_DescriptionBody();
	setPrayersBody();
	setPrivate_NotesBody();
	setRelated_landmarksBody();
	setRelated_racesBody();
	setRelated_townsBody();
	setRelicsBody();
	setReligionsBody();
	setRitualsBody();
	setSiblingsBody();
	setStrengthsBody();
	setSymbolsBody();
	setTagsBody();
	setTraditionsBody();
	setUniverseBody();
	setWeaknessesBody();
	setWeightBody();


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
