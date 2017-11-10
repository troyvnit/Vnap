'use strict';
var vnap = angular.module('vnap', [
    'ui.router',                    // Routing
    'ui.bootstrap',                 // Bootstrap
    'oc.lazyLoad',
    'ngIdle',                       // Idle timer
    'LocalStorageModule',
    'cloudinary',
    'validation',
    'validation.rule',
    'SignalR',
    'angularMoment',
    'toaster',
    'ngAnimate',
    'ngResource'
]);

var baseUrl = $('base').attr('href');
var appBaseUrl = baseUrl + 'wwwroot/app/';
var apiBaseUrl = '/api/';

vnap.constant('ngAuthSettings', {
    apiServiceBaseUri: baseUrl,
    clientId: 'Vnap.Web'
});

vnap.run(['authService', '$window', 'amMoment', function (authService, $window, amMoment) {
    authService.fillAuthData();
    amMoment.changeLocale('vi');
}]);