angular.module('app')
   .controller('app.views.stars.manageStarTag', ['$scope','$rootScope', 'abp.services.app.star', '$modalInstance', 'items', function ($scope,$rootScope, starService, $modalInstance, items) {
       var vm = this;
       var starId = items.starId;
       vm.starTagSettings = items.starTagSettings;
       starService.getStar({ id: starId }).success(function (data) {
           vm.star = data;
           var i = vm.star.tags.length;
           while (i--) {
               var tag = vm.star.tags[i];
               vm['checked'+tag.id] = true;
           }

       });
       vm.save = function () {
           var tagsInput = [];
           var i = vm.starTagSettings.length;
           while (i--) {
               var tagSetting = vm.starTagSettings[i];
               if (vm['checked' + tagSetting.id] != undefined && vm['checked' + tagSetting.id]) {
                   tagsInput.push({ starId: starId, tagId: tagSetting.id });
               }
           }
           starService.updateStarTag(
               {
                   starId: starId,
                   starTags: tagsInput
               })
               .success(function () {
                   $modalInstance.close();
               });

       };
       vm.cancel = function () {
           $modalInstance.dismiss('cancel');
       };
   }]);