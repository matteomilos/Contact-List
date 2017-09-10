var contactApp = angular.module('contactApp', ['ngRoute']);

contactApp.config(function ($routeProvider, $locationProvider) {
    $locationProvider.hashPrefix('');
    $routeProvider
        .when('/list', {
            templateUrl: 'Scripts/Angular/contactList.html',
            controller: 'listController',
        })

        .otherwise({
            redirectTo: '/'
        });
});