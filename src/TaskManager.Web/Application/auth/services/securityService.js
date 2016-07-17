(function () {
    /**
     * Сервис для облегчения работы с аутентификацией/авторизацией
     */
    angular.module('tm.auth').service('tm.securityService', ['$http', '$q', '$sessionStorage', function ($http, $q, $sessionStorage) {
        var that = this;
        var authenticatedDefer = $q.defer();
        var logoutDefer = null;

        var session = null;
        if ($sessionStorage.authData) {
            authenticated($sessionStorage.authData);
        }

        /** авторизовать пользователя */
        that.authenticate = function (credentials) {
            return $http({
                    method: 'POST',
                    url: 'Token',
                    data: $.param(angular.extend({}, { grant_type: 'password' }, credentials)),
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (response) {
                authenticated(response.data);
            });
        };

        /** выход */
        that.logOut = function () {
            authenticatedDefer = $q.defer();

            return $http.post('api/Account/Logout')
            .then(function (response) {
                var authData = $sessionStorage.authData || {};
                logoutDefer.resolve({ name: authData.userName });
                $sessionStorage.authData = null;
                $http.defaults.headers.common.Authorization = null;
            });
        };

        /** авторизационный промис. все, чему нужна аутентификация, должны проверять этот промис. */
        Object.defineProperty(that, 'authPromise', {
            get: function () {
                return (authenticatedDefer || {}).promise;
            }
        });
        /** аналогично для Logout. */
        Object.defineProperty(that, 'logoutPromise', {
            get: function () {
                return (logoutDefer || {}).promise;
            }
        });

        /** callback для аутентификации */
        function authenticated(authData) {
            logoutDefer = $q.defer();
            session = authData;
            $sessionStorage.authData = session;
            authenticatedDefer.resolve({ name: authData.userName });
            $http.defaults.headers.common.Authorization = 'Bearer ' + session.access_token;
        };

        return that;
    }]);
})();