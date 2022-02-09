
var Artifacts_editor;
var Deities_editor;
var Description_editor;
var Holidays_editor;
var Initiation_process_editor;
var Notable_figures_editor;
var Notes_editor;
var Obligations_editor;
var Origin_story_editor;
var Other_Names_editor;
var Places_of_worship_editor;
var Practicing_locations_editor;
var Practicing_races_editor;
var Private_notes_editor;
var Prophecies_editor;
var Rituals_editor;
var Tags_editor;
var Teachings_editor;
var Traditions_editor;
var Vision_of_paradise_editor;
var Worship_services_editor;


$(document).ready(function () {

	Artifacts_editor = createEditor("#Artifacts");
	Deities_editor = createEditor("#Deities");
	Description_editor = createEditor("#Description");
	Holidays_editor = createEditor("#Holidays");
	Initiation_process_editor = createEditor("#Initiation_process");
	Notable_figures_editor = createEditor("#Notable_figures");
	Notes_editor = createEditor("#Notes");
	Obligations_editor = createEditor("#Obligations");
	Origin_story_editor = createEditor("#Origin_story");
	Other_Names_editor = createEditor("#Other_Names");
	Places_of_worship_editor = createEditor("#Places_of_worship");
	Practicing_locations_editor = createEditor("#Practicing_locations");
	Practicing_races_editor = createEditor("#Practicing_races");
	Private_notes_editor = createEditor("#Private_notes");
	Prophecies_editor = createEditor("#Prophecies");
	Rituals_editor = createEditor("#Rituals");
	Tags_editor = createEditor("#Tags");
	Teachings_editor = createEditor("#Teachings");
	Traditions_editor = createEditor("#Traditions");
	Vision_of_paradise_editor = createEditor("#Vision_of_paradise");
	Worship_services_editor = createEditor("#Worship_services");


	setArtifactsBody();
	setDeitiesBody();
	setDescriptionBody();
	setHolidaysBody();
	setInitiation_processBody();
	setNameBody();
	setNotable_figuresBody();
	setNotesBody();
	setObligationsBody();
	setOrigin_storyBody();
	setOther_NamesBody();
	setPlaces_of_worshipBody();
	setPracticing_locationsBody();
	setPracticing_racesBody();
	setPrivate_notesBody();
	setPropheciesBody();
	setRitualsBody();
	setTagsBody();
	setTeachingsBody();
	setTraditionsBody();
	setUniverseBody();
	setVision_of_paradiseBody();
	setWorship_servicesBody();


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
