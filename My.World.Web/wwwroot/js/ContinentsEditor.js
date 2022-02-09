
var Architecture_editor;
var Bodies_of_water_editor;
var Countries_editor;
var Creatures_editor;
var Crops_editor;
var Demonym_editor;
var Description_editor;
var Discovery_editor;
var Economy_editor;
var Floras_editor;
var Formation_editor;
var Governments_editor;
var Humidity_editor;
var Landmarks_editor;
var Languages_editor;
var Local_name_editor;
var Mineralogy_editor;
var Natural_disasters_editor;
var Notes_editor;
var Other_Names_editor;
var Politics_editor;
var Popular_foods_editor;
var Population_editor;
var Precipitation_editor;
var Private_Notes_editor;
var Regional_advantages_editor;
var Regional_disadvantages_editor;
var Reputation_editor;
var Ruins_editor;
var Seasons_editor;
var Shape_editor;
var Tags_editor;
var Temperature_editor;
var Topography_editor;
var Tourism_editor;
var Traditions_editor;
var Wars_editor;
var Winds_editor;


$(document).ready(function () {

	Architecture_editor = createEditor("#Architecture");
	Bodies_of_water_editor = createEditor("#Bodies_of_water");
	Countries_editor = createEditor("#Countries");
	Creatures_editor = createEditor("#Creatures");
	Crops_editor = createEditor("#Crops");
	Demonym_editor = createEditor("#Demonym");
	Description_editor = createEditor("#Description");
	Discovery_editor = createEditor("#Discovery");
	Economy_editor = createEditor("#Economy");
	Floras_editor = createEditor("#Floras");
	Formation_editor = createEditor("#Formation");
	Governments_editor = createEditor("#Governments");
	Humidity_editor = createEditor("#Humidity");
	Landmarks_editor = createEditor("#Landmarks");
	Languages_editor = createEditor("#Languages");
	Local_name_editor = createEditor("#Local_name");
	Mineralogy_editor = createEditor("#Mineralogy");
	Natural_disasters_editor = createEditor("#Natural_disasters");
	Notes_editor = createEditor("#Notes");
	Other_Names_editor = createEditor("#Other_Names");
	Politics_editor = createEditor("#Politics");
	Popular_foods_editor = createEditor("#Popular_foods");
	Population_editor = createEditor("#Population");
	Precipitation_editor = createEditor("#Precipitation");
	Private_Notes_editor = createEditor("#Private_Notes");
	Regional_advantages_editor = createEditor("#Regional_advantages");
	Regional_disadvantages_editor = createEditor("#Regional_disadvantages");
	Reputation_editor = createEditor("#Reputation");
	Ruins_editor = createEditor("#Ruins");
	Seasons_editor = createEditor("#Seasons");
	Shape_editor = createEditor("#Shape");
	Tags_editor = createEditor("#Tags");
	Temperature_editor = createEditor("#Temperature");
	Topography_editor = createEditor("#Topography");
	Tourism_editor = createEditor("#Tourism");
	Traditions_editor = createEditor("#Traditions");
	Wars_editor = createEditor("#Wars");
	Winds_editor = createEditor("#Winds");


	setArchitectureBody();
	setAreaBody();
	setBodies_of_waterBody();
	setCountriesBody();
	setCreaturesBody();
	setCropsBody();
	setDemonymBody();
	setDescriptionBody();
	setDiscoveryBody();
	setEconomyBody();
	setFlorasBody();
	setFormationBody();
	setGovernmentsBody();
	setHumidityBody();
	setLandmarksBody();
	setLanguagesBody();
	setLocal_nameBody();
	setMineralogyBody();
	setNatural_disastersBody();
	setNotesBody();
	setOther_NamesBody();
	setPoliticsBody();
	setPopular_foodsBody();
	setPopulationBody();
	setPrecipitationBody();
	setPrivate_NotesBody();
	setRegional_advantagesBody();
	setRegional_disadvantagesBody();
	setReputationBody();
	setRuinsBody();
	setSeasonsBody();
	setShapeBody();
	setTagsBody();
	setTemperatureBody();
	setTopographyBody();
	setTourismBody();
	setTraditionsBody();
	setUniverseBody();
	setWarsBody();
	setWindsBody();


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
