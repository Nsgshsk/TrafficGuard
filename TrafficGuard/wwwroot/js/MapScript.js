/*Initialize*/
var map = L.map('map', {
    minZoom: 7,
    maxZoom: 19,
});

map.setView([42.642, 25.252], 7);

const apiKey = "AAPK2f0292e8582b48898e8218b94ee2f8e7Nor0WE0TdNry9C9CUMfx6utrn0Jy-AllDAWJVsYyoe8_3E5gJsggAg-_08WjaUie";
const basemapEnum = "ArcGIS:Streets";

L.esri.Vector.vectorBasemapLayer(basemapEnum, {
    apiKey: apiKey
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

function ReverseLocation() {
    const layerGroup = L.layerGroup().addTo(map);

    map.on("click", function (e) {

        L.esri.Geocoding
            .reverseGeocode({
                apikey: apiKey
            })
            .latlng(e.latlng)

            .run(function (error, result) {
                if (error) {
                    return;
                }

                layerGroup.clearLayers();
                marker = L.marker(result.latlng).addTo(layerGroup);

                const lngLatString = `${Math.round(result.latlng.lng * 100000) / 100000}, ${Math.round(result.latlng.lat * 100000) / 100000}`;

                marker.bindPopup(`<b>${lngLatString}</b><p>${result.address.Match_addr}</p>`);
                marker.openPopup();

                return lngLatString;
            });
    });
}