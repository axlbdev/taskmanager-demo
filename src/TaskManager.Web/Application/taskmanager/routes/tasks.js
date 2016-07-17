(function () {
    angular.module('taskManager').config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('root.index.tasks', {
                url: 'tasks',
                views: {
                    '': { templateUrl: 'Application/taskmanager/views/tasks/tasks.html', controller: 'tm.tasksController as $tmTasks' }
                }
            });
    }]);
})();
