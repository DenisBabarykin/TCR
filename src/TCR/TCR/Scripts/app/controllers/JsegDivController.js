tcrApp.controller("JsegDivController",
    function ($scope, $http) {
        $scope.chartDrawn = false;
        $scope.personSelected = false;
        $http.get('../api/People').success(function (response) {
            $scope.people = response;
        });
        $scope.selectPerson = function (person) {
            $scope.selectedPerson = person;
            $scope.personSelected = true;

            $http.get('../api/Service/GetJsegDiv/' + person.Id).success(function (response) {
                $scope.vSegDiv = [['J-сегменты', 'Частота']];
                for (var i = 0; i < response.length; i++) 
                    $scope.vSegDiv.push([response[i].Key, response[i].Value]);
                var data = google.visualization.arrayToDataTable($scope.vSegDiv);
                var options = {
                    height: 500,
                    width: 900,
                    colors: ["red"],
                    hAxis: {
                        title: 'Частота вхождения J-сегментов в рецепторы'
                    },
                    vAxis: {
                        title: 'J-сегменты'
                    }
                };
                var chart = new google.visualization.BarChart(document.getElementById('jseg_chart'));
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