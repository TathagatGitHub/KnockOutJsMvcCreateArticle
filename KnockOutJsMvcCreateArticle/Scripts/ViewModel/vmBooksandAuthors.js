
//<script type="text/javascript">


    function book(Id, AuthorId, Isbn, Description, Title, Synopisis, ImageUrl) {
        self.Id = ko.observable(Id);
        self.AuthorId = ko.observable(AuthorId);
        self.Isbn = ko.observable(Isbn);
        self.Description = ko.observable(Description);
        self.Title = ko.observable(Title);
        self.Synopsis = ko.observable(Synopisis);
        self.ImageUrl = ko.observable(ImageUrl);
    }

function VMBooks() {
    var self = this;

    self.books = ko.observableArray();

    $.getJSON("/api/bookapi/", function (data) {
        var xx = [];

        $.each(data, function (key, value) {

            self.books.push(new book(value.Id, value.AuthorId, value.Isbn, value.Description, value.Title, value.Synopsis, value.ImageUrl));
        });

    })
        .error(function () {
            alert("error");
        })
    .complete(function () {
        console.log("fetch complete + " + this);
        console.log("# of items in the array + " + self.books().length);
    });
}

   

// Now bind the VM
ko.applyBindings(new VMBooks(), $("#bookslist")[0]);
//</script>

//<script type="text/javascript">
    function author(Id, FirstName, LastName, Biography) {
        self.Id = ko.observable(Id);
        self.FirstName = ko.observable(FirstName);
        self.LastName = ko.observable(LastName);
        self.Biography = ko.observable(Biography);

    }

function VMAuthors() {
    var self = this;

    self.authors = ko.observableArray();

    $.getJSON("/api/authorapi/", function (data) {
        var xx = [];

        $.each(data, function (key, value) {

            self.authors.push(new author(value.Id, value.FirstName, value.LastName, value.Biography));
        });

    })
        .error(function () {
            alert("error");
        })
    .complete(function () {
        console.log("fetch complete + " + this);
        console.log("# of items in the array + " + self.authors().length);
    });
}

// Now bind the VM
ko.applyBindings(new VMAuthors(), $("#authorlist")[0]);
//</script>