app.controller("placedstudentctrl", function ($scope, $window, $http) {
    GetAllStudentslist();

    function GetAllStudentslist() {
        var getData = $http.get("/User/Placedstudent/gatallplacedstudentlist");

        getData.then(function (emp) {
            $scope.studentdata = emp.data;
        }, function (emp) {
            alert("Records gathering failed!");
        });
    }


});