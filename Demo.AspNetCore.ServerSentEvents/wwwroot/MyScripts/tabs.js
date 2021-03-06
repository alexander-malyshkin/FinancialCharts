const datesPanelRatio = 10;
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
    var tabPanelContentHtml = constructTabPanelTable(assetId, optionsList);
    tabs.append("<div id='" + id + "'>" + tabPanelContentHtml + "</div>");
    tabs.tabs("refresh");
}

function constructTabPanelTable(assetId, optionsList) {
    var dateCheckboxesPanel = constructDatesPanel(assetId, optionsList);
    var chartsPanelWidthRatio = 100 - datesPanelRatio;
    var chartsPanelId = getAssetTabChartsPanelId(assetId);
    var tabPanelHtml = '<table style="width:100%"> ' +
        '<tr> ' +
        '<th VALIGN=TOP style="width:' + chartsPanelWidthRatio + '%"> <div id="' +
        chartsPanelId + '" class="col-xs-12">'
        
        + '</div> </th> ' +
        '<th VALIGN=TOP align=RIGHT style="width:' + datesPanelRatio + '%">' +
        dateCheckboxesPanel +
        '</th> ' +
        '</tr> ' +
        '</table>';
    return tabPanelHtml;
}

function constructDatesPanel(assetId, optionsList) {
    var tabPanelHtml = '<fieldset>'
        + '<legend>Expiration Date: </legend>';
    [].forEach.call(optionsList, function (singleOption) {
        var chkId = getDateCheckboxId(singleOption.DateString);
        var labelId = getDateCheckboxLabelId(chkId);
        if (parseInt(singleOption.BaseAssetId) == assetId
            && !(tabPanelHtml.includes(singleOption.DateString)) ) {
            tabPanelHtml += '<label id="' + labelId + '" for="' + chkId + '" class="ui-checkboxradio-label ui-corner-all ui-button ui-widget">'
                + singleOption.DateString
                + '<input type="checkbox" id="' + chkId
                + '" class="ui-checkboxradio ui-helper-hidden-accessible"'
                + ' assetid="' + assetId
                //+ '" dateid="' + singleOption.Id
                + '" datestring="' + singleOption.DateString
                + '" onchange="toggleChart(this, \'' + singleOption.DateString
                + '\',' + assetId + ')">'
                +'</label><br/>';
        }

    });
    tabPanelHtml += '</fieldset>';
    var datesPanelId = getDatesPanelId(assetId);
    return '<div id="' + datesPanelId + '" class="dates-panel">' + tabPanelHtml + '</div>';
}

function toggleChart(checkBox, dateString, assetId) {
    var dateCheckboxId = checkBox.getAttribute("id");
    updateDateCheckboxStyle(dateCheckboxId);

    var chartId = getChartId(assetId, dateString);
    var chart = document.getElementById(chartId);
    if (checkBox.checked == true && chart != null) {
        var chartWidget = chart.parentNode;
        chartWidget.style.display = "block";
        chartWidget.style.transform = "scale(1)";
        chart.style.visibility = 'visible';
    }
    else if (chart == null) {
        createChart(dateString, assetId);
    }
    else if (chart != null && checkBox.checked == false){
        chart.style.visibility = 'hidden';
        var chartWidget = chart.parentNode;
        chartWidget.style.display = "none";
        chartWidget.style.transform = "scale(0.7)";
    }
}

function collapseExpandChart(chartId)
{
    var chartDiv = document.getElementById(chartId);

    if (chartDiv.classList.contains('in')) {
        chartDiv.classList.remove('in');
        chartDiv.setAttribute("aria-expanded", "false");
    } else {
        chartDiv.classList.add('in');
        chartDiv.setAttribute("aria-expanded", "true");
    }
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
        var tab = tabPanel.querySelector('li[aria-controls="'
            + tabId + '"]');
    }

    // put focus on tab corresponding to selected asset
    //PutFocusOnTab(tabId, tab, tabPanel);
    tab.querySelector('a').click();
}

function PutFocusOnTab(tabId, tab, tabPanel) {
    // remove focus from the rest of the tabs
    var otherTabs = tabPanel.querySelectorAll('li:not([aria-controls="' + tabId + '"])');
    for (var i = 0; i < otherTabs.length; i++) {
        var otherTab = otherTabs[i];
        RemoveFocusFromTab(otherTab, tabPanel);
    };

    // put focus to the selected tab
    tab.setAttribute("aria-selected", "true");
    tab.setAttribute("aria-expanded", "true");
    tab.setAttribute("aria-hidden", "false");
    tab.setAttribute("tabindex", "0");
    tab.classList.add("ui-tabs-active");
    tab.classList.add("ui-state-active");
    tab.classList.add("ui-state-focus");

    // in the panel there is a separate div with id equal to tabId
    var tabBlock = document.getElementById(tabId);
    tabBlock.setAttribute("style", "display: block");
    tabBlock.setAttribute("aria-hidden", "false");

    
    
}

function RemoveFocusFromTab(tab, tabPanel) {
    tab.setAttribute("aria-selected", "false");
    tab.setAttribute("aria-expanded", "false");
    tab.setAttribute("aria-hidden", "true");
    tab.setAttribute("tabindex", "-1");
    tab.classList.remove("ui-tabs-active");
    tab.classList.remove("ui-state-active");
    tab.classList.remove("ui-state-focus");

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

function updateDateCheckboxStyle(dateCheckboxId) {
    var dateLabelId = getDateCheckboxLabelId(dateCheckboxId);
    var label = document.getElementById(dateLabelId);
    var checkbox = document.getElementById(dateCheckboxId);

    if (checkbox.checked) {
        label.classList.add("ui-checkboxradio-checked");
        label.classList.add("ui-state-active");
    } else {
        label.classList.remove("ui-checkboxradio-checked");
        label.classList.remove("ui-state-active");
    }
};