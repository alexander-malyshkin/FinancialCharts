var source = new EventSource("/sse-financial");
source.onopen = function () { console.log('-- CONNECTION ESTABLISHED --'); };
source.onerror = function () { console.log('-- CONNECTION FAILED --'); };
source.onmessage = handleDateSeries(e);

//var chartTitleHandle = document.getElementById("ChartTitle");

//function appendInnerHtml(el,data) {
//    el.appendChild(document.createTextNode(data));
//    el.appendChild(document.createElement('br'));
//};

//function setInnerHtml(el,data) {
//    el.innerHTML = data;
//};

function handleDateSeries(event) {

    var seriesInput = JSON.parse(event.data);

    var chartDivs = document.querySelector('div[role="chart"]');
    for (var i = 0; i < chartDivs.length; i++) {
        
        var chartId = chartDivs[i];
        

        fillChartWithData(chartId, seriesInput);
    }

    
    if (event.id === "CLOSE") {
        source.close();
    }
}

function fillChartWithData(chartId, seriesInput) {

    var chartDiv = document.querySelector('#' + chartId);
    var assetId = chartDiv.getAttribute("assetId");
    var dateId = chartDiv.getAttribute("dateId");



    // find asset name from select list
    var assetItems = document.querySelector('#AssetsMenu');
    var assetName = assetItems.querySelector('option[value="' + assetId + '"]').text;

    // find date string from checkboxes on the tab
    var tabId = getTabId(assetId);
    // var tab = document.querySelector('#' + tabId);
    var dateString = document.querySelector('input[dateid="' + dateId + '"][type="checkbox"]');

    var partialSeries = seriesInput.filter(function (d) {
        return d.id == dateId;
    });


    Highcharts.chart(chartId, {

        title: {
            text: assetName
        },

        subtitle: {
            text: dateString
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

        series: partialSeries,


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
}





