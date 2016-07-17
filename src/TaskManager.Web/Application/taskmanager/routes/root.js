(function () {
    angular.module('taskManager').config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('root', {
                abstract: true,
                template: '<div ui-view></div>',
                controller: 'tm.rootController as $tmRoot'
            })
            .state('root.index', {
                url: '/',
                templateUrl: 'Application/taskmanager/views/index/index.html',
                controller: 'tm.indexController as $tmIndex'
            });
        $urlRouterProvider.otherwise('/');
    }]);
})();
