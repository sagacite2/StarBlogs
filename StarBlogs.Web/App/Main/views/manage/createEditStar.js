angular.module('app')
    .directive('ensureUnique', ['$http', function ($http) {
        return {
            require: 'ngModel',
            link: function (scope, ele, attrs, c) {
                scope.$watch(attrs.ngModel, function () {
                    $http({
                        method: 'POST',
                        url: '/api/check/' + attrs.ensureUnique,
                        data: { 'field': attrs.ensureUnique }
                    }).success(function (data, status, headers, cfg) {
                        c.$setValidity('unique', data.isUnique);
                    }).error(function (data, status, headers, cfg) {
                        c.$setValidity('unique', false);
                    });
                });
            }
        };
    }])
    .directive('ngFocus', [function () {
        var FOCUS_CLASS = "ng-focused";
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ctrl) {
                ctrl.$focused = false;
                element.bind('focus', function (evt) {
                    element.addClass(FOCUS_CLASS);
                    scope.$apply(function () { ctrl.$focused = true; });
                }).bind('blur', function (evt) {
                    element.removeClass(FOCUS_CLASS);
                    scope.$apply(function () { ctrl.$focused = false; });
                });
            }
        }
    }])
    .controller('app.views.stars.createEditStar', [
        '$scope',
        'abp.services.app.star',
        '$modalInstance',
        'items',
        '$modal',
        function ($scope, starService, $modalInstance, items, $modal) {
            var vm = this;
            vm.starId = items.starId;
            if (vm.starId != -1) {
                starService.getStar({
                    id: vm.starId
                }).success(function (data) {
                    vm.star = data;
                    vm.star.blogUrl = vm.star.blogs[0].url;
                });
                vm.save = function () {
                    if ($scope.star_form.$valid) {
                        starService
                            .updateStar(vm.star)
                            .success(function () {
                                $modalInstance.close();
                            });
                    }
                };
            } else {
                vm.star = {
                    id: -1,
                    name: '',
                    nickname: '',
                    chineseName: '',
                    gender: 2,
                    description: '',
                };
                vm.save = function () {
                    if ($scope.star_form.$valid) {
                        starService
                            .createStar(vm.star)
                            .success(function () {
                                $modalInstance.close();
                            });
                    }
                };
            }
            vm.cancel = function () {
                $modalInstance.dismiss('cancel');
            };
            $scope.showBlogsDialog = function () {
                $modalInstance.dismiss('cancel');
                var modalInstance = $modal.open({
                    templateUrl: abp.appPath + 'App/Main/views/manage/createEditBlog.html',
                    controller: 'app.views.stars.createEditBlog as vm',
                    animation: false,
                    backdrop: false,
                    size: 'md',
                    resolve: {
                        items: function () {
                            return items;
                        }
                    }
                });
                modalInstance.result.then(function () {
                    vm.loadStars();
                });
            };
        }]);


