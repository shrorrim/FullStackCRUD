let brands = [];
let connection = null;

let brandIdToUpdate = -1;

getData();
setUpSignalR();

function setUpSignalR() {

    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:27376/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();


    connection.on("BrandUpdated", (user, message) => {
        //console.log(user);
        //console.log(message);
        getData();
    });

    connection.on("BrandDeleted", (user, message) => {
        //console.log(user);
        //console.log(message);
        getData();
    });

    connection.on("BrandCreated", (user, message) => {
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

    if (brands.length != 0) {
        brands = [];
    }

    await fetch('http://localhost:27376/brand')
        .then(x => x.json())
        .then(y => {

            brands = y;
            display();
        });
}



function updateBrand() {

    //handle HTTP PUT request (input forms)

    let name = document.getElementById('nameToUpdate').value;
    let yearofappearance = document.getElementById('yearOfAppearanceToUpdate').value;


    fetch('http://localhost:27376/brand', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                id: brandIdToUpdate,
                name: name,
                yearOfAppearance: yearofappearance
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

    fetch('http://localhost:27376/brand/' + id, {
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

    brands.forEach(x => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + x.id + "</td><td>" + x.name + "</td>"
            + "<td>" + `<button type="button" onclick="remove(${x.id})">Delete</button>`
            + `<button type="button" onclick="showUpdateFroms(${x.id})">Update</button>`
            + "</td></tr>";
    });
}


function showUpdateFroms(id) {

    document.getElementById('brandUpdateFormDiv').style.display = 'flex';
    //flex-direction: column;
    document.getElementById('brandUpdateFormDiv').style.flexDirection = 'column';

    //bele kell írni a fromsba a kiválasztott adatait!

    let selectedBrand = brands.find(t => t['id'] == id);

    document.getElementById('nameToUpdate').value =
        selectedBrand['name'];

    document.getElementById('yearOfAppearanceToUpdate').value =
        selectedBrand['yearOfAppearance'];

   
    brandIdToUpdate = id; // globális változóba

}

function createBrand() {

    let name = document.getElementById('name').value;
    let yearofappearance = document.getElementById('yearOfAppearance').value;

    fetch('http://localhost:27376/brand', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: name,
                yearOfAppearance: yearofappearance
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