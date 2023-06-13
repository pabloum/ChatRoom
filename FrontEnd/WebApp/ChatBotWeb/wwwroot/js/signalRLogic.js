
var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:2701/chatroom").build();

connection.start().then(() => {
    connection.invoke("AddToGroup", "@Model.Room.RoomId")
}).catch(() => console.error(e));

document.getElementById("submitMessage").addEventListener("click", (event) => {

    let room = "@Model.Room.RoomId";
    let user = "@Model.User.FindFirst(Username)";
    let messagepromt = document.getElementById("inputMessage").value

    connection.invoke("SendMessage", room, user, messagepromt).catch((e) => {
        console.error(e.toString());
    });

    document.getElementById("inputMessage").value = "";
    document.getElementById("inputMessage").focus();

    event.preventDefault();
});


connection.on("ReceiveMessage", (user, message) => {
    console.log("ohhhhh" + user + message);
});