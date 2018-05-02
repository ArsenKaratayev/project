using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Models
{
    public class ElectiveGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public string Shifr { get; set; }
        public int Pr { get; set; }
        public string Date { get; set; }
        public string UpdateDate { get; set; }
        public int Deleted { get; set; } = 0;

        public int TypeId { get; set; }
        public SubjectType Type { get; set; }

        public string UserId { get; set; }

        public virtual List<SemesterElectiveGroup> SemesterElectiveGroups { get; set; }

        public virtual List<SubjectElectiveGroup> SubjectElectiveGroups { get; set; } = new List<SubjectElectiveGroup>();
    }
}
