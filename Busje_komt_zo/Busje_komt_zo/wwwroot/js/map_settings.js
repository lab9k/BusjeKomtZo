var root = window.location.host;
var busMarkers = [];

var mymap = L.map("mapid").setView([51.07421875, 3.74382209778], 13);


var myIcon = L.icon({
    iconUrl: "https://cdn2.iconfinder.com/data/icons/picons-basic-3/57/basic3-021_delivery_van-512.png",
    iconSize: [50, 59],
    iconAnchor: [22, 94],
    popupAnchor: [-3, -76]
});

var weba = L.circle([51.07421875, 3.74382209778],
    {
        color: "red",
        fillColor: "#f03",
        fillOpacity: 0.5,
        radius: 250
    }).addTo(mymap);
var jacob = L.circle([51.0565109253, 3.72703361511],
    {
        color: "red",
        fillColor: "#f03",
        fillOpacity: 0.5,
        radius: 250
    }).addTo(mymap);


L.tileLayer("https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}",
    {
        attribution:
            'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
        maxZoom: 25,
        id: "mapbox.streets",
        accessToken: "pk.eyJ1IjoicnZlcnZ1c3QiLCJhIjoiY2o2M2UwM2p5MWQ0ODMzcG9qcnlndWxjaCJ9.QkVicGhXCReO7kV9KSfqrA"
    }).addTo(mymap);


function GetBusses() {
    $.ajax(
        {
            dataType: "json",
            url: "http://" + root + "/api/bus",
            success: function(data) {
                updateMarkers(data);
            }
        });
}

function updateMarkers(busses) {
    $("#statusTable").find('tbody').empty();

    $("#statusTable").find('tbody')
        .append($('<tr>')
            .append($('<th>')
                .text("BusNr"))
            .append($('<th>')
                .text("Status"))
        );

    $.each(busses,
        function (i, item) {
            if (busMarkers.length < busses.length) {
                busMarkers.push(L.marker([item.position.latitude, item.position.longitude]).addTo(mymap));
            } else {
                var newLatLng = new L.LatLng(item.position.latitude, item.position.longitude);
                busMarkers[i].setLatLng(newLatLng);
            }

            $("#statusTable").find('tbody')
                .append($('<tr>')
                    .append($('<td>')
                            .text(item.id)
                        )
                    .append($('<td>')
                            .text(item.message)
                    )
                );

        });
}