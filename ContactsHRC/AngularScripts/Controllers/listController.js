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

            //ako je kontakt ažuriran, izbrisan ili dodan, lista sa strane se osvježi
            $scope.$on("$routeChangeSuccess", function() {
                var apiUrl = "/api/Contacts?filter=" + $scope.searched;
                $scope.contacts = [];
                $http.get(apiUrl).then(function (result) {
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