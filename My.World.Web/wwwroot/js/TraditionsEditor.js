
var Activities_editor;
var Alternate_names_editor;
var Countries_editor;
var Dates_editor;
var Description_editor;
var Etymology_editor;
var Food_editor;
var Games_editor;
var Gifts_editor;
var Groups_editor;
var Notable_events_editor;
var Notes_editor;
var Origin_editor;
var Private_Notes_editor;
var Religions_editor;
var Significance_editor;
var Symbolism_editor;
var Tags_editor;
var Towns_editor;
var Type_of_tradition_editor;


$(document).ready(function () {

	Activities_editor = createEditor("#Activities");
	Alternate_names_editor = createEditor("#Alternate_names");
	Countries_editor = createEditor("#Countries");
	Dates_editor = createEditor("#Dates");
	Description_editor = createEditor("#Description");
	Etymology_editor = createEditor("#Etymology");
	Food_editor = createEditor("#Food");
	Games_editor = createEditor("#Games");
	Gifts_editor = createEditor("#Gifts");
	Groups_editor = createEditor("#Groups");
	Notable_events_editor = createEditor("#Notable_events");
	Notes_editor = createEditor("#Notes");
	Origin_editor = createEditor("#Origin");
	Private_Notes_editor = createEditor("#Private_Notes");
	Religions_editor = createEditor("#Religions");
	Significance_editor = createEditor("#Significance");
	Symbolism_editor = createEditor("#Symbolism");
	Tags_editor = createEditor("#Tags");
	Towns_editor = createEditor("#Towns");
	Type_of_tradition_editor = createEditor("#Type_of_tradition");


	setActivitiesBody();
	setAlternate_namesBody();
	setCountriesBody();
	setDatesBody();
	setDescriptionBody();
	setEtymologyBody();
	setFoodBody();
	setGamesBody();
	setGiftsBody();
	setGroupsBody();
	setNameBody();
	setNotable_eventsBody();
	setNotesBody();
	setOriginBody();
	setPrivate_NotesBody();
	setReligionsBody();
	setSignificanceBody();
	setSymbolismBody();
	setTagsBody();
	setTownsBody();
	setType_of_traditionBody();
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
