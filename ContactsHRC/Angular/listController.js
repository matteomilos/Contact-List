(function () {
    angular
        .module("contactApp")
        .controller("listController", function ($scope, $http, $window) {
            var apiUrl = "/api/Contacts";
            $scope.contacts = [];
            $http.get(apiUrl).then(function (result) {
                $scope.contacts = result.data;
            });

            $scope.delete = function(id) {
                $http.delete("api/Contacts/" + id).then(function() {
                    $window.location.href = "";
                });
            }
        });
})();