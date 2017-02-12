var Hub = (function () {
    function Hub(hubName, options) {
        this.hubName = hubName;
        this.options = options;
        this.connection = this.getConnection(options);
        this.proxy = this.connection.createHubProxy(hubName);
        if (options && options.listeners) {
            Object.getOwnPropertyNames(options.listeners)
                .filter(function (propName) {
                return typeof options.listeners[propName] === 'function';
            })
                .forEach(function (propName) {
                this.on(propName, options.listeners[propName]);
            });
        }
        if (options && options.methods) {
            angular.forEach(options.methods, function (method) {
                this[method] = function () {
                    var args = $.makeArray(arguments);
                    args.unshift(method);
                    return this.invoke.apply(this, args);
                };
            });
        }
        if (options && options.queryParams) {
            this.connection.qs = options.queryParams;
        }
        if (options && options.errorHandler) {
            this.connection.error(options.errorHandler);
        }
        if (options && options.stateChanged) {
            this.connection.stateChanged(options.stateChanged);
        }
        //Adding additional property of promise allows to access it in rest of the application.
        if (options.autoConnect === undefined || options.autoConnect) {
            this.promise = this.connect();
        }
    }
    Hub.prototype.on = function (event, fn) {
        this.proxy.on(event, fn);
    };
    Hub.prototype.newConnection = function (options) {
        var connection = null;
        if (options && options.rootPath) {
            connection = $.hubConnection(options.rootPath, { useDefaultPath: false });
        }
        else {
            connection = $.hubConnection();
        }
        connection.logging = (options && options.logging ? true : false);
        return connection;
    };
    Hub.prototype.invoke = function (method) {
        var msg = [];
        for (var _i = 1; _i < arguments.length; _i++) {
            msg[_i - 1] = arguments[_i];
        }
        return this.proxy.invoke.apply(this.proxy, arguments);
    };
    ;
    Hub.prototype.getConnection = function (options) {
        var useSharedConnection = !(options && options.useSharedConnection === false);
        if (useSharedConnection) {
            return typeof Hub.globalConnections[options.rootPath] === 'undefined' ?
                Hub.globalConnections[options.rootPath] = this.newConnection(options) :
                Hub.globalConnections[options.rootPath];
        }
        else {
            return this.newConnection(options);
        }
    };
    Hub.prototype.connect = function (queryParams) {
        var startOptions = {};
        if (this.options.transport)
            startOptions.transport = this.options.transport;
        if (this.options.jsonp)
            startOptions.jsonp = this.options.jsonp;
        if (this.options.pingInterval !== undefined)
            startOptions.pingInterval = this.options.pingInterval;
        if (angular.isDefined(this.options.withCredentials))
            startOptions.withCredentials = this.options.withCredentials;
        if (queryParams)
            this.connection.qs = queryParams;
        return this.connection.start(startOptions);
    };
    ;
    Hub.prototype.disconnect = function () {
        this.connection.stop();
    };
    ;
    return Hub;
}());
//This will allow same connection to be used for all Hubs
//It also keeps connection as singleton.
Hub.globalConnections = [];
//# sourceMappingURL=hub-service.js.map