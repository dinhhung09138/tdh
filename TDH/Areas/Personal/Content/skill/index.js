var deletedID;
var type = '';
var groupOrder;
var skillOrder;
var skillId;

$(document).ready(function () {
    var allowEdit = $('#edit').val();
    var allowDelete = $('#delete').val();

    //groupOrder = new AutoNumeric('#txtGroupOrdering', {
    //    minimumValue: '0',
    //    maximumValue: '99',
    //    selectNumberOnly: true,
    //    decimalPlaces: 0
    //});
    
});

$(document).on('click', 'li.group_item > a', function (e) {
    $('li.group_item > a').removeClass('active');
    $(this).addClass('active');
    $('#selectedGroupID').val($(this).attr('data-id'));
    skillId = $(this).attr('data-id');
    UpdateSkillDisplay(skillId);
});

$('#groupModel').on('shown.bs.modal', function (e) {
    $('#txtGroupName').focus();
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


$('#skillModel').on('shown.bs.modal', function (e) {
    $('#txtSkillName').focus();
});

function UpdateSkillDisplay(groupID) {
    $.ajax({
        url: '/common/cmskill/getskillbygroup',
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
                    div += '                <span class="text">' + it.Name + '</span>';
                    div += '                <div class="tools">';
                    div += '                    <i class="fa fa-edit" onclick="editDefined(\'' + it.ID + '\', \'' + item.ID + '\',\'' + item.Name + '\');"></i>';
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

function editDefined(definedID, skillID, skillName) {
    $.ajax({
        url: '/common/cmskill/getskilldefineditem',
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        data: { id: definedID },
        success: function (response) {
            $('#hdDefinedID').val(definedID);
            $('#lblSkillName').html(skillName);
            $('#hdSkillID').val(skillID);
            $('#txtDefinedName').val(response.Name);
            skillOrder.set(response.Ordering);
            $('#txtDefinedDescription').val(response.Description);
            $('#skillDefinedModel').modal('show');
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function saveDefined() {
    var url = '/common/cmskill/createskilldefined';
    if ($('#hdDefinedID').val() !== '') {
        url = '/common/cmskill/editskilldefined';
    }
    loading($('#btnSaveSkillDefined'), 'show');
    $('#frmDefined').parsley().validate();
    if ($('#frmDefined').parsley().isValid() === true) {
        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify({
                ID: $('#hdDefinedID').val(),
                SkillID: $('#hdSkillID').val(),
                Name: $('#txtDefinedName').val(),
                Description: $('#txtDefinedDescription').val()
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
    } else {
        loading($('#btnSaveSkillDefined'), 'hide');
    }
}

$('#skillDefinedModel').on('shown.bs.modal', function (e) {
    $('#txtDefinedName').focus();
});

