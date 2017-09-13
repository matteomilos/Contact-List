(function () {
    angular
        .module("contactApp")
        .controller("editController", function ($scope, $http, $routeParams, $route, $location, $rootScope) {

            var contactId = $routeParams.id;
            var apiUrl = "/api/Contacts/" + contactId;

            $http.get(apiUrl).then(function (result) {
                $scope.currentContact = result.data;
            });


            $scope.save = function () {
                var contact = {
                    ContactId: contactId,
                    FirstName: $scope.currentContact.FirstName,
                    LastName: $scope.currentContact.LastName,
                    Address: $scope.currentContact.Address,
                    EmailAddresses: $scope.currentContact.EmailAddresses,
                    PhoneNumbers: $scope.currentContact.PhoneNumbers,
                    Tags: $scope.currentContact.Tags
                };

                $http.put(apiUrl, contact).then(function () {
                    $location.url("");
                    $rootScope.$broadcast("refresh");
                });
            }

            $scope.deleteNumber = function (index) {
                $scope.currentContact.PhoneNumbers.splice(index, 1);
            }

            $scope.deleteEmail = function (index) {
                $scope.currentContact.EmailAddresses.splice(index, 1);
            }

            $scope.addPhoneNumber = function () {
                var newPhone = { PhoneNumberId: "0", PhoneNumberValue: "", ContactId: contactId };
                $scope.currentContact.PhoneNumbers.push(newPhone);
            }

            $scope.addEmailAddress = function () {
                var newEmail = { EmailAddresId: "0", EmailAddressValue: "", ContactId: contactId };
                $scope.currentContact.EmailAddresses.push(newEmail);
            }

            $scope.addTag = function () {
                var newTag = { TagId: "0", TagName: "" }
                $scope.currentContact.Tags.push(newTag);
            }

            $scope.deleteTag = function (index) {
                $scope.currentContact.Tags.splice(index, 1);
            }


        });
})();