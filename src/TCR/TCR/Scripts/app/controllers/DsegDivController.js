tcrApp.controller("DsegDivController",
    function ($scope, $http) {
        $scope.chartDrawn = false;
        $scope.personSelected = false;
        $http.get('../api/People').success(function (response) {
            $scope.people = response;
        });
        $scope.selectPerson = function (person) {
            $scope.selectedPerson = person;
            $scope.personSelected = true;

            $http.get('../api/Service/GetDsegDiv/' + person.Id).success(function (response) {
                $scope.vSegDiv = [['D-сегменты', 'Частота']];
                for (var i = 0; i < response.length; i++) 
                    $scope.vSegDiv.push([response[i].Key, response[i].Value]);
                var data = google.visualization.arrayToDataTable($scope.vSegDiv);
                var options = {
                    height: 300,
                    width: 900,
                    colors: ["red"],
                    hAxis: {
                        title: 'Частота вхождения D-сегментов в рецепторы'
                    },
                    vAxis: {
                        title: 'D-сегменты'
                    }
                };
                var chart = new google.visualization.BarChart(document.getElementById('dseg_chart'));
                chart.draw(data, options);
                $scope.chartDrawn = true;
            });
        }
        $scope.returnToSelection = function () {
            $scope.personSelected = false;
            $scope.chartDrawn = false;
        }
    }
);