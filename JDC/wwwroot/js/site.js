"use strict"

/*-----------------------------------------------------------------------------------*/
/*	PRELOADER
/*-----------------------------------------------------------------------------------*/
$(window).on('load', function () {
    $('#preloader-block').css('display', 'none');
    new WOW().init();
});


/*-----------------------------------------------------------------------------------*/
/*	Modal for adding grade
/*-----------------------------------------------------------------------------------*/
var button, selectedChat = 1;

$('#setGradeModal').on('show.bs.modal', function (event) {
    button = $(event.relatedTarget);
    var title = button.data('title');
    var modal = $(this);
    console.log(group.students.find(student => student.id == button.data('studentid')).grades);
    modal.find('.modal-title').text(title);
})

$('#setGradeModal').on('hide.bs.modal', function () {
    $('#grade').val('');
    $('#comment').val('');
    $('#absent').prop('checked', false);    
})

$('#setGradeButton').on('click', function () {
    button.html($('#grade').val());
    let grade = {};

    let comment = $('#comment').val().trim();
    if (comment.length > 0) {
        grade.Comment = comment;
        button.css('background-color', 'rgba(255, 0, 0, 0.4)');
    }

    if ($('#absent').prop('checked')) {
        grade.IsAbsent = true;
        button.css('background-color', 'rgba(255, 255, 0, 0.4)');
    }

    let studentId = button.data('studentid');
    let value = $('#grade').val();

    if (value.length > 0 && !isNaN(value)) {
        let studentGrades = group.students.find(student => student.id == studentId).grades;
        studentGrades.push({ value: +value });
        let average = studentGrades.reduce((accumulate, value) => accumulate + value.value, 0) / studentGrades.length;
        $(`#${studentId}average`).text(average);

        $.ajax({
            type: "POST",
            url: "Students/SetGrade",
            data: {
                studentId: studentId,
                lessonId: $('#viewtitle').data('lessonId'),
                value: value,
                date: button.data('date'),
            },
            dataType: "text",
            success: function () {
            },
            error: function (req, status, error) {
                console.log(error);
            }
        });
    }

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
    this.value = this.value.substring(0, 4);
    this.value = this.value.replace(/[^0-9]/g, '');
});

$('#hideJournalButton').on('click', function () {
    $('#hideableTable').toggleClass('d-none');
    $('.hide-button-icon').toggleClass('d-none');
});
