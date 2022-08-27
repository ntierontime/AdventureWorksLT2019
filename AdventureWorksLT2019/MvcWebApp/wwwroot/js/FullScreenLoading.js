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
function attachFullScreenLoading() {
    let fullScreenLoadingEl = document.getElementById('fullScreenLoading');

    // 1. Show Modal
    fullScreenLoadingEl.addEventListener('show.bs.modal', function (event) {
        const sourceButton = event.relatedTarget;
        if ($(sourceButton).hasClass("nt-radio-pagedviewoption")) {
            // 1. Batch Delete
            pageViewOptionsClickedEventHandler(sourceButton);
        }
    })
}
