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

        //if (chartDiv.style.visibility == "visible") {
            fillChartWithData(chartDiv, seriesInput);
        //}
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

function getPartialData(assetId, dateString, seriesInput) {
    var partialSeries = [];
    for (var i = 0; i < seriesInput.length; i++) {
        var seriesItem = seriesInput[i];
        //var seriesItemToPush = {};
        var strikeVolatData = [];

        if (seriesItem.Option.DateString == dateString && seriesItem.Option.BaseAssetId == parseInt(assetId) ) {
            strikeVolatData = [seriesItem.Option.Strike, seriesItem.Volatility];
            //seriesItemToPush = { name: seriesItem.Name, data: strikeVolatData };
            partialSeries.push(strikeVolatData);
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
    var dateString = chartDiv.getAttribute("date");
    var assetsMenu = document.getElementById("AssetsMenu");
    var assetName = assetsMenu.querySelector('option[value="' + assetId + '"]').text;

    // find asset name from select list
    //var assetItems = document.querySelector('#AssetsMenu');
    //var assetName = assetItems.querySelector('option[value="' + assetId + '"]').text;

    // find date string from checkboxes on the tab
    //var tabId = getTabId(assetId);
    // var tab = document.querySelector('#' + tabId);
    //var dateCheckbox = document.querySelector('input[dateid="' + dateString + '"][type="checkbox"]');
    //var dateString = dateCheckbox.getAttribute("datestring");

    var partialData = getPartialData(assetId, dateString, seriesInput);

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

        //series: partialData,
        series: [
            {
                name: assetName,
                data: partialData
            }
        ],

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





