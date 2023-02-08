/*Initialize*/
var map = L.map('map').setView([42.642, 25.252], 7.5);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map);

/*marker*/
new function AddMarker(la, lo) {
    //la = parseFloat(la); lo = parseFloat(lo);
    var latlng = L.latLng(<Number>  la, <Number> lo)
    var marker = L.marker([la,lo]).addTo(map);
    /*marker.bindPopup("<b>Hello world!</b><br>I am a popup.").openPopup();*/
}

/*PopUp event*/
var popup = L.popup();

function onMapClick(e) {
    popup
        .setLatLng(e.latlng)
        .setContent("You clicked the map at " + e.latlng.toString())
        .openOn(map);
}

map.on('click', onMapClick);