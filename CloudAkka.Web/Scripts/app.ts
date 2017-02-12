let app = angular.module("MainApp", []);

class AppCtrl {

    Products: Product[];

    User: User;
    cart: CartService;

    constructor(_cart: CartService) {

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
            }];
    }

    get Cart() {
        return this.cart.Cart;
    }

    Login(name: string) {
        this.User = { Name: name };
        this.cart.loginUser(this.User);
    }

    Buy(product: Product) {
        this.cart.addProduct(product);
    }
}

app.controller("AppCtrl", AppCtrl);


class CartService {

    private isConnected: JQueryPromise<any>;
    private proxy: HubProxy;

    private cart = new Cart();

    constructor($rootScope) {

        var connection = $.hubConnection();

        this.proxy = connection.createHubProxy("cart");

        this.listen($rootScope);

        this.isConnected = this.connect(connection);
    }

    private connect(connection: HubConnection) {
        let promise = connection.start();
        promise.then(() => console.log("Connected"));
        promise.fail(() => console.error("Connection Failed"));
        return promise;
    }

    listen($scope: ng.IRootScopeService) {
        this.proxy.on("ProductAdded", (product: Product) => {
            $scope.$apply(() => this.cart.Add(product));
        });

        this.proxy.on("CartLoaded", (cart: Cart) => {
            $scope.$apply(() => this.cart = new Cart(cart));
        });
    }

    get Cart() {
        return this.cart;
    }

    addProduct(product: Product) {
        return this.invoke("AddProduct", product);
    }

    loginUser(user: User) {
        return this.invoke("LoginUser", user.Name);
    }

    invoke(methodName: string, ...args: any[]) {
        //Connection must be made before you can Invoke methods
        this.isConnected.then(() => this.proxy.invoke(methodName, ...args));
    }
}

app.service("_cart", CartService);

class Cart {
    Items: Item[] = []

    constructor(cart?: Cart) {
        if (cart) this.Items = cart.Items;
    }

    get Total() {
        if (this.Items.length) {
            return this.Items.map(i => i.Price * i.Quantity).reduce((ag, p) => ag + p)
        }
        return 0;
    }

    Add(product: Product) {
        let item = this.Items.filter(i => i.Name == product.Name)[0];
        if (item) {
            item.Quantity = item.Quantity + 1;
        }
        else {
            item = { ProductId: product.ProductId, Name: product.Name, Quantity: 1, Price: product.Price };
            this.Items.push(item);
        }
    }
}

interface User {
    Name: string;
}

interface Item {
    ProductId: number;
    Name: string;
    Price: number;
    Quantity: number;
}


interface Product {
    ProductId: number;
    Name: string;
    Price: number;
}