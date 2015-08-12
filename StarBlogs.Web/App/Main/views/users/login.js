(function () {

    $('#loginButton').click(function (e) {
        e.preventDefault();
        abp.ui.setBusy(
            $('#loginButton'),
            abp.ajax({
                url: abp.appPath + 'Account/Login',
                type: 'POST',
                data: JSON.stringify({
                    username: $('#UsernameInput').val(),
                    password: $('#PasswordInput').val(),
                    rememberMe: $('#RememberMeInput').is(':checked')
                })
            })
        );
    });
    angular.module('app')
    .controller('app.views.nav', ['$scope','$modal', function ($scope,$modal) {
        nav = this;
        nav.login = function (username, password, rememberMe) {
            abp.ajax({
                url: abp.appPath + 'Account/Login',
                type: 'POST',
                data: JSON.stringify({
                    username: username,
                    password: password,
                    rememberMe: rememberMe
                })
            })
        }
        nav.register = function () {
            var modalInstance = $modal.open({
                templateUrl: abp.appPath + 'App/Main/views/users/register.html',
                controller: 'app.views.user.register as vm',
                animation: false,
                backdrop: true,
                size: 'sm',
                resolve: {
                    items: function () {
                        return nav;
                    }
                }
            });
            modalInstance.result.then(function () {
                
            });
        };
    }]);

})();