function enableBtn(input) {
    var btn = document.getElementById(input);
    btn.disabled = false;
}
function sendRequest(method, actionName, value = null) {
    var token = document.getElementById("Verification-Token").value;

    var promise = new Promise(function (resolve, reject) {

        const xhttp = new XMLHttpRequest();
        xhttp.open(method, "/OrderManager/" + actionName, true);
        xhttp.onload = function () {
            if (this.readyState == 4 && this.status == 200) {
                resolve(this.responseText);
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
async function listAddresses() {
    var selectedAddress = document.querySelector("input[name='_receivalType']:checked").value;
  
    await sendRequest("get", "GetAddresses?AddressType=" + selectedAddress)
        .then(function (addressJson) {
            createAddressBlock(JSON.parse(addressJson));
        })
        .catch(function () {

        });
    
}
function createAddressBlock(address) {
    var div = document.createElement("div");
    document.getElementById("receivalType-container").classList.add("d-none");
    document.getElementById("address-container").classList.remove("d-none");


}
