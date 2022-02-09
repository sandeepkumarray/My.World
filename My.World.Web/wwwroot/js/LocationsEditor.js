
var Capital_cities_editor;
var Climate_editor;
var Crops_editor;
var Currency_editor;
var Description_editor;
var Founding_Story_editor;
var Landmarks_editor;
var Language_editor;
var Largest_cities_editor;
var Laws_editor;
var Leaders_editor;
var Located_at_editor;
var Motto_editor;
var Notable_cities_editor;
var Notable_Wars_editor;
var Notes_editor;
var Private_Notes_editor;
var Spoken_Languages_editor;
var Sports_editor;
var Tags_editor;
var Type_editor;


$(document).ready(function () {

	Capital_cities_editor = createEditor("#Capital_cities");
	Climate_editor = createEditor("#Climate");
	Crops_editor = createEditor("#Crops");
	Currency_editor = createEditor("#Currency");
	Description_editor = createEditor("#Description");
	Founding_Story_editor = createEditor("#Founding_Story");
	Landmarks_editor = createEditor("#Landmarks");
	Language_editor = createEditor("#Language");
	Largest_cities_editor = createEditor("#Largest_cities");
	Laws_editor = createEditor("#Laws");
	Leaders_editor = createEditor("#Leaders");
	Located_at_editor = createEditor("#Located_at");
	Motto_editor = createEditor("#Motto");
	Notable_cities_editor = createEditor("#Notable_cities");
	Notable_Wars_editor = createEditor("#Notable_Wars");
	Notes_editor = createEditor("#Notes");
	Private_Notes_editor = createEditor("#Private_Notes");
	Spoken_Languages_editor = createEditor("#Spoken_Languages");
	Sports_editor = createEditor("#Sports");
	Tags_editor = createEditor("#Tags");
	Type_editor = createEditor("#Type");


	setAreaBody();
	setCapital_citiesBody();
	setClimateBody();
	setCropsBody();
	setCurrencyBody();
	setDescriptionBody();
	setEstablished_YearBody();
	setFounding_StoryBody();
	setLandmarksBody();
	setLanguageBody();
	setLargest_citiesBody();
	setLawsBody();
	setLeadersBody();
	setLocated_atBody();
	setMottoBody();
	setNameBody();
	setNotable_citiesBody();
	setNotable_WarsBody();
	setNotesBody();
	setPopulationBody();
	setPrivate_NotesBody();
	setSpoken_LanguagesBody();
	setSportsBody();
	setTagsBody();
	setTypeBody();
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
