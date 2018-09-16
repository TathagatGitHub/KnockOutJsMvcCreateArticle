function AuthorViewModel() {
    var self = this;
    self.saveCompleted = ko.observable(false);
    self.sending = ko.observable(false);
    self.author = {
        firstName: ko.observable(),
        lastName: ko.observable(),
        biography: ko.observable(),
    };

    self.validateAndSave = function (form) {
        if (!$(form).valid())
            return false;
        self.sending(true);
        // include the anti forgery token
        self.author.__RequestVerificationToken = form[0].value;
       // $.ajax({
          //  url: 'Create',
         //   type: 'post',
         //   contentType: 'application/x-www-form-urlencoded',
          //  data: ko.toJS(self.author)
        //  })
        $.ajax({
            url: '/api/authorapi',
            type: 'post',
            contentType: 'application/json',
            data: ko.toJSON(self.author)
        })
        .success(self.successfulSave)
        .error(self.errorSave)
        .complete(function () { self.sending(false) });
    };

    self.validateAndUpdate = function (form) {
        if (!$(form).valid())
            return false;
        alert("here");
        self.sending(true);
        // include the anti forgery token
        self.author.__RequestVerificationToken = form[0].value;
      //  $.ajax({
     //       url: 'Edit',
      //      type: 'post',
      //      contentType: 'application/x-www-form-urlencoded',
      //      data: ko.toJS(self.author)
      //  })
        $.ajax({
            url: '/api/authorapi',
            type: 'put',
            contentType: 'application/json',
            data: ko.toJSON(self.author)
        })
        .success(self.successfulSave)
        .error(self.errorSave)
        .complete(function () { self.sending(false) });
    };

    self.successfulSave = function () {
        self.saveCompleted(true);
        $('.body-content').prepend('<div class="alert alert-success"> <strong>Success!</strong> The new author has been saved.</div>');
        setTimeout(function () { location.href = './'; }, 1000);
    };

    self.errorSave = function () {
        $('.body-content').prepend( '<div class="alert alert-danger"> <strong>Error!</strong> There was an error creating the author.</div>');
    };
}