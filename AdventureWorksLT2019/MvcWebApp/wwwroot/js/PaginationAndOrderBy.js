// 3.Start Pagination and OrderBy
/* 
 * data-nt-submittarget
 *
 * .nt-page-size-submit
 * .nt-order-by-submit
 * .nt-page-size
 * .nt-order-by
 * 
 */
function attatchPageSizeChangedUsingDropDown(selector) {
    $(selector).click(function (e) {
        const self = e.currentTarget;
        const theListWrapper = $(self).closest(".nt-list-wrapper");
        const theForm = $($(theListWrapper).data("nt-submittarget"));
        $(theForm).find(".nt-page-size").val($(self).data("nt-page-size"));
        $(theForm).find(".nt-page-index").val(1);
        $(self).closest(".dropdown-menu").find(".nt-page-size-item .fa-check").hide();
        $(self).find(".fa-check").show();
        ajaxLoadItemsSubmit($(theForm));
    });
}

function attatchOrderByChangedUsingDropDown(selector) {
    $(selector).click(function (e) {
        const self = e.currentTarget;
        const theListWrapper = $(self).closest(".nt-list-wrapper");
        const theForm = $($(theListWrapper).data("nt-submittarget"));
        $(theForm).find(".nt-order-by").val($(self).data("nt-order-by"));
        $(theForm).find(".nt-page-index").val(1);
        $(self).closest(".dropdown-menu").find(".nt-order-by-item .fa-check").hide();
        $(self).find(".fa-check").show();
        ajaxLoadItemsSubmit($(theForm));
    });
}
// 3.End Pagination and OrderBy

function attatchPageLinkClicked(selector) {
    $(selector).click(function (e) {
        const self = e.currentTarget;
        const theListWrapper = $(self).closest(".nt-list-wrapper");
        const theForm = $($(theListWrapper).data("nt-submittarget"));
        $(theForm).find(".nt-page-index").val($(self).data("nt-pageindex"));
        ajaxLoadItemsSubmit($(theForm));
    });
}
