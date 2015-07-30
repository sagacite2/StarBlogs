(function () {
    var controllerId = 'app.views.stars.index';
    angular.module('app').controller(controllerId, ['abp.services.app.star', '$scope', '$modal', function (starService, $scope, $modal) {
        var vm = this;
        vm.sortingDirections = ['Name Asc', 'CreationTime Desc', 'LastUpdateTime Desc'];
        vm.displaySortings = {
            'Name Asc': '按名称',
            'CreationTime Desc': '按创建时间倒序',
            'LastUpdateTime Desc': '按修改时间倒序'
        }
        vm.stars = [];
        vm.sorting = 'CreationTime Desc';
        vm.loadStars = function (append) {
            var skipCount = append ? vm.stars.length : 0;
            abp.ui.setBusy(
                null,
               starService.getStars({
                   skipCount: skipCount,
                   sorting: vm.sorting
               }).success(function (data) {
                   if (append) {
                       for (var i = 0; i < data.items.length; i++) {
                           vm.stars.push(data.items[i]);
                       }
                   } else {
                       vm.stars = data.items;
                   }
                   vm.totalStarCount = data.totalCount;
               })
            );
        };
        $scope.items = {
            starId: -1
        };
        $scope.showNewStarDialog = function () {
            var modalInstance = $modal.open({
                templateUrl: abp.appPath + 'App/Main/views/manage/createEditStar.html',
                controller: 'app.views.stars.createEditStar as vm',
                animation: false,
                backdrop: false,
                size: 'md',
                resolve: {
                    items: function () {
                        return $scope.items;
                    }
                }
            });
            modalInstance.result.then(function () {
                vm.loadStars();
            });
        };

        $scope.showEditStarDialog = function (starId) {
            $scope.items.starId = starId;
            var modalInstance = $modal.open({
                templateUrl: abp.appPath + 'App/Main/views/manage/createEditStar.html',
                controller: 'app.views.stars.createEditStar as vm',
                animation: false,
                backdrop: false,
                size: 'md',
                resolve: {
                    items: function () {
                        return $scope.items;
                    }
                }
            });
            modalInstance.result.then(function () {
                vm.loadStars();
            });
        };
        $scope.blockStar = function (starId,isBlocked) {
            starService.blockStar({id:starId, isBlocked:isBlocked})
                       .success(function () {
                           vm.loadStars();
                       });
        };
        $scope.deleteStar = function (starId) {
            starService.deleteStar({ id: starId })
                       .success(function () {
                           vm.loadStars();
                       });
        };
        $scope.showBlogsDialog = function (starId) {
            $scope.items.starId = starId;
            var modalInstance = $modal.open({
                templateUrl: abp.appPath + 'App/Main/views/manage/createEditBlog.html',
                controller: 'app.views.stars.createEditBlog as vm',
                animation: false,
                backdrop: false,
                size: 'md',
                resolve: {
                    items: function () {
                        return $scope.items;
                    }
                }
            });
            modalInstance.result.then(function () {
                vm.loadStars();
            });
        };
        vm.sort = function (sortingDirection) {
            vm.sorting = sortingDirection;
            vm.loadStars();
        };
        vm.showMore = function () {
            vm.loadStars(true);
        };
        vm.loadStars();
    }]);
})();