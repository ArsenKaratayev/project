using System;
using System.Collections.Generic;
using EF.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EF.Models
{
    public class RypVM
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Date { get; set; }
        public string UpdateDate { get; set; }
        public int OperatorCheck { get; set; }
        public int FullCheck { get; set; }
        public int Deleted { get; set; } = 0;
        public int Prototype { get; set; } = 0;

        public string UserId { get; set; }
        public Specialty Specialty { get; set; }
        public List<SemesterVM> Semesters { get; set; }
    }

    public class CheckedRyp
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int RypId { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Date { get; set; }
        public string UpdateDate { get; set; }
        public int OperatorCheck { get; set; }
        public int FullCheck { get; set; }
        public int isOpen { get; set; } = 0;

        public string UserId { get; set; }
        public Specialty Specialty { get; set; }
        public List<SemesterVM> Semesters { get; set; }
    }

    public class SemesterVM
    {
        public List<SubjectVM> Subjects { get; set; }
        public List<ElectiveGroupVM> Electives { get; set; }
    }
}
