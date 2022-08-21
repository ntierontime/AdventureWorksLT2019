// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*
 * 1. data-nt- 
 * 1.1. data-nt- in this modal
 * data-nt-action
 * data-nt-view // from $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-option-field").val();
 * data-nt-container // from $(button)
 * data-nt-template // from $(button)
 * data-nt-postbackurl // calculated
 *
 * 1.2. data-nt- in $(wrapper) .nt-list-wrapper
 * data-nt-loaditem-url
 * data-nt-createitem-url
 * data-nt-updateitem-url
 * data-nt-deleteitem-url
 * 
 * 1.3. data-nt- in .nt-listitem
 * data-nt-route-id
 * 
 * 1.4. data-nt- in trigger button .nt-btn-action-save or .nt-btn-action-delete
 * data-nt-action
 * data-nt-container
 * data-nt-template
 * 
 * 2. css classes
 * 2.1. only in this modal
 * .nt-status
 * .nt-modal-body // not in inline-editing
 * .btn-nt-pagination-previous // not in inline-editing
 * .btn-nt-pagination-next // not in inline-editing
 * .btn-nt-action
 * .btn-group-nt-action-pagination // not in inline-editing
 * .btn-group-nt-action-createanotherone // not in inline-editing
 * .btn-group-nt-action-save 
 * .btn-group-nt-action-delete // not in inline-editing??
 * .btn-group-nt-action-details
 * .nt-created, with .border-warning .border-3
 * .nt-updated, with .border-success .border-3
 * .nt-deleted, with .border-danger .border-3 // not in inline-editing
 * .nt-createnew-button-container // contains the create-new <button/>
 *  
 * 2.2. consumed css classes from where this modal is launched.
 * .nt-list-wrapper, shared with other .js files
 * .nt-list-container-submit, shared with other .js files
 * .nt-listitem
 * .nt-current // with on .nt-list-wrapper and .nt-list-container-submit, .nt-listitem(with .border-info .border-5)
 * .nt-paged-view-option-field: // read value for data-nt-view in this modal
 * 
 * 2.3. in ajax response html
 * .nt-hidden-modal-title, server side render modal title to a hidden field, will set to .modal-title // not in inline-editing
 */

$(document).ready(function () {
    attachInlineEditingLaunchButtonClickEvent(".btn-nt-inline-editing"); // all
})

function attachInlineEditingLaunchButtonClickEvent(selector) {
    $(selector).click(function (e) {
        let button = e.currentTarget;
        const currentListItem = $(button).closest(".nt-listitem");
        const wrapper = $(button).closest(".nt-list-wrapper");
        const view = $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-option-field").val();
        const container = $(button).data("nt-container");
        const template = $(button).data("nt-template");
        const action = $(button).data("nt-action");

        let routeid = "";
        let index = ""; // for Editable List/Create
        if (action == "POST") { // Create
            routeid = $(button).data("nt-route-id");
            if ($(wrapper).find(".nt-list-count").length > 0) {
                index = $(wrapper).find(".nt-list-count").val();
            }
        }
        else {
            routeid = $(button).closest(".nt-listitem").data("nt-route-id");
        }
        const loadItemUrl = $(wrapper).data("nt-loaditem-url");

        initializeInlineEditing(button, action)
        // 3. Ajax to get htmls
        ajaxLoadItemInlineEditing(loadItemUrl + "/" + routeid, currentListItem, view, container, template, action, index);
    });
}

function attachInlineEditingCancelButtonClickEvent() {
    $(".nt-listitem.nt-current .btn-nt-action-cancel").click(function (e) {
        const self = this;
        $(this).attr("disabled", true);
        let button = e.currentTarget;
        const currentListItem = $(button).closest(".nt-listitem");
        const wrapper = $(button).closest(".nt-list-wrapper");
        const view = $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-option-field").val();
        const container = "Inline";
        const template = "Details";
        const action = $(button).data("nt-action");

        let routeid = "";
        let index = "";
        if (action == "POST") { // Create
            routeid = $(button).data("nt-route-id");
            if ($(wrapper).find(".nt-list-count").length > 0) {
                index = $(wrapper).find(".nt-list-count").val();
            }
        }
        else {
            routeid = $(button).closest(".nt-listitem").data("nt-route-id");
        }

        const loadItemUrl = $(wrapper).data("nt-loaditem-url");

        ajaxLoadItemInlineEditing(loadItemUrl + "/" + routeid, currentListItem, view, container, template, action, index);
    });
}

function attachInlineEditingActionButtonClickEvent_InTable() {
    $(".nt-listitem.nt-current .btn-nt-action").click(function (e) {
        const self = this;
        $(this).attr("disabled", true);
        let button = event.currentTarget;
        const currentListItem = $(button).closest(".nt-listitem");
        const wrapper = $(button).closest(".nt-list-wrapper");
        const view = $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-option-field").val();
        const container = $(button).data("nt-container");
        const template = $(button).data("nt-template");
        const action = $(button).data("nt-action");
        // Not <form>...</form>, create FormData
        const form = $(this).closest("form");
        let formData = [];
        if (!!form) {
            formData = new FormData($(form)[0]);
        }
        //let formData = new FormData();
        //$(".nt-listitem.nt-current input.form-control").each((index, element) => {
        //    formData.append($(element).attr("name"), $(element).val());
        //});
        //$(".nt-listitem.nt-current input.form-check-input").each((index, element) => {
        //    formData.append($(element).attr("name"), $(element).is(":checked"));
        //});
        //$(".nt-listitem.nt-current select.form-control").each((index, element) => {
        //    formData.append($(element).attr("name"), $(element).find(":selected").val());
        //});
        formData.append("view", view);
        formData.append("container", container);
        formData.append("template", template);
        form.validate();
        let postbackurl = "";
        let routeid = ""
        if (action == "PUT") { // Edit
            routeid = $(button).closest(".nt-listitem").data("nt-route-id");
            postbackurl = $(wrapper).data("nt-updateitem-url") + "/" + routeid;
        }
        else if (action == "POST") { // Create
            postbackurl = $(wrapper).data("nt-createitem-url");
        }
        else if (action == "DELETE") {
            routeid = $(button).closest(".nt-listitem").data("nt-route-id");
            postbackurl = $(wrapper).data("nt-deleteitem-url") + "/" + routeid;
        }
        else {
            routeid = $(button).closest(".nt-listitem").data("nt-route-id");
        }
        const loadItemUrl = $(wrapper).data("nt-loaditem-url") + "/" + routeid ?? "";

        ajaxPostbackInlineEditing_InTable(postbackurl, formData, self, view, loadItemUrl, container, template);
    });
}

function ajaxPostbackInlineEditing_InTable(postbackurl, formData, self, view, loadItemUrl, container, template) {
    $.ajax({
        type: "POST",
        url: postbackurl,
        data: formData,
        async: false,
        processData: false,
        contentType: false,
        dataType: "html",
        success: function (response) {
            // console.log(response);
            const splitResponse = response.split("===---------===");
            // response part #1, status html / how to display status message?
            if (splitResponse.length > 0) {
                $(self).popover({
                    container: 'body',
                    html: true,
                    placement: "right",
                    // TODO: how to change popover width?
                    template: '<div class="popover" style="min-width:600px;max-width:600px;width:600px" role="tooltip"><div class="popover-arrow"></div><h3 class="popover-header"></h3><div class="popover-body" style="min-width:600px;max-width:600px;width:600px;"></div></div>',
                    content: splitResponse[0]
                });
                $(self).popover('show');
                // response part #1.1 TODO: should have a timer to auto hide after a few seconds.
                const action = $(self).data("nt-action");
                const actionSuccess = !!($(".popover .popover-body .text-success").length); // not exists when length=0
                if (action === "DELETE") {
                    if (actionSuccess) {
                        // Mark as .nt-deleted .border-danger .border-5. will be deleted after timeout
                        $(".nt-listitem.nt-current").addClass("nt-deleted border-danger border-3");
                    }
                }
                setTimeout(() => {
                    // response part #2, to update the item which is updated/created.
                    $(self).popover('hide');
                    if (actionSuccess) {
                        if (action === "PUT") { // EDIT
                            // When EDIT, insert a new tr after ".nt-listitem.nt-current", then remove it after 3 seconds.
                            // Mark as .nt-updated .border-success .border-4. will be deleted when Dialog/Modal closed
                            $(".nt-listitem.nt-current").removeClass("border-info border-5");
                            $(".nt-listitem.nt-current").addClass("nt-updated border-success border-4");
                            if (splitResponse.length > 1) {
                                $(".nt-listitem.nt-current").html(splitResponse[1]);
                                attachInlineEditingLaunchButtonClickEvent(".nt-listitem.nt-current .btn-nt-inline-editing");
                                attachIndividualSelectCheckboxClickEventHandler($(".nt-listitem.nt-current .nt-list-bulk-select .form-check-input"));
                            }
                            $(self).removeAttr("disabled");
                        }
                        else if (action === "POST") { // Create
                            // .nt-created .border-warning .border-4 added in the response
                            if (splitResponse.length > 1) {
                                const toAppend = $(splitResponse[1]);
                                let theTbody = $(self).closest("table").find("tbody");
                                theTbody.append($(toAppend)); // new item at the last place
                                $(toAppend).get(0).scrollIntoView();
                                attachInlineEditingLaunchButtonClickEvent($(toAppend).find(".btn-nt-inline-editing"));
                                attachIndividualSelectCheckboxClickEventHandler($(toAppend).find(".nt-list-bulk-select .form-check-input"));
                            }
                            const createNewButton = $(self).closest(".nt-listitem").find(".nt-createnew-button-container");
                            const listItem = $(self).closest(".nt-listitem");
                            const createNewFormControls = $(listItem).children(":not(.nt-createnew-button-container)");
                            createNewFormControls.remove();
                            $(createNewButton).show();
                        }
                        else if (action === "DELETE") {
                            $(".nt-listitem.nt-deleted").remove();
                        }
                    }
                    else {
                        $(self).removeAttr("disabled");
                    }
                }, 1500);
            }
        },
        failure: function (response) {
            // console.log(response);
            $(self).removeAttr("disabled");
        },
        error: function (response) {
            // console.log(response);
            $(self).removeAttr("disabled");
        }
    });
}

function attachInlineEditingActionButtonClickEvent_InTiles() {
    $(".nt-listitem.nt-current .btn-nt-action").click(function (e) {
        const self = this;
        $(this).attr("disabled", true);
        let button = event.currentTarget;
        const currentListItem = $(button).closest(".nt-listitem");
        const wrapper = $(button).closest(".nt-list-wrapper");
        const view = $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-option-field").val();
        const container = $(button).data("nt-container");
        const template = $(button).data("nt-template");
        const action = $(button).data("nt-action");
        // Get FormData if there are in this dialog
        const form = $(".nt-listitem.nt-current form")
        let formData = [];
        if (!!form) {
            formData = new FormData($(form)[0]);
        }
        formData.append("view", view);
        formData.append("container", container);
        formData.append("template", template);
        form.validate();
        let postbackurl = "";
        let routeid = ""
        if (action == "PUT") { // Edit
            routeid = $(button).closest(".nt-listitem").data("nt-route-id");
            postbackurl = $(wrapper).data("nt-updateitem-url") + "/" + routeid;
        }
        else if (action == "POST") { // Create
            routeid = $(button).data("nt-route-id");
            postbackurl = $(wrapper).data("nt-createitem-url");
        }
        else if (action == "DELETE") {
            routeid = $(button).closest(".nt-listitem").data("nt-route-id");
            postbackurl = $(wrapper).data("nt-deleteitem-url") + "/" + routeid;
        }
        else {
            routeid = $(button).closest(".nt-listitem").data("nt-route-id");
        }
        const loadItemUrl = $(wrapper).data("nt-loaditem-url") + "/" + routeid ?? "";

        ajaxPostbackInlineEditing_InTiles(postbackurl, formData, self, view, loadItemUrl, container, template);
    });
}

function ajaxLoadItemInlineEditing(loadItemUrl, currentListItem, view, container, template, action, index) {
    $.ajax({
        type: "GET",
        url: loadItemUrl,
        data: { view, container, template, index },
        async: false,
        contentType: "application/json",
        success: function (response) {
            // 1. add response html to .nt-listitem.nt-current
            // 2.1. when List POST/Create, PUT/Edit, no <form>...</form>, because html DOM doesn't allow <form>...</form> around <td/> or <tr/>
            // 2.2. when Tiles POST/Create, PUT/Edit, <form>...</form> is around .card-body and .card-footer
            // 2.3. when Lists/Tiles DELETE/Delete, <form>...</form> is round the button with hidden input which contains value of identifier query property.
            const responseHtml = $(response);
            if (action == "POST") { 
                if (view === "EditableTable") {
                    // Create, if "EditableTable", append to <tbody>
                    $(currentListItem).closest("table").find("tbody").append(responseHtml);
                    attachMultiItemsDeleteCheckboxEvent(responseHtml);
                    increateListCountBy1(currentListItem);
                    enableSaveButton($(currentListItem).closest(".nt-list-wrapper"), true);
                }
                else {
                    // Create, if not "EditableTable", keep the current, .nt-createnew-button-container, <td> when List, which contains the Create <button>
                    $(currentListItem).find(".nt-createnew-button-container").hide();
                    currentListItem.prepend(responseHtml);
                }
            }
            else if (action == "PUT") {
                currentListItem.html(responseHtml);
            }
            else if (action == "DELETE") { // DELETE, <form>...</form> wrapped around .nt-btn-action-delete
                currentListItem.html(responseHtml);
            }
            else { // GET
                currentListItem.html(responseHtml);
            }

            if (action != "GET") {
                if (view === "List") {// In HtmlTable
                    attachInlineEditingActionButtonClickEvent_InTable();
                }
                else { // In Tiles
                    attachInlineEditingActionButtonClickEvent_InTiles();
                }
                attachInlineEditingCancelButtonClickEvent();
            }

            const selectorOfInlineEditingButtons = $(responseHtml).find(".btn-nt-inline-editing");
            $(selectorOfInlineEditingButtons).off();
            attachInlineEditingLaunchButtonClickEvent(selectorOfInlineEditingButtons); // all in currentListItem
            const selectorOfIndividualSelectCheckBoxes = $(responseHtml).find(".nt-list-bulk-select .form-check-input");
            $(selectorOfIndividualSelectCheckBoxes).off();
            attachIndividualSelectCheckboxClickEventHandler(selectorOfIndividualSelectCheckBoxes);
            // console.log(response);
        },
        failure: function (response) {
            // console.log(response);
        },
        error: function (response) {
        }
    });
}

function ajaxPostbackInlineEditing_InTiles(postbackurl, formData, self, view, loadItemUrl, container, template) {
    $.ajax({
        type: "POST",
        url: postbackurl,
        data: formData,
        async: false,
        processData: false,
        contentType: false,
        dataType: "html",
        success: function (response) {
            // console.log(response);
            const splitResponse = response.split("===---------===");
            // response part #1, status html / how to display status message?
            if (splitResponse.length > 0) {
                $(self).popover({
                    container: 'body',
                    html: true,
                    placement: "right",
                    // TODO: how to change popover width?
                    template: '<div class="popover" style="min-width:600px;max-width:600px;width:600px" role="tooltip"><div class="popover-arrow"></div><h3 class="popover-header"></h3><div class="popover-body" style="min-width:600px;max-width:600px;width:600px;"></div></div>',
                    content: splitResponse[0]
                });
                $(self).popover('show');
                // response part #1.1 TODO: should have a timer to auto hide after a few seconds.
                const action = $(self).data("nt-action");
                const actionSuccess = !!($(".popover .popover-body .text-success").length); // not exists when length=0
                setTimeout(() => {
                    $(self).popover('hide')
                    $(self).removeAttr("disabled");
                    if (actionSuccess) {
                        if (action === "PUT") { // EDIT
                            // When EDIT, insert a new tr after ".nt-listitem.nt-current", then remove it after 3 seconds.
                            // Mark as .nt-updated .border-success .border-4. will be deleted when Dialog/Modal closed
                            $(".nt-listitem.nt-current").removeClass("border-info border-5");
                            $(".nt-listitem.nt-current").addClass("nt-updated border-success border-4");
                            if (splitResponse.length > 1) {
                                $(".nt-listitem.nt-current").html(splitResponse[1]);
                                attachInlineEditingLaunchButtonClickEvent(".nt-listitem.nt-current .btn-nt-inline-editing");
                                attachIndividualSelectCheckboxClickEventHandler($(".nt-listitem.nt-current .nt-list-bulk-select .form-check-input"));
                            }
                            $(self).removeAttr("disabled");
                        }
                        else if (action === "POST") { // Create
                            // .nt-created .border-warning .border-4 added in the response
                            if (splitResponse.length > 1) {
                                const toAppend = $(splitResponse[1]);
                                const thisListItem = $(self).closest(".nt-listitem");
                                toAppend.insertBefore(thisListItem);
                                attachInlineEditingLaunchButtonClickEvent($(toAppend).find(".btn-nt-inline-editing"));
                                attachIndividualSelectCheckboxClickEventHandler($(toAppend).find(".nt-list-bulk-select .form-check-input"));
                            }
                            const createNewButton = $(self).closest(".nt-listitem").find(".nt-createnew-button-container");
                            const listItem = $(self).closest(".nt-listitem");
                            const createNewFormControls = $(listItem).children(":not(.nt-createnew-button-container)");
                            createNewFormControls.remove();
                            $(createNewButton).show();
                        }
                        else if (action === "DELETE") {
                            // Mark as .nt-deleted .border-danger .border-5. will be deleted when Dialog/Modal closed
                            $(".nt-listitem.nt-deleted").remove();
                        }
                    }
                }, 1500);
                // $("<tr><td colspan='100'>" + splitResponse[0] + "</td></tr>").insertAfter($(".nt-listitem.nt-current"));
            }
            // response part #2, to update the item which is updated/created.

        },
        failure: function (response) {
            // console.log(response);
            $(self).removeAttr("disabled");
        },
        error: function (response) {
            // console.log(response);
            $(self).removeAttr("disabled");
        }
    });
}
function initializeInlineEditing(button, action) {
    // 1.1. clear .nt-current on all .nt-listitem, then set .nt-current to current item,
    $(".nt-listitem").removeClass("nt-current");
    // 1.2. set .nt-current to .nt-list-container-submit
    $(".nt-list-container-submit").removeClass("nt-current");
    // 1.3. set .nt-current to .nt-list-wrapper
    $(".nt-list-wrapper").removeClass("nt-current");

    $(button).closest(".nt-listitem").addClass("nt-current");
    $(button).closest(".nt-listitem").addClass("border-info border-5");

    $(button).closest(".nt-list-container-submit").addClass("nt-current");
    $(button).closest(".nt-list-wrapper").addClass("nt-current");
}