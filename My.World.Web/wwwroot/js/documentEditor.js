var quill_editor;
var wordCount;

$(document).ready(function () {

    quill_editor = new Quill('#editor', {
        theme: 'snow',
        modules: {
            toolbar: [
                ['bold', 'italic', 'underline', 'strike'],
                ['link'],
                ['blockquote'],
                [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                [{ 'script': 'sub' }, { 'script': 'super' }],
                ['align', { 'align': 'center' }, { 'align': 'right' }, { 'align': 'justify' }]
            ],
            counter: {
                container: '#counter',
                unit: 'word'
            },
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
        placeholder: 'write your document contents here...',
    });

    $("#title").blur(function () {
        postSaveUniverseData("title", $('#title').val());
    });

    $("#editor").focusout(function () {
        postSaveUniverseData("body", quill_editor.root.innerHTML);
        postSaveUniverseData("wordcount", wordCount);
    });

    setBody();
});

function getQuillMentions(searchTerm) {
    const allPeople = [
        {
            id: 1,
            value: "Fredrik Sundqvist"
        },
        {
            id: 2,
            value: "Patrik Sjölin"
        }
    ];
    var url = '/Document/GetMentions/' + searchTerm;

    $.get(url, function (data) {
        if (data != null) {
            return data.filter(person => person.value.includes(searchTerm));
        }
    });

    
}

class Counter {
    constructor(quill, options) {
        this.quill = quill;
        this.options = options;
        this.container = document.querySelector(options.container);
        quill.on('text-change', this.update.bind(this));
        this.update();  // Account for initial contents
    }

    calculate() {
        let text = this.quill.getText();
        if (this.options.unit === 'word') {
            text = text.trim();
            // Splitting empty text returns a non-empty array
            return text.length > 0 ? text.split(/\s+/).length : 0;
        } else {
            return text.length;
        }
    }

    update() {
        var length = this.calculate();
        var label = this.options.unit;
        if (length !== 1) {
            label += 's';
        }
        this.container.innerText = length + ' ' + label;
        wordCount = length.toString();
    }
}

Quill.register('modules/counter', Counter);

function htmlEncode(value) {
    return $('<div/>').text(value).html();
}

function htmlDecode(value) {
    return $('<div/>').html(value).text();
}