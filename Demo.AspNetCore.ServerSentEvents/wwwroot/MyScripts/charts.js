var source = new EventSource("/sse-financial");
source.onopen = function () { console.log('-- CONNECTION ESTABLISHED --'); };
source.onerror = function () { console.log('-- CONNECTION FAILED --'); };
source.onmessage = function(event) {
    var time = new Date();
    console.log(time.getHours() + ":" + time.getMinutes() + ":" + time.getSeconds());
    var seriesInput = JSON.parse(event.data);

    var chartDivs = document.querySelectorAll('div[role="chart"]');
    for (var i = 0; i < chartDivs.length; i++) {

        var chartDiv = chartDivs[i];

        if (chartDiv.style.visibility == "visible") {
            fillChartWithData(chartDiv, seriesInput);
        }
    }


    if (event.id === "CLOSE") {
        source.close();
    }
};

//var chartTitleHandle = document.getElementById("ChartTitle");

//function appendInnerHtml(el,data) {
//    el.appendChild(document.createTextNode(data));
//    el.appendChild(document.createElement('br'));
//};

//function setInnerHtml(el,data) {
//    el.innerHTML = data;
//};

function getPartialSeries(assetId, dateId, seriesInput) {
    var partialSeries = [];
    for (var i = 0; i < seriesInput.length; i++) {
        var seriesItem = seriesInput[i];
        var seriesItemToPush = {};
        var strikeVolatData = [];

        if (seriesItem.ExpirationDateId == dateId && seriesItem.AssetId == assetId) {
            for (var j = 0; j < seriesItem.Volatility.length; j++) {
                var strikeVolatItem = [seriesItem.Strike[j], seriesItem.Volatility[j] ];
                strikeVolatData.push(strikeVolatItem);
            }
            seriesItemToPush = { name: seriesItem.Name, data: strikeVolatData };
            partialSeries.push(seriesItemToPush);
        }
    }
    return partialSeries;
}

function formatTime(dateTime) {
    var hoursNum = dateTime.getHours();
    var minNum = dateTime.getMinutes();
    var secNum = dateTime.getSeconds();
    var hours = hoursNum < 10 ? '0' + hoursNum : hoursNum;
    var mins = minNum < 10 ? '0' + minNum : minNum;
    var sec = secNum < 10 ? '0' + secNum : secNum;
    return hours + ':' + mins + ':' + sec;
}

function fillChartWithData(chartDiv, seriesInput) {

    //var chartDiv = document.querySelector('#' + chartId);
    var assetId = chartDiv.getAttribute("assetId");
    var dateId = chartDiv.getAttribute("dateId");



    // find asset name from select list
    var assetItems = document.querySelector('#AssetsMenu');
    var assetName = assetItems.querySelector('option[value="' + assetId + '"]').text;

    // find date string from checkboxes on the tab
    var tabId = getTabId(assetId);
    // var tab = document.querySelector('#' + tabId);
    var dateCheckbox = document.querySelector('input[dateid="' + dateId + '"][type="checkbox"]');
    var dateString = dateCheckbox.getAttribute("datestring");

    var partialSeries = getPartialSeries(assetId, dateId, seriesInput);

    var chartId = chartDiv.getAttribute("id");
    Highcharts.chart(chartId, {

        title: {
            text: 'Expiration date ' + dateString
        },

        subtitle: {
            text: 'Last updated at ' + formatTime(new Date())
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
                }
                //,pointStart: 2010
            }
        },

        series: partialSeries,
        //series: [
        //    {
        //        name: "Sberbank Dec 3 - 1",
        //        data: [8, 7, 6, 7]
        //    },
        //    {
        //        name: "Sberbank Dec 3 - 2",
        //        data: [7, 6, 4, 5]
        //    }
        //],

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





