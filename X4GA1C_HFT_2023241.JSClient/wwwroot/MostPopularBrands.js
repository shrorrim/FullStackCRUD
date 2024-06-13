let MostPopularBrands = [];

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

    if (MostPopularBrands.length != 0) {
        MostPopularBrands = [];
    }

    await fetch('http://localhost:27376/Stat/MostPopularBrands')
        .then(x => x.json())
        .then(y => {

            MostPopularBrands = y;
            display();
        });
}



function display() {

    document.getElementById('resultarea').innerHTML = "";

    MostPopularBrands.forEach(x => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + x.name + "</td><td>" + x.yearOfAppearance + "</td></tr>";
    });
}