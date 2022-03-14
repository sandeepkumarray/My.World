
var description_editor;
var genre_editor;
var history_editor;
var laws_of_physics_editor;
var magic_system_editor;
var notes_editor;
var private_notes_editor;
var technology_editor;


$(document).ready(function () {

	description_editor = createEditor("#description");
	genre_editor = createEditor("#genre");
	history_editor = createEditor("#history");
	laws_of_physics_editor = createEditor("#laws_of_physics");
	magic_system_editor = createEditor("#magic_system");
	notes_editor = createEditor("#notes");
	private_notes_editor = createEditor("#private_notes");
	technology_editor = createEditor("#technology");


	setdescriptionBody();
	setfavoriteBody();
	setgenreBody();
	sethistoryBody();
	setlaws_of_physicsBody();
	setmagic_systemBody();
	setnameBody();
	setnotesBody();
	setprivacyBody();
	setprivate_notesBody();
	settechnologyBody();


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
