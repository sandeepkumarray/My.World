
var Background_information_editor;
var Believability_editor;
var Believers_editor;
var Buildings_editor;
var Characters_editor;
var Conditions_editor;
var Continents_editor;
var Countries_editor;
var Created_phrases_editor;
var Created_traditions_editor;
var Creatures_editor;
var Criticism_editor;
var Date_recorded_editor;
var Deities_editor;
var Dialect_editor;
var Evolution_over_time_editor;
var False_parts_editor;
var Floras_editor;
var Foods_editor;
var Full_text_editor;
var Genre_editor;
var Geographical_variations_editor;
var Governments_editor;
var Groups_editor;
var Historical_context_editor;
var Hoaxes_editor;
var Impact_editor;
var Important_translations_editor;
var Influence_on_modern_times_editor;
var Inspirations_editor;
var Interpretations_editor;
var Jobs_editor;
var Landmarks_editor;
var Magic_editor;
var Media_adaptations_editor;
var Morals_editor;
var Motivations_editor;
var Notes_editor;
var Original_author_editor;
var Original_languages_editor;
var Original_telling_editor;
var Planets_editor;
var Private_Notes_editor;
var Propagation_method_editor;
var Races_editor;
var Reception_editor;
var Related_lores_editor;
var Religions_editor;
var Schools_editor;
var Source_editor;
var Sports_editor;
var Structure_editor;
var Subjects_editor;
var Summary_editor;
var Symbolisms_editor;
var Tags_editor;
var Technologies_editor;
var Time_period_editor;
var Tone_editor;
var Towns_editor;
var Traditions_editor;
var Translation_variations_editor;
var True_parts_editor;
var Type_editor;
var Variations_editor;
var Vehicles_editor;


$(document).ready(function () {

	Background_information_editor = createEditor("#Background_information");
	Believability_editor = createEditor("#Believability");
	Believers_editor = createEditor("#Believers");
	Buildings_editor = createEditor("#Buildings");
	Characters_editor = createEditor("#Characters");
	Conditions_editor = createEditor("#Conditions");
	Continents_editor = createEditor("#Continents");
	Countries_editor = createEditor("#Countries");
	Created_phrases_editor = createEditor("#Created_phrases");
	Created_traditions_editor = createEditor("#Created_traditions");
	Creatures_editor = createEditor("#Creatures");
	Criticism_editor = createEditor("#Criticism");
	Date_recorded_editor = createEditor("#Date_recorded");
	Deities_editor = createEditor("#Deities");
	Dialect_editor = createEditor("#Dialect");
	Evolution_over_time_editor = createEditor("#Evolution_over_time");
	False_parts_editor = createEditor("#False_parts");
	Floras_editor = createEditor("#Floras");
	Foods_editor = createEditor("#Foods");
	Full_text_editor = createEditor("#Full_text");
	Genre_editor = createEditor("#Genre");
	Geographical_variations_editor = createEditor("#Geographical_variations");
	Governments_editor = createEditor("#Governments");
	Groups_editor = createEditor("#Groups");
	Historical_context_editor = createEditor("#Historical_context");
	Hoaxes_editor = createEditor("#Hoaxes");
	Impact_editor = createEditor("#Impact");
	Important_translations_editor = createEditor("#Important_translations");
	Influence_on_modern_times_editor = createEditor("#Influence_on_modern_times");
	Inspirations_editor = createEditor("#Inspirations");
	Interpretations_editor = createEditor("#Interpretations");
	Jobs_editor = createEditor("#Jobs");
	Landmarks_editor = createEditor("#Landmarks");
	Magic_editor = createEditor("#Magic");
	Media_adaptations_editor = createEditor("#Media_adaptations");
	Morals_editor = createEditor("#Morals");
	Motivations_editor = createEditor("#Motivations");
	Notes_editor = createEditor("#Notes");
	Original_author_editor = createEditor("#Original_author");
	Original_languages_editor = createEditor("#Original_languages");
	Original_telling_editor = createEditor("#Original_telling");
	Planets_editor = createEditor("#Planets");
	Private_Notes_editor = createEditor("#Private_Notes");
	Propagation_method_editor = createEditor("#Propagation_method");
	Races_editor = createEditor("#Races");
	Reception_editor = createEditor("#Reception");
	Related_lores_editor = createEditor("#Related_lores");
	Religions_editor = createEditor("#Religions");
	Schools_editor = createEditor("#Schools");
	Source_editor = createEditor("#Source");
	Sports_editor = createEditor("#Sports");
	Structure_editor = createEditor("#Structure");
	Subjects_editor = createEditor("#Subjects");
	Summary_editor = createEditor("#Summary");
	Symbolisms_editor = createEditor("#Symbolisms");
	Tags_editor = createEditor("#Tags");
	Technologies_editor = createEditor("#Technologies");
	Time_period_editor = createEditor("#Time_period");
	Tone_editor = createEditor("#Tone");
	Towns_editor = createEditor("#Towns");
	Traditions_editor = createEditor("#Traditions");
	Translation_variations_editor = createEditor("#Translation_variations");
	True_parts_editor = createEditor("#True_parts");
	Type_editor = createEditor("#Type");
	Variations_editor = createEditor("#Variations");
	Vehicles_editor = createEditor("#Vehicles");


	setBackground_informationBody();
	setBelievabilityBody();
	setBelieversBody();
	setBuildingsBody();
	setCharactersBody();
	setConditionsBody();
	setContinentsBody();
	setCountriesBody();
	setCreated_phrasesBody();
	setCreated_traditionsBody();
	setCreaturesBody();
	setCriticismBody();
	setDate_recordedBody();
	setDeitiesBody();
	setDialectBody();
	setEvolution_over_timeBody();
	setFalse_partsBody();
	setFlorasBody();
	setFoodsBody();
	setFull_textBody();
	setGenreBody();
	setGeographical_variationsBody();
	setGovernmentsBody();
	setGroupsBody();
	setHistorical_contextBody();
	setHoaxesBody();
	setImpactBody();
	setImportant_translationsBody();
	setInfluence_on_modern_timesBody();
	setInspirationsBody();
	setInterpretationsBody();
	setJobsBody();
	setLandmarksBody();
	setMagicBody();
	setMedia_adaptationsBody();
	setMoralsBody();
	setMotivationsBody();
	setNameBody();
	setNotesBody();
	setOriginal_authorBody();
	setOriginal_languagesBody();
	setOriginal_tellingBody();
	setPlanetsBody();
	setPrivate_NotesBody();
	setPropagation_methodBody();
	setRacesBody();
	setReceptionBody();
	setRelated_loresBody();
	setReligionsBody();
	setSchoolsBody();
	setSourceBody();
	setSportsBody();
	setStructureBody();
	setSubjectsBody();
	setSummaryBody();
	setSymbolismsBody();
	setTagsBody();
	setTechnologiesBody();
	setTime_periodBody();
	setToneBody();
	setTownsBody();
	setTraditionsBody();
	setTranslation_variationsBody();
	setTrue_partsBody();
	setTypeBody();
	setUniverseBody();
	setVariationsBody();
	setVehiclesBody();


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
