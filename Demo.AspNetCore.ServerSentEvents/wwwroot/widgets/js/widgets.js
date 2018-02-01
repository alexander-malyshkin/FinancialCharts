

function insertWidget(chartDiv, widgetTitle, dateCheckbox) {
    var undo = $("#undo"); // this bears no meaning that lets the things work

    var parentDivId = chartDiv.parentNode.getAttribute("id");
    var myWinId = chartDiv.getAttribute("id");
    var myWinFnInit = $('#' + myWinId);

    function onClose() {
        dateCheckbox.checked = false;
    }

    myWinFnInit.kendoWindow({
        appendTo: "div#" + parentDivId,
        width: "600px",
        //position: relative,
        title: widgetTitle,
        visible: false,
        actions: [
            //"Pin",
            "Minimize"
            //,"Maximize"
            //,"Close"
        ],
        close: onClose
    }).data("kendoWindow").open();

    
}