

$.ajax({ type: "GET", url: '/api/articleapi/1' })
             .done(function () {
                   
                 ko.mapping.fromJS(article, libraryViewModel);
                     
             });
var libraryViewModel = ko.mapping.fromJS(article);

ko.applyBindings(libraryViewModel);

