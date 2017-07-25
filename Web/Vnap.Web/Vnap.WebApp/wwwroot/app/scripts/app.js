'use strict';
var vnap = angular.module('vnap', [
        'ui.router',                    // Routing
        'ui.bootstrap',                 // Bootstrap
        'oc.lazyLoad',
        'ngIdle',                       // Idle timer
        'LocalStorageModule',
        'cloudinary'
]);

var baseUrl = $('base').attr('href');
var appBaseUrl = baseUrl + 'wwwroot/app/';
var apiBaseUrl = '/api/';

vnap.constant('ngAuthSettings', {
    apiServiceBaseUri: baseUrl,
    clientId: 'Vnap.Web'
});

vnap.run(['authService', '$window', function (authService, $window) {
    authService.fillAuthData();
}]);