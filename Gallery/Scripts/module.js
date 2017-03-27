angular.module('gallery', ['ngRoute'])
.config([
'$locationProvider', '$routeProvider', function ($locationProvider, $routeProvider) {
    $routeProvider
               /* admin */
               .when('/Angular/gallery', {
                   templateUrl: '/Angular/gallery.html',
                   controller: 'GalleryController'
               })
               .otherwise({
                   redirectTo: '/Angular/gallery'
               });
    // Uses HTLM5 history API for navigation
    $locationProvider.html5Mode(true);
}
])
.controller('GalleryController', ['$scope', 'dataCenter', function ($scope, dataCenter) {

    var defered = dataCenter.getAll();
    defered.then(function (response) {
        $scope.guitars = response.data;
    });

}])
.service('dataCenter', ['$http', function ($http) {
    return {
        getAll: getAll,
    };

    function getAll() {
        return $http({
            url: 'http://localhost:49390/Home/GetImages'
        });
    }
}])