
var Characters_in_scene_editor;
var Description_editor;
var Items_in_scene_editor;
var Locations_in_scene_editor;
var Notes_editor;
var Private_notes_editor;
var Results_editor;
var Summary_editor;
var Tags_editor;
var What_caused_this_editor;


$(document).ready(function () {

	Characters_in_scene_editor = createEditor("#Characters_in_scene");
	Description_editor = createEditor("#Description");
	Items_in_scene_editor = createEditor("#Items_in_scene");
	Locations_in_scene_editor = createEditor("#Locations_in_scene");
	Notes_editor = createEditor("#Notes");
	Private_notes_editor = createEditor("#Private_notes");
	Results_editor = createEditor("#Results");
	Summary_editor = createEditor("#Summary");
	Tags_editor = createEditor("#Tags");
	What_caused_this_editor = createEditor("#What_caused_this");


	setCharacters_in_sceneBody();
	setDescriptionBody();
	setItems_in_sceneBody();
	setLocations_in_sceneBody();
	setNameBody();
	setNotesBody();
	setPrivate_notesBody();
	setResultsBody();
	setSummaryBody();
	setTagsBody();
	setUniverseBody();
	setWhat_caused_thisBody();


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
