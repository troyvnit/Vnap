/**
 * SolutionFormCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} Solution TBD.
 */
function SolutionFormCtrl($scope, $rootScope, $stateParams, $state, $http, $uibModal, Solution, Upload, cloudinary) {

    $scope.Solution = new Solution();
    $scope.solution = { Id: 0, Images: [] };
    
    if ($stateParams.Id && $stateParams.Id > 0) {
        $scope.Solution.Get($stateParams.Id, function (data) {
            $scope.$apply(function() {
                $scope.solution.Id = data.Id;
                $scope.solution.Name = data.Name;
                $scope.solution.CompanyName = data.CompanyName;
                $scope.solution.Description = data.Description;
                $scope.solution.Priority = data.Priority;
                $scope.solution.Prime = data.Prime;
                $scope.solution.Avatar = data.Avatar;
                $scope.solution.PlantDiseaseId = data.PlantDiseaseId;
                $scope.selectedPlantDisease = { Name: data.PlantDiseaseName, Id: data.PlantDiseaseId };
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
                                tags: "solution-avatar",
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

                            $scope.solution.Avatar = data.url;

                        })
                        .error(function(data, status, headers, config) {
                            file.result = data;
                        });
                }
            });
    };

    $scope.save = function () {
        if ($scope.solution.Id > 0) {
            $scope.Solution.Update($scope.solution, function() {
                $state.go("index.solution");
            });
        } else {
            $scope.Solution.Add($scope.solution, function () {
                $state.go("index.solution");
            });
        }
    };

    $scope.searchPlantDisease = function (val) {
        return $http.get(apiBaseUrl + 'plantDisease/search', {
            params: {
                query: val
            }
        }).then(function (response) {
            return response.data.map(function (item) {
                return item;
            });
        });
    };

    $scope.onSelectPlantDisease = function (item, model, label) {
        $scope.solution.PlantDiseaseId = item.Id;
    }
}