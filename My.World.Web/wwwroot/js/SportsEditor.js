
var Common_injuries_editor;
var Countries_editor;
var Creators_editor;
var Description_editor;
var Equipment_editor;
var Evolution_editor;
var Famous_games_editor;
var How_to_win_editor;
var Merchandise_editor;
var Most_important_muscles_editor;
var Nicknames_editor;
var Notes_editor;
var Origin_story_editor;
var Penalties_editor;
var Play_area_editor;
var Players_editor;
var Popularity_editor;
var Private_Notes_editor;
var Rules_editor;
var Scoring_editor;
var Strategies_editor;
var Tags_editor;
var Teams_editor;
var Traditions_editor;
var Uniforms_editor;


$(document).ready(function () {

	Common_injuries_editor = createEditor("#Common_injuries");
	Countries_editor = createEditor("#Countries");
	Creators_editor = createEditor("#Creators");
	Description_editor = createEditor("#Description");
	Equipment_editor = createEditor("#Equipment");
	Evolution_editor = createEditor("#Evolution");
	Famous_games_editor = createEditor("#Famous_games");
	How_to_win_editor = createEditor("#How_to_win");
	Merchandise_editor = createEditor("#Merchandise");
	Most_important_muscles_editor = createEditor("#Most_important_muscles");
	Nicknames_editor = createEditor("#Nicknames");
	Notes_editor = createEditor("#Notes");
	Origin_story_editor = createEditor("#Origin_story");
	Penalties_editor = createEditor("#Penalties");
	Play_area_editor = createEditor("#Play_area");
	Players_editor = createEditor("#Players");
	Popularity_editor = createEditor("#Popularity");
	Private_Notes_editor = createEditor("#Private_Notes");
	Rules_editor = createEditor("#Rules");
	Scoring_editor = createEditor("#Scoring");
	Strategies_editor = createEditor("#Strategies");
	Tags_editor = createEditor("#Tags");
	Teams_editor = createEditor("#Teams");
	Traditions_editor = createEditor("#Traditions");
	Uniforms_editor = createEditor("#Uniforms");


	setCommon_injuriesBody();
	setCountriesBody();
	setCreatorsBody();
	setDescriptionBody();
	setEquipmentBody();
	setEvolutionBody();
	setFamous_gamesBody();
	setGame_timeBody();
	setHow_to_winBody();
	setMerchandiseBody();
	setMost_important_musclesBody();
	setNameBody();
	setNicknamesBody();
	setNotesBody();
	setNumber_of_playersBody();
	setOrigin_storyBody();
	setPenaltiesBody();
	setPlay_areaBody();
	setPlayersBody();
	setPopularityBody();
	setPositionsBody();
	setPrivate_NotesBody();
	setRulesBody();
	setScoringBody();
	setStrategiesBody();
	setTagsBody();
	setTeamsBody();
	setTraditionsBody();
	setUniformsBody();
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
