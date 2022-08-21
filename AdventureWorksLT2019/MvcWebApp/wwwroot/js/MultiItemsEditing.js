// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*
 * 1. data-nt- 
 * 1.1. data-nt- in this modal
 *
 * 1.2. data-nt- in $(wrapper) .nt-list-wrapper
 * 
 * 1.3. data-nt- in .nt-listitem
 * 
 * 1.4. data-nt- in trigger button .nt-btn-action-save or .nt-btn-action-delete
 * 
 * 2. css classes
 * 2.1. only in this modal
 *  
 * 2.2. consumed css classes from where this modal is launched.
 * 
 * 2.3. in ajax response html
 * 
 */

function multiItemsSubmitButtonClickEvent(sourceButton, dialog) {
    const currentListItem = $(sourceButton).closest(".nt-listitem");
    const wrapper = $(sourceButton).closest(".nt-list-wrapper");
    const view = $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-option-field").val();
    const container = $(sourceButton).data("nt-container");
    const template = $(sourceButton).data("nt-template");
    const action = $(sourceButton).data("nt-action");

    const multiitemsSubmitUrl = $(wrapper).data("nt-multiitems-submit-url");

    const form = $(wrapper).find("form");
    let formData = [];
    if (!!form) {
        formData = new FormData($(form)[0]);
    }

    $.ajax({
        type: "POST",
        url: multiitemsSubmitUrl,
        data: formData,
        async: false,
        processData: false,
        contentType: false,
        dataType: "html",
        success: function (response) {
            console.log(response);
            $(dialog).find(".modal-body").html(response);
            const actionSuccess = !!($(dialog).find(".text-success").length);
            if (actionSuccess) {
                // disable save button
                $(sourceButton).prop("disable", true);
            }
        },
        failure: function (response) {
            // console.log(response);
            // $(sourceButton).removeAttr("disabled");
        },
        error: function (response) {
        }
    });
}

function attachMultiItemsSubmitButtonClickEvent(selector) {
    $(selector).click(function (e) {
        let button = e.currentTarget;
        multiItemsSubmitButtonClickEvent();
    });
}

// nt-list-container-submit
function attachMultiItemsDeleteCheckboxEvent(selector) {
    $(selector).on('keyup change paste', 'td.nt-editablelist-delete-select input.nt-form-check', function () {
        const self = this;
        $(self).closest(".nt-listitem").find("input.nt-form-data").prop("readonly", $(self).is(':checked'));
        $(self).closest(".nt-listitem").find("textarea.nt-form-data").prop("readonly", $(self).is(':checked'));
        $(self).closest(".nt-listitem").find("select.nt-form-data").attr("readonly", $(self).is(':checked'));
        $(self).closest(".nt-listitem").find("select.nt-form-data option:not(:selected)").prop("disabled", $(self).is(':checked'));
        $(self).closest(".nt-listitem").find("input.nt-form-check").prop("readonly", $(self).is(':checked'));
        $(self).prop("readonly", false);
    });
}

function increateListCountBy1(selector) {
    const theHidden = $(selector).closest(".nt-list-container-submit").find(".nt-list-count");
    if (!!theHidden) {
        const current = parseInt(theHidden.val());
        theHidden.val(current + 1);
    }
}