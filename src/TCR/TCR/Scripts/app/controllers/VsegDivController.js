tcrApp.controller("VsegDivController",
    function ($scope, $http) {
        $http.get('../api/People').success(function (response) {
            $scope.people = response;
        });
        $scope.personSelected = false;
        $scope.selectPerson = function (person) {
            $scope.selectedPerson = person;
            $scope.personSelected = true;

            $http.get('../api/Service/GetVsegDiv/' + person.Id).success(function (response) {
                $scope.vSegDiv = [['V-сегменты', 'Частота']];
                for (var i = 0; i < response.length; i++) 
                    $scope.vSegDiv.push([response[i].Key, response[i].Value]);
                var data = google.visualization.arrayToDataTable($scope.vSegDiv);
                var options = {
                    height: 900,
                    width: 900,
                    colors: ["red"],
                    hAxis: {
                        title: 'Частота вхождения V-сегментов в рецепторы'
                    },
                    vAxis: {
                        title: 'V-сегменты'
                    }
                };
                var chart = new google.visualization.BarChart(document.getElementById('vseg_chart'));
                chart.draw(data, options);
            });
        }
        $scope.returnToSelection = function () {
            $scope.personSelected = false;
        }
    }
);