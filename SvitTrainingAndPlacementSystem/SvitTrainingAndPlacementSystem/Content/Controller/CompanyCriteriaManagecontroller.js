app.controller("myCntrlcompcriteria", function ($scope, $window,myService) {
    GetAllEmployees();
    
        //For sorting according to columns
        $scope.sort = function (keyname) {
            $scope.sortKey = keyname;
            $scope.reverse = !$scope.reverse;
        }

        $scope.selectedBracnhModel = [];
        //$scope.example13data = [
        //    {id: 1, label: "David"},
        //    {id: 2, label: "Jhon"},
        //    {id: 3, label: "Lisa"},
        //    {id: 4, label: "Nicole"},
        //    {id: 5, label: "Danny"}];

        $scope.multiBranchSelect = {
            smartButtonMaxItems: 3,
            smartButtonTextConverter: function (itemText, originalItem) {
                return itemText;
            }
        }

      
    //To Get All Records  
        function GetAllEmployees() {
            var getData = myService.getCompanycriteria();

            getData.then(function (emp) {
                $scope.employees = emp.data;
            }, function (emp) {
                alert("Records gathering failed!");
            });
        }

       
    
    });