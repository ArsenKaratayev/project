var app = angular.module("app", []);
app.controller("createRypController", function ($scope, $http) {

    self = $scope;
    self.subjects = [];
    self.semester = [];
    var Subjects = [[], [], [], [], [], [], [], []];
    self.semesterNumber;
    self.showSemesters = "f";
    self.showSubjects = "f";
    var subs = [];

    function RypClass(specialty, year, subjects)
    {
        this.SpecialtyId = specialty;
        this.Year = year;
        this.SubjectsId = subjects;
    }
    // подгружаем все существующие рупы для проверки
    self.loadRyps = function () {
        $http
            .get('http://localhost:5001/api/ryps')
            .then(function (response) {
                if (response.status === 200) {
                    self.ryps = response.data.slice();
                    console.log(self.ryps)
                }
            },
            function (response) {
                alert(response);
            }
        );
    };
    self.loadRyps();

    // подгружаем все специальности
    self.loadSpecialtys = function () {
        $http
            .get('http://localhost:5001/api/specialtys')
            .then(function (response) {
                if (response.status === 200) {
                    self.specialtys = response.data.slice();
                }
            },
            function (response) {
                alert(response);
            }
        );

    };
    self.loadSpecialtys();

    // загружаем все предметы из выбранной специальности
    self.loadSubjects = function () {
        for(var i = 0; i < self.ryps.length; i++) {
            if (self.ryps[i].specialtyId == self.Specialty.id && self.ryps[i].year == self.Year) {
                // ERROR MESSAGE 
            }
        }
        subs = [];
        self.semester = [];
        Subjects = [[], [], [], [], [], [], [], []];
        // если такого рупа еще нет
        self.Specialty = self.specialtys[self.Specialty - 1];
        for(var i = 0; i < self.Specialty.subjectsId.length; i++) {
            var id = self.Specialty.subjectsId[i];
            $http
                .get('http://localhost:5001/api/subjects/' + id)
                .then(function (response) {
                    if (response.status === 200) {
                        subs.push(response.data);
                    }
                })
        }
        self.subjects = subs;
        self.showSemesters = "t";
        //setTimeout(self.loadSubjectsFromRyp, 1000);
    }
    self.loadSubjectsFromRyp = function () {
        // если такой руп уже создан
        for(var i = 0; i < self.ryps.length; i++) {
            if (self.ryps[i].specialtyId == self.Specialty.id && self.ryps[i].year == self.Year) {
                var subId = self.subjects.map(a => a.id);
                for(var j = 0; j < self.ryps[i].subjectsId.length; j++) {
                    var arr = [];
                    for(var k = 0; k < self.ryps[i].subjectsId[j].length; k++) {
                        var id = self.ryps[i].subjectsId[j][k];
                        if (subId.indexOf(self.ryps[i].subjectsId[j][k]) != -1) {
                            arr.push(self.ryps[i].subjectsId[j][k]);
                        }
                    }
                    Subjects[j] = arr;
                    console.log(Subjects[j])
                }
                var subsId = subs.map(a => a.id);
                var arr = [[], [], [], [], [], [], [], []];
                for(var j = 0; j < Subjects.length; j++) {
                    for(var k = 0; k < Subjects[j].length; k++) {
                        if (subsId.indexOf(Subjects[j][k]) != -1) {
                            arr[j].push(subs[subsId.indexOf(Subjects[j][k])]);
                            self.subjects.splice(self.subjects.indexOf(arr[j][k]), 1);
                        }
                    }
                }
                Subjects = arr;
                console.log(Subjects)
            }
        }

    };

    // Присваиваем выбранные предметы выбранному семестру
    self.setSubjectsToPickedSemester = function () {
        var subjectsId = [];
        if (self.semester.length != 0) {
            Subjects[self.semesterNumber-1] = self.semester;
        }
    }

    // выбираем семестр
    self.pickSemester = function (semesterNumber) {       
        self.setSubjectsToPickedSemester();
        self.semester = [];
        self.semesterNumber = semesterNumber;
        if (semesterNumber == 1) {
           self.semester = Subjects[0];
        } else if (semesterNumber == 2) {
           self.semester = Subjects[1]; 
        } else if (semesterNumber == 3) {
           self.semester = Subjects[2]; 
        } else if (semesterNumber == 4) {
           self.semester = Subjects[3]; 
        } else if (semesterNumber == 5) {
           self.semester = Subjects[4]; 
        } else if (semesterNumber == 6) {
           self.semester = Subjects[5]; 
        } else if (semesterNumber == 7) {
           self.semester = Subjects[6]; 
        } else if (semesterNumber == 8) {
           self.semester = Subjects[7]; 
        }
        self.showSubjects = "t";
    }

    // добавляем в выбранный семестр предмет, а из списка убираем
    self.addSubjectToSemester = function (subject) {
        self.semester.push(subject);
        self.subjects.splice(self.subjects.indexOf(subject), 1);
    }

    self.sendRyp = function () {
        self.setSubjectsToPickedSemester();
        // отправляем только id, а не сами объекты
        var SubjectsId = [];
        for(var i = 0; i < Subjects.length; i++) {
            var subjectsId = [];
            for(var j = 0; j < Subjects[i].length; j++) {
                subjectsId.push(Subjects[i][j].id);
            }
            SubjectsId.push(subjectsId);
        }
        var SpecialtyId = self.Specialty.id;

        var ryp = new RypClass(SpecialtyId, self.Year, SubjectsId);
        $http
            .post('http://localhost:5001/api/ryps', ryp)
            .then(function (response) {
                if (response.status === 200) {
                    self.Specialty = "";
                    self.Year = "";
                    self.showSemesters = "f";
                    self.showSubjects = "f";
                    self.subjects = [];
                    self.semester = [];
                    Subjects = [[], [], [], [], [], [], [], []];
                } else {
                    alert("error");
                }
                console.log(response);
            },function (response) {
                alert("error...")
            });
    }
})