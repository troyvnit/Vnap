/**
 * SettingCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} Setting TBD.
 */
function SettingCtrl($scope, $rootScope, $uibModal, Setting, $state, authService) {
    this.authentication = authService.authentication;

    if (this.authentication.isAuth && this.authentication.isAdmin) {
        //Welcome Message
    } else {
        $state.go('login');
    }

    $scope.Setting = new Setting();
    $scope.Setting.GetAllSettings();

    $scope.confirmDelete = function (setting) {
        $scope.deletedSetting = setting;
        $scope.modalInstance = $uibModal.open({
            templateUrl: 'wwwroot/app/views/modals/delete-confirm.html',
            scope: $scope
        });
    }

    $scope.ok = function () {
        $scope.Setting.Delete($scope.deletedSetting, function () {
            $scope.$apply(function () {
                var index = $scope.Setting.settings.indexOf($scope.deletedSetting);
                $scope.Setting.settings.splice(index, 1);
            });
        });
        $scope.modalInstance.close();
    };

    $scope.cancel = function () {
        $scope.modalInstance.dismiss('cancel');
    };

    $scope.dataTypes = [{ Name: 'Chuỗi', Id: 0 }, { Name: 'Số', Id: 1 }, { Name: 'Luận lý', Id: 2 }, { Name: 'Ngày', Id: 3 }];
}