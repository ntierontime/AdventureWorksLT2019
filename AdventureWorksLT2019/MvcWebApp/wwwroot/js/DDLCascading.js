// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// 1.Start CascadingDropdown
/* data-targetchild
 * data-nt-queryparamname
 * 
 * .nt-ddlcascading
 */
$(".nt-ddlcascading").change(function (e) {
    const childlisturl = $(this).data("childlisturl");
    const targetchild = $(this).data("targetchild");

    let query = {
        pageIndex: 1, pageSize: 10000 // get all.
    };

    var sameTargetChilds = $('select[data-targetchild="' + targetchild + '"]');
    // console.log(sameTargetChilds);
    $.each(sameTargetChilds, function (index, item) {
        const selectedValue = $(item).find(":selected").val();
        const queryparamname = $(item).data("nt-queryparamname");
        query[queryparamname] = selectedValue;
        //console.log(queryparamname);
        //console.log(selectedValue);
    });
    $.ajax({
        type: "GET",
        url: childlisturl,
        data: query,
        async: false,
        dataType: "html",
        success: function (response) {
            if (response.status == 200 && !!response.responseBody) {
                // console.log($('select[name="' + targetchild + '"]'));
                let firstOption = $('select[name="' + targetchild + '"] option:first-child').attr("selected", 'selected')
                // console.log(firstOption);
                $('select[name="' + targetchild + '"]').empty();
                $('select[name="' + targetchild + '"]').append(firstOption);
                $.each(response.responseBody, function (index, item) {
                    $('select[name="' + targetchild + '"]').append('<option value="' + item.value + '">' + item.name + '</option>');
                });
            }
            // console.log(response);
        },
        failure: function (response) {
            // console.log(response);
        },
        error: function (response) {
            // console.log(response);
        }
    });

    //console.log(childlisturl);
    //console.log(targetchild);
});
// 1.End CascadingDropdown