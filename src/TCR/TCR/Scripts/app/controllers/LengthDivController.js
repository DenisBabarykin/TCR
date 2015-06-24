tcrApp.controller("LengthDivController",
    function ($scope, $http) {
        $http.get('../api/People').success(function (response) {
            $scope.people = response;
        });
        $scope.personSelected = false;
        $scope.selectPerson = function (person) {
            $scope.selectedPerson = person;
            $scope.personSelected = true;

            $http.get('../api/Service/GetLengthDiv/' + person.Id).success(function (response) {
                $scope.lengthDivAr = response;
                $scope.lengthDivAr.unshift(['Длина', 'Количество']);
                var data = google.visualization.arrayToDataTable($scope.lengthDivAr);
                var options = {
                    bar: {
                        groupWidth: '75%'
                    },
                    height: 500,
                    width: 900,
                    colors: ["green"],
                    hAxis: {
                        title: 'Длина нуклеотидной последовательности'
                    },
                    vAxis: {
                        title: 'Количество рецепторов'
                    }
                };
                var chart = new google.visualization.ColumnChart(
                  document.getElementById('length_chart'));
                chart.draw(data, options);
            });
        }
        $scope.returnToSelection = function () {
            $scope.personSelected = false;
        }
    }
);