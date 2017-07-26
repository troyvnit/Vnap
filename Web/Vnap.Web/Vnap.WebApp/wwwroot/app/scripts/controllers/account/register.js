/**
 * RegisterCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} User TBD.
 */
function RegisterCtrl($scope, $location, $stateParams, $state, authService, ngAuthSettings, $rootScope, $injector) {
    var $validationProvider = $injector.get('$validation');
    this.authentication = authService.authentication;

    if (this.authentication.isAuth && this.authentication.isAdmin) {
        //Welcome Message
    } else {
        $state.go('login');
    }

    //Register
    $scope.registerForm = {
        checkValid: $validationProvider.checkValid,
        submit: function (form) {
            $validationProvider.validate(form).success(function () {
                $scope.register();
            }).error(function () {
            });
        },
        reset: function (form) {
            $validationProvider.reset(form);
        }
    };

    $scope.register = function () {
        authService.saveRegistration($scope.registerForm).then(
            function (response) {
                swal('Tạo thành công!', 'Bấm đăng nhập bên dưới hoặc tạo tài khoản khác!', 'success');
            },
            function (response) {
                var errors = [];
                for (var key in response.data.ModelState) {
                    for (var i = 0; i < response.data.ModelState[key].length; i++) {
                        errors.push(response.data.ModelState[key][i]);
                    }
                }
                swal(response.data.Message, errors.join(' '), 'error');
            });
    }
}