var articleRegistrationVM;
// use as register article views view model
function Article(id, title, excerpts, content) {
    var self = this;
    // observable are update elements upon changes, also update on element data changes [two way binding]
    self.Id = ko.observable(id);
    self.Title = ko.observable(title);
    self.Excerpts = ko.observable(excerpts);
    // create computed field by combining first name and last name
  //  self.FullName = ko.computed(function () {
    //    return self.FirstName() + " " + self.LastName();
    //}, self);
    self.Content = ko.observable(content);
   // self.Description = ko.observable(description);
    //self.Gender = ko.observable(gender);
    // Non-editable catalog data - should come from the server
    //self.genders = [
    //"Male",
    //"Female",
    //"Other"
    //];
    self.addArticle = function () {
        var dataObject = ko.toJSON(this);
        // remove computed field from JSON data which server is not expecting
        //delete dataObject.FullName;
        $.ajax({
            url: '/api/articleapi',
            type: 'post',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) { //After inserting into DB, add into table list
                articleRegistrationVM.articleListViewModel.articles.push(new Article(data.id, data.Title, data.Excerpts, data.Content));
                self.Id(null);
                self.Title('');
                self.Excerpts('');
                self.Content('');
               // self.Description('');
            }
        });
    };

}
// use as article list view's view model
function ArticleList() {
    var self = this;
    self.Id = ko.observable();
    self.Title = ko.observable();
    self.Excerpts = ko.observable();
    // create computed field by combining first name and last name
    //  self.FullName = ko.computed(function () {
    //    return self.FirstName() + " " + self.LastName();
    //}, self);
    self.Content = ko.observable('');
    // observable arrays are update binding elements upon array changes
    self.articles = ko.observableArray([]);
   // self.articles = ko.mapping.fromJS([]);
    self.showdetails = ko.observable(false);
        self.enableAbout = function () {
            self.showdetails(true);
        };
        self.disableAbout = function () {
            self.showdetails(false);
        }
   // self.getArticles = function () {
    //    self.articles.removeAll();
        // retrieve students list from server side and push each object to model's students list
    //    $.getJSON('/api/articleapi/', self.articles); or
       $.getJSON('/api/articleapi', function (data) {
             $.each(data, function (key, value) {
                 self.articles.push(new Article(value.id, value.Title, value.Excerpts, value.Content));
                // debugger;
             });
            //ko.mapping.fromJS(data, self.articles);
       });

    //};

    // details student. current data context object is passed to function automatically.
    self.getArticlesDetails = function (article) {
        alert(article.Id());
        $.ajax({ type: "GET", contentType: 'application/json', url: '/api/articleapi' + '/' + article.Id() })
                  .done(function () {
                      alert(article.Title());
                      self.Title(article.Title());
                      self.Excerpts(article.Excerpts());
                      self.Content(article.Content());
                  });

    };
    // remove . current data context object is passed to function automatically.
    self.removeArticle = function (article) {
        alert(article.Id());
        $.ajax({
            url: '/api/articleapi/' + article.Id(),
            type: 'delete',
            contentType: 'application/json',
            success: function () {
                self.articles.remove(article);
            }
        });
    };
 
}

// create index view view model which contain two models for partial views
articleRegistrationVM = { addArticleViewModel: new Article(), articleListViewModel: new ArticleList()};
// on document ready
$(document).ready(function () {
    // bind view model to referring view
    ko.applyBindings(articleRegistrationVM);
    // load student data
  //  articleRegistrationVM.articleListViewModel.getArticles();
   // articleRegistrationVM.detailArticleViewModel.getArticlesDetails(1);
});