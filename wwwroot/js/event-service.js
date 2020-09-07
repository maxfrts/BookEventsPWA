define(['./template.js', './clientStorage.js'], function (template, clientStorage) {
    var eventUrl = '/Home/AvailableEvents/';
    var eventByIdUrl = '/Home/LoadEvent/?id=';

    function fetchPromise(url, link, text) {

        link = link || '';

        return new Promise(function (resolve, reject) {
            fetch(url + link)
                .then(function (data) {

                    var resolveSuccess = function () {
                        resolve('The connection is OK, showing latest results');
                    };

                    if (text) {
                        data.text().then(function (text) {
                            clientStorage.addEventText(link, text).then(resolveSuccess);
                        });
                    }
                    else {
                        data.json().then(function (jsonData) {
                            clientStorage.addEvents(jsonData).then(resolveSuccess);
                        });
                    }

                }).catch(function (e) {
                    resolve('No connection, showing offline results');
                });

            setTimeout(function () { resolve('The connection is hanging, showing offline results'); }, 800);
        });
    }

    function loadAvailableEvents() {
        fetch(eventUrl)
        .then(function (response) {
                return response.json();
            }).then(function (data) {
                   template.appendEventList(data);
                });
    }

    function loadEventDetail(eventId) {

        fetch(eventByIdUrl+eventId)
        .then(function (response) {
                return response.json();
            }).then(function (data) {
                if (!data) {
                    template.showEventDetailItem({});
                } else {
                    template.showEventDetailItem(data);
                }
                window.location = '#event' + eventId;
            });
    }

    return {
        loadAvailableEvents :loadAvailableEvents,
        loadEventDetail: loadEventDetail
    }
});