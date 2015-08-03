(function () {
    var controllerId = 'app.views.stars.post';
    angular.module('app')
        .controller(controllerId, ['abp.services.app.post', '$modal', function (postService,  $modal) {
            var vm = this;
            vm.sortingDirections = ['PostTime Asc', 'PostTime Desc'];
            vm.displaySortings = {
                'PostTime Asc': '按发表时间顺序',
                'PostTime Desc': '按发表时间倒序',
            }
            vm.posts = [];
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
            vm.blockPost = function (postId, isBlocked) {
                postService.blockPost({ id: postId, isBlocked: isBlocked })
                           .success(function () {
                               vm.loadPosts();
                           });
            };
            vm.deletePost = function (postId) {
                starService.deletePost({ id: postId })
                           .success(function () {
                               vm.loadPosts();
                           });
            };
            vm.sort = function (sortingDirection) {
                vm.sorting = sortingDirection;
                vm.loadPosts();
            };
            vm.showMore = function () {
                vm.loadPosts(true);
            };

            vm.loadPosts();
        }]);
})();