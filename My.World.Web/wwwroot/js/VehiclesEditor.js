
var Alternate_names_editor;
var Colors_editor;
var Description_editor;
var Designer_editor;
var Distance_editor;
var Features_editor;
var Manufacturer_editor;
var Materials_editor;
var Notes_editor;
var Owner_editor;
var Private_Notes_editor;
var Safety_editor;
var Tags_editor;
var Type_of_vehicle_editor;
var Variants_editor;


$(document).ready(function () {

	Alternate_names_editor = createEditor("#Alternate_names");
	Colors_editor = createEditor("#Colors");
	Description_editor = createEditor("#Description");
	Designer_editor = createEditor("#Designer");
	Distance_editor = createEditor("#Distance");
	Features_editor = createEditor("#Features");
	Manufacturer_editor = createEditor("#Manufacturer");
	Materials_editor = createEditor("#Materials");
	Notes_editor = createEditor("#Notes");
	Owner_editor = createEditor("#Owner");
	Private_Notes_editor = createEditor("#Private_Notes");
	Safety_editor = createEditor("#Safety");
	Tags_editor = createEditor("#Tags");
	Type_of_vehicle_editor = createEditor("#Type_of_vehicle");
	Variants_editor = createEditor("#Variants");


	setAlternate_namesBody();
	setColorsBody();
	setCostsBody();
	setCountryBody();
	setDescriptionBody();
	setDesignerBody();
	setDimensionsBody();
	setDistanceBody();
	setDoorsBody();
	setFeaturesBody();
	setFuelBody();
	setManufacturerBody();
	setMaterialsBody();
	setNameBody();
	setNotesBody();
	setOwnerBody();
	setPrivate_NotesBody();
	setSafetyBody();
	setSizeBody();
	setSpeedBody();
	setTagsBody();
	setType_of_vehicleBody();
	setUniverseBody();
	setVariantsBody();
	setWeightBody();
	setWindowsBody();


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
