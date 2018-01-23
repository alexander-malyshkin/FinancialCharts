function getTabId(assetId) {
    return 'tabs-' + assetId;
}

function getChartId(assetId, dateString) {
    return 'chart-' + assetId + '-' + dateString;
}

function getDateCheckboxId(dateString) {
    return 'chk-' + dateString;
}