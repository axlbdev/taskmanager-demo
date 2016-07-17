(function () {
    /** основной модуль приложения */
    angular.module('taskManager', ['tm.sockets', 'tm.auth', 'ui.router', 'ngResource'])
    .config(['$resourceProvider', function($resourceProvider) {
        $resourceProvider.defaults.actions.query.isArray = false;
    }]);
})();