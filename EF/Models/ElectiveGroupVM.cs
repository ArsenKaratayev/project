using System;
using System.Collections.Generic;
using EF.Models;

namespace EF.Models
{
    public class ElectiveGroupVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public string Shifr { get; set; }

        public string Date { get; set; }
        public string UpdateDate { get; set; }

        public SubjectType Type { get; set; } // obsheobraz, bazovie, profilnie

        public int Pr { get; set; }

        public string UserId { get; set; }
        public List<SubjectVM> Prerequisites { get; set; }
        public List<SubjectVM> Subjects { get; set; }

        public int Deleted { get; set; } = 0;
    }
}
