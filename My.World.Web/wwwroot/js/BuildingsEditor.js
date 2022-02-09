
var Address_editor;
var Affiliation_editor;
var Alternate_names_editor;
var Architect_editor;
var Architectural_style_editor;
var Description_editor;
var Developer_editor;
var Facade_editor;
var Notable_events_editor;
var Notes_editor;
var Owner_editor;
var Permits_editor;
var Private_Notes_editor;
var Purpose_editor;
var Tags_editor;
var Tenants_editor;
var Type_of_building_editor;


$(document).ready(function () {

	Address_editor = createEditor("#Address");
	Affiliation_editor = createEditor("#Affiliation");
	Alternate_names_editor = createEditor("#Alternate_names");
	Architect_editor = createEditor("#Architect");
	Architectural_style_editor = createEditor("#Architectural_style");
	Description_editor = createEditor("#Description");
	Developer_editor = createEditor("#Developer");
	Facade_editor = createEditor("#Facade");
	Notable_events_editor = createEditor("#Notable_events");
	Notes_editor = createEditor("#Notes");
	Owner_editor = createEditor("#Owner");
	Permits_editor = createEditor("#Permits");
	Private_Notes_editor = createEditor("#Private_Notes");
	Purpose_editor = createEditor("#Purpose");
	Tags_editor = createEditor("#Tags");
	Tenants_editor = createEditor("#Tenants");
	Type_of_building_editor = createEditor("#Type_of_building");


	setAddressBody();
	setAffiliationBody();
	setAlternate_namesBody();
	setArchitectBody();
	setArchitectural_styleBody();
	setCapacityBody();
	setConstructed_yearBody();
	setConstruction_costBody();
	setDescriptionBody();
	setDeveloperBody();
	setDimensionsBody();
	setFacadeBody();
	setFloor_countBody();
	setNameBody();
	setNotable_eventsBody();
	setNotesBody();
	setOwnerBody();
	setPermitsBody();
	setPriceBody();
	setPrivate_NotesBody();
	setPurposeBody();
	setTagsBody();
	setTenantsBody();
	setType_of_buildingBody();
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
