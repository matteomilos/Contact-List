(function () {
    angular
        .module("contactApp")
        .controller("listController", ['$scope', '$http', function ($scope, $http) {
            var apiUrl = "/api/Contacts";
            $scope.contacts = [];
            $http.get(apiUrl).then(function (result) {
                $scope.contacts = result.data;
            });
        }]);
})();