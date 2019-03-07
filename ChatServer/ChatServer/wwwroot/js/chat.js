"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub",
		{
			skipNegotiation: true,
			transport: signalR.HttpTransportType.LongPolling | signalR.HttpTransportType.WebSockets,
			headers: { userId: "BDA03A42-4CED-4AFE-A142-887AFEBCF45C"}
		})
	.build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (message) {
	var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
	var encodedMsg = msg;
	var li = document.createElement("li");
	li.textContent = encodedMsg;
	document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
	document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
	return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
	var user = document.getElementById("userInput").value;
	var message = document.getElementById("messageInput").value;
	connection.invoke("SendMessage",
		{
			messageContent: message , 
			userId: user,
			conversationId: "5C0FD2D3-84C2-4E07-9280-FCC92EF0512A",
			userConversationId: "85B5BDAB-3D5D-46AC-BC36-6B44B2CF1AF0"
		}).catch(function (err) {
		return console.error(err.toString());
	});
	event.preventDefault();
});