tcrApp.controller("VsegDivController",
    function ($scope, $http) {
        $http.get('../api/People').success(function (response) {
            $scope.people = response;
        });
        $scope.personSelected = false;
        $scope.selectPerson = function (person) {
            $scope.selectedPerson = person;
            $scope.personSelected = true;
        }
        $scope.returnToSelection = function () {
            $scope.personSelected = false;
        }
    }
);