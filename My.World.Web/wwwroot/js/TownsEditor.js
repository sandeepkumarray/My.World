
var Busy_areas_editor;
var Creatures_editor;
var Description_editor;
var Energy_sources_editor;
var Flora_editor;
var Food_sources_editor;
var Founding_story_editor;
var Groups_editor;
var Landmarks_editor;
var Languages_editor;
var Laws_editor;
var Notes_editor;
var Other_names_editor;
var Politics_editor;
var Private_Notes_editor;
var Recycling_editor;
var Sports_editor;
var Tags_editor;
var Waste_editor;


$(document).ready(function () {

	Busy_areas_editor = createEditor("#Busy_areas");
	Creatures_editor = createEditor("#Creatures");
	Description_editor = createEditor("#Description");
	Energy_sources_editor = createEditor("#Energy_sources");
	Flora_editor = createEditor("#Flora");
	Food_sources_editor = createEditor("#Food_sources");
	Founding_story_editor = createEditor("#Founding_story");
	Groups_editor = createEditor("#Groups");
	Landmarks_editor = createEditor("#Landmarks");
	Languages_editor = createEditor("#Languages");
	Laws_editor = createEditor("#Laws");
	Notes_editor = createEditor("#Notes");
	Other_names_editor = createEditor("#Other_names");
	Politics_editor = createEditor("#Politics");
	Private_Notes_editor = createEditor("#Private_Notes");
	Recycling_editor = createEditor("#Recycling");
	Sports_editor = createEditor("#Sports");
	Tags_editor = createEditor("#Tags");
	Waste_editor = createEditor("#Waste");


	setBuildingsBody();
	setBusy_areasBody();
	setCitizensBody();
	setCountryBody();
	setCreaturesBody();
	setDescriptionBody();
	setEnergy_sourcesBody();
	setEstablished_yearBody();
	setFloraBody();
	setFood_sourcesBody();
	setFounding_storyBody();
	setGroupsBody();
	setLandmarksBody();
	setLanguagesBody();
	setLawsBody();
	setNameBody();
	setNeighborhoodsBody();
	setNotesBody();
	setOther_namesBody();
	setPoliticsBody();
	setPrivate_NotesBody();
	setRecyclingBody();
	setSportsBody();
	setTagsBody();
	setUniverseBody();
	setWasteBody();


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
