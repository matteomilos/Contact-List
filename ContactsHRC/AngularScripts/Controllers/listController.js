(function () {
    angular
        .module("contactApp")
        .controller("listController", function ($scope, $http, $location, $rootScope) {
            $scope.searched = "";
            $scope.contacts = [];


            $scope.$watch("searched",
                function(val) {
                    var apiUrl = "/api/Contacts?filter=" + $scope.searched;

                    $http.get(apiUrl).then(function(result) {
                        $scope.contacts = result.data;
                    });
                });

            //ako je kontakt ažuriran, izbrisan ili dodan, lista sa strane se osvježi
            $scope.$on("refresh", function(event) {
                var apiUrl = "/api/Contacts?filter=" + $scope.searched;
                $scope.contacts = [];
                $http.get(apiUrl).then(function (result) {
                    $scope.contacts = result.data;
                });
            });

            $scope.delete = function (id) {
                $http.delete("api/Contacts/" + id).then(function() {
                    $rootScope.$broadcast("refresh");
                    $location.url("");
                });
            }


        });
})();