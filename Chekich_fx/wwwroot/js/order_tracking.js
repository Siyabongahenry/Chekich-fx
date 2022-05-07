function sendRequest(method, actionName, value = null) {
    var token = document.getElementById("Verification-Token").value;
    actionRequested = actionName;
    var promise = new Promise(function (resolve, reject) {
        const xhttp = new XMLHttpRequest();
        xhttp.open(method, "/OrderTracking/" + actionName, true);
        xhttp.onload = function () {
            if (this.readyState == 4 && this.status == 200) {
                if (this.responseText == "OrderNotFound" || this.responseText == "DeliveryNotFound" || this.responseText == "CollectionNotFound") {
                    reject("We couldn't find what you're looking for.");
                }
                else {
                    resolve(JSON.parse(this.responseText));
                }
            }
            else if (this.status >= 400 && this.status < 500) {
                reject("Something went wrong, please check your connections");
            }
            else if (this.status >= 500) {
                reject("Something went wrong with our servers, please try again later");
            }

        }
        xhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xhttp.setRequestHeader("Validation-Token", token);
        if (value != null) {
            xhttp.send(value);
        }
        else {
            xhttp.send();
        }
    });
    return promise;

}
async function GetDelivery(btn, orderId) {
    startOrStop_Spinner(btn, "spinner-" + orderId)
    var addressType = document.getElementById("addressType-" + orderId);
    var streetName = document.getElementById("StreetName-" + orderId);
    var townOrCity = document.getElementById("TownOrCity-" + orderId);
    var townOrCityCode = document.getElementById("Code-" + orderId);
    var dateType = document.getElementById("dateType-" + orderId);
    var date = document.getElementById("date-" + orderId);
    await sendRequest("get", "Delivery?_orderId=" + orderId)
        .then(function (delivery) {
            startOrStop_Spinner(btn, "spinner-" + orderId)
            addressType.innerHTML = "<b>Delivery Address</b>"
            streetName.innerText = delivery.Address.HouseNumber + " " + delivery.Address.StreetName;
            townOrCity.innerText = delivery.Address.TownOrCity;
            townOrCityCode.innerText = delivery.Address.Code;
            dateType.innerHTML = "<b>Delivery Date</b>"
            date.innerText = delivery.DateTime.substring(0,10);

            console.log(delivery);
        })
        .catch(function (errorMsg) {
            startOrStop_Spinner(btn, "spinner-" + orderId)
            var errorBlock = document.getElementById("error-" + orderId);
            errorBlock.innerText = errorMsg;
            errorBlock.classList.remove("d-none");
        });
}
async function GetCollecion(btn,orderId) {
    startOrStop_Spinner(btn, "spinner-" + orderId);
    var addressType = document.getElementById("addressType-" + orderId);
    var streetName = document.getElementById("StreetName-" + orderId);
    var townOrCity = document.getElementById("TownOrCity-" + orderId);
    var townOrCityCode = document.getElementById("Code-" + orderId);
    var dateType = document.getElementById("dateType-" + orderId);
    var date = document.getElementById("date-" + orderId);
    await sendRequest("get", "Collection?_orderId=" + orderId)
        .then(function (collection) {
            startOrStop_Spinner(btn, "spinner-" + orderId);
            addressType.innerHTML = "<b>Collection Address</b>";
            streetName.innerText = collection.Address.HouseNumber + " " + collection.Address.StreetName;
            townOrCity.innerText = collection.Address.TownOrCity;
            townOrCityCode.innerText = collection.Address.Code;
            dateType.innerHTML = "<b>Collection Date</b>"
            date.innerText = collection.DateTime;
        })
        .catch(function (errorMsg) {
            startOrStop_Spinner(btn, "spinner-" + orderId)
            var errorBlock = document.getElementById("error-" + orderId);
            errorBlock.innerText = errorMsg;
            errorBlock.classList.remove("d-none");
        });
}
