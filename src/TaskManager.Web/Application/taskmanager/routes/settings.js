(function () {
    angular.module('taskManager').config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('root.index.settings', {
                url: 'settings',
                views: {
                    '': { templateUrl: 'Application/taskmanager/views/settings/settings.html', controller: 'tm.settingsController as $tmSettings' }
                }
            });
    }]);
})();
