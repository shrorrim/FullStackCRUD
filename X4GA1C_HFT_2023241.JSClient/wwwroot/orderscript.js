let orders = [];
let connection = null;

let orderIdToUpdate = -1;

getData();
setUpSignalR();

function setUpSignalR() {

    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:27376/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();


    connection.on("OrderUpdated", (user, message) => {
        //console.log(user);
        //console.log(message);
        getData();
    });

    connection.on("OrderDeleted", (user, message) => {
        //console.log(user);
        //console.log(message);
        getData();
    });

    connection.on("OrderCreated", (user, message) => {
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

    if (orders.length != 0) {
        orders = [];
    }

    await fetch('http://localhost:27376/order')
        .then(x => x.json())
        .then(y => {

            orders = y;
            display();
        });
}



function updateOrder() {

    //handle HTTP PUT request (input forms)

    let date = document.getElementById('dateToUpdate').value;
    let laptopId = document.getElementById('laptopIdToUpdate').value;
    let ordererId = document.getElementById('ordererIdToUpdate').value;


    fetch('http://localhost:27376/order', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                id: orderIdToUpdate,
                date: date,
                laptopId: laptopId,
                ordererId: ordererId
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

    fetch('http://localhost:27376/order/' + id, {
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

    orders.forEach(x => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + x.id + "</td><td>" + x.date + "</td>"
            + "<td>" + `<button type="button" onclick="remove(${x.id})">Delete</button>`
            + `<button type="button" onclick="showUpdateFroms(${x.id})">Update</button>`
            + "</td></tr>";
    });
}


function showUpdateFroms(id) {

    document.getElementById('orderUpdateFormDiv').style.display = 'flex';
    //flex-direction: column;
    document.getElementById('orderUpdateFormDiv').style.flexDirection = 'column';

    //bele kell írni a fromsba a kiválasztott adatait!

    let selectedOrder = orders.find(t => t['id'] == id);

    document.getElementById('dateToUpdate').value =
        selectedOrder['date'];

    document.getElementById('laptopIdToUpdate').value =
        selectedOrder['laptopId'];

    document.getElementById('ordererIdToUpdate').value =
        selectedOrder['ordererId'];


    orderIdToUpdate = id; // globális változóba

}

function createOrder() {

    let date = document.getElementById('date').value;
    let laptopId = document.getElementById('laptopId').value;
    let ordererId = document.getElementById('ordererId').value;

    fetch('http://localhost:27376/order', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                date: date,
                laptopId: laptopId,
                ordererId: ordererId
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