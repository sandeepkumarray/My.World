
var Aggressiveness_editor;
var Ancestors_editor;
var Class_editor;
var Competitors_editor;
var Conditions_editor;
var Description_editor;
var Evolutionary_drive_editor;
var Family_editor;
var Food_sources_editor;
var Genus_editor;
var Habitats_editor;
var Herding_patterns_editor;
var Materials_editor;
var Mating_ritual_editor;
var Method_of_attack_editor;
var Methods_of_defense_editor;
var Migratory_patterns_editor;
var Mortality_rate_editor;
var Notable_features_editor;
var Notes_editor;
var Offspring_care_editor;
var Order_editor;
var Parental_instincts_editor;
var Phylum_editor;
var Predators_editor;
var Predictions_editor;
var Preferred_habitat_editor;
var Prey_editor;
var Private_notes_editor;
var Related_creatures_editor;
var Reproduction_editor;
var Reproduction_frequency_editor;
var Requirements_editor;
var Shape_editor;
var Similar_creatures_editor;
var Sounds_editor;
var Species_editor;
var Spoils_editor;
var Strengths_editor;
var Strongest_sense_editor;
var Symbolisms_editor;
var Tags_editor;
var Tradeoffs_editor;
var Type_of_creature_editor;
var Variations_editor;
var Vestigial_features_editor;
var Weakest_sense_editor;
var Weaknesses_editor;


$(document).ready(function () {

	Aggressiveness_editor = createEditor("#Aggressiveness");
	Ancestors_editor = createEditor("#Ancestors");
	Class_editor = createEditor("#Class");
	Competitors_editor = createEditor("#Competitors");
	Conditions_editor = createEditor("#Conditions");
	Description_editor = createEditor("#Description");
	Evolutionary_drive_editor = createEditor("#Evolutionary_drive");
	Family_editor = createEditor("#Family");
	Food_sources_editor = createEditor("#Food_sources");
	Genus_editor = createEditor("#Genus");
	Habitats_editor = createEditor("#Habitats");
	Herding_patterns_editor = createEditor("#Herding_patterns");
	Materials_editor = createEditor("#Materials");
	Mating_ritual_editor = createEditor("#Mating_ritual");
	Method_of_attack_editor = createEditor("#Method_of_attack");
	Methods_of_defense_editor = createEditor("#Methods_of_defense");
	Migratory_patterns_editor = createEditor("#Migratory_patterns");
	Mortality_rate_editor = createEditor("#Mortality_rate");
	Notable_features_editor = createEditor("#Notable_features");
	Notes_editor = createEditor("#Notes");
	Offspring_care_editor = createEditor("#Offspring_care");
	Order_editor = createEditor("#Order");
	Parental_instincts_editor = createEditor("#Parental_instincts");
	Phylum_editor = createEditor("#Phylum");
	Predators_editor = createEditor("#Predators");
	Predictions_editor = createEditor("#Predictions");
	Preferred_habitat_editor = createEditor("#Preferred_habitat");
	Prey_editor = createEditor("#Prey");
	Private_notes_editor = createEditor("#Private_notes");
	Related_creatures_editor = createEditor("#Related_creatures");
	Reproduction_editor = createEditor("#Reproduction");
	Reproduction_frequency_editor = createEditor("#Reproduction_frequency");
	Requirements_editor = createEditor("#Requirements");
	Shape_editor = createEditor("#Shape");
	Similar_creatures_editor = createEditor("#Similar_creatures");
	Sounds_editor = createEditor("#Sounds");
	Species_editor = createEditor("#Species");
	Spoils_editor = createEditor("#Spoils");
	Strengths_editor = createEditor("#Strengths");
	Strongest_sense_editor = createEditor("#Strongest_sense");
	Symbolisms_editor = createEditor("#Symbolisms");
	Tags_editor = createEditor("#Tags");
	Tradeoffs_editor = createEditor("#Tradeoffs");
	Type_of_creature_editor = createEditor("#Type_of_creature");
	Variations_editor = createEditor("#Variations");
	Vestigial_features_editor = createEditor("#Vestigial_features");
	Weakest_sense_editor = createEditor("#Weakest_sense");
	Weaknesses_editor = createEditor("#Weaknesses");


	setAggressivenessBody();
	setAncestorsBody();
	setClassBody();
	setColorBody();
	setCompetitorsBody();
	setConditionsBody();
	setDescriptionBody();
	setEvolutionary_driveBody();
	setFamilyBody();
	setFood_sourcesBody();
	setGenusBody();
	setHabitatsBody();
	setHeightBody();
	setHerding_patternsBody();
	setMaterialsBody();
	setMating_ritualBody();
	setMaximum_speedBody();
	setMethod_of_attackBody();
	setMethods_of_defenseBody();
	setMigratory_patternsBody();
	setMortality_rateBody();
	setNameBody();
	setNotable_featuresBody();
	setNotesBody();
	setOffspring_careBody();
	setOrderBody();
	setParental_instinctsBody();
	setPhylumBody();
	setPredatorsBody();
	setPredictionsBody();
	setPreferred_habitatBody();
	setPreyBody();
	setPrivate_notesBody();
	setRelated_creaturesBody();
	setReproductionBody();
	setReproduction_ageBody();
	setReproduction_frequencyBody();
	setRequirementsBody();
	setShapeBody();
	setSimilar_creaturesBody();
	setSizeBody();
	setSoundsBody();
	setSpeciesBody();
	setSpoilsBody();
	setStrengthsBody();
	setStrongest_senseBody();
	setSymbolismsBody();
	setTagsBody();
	setTradeoffsBody();
	setType_of_creatureBody();
	setUniverseBody();
	setVariationsBody();
	setVestigial_featuresBody();
	setWeakest_senseBody();
	setWeaknessesBody();
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
