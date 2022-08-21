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
$(document).ready($(function () {
    $(".nt-page-size-submit").change(function (e) {
        $($(this).closest(".nt-list-wrapper").data("nt-submittarget")).find(".nt-page-size").val(e.target.value);
        $($(this).closest(".nt-list-wrapper").data("nt-submittarget")).submit();
    });
}));
$(document).ready($(function () {
    $(".nt-order-by-submit").change(function (e) {
        $($(this).closest(".nt-list-wrapper").data("nt-submittarget")).find(".nt-order-by").val(e.target.value);
        $($(this).closest(".nt-list-wrapper").data("nt-submittarget")).submit();
    });
}));
// 3.End Pagination and OrderBy

function pageLinkClicked(self) {
    const theForm = $($(self).closest(".nt-list-wrapper").data("nt-submittarget"));
    $(theForm).find(".nt-page-index").val($(self).data("nt-pageindex"));
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
                $(updateTarget).children(".btn-nt-load-more").remove()
                $(updateTarget).append(toAppend);
            }
            attachInlineEditingLaunchButtonClickEvent($(toAppend).find(".btn-nt-inline-editing"));
            attachIndividualSelectCheckboxClickEventHandler($(toAppend).find(".nt-list-bulk-select .form-check-input"));
            //console.log("success", response);
        },
        failure: function (response) {
            // console.log("failure", response);
        },
        error: function (response) {
            // console.log("error", response);
        }
    });
    return false;
}
