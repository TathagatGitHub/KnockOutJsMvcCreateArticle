$(document).ready(function () {
    var viewModel = {};
    // Edit view bindings
    viewModel.Id = ko.observable();
    viewModel.AuthorId = ko.observable();
    viewModel.Isbn = ko.observable();
    viewModel.Title = ko.observable();
    viewModel.Description = ko.observable();
    viewModel.Synopsis = ko.observable();
    viewModel.ImageUrl = ko.observable();
    // List and Edit views settings
    viewModel.showEdit = ko.observable(false);
    viewModel.showList = ko.observable(true);
    viewModel.books = ko.observableArray([]);

    viewModel2.bookauthorlist = ko.observableArray([]);


    viewModel.bookUpdateForm = function (data, event) {
        console.log(data, event);
        console.log("\n data = " + ko.toJSON(data));
        viewModel.showEdit(true);
        viewModel.showList(false);

        var rowData = ko.toJSON(data);
        var obj = JSON.parse(ko.toJSON(data));
        var url = "/api/BookApi/GetBook?id=" + obj.Id;
        console.log("\n url = " + url);

        $.getJSON(url, function (data) {
            //console.log(data.Title);
            viewModel.Id(data.Id);
            viewModel.AuthorId(data.AuthorId);
            viewModel.Isbn(data.Isbn);
            viewModel.Title(data.Title);
            viewModel.Description(data.Description);
            viewModel.Synopsis(data.Synopsis);
            viewModel.ImageUrl(data.ImageUrl);
        });

    }

    viewModel.updateBook = function (book) {
        var dataObject = ko.toJSON(this);
        var id = this.Id();
        $.ajax({
            url: '/api/bookapi/PutBook/' + id,
            type: 'PUT',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) { //After inserting into DB, add into table list

                viewModel.books([]);
                viewModel.books(data);
                $.getJSON("/api/bookapi/GetBooks/", function (data) {
                    console.log(data);
                    var arraybook = ko.mapping.fromJS(data);
                    viewModel.books(arraybook());
                });

                viewModel.showEdit(false);
                viewModel.showList(true);

            }
        });


    }
    $.getJSON("/api/bookapi/GetBooks/", function (data) {
        console.log(data);
        var arraybook = ko.mapping.fromJS(data);
        viewModel.books(arraybook());// = ko.mapping.fromJS(data);
        ko.applyBindings((viewModel), $("#bookslist")[0]);

    });


    var viewModel1 = {};
    $.getJSON("/api/authorapi/GetAuthors/", function (data) {
        console.log(data);
        viewModel1.authors = ko.mapping.fromJS(data);
        ko.applyBindings((viewModel1), $("#authorlist")[0]);
        //   ko.applyBindings(new VMBooks(), $("#bookslist")[0]);
    });



});
var viewModel2 = {};
function searchBooksAndAuthor() {
    // var bookIsbn = document.getElementById("txtbookIsbnID").valueOf;
    var bookIsbn = document.getElementById("txtbookIsbnID").value;
    // alert(bookIsbn);
    viewModel2.bookauthorlist([]);
    console.log(bookIsbn);

    var url = "/api/BookAuthorApi/GetvmBookAuthorSearch?bookIsbn=" + bookIsbn;

    //$.getJSON("http://localhost:57367/api/BookAuthorApi/GetvmBookAuthorSearch?bookIsbn=12345", function (data) {
    $.getJSON(url, function (data) {

        var xx = ko.mapping.fromJS(data);
        console.log(xx());

        viewModel2.bookauthorlist(xx());
        //viewModel2.bookauthorlist = ko.mapping.fromJS(data);
        ko.applyBindings(viewModel2, $("#bookauthorlist")[0]);
    });
}

