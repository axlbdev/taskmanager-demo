(function () {
    angular.module('taskManager').controller('tm.tasksController', ['$resource', '$q', 'tm.securityService', '$rootScope', function ($resource, $q, securityService, $rootScope) {
        var that = this;

        var Task = $resource('api/task');
        securityService.authPromise.then(function () {
            Task.query({ take: 999, skip: 0, query: {} }, function (data) {
                that.tasks = data.page;
            });
        });

        that.add = function () {
            that.tasks.unshift({ _edit: true });
        };

        that.save = function (task) {
            that.savePromise = $q.when(task.$promise).then(function () {
                task.$promise = Task.save(task).$promise.then(function (saved) {
                    task.id = saved.id;
                });
                return task.$promise;
            });
            return that.savePromise;
        };

        that.delete = function (task) {
            that.tasks.splice(that.tasks.indexOf(task), 1);
            that.deletePromise = $q.when(task.$promise).then(function () {
                if (task.id) {
                    task.$promise = Task.delete(task).$promise;
                }
                return task.$promise;
            });
            return that.deletePromise;
        };


        $rootScope.$on('sockets-message-task:new', function (evt, task) {
            $q.when(that.savePromise).then(function () {
                if (!that.tasks.some(function (item) {
                    return item.id == task.id;
                })) {
                    that.tasks.unshift(task);
                }
            });
        });
        $rootScope.$on('sockets-message-task:update', function (evt, task) {
            that.tasks.filter(function (item) {
                return item.id == task.id;
            }).map(function (item) {
                item.name = task.name;
            });
        });
        $rootScope.$on('sockets-message-task:delete', function (evt, task) {
            that.tasks.filter(function (item) {
                return item.id == task.id;
            }).map(function (item, i) {
                that.tasks.splice(i, 1);
            });
        });
    }]);
})();