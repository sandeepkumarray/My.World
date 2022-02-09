function UniverseSelected() {
    var value = $('#ddlUniverseList').val();
    console.log(value);
    postSaveData('Universe', value);
}

$(document).ready(function () {
   
});

function loadUniverseList(existing_value){
    var url = '/universes/GetAllUniverses';
    var ddlUniverseList = $('#ddlUniverseList');
    ddlUniverseList.empty();

    var optionSelect = $("<option />");
    optionSelect.html("Select Universe");
    optionSelect.val(-1);
    ddlUniverseList.append(optionSelect);

    $.get(url, function (data) {
        if (data != null) {

            var result = data.map(function (item) {

                var option = $("<option />");
                option.html(item.name);
                option.val(item.id);
                ddlUniverseList.append(option);

            });


            ddlUniverseList.val(existing_value);
        }
    });
}