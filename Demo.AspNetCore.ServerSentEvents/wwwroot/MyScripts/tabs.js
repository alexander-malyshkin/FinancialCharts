var //tabTitle = $("#tab_title"),
    //tabContent = $("#tab_content"),
    tabTemplate = "<li><a href='#{href}'>#{label}</a> <span class='ui-icon ui-icon-close' role='presentation'>Remove Tab</span></li>"
    //,tabCounter = 2
    ;

var tabs = $("#tabs").tabs();

// Modal dialog init: custom buttons and a "close" callback resetting the form inside
var dialog = $("#dialog").dialog({
    autoOpen: false,
    modal: true,
    buttons: {
        Add: function () {
            addTab();
            $(this).dialog("close");
        },
        Cancel: function () {
            $(this).dialog("close");
        }
    },
    close: function () {
        form[0].reset();
    }
});

// AddTab form: calls addTab function on submit and closes the dialog
var form = dialog.find("form").on("submit", function (event) {
    addTab();
    dialog.dialog("close");
    event.preventDefault();
});

// Actual addTab function: adds new tab using the input from the form above
function addTab(assetId, assetName, optionsList) {
    var label = assetName,
        //id = "tabs-" + assetId,
        id = getTabId(assetId);
        li = $(tabTemplate.replace(/#\{href\}/g, "#" + id).replace(/#\{label\}/g, label));

    tabs.find(".ui-tabs-nav").append(li);
    var tabPanelContentHtml = constructTabPanel(assetId, optionsList);
    tabs.append("<div id='" + id + "'>" + tabPanelContentHtml + "</div>");
    tabs.tabs("refresh");
    //tabCounter++;
}

function constructTabPanel(assetId, optionsList) {
    var tabPanelHtml = '';
    [].forEach.call(optionsList, function (singleOption) {
        var chkId = getDateCheckboxId(singleOption.Id);
        if (parseInt(singleOption.BaseAssetId) == assetId
            && tabPanelHtml.contains('id="' + chkId + '"')) {
            tabPanelHtml += '  <input type="checkbox" id="' + chkId
                + '" assetid="' + assetId
                //+ '" dateid="' + singleOption.Id
                + '" datestring="' + singleOption.DateString
                + '" onchange="toggleChart(this, ' + singleOption.Id + ',' + assetId + ')"> ' + singleOption.DateString + '  </input>';
        }

    });
    return tabPanelHtml;
}

function toggleChart(checkBox, optionId, assetId) {
    var chartId = getChartId(assetId, optionId);
    var chart = document.querySelector('#' + chartId);
    if (checkBox.checked == true && chart != null) {
            chart.style.visibility = 'visible';
    }
    else if (chart != null){
            chart.style.visibility = 'hidden';
    }
}

function createChart(optionId, assetId) {
    var chartId = getChartId(assetId, optionId);
    var tabId = getTabId(assetId);
    var tabPanel = document.querySelector('#' + tabId);
    //var chartDivHtml = '<div id="' + chartId + '">' + '</div>';
    var chartDiv = document.createElement("div");
    chartDiv.setAttribute("id", chartId);
    chartDiv.setAttribute("role", "chart");
    chartDiv.setAttribute("assetId", assetId);
    //chartDiv.setAttribute("dateId", optionId);
    tabPanel.appendChild(chartDiv);
    //return chartId;
}

function createCharts(assetId, expDatesList) {
    [].forEach.call(expDatesList,
        function (singleDate) {
            if (parseInt(singleDate.AssetId) == assetId) {
                var dateId = singleDate.Id;
                createChart(dateId, assetId);
            }
        }
    );
}

function OnAssetSelected(optionsList) {


    var assetsMenu = document.getElementById("AssetsMenu");
    var assetId = assetsMenu.options[assetsMenu.selectedIndex].value;
    var assetName = assetsMenu.options[assetsMenu.selectedIndex].text;

    // check if this tab is already open
    var tabPanel = document.getElementById("tabs");
    var tabId = getTabId(assetId);
    var tab = tabPanel.querySelector('li[aria-controls="'
        + tabId + '"]');
    if (tab == null) {
        addTab(assetId, assetName, optionsList);
        createCharts(assetId, optionsList);
    }

    // put focus on tab corresponding to selected asset
    //PutFocusOnTab(tabId, tab, tabPanel);
}

//function PutFocusOnTab(tabId, tab, tabPanel) {
//    tab.setAttribute("aria-selected", "true");
//    tab.setAttribute("aria-expanded", "true");
//    tab.setAttribute("aria-hidden", "false");
//    tab.classList.add("ui-tabs-active");
//    tab.classList.add("ui-state-active");

//    // in the panel there is a separate div with id equal to tabId
//    var tabBlock = document.getElementById(tabId);
//    tabBlock.setAttribute("style", "display: block");
//    tabBlock.setAttribute("aria-hidden", "false");

//    var otherTabs = tabPanel.querySelectorAll('li:not([aria-controls="' + tabId + '"])');
//    [].forEach.call(otherTabs, function(otherTab) {
//        RemoveFocusFromTab(otherTab, tabPanel);
//    });
    
//}

function RemoveFocusFromTab(tab, tabPanel) {
    tab.setAttribute("aria-selected", "false");
    tab.setAttribute("aria-expanded", "false");
    tab.setAttribute("aria-hidden", "true");
    tab.classList.remove("ui-tabs-active");
    tab.classList.remove("ui-state-active");

    var tabId = tab.getAttribute("aria-controls");
    var tabBlock = document.getElementById(tabId);
    tabBlock.setAttribute("style", "display: none");
    tabBlock.setAttribute("aria-hidden", "true");
}

// AddTab button: just opens the dialog
$("#add_tab")
    .button()
    .on("click", function () {
        dialog.dialog("open");
    });

// Close icon: removing the tab on click
tabs.on("click", "span.ui-icon-close", function () {
    var panelId = $(this).closest("li").remove().attr("aria-controls");
    $("#" + panelId).remove();
    tabs.tabs("refresh");
});

tabs.on("keyup", function (event) {
    if (event.altKey && event.keyCode == $.ui.keyCode.BACKSPACE) {
        var panelId = tabs.find(".ui-tabs-active").remove().attr("aria-controls");
        $("#" + panelId).remove();
        tabs.tabs("refresh");
    }
});