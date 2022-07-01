//quantity setting
var btnIncQuantity = document.getElementsByClassName("btn-inc-quantity");
var btnDecQuantity = document.getElementsByClassName("btn-dec-quantity");
var quantyValue = document.getElementsByClassName("quantity-value");
var quantityInput = document.getElementsByClassName("quantity-input");
var btnUpdateChanges = document.getElementsByClassName("btn-update-changes");
//increase quanty
setQuantity();
function setQuantity() {
    for (let i = 0; i < btnIncQuantity.length; i++)
    {
        //increase quantity
        btnIncQuantity[i].addEventListener("click", () => {
            let value = parseInt(quantyValue[i].innerText) + 1;
            quantyValue[i].innerText = value;
            quantityInput[i].value = value;

            btnUpdateChanges[i].classList.remove("d-none");
        });
        //decrease quantity
        btnDecQuantity[i].addEventListener("click", () => {
            let value = parseInt(quantyValue[i].innerText);
            value = value > 1 ? value - 1:value;
            quantyValue[i].innerText = value;
            quantityInput[i].value = value;

            btnUpdateChanges[i].classList.remove("d-none");
        });
        //hide btn update changes
        btnUpdateChanges[i].addEventListener("click", () => {
            btnUpdateChanges[i].classList.add("d-none");
        });
    }
}


