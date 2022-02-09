
var Architecture_editor;
var Bordering_countries_editor;
var Climate_editor;
var Creatures_editor;
var Crops_editor;
var Currency_editor;
var Description_editor;
var Education_editor;
var Flora_editor;
var Founding_story_editor;
var Governments_editor;
var Landmarks_editor;
var Languages_editor;
var Laws_editor;
var Locations_editor;
var Music_editor;
var Notable_wars_editor;
var Notes_editor;
var Other_Names_editor;
var Pop_culture_editor;
var Private_Notes_editor;
var Religions_editor;
var Social_hierarchy_editor;
var Sports_editor;
var Tags_editor;
var Towns_editor;


$(document).ready(function () {

	Architecture_editor = createEditor("#Architecture");
	Bordering_countries_editor = createEditor("#Bordering_countries");
	Climate_editor = createEditor("#Climate");
	Creatures_editor = createEditor("#Creatures");
	Crops_editor = createEditor("#Crops");
	Currency_editor = createEditor("#Currency");
	Description_editor = createEditor("#Description");
	Education_editor = createEditor("#Education");
	Flora_editor = createEditor("#Flora");
	Founding_story_editor = createEditor("#Founding_story");
	Governments_editor = createEditor("#Governments");
	Landmarks_editor = createEditor("#Landmarks");
	Languages_editor = createEditor("#Languages");
	Laws_editor = createEditor("#Laws");
	Locations_editor = createEditor("#Locations");
	Music_editor = createEditor("#Music");
	Notable_wars_editor = createEditor("#Notable_wars");
	Notes_editor = createEditor("#Notes");
	Other_Names_editor = createEditor("#Other_Names");
	Pop_culture_editor = createEditor("#Pop_culture");
	Private_Notes_editor = createEditor("#Private_Notes");
	Religions_editor = createEditor("#Religions");
	Social_hierarchy_editor = createEditor("#Social_hierarchy");
	Sports_editor = createEditor("#Sports");
	Tags_editor = createEditor("#Tags");
	Towns_editor = createEditor("#Towns");


	setArchitectureBody();
	setAreaBody();
	setBordering_countriesBody();
	setClimateBody();
	setCreaturesBody();
	setCropsBody();
	setCurrencyBody();
	setDescriptionBody();
	setEducationBody();
	setEstablished_yearBody();
	setFloraBody();
	setFounding_storyBody();
	setGovernmentsBody();
	setLandmarksBody();
	setLanguagesBody();
	setLawsBody();
	setLocationsBody();
	setMusicBody();
	setNameBody();
	setNotable_warsBody();
	setNotesBody();
	setOther_NamesBody();
	setPop_cultureBody();
	setPopulationBody();
	setPrivate_NotesBody();
	setReligionsBody();
	setSocial_hierarchyBody();
	setSportsBody();
	setTagsBody();
	setTownsBody();
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
