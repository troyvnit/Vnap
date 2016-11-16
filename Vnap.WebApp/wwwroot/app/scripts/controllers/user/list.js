/**
 * UserCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} User TBD.
 */
function UserCtrl($scope, $rootScope, $uibModal, User) {
    $scope.User = new User();
    $scope.User.GetAllUsers();

    $scope.confirmDelete = function (user) {
        $scope.deletedUser = user;
        $scope.modalInstance = $uibModal.open({
            templateUrl: 'views/modals/delete-confirm.html',
            scope: $scope
        });
    }

    $scope.ok = function () {
        $scope.User.Delete($scope.deletedUser, function () {
            $scope.$apply(function () {
                var index = $scope.User.users.indexOf($scope.deletedUser);
                $scope.User.users.splice(index, 1);
            });
        });
        $scope.modalInstance.close();
    };

    $scope.cancel = function () {
        $scope.modalInstance.dismiss('cancel');
    };
}