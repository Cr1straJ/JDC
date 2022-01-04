"use strict"

/*-----------------------------------------------------------------------------------*/
/*	PRELOADER
/*-----------------------------------------------------------------------------------*/
$(window).on('load', function () {
    $('#preloader-block').css('display', 'none');
    new WOW().init();
});

$(function () {
    $('[data-toggle="popover"]').popover()
});

/*-----------------------------------------------------------------------------------*/
/*	Modal for adding grade
/*-----------------------------------------------------------------------------------*/
var button, gradeId, selectedChat = 1;

$('#setGradeModal').on('show.bs.modal', function (event) {
    button = $(event.relatedTarget);

    gradeId = button.attr('data-gradeid');
    if (!isNaN(gradeId)) {
        let currentGrade = group.students
            .find(student => student.id == button.data('studentid')).grades
            .find(grade => grade.id == gradeId);

        $('#grade').val(currentGrade.value);
        $('#comment').val(currentGrade.Comment);

        if (currentGrade.hasOwnProperty('IsAbsent') && currentGrade.IsAbsent) {
            $('#absent').prop('checked', true);
        }
    }

    var title = button.data('title');
    var modal = $(this);
    modal.find('.modal-title').text(title);
});

$('#setGradeModal').on('hide.bs.modal', function () {
    $('#grade').val('');
    $('#comment').val('');
    $('#absent').prop('checked', false);
});

$('#setGradeButton').on('click', function () {
    let grade = {};
    let value = $('#grade').val();
    let studentId = button.data('studentid');
    let comment = $('#comment').val().trim();
    let isAbsent = $('#absent').prop('checked');
    var studentGrades = group.students
        .find(student => student.id == studentId).grades
        .filter(grade => grade.lessonId == disciplineId);

    button.html(value);
    button.css('background-color', 'transparent');

    if (!isNaN(gradeId)) {
        grade = studentGrades.find(grade => grade.id == gradeId);

        if (comment.length == 0 && !isAbsent && (value.length == 0 || isNaN(value))) {
            button.removeAttr('data-gradeid');
            studentGrades = studentGrades.filter(function (grade) {
                return grade.id != gradeId;
            });
            group.students.find(student => student.id == button.data('studentid')).grades = studentGrades;
        }
        else {
            grade.Comment = comment;
            if (comment.length > 0) {
                button.css('background-color', 'rgba(255, 0, 0, 0.4)');
            }

            grade.IsAbsent = isAbsent;
            if (isAbsent) {
                button.css('background-color', 'rgba(255, 255, 0, 0.4)');
            }

            grade.value = null;
            if (value.length > 0 && !isNaN(value)) {
                grade.value = +value;
            }

            $.ajax({
                type: "POST",
                url: "UpdateGrade",
                data: {
                    gradeId: gradeId,
                    value: value
                },
                dataType: "text",
                success: function (result) {
                }
            });
        }
    }
    else {
        if (comment.length > 0) {
            grade.Comment = comment;
            button.css('background-color', 'rgba(255, 0, 0, 0.4)');
        }

        if ($('#absent').prop('checked')) {
            grade.IsAbsent = true;
            button.css('background-color', 'rgba(255, 255, 0, 0.4)');
        }

        if (value.length > 0 && !isNaN(value)) {
            grade.value = +value;
        }

        if (Object.keys(grade).length > 0) {
            $.ajax({
                type: "POST",
                url: "SetGrade",
                data: {
                    studentId: studentId,
                    disciplineId: $('#viewtitle').data('disciplineId'),
                    value: value,
                    date: button.data('date'),
                },
                dataType: "text",
                success: function (result) {
                }
            });

            grade.id = studentGrades.length;
            button.attr('data-gradeid', grade.id);
            studentGrades.push(grade);
        }
    }

    let average = studentGrades.reduce((accumulate, value) => accumulate + value.value, 0) / studentGrades.length;
    $(`#${studentId}average`).text(parseFloat((average).toFixed(2)));

    $('#setGradeModal').modal('hide');
});

$('.list_chats_item').on('click', function () {
    this.classList.add("selected");

    if (selectedChat != -1) {
        let currentChat = $(`#chat${selectedChat}`);
        currentChat.classList.remove("selected");
    }

    selectedChat = this.id.substr(4);
});

$('body').on('input', '.input_number', function () {
    this.value = this.value.substring(0, 4).replace(/[^0-9]/g, '');
});

$('#hideJournalButton').on('click', function () {
    $('#hideableTable').toggleClass('d-none');
    $('.hide-button-icon').toggleClass('d-none');
});

$('#gradeTable tbody tr').hover(function () {
    let id = $(this).attr('id').substring(3);
    $(this).toggleClass('back-lightgray');
    $(`#tr1${id}`).toggleClass('back-lightgray');
    $(`#tr2${id}`).toggleClass('back-lightgray');
});

$('#createLessonModal').on('show.bs.modal', function (event) {
    $('#theme').val('');
    $('#homework').val('');
    $('#lessonDuration').val(1);
});

$('#createLessonButton').on('click', function () {
    //add lesson to table

    $.ajax({
        type: "POST",
        url: "CreateLesson",
        data: {
            theme: $('#theme').val(),
            homework: $('#homework').val(),
            lessonDuration: $('#lessonDuration').val(),
            disciplineId: disciplineId,
        },
        dataType: "text",
        success: function (result) {
        }
    });

    $('#createLessonModal').modal('hide');
});

$('.request-action').on('click', function () {
    let action = $(this).data('action');
    let id = $(this).data('id');

    $.ajax({
        type: "POST",
        url: `Requests/${action}`,
        data: {
            id: id
        },
        dataType: "text",
        success: function (result) {
        }
    });

    if (window.location.href.indexOf("Details") != -1) {
        window.location.href = '/Admin/Requests';
    } else {
        $(`#request${id}`).remove();
    }
});

$('.change-status-button').on('click', function () {
    if ($(this).html().indexOf('unlock') != -1) {
        $(this).html('<i class="fa fa-lock text-danger"></i>');
    } else {
        $(this).html('<i class="fa fa-unlock text-success"></i>');
    }

    $.ajax({
        type: "POST",
        url: `Users/ChangeStatus`,
        data: {
            id: $(this).attr('id').substring(6)
        },
        dataType: "text",
        success: function (result) {
        }
    });
});