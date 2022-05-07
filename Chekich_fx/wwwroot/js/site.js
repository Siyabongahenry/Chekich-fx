
//Side Nav
function openSideNav() {
    var sidenav = document.getElementsByClassName("menu-2")[0];
    if (sidenav.style.marginLeft == "" || sidenav.style.marginLeft == "-80%") {
        sidenav.style.marginLeft = "0%";
    }
    else {
        sidenav.style.marginLeft = "-80%";

    }
    
}
function openSubMenu(btn,sub_menu_id,menu_name) {
    var submenu = document.getElementById(sub_menu_id);
    if (submenu.style.height != "initial") {
        submenu.style.height = "initial";  
        btn.innerHTML = menu_name + "&nbsp;<i class='fa fa-caret-up'></i>";
    }
    else {
        submenu.style.height = "0px";
        btn.innerHTML = menu_name + "&nbsp;<i class='fa fa-caret-down'></i>";
    }
}
function InputSearchDisplay() {
    var inputSearch = document.getElementsByClassName("search-form-container")[0];
    if (inputSearch != null)
    {
        inputSearch.style.display = "block";
        var nav = document.getElementsByTagName("header");
        nav[0].style.display = "none";
    }
}
//scroll back to top
var topbutton = document.getElementById("topBtn")

window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 350 || document.documentElement.scrollTop > 350) {
        topbutton.style.display = "block";
    }
    else {
        topbutton.style.display = "none";
    }
}

function topFunction() {
    document.body.scrollIntoView({
        behavior: "smooth",
    });
    document.documentElement.scrollIntoView({
        behavior: "smooth",
    });
}

//Spinner
function startOrStop_Spinner(btn,spinner_id) {
    btn.style.display = "none";
    var spinner = document.getElementById(spinner_id);
    
    if (spinner.classList.contains("spinner-animation")) {
        spinner.style.display = "none";
        spinner.classList.remove("spinner-animation");
    }
    else {
        spinner.style.display = "inline-block";
        spinner.classList.add("spinner-animation");
    }
}
