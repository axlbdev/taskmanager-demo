(function () {
    angular.module('taskManager').component('tmTaskEdit', {
        templateUrl: 'Application/taskmanager/components/tm-task-edit/tm-task-edit.html',
        bindings: {
            task: '=',
            onUpdated: '&',
            onRemoved: '&'
        },
        controller: [function(){
            var ctrl = this;
            Object.defineProperties(ctrl, {
                item: {
                    get: function () {
                        if (ctrl.task && !ctrl.task.id && !ctrl.task.name && !ctrl.task._edited) {
                            ctrl.editMode = true;
                        }
                        return ctrl.task;
                    },
                    set: function (value) {
                        ctrl.task = value;
                    }
                }
            });

            ctrl.edit = function () {
                ctrl.taskName = ctrl.task.name;
                ctrl.editMode = true;
            };
            ctrl.accept = function () {
                ctrl.task.name = ctrl.taskName;
                ctrl.task._edited = true;
                ctrl.editMode = false;
                ctrl.onUpdated({ task: ctrl.task });
            };
            ctrl.reset = function () {
                ctrl.taskName = ctrl.task.name;
                ctrl.editMode = false;
            };
            ctrl.delete = function () {
                ctrl.onRemoved({ task: ctrl.task });
            };
        }]
    });
})();