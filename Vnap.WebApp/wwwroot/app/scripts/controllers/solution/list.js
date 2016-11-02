/**
 * SolutionCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} Solution TBD.
 */
function SolutionCtrl($scope, $rootScope, $uibModal, Solution) {
    $scope.Solution = new Solution();
    $scope.Solution.GetAllSolutions();

    $scope.confirmDelete = function (solution) {
        $scope.deletedSolution = solution;
        $scope.modalInstance = $uibModal.open({
            templateUrl: 'views/modals/delete-confirm.html',
            scope: $scope
        });
    }

    $scope.ok = function () {
        $scope.Solution.Delete($scope.deletedSolution, function () {
            $scope.$apply(function () {
                var index = $scope.Solution.solutions.indexOf($scope.deletedSolution);
                $scope.Solution.solutions.splice(index, 1);
            });
        });
        $scope.modalInstance.close();
    };

    $scope.cancel = function () {
        $scope.modalInstance.dismiss('cancel');
    };
}