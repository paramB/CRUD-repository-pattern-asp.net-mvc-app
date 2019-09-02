var newPro = { Name: '', Price: ''};
function Product(data) {
    /*observables bound to model properties*/
    this.Id = ko.observable(data.Id);
    this.Name = ko.observable(data.Name).extend({
        required: true,
        minLength: 5,
        maxLength: 15
    });
    this.Price = ko.observable(data.Price).extend({
        required: true,
        pattern: {
            message: "Please enter appropriate amount.",
            params: /^(\d*\.)?\d+$/
        }
    });

    ModelErrors = ko.validation.group(this);
    IsValid = ko.computed(function () {
        ModelErrors.showAllMessages();
        return ModelErrors().length == 0;
    });
}

function ProductViewModel() {
    Products = ko.observableArray(); /*Array to contain the list of products*/
    ProductData = ko.observable(); /*Observable to bind product data for add/edit operation*/

    GetAllProducts();
    function GetAllProducts() {
        $.ajax({
            method: "GET",
            url: "/Product/GetProductList",
            contentType: "application/json;charset=utf-8",
            success: function (result) {                
                Products(result);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    /*reset modal*/
    ResetModal = function () {
        ProductData(new Product(newPro));
        $('#modalTitle').html('Add Product');
        $('#createBtn').show();
        $('#updateBtn').hide();
    };

    /*Add New Product*/
    AddProduct = function () {
        var data = ko.toJSON(ProductData);
        $.ajax({
            type: "POST",
            url: "/Product/AddProduct",
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (result) {
                Products.push(result);
                $('#myModal').modal('hide');
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    };

    /*Show data in modal to edit*/
    ShowEditModal = function (data) {        
        /*mapped data using ko.mapping plugin*/
        var selected = ko.mapping.toJS(data);        
        ProductData(new Product(selected));        
        $('#myModal').modal('show');
        $('#modalTitle').html('Edit Product');
        $('#createBtn').hide();
        $('#updateBtn').show();
    };

    /*Update Product Record*/
    UpdateProduct = function () {
        var data = ko.toJSON(ProductData);
        $.ajax({
            method: 'POST',
            url: '/Product/UpdateProduct',
            contentType: 'application/json; charset=utf-8',
            data: data,
            dataType: 'json',
            success: function (result) {                
                GetAllProducts();
                $('#myModal').modal('hide');               
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    };

    /*Delete Product Record*/
    DeleteProduct = function (data) {
        if (confirm('Are you sure to delete this record?')) {
            var id = data.Id;
            $.ajax({
                type: 'POST',
                url: '/Product/DeleteProduct/' + id,
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    Products.remove(data);
                    GetAllProducts();                    
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        }
    }
}
$(document).ready(function () {
    var ProductVM = new ProductViewModel();
    ko.applyBindings(ProductVM);
});



