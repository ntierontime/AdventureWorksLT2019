// 6.Start. PagedViewOptions clicked
/*
 * data-nt-submittarget - e.g. #thisForm
 * data-nt-paginationoptionupdatevalue - e.g. // List-Pagination, Tiles-More, Slideshow-NoPagination
 * data-nt-value - PagedViewOption - List/Tile/Slideshow/EditableTable
 * 
 * .nt-radio-pagedviewoption
 * 
 * nt-pagination-option-field
 * nt-paged-view-option-field
 * 
 * nt-submittarget
 */
function pageViewOptionsClickedEventHandler(sourceButton) {
    const submitTarget = $(sourceButton).closest(".nt-list-wrapper").data("nt-submittarget");
    const pagedViewOption = $(sourceButton).data("nt-value");
    if (pagedViewOption === "EditableTable") {
        $(sourceButton).closest(".nt-list-wrapper").find(".nt-multiitem-editing-buttons").show();
        $(submitTarget).find(".nt-template-field").val("Edit");
        $(sourceButton).closest(".nt-list-wrapper").find(".nt-bulk-select-filter").hide();
        $(sourceButton).closest(".nt-list-wrapper").find(".nt-bulk-actions-container").hide();
        $(sourceButton).closest(".nt-list-wrapper").find(".nt-bulk-select-filter .btn-nt-bulk-select-status").val("None");
        $(sourceButton).closest(".nt-list-wrapper").find(".btn-nt-create-new-float").hide();
    }
    else {
        $(sourceButton).closest(".nt-list-wrapper").find(".nt-multiitem-editing-buttons").hide();
        $(submitTarget).find(".nt-template-field").val("Details");
        $(sourceButton).closest(".nt-list-wrapper").find(".nt-bulk-select-filter").show();
        $(sourceButton).closest(".nt-list-wrapper").find(".btn-nt-create-new-float").hide();
    }

    // 1. update pagination-option
    $(submitTarget).find(".nt-pagination-option-field").val($(sourceButton).data("nt-paginationoptionupdatevalue")); // List-Pagination, Tiles-More, Slideshow-NoPagination

    // 2. update paged-view-option
    $(submitTarget).find(".nt-paged-view-option-field").val(pagedViewOption); // List-Pagination, Tiles-More, Slideshow-NoPagination

    // 3. reset pageIndex to 1
    $(submitTarget).find(".nt-page-index").val(1); 

    ajaxLoadItemsSubmit($(submitTarget));
    // console.log($(location));
    // Update QueryString - view with data-nt-value attribute
    let queryParams = new URLSearchParams(window.location.search);
    queryParams.set("PagedViewOption", $(sourceButton).data("nt-value"));
    history.pushState(null, null, "?" + queryParams.toString());

    $(".btn-nt-inline-editing").off();
    attachInlineEditingLaunchButtonClickEvent(".btn-nt-inline-editing");
}

function showHidePagedViewOptionsRelatedButtons(wrapperSelector) {
    const thePagedViewOptionField = $(wrapperSelector).find(".nt-paged-view-option-field");
    const currentView = $(thePagedViewOptionField).val();
    const listWrapper = $($(thePagedViewOptionField).closest(".nt-ajax-partial-load-post-formdata").data("nt-updatetarget"));
    const editableListButtons = $(listWrapper).find(".nt-multiitem-editing-buttons");
    const listBulkSelectFilter = $(listWrapper).find(".nt-bulk-select-filter");
    const listBulkActionContainer = $(listWrapper).find(".nt-bulk-actions-container");
    const createNewButtonFloat = $(listWrapper).find(".btn-nt-create-new-float");
    if (currentView == "EditableTable") {
        // 1. show EditableTable Buttons
        editableListButtons.show();
        listBulkSelectFilter.hide();
        listBulkActionContainer.hide();
        createNewButtonFloat.hide();
    }
    else {
        editableListButtons.hide();
        listBulkSelectFilter.show();
        createNewButtonFloat.show();
    }
}

// 6.End. PagedViewOptions clicked
