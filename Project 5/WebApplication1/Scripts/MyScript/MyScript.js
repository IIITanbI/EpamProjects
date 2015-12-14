

jQuery(function ($) {
    if ($("#page-nav").length) {
        redrawPaginator(2);
    } 
});

function redrawPaginator(perPage) {
    var pageParts = $(".paginate");
    var numPages = pageParts.length;
    

    pageParts.hide();
    pageParts.slice(0, perPage).show();

    $("#page-nav").pagination({
        items: numPages,
        itemsOnPage: perPage,
        cssStyle: "light-theme",

        onPageClick: function (pageNum) {
            var start = perPage * (pageNum - 1);
            var end = start + perPage;
            pageParts.hide();
            pageParts.slice(start, end).show();
        }
    });
   
};

