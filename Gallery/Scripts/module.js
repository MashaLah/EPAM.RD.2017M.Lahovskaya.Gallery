angular.module('gallery', ['ngRoute'])
.config([
'$locationProvider', '$routeProvider', function ($locationProvider, $routeProvider) {
    $routeProvider
               .when('/Angular/gallery', {
                   templateUrl: '/Angular/gallery.html',
                   controller: 'GalleryController'
               })
        .when('/Angular/description', {
            templateUrl: '/Angular/description.html',
            controller: 'DescriptionController'
        })

               .otherwise({
                   redirectTo: '/Angular/gallery'
               });

    $locationProvider.html5Mode(true);
}
])
.controller('GalleryController', ['$scope', 'dataCenter', function ($scope, dataCenter) {

    var defered = dataCenter.getAll();
    defered.then(function (response) {
        $scope.guitars = response.data;
    });
}])

    .controller('DescriptionController', ['$scope', function ($scope) {
        $scope.text = "This is gallery.";
        $scope.isEdit = false;

        $scope.goEdit = function () {
            $scope.isEdit = true;
        }

        $scope.applyEdit = function () {
            $scope.isEdit = false;
        }
    }])

.service('dataCenter', ['$http', function ($http) {
    return {
        getAll: getAll
    };

    function getAll() {
        return $http.get("/Home/GetImages");/*({
            url: 'http://localhost:49390/Home/GetImages'
        });*/
    }

}])