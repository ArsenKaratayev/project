using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Models
{
    public class Semester 
    {
        public int Id { get; set; } 

        public int RypId { get; set; } 
        public Ryp Ryp { get; set; }

        public virtual List<SemesterSubject> SemesterSubjects { get; set; }
        public virtual List<SemesterElectiveGroup> SemesterElectiveGroups { get; set; }
    }

    public class SemesterSubject
    {
        public int SemesterId { get; set; } 
        public int SubjectId { get; set; } 

        public Semester Semester { get; set; }
        public Subject Subject { get; set; }
    }

    public class SemesterElectiveGroup
    {
        public int SemesterId { get; set; }
        public int ElectiveGroupId { get; set; }

        public Semester Semester { get; set; }
        public ElectiveGroup ElectiveGroup { get; set; }
    }

    public class Ryp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Date { get; set; }
        public string UpdateDate { get; set; }
        public int OperatorCheck { get; set; } = 0;
        public int FullCheck { get; set; } = 0;
        public int Deleted { get; set; } = 0;
        public int Prototype { get; set; } = 0;

        public string UserId { get; set; }

        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public virtual List<Semester> Semesters { get; set; }

    }
}
