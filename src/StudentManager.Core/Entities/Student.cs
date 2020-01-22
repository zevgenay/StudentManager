using StudentManager.Core.Entities.Enums;
using StudentManager.SharedKernel;
using System.Collections.Generic;

namespace StudentManager.Core.Entities
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public Gender Gender { get; set; }

        public string NickName { get; set; }

        public List<StudentGroup> StudentGroups { get; set; }

        public Student()
        {
            StudentGroups = new List<StudentGroup>();
        }
    }
}
