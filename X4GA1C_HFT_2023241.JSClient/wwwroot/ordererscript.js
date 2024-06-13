let orderers = [];
let connection = null;

let ordererIdToUpdate = -1;

getData();
setUpSignalR();

function setUpSignalR() {

    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:27376/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();


    connection.on("OrdererUpdated", (user, message) => {
        //console.log(user);
        //console.log(message);
        getData();
    });

    connection.on("OrdererDeleted", (user, message) => {
        //console.log(user);
        //console.log(message);
        getData();
    });

    connection.on("OrdererCreated", (user, message) => {
        //console.log(user);
        //console.log(message);
        getData();
    });


    connection.onclose(async () => {
        await startSignalRConnection();
    });
    startSignalRConnection();

}


async function startSignalRConnection() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};


async function getData() {

    if (orderers.length != 0) {
        orderers = [];
    }

    await fetch('http://localhost:27376/orderer')
        .then(x => x.json())
        .then(y => {

            orderers = y;
            display();
        });
}



function updateOrderer() {

    //handle HTTP PUT request (input forms)

    let name = document.getElementById('nameToUpdate').value;
    let phoneNumber = document.getElementById('phoneNumberToUpdate').value;


    fetch('http://localhost:27376/orderer', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                id: ordererIdToUpdate,
                name: name,
                phoneNumber: phoneNumber
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

}

function remove(id) {

    fetch('http://localhost:27376/orderer/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

}



function display() {

    document.getElementById('resultarea').innerHTML = "";

    orderers.forEach(x => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + x.id + "</td><td>" + x.name + "</td>"
            + "<td>" + `<button type="button" onclick="remove(${x.id})">Delete</button>`
            + `<button type="button" onclick="showUpdateFroms(${x.id})">Update</button>`
            + "</td></tr>";
    });
}


function showUpdateFroms(id) {

    document.getElementById('orderersUpdateFormDiv').style.display = 'flex';
    //flex-direction: column;
    document.getElementById('orderersUpdateFormDiv').style.flexDirection = 'column';

    //bele kell írni a fromsba a kiválasztott adatait!

    let selectedOrderer = orderers.find(t => t['id'] == id);

    document.getElementById('nameToUpdate').value =
        selectedOrderer['name'];

    document.getElementById('phoneNumberToUpdate').value =
        selectedOrderer['phoneNumber'];


    ordererIdToUpdate = id; // globális változóba

}

function createOrderer() {

    let name = document.getElementById('name').value;
    let phoneNumber = document.getElementById('phoneNumber').value;

    fetch('http://localhost:27376/orderer', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: name,
                phoneNumber: phoneNumber
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });


}