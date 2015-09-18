(function () {
    var controllerId = 'app.views.stars.post';
    angular.module('app')
        .controller(controllerId, ['abp.services.app.post', 'abp.services.app.star', '$modal','$scope', function (postService, starService, $modal,$scope) {
            var vm = this;
            vm.showing = 'NewPosts';
            vm.sortingDirections = ['PostTime Asc', 'PostTime Desc'];
            vm.displaySortings = {
                'PostTime Asc': '按发表时间顺序',
                'PostTime Desc': '按发表时间倒序',
            }
            //vm.posts = [];
            vm.translate = [];
            vm.sorting = 'PostTime Desc';
            vm.starId = -1;
            vm.bolst; //存储微博是否为id取值
            //分页
            vm.pageNumber = 1;          //每页数量
            vm.pageBodyNumber = 7;
            //分页end
            vm.maxCount = vm.totalPostCount != null ? vm.totalPostCount : 1;
            vm.loadPosts = function (append, page) {
                vm.bolst = true;
                page = page > 0 ? page : 0;
                page = page > vm.maxCount - 1 ? vm.maxCount - 1 : page;
                vm.currentPage = page > vm.maxCount - 1 ? vm.currentPage = vm.maxCount : vm.currentPage = page + 1;
                $scope.sePage = page;
                //var skipCount = append ? vm.posts.length : 0;
                var skipCount = append ? vm.pageNumber * page : 0;               
                abp.ui.setBusy(
                    null,
                   postService.getPosts({
                       maxResultCount: vm.pageNumber,
                       skipCount: skipCount,
                       sorting: vm.sorting
                   }).success(function (data) {
                      console.log(data)
                       vm.posts = [];
                       if (append) {
                           data.items.forEach(function (item) {
                               vm.posts.push(item);
                           });

                       } else {
                           vm.posts = data.items;
                       }
                       vm.totalPostCount = data.totalCount;
                      
                       //分页
                       vm.pageNumberAll = [];
                       vm.pageFos = page;
                       vm.maxpageBodyNumber;
                       vm.pageLength = vm.maxCount = Math.ceil(vm.totalPostCount / vm.pageNumber);
                       if (vm.pageLength <= vm.pageBodyNumber) {
                           vm.pageFos = 0;
                           vm.maxpageBodyNumber = vm.pageLength;
                       }
                       else {
                           if (page + vm.pageBodyNumber > vm.pageLength) {
                               vm.pageFos = page - ((page + vm.pageBodyNumber) - vm.pageLength);
                               vm.maxpageBodyNumber = vm.pageFos + vm.pageBodyNumber;
                           } else {

                               if (vm.pageFos < Math.ceil(vm.pageBodyNumber / 2) - 1) {


                                   vm.pageFos = (vm.pageFos + 1) - Math.ceil(vm.pageBodyNumber / 2);

                                   vm.pageFos = 0;


                                   vm.maxpageBodyNumber = vm.pageBodyNumber;

                               } else {

                                   vm.pageFos = (vm.pageFos + 1) - Math.ceil(vm.pageBodyNumber / 2);

                                   vm.maxpageBodyNumber = vm.pageFos + vm.pageBodyNumber;
                               }


                           }
                       }                     
                     for (var i = vm.pageFos; i < vm.maxpageBodyNumber; i++) {
                           vm.pageNumberAll.push({ id: i + 1 });
                       }                       
                       //分页end
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
                        vm.loadPosts(true,0);
                        break;
                    default:
                        vm.loadTranslate(true);
                }
            };
            vm.switchShow = function (showing) {
                vm.showing = showing;
                switch (vm.showing) {
                    case 'NewPosts':
                        vm.loadPosts(true,0);
                        break;
                    default:
                        vm.loadTranslate();
                }
            }

            vm.starNameFunt = function () {     //获取明星姓名
                starService.getStars({ MaxResultCount: 999, skipCount: 0, Sorting: 'CreationTime Desc' }).success(function (data) {
                    vm.starName = data.items;
                });
            }


            vm.getPostsByStarMxBw = function (id, page) {         //根据明星id 获取所有博文
                vm.bolst = false;
                page = page > 0 ? page : 0
                vm.pageFos = page;
                page = page > vm.maxCount - 1 ? vm.maxCount - 1 : page;              
                vm.currentPage = page > vm.maxCount - 1 ? vm.currentPage = vm.maxCount : vm.currentPage = page + 1;
                $scope.sePage = page;
                //var skipCount = append ? vm.posts.length : 0;
                var skipCount = true ? vm.pageNumber * page : 0;
                abp.ui.setBusy(
                    null,
                   postService.getPostsByStar({
                       starId: id,
                       maxResultCount:vm.pageNumber,
                       skipCount: skipCount,
                       sorting: vm.sorting
                   }).success(function (data) {
                       
                       vm.posts = [];
                       vm.posts = data.items;
                       vm.totalPostCount = data.totalCount;
                       console.log(data)
                       
                       //分页
                       vm.pageNumberAll = [];                       
                       vm.maxpageBodyNumber;                     
                       vm.pageLength = vm.maxCount = Math.ceil(vm.totalPostCount / vm.pageNumber);

                       if (vm.pageLength <= vm.pageBodyNumber) {
                           vm.pageFos = 0;
                           vm.maxpageBodyNumber = vm.pageLength;
                       }
                       else {
                           if (page + vm.pageBodyNumber > vm.pageLength) {
                               vm.pageFos = page - ((page + vm.pageBodyNumber) - vm.pageLength);
                               vm.maxpageBodyNumber = vm.pageFos + vm.pageBodyNumber;
                           } else {

                               if (vm.pageFos < Math.ceil(vm.pageBodyNumber / 2) - 1) {
                                   vm.pageFos = (vm.pageFos + 1) - Math.ceil(vm.pageBodyNumber / 2);
                                   vm.pageFos = 0;
                                   vm.maxpageBodyNumber = vm.pageBodyNumber;
                               } else {
                                   vm.pageFos = (vm.pageFos + 1) - Math.ceil(vm.pageBodyNumber / 2);
                                   vm.maxpageBodyNumber = vm.pageFos + vm.pageBodyNumber;
                               }
                           }
                       }
                       for (var i = vm.pageFos; i < vm.maxpageBodyNumber; i++) {
                           vm.pageNumberAll.push({ id: i + 1 });
                       }
                       //分页end
                   })

                );
                vm.StarNameID = id;
            }


            vm.clickPaga = function (id) {
                var pageId;
                if (id == 'left') {
                    pageId = $scope.sePage - 1;

                } else if (id == 'right') {
                    pageId = $scope.sePage + 1;
                } else {
                    pageId = id - 1;
                    vm.currentPage = pageId + 1;
                }
                if (vm.bolst)            //判断参数bol为true 显示最近微博
                {
                    vm.loadPosts(true, pageId);
                } else {            //flase 显示明星id
                    vm.getPostsByStarMxBw(vm.StarNameID, pageId);
                }


            }
            //添加博文的测试
            vm.addBowen = function () {
                postService.createUpdatePost({BlogId:14,Content:'你好啊你好',DefaultTranslate:'ssss'});
            }

            //vm.addBowen();//post
            vm.starNameFunt();
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