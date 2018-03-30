var app = angular.module("app", []);
app.controller("createSubjectController", function ($scope, $http) {

    self = $scope;

    function subjectClass(name, type, subjectHours, credits, shifr)
    {
        this.Name = name;
        this.Type = type;
        this.SubjectHours = subjectHours;
        this.Credits = credits;
        this.Shifr = shifr;
    }
    function subjectHoursClass(lec, lab, pr) {
        this.Lec = lec;
        this.Lab = lab;
        this.Pr = pr;
    }

    self.sendSubject = function () {
        var SubjectHours = new subjectHoursClass(self.Lec, self.Lab, self.Pr);
        var subject = new subjectClass(self.Name, self.Type, SubjectHours, (parseInt(SubjectHours.Lec) + parseInt(SubjectHours.Lab) + parseInt(SubjectHours.Pr)), self.Shifr);
        $http
            .post('http://localhost:5001/api/subjects', subject)
            .then(function (response) {
                if (response.status === 200) {
                    self.Name = "";
                    self.Type = "";
                    self.Credits = "";
                    self.Shifr = "";
                    self.Lab = "";
                    self.Lec = "";
                    self.Pr = "";
                } else {
                    alert("error");
                }
                console.log(response);
            },function (response) {
                alert("error...")
            });
    }
})