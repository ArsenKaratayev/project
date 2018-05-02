using System;
using System.Collections.Generic;
using EF.Models;

namespace EF.Models
{
    public class SubjectVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Credits { get; set; }
        public string Shifr { get; set; }

        //public User User { get; set; }
        public string UserId { get; set; }

        public string Date { get; set; }
        public string UpdateDate { get; set; }

        public List<SubjectVM> Prerequisites { get; set; }
        public SubjectType Type { get; set; } 

        public int Lec { get; set; }
        public int Lab { get; set; }
        public int Pr { get; set; }

        public int Deleted { get; set; } = 0;
    }
}
