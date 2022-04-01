function FilterByCategory() {
    var elements = document.getElementsByTagName("show-product");
    for (i = 0; i < elements.length; i++) {
        if (elements.innerHTML != 'beauty') {
            elements[i].style.display = "none";
        }
    }
}