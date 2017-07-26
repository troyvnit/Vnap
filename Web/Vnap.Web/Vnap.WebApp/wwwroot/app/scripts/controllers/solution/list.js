/**
 * SolutionCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} Solution TBD.
 */
function SolutionCtrl($scope, $rootScope, $uibModal, Solution, $state, authService) {
    this.authentication = authService.authentication;

    if (this.authentication.isAuth && this.authentication.isAdmin) {
        //Welcome Message
    } else {
        $state.go('login');
    }

    $scope.Solution = new Solution();
    $scope.Solution.GetAllSolutions();

    $scope.confirmDelete = function (solution, solutions) {
        $scope.deletedSolution = solution;
        $scope.solutions = solutions;
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

                if ($scope.solutions) {
                    index = $scope.solutions.indexOf($scope.deletedSolution);
                    $scope.solutions.splice(index, 1);
                }
            });
        });
        $scope.modalInstance.close();
    };

    $scope.cancel = function () {
        $scope.modalInstance.dismiss('cancel');
    };
}