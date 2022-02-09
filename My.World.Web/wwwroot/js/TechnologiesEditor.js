
var Characters_editor;
var Child_technologies_editor;
var Colors_editor;
var Countries_editor;
var Creatures_editor;
var Description_editor;
var Groups_editor;
var How_It_Works_editor;
var Magic_effects_editor;
var Manufacturing_Process_editor;
var Materials_editor;
var Notes_editor;
var Other_Names_editor;
var Parent_technologies_editor;
var Physical_Description_editor;
var Planets_editor;
var Private_Notes_editor;
var Purpose_editor;
var Rarity_editor;
var Related_technologies_editor;
var Resources_Used_editor;
var Sales_Process_editor;
var Tags_editor;
var Towns_editor;


$(document).ready(function () {

	Characters_editor = createEditor("#Characters");
	Child_technologies_editor = createEditor("#Child_technologies");
	Colors_editor = createEditor("#Colors");
	Countries_editor = createEditor("#Countries");
	Creatures_editor = createEditor("#Creatures");
	Description_editor = createEditor("#Description");
	Groups_editor = createEditor("#Groups");
	How_It_Works_editor = createEditor("#How_It_Works");
	Magic_effects_editor = createEditor("#Magic_effects");
	Manufacturing_Process_editor = createEditor("#Manufacturing_Process");
	Materials_editor = createEditor("#Materials");
	Notes_editor = createEditor("#Notes");
	Other_Names_editor = createEditor("#Other_Names");
	Parent_technologies_editor = createEditor("#Parent_technologies");
	Physical_Description_editor = createEditor("#Physical_Description");
	Planets_editor = createEditor("#Planets");
	Private_Notes_editor = createEditor("#Private_Notes");
	Purpose_editor = createEditor("#Purpose");
	Rarity_editor = createEditor("#Rarity");
	Related_technologies_editor = createEditor("#Related_technologies");
	Resources_Used_editor = createEditor("#Resources_Used");
	Sales_Process_editor = createEditor("#Sales_Process");
	Tags_editor = createEditor("#Tags");
	Towns_editor = createEditor("#Towns");


	setCharactersBody();
	setChild_technologiesBody();
	setColorsBody();
	setCostBody();
	setCountriesBody();
	setCreaturesBody();
	setDescriptionBody();
	setGroupsBody();
	setHow_It_WorksBody();
	setMagic_effectsBody();
	setManufacturing_ProcessBody();
	setMaterialsBody();
	setNameBody();
	setNotesBody();
	setOther_NamesBody();
	setParent_technologiesBody();
	setPhysical_DescriptionBody();
	setPlanetsBody();
	setPrivate_NotesBody();
	setPurposeBody();
	setRarityBody();
	setRelated_technologiesBody();
	setResources_UsedBody();
	setSales_ProcessBody();
	setSizeBody();
	setTagsBody();
	setTownsBody();
	setUniverseBody();
	setWeightBody();


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
