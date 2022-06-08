using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace school_itg_shadi.Models
{
    public partial class Client
    {
        public Client()
        {
            Associations = new HashSet<Association>();
            StudentMarkStudentNameNavigations = new HashSet<StudentMark>();
            StudentMarkStudents = new HashSet<StudentMark>();
        }

        public string Name { get; set; } = null!;
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int? GenderId { get; set; }
        public int? TypeId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        public virtual Gender? Gender { get; set; }
        public virtual Type? Type { get; set; }
        public virtual ICollection<Association> Associations { get; set; }
        public virtual ICollection<StudentMark> StudentMarkStudentNameNavigations { get; set; }
        public virtual ICollection<StudentMark> StudentMarkStudents { get; set; }
    }
}
