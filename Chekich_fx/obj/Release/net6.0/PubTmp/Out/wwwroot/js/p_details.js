showSizesRem();
function showSizesRem() {
    var sizeInput = document.querySelector("input:checked");
    var quantity = parseInt(sizeInput.getAttribute("data-size-quantity"));
    var showRemElement = document.getElementById("remItem");
    showRemElement.innerText = "";
    if (quantity < 5) {
        showRemElement.innerText = quantity + " remaining" + "(size: " + sizeInput.value+")";
    }  
}
