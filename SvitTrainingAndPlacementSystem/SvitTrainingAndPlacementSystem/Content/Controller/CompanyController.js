app.controller("myCntrl", function ($scope, $window,myService) {
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

    //show add compnay popup
    $scope.AddCompany = function () {
        ClearFields();
        $scope.Action = "Add";
        GetBranch();
    }
    
    $scope.editCompany = function (employee) {
        GetBranch();
        var getData = myService.getCompanyInfo(employee.CompanyId);
        getData.then(function (emp) {
            $scope.companyName = emp.data.CompanyName;
            $scope.CompanyId = emp.data.CompanyId;
            $scope.companyWebsite = emp.data.WebsiteName;
            $scope.companyDomain = emp.data.Domain;
            $scope.IsTrainingProvide = emp.data.IsTrainingProvide;
            $scope.IsPlacementProvide = emp.data.IsPlacementProvide;
            $scope.Address = emp.data.Address;
            $scope.selectedBracnhModel = emp.data.CompanyBranches;
            $scope.Action = "Edit";
            $("#myModal").modal('show');
        },
        function (msg) {
            $("#alertModal").modal('show');
            $scope.msg = msg.data;
        });
    }

    $scope.AddUpdateCompanyInfo = function () {
        var Compnay = {
            CompanyName: $scope.companyName,
            WebsiteName: $scope.companyWebsite,
            Domain: $scope.companyDomain,
            IsTrainingProvide: $scope.IsTrainingProvide == undefined ? false : $scope.IsTrainingProvide,
            IsPlacementProvide: $scope.IsPlacementProvide == undefined ? false : $scope.IsPlacementProvide,
            Address: $scope.Address,
            CompanyBranches: $scope.selectedBracnhModel
        };

        var getAction = $scope.Action;

        if (getAction == "Edit") {
            Compnay.CompanyId = $scope.CompanyId;
            var getData = myService.updateCompanyInfo(Compnay);
            getData.then(function (msg) {
                ClearFields();
                $("#alertModal").modal('show');
                $scope.msg = msg.data;
            }, function (msg) {
                $("#alertModal").modal('show');
                $scope.msg = msg.data;
            });
        }
        else {
            var getData = myService.AddCompnayInfo(Compnay);
            getData.then(function (msg) {
               // GetAllEmployee();
                $("#alertModal").modal('show');
                $scope.msg = msg.data;
                ClearFields();

            }, function (msg) {
                $("#alertModal").modal('show');
                $scope.msg = msg.data;
            });
        }
        //debugger;
        //GetAllEmployee();
      //  $scope.refresh();
       
    }


    $scope.alertmsg = function () {
        $("#alertModal").modal('hide');
        $window.location.reload();
    };

    //To Get All Records  
    function GetAllEmployees() {
        var getData = myService.getEmployees();

        getData.then(function (emp) {
            $scope.employees = emp.data;
        }, function (emp) {
            alert("Records gathering failed!");
        });
    }

    function GetBranch() {
        var branches = myService.BranchSelectAll();

        branches.then(function (dep) {
            $scope.example13data = dep.data;
        }, function (dep) {
            $("#alertModal").modal('show');
            $scope.msg = "Error in filling departments drop down !";
        });
    }

    function ClearFields() {

        $scope.CompanyId = "";
        $scope.companyName = "";
        $scope.companyDomain = "";
        $scope.Address = "";
        $scope.IsTrainingProvide = "";
        $scope.IsPlacementProvide = "";
        $scope.companyWebsite = "";
        $scope.companyBranch = "";
    }
   
    $scope.manageCriteria = function (id) {
        $window.location.href = "/User/Companyinfo/CompanyCriteriaManage/"+id;
    }
    
});