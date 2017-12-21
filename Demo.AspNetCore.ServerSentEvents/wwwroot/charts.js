
var chartTitleHandle = document.getElementById("ChartTitle");

function appendInnerHtml(el,data) {
    el.appendChild(document.createTextNode(data));
    el.appendChild(document.createElement('br'));
};

function setInnerHtml(el,data) {
    el.innerHTML = data;
};

var source = new EventSource("/sse-financial");
source.onopen = function () { chartTitleHandle.innerHTML = '-- CONNECTION ESTABLISHED --' };
source.onerror = function () { appendInnerHtml(chartRef, '-- CONNECTION FAILED --'); };

source.onmessage = function (event) {

    console.log('SSE EVENT: { id: "' + event.id + '", data: "' + event.data + '" }');

    var seriesInput = JSON.parse(event.data);


    
    Highcharts.chart('browserChart', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false//,
            //type: 'pie'
        },
        title: {
            text: 'Some distribution chart'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        /*
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                    }
                }
            }
        },*/
        series: seriesInput
    });

    





        
        //setInnerHtml(chartRef, event.data);


        if (event.id === "CLOSE") {
            source.close();
        }
    }