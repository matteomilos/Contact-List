(function () {
    angular
        .module("contactApp")
        .controller("editController", function ($scope, $http, $routeParams, $location) {
            var contactId = $routeParams.id | 0;
            var apiUrl = "/api/Contacts/" + contactId;
            $scope.contacts = [];
            $http.get(apiUrl).then(function (result) {
                $scope.currentContact = result.data;
            });


            $scope.save = function(){
                var Contact = {
                    ContactId: contactId,
                    FirstName: $scope.currentContact.FirstName,
                    LastName: $scope.currentContact.LastName,
                    Address: $scope.currentContact.Address,
                    EmailAddresses: $scope.currentContact.EmailAddresses,
                    PhoneNumbers: $scope.currentContact.PhoneNumbers,
                    Tags: $scope.currentContact.Tags
                };

                $http.put(apiUrl, Contact).then($location.path("#/list"));

            }
        });
})();