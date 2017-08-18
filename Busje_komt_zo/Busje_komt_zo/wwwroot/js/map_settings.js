var root = window.location.host;
var busMarkers = [];

var mymap = L.map("mapid").setView([51.07421875, 3.74382209778], 13);


var myIcon = L.icon({
    iconUrl: "https://d30y9cdsu7xlg0.cloudfront.net/png/7892-200.png",
    iconSize: [30, 30],
    iconAnchor: [15, 30],
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
            .append($('<th>')
                .text("Min"))
        );

    $.each(busses,
        function (i, item) {
            if (busMarkers.length < busses.length) {
                busMarkers.push(L.marker([item.position.latitude, item.position.longitude], { icon: myIcon}).addTo(mymap));
            } else {
                var newLatLng = new L.LatLng(item.position.latitude, item.position.longitude);
                busMarkers[i].setLatLng(newLatLng);
            }

            if (item.minutesTillArrival === -1) {
                item.minutesTillArrival = "==>"
            }

            $("#statusTable").find('tbody')
                .append($('<tr>')
                    .append($('<td>')
                            .text(item.id)
                        )
                    .append($('<td>')
                            .text(item.message)
                    ).append($('<td>')
                        .text(item.minutesTillArrival)
                    )
                );

        });
}