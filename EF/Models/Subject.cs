using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Models
{
    public class SubjectType 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Color { get; set; }

        public virtual List<Subject> Subjects { get; set; }
    }

    public class SubjectPrerequisiteSubject
    {
        public int Id { get; set; }
        public int PrimaryId { get; set; }
        public int RelatedId { get; set; }

        public Subject Primary { get; set; }
        public Subject Related { get; set; }
    }

    public class SubjectElectiveGroup
    {
        public int SubjectId { get; set; }
        public int ElectiveGroupId { get; set; }

        public Subject Subject { get; set; }
        public ElectiveGroup ElectiveGroup { get; set; }
    }

    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public string Shifr { get; set; }
        public int Lec { get; set; }
        public int Lab { get; set; }
        public int Pr { get; set; }
        public string Date { get; set; }
        public string UpdateDate { get; set; }
        public int Deleted { get; set; } = 0;

        public string UserId { get; set; }
        public int TypeId { get; set; }
        public SubjectType Type { get; set; }

        public virtual List<SubjectPrerequisiteSubject> RelatedItems { get; set; } = new List<SubjectPrerequisiteSubject>();
        public virtual List<SubjectPrerequisiteSubject> RelatedTo { get; set; } = new List<SubjectPrerequisiteSubject>();

        public virtual List<SubjectElectiveGroup> SubjectElectiveGroups { get; set; } = new List<SubjectElectiveGroup>();
        public virtual List<SemesterSubject> SemesterSubjects { get; set; }
    }
}
