// When the document is ready
$(document).ready(function () {

    $('#example1').datepicker({
        format: "mm/yyyy",
        viewMode: "months",
        minViewMode: "months"
    });

    var nowTemp = new Date();
    var now = new Date(nowTemp.getFullYear(), nowTemp.getMonth(), nowTemp.getDate(), 0, 0, 0, 0);

    var startDay = $('#startDay').datepicker({
        format: "dd/mm/yyyy"
    }).on('changeDate', function (ev) {
        if (ev.date.valueOf() > endDay.date.valueOf()) {
            var newDate = new Date(ev.date)
            newDate.setDate(newDate.getDate() + 1);
            endDay.setValue(newDate);
        }
        startDay.hide();
        $('#endDay')[0].focus();
    }).data('datepicker');

    var endDay = $('#endDay').datepicker({
        format: "dd/mm/yyyy",
        onRender: function (date) {

        }
    }).on('changeDate', function (ev) {
        endDay.hide();
    }).data('datepicker');

    //Month
    var startMonth = $('#startMonth').datepicker({
        format: "mm/yyyy",
        viewMode: "months",
        minViewMode: "months",
        onRender: function (date) {
            return date.valueOf() < now.valueOf() ? 'disabled' : '';
        }
    }).on('changeDate', function (ev) {
        if (ev.date.valueOf() > endMonth.date.valueOf()) {
            var newDate = new Date(ev.date)
            newDate.setDate(newDate.getDate() + 1);
            endMonth.setValue(newDate);
        }
        startMonth.hide();
        $('#endMonth')[0].focus();
    }).data('datepicker');

    var endMonth = $('#endMonth').datepicker({
        format: "mm/yyyy",
        viewMode: "months",
        minViewMode: "months",
        onRender: function (date) {
            return date.valueOf() <= startMonth.date.valueOf() ? 'disabled' : '';
        }
    }).on('changeDate', function (ev) {
        endMonth.hide();
    }).data('datepicker');

    //Year
    var startYear = $('#startYear').datepicker({
        format: " yyyy",
        viewMode: "years",
        minViewMode: "years",
        onRender: function (date) {
            return date.valueOf() < now.valueOf() ? 'disabled' : '';
        }
    }).on('changeDate', function (ev) {
        if (ev.date.valueOf() > endYear.date.valueOf()) {
            var newDate = new Date(ev.date)
            newDate.setDate(newDate.getDate() + 1);
            endYear.setValue(newDate);
        }
        startYear.hide();
        $('#endYear')[0].focus();
    }).data('datepicker');

    var endYear = $('#endYear').datepicker({
        format: " yyyy",
        viewMode: "years",
        minViewMode: "years",
        onRender: function (date) {
            return date.valueOf() <= startYear.date.valueOf() ? 'disabled' : '';
        }
    }).on('changeDate', function (ev) {
        endYear.hide();
    }).data('datepicker');

    //Quarter
    var startQuarter = $('#startQuarterYear').datepicker({
        format: " yyyy",
        viewMode: "years",
        minViewMode: "years",
        onRender: function (date) {
            return date.valueOf() < now.valueOf() ? 'disabled' : '';
        }
    }).on('changeDate', function (ev) {
        if (ev.date.valueOf() > endYear.date.valueOf()) {
            var newDate = new Date(ev.date)
            newDate.setDate(newDate.getDate() + 1);
            endYear.setValue(newDate);
        }
        startQuarter.hide();
        $('#endQuarterYear')[0].focus();
    }).data('datepicker');

    var endQuarter = $('#endQuarterYear').datepicker({
        format: " yyyy",
        viewMode: "years",
        minViewMode: "years",
        onRender: function (date) {
            return date.valueOf() <= startYear.date.valueOf() ? 'disabled' : '';
        }
    }).on('changeDate', function (ev) {
        endQuarter.hide();
    }).data('datepicker');

});

//Config for quick report
$(document).ready(function () {
    var dateFormat = 'YYYYMMDD';
    $('#viewThisMonth').click(function () {
        $("#reportStartDate").attr("value", moment().startOf('month').format(dateFormat));
        $("#reportEndDate").attr("value", moment().startOf('day').format(dateFormat));
        $("#reportType").attr("value", "Day");
        $("#mainform").submit();
    });

    $('#viewThisYear').click(function () {
        $("#reportStartMonth").attr("value", '1');
        $("#reportEndMonth").attr("value", moment().startOf('month').format('MM'));
        $("#reportStartYear").attr("value", moment().startOf('year').format('YYYY'));
        $("#reportEndYear").attr("value", moment().startOf('year').format('YYYY'));
        $("#reportType").attr("value", "Month");
        $("#mainform").submit();
    });

    $('#viewThisQuarter').click(function () {
        $("#reportStartMonth").attr("value", moment().startOf('quarter').format('MM'));
        $("#reportEndMonth").attr("value", moment().startOf('month').format('MM'));
        $("#reportStartYear").attr("value", moment().startOf('year').format('YYYY'));
        $("#reportEndYear").attr("value", moment().startOf('year').format('YYYY'));
        $("#reportType").attr("value", "Month");
        $("#mainform").submit();
    });

    $('#viewDay').click(function () {
        $("#reportStartDate").attr("value", moment($('#startDay').val(), "DD/MM/YYYY").format(dateFormat));
        $("#reportEndDate").attr("value", moment($('#endDay').val(), "DD/MM/YYYY").format(dateFormat));
        $("#reportType").attr("value", "Day");
        $("#mainform").submit();
    });

    $('#viewMonth').click(function () {
        var startDateUserInput = moment("01/" + $('#startMonth').val(), "DD/MM/YYYY");
        var endDateUserInput = moment("01/" + $('#endMonth').val(), "DD/MM/YYYY");

        if ($('#startMonth').val() === $('#endMonth').val()) {
            $("#reportStartDate").attr("value", startDateUserInput.format(dateFormat));
            $("#reportEndDate").attr("value", endDateUserInput.endOf("month").format(dateFormat));
            $("#reportType").attr("value", "Day");
        } else {
            $("#reportStartMonth").attr("value", startDateUserInput.format('MM'));
            $("#reportEndMonth").attr("value", endDateUserInput.format('MM'));
            $("#reportStartYear").attr("value", startDateUserInput.format('YYYY'));
            $("#reportEndYear").attr("value", endDateUserInput.format('YYYY'));
            $("#reportType").attr("value", "Month");
        }

        $("#mainform").submit();
    });

    $('#viewQuarter').click(function () {
        var startQuarterRp = document.getElementById('startQuarter').value;
        var endQuarterRp = document.getElementById('endQuarter').value;
        var startYearRp = $('#startQuarterYear').val().trim();
        var endYearRp = $('#endQuarterYear').val().trim();

        if ((startQuarterRp === endQuarterRp) && (startYearRp === endYearRp)) { //report by month
            $("#reportStartMonth").attr("value", (startQuarterRp - 1) * 3 + 1);
            $("#reportEndMonth").attr("value", (endQuarterRp - 1) * 3 + 1);
            $("#reportStartYear").attr("value", startYearRp);
            $("#reportEndYear").attr("value", startYearRp);
            $("#reportType").attr("value", "Month");
            $("#mainform").submit();
        } else { //report by quarter
            $("#reportStartQuarter").attr("value", startQuarterRp);
            $("#reportEndQuarter").attr("value", endQuarterRp);
            $("#reportStartYear").attr("value", startYearRp);
            $("#reportEndYear").attr("value", endYearRp);
            $("#reportType").attr("value", "Quarter");
            $("#mainform").submit();
        }

        $("#startdate").attr("value", moment("01/" + $('#startQuarter').val(), "DD/MM/YYYY").format(dateFormat));
        $("#enddate").attr("value", moment("01/" + $('#endQuarter').val(), "DD/MM/YYYY").endOf("month").format(dateFormat));
        $("#mainform").submit();

        console.log(moment("01/" + $('#startMonth').val(), "DD/MM/YYYY").format(dateFormat));
        console.log(moment("01/" + $('#endMonth').val(), "DD/MM/YYYY").endOf("month").format(dateFormat));
    });

    $('#viewYear').click(function () {
        if ($('#startYear').val() === $('#endYear').val()) {
            $("#reportStartMonth").attr("value", '1');
            $("#reportEndMonth").attr("value", '12');

            $("#reportType").attr("value", "Month");
        } else {
            $("#reportType").attr("value", "Year");
        }
        $("#reportStartYear").attr("value", $('#startYear').val().replace(" ", ""));
        $("#reportEndYear").attr("value", $('#endYear').val().replace(" ", ""));

        $("#mainform").submit();
    });

});

$(function () {
    // Donut Chart
    Morris.Donut({
        element: 'morris-donut-chart',
        data: DonutData,
        resize: true
    });

    Morris.Bar({
        element: 'morris-bar-chart',
        data: BarData,
        xkey: 'region',
        ykeys: ['sale'],
        labels: ['Sale amount'],
        barRatio: 0.4,
        xLabelAngle: 35,
        hideHover: 'auto',
        resize: true
    });

    Morris.Line({
        // ID of the element in which to draw the chart.
        element: 'morris-line-chart',
        // Chart data records -- each entry in this array corresponds to a point on
        // the chart.
        data: LineData,
        // The name of the data record attribute that contains x-visitss.
        xkey: 'd',
        // A list of names of data record attributes that contain y-visitss.
        ykeys: ['sales'],
        // Labels for the ykeys -- will be displayed when you hover over the
        // chart.
        labels: ['Sale amount'],
        // Disables line smoothing
        smooth: false,
        resize: true,
        parseTime: false,
        lineColors: ['#F5ABB0'],
        pointFillColors: ['#FF3300'],
        hoverCallback: function (index, options, content) {
            var data = options.data[index];
            content = "<div class='morris-hover-row-label'>" + data.d + "</div><div class='morris-hover-point' style='color: #0b62a4'>Sale Amount: $" + data.sales + "<br>Return Amount: $" + -data.returns + "<br>Transactions: " + data.count + "</div>"
            return content
        },
    });

    var data = CardTypeData;

    var plotObj = $.plot($("#flot-pie-chart"), data, {
        series: {
            pie: {
                show: true
            }
        },
        grid: {
            hoverable: true
        },
        tooltip: true,
        tooltipOpts: {
            content: "%p.0%, %s", // show percentages, rounding to 2 decimal places
            shifts: {
                x: 20,
                y: 0
            },
            defaultTheme: false
        }
    });
});

function Display(idBtn, idForm, index) {
    // Get the modal
    var modal = document.getElementById(idForm);

    // Get the button that opens the modal
    //var btn = document.getElementById(idBtn);
    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[index];

    // When the user clicks the button, open the modal

    //btn.onclick = function () {
    modal.style.display = "block";
    //}

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
    }

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
}