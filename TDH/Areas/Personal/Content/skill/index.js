var deletedID;
var type = '';
var groupOrder;
var skillOrder;
var skillId;

$(document).ready(function () {
    var allowEdit = $('#edit').val();
    var allowDelete = $('#delete').val();
});

$(document).on('click', 'li.group_item > a', function (e) {
    $('li.group_item > a').removeClass('active');
    $(this).addClass('active');
    $('#selectedGroupID').val($(this).attr('data-id'));
    skillId = $(this).attr('data-id');
    UpdateSkillDisplay(skillId);
});

function UpdateGroupDisplay() {
    $.ajax({
        url: '/common/cmskill/getgroup',
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        success: function (response) {
            $('#list_group_display').empty();
            $.each(response, function (idx, item) {
                var li = $('<li/>').attr('class', 'nav-item group_item');
                var a = $('<a/>').attr('href', 'javascript:;').attr('class', 'nav-link').attr('data-id', item.ID).attr('data-name', item.Name);
                if (idx === 0) {
                    a.attr('class', 'nav-link active');
                    $('#selectedGroupID').val(item.ID);
                    skillId = item.ID;
                    UpdateSkillDisplay(item.ID);
                }
                a.append('<i class="fa fa-file-text-o"></i> ' + item.Name);
                a.append('<span class="badge bg-warning">' + item.CountSkill + '</span>');
                li.append(a);
                $('#list_group_display').append(li);
            });
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}


$('#skillDefinedModel').on('shown.bs.modal', function (e) {
    $('#cbLevel').focus();
});

function UpdateSkillDisplay(groupID) {
    $.ajax({
        url: '/personal/pnskill/getskillbygroup',
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        data: { groupID: groupID },
        success: function (response) {
            $('#list_skill_display').empty();
            $.each(response, function (idx, item) {
                var div = '';
                div += '<div class="col-md-6 col-xs-12">';
                div += '    <div class="card">';
                div += '        <div class="card-header bg-header">';
                div += '            <h3 class="card-title">' + item.Name + '</h3>';
                div += '            <div class="card-tools">';
                div += '                <span data-toggle="tooltip" title="' + item.Defined.length + '" class="badge badge-primary">' + item.Defined.length + '</span>';
                div += '            </div>';
                div += '        </div>';
                div += '        <div class="card-body direct-chat-messages">';
                div += '            <ul class="todo-list">';
                $.each(item.Defined, function (id, it) {
                    div += '            <li>';
                    div += '                <div class="progress-group">';
                    div += '                    <span class="progress-text">' + it.Name + '</span>';
                    div += '                    <span class="progress-number"><b>' + it.Level + '</b>/100</span>';
                    div += '                    <a class="tools">';
                    div += '                        <i class="fa fa-edit" style="float: right;" onclick="editLevel(\'' + it.ID + '\', \'' + item.ID + '\',\'' + item.Name + '\', \'' + it.Name + '\', ' + it.Level + ');"></i>';
                    div += '                    </a>';
                    div += '                    <div class="progress sm">';
                    div += '                        <div class="progress-bar progress-bar-aqua" style="width: ' + it.Level + '%"></div>';
                    div += '                    </div>';
                    div += '                </div>';
                    div += '            </li>';
                });
                div += '             </ul>';
                div += '        </div>';
                div += '    </div>';
                div += '</div>';
                $('#list_skill_display').append(div);
            });
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function editLevel(definedID, skillID, skillName, definedName, level) {
    $('#hdDefinedID').val(definedID);
    $('#lblSkillName').html(skillName);
    $('#lblDefinedName').html(definedName);
    $('#hdSkillID').val(skillID);
    $('#cbLevel').val(level);
    $('#skillDefinedModel').modal('show');
}

function saveLevel() {
    var url = '/personal/pnskill/saveskilldefined';
    loading($('#btnSaveSkillDefined'), 'show');
    $('#frmDefined').parsley().validate();
    $.ajax({
        url: url,
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({
            id: $('#hdDefinedID').val(),
            skillId: $('#hdSkillID').val(),
            level: $('#cbLevel').val()
        }),
        success: function (response) {
            UpdateSkillDisplay(skillId);
            loading($('#btnSaveSkillDefined'), 'hide');
            $('#skillDefinedModel').modal('hide');
        },
        error: function (xhr, status, error) {
            console.log(error);
            loading($('#btnSaveSkillDefined'), 'hide');
        }
    });
}


