function hideFilters(btn_filter_id, filterContainer) {
    var btn = document.getElementById(btn_filter_id);
    var filterCont = document.getElementById(filterContainer);
    if (filterCont.style.height != "0px") {
        filterCont.style.height = "0px";
        btn.innerHTML = "Show Filters &nbsp;<i class='fa fa-caret-down'></i>";
    }
    else {
        filterCont.style.height = "initial";
        btn.innerHTML = "Hide Filters&nbsp;<i class='fa fa-caret-up'></i>";
    }
}