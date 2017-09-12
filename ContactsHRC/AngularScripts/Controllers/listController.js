(function () {
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
                $http.delete("api/Contacts/" + id).then($location.path(""));
            }


        });
})();