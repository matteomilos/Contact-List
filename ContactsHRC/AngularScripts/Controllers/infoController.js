(function () {
    angular
        .module("contactApp")
        .controller("infoController", function ($scope, $http, $routeParams) {

            var contactId = $routeParams.id;
            var apiUrl = "/api/Contacts/" + contactId;

            if (contactId !== 0) {
                $http.get(apiUrl).then(function (result) {
                    $scope.currentContact = result.data;
                });
            }


        });
})();