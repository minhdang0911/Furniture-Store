/// <reference path="../angular/angular.js" />
var app = angular.module('bookshop', []);
app.controller("SilderController", SilderController);
SilderController.$inject = ['$scope', '$http'];
function SilderController($scope, $http) {
    $scope.listSlider = []
    $http.get("Home/Slider_Json").then(function (result) {
        $scope.listSlider = result.data;
    })
}