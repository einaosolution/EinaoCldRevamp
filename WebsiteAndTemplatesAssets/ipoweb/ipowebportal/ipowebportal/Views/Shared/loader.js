var Loader = function () {
    return {
        show: function (id) {
            var pageLoader = document.getElementById(id);
            pageLoader.classList.remove("hide");
        },
        hide: function (id) {
            var pageLoader = document.getElementById(id);
            pageLoader.classList.add("hide");
        }
    }
}
var loader = Loader();



var PageLoader = function () {
    var pageLoader = document.getElementById("page-loader");
    return {
        show: function () {
            pageLoader.setAttribute("show", "");
            document.body.setAttribute("page-loading", "");

            setTimeout(function () {
                pageLoader.setAttribute("popout", "");
            }, 100);
        },
        hide: function () {
            pageLoader.removeAttribute("popout");
            document.body.removeAttribute("page-loading");

            setTimeout(function () {
                pageLoader.removeAttribute("show");
            }, 100);
        }
    }
}
var pageLoader = PageLoader();