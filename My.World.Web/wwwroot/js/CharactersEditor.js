
var Age_editor;
var Aliases_editor;
var Background_editor;
var Birthday_editor;
var Birthplace_editor;
var Bodytype_editor;
var Education_editor;
var Eyecolor_editor;
var Facialhair_editor;
var Fave_animal_editor;
var Fave_color_editor;
var Fave_food_editor;
var Fave_possession_editor;
var Fave_weapon_editor;
var Flaws_editor;
var Gender_editor;
var Haircolor_editor;
var Hairstyle_editor;
var Height_editor;
var Hobbies_editor;
var Identmarks_editor;
var Mannerisms_editor;
var Motivations_editor;
var Notes_editor;
var Occupation_editor;
var Personality_type_editor;
var Pets_editor;
var Politics_editor;
var Prejudices_editor;
var Privacy_editor;
var Private_notes_editor;
var Race_editor;
var Religion_editor;
var Role_editor;
var Skintone_editor;
var Talents_editor;
var Weight_editor;


$(document).ready(function () {

	Age_editor = createEditor("#Age");
	Aliases_editor = createEditor("#Aliases");
	Background_editor = createEditor("#Background");
	Birthday_editor = createEditor("#Birthday");
	Birthplace_editor = createEditor("#Birthplace");
	Bodytype_editor = createEditor("#Bodytype");
	Education_editor = createEditor("#Education");
	Eyecolor_editor = createEditor("#Eyecolor");
	Facialhair_editor = createEditor("#Facialhair");
	Fave_animal_editor = createEditor("#Fave_animal");
	Fave_color_editor = createEditor("#Fave_color");
	Fave_food_editor = createEditor("#Fave_food");
	Fave_possession_editor = createEditor("#Fave_possession");
	Fave_weapon_editor = createEditor("#Fave_weapon");
	Flaws_editor = createEditor("#Flaws");
	Gender_editor = createEditor("#Gender");
	Haircolor_editor = createEditor("#Haircolor");
	Hairstyle_editor = createEditor("#Hairstyle");
	Height_editor = createEditor("#Height");
	Hobbies_editor = createEditor("#Hobbies");
	Identmarks_editor = createEditor("#Identmarks");
	Mannerisms_editor = createEditor("#Mannerisms");
	Motivations_editor = createEditor("#Motivations");
	Notes_editor = createEditor("#Notes");
	Occupation_editor = createEditor("#Occupation");
	Personality_type_editor = createEditor("#Personality_type");
	Pets_editor = createEditor("#Pets");
	Politics_editor = createEditor("#Politics");
	Prejudices_editor = createEditor("#Prejudices");
	Privacy_editor = createEditor("#Privacy");
	Private_notes_editor = createEditor("#Private_notes");
	Race_editor = createEditor("#Race");
	Religion_editor = createEditor("#Religion");
	Role_editor = createEditor("#Role");
	Skintone_editor = createEditor("#Skintone");
	Talents_editor = createEditor("#Talents");
	Weight_editor = createEditor("#Weight");


	setAgeBody();
	setAliasesBody();
	setBackgroundBody();
	setBirthdayBody();
	setBirthplaceBody();
	setBodytypeBody();
	setEducationBody();
	setEyecolorBody();
	setFacialhairBody();
	setFave_animalBody();
	setFave_colorBody();
	setFave_foodBody();
	setFave_possessionBody();
	setFave_weaponBody();
	setFavoriteBody();
	setFlawsBody();
	setGenderBody();
	setHaircolorBody();
	setHairstyleBody();
	setHeightBody();
	setHobbiesBody();
	setIdentmarksBody();
	setMannerismsBody();
	setMotivationsBody();
	setNameBody();
	setNotesBody();
	setOccupationBody();
	setPersonality_typeBody();
	setPetsBody();
	setPoliticsBody();
	setPrejudicesBody();
	setPrivacyBody();
	setPrivate_notesBody();
	setRaceBody();
	setReligionBody();
	setRoleBody();
	setSkintoneBody();
	setTalentsBody();
	setUniverseBody();
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
