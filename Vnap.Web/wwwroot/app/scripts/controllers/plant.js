/**
 * PlantCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} Plant TBD.
 */
function PlantCtrl($scope, $rootScope, Plant, $http, Upload, cloudinary) {
    var _self = this;

    _self.Plant = new Plant();
    _self.Plant.GetAllPlants();
    $scope.plant = {};

    $scope.fileProgress = 0;
    $scope.uploadFiles = function(files) {
        $scope.files = files;

        if (!$scope.files || $scope.files.length > 2) {
            return;
        }

        angular.forEach(files,
            function(file) {
                if (file && !file.$error) {
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

    $scope.save = function() {
        $.ajax({
                method: "POST",
                url: apiBaseUrl + "plant/add",
                data: $scope.plant
            })
            .success(function(data) {
                debugger;
            });
    };
}