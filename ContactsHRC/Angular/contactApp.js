var contactApp = angular.module("contactApp", ["ngRoute"]);

contactApp.config(function ($routeProvider, $locationProvider) {
    $locationProvider.hashPrefix("");
        $routeProvider
            .when("/list",
                {
                    templateUrl: "Angular/contactList.html",
                    controller: "listController"
                })
            .when("/edit/:id",
                {
                    templateUrl: "Angular/editView.html",
                    controller: "editController"
                })
            .when("/newContact", {
                templateUrl: "Angular/editView.html",
                controller: "createController"
            })

        .otherwise({
            redirectTo: "/"
        });
});