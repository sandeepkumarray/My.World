
var Berries_editor;
var Colorings_editor;
var Description_editor;
var Eaten_by_editor;
var Family_editor;
var Fruits_editor;
var Genus_editor;
var Locations_editor;
var Magical_effects_editor;
var Material_uses_editor;
var Medicinal_purposes_editor;
var Notes_editor;
var Nuts_editor;
var Order_editor;
var Other_Names_editor;
var Private_Notes_editor;
var Related_flora_editor;
var Reproduction_editor;
var Seasonality_editor;
var Seeds_editor;
var Size_editor;
var Smell_editor;
var Tags_editor;
var Taste_editor;


$(document).ready(function () {

	Berries_editor = createEditor("#Berries");
	Colorings_editor = createEditor("#Colorings");
	Description_editor = createEditor("#Description");
	Eaten_by_editor = createEditor("#Eaten_by");
	Family_editor = createEditor("#Family");
	Fruits_editor = createEditor("#Fruits");
	Genus_editor = createEditor("#Genus");
	Locations_editor = createEditor("#Locations");
	Magical_effects_editor = createEditor("#Magical_effects");
	Material_uses_editor = createEditor("#Material_uses");
	Medicinal_purposes_editor = createEditor("#Medicinal_purposes");
	Notes_editor = createEditor("#Notes");
	Nuts_editor = createEditor("#Nuts");
	Order_editor = createEditor("#Order");
	Other_Names_editor = createEditor("#Other_Names");
	Private_Notes_editor = createEditor("#Private_Notes");
	Related_flora_editor = createEditor("#Related_flora");
	Reproduction_editor = createEditor("#Reproduction");
	Seasonality_editor = createEditor("#Seasonality");
	Seeds_editor = createEditor("#Seeds");
	Size_editor = createEditor("#Size");
	Smell_editor = createEditor("#Smell");
	Tags_editor = createEditor("#Tags");
	Taste_editor = createEditor("#Taste");


	setBerriesBody();
	setColoringsBody();
	setDescriptionBody();
	setEaten_byBody();
	setFamilyBody();
	setFruitsBody();
	setGenusBody();
	setLocationsBody();
	setMagical_effectsBody();
	setMaterial_usesBody();
	setMedicinal_purposesBody();
	setNameBody();
	setNotesBody();
	setNutsBody();
	setOrderBody();
	setOther_NamesBody();
	setPrivate_NotesBody();
	setRelated_floraBody();
	setReproductionBody();
	setSeasonalityBody();
	setSeedsBody();
	setSizeBody();
	setSmellBody();
	setTagsBody();
	setTasteBody();
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
