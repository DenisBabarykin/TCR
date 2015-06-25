'use strict';

google.load('visualization', '1', {
    packages: ['corechart']
});

google.setOnLoadCallback(function () {
    angular.bootstrap(document.body, ['tcrApp']);
});

var tcrApp = angular.module("tcrApp", ["highcharts-ng"])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/Home/menu',
        {
            templateUrl: '/Scripts/app/views/menu.html',
            controller: 'MenuController'
        });
        $routeProvider.when('/Home/lengthDiv',
        {
            templateUrl: '/Scripts/app/views/lengthDiv.html',
            controller: 'LengthDivController'
        });
        $routeProvider.when('/Home/vsegDiv',
        {
            templateUrl: '/Scripts/app/views/vSegDiv.html',
            controller: 'VsegDivController'
        });
        $routeProvider.when('/Home/repertoireClones',
        {
            templateUrl: '/Scripts/app/views/repertoireClones.html',
            controller: 'RepertoireClonesController'
        });
        $routeProvider.when('/Home/About',
        {
            templateUrl: '/Scripts/app/views/About.html',
            controller: 'AboutController'
        });
        $routeProvider.when('/Home/Contact',
        {
            templateUrl: '/Scripts/app/views/Contact.html',
            controller: 'ContactController'
        });
        $routeProvider.otherwise({ redirectTo: '/Home/menu' });
        $locationProvider.html5Mode(true);
    });