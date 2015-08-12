angular.module('app')
.controller('app.views.user.register', ['items', '$scope', '$modalInstance', function (items, $scope, $modalInstance) {
    var vm = this;
    vm.reg = function () {
        abp.ajax({
            url: abp.appPath + 'Account/Register',
            type: 'POST',
            data: JSON.stringify({
                userName: vm.userName,
                password: vm.password
            })
        })
    };
    vm.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

}]);