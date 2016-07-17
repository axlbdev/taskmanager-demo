(function () {
    /**
     * Сервис для облегчения работы с сокетами
     */
    angular.module('tm.sockets').service('tm.socketsService', ['tm.securityService', 'Hub', '$rootScope', function (securityService, Hub, $rootScope) {
        var that = this;

        var hub = null;

        function connect() {
            return securityService.authPromise.then(function () {
                hub = new Hub('CommonHub', {
                    logging: true,
                    listeners: {
                        'receive': function (message) {
                            $rootScope.$apply(function () {
                                $rootScope.$emit('sockets-message-' + message.scope + ':' + message.type, message.payload);
                            });
                        }
                    }
                });
            }).then(function () {
                securityService.logoutPromise.then(function () {
                    hub.disconnect();
                    connect();
                });
            });
        };
        connect();

        return that;
    }]);
})();