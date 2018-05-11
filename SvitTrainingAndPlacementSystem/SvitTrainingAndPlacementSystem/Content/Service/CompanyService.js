app.service("myService", function ($http) {

	//get All Employee
	this.getEmployees = function () {			   
	    return $http.get("/Company/getAll");
	};


    //get All Employee
	this.getCompanycriteria = function () {
	    return $http.get("/CompanyCriteriaManage/getallCompanycriteria");
	};

    // get companyInfo By Id
	this.getCompanyInfo = function (employeeID) {
	    var response = $http({
	        method: "post",
	        url: "/Company/getCompanyInfoById",
	        params: {
	            id: JSON.stringify(employeeID)
	        }
	    });
	    return response;
	}

    // Update compnay 
	this.updateCompanyInfo = function (employee) {
	    var response = $http({
	        method: "post",
	        url: "/Company/UpdateCompnayInfo",
	        data: JSON.stringify(employee),
	        dataType: "json"
	    });
	    return response;
	}

    // Add company info
	this.AddCompnayInfo = function (employee) {
	    var response = $http({
	        method: "post",
	        url: "/Company/AddCompnayInfo",
	        data: JSON.stringify(employee),
	        dataType: "json"
	    });

	    return response;
	}


    //select all branch
	this.BranchSelectAll = function () {
	    return $http.get("/Company/BranchSelectAll");
	};
});