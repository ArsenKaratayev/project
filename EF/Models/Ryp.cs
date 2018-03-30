using System;
using System.Collections.Generic;

namespace EF.Models
{
    public class Ryp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Specialty Specialty { get; set; }
        public string Year { get; set; }
        public List<List<Subject>> Subjects { get; set; }
        public List<List<ElectiveGroup>> ElectiveGroups { get; set; }
        public User User { get; set; }
        public string Date { get; set; }
        public string UpdateDate { get; set; }
        public bool OperatorCheck { get; set; }
        public bool FullCheck { get; set; }
    }
}
