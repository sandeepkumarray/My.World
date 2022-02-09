
var Alternate_names_editor;
var Description_editor;
var Education_editor;
var Experience_editor;
var Field_editor;
var Initial_goal_editor;
var Job_origin_editor;
var Long_term_risks_editor;
var Notable_figures_editor;
var Notes_editor;
var Occupational_hazards_editor;
var Private_Notes_editor;
var Promotions_editor;
var Ranks_editor;
var Similar_jobs_editor;
var Specializations_editor;
var Tags_editor;
var Time_off_editor;
var Traditions_editor;
var Training_editor;
var Type_of_job_editor;
var Vehicles_editor;


$(document).ready(function () {

	Alternate_names_editor = createEditor("#Alternate_names");
	Description_editor = createEditor("#Description");
	Education_editor = createEditor("#Education");
	Experience_editor = createEditor("#Experience");
	Field_editor = createEditor("#Field");
	Initial_goal_editor = createEditor("#Initial_goal");
	Job_origin_editor = createEditor("#Job_origin");
	Long_term_risks_editor = createEditor("#Long_term_risks");
	Notable_figures_editor = createEditor("#Notable_figures");
	Notes_editor = createEditor("#Notes");
	Occupational_hazards_editor = createEditor("#Occupational_hazards");
	Private_Notes_editor = createEditor("#Private_Notes");
	Promotions_editor = createEditor("#Promotions");
	Ranks_editor = createEditor("#Ranks");
	Similar_jobs_editor = createEditor("#Similar_jobs");
	Specializations_editor = createEditor("#Specializations");
	Tags_editor = createEditor("#Tags");
	Time_off_editor = createEditor("#Time_off");
	Traditions_editor = createEditor("#Traditions");
	Training_editor = createEditor("#Training");
	Type_of_job_editor = createEditor("#Type_of_job");
	Vehicles_editor = createEditor("#Vehicles");


	setAlternate_namesBody();
	setDescriptionBody();
	setEducationBody();
	setExperienceBody();
	setFieldBody();
	setInitial_goalBody();
	setJob_originBody();
	setLong_term_risksBody();
	setNameBody();
	setNotable_figuresBody();
	setNotesBody();
	setOccupational_hazardsBody();
	setPay_rateBody();
	setPrivate_NotesBody();
	setPromotionsBody();
	setRanksBody();
	setSimilar_jobsBody();
	setSpecializationsBody();
	setTagsBody();
	setTime_offBody();
	setTraditionsBody();
	setTrainingBody();
	setType_of_jobBody();
	setUniverseBody();
	setVehiclesBody();
	setWork_hoursBody();


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
