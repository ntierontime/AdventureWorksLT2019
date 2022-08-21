// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*
 * single dialog instance with id="crudActionDialog"
 * #crudActionDialog
 *
 * 1. data-nt- 
 * 1.1. data-nt- in this modal
 * data-nt-action
 * data-nt-view // from $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-option-field").val();
 * data-nt-container // from $(button)
 * data-nt-template // from $(button)
 * data-nt-postbackurl // calculated
 * data-nt-pagination PREV/NEXT on .btn-nt-pagination
 * 
 * 1.2. data-nt- in $(wrapper) .nt-list-wrapper
 * data-nt-bs-modalsize // .modal-sm (NONE) .modal-lg .modal-xl this is bootstrap modal size
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
 * .nt-modal-body
 * .btn-nt-pagination
 * .btn-nt-action
 * .btn-group-nt-action-pagination
 * .btn-group-nt-action-createanotherone
 * .btn-group-nt-action-save
 * .btn-group-nt-action-delete
 * .btn-group-nt-action-details
 * .nt-created, with .border-warning .border-3
 * .nt-updated, with .border-success .border-3
 * .nt-deleted, with .border-danger .border-3
 *  
 * 2.2. consumed css classes from where this modal is launched.
 * .nt-list-wrapper, shared with other .js files
 * .nt-list-container-submit, shared with other .js files
 * .nt-listitem
 * .nt-current // with on .nt-list-wrapper and .nt-list-container-submit, .nt-listitem(with .border-info .border-5)
 * .nt-paged-view-option-field: // read value for data-nt-view in this modal
 * 
 * 2.3. in ajax response html
 * .nt-hidden-modal-title, server side render modal title to a hidden field, will set to .modal-title 
 */

$(document).ready(function () {
    attachCrudActionDialog();
    attachCrudActionDialogActionEventHandler();
    attachCrudActionDialogPaginationEventHandler();
});

function attachCrudActionDialog() {
    let crudActionDialog = document.getElementById('crudActionDialog');

    // 1. Show Modal
    crudActionDialog.addEventListener('show.bs.modal', function (event) {
        let button = event.relatedTarget;

        const wrapper = $(button).closest(".nt-list-wrapper");
        const view = $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-option-field").val();
        const container = $(button).data("nt-container");
        const template = $(button).data("nt-template");
        const action = $(button).data("nt-action");
        let postbackurl = "";
        let routeid = ""
        if (action == "PUT") { // Edit
            routeid = $(button).closest(".nt-listitem").data("nt-route-id");
            postbackurl = $(wrapper).data("nt-updateitem-url");
        }
        else if (action == "POST") { // Create
            routeid = $(button).data("nt-route-id");
            postbackurl = $(wrapper).data("nt-createitem-url");
        }
        else if (action == "DELETE") {
            routeid = $(button).closest(".nt-listitem").data("nt-route-id");
            postbackurl = $(wrapper).data("nt-deleteitem-url");
        }
        else {
            routeid = $(button).closest(".nt-listitem").data("nt-route-id");
        }
        const loadItemUrl = $(wrapper).data("nt-loaditem-url");
        
        initializeCrudActionDialog(button, action, view, container, template, loadItemUrl, postbackurl, routeid)
        // 3. Ajax to get htmls
        ajaxLoadItemWhenCrudActionDialog(loadItemUrl + "/" + routeid, view, container, template, action);
    })

    // 2. Hide Modal
    crudActionDialog.addEventListener('hide.bs.modal', function (event) {
        // 1.1. clear .nt-current on all .nt-listitem, then set .nt-current to current item,
        closeCrudActionDialog();
    })
}

function attachCrudActionDialogPaginationEventHandler() {
    $("#crudActionDialog .btn-nt-pagination").click(function (e) {
        let button = event.currentTarget;
        const direction = $(button).data("nt-pagination");
        let routeid = "";

        if (direction === "PREV") { // previous item
            let prevItem = $(".nt-listitem.nt-current").prev(".nt-listitem");
            if (prevItem.length === 0) {
                prevItem = $(".nt-listitem.nt-current").closest(".nt-list-container-submit").find(".nt-listitem:last");
            }
            $(".nt-listitem.nt-current").removeClass("nt-current border-info border-5");
            $(prevItem).addClass("nt-current border-info border-5");
            $(prevItem).get(0).scrollIntoView();
            routeid = $(prevItem).data("nt-route-id");
        }
        else { // next item
            let nextItem = $(".nt-listitem.nt-current").next(".nt-listitem");
            if (nextItem.length === 0) {
                nextItem = $(".nt-listitem.nt-current").closest(".nt-list-container-submit").find(".nt-listitem:first");
            }
            $(".nt-listitem.nt-current").removeClass("nt-current border-info border-5");
            $(nextItem).addClass("nt-current border-info border-5");
            $(nextItem).get(0).scrollIntoView();
            routeid = $(nextItem).data("nt-route-id");
        }
        $("#crudActionDialog").data("nt-route-id", routeid);

        const loadItemUrl = $("#crudActionDialog").data("nt-loadItemUrl");

        const action = $("#crudActionDialog").data("nt-action");
        const view = $("#crudActionDialog").data("nt-view");
        const container = $("#crudActionDialog").data("nt-container");
        const template = $("#crudActionDialog").data("nt-template");

        // 3. Ajax to get htmls
        ajaxLoadItemWhenCrudActionDialog(loadItemUrl + "/" + routeid, view, container, template, action);
    });
}

function attachCrudActionDialogActionEventHandler() {
    $("#crudActionDialog .btn-nt-action").click(function (e) {
        const self = this;
        $(this).attr("disabled", true);
        const action = $("#crudActionDialog").data("nt-action");
        let loadItemUrl = $("#crudActionDialog").data("nt-loadItemUrl");
        let postbackurl = $("#crudActionDialog").data("nt-postbackurl");
        if (action != "POST") {
            const routeid = $("#crudActionDialog").data("nt-route-id");
            postbackurl = postbackurl + "/" + routeid;
            loadItemUrl = loadItemUrl + "/" + routeid;
        }
        const view = $("#crudActionDialog").data("nt-view");
        const container = $("#crudActionDialog").data("nt-container");
        const template = $("#crudActionDialog").data("nt-template");
        // Get FormData if there are in this dialog
        const form = $("#crudActionDialog form");
        let formData = [];
        if (!!form) {
            formData = new FormData($(form)[0]);
        }
        formData.append("view", view);
        formData.append("container", container);
        formData.append("template", template);

        ajaxPostbackWhenCrudActionDialog(postbackurl, formData, self, view, loadItemUrl, container, template);
    });
}

function closeCrudActionDialog() {
    //if (view == "List") {
    //    $(".nt-listitem").removeClass("nt-current border-info border-5");
    //}
    //else { // Tiles
    //    $(".nt-listitem .card").removeClass("border-info border-5");
    //    $(".nt-listitem").removeClass("nt-current");
    //}
    const action = $("#crudActionDialog").data("nt-action");
    setTimeout(() => {
        if (action === "PUT" || action === "POST") { // Edit/Create, do nothing
        }
        else if (action === "DELETE") {
            // remove deleted items after 1s Modal closed
            $(".nt-listitem.nt-deleted").remove();
        }
    }, action === "DELETE" ? 1000 : 2000);
    // 1.2. clear .nt-current to .nt-list-container-submit
    $(".nt-list-container-submit").removeClass("nt-current");
    // 1.3. clear .nt-current to .nt-list-wrapper
    $(".nt-list-wrapper").removeClass("nt-current");

    // 2.1. hide status section
    $("#crudActionDialog .nt-result").hide();
    // 2.2. clear .nt-modal-body inner html, remove all htmls
    $("#crudActionDialog .nt-modal-body").empty();

    // 3. removeData data-nt-* attribute in this modal.
    $("#crudActionDialog").removeData("nt-action nt-view nt-container nt-template nt-postbackurl");

    // 4. hide Save/Delete button groups
    $("#crudActionDialog .modal-footer .btn-group-nt-action-save").hide();
    $("#crudActionDialog .modal-footer .btn-group-nt-action-delete").hide();

    // 5. 
}

function ajaxLoadItemWhenCrudActionDialog(loadItemUrl, view, container, template, action) {
    $.ajax({
        type: "GET",
        url: loadItemUrl,
        data: { view, container, template },
        async: false,
        contentType: "application/json",
        success: function(response) {
            // 3.1. add response html to .nt-modal-body
            let modalBody = $("#crudActionDialog .nt-modal-body");
            if (action == "PUT" || action == "POST") // wrap with <form>..</form> Edit or Create
            {
                modalBody.html("<form>" + response + "</form>");
            }
            else { // DELETE, <form>...</form> wrapped around .nt-btn-action-delete
                modalBody.html(response);
            }
            // console.log(response);
            // 3.2. set text for .modal-title in .nt.hidden-modal-title
            $("#crudActionDialog .modal-header .modal-title").text($("#crudActionDialog .modal-body .nt-hidden-modal-title").val());
        },
        failure: function(response) {
            // console.log(response);
        },
        error: function(response) {
        }
    });
}

function ajaxPostbackWhenCrudActionDialog(postbackurl, formData, self, view, loadItemUrl, container, template) {
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
            // response part #1, status html
            if (splitResponse.length > 0) {
                $("#crudActionDialog .nt-status").html(splitResponse[0]);
                $("#crudActionDialog .nt-status").show();
            }
            // response part #1.1 TODO: should have a timer to auto hide after a few seconds.
            // response part #2, to update the item which is updated/created.
            const action = $("#crudActionDialog").data("nt-action");
            const actionSuccess = !!($("#crudActionDialog .nt-status .text-success").length);
            if (action === "PUT") { // EDIT 
                if (actionSuccess) {
                    // Mark as .nt-updated .border-success .border-4. will be deleted when Dialog/Modal closed
                    if (view == "List") {
                        $(".nt-listitem.nt-current").removeClass("border-info border-5");
                        $(".nt-listitem.nt-current").addClass("nt-updated border-success border-4");
                    }
                    else { // Tiles
                        $(".nt-listitem.nt-current .card").removeClass("border-info border-5");
                        $(".nt-listitem.nt-current").addClass("nt-updated");
                        $(".nt-listitem.nt-current .card").addClass("border-success border-4");
                    }
                }
                if (splitResponse.length > 1) {
                    $(".nt-listitem.nt-current").html(splitResponse[1]);
                    if (view == "Tiles") {
                        $(".nt-listitem.nt-current .card").addClass("border-success border-4");
                    }
                }
                $(self).removeAttr("disabled");
            }
            else if (action === "POST") { // Create
                // .nt-created .border-warning .border-4 added in the response
                if (splitResponse.length > 1) {
                    if (view === "List") { // html table
                        let theBody = $($("#crudActionDialog").data("nt-list-wrapper-id") + " tbody");
                        theBody.prepend(splitResponse[1]);
                    }
                    else { // Tile
                        let theBody = $($("#crudActionDialog").data("nt-list-wrapper-id") + " .nt-list-container-submit");
                        theBody.prepend(splitResponse[1]);
                    }
                }

                const createAnotherOneChecked = $("#crudActionDialog .modal-footer .btn-group-nt-action-createanotherone input").is(':checked');
                if (createAnotherOneChecked) {
                    $(self).removeAttr("disabled");
                    ajaxLoadItemWhenCrudActionDialog(loadItemUrl, view, container, template, action);
                }
            }
            else if (action === "DELETE") {
                if (actionSuccess) {
                    // Mark as .nt-deleted .border-danger .border-5. will be deleted when Dialog/Modal closed
                    if (view == "List") {
                        $(".nt-listitem.nt-current").addClass("nt-deleted border-danger border-3");
                    }
                    else { // Tiles
                        $(".nt-listitem.nt-current").addClass("nt-deleted");
                        $(".nt-listitem.nt-current .card").addClass("border-danger border-3");
                    }
                }
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

function initializeCrudActionDialog(button, action, view, container, template, loadItemUrl, postbackurl, routeid) {
    // 1.1. clear .nt-current on all .nt-listitem, then set .nt-current to current item,
    $(".nt-listitem").removeClass("nt-current");
    // 1.2. set .nt-current to .nt-list-container-submit
    $(".nt-list-container-submit").removeClass("nt-current");
    // 1.3. set .nt-current to .nt-list-wrapper
    $(".nt-list-wrapper").removeClass("nt-current");
    if (action != "POST") { // set .nt-current when not Create
        $(button).closest(".nt-listitem").addClass("nt-current");
        if (view == "List") {
            $(button).closest(".nt-listitem").addClass("border-info border-5");
        }
        else { // Tiles
            $(button).closest(".nt-listitem .card").addClass("border-info border-5");
        }
        
        $(button).closest(".nt-list-container-submit").addClass("nt-current");
        $(button).closest(".nt-list-wrapper").addClass("nt-current");
    }
    // 2. load the item partial view from .data-nt-partialurl
    const sizeCss = $(button).closest(".nt-list-wrapper").data("nt-bs-modalsize");
    if (!!sizeCss) {
        $("#crudActionDialog .modal-dialog").addClass(sizeCss);
    }
    $("#crudActionDialog").data("nt-action", action);
    $("#crudActionDialog").data("nt-view", view);
    $("#crudActionDialog").data("nt-container", container);
    $("#crudActionDialog").data("nt-template", template);
    $("#crudActionDialog").data("nt-loadItemUrl", loadItemUrl);
    $("#crudActionDialog").data("nt-postbackurl", postbackurl);
    $("#crudActionDialog").data("nt-route-id", routeid);

    // 2.1. Create: get id of the table wrapper .nt-list-wrapper
    if (action === "POST") { 
        $("#crudActionDialog").data("nt-list-wrapper-id", "#"+$(button).closest(".nt-list-wrapper").attr("id"));
    }
    // 3.2. clear .nt-result
    $("#crudActionDialog .nt-status").html("");
    $("#crudActionDialog .nt-status").hide();
    // 3.1. show/hide button based on CRUD Action Types
    $("#crudActionDialog div.btn-group").hide();
    if (action === "PUT") // EDIT 
    {
        $("#crudActionDialog .modal-footer .btn-group-nt-action-save").show();
        $("#crudActionDialog .modal-footer .btn-group-nt-action-pagination").show();
    }
    else if (action == "POST") { // Create
        $("#crudActionDialog .modal-footer .btn-group-nt-action-save").show();
        $("#crudActionDialog .modal-footer .btn-group-nt-action-createanotherone").show();
    }
    else if (action == "DELETE") { // Delete
        $("#crudActionDialog .modal-footer .btn-group-nt-action-delete").show();
        $("#crudActionDialog .modal-footer .btn-group-nt-action-pagination").show();
    }
    else { // Details
        $("#crudActionDialog .modal-footer .btn-group-nt-action-details").show();
        $("#crudActionDialog .modal-footer .btn-group-nt-action-pagination").show();
    }
    $("#crudActionDialog .modal-footer .btn-group").removeAttr("disabled");
}
