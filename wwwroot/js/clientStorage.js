define([], function () {

    var oldestEventId = "";
    var limit = 4;
    var eventInstance = localforage.createInstance({
        name: 'event'
    });

    function getEventDetail(eventId) {
        return new Promise(function (resolve, reject) {
            eventInstance.getItem(eventId)
                .then(function (text) {
                    resolve(text);
                });
        });
    }

    function addEvent(event) {
        return new Promise(function (resolve, reject) {

            var keyValuePair = [];

            
            keyValuePair.push({ key: String(event.eventId), value: event });

            keyValuePair = keyValuePair.sort(function (a, b) { return b.key - a.key });

            eventInstance.setItems(keyValuePair)
                .then(function () {
                    resolve();
                });
        });
    }

    function getEvents() {

        return new Promise(function (resolve, reject) {

            eventInstance.keys().then(function (keys) {

                keys = keys.filter(function (a) { return a && !a.includes('#') });
                keys = keys.sort(function (a, b) { return a - b });

                var index = keys.indexOf(oldestEventId);
                if (index == -1) { index = keys.length; }
                if (index == 0) { resolve([]); return; }

                var start = index - limit;
                var limitAdjusted = start < 0 ? index : limit;

                var keys = keys.splice(Math.max(0, start), limitAdjusted);

                eventInstance.getItems(keys).then(function (results) {
                    var events = Object.keys(results).map(function (k) { return results[k] }).reverse();
                    oldestEventId = String(events[events.length - 1].eventId);
                    resolve(events);
                });
            });

        });

    }

    function getOldestEventId() {
        return oldestEventId;
    }

    return {
        addEvent: addEvent,
        getEvents: getEvents,
        getOldestEventId: getOldestEventId,
        getEventDetail: getEventDetail
    }
});