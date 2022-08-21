// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*
 * 1. data-nt- 
 * 1.1. data-nt- in this modal
 *
 * 1.2. data-nt- in $(wrapper) .nt-list-wrapper
 * data-nt-route-id-def
 * data-nt-batchdelete-url
 * 
 * 1.3. data-nt- in .nt-listitem
 * 
 * 1.4. data-nt- in trigger button .nt-btn-action-save or .nt-btn-action-delete
 * 
 * 2. css classes
 * 2.1. only in this modal
 *  
 * 2.2. consumed css classes from where this modal is launched.
 * .nt-list-wrapper
 * .nt-listitem
 * .nt-list-bulk-select
 * .nt-bulk-delete
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

function bulkDelete(sourceButton, dialog) {
    // composite id when data-nt-route-id-def is null, ids is a string array, only one property name(in identifier query) is allowed in data-nt-route-id-def
    const routeIdDef = $(sourceButton).closest(".nt-list-wrapper").data("nt-route-id-def");
    const routeIds = $(sourceButton).closest(".nt-list-wrapper").find(".nt-listitem .nt-list-bulk-select .form-check-input:checked").closest(".nt-listitem").map((i, x) => $(x).data("nt-route-id"));
    let formData = new FormData();
    $.each(routeIds, function (index, l) {
        const formDataPropertyName = "ids[" + index + "]" + (!!routeIdDef ? "." + routeIdDef : "");
        formData.append(formDataPropertyName, l);
    });
    const postbackurl = $(sourceButton).closest(".nt-list-wrapper").data("nt-bulk-delete-url");
    $.ajax({
        type: "POST",
        url: postbackurl,
        data: formData,
        async: false,
        processData: false,
        contentType: false,
        dataType: "html",
        success: function (response) {
            $(sourceButton).closest(".nt-list-wrapper").find(".nt-listitem .nt-list-bulk-select .form-check-input:checked").closest(".nt-listitem").remove();
            // console.log(response);
            const modal = bootstrap.Modal.getInstance(dialog);
            modal.hide();
            setStatusDataAndIcon($(sourceButton).closest(".nt-list-wrapper"));
            showSingletonMessagePopup(response);
        },
        failure: function (response) {
            // console.log(response);
            $(dialog).find(".modal-body").append(response);
            $(sourceButton).removeAttr("disabled");
        },
        error: function (response) {
            // console.log(response);
            $(dialog).find(".modal-body").append(response);
            $(sourceButton).removeAttr("disabled");
        }
    });
}

function bulkUpdateFixedValue(sourceButton, dialog) {
    const wrapper = $(sourceButton).closest(".nt-list-wrapper");
    const view = $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-option-field").val();
    // composite id when data-nt-route-id-def is null, ids is a string array, only one property name(in identifier query) is allowed in data-nt-route-id-def
    const routeIdDef = $(sourceButton).closest(".nt-list-wrapper").data("nt-route-id-def");
    const routeIds = $(sourceButton).closest(".nt-list-wrapper").find(".nt-listitem .nt-list-bulk-select .form-check-input:checked").closest(".nt-listitem").map((i, x) => $(x).data("nt-route-id"));
    let formData = new FormData();
    $.each(routeIds, function (index, l) {
        const formDataPropertyName = "ids[" + index + "]" + (!!routeIdDef ? "." + routeIdDef : "");
        formData.append(formDataPropertyName, l);
    });
    // format: {name1}={value1}|{name2}={value2}|...
    const toUpdateNameValueSplit = $(sourceButton).data("nt-namevalue").split("|");
    $.each(toUpdateNameValueSplit, function (index, l) {
        if (!!l && l.includes(":")) {
            const lsplit = l.split(":");
            formData.append(lsplit[0], lsplit[1]);
        }
    });
    // e.g. ~ElmahError/BulkUpdate + Application(ActionName)
    let postbackurl = $(sourceButton).closest(".nt-list-wrapper").data("nt-bulk-update-url") + $(sourceButton).data("nt-actionname");
    postbackurl = postbackurl + "?view=" + view; 
    $.ajax({
        type: "POST",
        url: postbackurl,
        data: formData,
        async: false,
        processData: false,
        contentType: false,
        dataType: "html",
        success: function (response) {
            //$(sourceButton).closest(".nt-list-wrapper").find(".nt-listitem .nt-list-bulk-select .form-check-input:checked").closest(".nt-listitem").remove();
            const splitResponse = response.split("===---------===");
            // response part #1, status html
            if (splitResponse.length > 0) {
                $(dialog).find(".modal-body").append(splitResponse[0]);
            }
            const actionSuccess = !!($(dialog).find(".text-success").length);
            if (actionSuccess) {
                if (splitResponse.length > 1) {
                    for (i = 1; i < splitResponse.length; i++) {
                        const responseRoutId = $(splitResponse[i]).data("nt-route-id");
                        $(sourceButton).closest(".nt-list-wrapper").find(".nt-listitem[data-nt-route-id='" + responseRoutId + "']").html($(splitResponse[i]).html())
                    }
                }
                attachIndividualSelectCheckboxClickEventHandler($(wrapper).find(".nt-listitem .nt-list-bulk-select .form-check-input:checked"));
                attachInlineEditingLaunchButtonClickEvent($(wrapper).find(".nt-listitem .nt-list-bulk-select .form-check-input:checked").closest(".nt-listitem").find(".btn-nt-inline-editing"));
                const modal = bootstrap.Modal.getInstance(dialog);
                modal.hide();
                showSingletonMessagePopup(splitResponse[0]);
            }
        },
        failure: function (response) {
            // console.log(response);
            $(sourceButton).removeAttr("disabled");
        },
        error: function (response) {
            // console.log(response);
            $(sourceButton).removeAttr("disabled");
        }
    });
}