var contactApp = angular.module("contactApp", ["ngRoute"]);

contactApp.config(function ($routeProvider, $locationProvider) {
    $locationProvider.hashPrefix("");
    $routeProvider
        .when("/list",
        {
            controller: "listController"
        })
        .when("/edit/:id",
        {
            templateUrl: "AngularScripts/Views/editView.html",
            controller: "editController"
        })
        .when("/newContact", {
            templateUrl: "AngularScripts/Views/newView.html",
            controller: "createController"
        })
        .when("/contactInfo/:id", {
            templateUrl: "AngularScripts/Views/contactInfo.html",
            controller: "infoController"
        })

        .otherwise({
            redirectTo: "/"
        });
});