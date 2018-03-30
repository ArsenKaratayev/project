using System;
using System.Collections.Generic;

namespace EF.Models
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Shifr { get; set; }
        public User User { get; set; }
        public string Date { get; set; }
        public string UpdateDate { get; set; }

        public Specialty(int id, string name, string shifr, User user, string date) {
            this.Id = id;
            this.Name = name;
            this.Shifr = shifr;
            this.User = user;
            this.Date = date;
        }
    }
}
