var successMessage = "Updated successfully " + "<i class='float-right fa fa-check'></i>";
var errorMessage = "Couldn't update " + "<i class='float-right fa fa-times'></i>";

function sendRequest(method, actionName, value = null) {

    var token = document.getElementById("Verification-Token").value;

    var promise = new Promise(function (resolve, reject) {

        const xhttp = new XMLHttpRequest();
        xhttp.open(method,"/Profile/"+actionName, true);
        xhttp.onload = function () {
            if (this.readyState == 4 && this.status == 200) {
                resolve();
            }

        }
        xhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
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
function showEditBlock(inputName) {
    var d_block = document.getElementById(inputName+"DisplayBlock");
    var e_block = document.getElementById(inputName+"EditBlock");

    d_block.classList.add("d-none");
    e_block.classList.remove("d-none");
    var input = document.getElementById(inputName + "Input");
    input.focus();
    value = input.value;
    input.value = '';
    input.value = value;
   

}
function hideEditBlock(inputName) {
    var d_block = document.getElementById(inputName+"DisplayBlock");
    var e_block = document.getElementById(inputName+"EditBlock");

    d_block.classList.remove("d-none");
    e_block.classList.add("d-none");
}

async function saveInput(inputName) {
    var value = document.getElementById(inputName+"Input").value;
    await sendRequest("Post", "Replace"+inputName,"new"+inputName+"="+ value)
        .then(function () {
            var label = document.getElementById(inputName + "Label");
            label.innerText = value;
            hideEditBlock(inputName);
            var successBlock = document.getElementById(inputName + "Success");
            if (inputName = "Email") {
                successMessage ="Please check your emails!!"
            }
            successBlock.innerHTML = successMessage;
            successBlock.classList.remove("d-none");
            setTimeout(() => {     
                successBlock.classList.add("d-none");
                successBlock.innerHtml = "";
            }, 5000)
        })
        .catch(function () {
            var errorBlock = document.getElementById(inputName + "Error");
            errorBlock.innerHTML = errorMessage;
            errorBlock.classList.remove("d-none");
            setTimeout(() => {
                errorBlock.innerHtml = "";
                errorBlock.classList.add("d-none");
            }, 2000)
        });
}
