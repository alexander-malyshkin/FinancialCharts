
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

        title: {
            text: 'Volatility Smile'
        },

        subtitle: {
            text: ''
        },

        yAxis: {
            title: {
                text: 'VOL'
            }
        },
        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle'
        },
        
        plotOptions: {
            series: {
                label: {
                    connectorAllowed: false
                },
                pointStart: 100
            }
        },

        series: seriesInput,


        responsive: {
            rules: [{
                condition: {
                    maxWidth: 500
                },
                chartOptions: {
                    legend: {
                        layout: 'horizontal',
                        align: 'center',
                        verticalAlign: 'bottom'
                    }
                }
            }]
        }

    });

    





        
        //setInnerHtml(chartRef, event.data);


        if (event.id === "CLOSE") {
            source.close();
        }
    }