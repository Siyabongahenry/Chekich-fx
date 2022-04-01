var table = document.getElementById("Users-Emails");
var actionRequested = "";
function sendRequest(method, actionName, value = null) {
    var token = document.getElementById("Verification-Token").value;
    actionRequested = actionName;
    var promise = new Promise(function (resolve, reject) {
        const xhttp = new XMLHttpRequest();
        xhttp.open(method, "/Admin/Users/" + actionName, true);
        xhttp.onload = function () {
            if (this.readyState == 4 && this.status == 200) {
                if (actionRequested == "AddUserToRole") {
                    if (this.responseText == "Enrolled") {
                        resolve("Successfully added in admin role.");
                    }
                    else if (this.responseText == "NotFound") {
                        reject("This user is not registered, the user should register in first.")
                    }
                    else if (this.responseText == "Error") {
                        reject("Something went wrong, Please try again after sometime, or contact the system administrator.");
                    }
                    else {
                        reject("This user already exist in the role.");
                    }
                }
                else if (actionRequested =="RemoveUserFromRole") {
                    if (this.responseText == "Removed") {
                        resolve("Successfully removed from Admin role.");
                    }
                    else if (this.responseText == "NotFound") {
                        reject("This user does not exist in the role.");
                    }
                    else {
                        reject("Something went wrong, please try again later of contact administrator.")
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
async function addUserToRole() {
    var input = document.getElementById("Email");
    var email = input.value;
    await sendRequest("Post", "AddUserToRole", "email="+email)
        .then(function (responseText) {
            createNewRow(email, "Admin");
            responseBox("addResponseTextBox", "success", responseText);
            input.value = '';
        })
        .catch(function (responseText) {
            
            responseBox("addResponseTextBox", "error", responseText)
        });
}
function createNewRow(email, input_2) {
    var row = document.createElement("tr");
    row.setAttribute("id", "row-" + email);
    var td_1 = document.createElement("td");
    td_1.innerText = email;
    var td_2 = document.createElement("td");
    td_2.innerText = input_2;
    var td_3 = document.createElement("td");
    var delIcon = document.createElement("i");
    delIcon.classList.add("fa", "fa-trash");
    delIcon.addEventListener("click", function () { removeUserFromRole('row-' + email, email) });
    td_3.appendChild(delIcon);
    row.appendChild(td_1);
    row.appendChild(td_2);
    row.appendChild(td_3);
    table.appendChild(row);
}
async function removeUserFromRole(row, email) {
    if (confirm("Are you sure, you want to remove the user from the admin role?")) {
        await sendRequest("Post", "RemoveUserFromRole", "email=" + email)
            .then(function (responseText) {
                var rowToRemove = document.getElementById(row);
                console.log(rowToRemove);
                rowToRemove.remove();
                responseBox("removeResponseTextBox", "success", responseText);
            })
            .catch(function (responseText) {
                responseBox("removeResponseTextBox", "error", responseText);
            });
    }
}
function responseBox(id, type,message) {
    var responseTextBox = document.getElementById(id);
    responseTextBox.innerText = message;
    responseTextBox.classList.add(type);
    responseTextBox.classList.remove("d-none");
    setTimeout(() => {
        responseTextBox.classList.remove(type);
        responseTextBox.classList.add("d-none");
    }, 6000);
}