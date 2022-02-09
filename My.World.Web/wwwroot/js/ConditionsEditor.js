
var Alternate_names_editor;
var Description_editor;
var Diagnostic_method_editor;
var Duration_editor;
var Environmental_factors_editor;
var Epidemiology_editor;
var Evolution_editor;
var Genetic_factors_editor;
var Immunization_editor;
var Lifestyle_factors_editor;
var Medication_editor;
var Mental_effects_editor;
var Notes_editor;
var Origin_editor;
var Prevention_editor;
var Private_Notes_editor;
var Prognosis_editor;
var Rarity_editor;
var Specialty_Field_editor;
var Symbolism_editor;
var Symptoms_editor;
var Tags_editor;
var Transmission_editor;
var Treatment_editor;
var Type_of_condition_editor;
var Variations_editor;
var Visual_effects_editor;


$(document).ready(function () {

	Alternate_names_editor = createEditor("#Alternate_names");
	Description_editor = createEditor("#Description");
	Diagnostic_method_editor = createEditor("#Diagnostic_method");
	Duration_editor = createEditor("#Duration");
	Environmental_factors_editor = createEditor("#Environmental_factors");
	Epidemiology_editor = createEditor("#Epidemiology");
	Evolution_editor = createEditor("#Evolution");
	Genetic_factors_editor = createEditor("#Genetic_factors");
	Immunization_editor = createEditor("#Immunization");
	Lifestyle_factors_editor = createEditor("#Lifestyle_factors");
	Medication_editor = createEditor("#Medication");
	Mental_effects_editor = createEditor("#Mental_effects");
	Notes_editor = createEditor("#Notes");
	Origin_editor = createEditor("#Origin");
	Prevention_editor = createEditor("#Prevention");
	Private_Notes_editor = createEditor("#Private_Notes");
	Prognosis_editor = createEditor("#Prognosis");
	Rarity_editor = createEditor("#Rarity");
	Specialty_Field_editor = createEditor("#Specialty_Field");
	Symbolism_editor = createEditor("#Symbolism");
	Symptoms_editor = createEditor("#Symptoms");
	Tags_editor = createEditor("#Tags");
	Transmission_editor = createEditor("#Transmission");
	Treatment_editor = createEditor("#Treatment");
	Type_of_condition_editor = createEditor("#Type_of_condition");
	Variations_editor = createEditor("#Variations");
	Visual_effects_editor = createEditor("#Visual_effects");


	setAlternate_namesBody();
	setDescriptionBody();
	setDiagnostic_methodBody();
	setDurationBody();
	setEnvironmental_factorsBody();
	setEpidemiologyBody();
	setEvolutionBody();
	setGenetic_factorsBody();
	setImmunizationBody();
	setLifestyle_factorsBody();
	setMedicationBody();
	setMental_effectsBody();
	setNameBody();
	setNotesBody();
	setOriginBody();
	setPreventionBody();
	setPrivate_NotesBody();
	setPrognosisBody();
	setRarityBody();
	setSpecialty_FieldBody();
	setSymbolismBody();
	setSymptomsBody();
	setTagsBody();
	setTransmissionBody();
	setTreatmentBody();
	setType_of_conditionBody();
	setUniverseBody();
	setVariationsBody();
	setVisual_effectsBody();


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
