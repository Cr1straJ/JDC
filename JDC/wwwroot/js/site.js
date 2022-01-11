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

$('.counter').each(function () {
    $(this).prop('Counter', +$(this).text() * 0.25).animate({
        Counter: $(this).text()
    }, {
        duration: 2000,
        easing: 'swing',
        step: function (now) {
            if ($(this).hasClass('float-number')) {
                $(this).text(parseFloat((now).toFixed(2)));
            } else {
                $(this).text(Math.ceil(now));
            }            
        }
    });
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

$('.delete-user-button').on('click', function () {
    let id = $(this).attr('id');

    $.ajax({
        type: "POST",
        url: `Users/Delete`,
        data: {
            id: id
        },
        dataType: "text",
        success: function (result) {
        }
    });

    if (window.location.href.indexOf("Details") != -1) {
        window.location.href = '/Admin/Users';
    } else {
        $(`#user${id}`).remove();
    }
});


/*
    Init admin chart
 */

$(function () {
    let width, height, gradient;

    function getGradient(ctx, chartArea) {
        const chartWidth = chartArea.right - chartArea.left;
        const chartHeight = chartArea.bottom - chartArea.top;
        if (!gradient || width !== chartWidth || height !== chartHeight) {
            width = chartWidth;
            height = chartHeight;
            gradient = ctx.createLinearGradient(0, chartArea.bottom, 0, chartArea.top);
            gradient.addColorStop(0, 'rgb(255, 99, 132)');
            gradient.addColorStop(0.5, 'rgb(255, 205, 86)');
            gradient.addColorStop(1, 'rgb(75, 192, 192)');
        }

        return gradient;
    }

    let canvas = document.getElementById('myChart');
    if (canvas == null) {
        return;
    }

    const ctx = canvas.getContext('2d');
    const myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [
                'Январь',
                'Февраль',
                'Март',
                'Апрель',
                'Май',
                'Июнь',
                'Июль',
                'Август',
                'Сентябрь',
                'Октябрь',
                'Ноябрь',
                'Декабрь'
            ],
            datasets: [{
                data: [12, 19, 3, 5, 2, 3, 19, 3, 5, 2, 3, 7],
                borderColor: function (context) {
                    const chart = context.chart;
                    const { ctx, chartArea } = chart;

                    if (!chartArea) {
                        // This case happens on initial chart load
                        return;
                    }

                    return getGradient(ctx, chartArea);
                },
                borderWidth: 4
            }]
        },
        options: {
            animations: {
                radius: {
                    duration: 400,
                    easing: 'linear',
                    loop: (context) => context.active
                }
            },
            plugins: {
                legend: false,
            },
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
});

/*
    institution index page
*/
$(function () {
    if (window.location.href.indexOf("Institutions/Index") > 0) {
        printInstitutions(institutions);
    }

    $('body').on('input', '#inputInstitutionTitle', function () {
        getInstitutions();
    });

    $('.institute-type-check').click(function () {
        getInstitutions();
    });

    function getSelectedTypes() {
        return Array.prototype.filter.call($(`.institute-type-check`), function (item) { return item.checked }).map(item => +item.getAttribute('data-id'));
    }

    function getInstitutions() {
        var mask = $('#inputInstitutionTitle').val();

        $.ajax({
            type: 'POST',
            url: '/Institutions/GetInstitutions',
            data: {
                filter: mask,
                types: getSelectedTypes()
            },
            dataType: 'json',
            success: function (respons) {
                printInstitutions(respons);
            },
        })
    }

    function printInstitutions(institutions) {
        $('#institutionContainer').val('');
        institutions.forEach(institution => generateInstitutionsCard(institution));
    }

    function generateInstitutionsCard(institution) {
        var dropdown = '<a class="dropdown-item text-primary" href="' + urlDetails.replace('__id__', institution.id) + '">' +
            '<i class="fa fa-info-circle mr-1"></i>Подробнее</a>';

        if (isEditable) {
            dropdown = '<a class="dropdown-item text-warning" href="' + urlEdit.replace('__id__', institution.id) + '">' +
                '<i class="fa fa-pencil mr-1"></i>Редактировать</a>' + dropdown + '<a class="dropdown-item text-danger" href="' + urlDelete.replace('__id__', institution.id) + '">' +
                '<i class="fa fa-trash mr-1"></i>Удалить</a>';
        }

        var cardheader = '<div class="card-header overflow-hidden p-0">' + '<img src="https://st3.depositphotos.com/1007963/36020/i/600/depositphotos_360209078-stock-photo-classic-view-of-the-univeristy.jpg" alt="rover" />' +
            '<div class="card-menu"><div class="dropdown open off-animation">' +
            '<a href="" id="triggerId2" class="institute-card-dropdown" data-toggle="dropdown" aria-haspopup="true"' +
            'aria-expanded="false"><i class="fa fa-ellipsis-v"></i></a>' +
            '<div class="dropdown-menu off-animation" aria-labelledby="triggerId2">' +
            dropdown + '</div></div></div></div>';

        var cardbody = '<div class="institution-card-body">' + '<span class="tag tag-teal">' + institution.type + '</span>' +
            `<p>${institution.name}</p><div class="user"><img src="/images/default_avatar.png" alt="Директор" />` +
            '<div class="user-info"><h5>' + (institution.director != null ? institution.director.getShortName : 'No name') + '</h5>' +
            '</div></div></div>';

        let card = `<div id="${institution.id}" class="col-sm-6 col-md-6 col-lg-4"><div class="institution-card position-relative">${cardheader}${cardbody}</div></div>`;
        $('#institutionContainer').append(card);
    }
});

// create institution page

$.fn.fileinputBsVersion = "3.3.7";

$("#input-file").fileinput({
    browseLabel: "Pick Image",
    showUpload: false,
    allowedFileExtensions: ["jpg", "png", "gif"],
    previewFileType: 'any'
});