var newSale = { };

//observables for add new sale
function Sale(data) {
    var self = this;
    self.Id = ko.observable(data.Id);
    self.Customer = ko.observable(data.Customer);
    self.CustomerId = ko.observable(data.CustomerId);
    self.Product = ko.observable(data.Product);
    self.ProductId = ko.observable(data.ProductId);
    self.Store = ko.observable(data.Store);
    self.StoreId = ko.observable(data.StoreId);
    self.DateSold = ko.observable(moment(data.DateSold).format('YYYY-MM-DD')); //format date using moment().format('YYYY-MM-DD');
}

//View Model
function SaleViewModel() {
    var self = this;
    self.AvailableCustomers = ko.observableArray();
    self.SelectedCustomer = ko.observable();
    self.AvailableProducts = ko.observableArray();
    self.SelectedProduct = ko.observable();
    self.AvailableStores = ko.observableArray();
    self.SelectedStore = ko.observable();
    self.ProductSoldData = ko.observable(); // Array contains the products sold data to add/edit
    self.ProductSolds = ko.observableArray(); //Array Contains the list of products sold

    GetAllSales();
    GetAllCustomers();
    GetAllProducts();
    GetAllStores();
    
    function GetAllCustomers() {
        $.ajax({
            method: 'GET',
            url: '/Customer/GetCustomerList',
            dataType: 'json',
            success: function (response) {
                self.AvailableCustomers(response);
            }
        });
    }
    function GetAllProducts() {
        $.ajax({
            method: 'GET',
            url: '/Product/GetProductList',
            dataType: 'json',
            success: function (response) {
                self.AvailableProducts(response);
            }
        });
    }
    function GetAllStores() {
        $.ajax({
            method: 'GET',
            url: '/Store/GetStoreList',
            dataType: 'json',
            success: function (response) {
                self.AvailableStores(response);
            }
        });
    }
     function GetAllSales() {
        $.ajax({
            method: "GET",
            url: "/ProductSold/GetSalesList",
            contentType: "application/json",
            success: function (response) {
                response.forEach(function (item) {
                    self.ProductSolds.push(new Sale(item));
                });
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    //Reset Modal
     self.ResetModal = function () {
        self.ProductSoldData(new Sale(newSale));
        $('#saledate').val('');
        $('#modalTitle').html('Add Sale');
        $('#updateBtn').hide();
     };

    //Add New Sale
     self.AddSale = function () {
         var data = {
             CustomerId: self.SelectedCustomer().Id,
             ProductId: self.SelectedProduct().Id,
             StoreId: self.SelectedStore().Id,
             DateSold: $("#saledate").val(),
         };
        $.ajax({
            type: "POST",
            url: "/ProductSold/AddSale",
            data: ko.toJSON(data),
            contentType: "application/json",
            dataType: 'json',
            success: function (result) {
                location.reload();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    };

    //Show data in modal to edit
    self.ShowEditModal = function (data) {
        self.ProductSoldData(data);
        var cus = ko.utils.arrayFirst(self.AvailableCustomers(), function (item) {
            return item.Id == data.CustomerId();
        });
        var prod = ko.utils.arrayFirst(self.AvailableProducts(), function (item) {
            return item.Id == data.ProductId();
        });
        var store = ko.utils.arrayFirst(self.AvailableStores(), function (item) {
            return item.Id == data.StoreId();
        });
        self.SelectedCustomer(cus);
        self.SelectedProduct(prod);
        self.SelectedStore(store);
        $('#myModal').modal('show');
        $('#modalTitle').html('Edit Sale');
        $('#createBtn').hide();
        $('#updateBtn').show();
    };

    //Update Sale Record
    self.UpdateSale = function (data) {        
        var Id = data.Id();
        var data = {
            CustomerId: self.SelectedCustomer().Id,
            ProductId: self.SelectedProduct().Id,
            StoreId: self.SelectedStore().Id,
            DateSold: $("#saledate").val(),
        };        
        $.ajax({
            method: 'POST',
            url: '/ProductSold/UpdateSale/' + Id,
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(data),
            dataType: 'json',
            success: function (result) {
                $('#myModal').modal('hide');
                alert("Sale updated successfully");
                location.reload();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    };

    //Delete Sale Record
    self.DeleteSale = function (data) {
        var id = data.Id();
        if (confirm('Are you sure to delete this record?')) {
            $.ajax({
                type: 'POST',
                url: '/ProductSold/DeleteSale/' + id,
                data: 'json',
                success: function (data) {
                    alert("Record deleted successfully");
                    location.reload();
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        }
    }
}

$(document).ready(function () {
    var sales = new SaleViewModel();
    ko.applyBindings(sales);
    
});
