var token = document.getElementById("Verification-Token").value;
//size
var btnInc = document.querySelectorAll(".size-container .btn-increment");
var btnDec = document.querySelectorAll(".size-container .btn-decrement");
var sizeGroup = document.getElementsByClassName("size-group");
var sizeInput = document.getElementsByClassName("size-input");
incSizeOnClick();
decSizeOnClick();
function incSizeOnClick() {
    console.log("clicked");
    for (let i = 0; i < btnInc.length; i++)
    {
        btnInc[i].addEventListener("click", () => {
            sizes = sizeGroup[i].getElementsByClassName("size");
            let sizeMarginTop = parseInt(sizes[0].style.marginTop.replace("px", ""));
            if (sizeMarginTop > -1*(30*sizes.length - 30) )
            {
                sizeMarginTop = sizeMarginTop - 30;
                sizes[0].style.marginTop = sizeMarginTop + "px";
                sizeInput[i].value = sizes[Math.abs(sizeMarginTop / 30)].innerText.replace("s ", "");
            }
        });
    }
}
function decSizeOnClick() {
    for (let i = 0; i < btnInc.length; i++) {
        btnDec[i].addEventListener("click", () => {
            sizes = sizeGroup[i].getElementsByClassName("size");
            let sizeMarginTop = parseInt(sizes[0].style.marginTop.replace("px", ""));
            if (sizeMarginTop < 0) {
                sizeMarginTop = sizeMarginTop + 30;
                sizes[0].style.marginTop = sizeMarginTop + "px";
                sizeInput[i].value = sizes[Math.abs(sizeMarginTop / 30)].innerText.replace("s ", "");
            }
        });
    }
}

//add item to cart
var btnCart = document.getElementsByClassName("btn-cart");
var btnFav = document.getElementsByClassName("btn-favourite");

async function postData(url = "", data = {})
{
    const response = await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded",
            "Validation-Token":token
        },
        body:encodeObj(data)
    });
    return response.text();
}

function encodeObj(obj = {}) {
    let encodedString = "";
    for (let item in obj) {
        encodedString += item+"="+obj[item] + "&";
    }
    return encodedString.slice(0,encodedString.length-1);
}

var cartCounter = document.getElementById("cart-counter");
var favCounter = document.getElementById("fav-counter");
var itemId = document.getElementsByClassName("item-id");
addItemToCartorFavourite();
function addItemToCartorFavourite() {
    for (let i = 0; i < btnCart.length; i++) {
        btnCart[i].addEventListener("click", () => {
            disableBtnOnProcess(btnCart[i]);
            postData("/Store/AddItemToCart", {"itemId":itemId[i].value,"size":sizeInput[i].value})
                .then((response) => {
                    if (response != "failed") {
                        btnCart[i].innerHTML += "<i class='click-effect'>+1</i>";
                        //cart counter
                        let cartCountNewValue = parseInt(cartCounter.innerText) + 1;
                        cartCounter.innerText = cartCountNewValue;
                    }
                    enableBtnAfterProcess(btnCart[i]);

                }).catch(() => {
                    enableBtnAfterProcess(btnCart[i]);
                    console.log("Something went wrong");
                });
        });

        btnFav[i].addEventListener("click", () => {
            disableBtnOnProcess(btnFav[i]);
            postData("/Favourite/Add", { "ShoeId": itemId[i].value})
                .then((response) => {
                    //cart counter
                    if (response != "failed") {
                        btnFav[i].innerHTML += "<i class='click-effect fa fa-heart'></i>";
                        favCounter.innerText = parseInt(favCounter.innerText) + 1;
                    }
                    else {
                        btnFav[i].innerHTML += "<i class='click-effect'>exist</i>";
                    }
                    enableBtnAfterProcess(btnFav[i]);

                }).catch(() => {
                    enableBtnAfterProcess(btnFav[i]);
                    console.log("Something went wrong");
                });
        });
    }
}
function disableBtnOnProcess(btn) {
    btn.disabled = true;
    btn.classList.remove("btn-store-hover");
    btn.classList.add("busy-indicator");
}
function enableBtnAfterProcess(btn) {
    btn.disabled = false;
    btn.classList.add("btn-store-hover");
    btn.classList.remove("busy-indicator");
}