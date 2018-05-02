using System;
namespace EF.Models
{
    public class SpecialtyVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Shifr { get; set; }
        public string Date { get; set; }
        public string UpdateDate { get; set; }
        public string UserId { get; set; }
        public int Deleted { get; set; } = 0;
    }
}
