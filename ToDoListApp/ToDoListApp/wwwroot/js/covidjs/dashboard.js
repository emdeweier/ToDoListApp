$(document).ready(function () {
    CovidIndonesia();
    CovidAllCountries();
});
debugger;
function CovidIndonesia() {
    $.ajax({
        type: 'GET',
        url: '/Covid/CovidIndonesia/',
        success: function (data) {
            debugger;
            Morris.Donut({
                element: 'covidid-chart',
                data: $.each(JSON.parse(data), function (index, val) {
                    [{
                        label: val.label,
                        value: val.value
                    }]
                }),
                resize: true,
                colors: ['#9CC4E4', '#3A89C9', '#F26C4F']
            });
        }
    });
}
debugger;
function CovidAllCountries() {
    $.ajax({
        type: 'GET',
        url: '/Covid/CovidAllCountries/',
        success: function (data) {
            debugger;
            Morris.Donut({
                element: 'covid-chart',
                data: $.each(JSON.parse(data), function (index, val) {
                    [{
                        label: val.label,
                        value: val.value
                    }]
                }),
                resize: true
            });
        }
    });
}