define(['./template.js'], function (template) {
    var eventUrl = '/Home/AvailableEvents/';
    function loadAvailableEvents() {
        fetch(eventUrl)
        .then(function (response) {
                return response.json();
            }).then(function (data) {
                   template.appendEventList(data);
                });
    }
    return {
        loadAvailableEvents :loadAvailableEvents
    }
});