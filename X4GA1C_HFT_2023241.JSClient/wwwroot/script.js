let laptops = [];
let connection = null;

let laptopIdToUpdate = -1;

getData();
setUpSignalR();

function setUpSignalR() {

    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:27376/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();


    connection.on("LaptopUpdated", (user, message) => {
        //console.log(user);
        //console.log(message);
        getData();
    });

    connection.on("LaptopDeleted", (user, message) => {
        //console.log(user);
        //console.log(message);
        getData();
    });

    connection.on("LaptopCreated", (user, message) => {
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

    if (laptops.length != 0) {
        laptops = [];
    }

    await fetch('http://localhost:27376/laptop')
        .then(x => x.json())
        .then(y => {

            laptops = y;
            //console.log(laptops);
            display();
        });
}



function updateLaptop() {

    //handle HTTP PUT request (input forms)

    let name = document.getElementById('laptopModelNameToUpdate').value;
    let processor = document.getElementById('cpuToUpdate').value;
    let ram = document.getElementById('ramToUpdate').value;
    let inputstorage = document.getElementById('storageToUpdate').value;
    var ram_upgradeable = document.getElementById('ramUpgradeableToUpdate').checked;
    let price = document.getElementById('priceToUpdate').value;
    let brandId = document.getElementById('brandIdToUpdate').value;

    let checkboxval = false;

    if (ram_upgradeable) {
        checkboxval = true;
    }

    fetch('http://localhost:27376/laptop', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                id: laptopIdToUpdate,
                modelName: name,
                processor: processor,
                ram: ram,
                storage: inputstorage,
                raM_Upgradeable: checkboxval,
                price: price,
                brandId: brandId
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

    fetch('http://localhost:27376/laptop/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null })
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

    laptops.forEach(x => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + x.id + "</td><td>" + x.modelName + "</td>"
        + "<td>" + `<button type="button" onclick="remove(${x.id})">Delete</button>`
        + `<button type="button" onclick="showUpdateFroms(${x.id})">Update</button>`
            + "</td></tr>";
    });
}


function showUpdateFroms(id) {

    document.getElementById('laptopUpdateFormDiv').style.display = 'flex';
    //flex-direction: column;
    document.getElementById('laptopUpdateFormDiv').style.flexDirection = 'column';

    //bele kell írni a fromsba a kiválasztott adatait!

    let selectedLaptop = laptops.find(t => t['id'] == id);

    document.getElementById('laptopModelNameToUpdate').value =
        selectedLaptop['modelName'];

    document.getElementById('cpuToUpdate').value =
        selectedLaptop['processor'];

    document.getElementById('ramToUpdate').value =
        selectedLaptop['ram'];

    document.getElementById('storageToUpdate').value =
        selectedLaptop['storage'];

    document.getElementById('brandIdToUpdate').value =
        selectedLaptop['brandId'];

    document.getElementById('priceToUpdate').value =
        selectedLaptop['price'];

    if (selectedLaptop['raM_Upgradeable'] == true) {
        document.getElementById('ramUpgradeableToUpdate').checked = true;
    }
    else {
        document.getElementById('ramUpgradeableToUpdate').checked = false;
    }


    laptopIdToUpdate = id; // globális változóba

}

function createLaptop() {
    let name = document.getElementById('laptopModelName').value;
    let processor = document.getElementById('cpu').value;
    let ram = document.getElementById('ram').value;
    let inputstorage = document.getElementById('storage').value;
    var ram_upgradeable = document.getElementById('ramUpgradeable').checked;
    let price = document.getElementById('price').value;
    let brandId = document.getElementById('brandId').value;

    let checkboxval = false;

    if (ram_upgradeable) {
        checkboxval = true;
    }

    fetch('http://localhost:27376/laptop', {
        method: 'POST',
        headers: {'Content-Type': 'application/json',},
        body: JSON.stringify(
            {
                modelName: name,
                processor: processor,
                ram: ram,
                storage: inputstorage,
                raM_Upgradeable: checkboxval,
                price: price,
                brandId: brandId
            })})
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
            })
            .catch((error) => {
                console.error('Error:', error);
            });


   

}