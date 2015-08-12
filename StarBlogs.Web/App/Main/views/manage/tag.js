angular.module('app')
.controller('app.views.stars.tag', ['abp.services.app.star', function (starService) {
    vm = this;
    vm.loadTagSettings = function () {
        abp.ui.setBusy(
            null,
            starService.getStarTagSettings().success(function (data) {
                vm.tags = data;
            })
        );
    };
    vm.loadTagSettings();
    vm.save = function (id, name) {
        starService.updateTagSetting({ id: id, name: name }).success(function () {
            vm.loadTagSettings();
        })
    };
    vm.create = function (name) {
        if (name) {
            starService.createTagSetting({ name: name }).success(function () {
                vm.loadTagSettings();
            });
        }
    };
}
]);