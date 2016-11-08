// Flot Charts sample data for SB Admin template


// Flot Pie Chart with Tooltips
$(document).ready(function () {

    var data = [{
        label: "Visa",
        data: 1
    }, {
        label: "MasterCard",
        data: 3
    }, {
        label: "DebitCard",
        data: 9
    }, {
        label: "American Express Card",
        data: 9
    },{
        label: "Other card",
        data: 20
    }];

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



