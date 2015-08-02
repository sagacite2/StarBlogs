angular.module('app')
   .controller('app.views.stars.createEditBlog', ['$scope', 'abp.services.app.star', 'abp.services.app.blog', '$modalInstance', 'items', function ($scope, starService, blogService, $modalInstance, items) {
       var vm = this;
       var starId = items.starId;
       var blogIds = [-1,-1,-1];
       starService.getStar({
           id: starId
       }).success(function (data) {
           vm.star = data;
           vm.star.blogs.forEach(function (blog) {
               vm['provider' + blog.provider] = blog['url'];
               blogIds[blog.provider - 1] = blog.id;
           });
       });
       vm.save = function () {
           if ($scope.blog_form.$valid) {
               blogService.createUpdateBlog(
                   {
                       starId: starId,
                       blogUrls: [vm.provider1, vm.provider2, vm.provider3],
                       blogIds: blogIds
                   })
                   .success(function () {
                       $modalInstance.close();
                   });
           }
       };
       vm.cancel = function () {
           $modalInstance.dismiss('cancel');
       };
   }]);