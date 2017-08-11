/**
 * SettingFormCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} Setting TBD.
 */
function SettingFormCtrl($scope, $rootScope, $stateParams, $state, Setting, Upload, cloudinary, authService) {
    this.authentication = authService.authentication;

    if (this.authentication.isAuth && this.authentication.isAdmin) {
        //Welcome Message
    } else {
        $state.go('login');
    }

    $scope.Setting = new Setting();
    $scope.setting = { Id: 0 };
    
    if ($stateParams.Id && $stateParams.Id > 0) {
        $scope.Setting.Get($stateParams.Id, function (data) {
            $scope.$apply(function() {
                $scope.setting.Id = data.Id;
                $scope.setting.Key = data.Key;
                $scope.setting.Name = data.Name;
                $scope.setting.Value = data.Value;
                $scope.setting.DataType = data.DataType;
            });
        });
    }

    $scope.dataTypes = [{ Name: 'Chuỗi', Id: 0 }, { Name: 'Số', Id: 1 }, { Name: 'Luận lý', Id: 2 }, { Name: 'Ngày', Id: 3 }];

    $scope.save = function () {
        if ($scope.setting.Id > 0) {
            $scope.Setting.Update($scope.setting, function() {
                $state.go("index.setting");
            });
        } else {
            $scope.Setting.Add($scope.setting, function () {
                $state.go("index.setting");
            });
        }
    };
}