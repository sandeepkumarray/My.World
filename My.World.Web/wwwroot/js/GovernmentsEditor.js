
var Airforce_editor;
var Approval_Ratings_editor;
var Checks_And_Balances_editor;
var Civilian_Life_editor;
var Creatures_editor;
var Criminal_System_editor;
var Description_editor;
var Electoral_Process_editor;
var Flag_Design_Story_editor;
var Founding_Story_editor;
var Geocultural_editor;
var Groups_editor;
var Holidays_editor;
var Immigration_editor;
var International_Relations_editor;
var Items_editor;
var Jobs_editor;
var Laws_editor;
var Leaders_editor;
var Military_editor;
var Navy_editor;
var Notable_Wars_editor;
var Notes_editor;
var Political_figures_editor;
var Power_Source_editor;
var Power_Structure_editor;
var Privacy_Ideologies_editor;
var Private_Notes_editor;
var Socioeconomical_editor;
var Sociopolitical_editor;
var Space_Program_editor;
var Tags_editor;
var Technologies_editor;
var Term_Lengths_editor;
var Type_Of_Government_editor;
var Vehicles_editor;


$(document).ready(function () {

	Airforce_editor = createEditor("#Airforce");
	Approval_Ratings_editor = createEditor("#Approval_Ratings");
	Checks_And_Balances_editor = createEditor("#Checks_And_Balances");
	Civilian_Life_editor = createEditor("#Civilian_Life");
	Creatures_editor = createEditor("#Creatures");
	Criminal_System_editor = createEditor("#Criminal_System");
	Description_editor = createEditor("#Description");
	Electoral_Process_editor = createEditor("#Electoral_Process");
	Flag_Design_Story_editor = createEditor("#Flag_Design_Story");
	Founding_Story_editor = createEditor("#Founding_Story");
	Geocultural_editor = createEditor("#Geocultural");
	Groups_editor = createEditor("#Groups");
	Holidays_editor = createEditor("#Holidays");
	Immigration_editor = createEditor("#Immigration");
	International_Relations_editor = createEditor("#International_Relations");
	Items_editor = createEditor("#Items");
	Jobs_editor = createEditor("#Jobs");
	Laws_editor = createEditor("#Laws");
	Leaders_editor = createEditor("#Leaders");
	Military_editor = createEditor("#Military");
	Navy_editor = createEditor("#Navy");
	Notable_Wars_editor = createEditor("#Notable_Wars");
	Notes_editor = createEditor("#Notes");
	Political_figures_editor = createEditor("#Political_figures");
	Power_Source_editor = createEditor("#Power_Source");
	Power_Structure_editor = createEditor("#Power_Structure");
	Privacy_Ideologies_editor = createEditor("#Privacy_Ideologies");
	Private_Notes_editor = createEditor("#Private_Notes");
	Socioeconomical_editor = createEditor("#Socioeconomical");
	Sociopolitical_editor = createEditor("#Sociopolitical");
	Space_Program_editor = createEditor("#Space_Program");
	Tags_editor = createEditor("#Tags");
	Technologies_editor = createEditor("#Technologies");
	Term_Lengths_editor = createEditor("#Term_Lengths");
	Type_Of_Government_editor = createEditor("#Type_Of_Government");
	Vehicles_editor = createEditor("#Vehicles");


	setAirforceBody();
	setApproval_RatingsBody();
	setChecks_And_BalancesBody();
	setCivilian_LifeBody();
	setCreaturesBody();
	setCriminal_SystemBody();
	setDescriptionBody();
	setElectoral_ProcessBody();
	setFlag_Design_StoryBody();
	setFounding_StoryBody();
	setGeoculturalBody();
	setGroupsBody();
	setHolidaysBody();
	setImmigrationBody();
	setInternational_RelationsBody();
	setItemsBody();
	setJobsBody();
	setLawsBody();
	setLeadersBody();
	setMilitaryBody();
	setNameBody();
	setNavyBody();
	setNotable_WarsBody();
	setNotesBody();
	setPolitical_figuresBody();
	setPower_SourceBody();
	setPower_StructureBody();
	setPrivacy_IdeologiesBody();
	setPrivate_NotesBody();
	setSocioeconomicalBody();
	setSociopoliticalBody();
	setSpace_ProgramBody();
	setTagsBody();
	setTechnologiesBody();
	setTerm_LengthsBody();
	setType_Of_GovernmentBody();
	setUniverseBody();
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
