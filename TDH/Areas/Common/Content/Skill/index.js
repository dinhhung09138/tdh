var deletedID;
var type = '';
var groupOrder;
var skillOrder;
var skillId;

$(document).ready(function () {
    var allowEdit = $('#edit').val();
    var allowDelete = $('#delete').val();

    groupOrder = new AutoNumeric('#txtGroupOrdering', {
        minimumValue: '0',
        maximumValue: '99',
        selectNumberOnly: true,
        decimalPlaces: 0
    });

    skillOrder = new AutoNumeric('#txtSkillOrdering', {
        minimumValue: '0',
        maximumValue: '99',
        selectNumberOnly: true,
        decimalPlaces: 0
    });

});

$(document).on('click', 'li.group_item > a', function (e) {
    $('li.group_item > a').removeClass('active');
    $(this).addClass('active');
    $('#selectedGroupID').val($(this).attr('data-id'));
    skillId = $(this).attr('data-id');
    UpdateSkillDisplay(skillId);
});

function addGroup() {
    $('#hdGroupID').val('');
    $('#txtGroupName').val('');
    groupOrder.set(1);
    $('#txtGroupNotes').val('');
    $('#groupModel').modal('show');
}

function editGroup(id) {
    $.ajax({
        url: '/common/cmskill/getgroupitem',
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        data: { id: id },
        success: function (response) {
            $('#hdGroupID').val(id);
            $('#txtGroupName').val(response.Name);
            groupOrder.set(response.Ordering);
            $('#txtGroupNotes').val(response.Notes);
            $('#groupModel').modal('show');
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function saveGroup() {
    var url = '/common/cmskill/creategroup';
    if ($('#hdGroupID').val() !== '') {
        url = '/common/cmskill/editgroup';
    }
    loading($('#btnSaveGroup'), 'show');
    $('#frmGroup').parsley().validate();
    if ($('#frmGroup').parsley().isValid() === true) {
        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify({
                ID: $('#hdGroupID').val(),
                Name: $('#txtGroupName').val(),
                Ordering: $('#txtGroupOrdering').val(),
                Notes: $('#txtGroupNotes').val()
            }),
            success: function (response) {
                UpdateGroupDisplay();
                loading($('#btnSaveGroup'), 'hide');
                $('#groupModel').modal('hide');
            },
            error: function (xhr, status, error) {
                console.log(error);
                loading($('#btnSaveGroup'), 'hide');
            }
        });
    } else {
        loading($('#btnSaveGroup'), 'hide');
    }
}

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
                a.append('<i class="fa fa-trash-o float-right ml-5" onclick="confirmDeleteGroup(\'' + item.ID + '\');"></i>');
                a.append('<i class="fa fa-pencil-square-o float-right" onclick="editGroup(\'' + item.ID + '\');"></i>');
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

function confirmDeleteGroup(id) {
    type = 'group';
    $.ajax({
        url: '/common/cmskill/checkdeletegroup',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ ID: id }),
        success: function (response) {
            if (response === 3) {
                deletedID = id;
                $('#deleteModal').modal('show');
            } else {
                deletedID = '';
            }
            $('#deleteModal').modal('hide');
        },
        error: function (xhr, status, error) {
            console.log(error);
            $('#deleteModal').modal('hide');
        }
    });
}

function addSkill() {
    $('#hdSkillID').val('');
    $('#lblSkillGroupName').html($('li.group_item > a.active').attr('data-name'));
    $('#txtSkillName').val('');
    skillOrder.set(1);
    $('#txtSkillNotes').val('');
    $('#skillModel').modal('show');
}

function editSkill(id) {
    $.ajax({
        url: '/common/cmskill/getskillitem',
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        data: { id: id },
        success: function (response) {
            $('#lblSkillGroupName').html($('li.group_item > a.active').attr('data-name'));
            $('#hdSkillID').val(id);
            $('#txtSkillName').val(response.Name);
            skillOrder.set(response.Ordering);
            $('#txtSkillNotes').val(response.Notes);
            $('#skillModel').modal('show');
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function saveSkill() {
    var url = '/common/cmskill/createskill';
    if ($('#hdSkillID').val() !== '') {
        url = '/common/cmskill/editskill';
    }
    loading($('#btnSaveSkill'), 'show');
    $('#frmSkill').parsley().validate();
    if ($('#frmSkill').parsley().isValid() === true) {
        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify({
                ID: $('#hdSkillID').val(),
                GroupID: $('#selectedGroupID').val(),
                Name: $('#txtSkillName').val(),
                Ordering: $('#txtSkillOrdering').val(),
                Notes: $('#txtSkillNotes').val()
            }),
            success: function (response) {
                skillId = $('#selectedGroupID').val();
                UpdateSkillDisplay(skillId);
                loading($('#btnSaveSkill'), 'hide');
                $('#skillModel').modal('hide');
            },
            error: function (xhr, status, error) {
                console.log(error);
                loading($('#btnSaveSkill'), 'hide');
            }
        });
    } else {
        loading($('#btnSaveSkill'), 'hide');
    }
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
                div += '                <button type="button" class="btn btn-tool" onclick="addDefined(\'' + item.ID + '\',\'' + item.Name + '\');">';
                div += '                    <i class="fa fa-cog"></i>';
                div += '                </button>';
                div += '                <button type="button" class="btn btn-tool" onclick="editSkill(\'' + item.ID + '\');">';
                div += '                    <i class="fa fa-pencil-square-o"></i>';
                div += '                </button>';
                div += '                <button type="button" class="btn btn-tool" data-widget="collapse">';
                div += '                    <i class="fa fa-minus"></i>';
                div += '                </button>';
                div += '                <button type="button" class="btn btn-tool" onclick="confirmDeleteSkill(\'' + item.ID + '\');">';
                div += '                    <i class="fa fa-times"></i>';
                div += '                </button>';
                div += '            </div>';
                div += '        </div>';
                div += '        <div class="card-body direct-chat-messages">';
                div += '            <ul class="todo-list">';
                $.each(item.Defined, function (id, it) {
                    div += '            <li>';
                    div += '                <span class="text">' + it.Name + '</span>';
                    div += '                <div class="tools">';
                    div += '                    <i class="fa fa-edit" onclick="editDefined(\'' + it.ID + '\', \'' + item.ID + '\',\'' + item.Name + '\');"></i>';
                    div += '                    <i class="fa fa-trash-o" onclick="confirmDeleteDefined(\'' + it.ID + '\');"></i>';
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

function confirmDeleteSkill(id) {
    type = 'skill';
    $.ajax({
        url: '/common/cmskill/checkdeleteskill',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ ID: id }),
        success: function (response) {
            if (response === 3) {
                deletedID = id;
                $('#deleteModal').modal('show');
            } else {
                deletedID = '';
            }
            $('#deleteModal').modal('hide');
        },
        error: function (xhr, status, error) {
            console.log(error);
            $('#deleteModal').modal('hide');
        }
    });
}

function addDefined(skillID, skillName) {
    $('#lblSkillName').html(skillName);
    $('#hdSkillID').val(skillID);
    $('#hdDefinedID').val('');
    $('#txtDefinedName').val('');
    $('#txtDefinedDescription').val('');
    $('#skillDefinedModel').modal('show');
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

function confirmDeleteDefined(id) {
    type = 'defined';
    deletedID = id;
    $('#deleteModal').modal('show');
}

function deleteItem() {
    var url = '';
    if (type === 'group') {
        url = '/common/cmskill/deletegroup';
    }
    if (type === 'skill') {
        url = '/common/cmskill/deleteskill';
    }
    if (type === 'defined') {
        url = '/common/cmskill/deleteskilldefined';
    }

    $.ajax({
        url: url,
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ ID: deletedID }),
        success: function (response) {
            if (response === 0) {
                if (type === 'group') {
                    UpdateGroupDisplay();
                }
                if (type === 'skill') {
                    skillId = $('#selectedGroupID').val();
                    UpdateSkillDisplay(skillId);
                }
                if (type === 'defined') {
                    UpdateGroupDisplay();
                }
            }
            deletedID = '';
            type = '';
            $('#deleteModal').modal('hide');
        },
        error: function (xhr, status, error) {
            console.log(error);
            $('#deleteModal').modal('hide');
        }
    });
}

