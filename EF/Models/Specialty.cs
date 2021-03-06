﻿using System;
using System.Collections.Generic;

namespace EF.Models
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Shifr { get; set; }
        public string Date { get; set; }
        public string UpdateDate { get; set; }
        public int Deleted { get; set; } = 0;

        public string UserId { get; set; }

        public virtual List<Ryp> Ryps { get; set; } // ne nyzhno, y odnoi specialnosty 1 ryp, a y rypa 1 spec 1:1
    }
}
