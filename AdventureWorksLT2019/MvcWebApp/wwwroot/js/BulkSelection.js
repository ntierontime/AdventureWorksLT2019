// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*
 * 1. data-nt- 
 * 1.1. data-nt- in this modal
 * data-nt-filtername
 * data-nt-filtervalue
 * data-nt-checkbox-checked
 * data-nt-filter-{filterName}="{filterValue}"
 * 
 * 1.2. data-nt- in $(wrapper) .nt-list-wrapper
 * 
 * 1.3. data-nt- in .nt-listitem
 * 
 * 1.4. data-nt- in trigger button .nt-btn-action-save or .nt-btn-action-delete
 * 
 * 2. css classes
 * 2.1. only in this modal
 * .nt-bulk-select-filter
 * .btn-nt-bulk-select-status
 * .nt-list-bulk-select
 *  
 * 2.2. consumed css classes from where this modal is launched.
 * .nt-list-wrapper
 * .nt-listitem
 * 
 * 2.3. bootstrap 5 css classses
 * .form-check-input :check
 * .bg-info
 * 
 * 2.4. in ajax response html
 * 
 * 3. Html tags
 * table tbody
 */

$(document).ready(function () {
    attachBulkSelectStatusClickEventHandler(".nt-bulk-select-filter .btn-nt-bulk-select-status");
    attachIndividualSelectCheckboxClickEventHandler(".nt-list-wrapper .nt-listitem .nt-list-bulk-select .form-check-input");
    attachQuickSelectClickEventHandler(".nt-bulk-select-filter .nt-quick-bulk-select");
});

function attachBulkSelectStatusClickEventHandler(selector) {
    $(selector).click(function (e) {
        const currentStatus = $(e.currentTarget).data("nt-bulk-select-status");
        const allCheckBoxes = $(e.currentTarget).closest(".nt-list-wrapper").find(".nt-listitem .nt-list-bulk-select .form-check-input");
        if (currentStatus !== "None") {// Some or All 
            $(allCheckBoxes).prop("checked", false); // uncheck all
            $(e.currentTarget).data("nt-bulk-select-status", "None")
            $(e.currentTarget).html('<i class="fa-regular fa-square"></i>');
        }
        else {
            $(allCheckBoxes).prop("checked", true); // check all
            $(e.currentTarget).data("nt-bulk-select-status", "All")
            $(e.currentTarget).html('<i class="fa-regular fa-square-check"></i>');
        }
        toggleListItemBackground($(e.currentTarget).closest(".nt-list-wrapper"));
        toggleBulkActionButtons($(e.currentTarget).closest(".nt-list-wrapper"));
        toggleRefreshButton($(e.currentTarget).closest(".nt-list-wrapper"))
    })
}

function attachIndividualSelectCheckboxClickEventHandler(selector) {
    $(selector).click(function (e) {
        setStatusDataAndIcon($(e.currentTarget).closest(".nt-list-wrapper"));
        toggleListItemBackground($(e.currentTarget).closest(".nt-list-wrapper"));
        toggleBulkActionButtons($(e.currentTarget).closest(".nt-list-wrapper"));
        toggleRefreshButton($(e.currentTarget).closest(".nt-list-wrapper"))
    })
}

function attachQuickSelectClickEventHandler(selector) {
    $(selector).click(function (e) {
        $(e.currentTarget).closest(".nt-list-wrapper").find(".nt-listitem .nt-list-bulk-select .form-check-input").prop("checked", false);
        const filterName = $(e.currentTarget).data("nt-filtername");
        const filterValue = $(e.currentTarget).data("nt-filtervalue");
        const setCheckedTo = $(e.currentTarget).data("nt-checkbox-checked");
        $(e.currentTarget).closest(".nt-list-wrapper").find(".nt-listitem .nt-quick-bulk-select[data-nt-filter-" + filterName + "='" + filterValue + "']").closest(".nt-listitem").find(".nt-list-bulk-select .form-check-input").prop("checked", setCheckedTo);
        setStatusDataAndIcon($(e.currentTarget).closest(".nt-list-wrapper"));
        toggleListItemBackground($(e.currentTarget).closest(".nt-list-wrapper"));
        toggleBulkActionButtons($(e.currentTarget).closest(".nt-list-wrapper"));
        toggleRefreshButton($(e.currentTarget).closest(".nt-list-wrapper"))
    })
}

function setStatusDataAndIcon(parentselector) {
    const checked = $(parentselector).find(".nt-listitem .nt-list-bulk-select .form-check-input:checked");
    const notChecked = $(parentselector).find(".nt-listitem .nt-list-bulk-select .form-check-input:not(:checked)");
    const bulkSelectStatus = $(parentselector).find(".nt-bulk-select-filter .btn-nt-bulk-select-status");
    if (checked.length === 0 && notChecked.length !== 0) {
        bulkSelectStatus.data("nt-bulk-select-status", "None")
        bulkSelectStatus.html('<i class="fa-regular fa-square"></i>');
    }
    else if (checked.length !== 0 && notChecked.length === 0) {
        bulkSelectStatus.data("nt-bulk-select-status", "All")
        bulkSelectStatus.html('<i class="fa-regular fa-square-check"></i>');
    }
    else {
        bulkSelectStatus.data("nt-bulk-select-status", "Some")
        bulkSelectStatus.html('<i class="fa-regular fa-square-minus"></i>');
    }
 }


function toggleListItemBackground(parentselector) {
    $(parentselector).find(".nt-listitem:not(.nt-new) .nt-list-bulk-select .form-check-input:checked").closest(".nt-listitem").addClass("bg-info");
    $(parentselector).find(".nt-listitem:not(.nt-new) .nt-list-bulk-select .form-check-input:not(:checked)").closest(".nt-listitem").removeClass("bg-info");
}

function toggleBulkActionButtons(parentselector) {
    const bulkSelectStatus = $(parentselector).find(".nt-bulk-select-filter .btn-nt-bulk-select-status").data("nt-bulk-select-status");
    if (bulkSelectStatus !== "None") {
        $(parentselector).find(".nt-bulk-actions-container").show();
    }
    else {
        $(parentselector).find(".nt-bulk-actions-container").hide();
    }
}

function toggleRefreshButton(parentselector) {
    const bulkSelectStatus = $(parentselector).find(".nt-bulk-select-filter .btn-nt-bulk-select-status").data("nt-bulk-select-status");
    if (bulkSelectStatus == "None") {
        $(parentselector).find(".nt-refresh-button-group").show();
    }
    else {
        $(parentselector).find(".nt-refresh-button-group").hide();
    }
}
