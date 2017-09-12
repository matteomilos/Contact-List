﻿(function () {
    angular
        .module("contactApp")
        .controller("listController", function ($scope, $http, $location) {
            $scope.searched = "";
            $scope.contacts = [];


            $scope.$watch("searched",
                function(val) {
                    var apiUrl = "/api/Contacts?filter=" + $scope.searched;

                    $http.get(apiUrl).then(function(result) {
                        $scope.contacts = result.data;
                    });
                });

            $scope.delete = function (id) {
                var index = $scope.contacts.indexOf(id);
                $http.delete("api/Contacts/" + id).then(function() {
                    $scope.contacts.splice(index, 1);
                    $location.url("");
                });
            }


        });
})();