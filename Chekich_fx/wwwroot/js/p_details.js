showSizesRem();
function showSizesRem() {
    var sizeInput = document.querySelector("input:checked");
    var quantity = parseInt(sizeInput.getAttribute("data-size-quantity"));
    var showRemElement = document.getElementById("remaining-items");
    showRemElement.innerText = "In Store";
    if (quantity < 5) {
        showRemElement.innerText ="only "+quantity+" remaining!!";
    }  
}
