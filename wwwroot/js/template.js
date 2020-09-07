define([], function () {
    function generateEventItem(template, item) {
        var template = $('#'+template+'').html();
        template = template.replaceAll('{{ThumbLocation}}', item.thumbLocation);
        template = template.replaceAll('{{Description}}', item.description);
        template = template.replaceAll('{{EventId}}', item.eventId);
        template = template.replaceAll('{{EventType}}', item.eventType.name);
        template = template.replaceAll('{{Summary}}', item.summary);
        template = template.replaceAll('{{dataevento}}', formatDate(item.eventDate));
        template = template.replaceAll('{{CategoryList}}', formatCategoryList(item.categoryList));
        template = template.replaceAll('{{Duration}}', item.duration);
        template = template.replaceAll('{{Location}}', item.location.address);
        return template;
    }

    function formatDate(date){
        var dataCsharp = date.split('T')[0];
        var day = dataCsharp.split('-')[2];
        var month = dataCsharp.split('-')[1];
        var year = dataCsharp.split('-')[0];
        return day + "/" + month + "/" + year;
    }

    function formatCategoryList(items)
    {
        var formatedCategory = '';
        for (var i = 0; i < items.length; i++) {
            formatedCategory += items[i].name + " ";
        };
        return formatedCategory;
    }

    function appendEventList(items) {
     var cardHtml = '';
     for (var i = 0; i < items.length; i++) {
        cardHtml += generateEventItem("event-card",items[i]);}
        $('.event-list').append(cardHtml);
    }

    function showEventDetailItem(eventItem)
    {
       var modalHtml = generateEventItem("event-modal-body-template", eventItem);
       $('#event-modal-body').html(modalHtml);
    }

    $('#event-modal').on('hidden.bs.modal', function () {
        $('#event-modal-body').html("");
    });

    return {
        appendEventList: appendEventList,
        showEventDetailItem: showEventDetailItem
    }
});
    