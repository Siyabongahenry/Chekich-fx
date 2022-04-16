function sendRequest(method, actionName, value = null) {
    var token = document.getElementById("Verification-Token").value;
    actionRequested = actionName;
    var promise = new Promise(function (resolve, reject) {
        const xhttp = new XMLHttpRequest();
        xhttp.open(method, "/OrderTracking/" + actionName, true);
        xhttp.onload = function () {
            if (this.readyState == 4 && this.status == 200) {
                if (this.responseText = "OrderNotFound" || this.responseText == "DeliveryNotFound" || this.responseText == "CollectionNotFound") {
                    reject(this.responseText);
                }
                else {
                    resolve(this.responseText);
                }
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
async function GetDelivery(orderId) {
    var streetName = documents.getElementById("streetName-" + orderId);
    var cityOrTownCode = documents.getElementById("code-" + orderId);
    await sendRequest("get", "Delivery?_orderId=" + orderId)
        .then(function (delivery) {
            streetName.innerText = delivery.HouseNumber +" "+ delivery.StreetName;
            cityOrTownCode.innerText = delivery.Code;
        })
        .catch(function () {

        });
}
async function GetCollecion(orderId) {

    await sendRequest("get", "Collection?_orderId=" + orderId)
        .then(function (delivery) {
            console.log(delivery);
        })
        .catch(function () {

        });
}