var app = angular.module("MainApp", []);
var AppCtrl = (function () {
    function AppCtrl(_cart) {
        this.cart = _cart;
        this.Products = [
            {
                ProductId: 1,
                Name: "Laptop",
                Price: 2000
            },
            {
                ProductId: 2,
                Name: "Phone",
                Price: 500
            }
        ];
    }
    Object.defineProperty(AppCtrl.prototype, "Cart", {
        get: function () {
            return this.cart.Cart;
        },
        enumerable: true,
        configurable: true
    });
    AppCtrl.prototype.Login = function (name) {
        this.User = { Name: name };
        this.cart.loginUser(this.User);
    };
    AppCtrl.prototype.Buy = function (product) {
        this.cart.addProduct(product);
    };
    return AppCtrl;
}());
app.controller("AppCtrl", AppCtrl);
var CartService = (function () {
    function CartService($rootScope) {
        this.cart = new Cart();
        var connection = $.hubConnection();
        this.proxy = connection.createHubProxy("cart");
        this.listen($rootScope);
        this.isConnected = this.connect(connection);
    }
    CartService.prototype.connect = function (connection) {
        var promise = connection.start();
        promise.then(function () { return console.log("Connected"); });
        promise.fail(function () { return console.error("Connection Failed"); });
        return promise;
    };
    CartService.prototype.listen = function ($scope) {
        var _this = this;
        this.proxy.on("ProductAdded", function (product) {
            $scope.$apply(function () { return _this.cart.Add(product); });
        });
        this.proxy.on("CartLoaded", function (cart) {
            $scope.$apply(function () { return _this.cart = new Cart(cart); });
        });
    };
    Object.defineProperty(CartService.prototype, "Cart", {
        get: function () {
            return this.cart;
        },
        enumerable: true,
        configurable: true
    });
    CartService.prototype.addProduct = function (product) {
        return this.invoke("AddProduct", product);
    };
    CartService.prototype.loginUser = function (user) {
        return this.invoke("LoginUser", user.Name);
    };
    CartService.prototype.invoke = function (methodName) {
        var _this = this;
        var args = [];
        for (var _i = 1; _i < arguments.length; _i++) {
            args[_i - 1] = arguments[_i];
        }
        //Connection must be made before you can Invoke methods
        this.isConnected.then(function () {
            return (_a = _this.proxy).invoke.apply(_a, [methodName].concat(args));
            var _a;
        });
    };
    return CartService;
}());
app.service("_cart", CartService);
var Cart = (function () {
    function Cart(cart) {
        this.Items = [];
        if (cart)
            this.Items = cart.Items;
    }
    Object.defineProperty(Cart.prototype, "Total", {
        get: function () {
            if (this.Items.length) {
                return this.Items.map(function (i) { return i.Price * i.Quantity; }).reduce(function (ag, p) { return ag + p; });
            }
            return 0;
        },
        enumerable: true,
        configurable: true
    });
    Cart.prototype.Add = function (product) {
        var item = this.Items.filter(function (i) { return i.Name == product.Name; })[0];
        if (item) {
            item.Quantity = item.Quantity + 1;
        }
        else {
            item = { ProductId: product.ProductId, Name: product.Name, Quantity: 1, Price: product.Price };
            this.Items.push(item);
        }
    };
    return Cart;
}());
//# sourceMappingURL=app.js.map