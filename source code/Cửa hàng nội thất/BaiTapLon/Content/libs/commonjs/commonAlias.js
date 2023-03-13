
var app = angular.module('bookshop', []);
    app.controller("ProductAliasController", ProductAliasController);
    ProductAliasController.$inject = ['$scope', 'commonService'];
    function ProductAliasController($scope, commonService) {
        $scope.product ={}
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeotitle($scope.Name);
        }
    }