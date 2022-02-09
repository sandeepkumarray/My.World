
var Colors_editor;
var Country_editor;
var Creation_story_editor;
var Creatures_editor;
var Description_editor;
var Flora_editor;
var Materials_editor;
var Nearby_towns_editor;
var Notes_editor;
var Other_Names_editor;
var Private_Notes_editor;
var Tags_editor;
var Type_of_landmark_editor;


$(document).ready(function () {

	Colors_editor = createEditor("#Colors");
	Country_editor = createEditor("#Country");
	Creation_story_editor = createEditor("#Creation_story");
	Creatures_editor = createEditor("#Creatures");
	Description_editor = createEditor("#Description");
	Flora_editor = createEditor("#Flora");
	Materials_editor = createEditor("#Materials");
	Nearby_towns_editor = createEditor("#Nearby_towns");
	Notes_editor = createEditor("#Notes");
	Other_Names_editor = createEditor("#Other_Names");
	Private_Notes_editor = createEditor("#Private_Notes");
	Tags_editor = createEditor("#Tags");
	Type_of_landmark_editor = createEditor("#Type_of_landmark");


	setColorsBody();
	setCountryBody();
	setCreation_storyBody();
	setCreaturesBody();
	setDescriptionBody();
	setEstablished_yearBody();
	setFloraBody();
	setMaterialsBody();
	setNameBody();
	setNearby_townsBody();
	setNotesBody();
	setOther_NamesBody();
	setPrivate_NotesBody();
	setSizeBody();
	setTagsBody();
	setType_of_landmarkBody();
	setUniverseBody();


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
