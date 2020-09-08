define(['./template.js', './clientStorage.js'], function (template, clientStorage) {
    var eventUrl = '/Home/AvailableEvents/';
    var eventLocationUrl = '/Home/AvailableEventsByLocation/?userLatitude=lat&userLongitude=lon';
    var eventByIdUrl = '/Home/LoadEvent/?id=';

    function fetchPromiseEventos(url) {
        return new Promise(function (resolve, reject) {
            fetch(url)
                .then(function (data) {    
                    $("#homeText").html("Eventos Disponiveis");
                    $("#connected").val('1');

                    data.json().then(function(jsonData){
                        template.appendEventList(jsonData);
                        resolve('Conectado com sucesso, exibindo resultados atualizados.');
                    });
                }).catch(function (e) {
                    resolve('Sem conexão, exibindo os resultados salvos');
                    $("#homeText").html("Eventos armazenados");
                    $("#connected").val('0');
                });

            setTimeout(function () { resolve('A conexão está lenta, exibindo os resultados salvos');}, 8000);
        });
    }
    function fetchPromiseEventDetail(url, eventId) {
        return new Promise(function (resolve, reject) {
            fetch(url)
                .then(function (data) {    
                    data.json().then(function(jsonData){

                        if (!jsonData) {
                            template.showEventDetailItem({});
                        } else {
                            template.showEventDetailItem(jsonData);
                        }
                    });
                }).catch(function (e) {
                    clientStorage.getEventDetail(eventId).then(function (data)
                    {
                        template.showEventDetailItem(data);
                    });
                });

            setTimeout(function () { resolve('A conexão está lenta, exibindo os resultados salvos');}, 8000);
        });
    }

    function loadAvailableEvents() {
        $("#eventlist").html("");
        fetchPromiseEventos(eventUrl)
        .then(function (status) {
                $('#connection-status').html(status);

                if ($("#connected").val() == '0' || $('#connection-status').html().indexOf('lenta') >= 0){
                    $("#homeText").html("Eventos armazenados");
                    clientStorage.getEvents().then(function (data) {
                        template.appendEventList(data);
                     });  
                };
            });
    }

    function loadEventDetail(eventId) {

        fetchPromiseEventDetail(eventByIdUrl+eventId, eventId)
        .then(function (response) {
                return response.json();
            }).then(function (data) {
                window.location = '#event' + eventId;
            });
    }

    function saveEventInCache(eventId){
        fetch(eventByIdUrl+eventId)
        .then(function (response) {
                return response.json();
            }).then(function (data) {
                if (!data) {
                    console.log("falha ao obter evento");
                } else {
                    clientStorage.addEvent(data);
                    alert("Evento armazenado com sucesso.");
                }
            });
    }

    function loadAvailableEventsByLocation(latitude, longitude) {
        $("#eventlist").html("");
        var url = eventLocationUrl.replace('lat', latitude).replace('lon', longitude);
        fetch(url)
        .then(function (response) {
                return response.json();
            }).then(function (data) {
                if (!data) {
                    console.log("falha ao obter eventos por proximidade.");
                } else {
                    $("#homeText").html("Eventos por proximidade");
                    template.appendEventList(data);
                }
            });
    }

    return {
        loadAvailableEvents :loadAvailableEvents,
        loadAvailableEventsByLocation :loadAvailableEventsByLocation,
        loadEventDetail: loadEventDetail,
        saveEventInCache: saveEventInCache
    }
});