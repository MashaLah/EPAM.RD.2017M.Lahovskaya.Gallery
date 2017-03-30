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
         .when('/Angular/addImage', {
             templateUrl: '/Angular/addImage.html',
             controller: 'GalleryController'
         })

               .otherwise({
                   redirectTo: '/Angular/gallery'
               });

    $locationProvider.html5Mode(true);
}
])
.controller('GalleryController', ['$scope', 'dataCenter', function ($scope, dataCenter) {

    $scope.NewImage = { 
        Title: "", 
        Src: null,  
        Date: null,
        AlbumId: null,
        extension: null,
    } 

    $scope.Album = {
        Id: null,
        Name:"",
    };

    var defered = dataCenter.getAll();
    defered.then(function (response) {
        $scope.pictures = response.data;
    });

    $scope.SaveImage = function () {
        dataCenter.saveImage($scope.NewImage);
    };

    $scope.getAlbums = dataCenter.getAlbums().then(function (response) {
        $scope.albums=response.data;
    })

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

        getAll: function () {
            return $http.get('/Home/GetImages');
        },

        saveImage: function (NewImage) {
            return $http.post('/Home/SaveImage', {
                src: NewImage.Src,
                title: NewImage.Title,
                albumId: NewImage.AlbumId,
                extension: NewImage.Extension
            });
        },

        getAlbums: function(){
            return $http.get('/Home/GetAlbums');
    } 
    }
        }

])

.directive("fileread", [function () {
    return {
        scope: {
            fileread: "="
        },
        link: function (scope, element) {
            element.bind("change", function (changeEvent) {
                var reader = new FileReader();
                reader.onload = function (loadEvent) {
                    scope.$apply(function () {
                        scope.fileread = loadEvent.target.result;
                    });
                }
                reader.readAsDataURL(changeEvent.target.files[0]);
            });
        }
    }
}])