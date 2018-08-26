"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveEmployee", function (firstName, lastName, email, phoneNumber) {
    var table = document.getElementById("employeeTable");

    var tr = table.insertRow(0);

    var fnrow = tr.insertCell(0);
    fnrow.innerHtml = firstName;

    var lnrow = tr.insertCell(1);
    lnrow.innerHtml = lastName;

    var prow = tr.insertCell(2);
    prow.innerHtml = "pesho";

    var erow = tr.insertCell(3);
    erow.innerHtml = email;

    var pnrow = tr.insertCell(4);
    pnrow.innerHtml = phoneNumber;
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var firstName = document.getElementById("firstName").value;
    var lastName = document.getElementById("lastName").value;
    var email = document.getElementById("email").value;
    var phoneNumber = document.getElementById("phoneNumber").value;
    connection.invoke("SendEmployee", firstName, lastName, email, phoneNumber).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

//"use strict";

//var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//connection.on("ReceiveMessage", function (user, message, something) {
//    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
//    var encodedMsg = user + " says " + msg + " nope " + something;
//    var li = document.createElement("li");
//    li.textContent = encodedMsg;
//    document.getElementById("messagesList").appendChild(li);
//});

//connection.start().catch(function (err) {
//    return console.error(err.toString());
//});

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    var something = document.getElementById("something").value;
//    connection.invoke("SendMessage", user, message, something).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});