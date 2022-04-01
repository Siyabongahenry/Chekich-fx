var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveMessage", function (sender_id, message, chat_screen_id) {
    var message_link = document.getElementById("message-link");
    message_link.innerText = parseInt(message_link.innerText) + 1;
});

connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});