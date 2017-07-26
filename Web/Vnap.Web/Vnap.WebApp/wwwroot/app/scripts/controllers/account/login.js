/**
 * LoginCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} User TBD.
 */
function LoginCtrl($scope, $location, $stateParams, $state, authService, ngAuthSettings, $rootScope, $injector) {
    var $validationProvider = $injector.get('$validation');

    //Login
    $scope.loginForm = {
        checkValid: $validationProvider.checkValid,
        submit: function (form) {
            $validationProvider.validate(form).success(function () {
                $scope.login();
            }).error(function () {
            });
        },
        reset: function (form) {
            $validationProvider.reset(form);
        }
    };

    $scope.login = function () {
        authService.login($scope.loginForm).then(
            function (response) {
                $state.go('index.user');
            },
            function (err) {
                swal('Login failed!', err.error_description, 'error');
            })
    }
}