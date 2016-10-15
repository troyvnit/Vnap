/**
 * PlantFormCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} Plant TBD.
 */
function PlantFormCtrl($scope, $rootScope, $stateParams, $state, Plant, Upload, cloudinary) {

    $scope.Plant = new Plant();
    $scope.plant = { Id: 0 };
    
    if ($stateParams.id && $stateParams.id > 0) {
        $scope.Plant.Get($stateParams.id, function (data) {
            $scope.$apply(function() {
                $scope.plant.Id = data.id;
                $scope.plant.Name = data.name;
                $scope.plant.Description = data.description;
                $scope.plant.Priority = data.priority;
                $scope.plant.Avatar = data.avatar;
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

                        })
                        .error(function(data, status, headers, config) {
                            file.result = data;
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