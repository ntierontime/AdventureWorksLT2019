// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*
 * single dialog instance with id="crudActionDialog"
 * simpleActionConfirmationDialog
 *
 * 1. data-nt- 
 * 1.1. data-nt- in this modal
 * data-nt-confirmation-message
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
$(document).ready(function () {
    attachSimpleActionConfirmationDialog();
})

function attachSimpleActionConfirmationDialog() {
    let simpleActionConfirmationDialog = document.getElementById('simpleActionConfirmationDialog');

    // 1. Show Modal
    simpleActionConfirmationDialog.addEventListener('show.bs.modal', function (event) {
        const sourceButton = event.relatedTarget;
        const confirmationMessage = $(sourceButton).data("nt-confirmation-message");
        $("#simpleActionConfirmationDialog .modal-body").html(confirmationMessage);
        $("#simpleActionConfirmationDialog .btn-nt-action-confirm").off();
        $("#simpleActionConfirmationDialog .btn-nt-action-confirm").removeAttr("disabled");
        $("#simpleActionConfirmationDialog .btn-nt-action-confirm").click(function (e) {
            $(this).attr("disabled", true);
            if ($(sourceButton).hasClass("nt-bulk-delete")) {
                // 1. Batch Delete
                bulkDelete(sourceButton, simpleActionConfirmationDialog);
            }
            else if ($(sourceButton).hasClass("nt-bulk-update-fixedvalue")) {
                // 2. Batch Action: Update Fixed Value
                bulkUpdateFixedValue(sourceButton, simpleActionConfirmationDialog);
            }
            else if ($(sourceButton).hasClass("btn-nt-multiitems-editing-submit")) {
                // 3. EditableTable: Create/Update/Delete
                multiItemsSubmitButtonClickEvent(sourceButton, simpleActionConfirmationDialog);
            }
        });

        $("#simpleActionConfirmationDialog .btn-nt-action-cancel").click(function (e) {
            if ($(sourceButton).hasClass("btn-nt-multiitems-editing-submit")) {
                // 3. EditableTable: reload if Success
                const actionSuccess = !!($(simpleActionConfirmationDialog).find(".text-success").length);
                if (actionSuccess) {
                    indexSearchSubmit($($(sourceButton).closest(".nt-list-wrapper").data("nt-submittarget")));
                    $(sourceButton).prop("disabled", true);
                }
            }
        });
    })
}
