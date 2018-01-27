webix.ready(function () {
    // Main Demo
    webix.ui({
        view: "window",
        height: 250,
        width: 350,
        left: 10, top: 10,
        head: {
            view: "toolbar", cols: [
                { view: "label", label: "This window can be closed" },
                {
                    view: "button", label: 'Close Me', width: 100, align: 'right', click: function () {
                        this.getTopParentView().close();
                    }
                }
            ]
        },
        body: {
            template: "Some text"
        },
        container: "window-demo"
    }).show();

    webix.ui({
        view: "window",
        height: 250,
        width: 300,
        left: 390, top: 10,
        resize: true,
        head: "This window can be resized",
        body: {
            template: "Some text"
        },
        container: "window-demo"
    }).show();

    webix.ui({
        view: "window",
        height: 230,
        width: 300,
        left: 220, top: 210,
        head: false,
        body: {
            template: "Plain window without head"
        },
        container: "window-demo"
    }).show();


    //modal(less) state
    var form = {
        view: "form",
        elements: [
            { view: "text", label: 'Login', name: "login" },
            { view: "text", label: 'Email', name: "email" },
            {
                view: "button", value: "Submit", click: function () {
                    this.getTopParentView().hide();
                }
            }
        ],
        elementsConfig: {
            labelPosition: "top",
        }
    };

    function showForm(node, modal) {
        if (!$$("win1"))
            webix.ui({
                view: "window",
                id: "win1",
                head: "Fill in the form",
                width: 350,
                modal: modal,
                body: webix.copy(form)
            });
        $$("win1").config.modal = modal;
        $$("win1").getBody().clear();
        $$("win1").show(node, { pos: "top" });
        $$("win1").getBody().focus();
    }
    webix.ui({
        container: "windowModal",
        align: "left",
        body: {
            height: 40,
            margin: 10,
            cols: [
                {
                    view: "button",
                    width: 300, value: 'Click to show a standard window',
                    click: function () { showForm(this.$view); }
                },
                {
                    view: "button",
                    width: 300, value: 'Click to show a modal window',
                    click: function () { showForm(this.$view, true); }
                }
            ]
        }
    });


    // Window Size and Position
    webix.ui({
        view: "window",
        height: 250,
        width: 300,
        top: 127, left: 163,
        head: "This window is centered",
        //position:"center",
        body: {
            template: "Some text"
        },
        container: "windowSize"
    }).show();


    // Window Resize
    webix.ui({
        view: "window",
        height: 250,
        width: 300,
        top: 80, left: 163,
        resize: true,
        head: "This window can be resized",
        body: {
            template: "Some text"
        },
        container: "windowResize"
    }).show();


    // Head and Body
    webix.ui({
        view: "window",
        head: "DataTable",
        left: 0, top: 0,
        body: {
            view: "datatable",
            columns: [
                { id: "rank", header: "", css: "rank", width: 50, sort: "int" },
                { id: "title", header: "Film title", width: 200, sort: "string" },
                { id: "year", header: "Released", width: 80, sort: "int" },
                { id: "votes", header: "Votes", width: 100, sort: "int" }
            ],
            select: "row",
            autoheight: true,
            autowidth: true,
            data: grid_data
        },
        container: "windowHeadBody"
    }).show();


});