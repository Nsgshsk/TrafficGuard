/*Initialize*/
var map = L.map('map').setView([42.642, 25.252], 7);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    minZoom: 7,
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map);
map.setMaxBounds(map.getBounds());

/*marker*/
var la = 0;
var lo = 0;
function AddMarker(la, lo) {
    //la = parseFloat(la); lo = parseFloat(lo);
    var latlng = L.latLng(la, lo)
    var marker = L.marker([la,lo]).addTo(map);
    marker.bindPopup("<b>Accident!</b><br><p>Location: " + la + " | " + lo +"</p>");
}

/*PopUp event*/
function GetLocation() {
    var popup = L.popup();

    function onMapClick(e) {
        popup
            .setLatLng(e.latlng)
            .setContent("You clicked the map at " + e.latlng.toString())
            .openOn(map);
    }

    map.on('click', onMapClick);
}