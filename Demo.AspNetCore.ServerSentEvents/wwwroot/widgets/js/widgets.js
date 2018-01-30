

function insertWidget(chartDiv, widgetTitle, dateCheckbox) {
    var undo = $("#undo"); // this bears no meaning that lets the things work

    var parentDivId = chartDiv.parentNode.getAttribute("id");
    var myWinId = chartDiv.getAttribute("id");
    var myWinFnInit = $('#' + myWinId);

    function onClose() {
        //undo.fadeIn();
    }

    myWinFnInit.kendoWindow({
        appendTo: "div#" + parentDivId,
        width: "600px",
        title: widgetTitle,
        visible: false,
        actions: [
            //"Pin",
            "Minimize",
            "Maximize",
            "Close"
        ],
        close: onClose
    }).data("kendoWindow").open();

    dateCheckbox.checked = true;
}