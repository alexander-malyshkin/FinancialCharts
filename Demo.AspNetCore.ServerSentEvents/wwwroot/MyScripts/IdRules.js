function getTabId(assetId) {
    return 'tabs-' + assetId;
}

function getChartId(assetId, dateString) {
    var preparedDate = prepareDateString(dateString);
    return 'chart-' + assetId + '-' + preparedDate;
}

function getDateCheckboxId(dateString) {
    var preparedDate = prepareDateString(dateString);
    return 'chk-' + preparedDate;
}

function getChartBtnId(assetId, dateString) {
    var preparedDate = prepareDateString(dateString);
    return 'btn-' + assetId + '-' + preparedDate;
}

function getAssetTabChartsPanelId(assetId) {
    return 'chartsPanel-' + assetId;
}

function getChartWidgetId(assetId, dateString) {
    var preparedDate = prepareDateString(dateString);
    return 'chartWidget-' + assetId + '-' + preparedDate;
}

function getDatesPanelId(assetId) {
    return 'dates-' + assetId;
}

function prepareDateString(dateString) {
    dateString = dateString.replace('.', '');
    return dateString.replace('.', '');

    //return dateString;
}

function getDateCheckboxLabelId(dateCheckboxId) {
    return 'label-' + dateCheckboxId;
}