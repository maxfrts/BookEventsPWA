define([], function () {
    function generateEventItem(item) {
        var template = $('#event-card').html();
        template = template.replace('{{ThumbLocation}}', item.thumbLocation);
        template = template.replace('{{EventId}}', item.eventId);
        template = template.replace('{{Description}}', item.description);
        template = template.replace('{{Summary}}', item.summary);
        template = template.replace('{{Link}}', item.eventId);
     return template;
    }

    function appendEventList(items) {
     var cardHtml = '';
     for (var i = 0; i < items.length; i++) {
        cardHtml += generateEventItem(items[i]);}
        $('.event-list').append(cardHtml);
    }
    return {
        appendEventList: appendEventList
    }
});
    