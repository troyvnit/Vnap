/**
 * PlantFormCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} Plant TBD.
 */
function PlantFormCtrl($scope, $rootScope, $stateParams, $state, Plant, Upload, cloudinary, authService) {
    this.authentication = authService.authentication;

    if (this.authentication.isAuth && this.authentication.isAdmin) {
        //Welcome Message
    } else {
        $state.go('login');
    }

    $scope.Plant = new Plant();
    $scope.plant = { Id: 0 };
    
    if ($stateParams.Id && $stateParams.Id > 0) {
        $scope.Plant.Get($stateParams.Id, function (data) {
            $scope.$apply(function() {
                $scope.plant.Id = data.Id;
                $scope.plant.Name = data.Name;
                $scope.plant.Description = data.Description;
                $scope.plant.Priority = data.Priority;
                $scope.plant.Avatar = data.Avatar;
            });
        });
    }

    $scope.fileProgress = 0;
    $scope.uploadFiles = function(files) {
        $scope.files = files;

        if (!$scope.files || $scope.files.length > 2) {
            return;
        }

        angular.forEach(files,
            function(file) {
                if (file && !file.$error) {
                    $scope.fileProgress = 0.1;
                    file.upload = Upload.upload({
                            url: "https://api.cloudinary.com/v1_1/vnap/upload",
                            data: {
                                upload_preset: cloudinary.config().upload_preset,
                                api_key: cloudinary.config().api_key,
                                api_secret: cloudinary.config().api_secret,
                                tags: "plant-avatar",
                                context: "photo=" + $scope.title,
                                file: file
                            }
                        })
                        .progress(function(e) {
                            var progressValue = e.loaded * 100.0 / e.total;

                            $scope.fileProgress = Math.round(progressValue);
                            file.status = "Uploading... " + $scope.fileProgress + "%";
                        })
                        .success(function(data, status, headers, config) {

                            $scope.plant.Avatar = data.url;
                            $scope.fileProgress = 0;
                        })
                        .error(function(data, status, headers, config) {
                            file.result = data;
                            $scope.fileProgress = 0;
                        });
                }
            });
    };

    $scope.save = function () {
        if ($scope.plant.Id > 0) {
            $scope.Plant.Update($scope.plant, function() {
                $state.go("index.plant");
            });
        } else {
            $scope.Plant.Add($scope.plant, function () {
                $state.go("index.plant");
            });
        }
    };
}