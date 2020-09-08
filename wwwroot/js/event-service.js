define(['./template.js', './clientStorage.js'], function (template, clientStorage) {
    var eventUrl = '/Home/AvailableEvents/';
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

    function loadAvailableEvents() {
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

    function saveEventInCache(eventId){
        fetch(eventByIdUrl+eventId)
        .then(function (response) {
                return response.json();
            }).then(function (data) {
                if (!data) {
                    console.log("falha ao obter evento");
                } else {
                    clientStorage.addEvent(data);
                }
            });
    }
    return {
        loadAvailableEvents :loadAvailableEvents,
        loadEventDetail: loadEventDetail,
        saveEventInCache: saveEventInCache
    }
});