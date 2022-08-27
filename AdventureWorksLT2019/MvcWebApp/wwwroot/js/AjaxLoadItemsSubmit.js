﻿// 4.Start. Clear all select/input inside .nt-advanced-search-field when .nt-advanced-search-button button clicked
/*
 * .nt-advanced-search-button
 * .nt-advanced-search-field
 */
$(document).ready($(function () {
    $('.nt-advanced-search-button').click(function (e) {
        $('.nt-advanced-search-field div div select').val('')
        $('.nt-advanced-search-field div div input').val('')
    });
}));
// 4.End. Clear all select/input inside .advanced-search-field when .advanced-search-button button clicked

// 5.start form ajax-submit: POST .ajax-partial-load-post
/* 
 * data-nt-partial-url
 * data-nt-updatetarget
 * data-nt-pageindex
 * 
 * .nt-ajax-partial-load-post-formdata
 * .nt-ajax-partial-load-get
 * .nt-list-container-submit
 * .nt-page-index
 * .nt-paged-view-option-field
 * .btn-nt-load-more
 *
 */
function attachListRefreshButtonClickEvent(selector) {
    $(selector).click(function (e) {
        let button = e.currentTarget;
        const submitTarget = $(button).closest(".nt-list-wrapper").data("nt-submittarget");
        ajaxLoadItemsSubmit($(submitTarget))
    });
}

function attachAjaxLoadItemsSubmit(selector) {
    $(selector).submit(function (e) {
        $(this).find(".nt-page-index").val(1);
        ajaxLoadItemsSubmit(this);
    });
}

function attachEventHandlersWhenAjaxLoadItemsSubmitSuccess(toAppend, updateTarget) {
    attachInlineEditingLaunchButtonClickEvent($(toAppend).find(".btn-nt-inline-editing"));
    attachIndividualSelectCheckboxClickEventHandler($(toAppend).find(".nt-list-bulk-select .form-check-input"));
    attachFormDataChanged($(toAppend).find("form"));
    attachMultiItemsDeleteCheckboxEvent($(toAppend).find("form"));
    attatchPageLinkClicked($(toAppend).find(".page-link"));
    resetBulkSelectStatus($(updateTarget).closest(".nt-list-wrapper"));
}

function ajaxLoadItemsSubmit(theForm) {
    const url = $(theForm).data("nt-partial-url");
    const updateTarget = $($(theForm).data("nt-updatetarget")).find(".nt-list-container-submit");
    var formData = new FormData($(theForm)[0]);
    const pagedViewOption = $(theForm).children(".nt-paged-view-option-field").val();
    const pageIndex = $(theForm).children(".nt-page-index").val();
    $.ajax({
        type: "POST",
        url: url,
        data: formData,
        async: false,
        processData: false,
        contentType: false,
        dataType: "html",
        success: function (response) {
            const toAppend = $(response);
            if (pagedViewOption !== "Tiles" || pageIndex == 1) {
                $(updateTarget).html(toAppend);
            }
            else {
                $(updateTarget).children(".btn-nt-load-more").remove();
                $(updateTarget).append(toAppend);
            }
            attachEventHandlersWhenAjaxLoadItemsSubmitSuccess(toAppend, updateTarget);

            setTimeout(() => {
                bootstrap.Modal.getOrCreateInstance(document.getElementById('fullScreenLoading')).hide();
            }, 1000);
        },
        failure: function(response) {
            // console.log("failure", response);
            setTimeout(() => {
                bootstrap.Modal.getOrCreateInstance(document.getElementById('fullScreenLoading')).hide();
            }, 1000);
        },
        error: function(response) {
            // console.log("error", response);
            setTimeout(() => {
                bootstrap.Modal.getOrCreateInstance(document.getElementById('fullScreenLoading')).hide();
            }, 1000);
        }
    });
}

// 5.end form ajax-submit