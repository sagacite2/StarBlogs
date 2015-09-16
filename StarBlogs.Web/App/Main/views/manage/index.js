(function () {
    var controllerId = 'app.views.stars.index';

    angular.module('app')
        .controller(controllerId, ['abp.services.app.star', '$scope', '$modal', '$location', function (starService, $scope, $modal, $location) {
            var vm = this;
            vm.sortingDirections = ['Name Asc', 'CreationTime Desc', 'LastUpdateTime Desc'];
            vm.displaySortings = {
                'Name Asc': '按名称',
                'CreationTime Desc': '按创建时间倒序',
                'LastUpdateTime Desc': '按修改时间倒序'
            }

            //vm.stars = [];
            vm.sorting = 'CreationTime Desc';
            vm.starId = -1;
            vm.pageNumber = 3;          //每页数量
            vm.pageBodyNumber = 3;
            vm.maxCount = vm.totalStarCount != null ? vm.totalStarCount : 1;
            vm.loadStars = function (append, page) {
                page = page > 0 ? page : 0;
                page = page > vm.maxCount - 1 ? vm.maxCount - 1 : page;
                vm.currentPage = page > vm.maxCount - 1 ? vm.currentPage = vm.maxCount : vm.currentPage = page + 1;
                $scope.sePage = page;
                var skipCount = append ? vm.pageNumber * page : 0;
                abp.ui.setBusy(
                    null,
                   starService.getStars({
                       maxResultCount: vm.pageNumber,
                       skipCount: skipCount,
                       sorting: vm.sorting
                   }).success(function (data) {
                       vm.stars = [];
                       if (append) {
                           for (var i = 0; i < data.items.length; i++) {
                               vm.stars.push(data.items[i]);
                           }
                       } else {
                           vm.stars = data.items;
                       }
                       vm.totalStarCount = data.totalCount;
                       vm.pageNumberAll = [];
                       vm.pageFos = page;
                       vm.maxpageBodyNumber;
                       vm.pageLength = vm.maxCount = Math.ceil(vm.totalStarCount / vm.pageNumber);


                       if (page + vm.pageBodyNumber > vm.pageLength) {
                           vm.pageFos = page - ((page + vm.pageBodyNumber) - vm.pageLength);
                           vm.maxpageBodyNumber = vm.pageFos + vm.pageBodyNumber;
                       }
                       else {

                           if (vm.pageFos < Math.ceil(vm.pageBodyNumber / 2) - 1) {


                               vm.pageFos = (vm.pageFos + 1) - Math.ceil(vm.pageBodyNumber / 2);

                               vm.pageFos = 0;


                               vm.maxpageBodyNumber = vm.pageBodyNumber;

                           } else {

                               vm.pageFos = (vm.pageFos + 1) - Math.ceil(vm.pageBodyNumber / 2);

                               vm.maxpageBodyNumber = vm.pageFos + vm.pageBodyNumber;
                           }


                       }

                       for (var i = vm.pageFos; i < vm.maxpageBodyNumber; i++) {
                           vm.pageNumberAll.push({ id: i + 1 });
                       }





                   })
                );
            };

            vm.loadTagSettings = function () {
                starService.getStarTagSettings().success(function (data) {
                    vm.starTagSettings = data;
                    vm.starTagSettings.forEach(function (item) {
                        vm['tagName' + item.id] = item.tagName;
                    });

                });
            }
            vm.loadTagSettings();

            $scope.showNewStarDialog = function () {
                vm.starId = -1;
                var modalInstance = $modal.open({
                    templateUrl: abp.appPath + 'App/Main/views/manage/createEditStar.html',
                    controller: 'app.views.stars.createEditStar as vm',
                    animation: false,
                    backdrop: false,
                    size: 'md',
                    resolve: {
                        items: function () {
                            return vm;
                        }
                    }
                });
                modalInstance.result.then(function () {
                    vm.loadStars();
                });
            };

            $scope.showEditStarDialog = function (starId) {
                vm.starId = starId;
                var modalInstance = $modal.open({
                    templateUrl: abp.appPath + 'App/Main/views/manage/createEditStar.html',
                    controller: 'app.views.stars.createEditStar as vm',
                    animation: false,
                    backdrop: false,
                    size: 'md',
                    resolve: {
                        items: function () {
                            return vm;
                        }
                    }
                });
                modalInstance.result.then(function () {
                    vm.starId = -1;
                    vm.loadStars();
                });
            };
            $scope.blockStar = function (starId, isBlocked) {
                starService.blockStar({ id: starId, isBlocked: isBlocked })
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
                vm.starId = starId;
                var modalInstance = $modal.open({
                    templateUrl: abp.appPath + 'App/Main/views/manage/createEditBlog.html',
                    controller: 'app.views.stars.createEditBlog as vm',
                    animation: false,
                    backdrop: false,
                    size: 'md',
                    resolve: {
                        items: function () {
                            return vm;
                        }
                    }
                });
                modalInstance.result.then(function () {
                    vm.starId = -1;
                    vm.loadStars();
                });
            };
            $scope.showAddTagDialog = function (starId) {
                vm.starId = starId;
                var modalInstance = $modal.open({
                    templateUrl: abp.appPath + 'App/Main/views/manage/manageStarTags.html',
                    controller: 'manageStarTag as vm',
                    animation: false,
                    backdrop: false,
                    size: 'md',
                    resolve: {
                        items: function () {
                            return vm;
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
            vm.clickPaga = function (id) {
                var pageId;
                if (typeof id == 'boolean') {
                    if (id) {
                        pageId = $scope.sePage - 1;


                    } else {
                        pageId = $scope.sePage + 1;
                    }
                } else {
                    pageId = id - 1;
                    vm.currentPage = pageId + 1;
                }
                vm.loadStars(true, pageId);




            }

            vm.clickPaga(0);



        }]);
})();