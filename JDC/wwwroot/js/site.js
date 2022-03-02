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



$('#createLessonModal').on('show.bs.modal', function (event) {
    $('#theme').val('');
    $('#homework').val('');
    $('#lessonDuration').val(1);
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

