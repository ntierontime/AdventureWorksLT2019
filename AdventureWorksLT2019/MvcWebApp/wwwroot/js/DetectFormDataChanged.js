
function attachFormDataChanged(theWrapper) {
    $(theWrapper).on('keyup change paste', 'input.nt-form-data, select.nt-form-data, textarea.nt-form-data', function () {
        const self = this;
        const itemStatusField = $(self).closest(".nt-listitem").find(".nt-item-status input");
        const currentItemStatus = $(itemStatusField).val(); 
        // 1. add/remove field changed css classes - nt-form-data-changed border-danger border-2
        if (currentItemStatus !== "New") { // not NoChange or Updated, see C# Framework.Models.ItemUIStatus
            const thisFieldChanged = $(self).val().toString() !== $(self).data("nt-value");
            if (thisFieldChanged) {
                $(self).closest(".form-group").addClass("nt-form-data-changed border-danger border-2");
            }
            else {
                $(self).closest(".form-group").removeClass("nt-form-data-changed border-danger border-2");
            }

            if (!$(self).closest(".form-group").hasClass("nt-editablelist-delete-select")) {
                const newItemStatus = $(self).closest(".nt-listitem").find(".nt-form-data-changed").length === 0 ? "NoChange" : "Updated";
                $(itemStatusField).val(newItemStatus);
            }
        }
        const formDataChanged = $(self).closest(".nt-list-wrapper").find(".nt-form-data-changed").length > 0 ||
            $(self).closest(".nt-list-wrapper").find(".nt-item-status input[data-nt-value='New']").length > 0;
        enableSaveButton($(self).closest(".nt-list-wrapper"), formDataChanged);
    });

    $(theWrapper).on('keyup change paste', 'input.nt-form-check', function () {
        const self = this;
        const itemStatusField = $(self).closest(".nt-listitem").find(".nt-item-status input");
        const currentItemStatus = $(itemStatusField).val();
        // 1. add/remove field changed css classes - nt-form-data-changed border-danger border-2
        if (currentItemStatus !== "New") { // not NoChange or Updated, see C# Framework.Models.ItemUIStatus
            const thisFieldChanged = $(self).is(':checked').toString() !== $(self).data("nt-value").toString().toLowerCase();
            if (thisFieldChanged) {
                $(self).closest(".form-group").addClass("nt-form-data-changed border-danger border-2");
            }
            else {
                $(self).closest(".form-group").removeClass("nt-form-data-changed border-danger border-2");
            }
            if (!$(self).closest(".form-group").hasClass("nt-editablelist-delete-select")) {
                const newItemStatus = $(self).closest(".nt-listitem").find(".nt-form-data-changed").length === 0 ? "NoChange" : "Updated";
                $(itemStatusField).val(newItemStatus);
            }
        }
        const formDataChanged = $(self).closest(".nt-list-wrapper").find(".nt-form-data-changed").length > 0 ||
            $(self).closest(".nt-list-wrapper").find(".nt-item-status input[data-nt-value='New']").length > 0;
        enableSaveButton($(self).closest(".nt-list-wrapper"), formDataChanged);
    });
}

function enableSaveButton(listWrapperSelector, formDataChanged) {
    // 2. update paged-view-option
    const submitTarget = $(listWrapperSelector).data("nt-submittarget");
    const view = $(submitTarget).find(".nt-paged-view-option-field").val();
    if (view === "EditableTable") {
        $(listWrapperSelector).find(".nt-multiitem-editing-buttons .btn-nt-multiitems-editing-submit").prop("disabled", !formDataChanged);
    }
    else {

    }
}