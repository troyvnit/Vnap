/**
 * UserCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} User TBD.
 */
function UserCtrl($scope, $rootScope, $uibModal, User, Upload, cloudinary, $state, authService) {
    this.authentication = authService.authentication;

    if (this.authentication.isAuth && (this.authentication.isAdmin || this.authentication.isMod)) {
        //Welcome Message
    } else {
        $state.go('login');
    }

    $scope.User = new User();
    $scope.User.GetAllUsers();

    $scope.confirmDelete = function (user) {
        $scope.deletedUser = user;
        $scope.modalInstance = $uibModal.open({
            templateUrl: 'wwwroot/app/views/modals/delete-confirm.html',
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