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

        var data = google.visualization.arrayToDataTable([
            ['Length', 'Amount'],
            [50, 400],
            [51, 460],
            [49, 1120],
            [52, 540]
        ]);

        var options = {
            colors: ["red"],
            //title: 'Распределение длин',
            hAxis: {
                title: 'Длина нуклеотидной последовательности'
                //format: 'h:mm a',
                //viewWindowMode: 'explicit'
                //viewWindow: {
                //    min: [7, 30, 0],
                //    max: [17, 30, 0]
                //}
            },
            vAxis: {
                title: 'Процент рецепторов'
            }
        };

        var chart = new google.visualization.ColumnChart(
          document.getElementById('vseg_chart'));

        chart.draw(data, options);
    }
);