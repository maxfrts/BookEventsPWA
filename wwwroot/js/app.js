var eventService = require('./event-service.js');
var testPushService = require('./testPushService.js');
var serviceWorker = require('./swRegister.js');
var localization = require('./localization.js');

var eventByIdUrl = '/Home/LoadEvent/?id=';

//window events
let defferedPrompt;
window.addEventListener('beforeinstallprompt', (e) => {
    e.preventDefault();
    defferedPrompt = e;
    //atualizar a tela para notificar o usuario
    // que ele pode adicionar à tela de home
    $('#install-container').show();
});

window.addEventListener('appinstalled', (evt) => {
    console.log('app foi adicionada na home screen!');
});

if ('BackgroundFetchManager' in self) {
    console.log('this browser supports Background Fetch!');
}

window.pageEvents = {
    loadEvent: function (eventId) {
        eventService.loadEventDetail(eventId);
    },
    tryAddHomeScreen: function () {
        defferedPrompt.prompt();
        defferedPrompt.userChoice.then((choiceResult) => {
            if (choiceResult.outcome == 'accepted') {
                $('#install-container').hide();
            }
            defferedPrompt = null;
        });
    },
    setBackgroundFetch: function (eventId) {
        navigator.serviceWorker.ready.then(async (swReg) => {

            //receive confirmation message 
            navigator.serviceWorker.addEventListener('message', event => {
                $('.download-response').html('msg : ' + event.data.msg + ' url: ' + event.data.url);
                console.log(event.data.msg, event.data.url);
            });

            var date = new Date();
            var timestamp = date.getTime();            
            const bgFetch = await swReg.backgroundFetch.fetch(eventByIdUrl+eventId,
                ['/Home/LoadEvent/?id=' + eventId]
                , {
                    downloadTotal: 2 * 1024 * 1024,
                    title: 'download Event',
                    icons: [{
                        sizes: '72x72',
                        src: 'img/icons/icon-72x72.png',
                        type: 'image/png',
                    }]
                });

            bgFetch.addEventListener('progress', () => {
                if (!bgFetch.downloadTotal) return;

                const percent = Math.round(bgFetch.downloaded / bgFetch.downloadTotal * 100);

                $('.download-start').hide();
                $('#status-download').show();
                $('#status-download > .progress > .progress-bar').css('width', percent + '%');

                if (bgFetch.result === 'success') {

                    $('#status-download > .text-success').show();
                }
            });
        });
    },
    requestPushPermission: function () {
        serviceWorker.requestPushPermission();
    },
    getGeolocation: function(){
        localization.getGeolocation();
    },
    saveEventInCache: function(eventId){
        eventService.saveEventInCache(eventId);
    }
};

function getCoordinates(){
    localization.getCoordinates();
};

eventService.loadAvailableEvents();
$("#userLocation").val(getCoordinates());
//testPushService.bindSendNotification();
