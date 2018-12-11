var newStore = { Name: '', Address: '' };

function Store(data) {
    /*observables bound to model properties*/
    this.Id = ko.observable(data.Id);
    this.Name = ko.observable(data.Name).extend({
        required: true,
        minLength: 5,
        maxLength: 15
    });
    this.Address = ko.observable(data.Address).extend({
        required: true        
    });

    ModelErrors = ko.validation.group(this);
    IsValid = ko.computed(function () {
        ModelErrors.showAllMessages();
        return ModelErrors().length == 0;
    });
}

function StoreViewModel() {
    Stores = ko.observableArray(); /*Array to contain the list of stores*/
    StoreData = ko.observable(); /*Observable to bind store data for add/edit operation*/

    GetAllStores();
    function GetAllStores() {
        $.ajax({
            method: "GET",
            url: "/Store/GetStoreList",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                Stores(result);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    /*reset Modal*/
    ResetModal = function () {
        StoreData(new Store(newStore));
        $('#modalTitle').html('Add Store');
        $('#createBtn').show();
        $('#updateBtn').hide();
    };

    /*add new store*/
    AddStore = function () {
        var data = ko.toJSON(StoreData);
        $.ajax({
            type: "POST",
            url: "/Store/AddStore",
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (result) {
                Stores.push(result);
                $('#myModal').modal('hide');
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    };

    /*show data in modal to edit*/
    ShowEditModal = function (data) {
        /*mapped data using ko.mapping plugin*/
        var selected = ko.mapping.toJS(data);
        StoreData(new Store(selected));
        $('#myModal').modal('show');
        $('#modalTitle').html('Edit Store');
        $('#createBtn').hide();
        $('#updateBtn').show();
    };

    /*update store record*/
    UpdateStore = function () {
        var data = ko.toJSON(StoreData);
        $.ajax({
            method: 'POST',
            url: '/Store/UpdateStore',
            contentType: 'application/json; charset=utf-8',
            data: data,
            dataType: 'json',
            success: function (result) {
                GetAllStores();
                $('#myModal').modal('hide');
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    };

    /*delete store record*/
    DeleteStore = function (data) {
        if (confirm('Are you sure to delete this record?')) {
            var id = data.Id;
            $.ajax({
                type: 'POST',
                url: '/Store/DeleteStore/' + id,
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    Stores.remove(data);
                    GetAllStores();
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        }
    }
}
$(document).ready(function () {
    var StoreVM = new StoreViewModel();
    ko.applyBindings(StoreVM);
});



