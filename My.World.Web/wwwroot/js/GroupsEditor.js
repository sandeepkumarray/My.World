
var Allies_editor;
var Clients_editor;
var Creatures_editor;
var Description_editor;
var Enemies_editor;
var Equipment_editor;
var Goals_editor;
var Headquarters_editor;
var Inventory_editor;
var Key_items_editor;
var Leaders_editor;
var Locations_editor;
var Members_editor;
var Motivations_editor;
var Notes_editor;
var Obstacles_editor;
var Offices_editor;
var Organization_structure_editor;
var Other_Names_editor;
var Private_notes_editor;
var Risks_editor;
var Rivals_editor;
var Sistergroups_editor;
var Subgroups_editor;
var Supergroups_editor;
var Suppliers_editor;
var Tags_editor;
var Traditions_editor;


$(document).ready(function () {

	Allies_editor = createEditor("#Allies");
	Clients_editor = createEditor("#Clients");
	Creatures_editor = createEditor("#Creatures");
	Description_editor = createEditor("#Description");
	Enemies_editor = createEditor("#Enemies");
	Equipment_editor = createEditor("#Equipment");
	Goals_editor = createEditor("#Goals");
	Headquarters_editor = createEditor("#Headquarters");
	Inventory_editor = createEditor("#Inventory");
	Key_items_editor = createEditor("#Key_items");
	Leaders_editor = createEditor("#Leaders");
	Locations_editor = createEditor("#Locations");
	Members_editor = createEditor("#Members");
	Motivations_editor = createEditor("#Motivations");
	Notes_editor = createEditor("#Notes");
	Obstacles_editor = createEditor("#Obstacles");
	Offices_editor = createEditor("#Offices");
	Organization_structure_editor = createEditor("#Organization_structure");
	Other_Names_editor = createEditor("#Other_Names");
	Private_notes_editor = createEditor("#Private_notes");
	Risks_editor = createEditor("#Risks");
	Rivals_editor = createEditor("#Rivals");
	Sistergroups_editor = createEditor("#Sistergroups");
	Subgroups_editor = createEditor("#Subgroups");
	Supergroups_editor = createEditor("#Supergroups");
	Suppliers_editor = createEditor("#Suppliers");
	Tags_editor = createEditor("#Tags");
	Traditions_editor = createEditor("#Traditions");


	setAlliesBody();
	setClientsBody();
	setCreaturesBody();
	setDescriptionBody();
	setEnemiesBody();
	setEquipmentBody();
	setGoalsBody();
	setHeadquartersBody();
	setInventoryBody();
	setKey_itemsBody();
	setLeadersBody();
	setLocationsBody();
	setMembersBody();
	setMotivationsBody();
	setNameBody();
	setNotesBody();
	setObstaclesBody();
	setOfficesBody();
	setOrganization_structureBody();
	setOther_NamesBody();
	setPrivate_notesBody();
	setRisksBody();
	setRivalsBody();
	setSistergroupsBody();
	setSubgroupsBody();
	setSupergroupsBody();
	setSuppliersBody();
	setTagsBody();
	setTraditionsBody();
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
