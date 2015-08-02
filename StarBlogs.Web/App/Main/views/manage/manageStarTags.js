angular.module('app')
   .controller('manageStarTag', ['abp.services.app.star', '$modalInstance', 'items', function (starService, $modalInstance, items) {
       var vm = this;
       var starId = items.starId;
       vm.starTagSettings = items.starTagSettings;
       starService.getStar({ id: starId })
           .success(function (data) {
               vm.star = data;
               vm.star.tags.forEach(function (starTag) {
                   vm['checked' + starTag.tagId] = true;
               });
           });
       vm.save = function () {
           var tagsInput = [];
           vm.starTagSettings.forEach(function (tagSetting) {
               if (vm['checked' + tagSetting.id] && vm['checked' + tagSetting.id]) {
                   tagsInput.push({ starId: starId, tagId: tagSetting.id });
               }
           });
           starService.updateStarTag({starId: starId, starTags: tagsInput})
               .success(function () {
                   $modalInstance.close();
               });
       };
       vm.cancel = function () {
           $modalInstance.dismiss('cancel');
       };
   }]);