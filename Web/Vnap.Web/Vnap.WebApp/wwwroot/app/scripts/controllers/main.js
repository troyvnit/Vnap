/**
 * MainCtrl - controller
 */
function MainCtrl($timeout, $state, authService, $scope) {
    this.authentication = authService.authentication;

    if (this.authentication.isAuth) {
        //Welcome Message
    } else {
        $state.go('login');
    }

    this.logOut = function () {
        authService.logOut();
        $state.go('login');
    }
}

function NavigationCtrl($timeout, $state, authService, $scope) {
    
}