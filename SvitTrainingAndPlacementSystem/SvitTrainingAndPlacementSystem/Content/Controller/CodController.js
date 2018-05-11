app.controller("coodinatorcontroller", function ($scope, $window, $http) {
    GetAllStudentslist();

    function GetAllStudentslist() {
        var getData = $http.get("/User/Cod/gatallcordinTORlist");

        getData.then(function (emp) {
            $scope.CodData = emp.data;
        }, function (emp) {
            alert("Records gathering failed!");
        });
    }
    
    $scope.createCoordinator = function (id) {
    $window.location.href = "/User/Cod/CreateCordinator";
    }
   
   

});