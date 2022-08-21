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
 * .nt-singlton-message-popup
 *  
 * 2.2. consumed css classes from where this modal is launched.
 * 
 * 2.3. in ajax response html
 */

function showSingletonMessagePopup(content) {
    $(".nt-singlton-message-popup").html(content);
    $(".nt-singlton-message-popup").show();
    setTimeout(() => {
        // response part #2, to update the item which is updated/created.
        $(".nt-singlton-message-popup").hide();
        $(".nt-singlton-message-popup").html("");
    }, 1500);
}