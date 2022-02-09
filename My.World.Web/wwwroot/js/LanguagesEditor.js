
var Body_parts_editor;
var Determiners_editor;
var Dialectical_information_editor;
var Evolution_editor;
var Family_editor;
var Gestures_editor;
var Goodbyes_editor;
var Grammar_editor;
var Greetings_editor;
var History_editor;
var No_words_editor;
var Notes_editor;
var Numbers_editor;
var Other_Names_editor;
var Phonology_editor;
var Please_editor;
var Private_notes_editor;
var Pronouns_editor;
var Quantifiers_editor;
var Register_editor;
var Sorry_editor;
var Tags_editor;
var Thank_you_editor;
var Trade_editor;
var Typology_editor;
var Yes_words_editor;
var You_are_welcome_editor;


$(document).ready(function () {

	Body_parts_editor = createEditor("#Body_parts");
	Determiners_editor = createEditor("#Determiners");
	Dialectical_information_editor = createEditor("#Dialectical_information");
	Evolution_editor = createEditor("#Evolution");
	Family_editor = createEditor("#Family");
	Gestures_editor = createEditor("#Gestures");
	Goodbyes_editor = createEditor("#Goodbyes");
	Grammar_editor = createEditor("#Grammar");
	Greetings_editor = createEditor("#Greetings");
	History_editor = createEditor("#History");
	No_words_editor = createEditor("#No_words");
	Notes_editor = createEditor("#Notes");
	Numbers_editor = createEditor("#Numbers");
	Other_Names_editor = createEditor("#Other_Names");
	Phonology_editor = createEditor("#Phonology");
	Please_editor = createEditor("#Please");
	Private_notes_editor = createEditor("#Private_notes");
	Pronouns_editor = createEditor("#Pronouns");
	Quantifiers_editor = createEditor("#Quantifiers");
	Register_editor = createEditor("#Register");
	Sorry_editor = createEditor("#Sorry");
	Tags_editor = createEditor("#Tags");
	Thank_you_editor = createEditor("#Thank_you");
	Trade_editor = createEditor("#Trade");
	Typology_editor = createEditor("#Typology");
	Yes_words_editor = createEditor("#Yes_words");
	You_are_welcome_editor = createEditor("#You_are_welcome");


	setBody_partsBody();
	setDeterminersBody();
	setDialectical_informationBody();
	setEvolutionBody();
	setFamilyBody();
	setGesturesBody();
	setGoodbyesBody();
	setGrammarBody();
	setGreetingsBody();
	setHistoryBody();
	setNameBody();
	setNo_wordsBody();
	setNotesBody();
	setNumbersBody();
	setOther_NamesBody();
	setPhonologyBody();
	setPleaseBody();
	setPrivate_notesBody();
	setPronounsBody();
	setQuantifiersBody();
	setRegisterBody();
	setSorryBody();
	setTagsBody();
	setThank_youBody();
	setTradeBody();
	setTypologyBody();
	setUniverseBody();
	setYes_wordsBody();
	setYou_are_welcomeBody();


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
