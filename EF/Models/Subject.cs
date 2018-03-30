using System;
using System.Collections.Generic;

namespace EF.Models
{
    public class SubjectHours {
        public int Lec { get; set; }
        public int Lab { get; set; }
        public int Pr { get; set; }

        public SubjectHours(int lec, int lab, int pr) {
            this.Lec = lec;
            this.Lab = lab;
            this.Pr = pr;
        }
    }

    public class SubjectType {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Color { get; set; }
        public SubjectType(string name)
        {
            this.Name = name;
            this.Code = name.Substring(0, 1);
            if (name == "Базовая") {
                this.Color = "#64B5F6";
            } else if (name == "Профилирующая") {
                this.Color = "#7986CB";
            } else if (name == "Общеобразовательная") {
                this.Color = "#81C784";
            } else {
                this.Color = "#FF7043";
            }
        }
    }
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SubjectType Type { get; set; } // obsheobraz, bazovie, profilnie
        public SubjectHours Hours { get; set; }
        public int Credits { get; set; }
        public string Shifr { get; set; }
        public List<Subject> Prerequisites { get; set; }
        public User User { get; set; }
        public string Date { get; set; }
        public string UpdateDate { get; set; }

        public Subject(int id, string name, SubjectType type, SubjectHours hours, string shifr, User user, string date) {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Hours = hours;
            this.Credits = hours.Lec + hours.Lab + hours.Pr;
            this.Shifr = shifr;
            this.Prerequisites = new List<Subject>();
            this.User = user;
            this.Date = date;
        }
    }
}
