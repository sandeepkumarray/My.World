
var Current_Owners_editor;
var Description_editor;
var Item_Type_editor;
var Magic_editor;
var Magical_effects_editor;
var Makers_editor;
var Materials_editor;
var Notes_editor;
var Original_Owners_editor;
var Past_Owners_editor;
var Private_Notes_editor;
var Tags_editor;
var Technical_effects_editor;
var Technology_editor;


$(document).ready(function () {

	Current_Owners_editor = createEditor("#Current_Owners");
	Description_editor = createEditor("#Description");
	Item_Type_editor = createEditor("#Item_Type");
	Magic_editor = createEditor("#Magic");
	Magical_effects_editor = createEditor("#Magical_effects");
	Makers_editor = createEditor("#Makers");
	Materials_editor = createEditor("#Materials");
	Notes_editor = createEditor("#Notes");
	Original_Owners_editor = createEditor("#Original_Owners");
	Past_Owners_editor = createEditor("#Past_Owners");
	Private_Notes_editor = createEditor("#Private_Notes");
	Tags_editor = createEditor("#Tags");
	Technical_effects_editor = createEditor("#Technical_effects");
	Technology_editor = createEditor("#Technology");


	setCurrent_OwnersBody();
	setDescriptionBody();
	setItem_TypeBody();
	setMagicBody();
	setMagical_effectsBody();
	setMakersBody();
	setMaterialsBody();
	setNameBody();
	setNotesBody();
	setOriginal_OwnersBody();
	setPast_OwnersBody();
	setPrivate_NotesBody();
	setTagsBody();
	setTechnical_effectsBody();
	setTechnologyBody();
	setUniverseBody();
	setWeightBody();
	setYear_it_was_madeBody();


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
