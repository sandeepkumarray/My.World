
var Aftereffects_editor;
var Conditions_editor;
var Deities_editor;
var Description_editor;
var Education_editor;
var Effects_editor;
var Element_editor;
var Limitations_editor;
var Materials_required_editor;
var Negative_effects_editor;
var Neutral_effects_editor;
var Notes_editor;
var Positive_effects_editor;
var Private_notes_editor;
var Resource_costs_editor;
var Skills_required_editor;
var Tags_editor;
var Type_of_magic_editor;
var Visuals_editor;


$(document).ready(function () {

	Aftereffects_editor = createEditor("#Aftereffects");
	Conditions_editor = createEditor("#Conditions");
	Deities_editor = createEditor("#Deities");
	Description_editor = createEditor("#Description");
	Education_editor = createEditor("#Education");
	Effects_editor = createEditor("#Effects");
	Element_editor = createEditor("#Element");
	Limitations_editor = createEditor("#Limitations");
	Materials_required_editor = createEditor("#Materials_required");
	Negative_effects_editor = createEditor("#Negative_effects");
	Neutral_effects_editor = createEditor("#Neutral_effects");
	Notes_editor = createEditor("#Notes");
	Positive_effects_editor = createEditor("#Positive_effects");
	Private_notes_editor = createEditor("#Private_notes");
	Resource_costs_editor = createEditor("#Resource_costs");
	Skills_required_editor = createEditor("#Skills_required");
	Tags_editor = createEditor("#Tags");
	Type_of_magic_editor = createEditor("#Type_of_magic");
	Visuals_editor = createEditor("#Visuals");


	setAftereffectsBody();
	setConditionsBody();
	setDeitiesBody();
	setDescriptionBody();
	setEducationBody();
	setEffectsBody();
	setElementBody();
	setLimitationsBody();
	setMaterials_requiredBody();
	setNameBody();
	setNegative_effectsBody();
	setNeutral_effectsBody();
	setNotesBody();
	setPositive_effectsBody();
	setPrivate_notesBody();
	setResource_costsBody();
	setScaleBody();
	setSkills_requiredBody();
	setTagsBody();
	setType_of_magicBody();
	setUniverseBody();
	setVisualsBody();


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
