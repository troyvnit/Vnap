/**
 * PlantCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} Plant TBD.
 */
function PlantCtrl($scope, $rootScope, $uibModal, Plant) {
    $scope.Plant = new Plant();
    $scope.Plant.GetAllPlants();

    $scope.confirmDelete = function (plant) {
        $scope.deletedPlant = plant;
        $scope.modalInstance = $uibModal.open({
            templateUrl: 'views/modals/delete-confirm.html',
            scope: $scope
        });
    }

    $scope.ok = function () {
        $scope.Plant.Delete($scope.deletedPlant, function () {
            $scope.$apply(function () {
                var index = $scope.Plant.plants.indexOf($scope.deletedPlant);
                $scope.Plant.plants.splice(index, 1);
            });
        });
        $scope.modalInstance.close();
    };

    $scope.cancel = function () {
        $scope.modalInstance.dismiss('cancel');
    };
}