/* this file contains all functions for different views of $(document).ready($(function() { ... }
 */

function documentReadyForSearchList(searchFormId, listWrapperId) {
    // 1. SearchForm related
    attachAjaxLoadItemsSubmit("#" + searchFormId);
    attachCacadingDropDownListChanged("#" + searchFormId);

    // 2. List Wrapper Related
    attachCacadingDropDownListChanged("#" + listWrapperId);
    showHidePagedViewOptionsRelatedButtons("#" + listWrapperId);
    attachFormDataChanged($("#" + listWrapperId + " form"));
    attachMultiItemsDeleteCheckboxEvent($("#" + listWrapperId + " form"));
    attachListRefreshButtonClickEvent($("#" + listWrapperId + " .btn-nt-fresh"));
    attatchPageLinkClicked($("#" + listWrapperId + " .page-link"));
    attachBulkSelectStatusClickEventHandler("#" + listWrapperId + " .nt-bulk-select-filter .btn-nt-bulk-select-status");
    attachIndividualSelectCheckboxClickEventHandler("#" + listWrapperId + " .nt-listitem .nt-list-bulk-select .form-check-input");
    attachQuickSelectClickEventHandler("#" + listWrapperId + " .nt-bulk-select-filter .nt-quick-bulk-select");
    //attatchPageSizeChangedUsingSelect($("#" + listWrapperId + " .nt-page-size-submit"));
    //attatchOrderByChangedUsingSelect($("#" + listWrapperId + " .nt-order-by-submit"));
    attatchPageSizeChangedUsingDropDown($("#" + listWrapperId + " .nt-page-size-and-order-by .nt-page-size-item"));
    attatchOrderByChangedUsingDropDown($("#" + listWrapperId + " .nt-page-size-and-order-by .nt-order-by-item"));
}

function documentReadyForPopups(f, s, c) {
    // 1. FullPageLoading
    if (!!f)
        attachFullScreenLoading();
    // 2. SimpleActionConfirmationDialog
    if (!!s)
        attachSimpleActionConfirmationDialog();
    // 3. CrudActionDialog
    if (!!c) {
        attachCrudActionDialog();
        attachCrudActionDialogActionEventHandler();
        attachCrudActionDialogPaginationEventHandler();
    }
}
