(function () {
    var controllerId = 'app.views.stars.post';
    angular.module('app')
        .controller(controllerId, ['abp.services.app.post', '$modal', function (postService, $modal) {
            var vm = this;
            vm.showing = 'NewPosts';
            vm.sortingDirections = ['PostTime Asc', 'PostTime Desc'];
            vm.displaySortings = {
                'PostTime Asc': '按发表时间顺序',
                'PostTime Desc': '按发表时间倒序',
            }
            vm.posts = [];
            vm.translate = [];
            vm.sorting = 'PostTime Desc';
            vm.starId = -1;
            vm.loadPosts = function (append) {
                var skipCount = append ? vm.posts.length : 0;
                abp.ui.setBusy(
                    null,
                   postService.getPosts({
                       skipCount: skipCount,
                       sorting: vm.sorting
                   }).success(function (data) {
                       console.log(data)
                       if (append) {
                           data.items.forEach(function (item) {
                               vm.posts.push(item);
                           });

                       } else {
                           vm.posts = data.items;
                       }
                       vm.totalPostCount = data.totalCount;
                   })
                );

            };

            vm.deletePictureCon = function (postid) {
                if (confirm("确认要删除？")) {
                    postService.deletePicture({ id: postid }).success(function () {
                        vm.loadPosts();
                    })

                }

            }

            vm.blockPost = function (postId, isBlocked) {
                postService.blockPost({ id: postId, isBlocked: isBlocked })
                           .success(function () {
                               vm.loadPosts();
                           });
            };
            vm.deletePost = function (postId) {

                if (confirm("确认要删除？")) {
                    postService.deletePost({ id: postId })
                           .success(function () {
                               vm.loadPosts();
                           });

                }

            };
            vm.loadTranslate = function (append) {

            };
            vm.loadTranslateByPostId = function (postId, append) {
                var skipCount = append ? vm.translates.length : 0;
                abp.ui.setBusy(
                    null,
                   translateService.getTranslateByPost({
                       skipCount: skipCount,
                       sorting: vm.sorting
                   }).success(function (data) {
                       if (append) {
                           data.items.forEach(function (item) {
                               vm.translates.push(item);
                           });
                       } else {
                           vm.translates = data.items;
                       }
                       vm.totalTranslateCount = data.totalCount;
                   })
                );
            };
            vm.sort = function (sortingDirection) {
                vm.sorting = sortingDirection;
                vm.loadPosts();
            };
            vm.showMore = function () {
                switch (vm.showing) {
                    case 'NewPosts':
                        vm.loadPosts(true);
                        break;
                    default:
                        vm.loadTranslate(true);
                }
            };
            vm.switchShow = function (showing) {
                vm.showing = showing;
                switch (vm.showing) {
                    case 'NewPosts':
                        vm.loadPosts();
                        break;
                    default:
                        vm.loadTranslate();
                }
            }


            vm.loadTranslate();

        }]);



    angular.module('app').controller('test', ['$scope', 'abp.services.app.post', function ($scope, testServices) {


        testServices.getPosts({ skipCount: 0, sorting: 'PostTime Desc' }).success(function (data) {
            console.log(data)
            $scope.items = data

        }).error(function (data) {
            console.log('失败')
        });

        $scope.delectTest = function (id) {

            testServices.deletePost(id);
            console.log('成功删除')

        }



    }])


})();