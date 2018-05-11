app.controller("studentctrl", function ($scope, $window, $http) {
    GetAllStudentslist();

    function GetAllStudentslist() {
        var getData = $http.get("/User/Student/gatallstudentlist");

        getData.then(function (emp) {
            $scope.studentdata = emp.data;
        }, function (emp) {
            alert("Records gathering failed!");
        });
    }

    $scope.gotostudentdetail = function (id) {
        $window.location.href = "/User/Student/StudentDetail/" + id;
    }


});