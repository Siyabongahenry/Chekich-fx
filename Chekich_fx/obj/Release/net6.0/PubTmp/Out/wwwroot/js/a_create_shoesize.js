var responseBoxElement = document.getElementById("responseBox");

function sendRequest(method, actionName, value = null) {
    var promise = new Promise(function (resolve, reject) {
        var token = document.getElementById("Verification-Token").value;
        const xhttp = new XMLHttpRequest();
        xhttp.open(method,"/Admin/ShoeSize/"+actionName, true);
        xhttp.onload = function () {
            if (this.readyState == 4 && this.status == 200) {
                if (actionName == "Create") {
                    if (this.responseText == "Inserted") {
                        resolve({ text: "The size has been successfully added.", method: "insert" });
                    }
                    else if (this.responseText == "Updated") {
                        resolve({ text: "Successfully updated.", method: "update" });
                    }
                    else if (this.responseText == "Exist") {
                        reject("This size already exist, please reload the website or update quantity.");
                    }
                    else if (this.responseText == "Rejected") {
                        reject("Something might be wrong with your input, please check your input or contact admitrator.");
                    }
                    else if (this.responseText == "Error") {
                        reject("Something went wrong please try again later.");
                    }
                   
                }
                else {
                    if (this.responseText == "Removed") {
                        resolve("Successfully removed.");
                    }
                    else if (this.responseText == "NotFound") {
                        reject("The size you're trying to delete does not exist.");
                    }
                    else if (this.responseText == "Rejected") {
                        reject("Something might be wrong with your input, please check your input or contact admitrator.");
                    }
                    else {
                        reject("Something went wrong please try again later or contact administrator.");
                    }
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
async function add(btn) {
    var quantityInput = document.querySelector("input[name='Quantity']");
    var sizeInput = document.querySelector("input[name='Size']");
    var shoeIdInput = document.querySelector("input[name='ShoeId']");


    await sendRequest("Post", "Create",
        "Size=" + sizeInput.value + "&Quantity=" + quantityInput.value + "&ShoeId=" + shoeIdInput.value)
        .then(function (obj) {

            if (obj.method == "insert") {    
                addRow(sizeInput.value, quantityInput.value,shoeIdInput.value);
                totalItems(quantityInput.value);        
            }
            else {
                document.getElementById(sizeInput.value + "-quantity").innerText = quantityInput.value;
                sizeInput.disabled = "";
                document.getElementById("SubmitBtn").innerText = "Add";
            }
            quantityInput.value = "";
            sizeInput.value = "";
            setResponseBox("success", obj.text)
        })
        .catch(function (responseText) {
           
            setResponseBox("error", responseText);
        });
}
function addRow(size,quantity,shoeId) {
    var sizesContainer = document.querySelector("#sizes-table tbody");
    var newSizeElement = document.createElement("tr");
    newSizeElement.setAttribute("id", size);
    newSizeElement.innerHTML = "<td>" + size + "</td>"
        + "<td class='quantity' id='" + size + "-quantity' onclick ='allowUpdate(" + size + "," + quantity + ")'>"
        + quantity + "</td>";
    newSizeElement.innerHTML += "<td onclick='removeSize("+shoeId+","+size+")'><i class='fa fa-trash'></i></td>";
    sizesContainer.appendChild(newSizeElement);
}

function totalItems(quantity) {
    var totalItemsElement = document.getElementById("TotalItems");
    var total = parseInt(totalItemsElement.innerText);
    total += parseInt(quantity);
    totalItemsElement.innerText = total;
}

function allowUpdate(size,quantity) {
    var sizeInput = document.querySelector("input[name='Size']");
    sizeInput.value = size;
    sizeInput.disabled = "true";
    var quantityInput = document.querySelector("input[name='Quantity']");
    quantityInput.value = quantity;
    document.getElementById("SubmitBtn").innerText = "Update";
}
async function removeSize(shoeId,size) {
    await sendRequest("Post", "Remove","ShoeId="+shoeId+"&Size="+size+"&Quantity=0")
        .then(function (responseText) {
            document.getElementById(size).remove();
            setResponseBox("success", responseText);
        })
        .catch(function (responseText) {
            setResponseBox("error", responseText);
        });
}

function setResponseBox(type,text) {
    addResponseBox(type, text);
    setTimeout(() => {
        removeResponseBox(type);
    }, 10000)
}
function addResponseBox(type,text) {
    responseBoxElement.classList.add(type);
    responseBoxElement.classList.remove("d-none");
    responseBoxElement.innerHTML = text;
}
function removeResponseBox(type) {
    responseBoxElement.innerHTML = "";
    responseBoxElement.classList.remove(type);
    responseBoxElement.classList.add("d-none");
}