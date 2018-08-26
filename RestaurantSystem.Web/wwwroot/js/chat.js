"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveEmployee", function (firstName, lastName, email, phoneNumber) {

    var tr = document.createElement("tr");

    var fnrow = document.createElement("td");
    fnrow.textContent = firstName;

    var lnrow = document.createElement("td");
    lnrow.textContent = lastName;

    var prow = document.createElement("td");
    prow.textContent = "pesho";

    var erow = document.createElement("td");
    erow.textContent = email;

    var pnrow = document.createElement("td");
    pnrow.textContent = phoneNumber;

    var pesho = document.createElement("td");

    tr.appendChild(fnrow);
    tr.appendChild(lnrow);
    tr.appendChild(prow);
    tr.appendChild(erow);
    tr.appendChild(pnrow);
    tr.appendChild(pesho);

    document.getElementById("employeeTable").appendChild(tr);
    
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