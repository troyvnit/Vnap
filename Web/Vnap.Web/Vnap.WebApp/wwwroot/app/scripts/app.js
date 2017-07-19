'use strict';
var vnap = angular.module('vnap', [
        'ui.router',                    // Routing
        'ui.bootstrap',                 // Bootstrap
        'oc.lazyLoad',
        'ngIdle',                       // Idle timer
        'cloudinary'
]);

var baseUrl = $('base').attr('href');
var appBaseUrl = baseUrl + '/';
var apiBaseUrl = '/api/';