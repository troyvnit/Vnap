/**
 * PlantDiseaseCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} PlantDisease TBD.
 */
function PlantDiseaseCtrl($scope, $rootScope, $uibModal, PlantDisease) {
    $scope.PlantDisease = new PlantDisease();
    $scope.PlantDisease.GetAllPlantDiseases();

    $scope.confirmDelete = function (plantDisease) {
        $scope.deletedPlantDisease = plantDisease;
        $scope.modalInstance = $uibModal.open({
            templateUrl: 'views/modals/delete-confirm.html',
            scope: $scope
        });
    }

    $scope.ok = function () {
        $scope.PlantDisease.Delete($scope.deletedPlantDisease, function () {
            $scope.$apply(function () {
                var index = $scope.PlantDisease.plantDiseases.indexOf($scope.deletedPlantDisease);
                $scope.PlantDisease.plantDiseases.splice(index, 1);
            });
        });
        $scope.modalInstance.close();
    };

    $scope.cancel = function () {
        $scope.modalInstance.dismiss('cancel');
    };
}