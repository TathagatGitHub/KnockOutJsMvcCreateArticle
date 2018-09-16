
function articleViewModel() {
    var self = this;

    self.articleList = ko.observableArray();

    self.getarticles = function () {
        $.ajax({
            type: 'GET',
            url: '/api/articleapi',
            contentType: "application/javascript",
            dataType: "jsonp",
            success: function (data) {
                var observableData = ko.mapping.fromJS(data);
                var array = observableData();
                self.articleList(array);
            },
            error: function (jq, st, error) {
                alert(error);
            }
        });
    };
}

$(document).ready(function () {
    ko.applyBindings(new articleViewModel());
});