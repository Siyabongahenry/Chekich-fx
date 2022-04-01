"use strict";
document.getElementById("message-link").innerText = "0";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//document.getElementById("sendButton").disabled = true;
connection.on("ReceiveMessage", function (sender_id, message,chat_screen_id) {
    message = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    addChatToContainer(message, "received-chat", chat_screen_id)
    var small_chat = document.getElementById("small-chat-" + chat_screen_id);
    small_chat.innerHTML = message;

    var not_count = document.getElementById("not-count-" + chat_screen_id);
    var not_countValue =parseInt(not_count.innerHTML);
    not_countValue = not_countValue + 1;
    not_count.innerHTML = not_countValue;
    var message_link = document.getElementById("message-link");
    message_link.innerText = parseInt(message_link.innerText) + 1;
});
//Alerting a receiver that a sender is typing
connection.on("ReceiveTypingAlert", function (screen_id)
{
    var icon = document.createElement("i");
    icon.classList.add("fa", "fa-smile");
    var typingAlertBox = document.getElementById("typing-alert-" + screen_id);
   var small_typingAlert = document.getElementById("small-chat-" + screen_id)
   var small_chatValue = small_typingAlert.innerHTML;
    if (typingAlertBox.innerHTML !="typing..") {
        typingAlertBox.innerHTML = "typing..";
       small_typingAlert.style.color = "green";
        small_typingAlert.innerHTML = "typing..";

       setTimeout(() => {
            typingAlertBox.innerHTML = "";
            typingAlertBox.appendChild(icon);
          small_typingAlert.style.color = "black";
          small_typingAlert.innerHTML = small_chatValue;
        }, 2000);
    } 
    
});

connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});

function scrollToLastChat() {
    var scrollMain = document.getElementsByClassName("chat-main-container")[0];
    var scrollValue = scrollMain.scrollHeight - scrollMain.clientHeight;
    scrollMain.scrollTo({ top: scrollValue, behavior: "smooth" });
}
function addChatToContainer(message, chat_class_style, chat_screen_id, chatSent) {

    var chatContainer = document.getElementById("chat-container-"+chat_screen_id);

    var chat = document.createElement("div");
    chat.innerHTML = message;
    chat.classList.add(chat_class_style);

    var timespan = document.createElement("span");
    timespan.classList.add("timespan");
    var date = new Date();
    timespan.innerHTML = date.getHours()+":"+date.getMinutes();
    
    //create a box to put chat inside
    var block = document.createElement("div");
  
    //Add a check in the chat block to show if message is received/Not (for sent chat)
    if (chatSent !== undefined) {
   
        var mark = document.createElement("i");

        if (chatSent) {
            mark.classList.add("fa", "fa-check-double");
            mark.style.color = "#1b6ec2";
        }
        else {
            mark.classList.add("fa", "fa-check");
            mark.style.color = "silver";
        }
        timespan.appendChild(mark);
    }
    chat.appendChild(timespan);
    block.appendChild(chat); 
    chatContainer.appendChild(block);

    scrollToLastChat();
}
function SendMessage(chat_screen_Id)
{
    
    var sender = document.getElementById("senderId-"+chat_screen_Id).value;
    var receiver = document.getElementById("receiverId-"+chat_screen_Id).value;
    var messageTextarea = document.getElementById("messageInput-"+chat_screen_Id);
    var message = messageTextarea.value;

    messageTextarea.value = ""; 
    messageTextarea.style.height = "initial";

    event.preventDefault();
    //Save Chat To Database
    var IsSent = false;
    try {
        connection.invoke("SendMessage", receiver, sender, message,chat_screen_Id);
        addChatToContainer(message, "sent-chat",chat_screen_Id,true);
        IsSent = true;
    }
    catch (err) {
        addChatToContainer("Sorry, we couldn't connect you \n for immediate chat.", "sent-chat", chat_screen_Id, false);
       
    }
    saveChatToDbRequest(sender, receiver, message,
        (IsSaved) => {
            if (!IsSaved && !IsSent) {
                setTimeout(() => {
                    addChatToContainer("We couldn't connect you,\nplease try again later.", chat_screen_Id, false);
                }, 3000);   
            }
            else if (IsSent && IsSaved) {
                setTimeout(() => {
                    addChatToContainer("We'll be in touched with you soon.", "received-chat", chat_screen_Id,false);
                    setTimeout(() => {
                        addChatToContainer("We apologize for taking this long,"+"\n we're currently on heavy duty.", "received-chat", chat_screen_Id,false);
                    }, 10000);
                },3000);
                
            }
            else if (IsSent && !IsSaved) {
                setTimeout(() => {
                    addChatToContainer("We couldn't save your message,\n but we might respond," +
                        "\nmake sure you don't close your connections,\n" +
                        " or you can try again later."
                        + "received-chat", chat_screen_Id, true);
                },3000);
            }
            else if (!IsSent && IsSaved) {
                setTimeout(addChatToContainer("We couldn't connect you for instant response,\n "
                    + "it might take us to respond", "received-chat", chat_screen_Id, true),3000);
            }
        }
        , chat_screen_Id);

}

function growTextArea(element, id) {
    var inputBox = document.getElementById("chat-input-"+id);
    element.style.height = "auto";
    element.style.height = element.scrollHeight + "px";
    inputBox.style.height = "auto";
    inputBox.style.height = inputBox.scrollHeight + "px";
    inputBox.scrollTo = inputBox.scrollHeight;

    scrollToLastChat();

    sendTypingAlert(id);
}

function sendTypingAlert(id) {
    var receiver = document.getElementById("receiverId-"+id).value;
    try {
          connection.invoke("UserTypingAlert",receiver,id)
    }
    catch (err) {
        addChatToContainer("We are unable to connect you..", "text-danger",id);
    }
}

//Saving chat to database

function saveChatToDbRequest(senderId, receiverId, message, IsSaved) {
    var token = document.getElementById("Verification-Token").value;
    const xhttp = new XMLHttpRequest();
   
    xhttp.open("POST", "/Chat/SaveChat", true);
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            IsSaved(this.responseText == "true");
        }
        
    }
    xhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xhttp.setRequestHeader("Validation-Token", token);
    xhttp.send("SenderId=" + senderId + "&ReceiverId=" + receiverId + "&Message=" + message);

}

function openCloseChat(openChat, id) {
    var chat_screen = document.getElementById("a-screen-" + id);
    var chat_screens = document.getElementsByClassName("a-screen");
    var viewed_screen = document.getElementsByClassName("a-chat");
    for (let i = 0; i < viewed_screen.length; i++) {
        if (chat_screens[i].classList.contains("d-block")) {
            chat_screens[i].classList.replace("d-block", "d-none");
        }
        viewed_screen[i].classList.replace("bg-theme-image","bg-theme");
    }
  
    openChat.classList.add("bg-theme-image");
    chat_screen.classList.replace("d-none", "d-block");
   
}
function openUserChatScreen(screenId) {
   
    var chatScreen = document.getElementById(screenId);
    if (chatScreen.style.display != "none") {
        chatScreen.style.display = "none";
    }
    else {
        chatScreen.style.display = "block";  
    }

}
function closeChatScreen(screenId) {
    var chatScreen = document.getElementById(screenId);
    chatScreen.style.display = "none";
}