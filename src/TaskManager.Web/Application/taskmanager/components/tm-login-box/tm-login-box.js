(function () {
    angular.module('taskManager').component('tmLoginBox', {
        templateUrl: 'Application/taskmanager/components/tm-login-box/tm-login-box.html',
        bindings: { },
        controller: ['tm.securityService', 'tm.socketsService', '$rootScope', function (securityService, socketsService, $rootScope) {
            var ctrl = this;

            ctrl.login = function () {
                ctrl.state = null;
                securityService.authenticate({
                    username: ctrl.loginName,
                    password: ctrl.loginPassword
                }).then(function () {
                    ctrl.loginName = null;
                    ctrl.loginPassword = null;
                }).catch(function () {
                    ctrl.state = 'Ошибка';
                });
            };

            ctrl.logOut = function () {
                ctrl.state = null;
                securityService.logOut();
                securityService.authPromise.then(function (data) {
                    ctrl.user = data;
                    securityService.logoutPromise.then(function (data) {
                        ctrl.user = null;
                    });
                });
            };

            securityService.authPromise.then(function (data) {
                ctrl.user = data;
                securityService.logoutPromise.then(function (data) {
                    ctrl.user = null;
                });
            });
        }]
    });
})();