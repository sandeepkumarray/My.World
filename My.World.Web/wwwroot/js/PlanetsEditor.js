
var Atmosphere_editor;
var Calendar_System_editor;
var Climate_editor;
var Continents_editor;
var Countries_editor;
var Creatures_editor;
var Day_sky_editor;
var Deities_editor;
var Description_editor;
var First_Inhabitants_Story_editor;
var Flora_editor;
var Groups_editor;
var Landmarks_editor;
var Languages_editor;
var Locations_editor;
var Moons_editor;
var Natural_diasters_editor;
var Natural_Resources_editor;
var Nearby_planets_editor;
var Night_sky_editor;
var Notes_editor;
var Orbit_editor;
var Private_Notes_editor;
var Races_editor;
var Religions_editor;
var Seasons_editor;
var Suns_editor;
var Tags_editor;
var Towns_editor;
var Visible_Constellations_editor;
var Weather_editor;
var World_History_editor;


$(document).ready(function () {

	Atmosphere_editor = createEditor("#Atmosphere");
	Calendar_System_editor = createEditor("#Calendar_System");
	Climate_editor = createEditor("#Climate");
	Continents_editor = createEditor("#Continents");
	Countries_editor = createEditor("#Countries");
	Creatures_editor = createEditor("#Creatures");
	Day_sky_editor = createEditor("#Day_sky");
	Deities_editor = createEditor("#Deities");
	Description_editor = createEditor("#Description");
	First_Inhabitants_Story_editor = createEditor("#First_Inhabitants_Story");
	Flora_editor = createEditor("#Flora");
	Groups_editor = createEditor("#Groups");
	Landmarks_editor = createEditor("#Landmarks");
	Languages_editor = createEditor("#Languages");
	Locations_editor = createEditor("#Locations");
	Moons_editor = createEditor("#Moons");
	Natural_diasters_editor = createEditor("#Natural_diasters");
	Natural_Resources_editor = createEditor("#Natural_Resources");
	Nearby_planets_editor = createEditor("#Nearby_planets");
	Night_sky_editor = createEditor("#Night_sky");
	Notes_editor = createEditor("#Notes");
	Orbit_editor = createEditor("#Orbit");
	Private_Notes_editor = createEditor("#Private_Notes");
	Races_editor = createEditor("#Races");
	Religions_editor = createEditor("#Religions");
	Seasons_editor = createEditor("#Seasons");
	Suns_editor = createEditor("#Suns");
	Tags_editor = createEditor("#Tags");
	Towns_editor = createEditor("#Towns");
	Visible_Constellations_editor = createEditor("#Visible_Constellations");
	Weather_editor = createEditor("#Weather");
	World_History_editor = createEditor("#World_History");


	setAtmosphereBody();
	setCalendar_SystemBody();
	setClimateBody();
	setContinentsBody();
	setCountriesBody();
	setCreaturesBody();
	setDay_skyBody();
	setDeitiesBody();
	setDescriptionBody();
	setFirst_Inhabitants_StoryBody();
	setFloraBody();
	setGroupsBody();
	setLandmarksBody();
	setLanguagesBody();
	setLength_Of_DayBody();
	setLength_Of_NightBody();
	setLocationsBody();
	setMoonsBody();
	setNameBody();
	setNatural_diastersBody();
	setNatural_ResourcesBody();
	setNearby_planetsBody();
	setNight_skyBody();
	setNotesBody();
	setOrbitBody();
	setPopulationBody();
	setPrivate_NotesBody();
	setRacesBody();
	setReligionsBody();
	setSeasonsBody();
	setSizeBody();
	setSunsBody();
	setSurfaceBody();
	setTagsBody();
	setTemperatureBody();
	setTownsBody();
	setUniverseBody();
	setVisible_ConstellationsBody();
	setWater_ContentBody();
	setWeatherBody();
	setWorld_HistoryBody();


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
