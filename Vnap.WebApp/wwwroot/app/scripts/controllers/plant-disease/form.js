/**
 * PlantDiseaseFormCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} PlantDisease TBD.
 */
function PlantDiseaseFormCtrl($scope, $rootScope, $stateParams, $state, $http, $uibModal, PlantDisease, Upload, cloudinary) {

    $scope.PlantDisease = new PlantDisease();
    $scope.plantDisease = { Id: 0, Images: [] };
    
    if ($stateParams.Id && $stateParams.Id > 0) {
        $scope.PlantDisease.Get($stateParams.Id, function (data) {
            $scope.$apply(function() {
                $scope.plantDisease.Id = data.Id;
                $scope.plantDisease.Name = data.Name;
                $scope.plantDisease.Description = data.Description;
                $scope.plantDisease.Priority = data.Priority;
                $scope.plantDisease.Avatar = data.Avatar;
                $scope.plantDisease.PlantId = data.PlantId;
                $scope.selectedPlant = { Name: data.PlantName, Id: data.PlantId };
                $scope.plantDisease.PlantDiseaseType = data.PlantDiseaseType;
                $scope.plantDisease.Images = data.Images;
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
                                tags: "plant-disease-avatar",
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

                            $scope.plantDisease.Avatar = data.url;

                        })
                        .error(function(data, status, headers, config) {
                            file.result = data;
                        });
                }
            });
    };

    $scope.save = function () {
        if ($scope.plantDisease.Id > 0) {
            $scope.PlantDisease.Update($scope.plantDisease, function() {
                $state.go("index.plant-disease");
            });
        } else {
            $scope.PlantDisease.Add($scope.plantDisease, function () {
                $state.go("index.plant-disease");
            });
        }
    };

    $scope.searchPlant = function (val) {
        return $http.get(apiBaseUrl + 'plant/search', {
            params: {
                query: val
            }
        }).then(function (response) {
            return response.data.map(function (item) {
                return item;
            });
        });
    };

    $scope.onSelectPlant = function (item, model, label) {
        $scope.plantDisease.PlantId = item.Id;
    }

    $scope.plantDiseaseTypes = [{ Name: 'Bệnh hại', Id: 0 }, { Name: 'Sâu hại', Id: 1 }];

    $scope.onSelectedDiseaseType = function (item) {
        $scope.plantDisease.PlantDiseaseType = item.Id;
    }


    $scope.imageProgress = 0;
    $scope.uploadImages = function (images) {
        $scope.images = images;
        
        if (!$scope.images || $scope.images.length > 2) {
            return;
        }

        angular.forEach(images,
            function (image) {
                if (image && !image.$error) {
                    $scope.imageProgress = 0.1;
                    image.upload = Upload.upload({
                        url: "https://api.cloudinary.com/v1_1/vnap/upload",
                        data: {
                            upload_preset: cloudinary.config().upload_preset,
                            api_key: cloudinary.config().api_key,
                            api_secret: cloudinary.config().api_secret,
                            tags: "plant-disease-image",
                            context: "photo=" + $scope.title,
                            file: image
                        }
                    })
                        .progress(function (e) {
                            var progressValue = e.loaded * 100.0 / e.total;

                            $scope.imageProgress = Math.round(progressValue);
                            image.status = "Uploading... " + $scope.imageProgress + "%";
                        })
                        .success(function (data, status, headers, config) {

                            $scope.PlantDisease.AddImage({ PlantDiseaseId: $scope.plantDisease.Id, Url: data.url }, function (image) {
                                $scope.$apply(function() {
                                    $scope.plantDisease.Images.push(image);
                                });
                            });

                        })
                        .error(function (data, status, headers, config) {
                            image.result = data;
                        });
                }
            });
    };

    $scope.imageChanged = function(image) {
        $scope.PlantDisease.UpdateImage(image, function () {
            console.log('Updated image');
        });
    }

    $scope.confirmDeleteImage = function (image) {
        $scope.deletedImage = image;
        $scope.modalInstance = $uibModal.open({
            templateUrl: 'views/modals/delete-confirm.html',
            scope: $scope
        });
    }

    $scope.ok = function () {
        $scope.PlantDisease.DeleteImage($scope.deletedImage, function () {
            $scope.$apply(function () {
                var index = $scope.plantDisease.Images.indexOf($scope.deletedImage);
                $scope.plantDisease.Images.splice(index, 1);
            });
        });
        $scope.modalInstance.close();
    };

    $scope.cancel = function () {
        $scope.modalInstance.dismiss('cancel');
    };
}