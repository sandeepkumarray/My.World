
var Address_editor;
var Alternate_names_editor;
var Description_editor;
var Headquarters_editor;
var Locations_editor;
var Members_editor;
var Notes_editor;
var Offices_editor;
var Organization_structure_editor;
var Owner_editor;
var Private_Notes_editor;
var Purpose_editor;
var Rival_organizations_editor;
var Services_editor;
var Sister_organizations_editor;
var Sub_organizations_editor;
var Super_organizations_editor;
var Tags_editor;
var Type_of_organization_editor;


$(document).ready(function () {

	Address_editor = createEditor("#Address");
	Alternate_names_editor = createEditor("#Alternate_names");
	Description_editor = createEditor("#Description");
	Headquarters_editor = createEditor("#Headquarters");
	Locations_editor = createEditor("#Locations");
	Members_editor = createEditor("#Members");
	Notes_editor = createEditor("#Notes");
	Offices_editor = createEditor("#Offices");
	Organization_structure_editor = createEditor("#Organization_structure");
	Owner_editor = createEditor("#Owner");
	Private_Notes_editor = createEditor("#Private_Notes");
	Purpose_editor = createEditor("#Purpose");
	Rival_organizations_editor = createEditor("#Rival_organizations");
	Services_editor = createEditor("#Services");
	Sister_organizations_editor = createEditor("#Sister_organizations");
	Sub_organizations_editor = createEditor("#Sub_organizations");
	Super_organizations_editor = createEditor("#Super_organizations");
	Tags_editor = createEditor("#Tags");
	Type_of_organization_editor = createEditor("#Type_of_organization");


	setAddressBody();
	setAlternate_namesBody();
	setClosure_yearBody();
	setDescriptionBody();
	setFormation_yearBody();
	setHeadquartersBody();
	setLocationsBody();
	setMembersBody();
	setNameBody();
	setNotesBody();
	setOfficesBody();
	setOrganization_structureBody();
	setOwnerBody();
	setPrivate_NotesBody();
	setPurposeBody();
	setRival_organizationsBody();
	setServicesBody();
	setSister_organizationsBody();
	setSub_organizationsBody();
	setSuper_organizationsBody();
	setTagsBody();
	setType_of_organizationBody();
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
