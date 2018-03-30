using System;
using System.Collections.Generic;

namespace EF.Models
{
    public class ElectiveGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SubjectType Type { get; set; } // obsheobraz, bazovie, profilnie
        public SubjectHours Hours { get; set; }
        public int Credits { get; set; }
        public string Shifr { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Subject> Prerequisites { get; set; }
        public User User { get; set; }
        public string Date { get; set; }
        public string UpdateDate { get; set; }

        //public ElectiveGroup(int id, string name, string shifr, List<Subject> subjects)
        //{
        //    this.Id = id;
        //    this.Name = name;
        //    this.Type = type;
        //    this.Hours = hours;
        //    this.Credits = hours.Lec + hours.Lab + hours.Pr;
        //    this.Shifr = shifr;
        //}
    }
}
